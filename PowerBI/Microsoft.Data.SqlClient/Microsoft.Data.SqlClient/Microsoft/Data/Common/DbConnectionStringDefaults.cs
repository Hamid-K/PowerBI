using System;
using Microsoft.Data.SqlClient;

namespace Microsoft.Data.Common
{
	// Token: 0x0200017A RID: 378
	internal static class DbConnectionStringDefaults
	{
		// Token: 0x04000B80 RID: 2944
		internal const ApplicationIntent ApplicationIntent = ApplicationIntent.ReadWrite;

		// Token: 0x04000B81 RID: 2945
		internal const string ApplicationName = "Framework Microsoft SqlClient Data Provider";

		// Token: 0x04000B82 RID: 2946
		internal const string AttachDBFilename = "";

		// Token: 0x04000B83 RID: 2947
		internal const int CommandTimeout = 30;

		// Token: 0x04000B84 RID: 2948
		internal const int ConnectTimeout = 15;

		// Token: 0x04000B85 RID: 2949
		internal const bool ConnectionReset = true;

		// Token: 0x04000B86 RID: 2950
		internal const bool ContextConnection = false;

		// Token: 0x04000B87 RID: 2951
		internal static readonly bool TransparentNetworkIPResolution = !LocalAppContextSwitches.DisableTNIRByDefault;

		// Token: 0x04000B88 RID: 2952
		internal const string NetworkLibrary = "";

		// Token: 0x04000B89 RID: 2953
		internal const string CurrentLanguage = "";

		// Token: 0x04000B8A RID: 2954
		internal const string DataSource = "";

		// Token: 0x04000B8B RID: 2955
		internal static readonly SqlConnectionEncryptOption Encrypt = SqlConnectionEncryptOption.Mandatory;

		// Token: 0x04000B8C RID: 2956
		internal const string HostNameInCertificate = "";

		// Token: 0x04000B8D RID: 2957
		internal const string ServerCertificate = "";

		// Token: 0x04000B8E RID: 2958
		internal const bool Enlist = true;

		// Token: 0x04000B8F RID: 2959
		internal const string FailoverPartner = "";

		// Token: 0x04000B90 RID: 2960
		internal const string InitialCatalog = "";

		// Token: 0x04000B91 RID: 2961
		internal const bool IntegratedSecurity = false;

		// Token: 0x04000B92 RID: 2962
		internal const int LoadBalanceTimeout = 0;

		// Token: 0x04000B93 RID: 2963
		internal const bool MultipleActiveResultSets = false;

		// Token: 0x04000B94 RID: 2964
		internal const bool MultiSubnetFailover = false;

		// Token: 0x04000B95 RID: 2965
		internal const int MaxPoolSize = 100;

		// Token: 0x04000B96 RID: 2966
		internal const int MinPoolSize = 0;

		// Token: 0x04000B97 RID: 2967
		internal const int PacketSize = 8000;

		// Token: 0x04000B98 RID: 2968
		internal const string Password = "";

		// Token: 0x04000B99 RID: 2969
		internal const bool PersistSecurityInfo = false;

		// Token: 0x04000B9A RID: 2970
		internal const bool Pooling = true;

		// Token: 0x04000B9B RID: 2971
		internal const bool TrustServerCertificate = false;

		// Token: 0x04000B9C RID: 2972
		internal const string TypeSystemVersion = "Latest";

		// Token: 0x04000B9D RID: 2973
		internal const string UserID = "";

		// Token: 0x04000B9E RID: 2974
		internal const bool UserInstance = false;

		// Token: 0x04000B9F RID: 2975
		internal const bool Replication = false;

		// Token: 0x04000BA0 RID: 2976
		internal const string WorkstationID = "";

		// Token: 0x04000BA1 RID: 2977
		internal const string TransactionBinding = "Implicit Unbind";

		// Token: 0x04000BA2 RID: 2978
		internal const int ConnectRetryCount = 1;

		// Token: 0x04000BA3 RID: 2979
		internal const int ConnectRetryInterval = 10;

		// Token: 0x04000BA4 RID: 2980
		internal static readonly SqlAuthenticationMethod Authentication = SqlAuthenticationMethod.NotSpecified;

		// Token: 0x04000BA5 RID: 2981
		internal const SqlConnectionColumnEncryptionSetting ColumnEncryptionSetting = SqlConnectionColumnEncryptionSetting.Disabled;

		// Token: 0x04000BA6 RID: 2982
		internal const string EnclaveAttestationUrl = "";

		// Token: 0x04000BA7 RID: 2983
		internal const SqlConnectionAttestationProtocol AttestationProtocol = SqlConnectionAttestationProtocol.NotSpecified;

		// Token: 0x04000BA8 RID: 2984
		internal const SqlConnectionIPAddressPreference IPAddressPreference = SqlConnectionIPAddressPreference.IPv4First;

		// Token: 0x04000BA9 RID: 2985
		internal const PoolBlockingPeriod PoolBlockingPeriod = PoolBlockingPeriod.Auto;

		// Token: 0x04000BAA RID: 2986
		internal const string ServerSPN = "";

		// Token: 0x04000BAB RID: 2987
		internal const string FailoverPartnerSPN = "";
	}
}
