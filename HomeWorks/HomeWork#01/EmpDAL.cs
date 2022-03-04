using System.Collections.Generic;
using System.IO;
using BussinessObjects;

namespace DataAccessLayer
{
    public class EmpDAL
    {
        public void saveEmp(EmpBussinessObjs bussObj)
        {
            string data = bussObj.name + "," + bussObj.age + "," + bussObj.salary + "," + bussObj.tax;
            StreamWriter sw = new StreamWriter("file.txt",append:true);
            sw.WriteLine(data);
            sw.Close();
        }
        //return list of emps
        public List<EmpBussinessObjs> readEmp()
        {
            List<EmpBussinessObjs> list = new List<EmpBussinessObjs>();
            StreamReader sw = new StreamReader("file.txt");
            string Objets = "";
            string data = sw.ReadLine();
            while(data != null)
            {
                
                Objets += data;
                Objets += "\n";
                data = sw.ReadLine();
            }
            sw.Close();
            string [] objList = Objets.Split("\n");
            foreach(string objj in objList)
            {
                string[] dataList = objj.Split(",");
                if(dataList.Length > 2)
                {
                    EmpBussinessObjs obj = new EmpBussinessObjs();
                    obj.name = dataList[0];
                    obj.age = int.Parse(dataList[1]);
                    obj.salary = decimal.Parse(dataList[2]);
                    obj.tax = decimal.Parse(dataList[3]);
                    list.Add(obj);
                }
            }
            return list;
        }
    }
}
