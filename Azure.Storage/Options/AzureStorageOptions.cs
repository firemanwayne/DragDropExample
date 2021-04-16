namespace Azure.CustomStorage.Options
{
    public class AzureStorageOptions
    {
        bool isDevelopment;
        const string DevelopmentAccountName = "devstoreaccount1";
        const string DevelopmentAccountKey = "Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==";
        const string DevelopmentBlobEndpoint = "BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;";
        const string DevelopmentQueueEndpoint = "QueueEndpoint=http://127.0.0.1:10001/devstoreaccount1;";
        const string DevelopmentTableEndpoint = "TableEndpoint=http://127.0.0.1:10002/devstoreaccount1;";

        public bool IsDevelopment
        {
            get => isDevelopment;
            set 
            {
                isDevelopment = value;

                if(value)
                {
                    AccountKey = DevelopmentAccountKey;
                    AccountName = DevelopmentAccountName;
                }
            }
        }
        public string AccountKey { get; set; }
        public string AccountName { get; set; }
        public string ConnectionString 
        { 
            get => isDevelopment ?
                $"AccountName={AccountName};AccountKey={AccountKey};DefaultEndpointsProtocol=http;{DevelopmentBlobEndpoint};{DevelopmentQueueEndpoint};{DevelopmentTableEndpoint};" :
                $"AccountName={AccountName}; AccountKey={AccountKey}; DefaultEndpointsProtocol=http;";
        }
    }
}
