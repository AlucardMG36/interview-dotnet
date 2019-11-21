using GroceryStore.Core.Entities;
using GroceryStore.Core.Helpers;
using System.IO;

namespace GroceryStore.Data.Readers
{
    internal static class JsonFileReader
    {
        private const string _filePath ="database.json";
      
        public static RootObject RootObject { get => ReadJsonDataBaseFile(); }

        private static RootObject ReadJsonDataBaseFile()
        {
            using var reader = new StreamReader(_filePath);

            var json = reader.ReadToEnd();

            return JsonHelper.ToClass<RootObject>(json);
        }
    }
}
