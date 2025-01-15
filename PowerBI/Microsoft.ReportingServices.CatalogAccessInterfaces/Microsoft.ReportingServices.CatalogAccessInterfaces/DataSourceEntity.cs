using System;

namespace Microsoft.ReportingServices.CatalogAccess
{
	// Token: 0x02000007 RID: 7
	public class DataSourceEntity
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002256 File Offset: 0x00000456
		// (set) Token: 0x06000042 RID: 66 RVA: 0x0000225E File Offset: 0x0000045E
		public Guid DSID { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002267 File Offset: 0x00000467
		// (set) Token: 0x06000044 RID: 68 RVA: 0x0000226F File Offset: 0x0000046F
		public Guid? ItemID { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002278 File Offset: 0x00000478
		// (set) Token: 0x06000046 RID: 70 RVA: 0x00002280 File Offset: 0x00000480
		public string Name { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002289 File Offset: 0x00000489
		// (set) Token: 0x06000048 RID: 72 RVA: 0x00002291 File Offset: 0x00000491
		public string Extension { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000049 RID: 73 RVA: 0x0000229A File Offset: 0x0000049A
		// (set) Token: 0x0600004A RID: 74 RVA: 0x000022A2 File Offset: 0x000004A2
		public Guid? Link { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600004B RID: 75 RVA: 0x000022AB File Offset: 0x000004AB
		// (set) Token: 0x0600004C RID: 76 RVA: 0x000022B3 File Offset: 0x000004B3
		public DataSourceEntity.CredentialsRetrievalOption CredentialRetrieval { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600004D RID: 77 RVA: 0x000022BC File Offset: 0x000004BC
		// (set) Token: 0x0600004E RID: 78 RVA: 0x000022C4 File Offset: 0x000004C4
		public string Prompt { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600004F RID: 79 RVA: 0x000022CD File Offset: 0x000004CD
		// (set) Token: 0x06000050 RID: 80 RVA: 0x000022D5 File Offset: 0x000004D5
		public byte[] ConnectionString { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000051 RID: 81 RVA: 0x000022DE File Offset: 0x000004DE
		// (set) Token: 0x06000052 RID: 82 RVA: 0x000022E6 File Offset: 0x000004E6
		public byte[] OriginalConnectionString { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000053 RID: 83 RVA: 0x000022EF File Offset: 0x000004EF
		// (set) Token: 0x06000054 RID: 84 RVA: 0x000022F7 File Offset: 0x000004F7
		public byte[] UserName { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00002300 File Offset: 0x00000500
		// (set) Token: 0x06000056 RID: 86 RVA: 0x00002308 File Offset: 0x00000508
		public byte[] Password { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00002311 File Offset: 0x00000511
		// (set) Token: 0x06000058 RID: 88 RVA: 0x00002319 File Offset: 0x00000519
		public DataSourceEntity.DataSourceFlags Flags { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00002322 File Offset: 0x00000522
		// (set) Token: 0x0600005A RID: 90 RVA: 0x0000232A File Offset: 0x0000052A
		public Guid? DSLinkDSID { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002333 File Offset: 0x00000533
		// (set) Token: 0x0600005C RID: 92 RVA: 0x0000233B File Offset: 0x0000053B
		public Guid? DSLinkItemId { get; set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00002344 File Offset: 0x00000544
		// (set) Token: 0x0600005E RID: 94 RVA: 0x0000234C File Offset: 0x0000054C
		public string DSLinkName { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002355 File Offset: 0x00000555
		// (set) Token: 0x06000060 RID: 96 RVA: 0x0000235D File Offset: 0x0000055D
		public string DSLinkExtension { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00002366 File Offset: 0x00000566
		// (set) Token: 0x06000062 RID: 98 RVA: 0x0000236E File Offset: 0x0000056E
		public Guid? DSLinkLink { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00002377 File Offset: 0x00000577
		// (set) Token: 0x06000064 RID: 100 RVA: 0x0000237F File Offset: 0x0000057F
		public int? DSLinkCredentialRetrieval { get; set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00002388 File Offset: 0x00000588
		// (set) Token: 0x06000066 RID: 102 RVA: 0x00002390 File Offset: 0x00000590
		public string DSLinkPrompt { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00002399 File Offset: 0x00000599
		// (set) Token: 0x06000068 RID: 104 RVA: 0x000023A1 File Offset: 0x000005A1
		public byte[] DSLinkConnectionString { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000069 RID: 105 RVA: 0x000023AA File Offset: 0x000005AA
		// (set) Token: 0x0600006A RID: 106 RVA: 0x000023B2 File Offset: 0x000005B2
		public byte[] DSLinkUserName { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600006B RID: 107 RVA: 0x000023BB File Offset: 0x000005BB
		// (set) Token: 0x0600006C RID: 108 RVA: 0x000023C3 File Offset: 0x000005C3
		public byte[] DSLinkPassword { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600006D RID: 109 RVA: 0x000023CC File Offset: 0x000005CC
		// (set) Token: 0x0600006E RID: 110 RVA: 0x000023D4 File Offset: 0x000005D4
		public int? DSLinkFlags { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600006F RID: 111 RVA: 0x000023DD File Offset: 0x000005DD
		// (set) Token: 0x06000070 RID: 112 RVA: 0x000023E5 File Offset: 0x000005E5
		public string Path { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000071 RID: 113 RVA: 0x000023EE File Offset: 0x000005EE
		// (set) Token: 0x06000072 RID: 114 RVA: 0x000023F6 File Offset: 0x000005F6
		public byte[] NtSecDescPrimary { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000073 RID: 115 RVA: 0x000023FF File Offset: 0x000005FF
		// (set) Token: 0x06000074 RID: 116 RVA: 0x00002407 File Offset: 0x00000607
		public bool? OriginalConnectStringExpressionBased { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00002410 File Offset: 0x00000610
		// (set) Token: 0x06000076 RID: 118 RVA: 0x00002418 File Offset: 0x00000618
		public int Version { get; set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00002421 File Offset: 0x00000621
		// (set) Token: 0x06000078 RID: 120 RVA: 0x00002429 File Offset: 0x00000629
		public int? DSLinkVersion { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00002432 File Offset: 0x00000632
		// (set) Token: 0x0600007A RID: 122 RVA: 0x0000243A File Offset: 0x0000063A
		public int? IsModelItemPolicyEnabled { get; set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00002443 File Offset: 0x00000643
		// (set) Token: 0x0600007C RID: 124 RVA: 0x0000244B File Offset: 0x0000064B
		public long? DSIDNum { get; set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00002454 File Offset: 0x00000654
		public bool UseAsWindowsCredentials
		{
			get
			{
				return (this.Flags & DataSourceEntity.DataSourceFlags.WindowsCredentials) > (DataSourceEntity.DataSourceFlags)0;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00002461 File Offset: 0x00000661
		public bool ImpersonateAuthenticatedUser
		{
			get
			{
				return (this.Flags & DataSourceEntity.DataSourceFlags.ImpersonateUser) > (DataSourceEntity.DataSourceFlags)0;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600007F RID: 127 RVA: 0x0000246E File Offset: 0x0000066E
		public bool IsEnabled
		{
			get
			{
				return (this.Flags & DataSourceEntity.DataSourceFlags.Enabled) > (DataSourceEntity.DataSourceFlags)0;
			}
		}

		// Token: 0x02000023 RID: 35
		public enum CredentialsRetrievalOption
		{
			// Token: 0x040000B8 RID: 184
			Unknown,
			// Token: 0x040000B9 RID: 185
			Prompt,
			// Token: 0x040000BA RID: 186
			Store,
			// Token: 0x040000BB RID: 187
			Integrated,
			// Token: 0x040000BC RID: 188
			None,
			// Token: 0x040000BD RID: 189
			ServiceAccount,
			// Token: 0x040000BE RID: 190
			SecureStore
		}

		// Token: 0x02000024 RID: 36
		[Flags]
		public enum DataSourceFlags
		{
			// Token: 0x040000C0 RID: 192
			Enabled = 1,
			// Token: 0x040000C1 RID: 193
			ReferenceIsValid = 2,
			// Token: 0x040000C2 RID: 194
			ImpersonateUser = 4,
			// Token: 0x040000C3 RID: 195
			WindowsCredentials = 8,
			// Token: 0x040000C4 RID: 196
			IsModel = 16,
			// Token: 0x040000C5 RID: 197
			ConnectionStringUseridReference = 32
		}
	}
}
