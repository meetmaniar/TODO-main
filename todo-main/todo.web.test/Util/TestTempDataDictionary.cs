using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace todo.web.test.Util
{
    internal class TestTempDataDictionary : Dictionary<string, object?>, ITempDataDictionary
    {
        public void Keep() { }
        public void Keep(string key) { }
        public void Load() { }
        public object? Peek(string key) => this[key];
        public void Save() { }
    }
}
