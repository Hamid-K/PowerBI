using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x02000054 RID: 84
	[DataContract]
	[Flags]
	public enum GatewayStaticCapabilities : long
	{
		// Token: 0x040001E1 RID: 481
		[EnumMember]
		None = 0L,
		// Token: 0x040001E2 RID: 482
		[EnumMember]
		FetchDataSourceDetails = 1L,
		// Token: 0x040001E3 RID: 483
		[EnumMember]
		SchemaRowsets = 2L,
		// Token: 0x040001E4 RID: 484
		[EnumMember]
		MdDataset = 4L,
		// Token: 0x040001E5 RID: 485
		[EnumMember]
		ColumnOrdinals = 8L,
		// Token: 0x040001E6 RID: 486
		[EnumMember]
		StatefulSessions = 16L,
		// Token: 0x040001E7 RID: 487
		[EnumMember]
		DirectQuerySingleSignOn_Kerberos = 32L,
		// Token: 0x040001E8 RID: 488
		[EnumMember]
		MashupCustomConnectors = 64L,
		// Token: 0x040001E9 RID: 489
		[EnumMember]
		MashupCustomConnectorsForODG = 128L,
		// Token: 0x040001EA RID: 490
		[EnumMember]
		SAPHanaSSOManagedProvider = 256L,
		// Token: 0x040001EB RID: 491
		[EnumMember]
		FilteringMashupExtensionsOnODG = 512L,
		// Token: 0x040001EC RID: 492
		[EnumMember]
		CertifiedConnectors = 1024L,
		// Token: 0x040001ED RID: 493
		[EnumMember]
		MashupAzureConnectorsCachingPool = 2048L,
		// Token: 0x040001EE RID: 494
		[EnumMember]
		SkipTestConnection = 4096L,
		// Token: 0x040001EF RID: 495
		[EnumMember]
		EncryptedConnectionOptionOverride = 8192L,
		// Token: 0x040001F0 RID: 496
		[EnumMember]
		MashupADONetProviderSSO = 16384L,
		// Token: 0x040001F1 RID: 497
		[EnumMember]
		AuthTypeSAS = 32768L,
		// Token: 0x040001F2 RID: 498
		[EnumMember]
		PowerQueryOnlineCachingPool = 65536L,
		// Token: 0x040001F3 RID: 499
		[EnumMember]
		SSOTestConnection = 131072L,
		// Token: 0x040001F4 RID: 500
		[EnumMember]
		MashupIgnorePreviouslyCachedData = 262144L,
		// Token: 0x040001F5 RID: 501
		[EnumMember]
		MashupQueryStatus = 524288L,
		// Token: 0x040001F6 RID: 502
		[EnumMember]
		MultipleResults = 1048576L,
		// Token: 0x040001F7 RID: 503
		[EnumMember]
		RenewDiagnosticsStorageAccess = 2097152L,
		// Token: 0x040001F8 RID: 504
		[EnumMember]
		OAuthResource = 4194304L,
		// Token: 0x040001F9 RID: 505
		[EnumMember]
		StatefulDbConnection = 8388608L,
		// Token: 0x040001FA RID: 506
		[EnumMember]
		DiscoverDatasourcesUsingMashupDSR = 16777216L,
		// Token: 0x040001FB RID: 507
		[EnumMember]
		XLPivotTable = 31L
	}
}
