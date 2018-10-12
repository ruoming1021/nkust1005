using System;
using opendata.model;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
namespace opendata
{
    class Program
    {
        static void Main(string[] args)
        {
            var nodes = findopendata();
            showopendata(nodes);
            Console.ReadKey();

        }
        private static void showopendata(List<Class1> nodes)
        {
            Console.WriteLine(string.Format("共收到{0}筆資料", nodes.Count));
            nodes.GroupBy(node => node.資料集名稱).ToList()
                .ForEach(group =>
                {
                    var key = group.Key;
                    var groupdatas = group.ToList();
                    var message = $"資料集名稱:{key},共有{groupdatas.Count()}筆資料";
                    Console.WriteLine(message);

                });
        }

        static List<opendata.model.Class1> findopendata()
        {
            List<opendata.model.Class1> result = new List<Class1>();
            var xml = XElement.Load(@"C:\Users\user\Downloads\datagovtw_dataset_20181012 (3).xml");

            var nodes = xml.Descendants("node").ToList();

            for (var i = 0; i < nodes.Count; i++)
            {
                var node = nodes[i];
                opendata.model.Class1 item = new opendata.model.Class1();
                item.id = int.Parse(getvalue(node, "id"));
                item.服務分類 = getvalue(node, "服務分類");
                item.資料集名稱 = getvalue(node, "資料集名稱");
                result.Add(item);
            }
            return result;
        }

        private static string getvalue(XElement node, string x)
        {
            return node.Element(x)?.Value?.Trim();
        }
    }
}
