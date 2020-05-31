using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Json;
using CarCenterBusinessLogic.Enums;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace CarCenterBusinessLogic.BusinessLogic
{
    public abstract class BackUpAbstractLogic
    {
        public void CreateDir(string folderPath, BackUpTypeEnum backUpType)
        {
            try
            {
                if(Directory.Exists(folderPath + $"/{backUpType}BackUp"))
                {
                    foreach (var file in Directory.GetFiles(folderPath + $"/{backUpType}BackUp"))
                        File.Delete(file);
                }else
                    Directory.CreateDirectory(folderPath + $"/{backUpType}BackUp");

                MethodInfo method = default;
                switch (backUpType)
                {
                    case BackUpTypeEnum.Json:
                        method = GetType().BaseType.GetTypeInfo().GetDeclaredMethod("CreateJsonBackUp");
                        break;
                    case BackUpTypeEnum.Xml:
                        method = GetType().BaseType.GetTypeInfo().GetDeclaredMethod("CreateXmlBackUp");
                        break;
                }

                foreach(var set in GetSetsOfModels())
                {
                    var genericMethod = method.MakeGenericMethod(
                        GetAssembly().CreateInstance(set.PropertyType.GenericTypeArguments[0].FullName).GetType());
                    genericMethod.Invoke(this, new object[] { folderPath });
                }
            }
            catch (Exception) { throw; }
        }

        private void CreateJsonBackUp<T>(string folderPath) where T: class, new()
        {
            var records = GetSetValues<T>();
            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(List<T>));
            using(FileStream fs = new FileStream(string.Format("{0}/jsonBackUp/{1}.json", folderPath, new T().GetType().Name), FileMode.OpenOrCreate))
            {
                jsonSerializer.WriteObject(fs, records);
            }
        }

        private void CreateXmlBackUp<T>(string folderPath) where T : class, new()
        {
            var records = GetSetValues<T>();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<T>));
            using(FileStream fs = new FileStream(string.Format("{0}/xmlBackUp/{1}.xml", folderPath, new T().GetType().Name), FileMode.OpenOrCreate))
            {
                xmlSerializer.Serialize(fs, records);
            }
        }

        protected abstract Assembly GetAssembly();
        protected abstract List<PropertyInfo> GetSetsOfModels();
        protected abstract List<T> GetSetValues<T>() where T: class, new(); 
    }
}
