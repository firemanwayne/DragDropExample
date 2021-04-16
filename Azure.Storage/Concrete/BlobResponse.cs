using System;
using System.Collections.Generic;

namespace Azure.CustomStorage
{
    public class BlobResponse
    {
        public string Name { get; set; }       
        public bool Deleted { get; set; }      
        public string Snapshot { get; set; }      
        public string VersionId { get; set; }      
        public bool? IsLatestVersion { get; set; }      
        public BlobPropertiesResponse Properties { get; set; }
        public IDictionary<string, string> Metadata { get; set; }       
        public IDictionary<string, string> Tags { get; set; }
    }

    public class BlobPropertiesResponse
    {
        public DateTimeOffset? LastModified { get; set; }
        public string DestinationSnapshot { get; set; }
        public int? RemainingRetentionDays { get; set; }              
        public bool AccessTierInferred { get; set; }              
        public string CustomerProvidedKeySha256 { get; set; }       
        public string EncryptionScope { get; set; }      
        public long? TagCount { get; set; }        
        public DateTimeOffset? ExpiresOn { get; set; }        
        public bool? IsSealed { get; set; }               
        public DateTimeOffset? LastAccessedOn { get; set; }       
        public ETag? ETag { get; set; }       
        public DateTimeOffset? CreatedOn { get; set; }        
        public DateTimeOffset? CopyCompletedOn { get; set; }        
        public bool? IncrementalCopy { get; set; }        
        public DateTimeOffset? DeletedOn { get; set; }       
        public bool? ServerEncrypted { get; set; }       
        public string CopyProgress { get; set; }      
        public long? ContentLength { get; set; }      
        public string ContentType { get; set; }      
        public string ContentEncoding { get; set; }     
        public string ContentLanguage { get; set; }        
        public byte[] ContentHash { get; set; }      
        public string ContentDisposition { get; set; }      
        public string CacheControl { get; set; }      
        public long? BlobSequenceNumber { get; set; }        
        public string CopyId { get; set; }            
        public Uri CopySource { get; set; }       
        public string CopyStatusDescription { get; set; }        
        public DateTimeOffset? AccessTierChangedOn { get; set; }
    }
}
