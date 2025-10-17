using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerimalQuest.System
{
    public class ItemData
    {
        public int id { get; set; }
        public string name { get; set; }
        public string desc { get; set; }
        public int price { get; set; }
        public string type { get; set; }  
        public float atk { get; set; }
        public float def { get; set; }
        public float healAmount { get; set; }
        public string potionType { get; set; }
        public int lastId { get; set; }
    }

    public class ItemDataList
    {
        public List<ItemData> items { get; set; }
    }
}
