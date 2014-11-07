using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoolBan.Models.Abstract
{
    interface IColumnRepository
    {
        Column Find(int id);
        void Delete(int id);
        void Create(Column column);
        void Edit(Column column);
        void Save();
    }
}
