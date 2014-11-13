using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

public sealed class JsonDotNetValueProviderFactory : ValueProviderFactory
{
    public override IValueProvider GetValueProvider(ControllerContext controllerContext)
    {
        if (controllerContext == null)
        {
            throw new ArgumentNullException("controllerContext");
        }

        var jsonData = GetDeserializedObject(controllerContext);
        if (jsonData == null)
        {
            return null;
        }

        var backingStore = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
        AddToBackingStore(backingStore, String.Empty, jsonData);
        return new DictionaryValueProvider<object>(backingStore, CultureInfo.CurrentCulture);
    }

    private static object GetDeserializedObject(ControllerContext controllerContext)
    {
        if (!controllerContext.HttpContext.Request.IsAjaxRequest()) return null;

        StreamReader reader = new StreamReader(controllerContext.HttpContext.Request.InputStream);
        string bodyText = reader.ReadToEnd().Trim();
        if (string.IsNullOrEmpty(bodyText))
        {
            // no JSON data
            return null;
        }

        object retVal;

        if (bodyText.StartsWith("[") && bodyText.EndsWith("]"))
        {
            //base element is an array
            IList<object> obj = JsonConvert.DeserializeObject<IList<object>>(bodyText, new ExpandoObjectConverter());

            //the above conversion will leave nested objects as JContainers, so we need to locate them 
            //and convert them to ExpandoObject instances, but leave everything else alone
            for (int c = 0; c < obj.Count; c++)
            {
                if (obj[c] is JContainer)
                {
                    //convert the JContainer to an expando object
                    obj[c] = ((JContainer)obj[c]).ToObject<ExpandoObject>();
                }
            }

            retVal = obj;
        }
        else if (bodyText.StartsWith("{") && bodyText.EndsWith("}"))
        {
            //base element is an object
            retVal = JsonConvert.DeserializeObject<ExpandoObject>(bodyText, new ExpandoObjectConverter());
        }
        else
        {
            //base element is a primitive type... technically not correct, but let's not ignore it
            retVal = JsonConvert.DeserializeObject<object>(bodyText);
        }

        return retVal;
    }

    private static void AddToBackingStore(IDictionary<string, object> backingStore, string prefix, object value)
    {
        var d = value as IDictionary<string, object>;
        if (d != null)
        {
            foreach (var entry in d)
            {
                AddToBackingStore(backingStore, MakePropertyKey(prefix, entry.Key), entry.Value);
            }
            return;
        }

        var l = value as IList;
        if (l != null)
        {
            for (var i = 0; i < l.Count; i++)
            {
                AddToBackingStore(backingStore, MakeArrayKey(prefix, i), l[i]);
            }
            return;
        }

        // primitive
        backingStore.Add(prefix, value);
    }

    private static string MakeArrayKey(string prefix, int index)
    {
        return prefix + "[" + index.ToString(CultureInfo.InvariantCulture) + "]";
    }

    private static string MakePropertyKey(string prefix, string propertyName)
    {
        return (String.IsNullOrEmpty(prefix)) ? propertyName : prefix + "." + propertyName;
    }
}