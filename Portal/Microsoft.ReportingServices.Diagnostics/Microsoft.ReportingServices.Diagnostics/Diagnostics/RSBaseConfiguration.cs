using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000038 RID: 56
	[Serializable]
	internal abstract class RSBaseConfiguration
	{
		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000109 RID: 265
		// (set) Token: 0x0600010A RID: 266
		public abstract long CurrentSequence { get; set; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600010B RID: 267
		public abstract string ConfigFilePath { get; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600010C RID: 268
		public abstract string ConfigFileName { get; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600010D RID: 269
		public abstract string BaseConnectionString { get; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600010E RID: 270
		public abstract RSBaseConfiguration.CatalogConnectionType ConnectionType { get; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600010F RID: 271
		public abstract RSBaseConfiguration.CatalogConnectionAuth ConnectionAuth { get; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000110 RID: 272
		public abstract string CatalogUser { get; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000111 RID: 273
		public abstract string CatalogDomain { get; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000112 RID: 274
		public abstract string CatalogCred { get; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000113 RID: 275
		public abstract int DBQueryTimeout { get; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000114 RID: 276
		public abstract int ProcessTimeout { get; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000115 RID: 277
		public abstract int ProcessTimeoutGcExtension { get; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000116 RID: 278
		public abstract int DBCleanupTimeout { get; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000117 RID: 279
		public abstract int DBCleanupBatchFactor { get; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000118 RID: 280
		public abstract string InstanceID { get; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000119 RID: 281
		public abstract string InstanceName { get; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600011A RID: 282
		public abstract Guid InstallationID { get; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600011B RID: 283
		public abstract Guid InstallationIDWebApp { get; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600011C RID: 284
		public abstract string RpcEndpoint { get; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600011D RID: 285
		public abstract string WindowsServiceName { get; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600011E RID: 286
		public abstract int WatsonFlags { get; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600011F RID: 287
		public abstract StringCollection WatsonDumpOnExceptions { get; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000120 RID: 288
		public abstract StringCollection WatsonDumpExcludeIfContainsExceptions { get; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000121 RID: 289
		public abstract Dictionary<RunningApplication, UrlConfiguration> UrlConfiguration { get; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000122 RID: 290
		public abstract AuthenticationTypes AuthenticationTypes { get; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000123 RID: 291
		public abstract LogonMethod LogonMethod { get; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000124 RID: 292
		public abstract string AuthRealm { get; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000125 RID: 293
		public abstract string AuthDomain { get; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000126 RID: 294
		public abstract bool AuthPersistence { get; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000127 RID: 295
		public abstract bool IsWebServiceEnabled { get; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000128 RID: 296
		public abstract bool IsReportBuilderAnonymousAccessEnabled { get; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000129 RID: 297
		public abstract int RequestCacheSlots { get; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600012A RID: 298
		public abstract ExtendedProtectionLevel ExtendedProtectionLevel { get; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600012B RID: 299
		public abstract ExtendedProtectionScenario ExtendedProtectionScenario { get; }

		// Token: 0x02000095 RID: 149
		public enum CatalogConnectionType
		{
			// Token: 0x04000386 RID: 902
			Default,
			// Token: 0x04000387 RID: 903
			Impersonate
		}

		// Token: 0x02000096 RID: 150
		public enum CatalogConnectionAuth
		{
			// Token: 0x04000389 RID: 905
			Windows,
			// Token: 0x0400038A RID: 906
			Sql
		}
	}
}
