using GroceryStore.Core.Helpers;
using GroceryStore.Data.Readers;
using Newtonsoft.Json.Linq;
using System.IO;

namespace GroceryStore.Data.Writers
{
    internal static class JsonDatabaseFileWriter
    {
        private const string _filePath = "database.json";

        public static void WriteDataToDatabase<T>(T entity)
        {
            var valueAccessName = ($"{typeof(T).Name}s").ToLower();
            
            var json = JsonHelper.FromClass(JsonFileReader.RootObject);

            var jsonObj = JObject.Parse(json);

            var valueArray = jsonObj.GetValue(valueAccessName) as JArray;

            var valueToAddToDatabaseFile = JObject.Parse(entity.ToString());

            valueArray.Add(valueToAddToDatabaseFile);

            jsonObj[valueAccessName] = valueArray;

            var newJsonResult = JsonHelper.FromClass(jsonObj);

            using var writer = new StreamWriter(_filePath);

            writer.Write(newJsonResult);
        }
    }
}
