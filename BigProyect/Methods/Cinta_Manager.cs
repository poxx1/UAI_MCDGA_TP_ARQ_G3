using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using APIs;
using System.Text;
using System.Threading.Tasks;

namespace APIs
{
    public class Cinta_Manager
    {
        //Capa intermedia entre el BD Connector y el Main module

        MongoBD mongo = new MongoBD();
        public bool Insert(int id)
        {
            mongo.bultosInsert(id);
            return true;
        }
        public bool Delete(string mongoId)
        {
            mongo.bultosDelete(mongoId);
            return true;
        }
        public List<Bultos> lstBultos()
        {
            return mongo.getBultos();
        }
        public int GetBultosQuantity()
        {
            return mongo.getBultosQuantity();
        }
    }
}