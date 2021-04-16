using System.Configuration;
using System.Runtime.Serialization;

namespace RedisCache
{
    public sealed class RedisCacheConfiguration : ConfigurationSection
    {
        [DataMember(Name = "Host")]
        [ConfigurationProperty("Host", DefaultValue = "", IsRequired = false)]
        public string Host { get; set; }

        [DataMember(Name = "SSLPort")]
        [ConfigurationProperty("SSLPort", DefaultValue = "", IsRequired = false)]
        public string SSLPort { get; set; }

        [DataMember(Name = "Settings")]
        [ConfigurationProperty("Settings", IsRequired = true)]
        public CacheConfiguration[] Settings { get; set; }

    }

    public class CacheConfiguration
    {
        [DataMember(Name = "Name")]
        [ConfigurationProperty("Name", DefaultValue = "RedisCacheConfiguration", IsRequired = false)]
        public string Name { get; set; }

        [DataMember(Name = "Key")]
        [ConfigurationProperty("Key", DefaultValue = "", IsRequired = true)]
        public string Key { get; set; }

        [DataMember(Name = "ConnectionString")]
        [ConfigurationProperty("ConnectionString", DefaultValue = "", IsRequired = true)]
        public string ConnectionString { get; set; }
    }
}
