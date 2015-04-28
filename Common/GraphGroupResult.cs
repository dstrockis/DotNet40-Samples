using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;


// This class is just used for deserializing the JSON result from the Graph API.
namespace Common
{
    [DataContract]
    public class GraphGroupResult
    {
        [DataMember(Name= "odata.metadata")]
        public string odatametadata { get; set; }
        public List<Value> value { get; set; }
    }

    [DataContract]
    public class Value
    {
        [DataMember(Name = "odata.type")]
        public string odatatype { get; set; }
        public string objectType { get; set; }
        public string objectId { get; set; }
        public object deletionTimestamp { get; set; }
        public string description { get; set; }
        public bool? dirSyncEnabled { get; set; }
        public string displayName { get; set; }
        public string lastDirSyncTime { get; set; }
        public object mail { get; set; }
        public string mailNickname { get; set; }
        public bool mailEnabled { get; set; }
        public string onPremisesSecurityIdentifier { get; set; }
        public List<object> provisioningErrors { get; set; }
        public List<object> proxyAddresses { get; set; }
        public bool securityEnabled { get; set; }
    }
}
