using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LTTQ_DoAn.Model
{
    public interface IVictimRepository
    {        
        void Add(VictimModel victimModel);
        void Edit(UserModel victimModel);
        void Remove(int id);
        VictimModel GetById(int id);
        VictimModel GetByUsername(string username);
        IEnumerable<VictimModel> GetByAll();
        //...
    }
}
