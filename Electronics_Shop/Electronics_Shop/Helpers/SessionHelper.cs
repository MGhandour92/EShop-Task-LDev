using Newtonsoft.Json;

namespace Electronics_Shop.Helpers
{
    public static class SessionHelper
    {
        /// <summary>
        /// Building session with key and value as json data
        /// </summary>
        /// <param name="session">session obj</param>
        /// <param name="key">Key name</param>
        /// <param name="value">Object value to be converted to json</param>
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        /// <summary>
        /// Read the session saved earlier
        /// </summary>
        /// <typeparam name="T">The return type that will be deserialized from json saved</typeparam>
        /// <param name="session">session obj</param>
        /// <param name="key">Key name</param>
        /// <returns></returns>
        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
