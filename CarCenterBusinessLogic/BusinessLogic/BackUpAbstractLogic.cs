using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Json;
using CarCenterBusinessLogic.Enums;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Linq;
using DocumentFormat.OpenXml.EMMA;

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

        public void LoadBackUp(string folderPath, BackUpTypeEnum backUpType)
        {
            try
            {
                var filesPaths = Directory.GetFiles(folderPath);
                if (filesPaths.Count() == 0)
                    throw new Exception("Выбранная папка не содержит файлов");
                MethodInfo method = default;
                switch (backUpType)
                {
                    case BackUpTypeEnum.Json:
                        method = GetType().BaseType.GetTypeInfo().GetDeclaredMethod("DeserializeSetJsonBackUp");
                        break;
                    case BackUpTypeEnum.Xml:
                        method = GetType().BaseType.GetTypeInfo().GetDeclaredMethod("DeserializeSetXmlBackUp");
                        break;
                }
                var filesNames = filesPaths
                    .Select(f => f.Substring(folderPath.Length + 1, f.Length - folderPath.Length - backUpType.ToString().Length - 2))
                    .ToList();
                var models = GetAssembly().GetTypes()
                    .Where(t => t.FullName.StartsWith("CarCenterImplementation.Models."));
                var modelsNames = models.Select(m => m.Name);
                foreach (var model in modelsNames)
                    if (!filesNames.Contains(model))
                        throw new Exception($"Отсутствует файл {model}.{backUpType.ToString().ToLower()}");
                var sortedModels = models
                    .OrderBy(m => m.GetProperties().Where(p => modelsNames.Contains(p.PropertyType.Name)).Count());
                foreach(var model in sortedModels)
                {
                    var genericMethod = method.MakeGenericMethod(model);
                    genericMethod.Invoke(this, new object[] { 
                        filesPaths[filesNames.IndexOf(filesNames.FirstOrDefault(f => f.Equals(model.Name)))]
                    });
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

        private void DeserializeSetJsonBackUp<T>(string filePath) where T: class, new()
        {
            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(List<T>));
            using(FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                InsertSetValues<T>((List<T>)jsonSerializer.ReadObject(fs));
            }
        }

        private void DeserializeSetXmlBackUp<T>(string filePath) where T : class, new()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<T>));
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                InsertSetValues<T>((List<T>)xmlSerializer.Deserialize(fs));
            }
        }

        protected abstract Assembly GetAssembly();
        protected abstract List<PropertyInfo> GetSetsOfModels();
        protected abstract List<T> GetSetValues<T>() where T: class, new();
        protected abstract void InsertSetValues<T>(List<T> records) where T : class, new();
    }
}
