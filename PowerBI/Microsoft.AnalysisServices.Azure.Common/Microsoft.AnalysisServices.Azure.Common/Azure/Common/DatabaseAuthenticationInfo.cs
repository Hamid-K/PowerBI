using System;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x02000049 RID: 73
	[CLSCompliant(true)]
	public class DatabaseAuthenticationInfo
	{
		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x0000F106 File Offset: 0x0000D306
		// (set) Token: 0x060003A4 RID: 932 RVA: 0x0000F10E File Offset: 0x0000D30E
		public bool BypassAuthorization { get; set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x0000F117 File Offset: 0x0000D317
		// (set) Token: 0x060003A6 RID: 934 RVA: 0x0000F11F File Offset: 0x0000D31F
		public string CertificateThumbprint { get; set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060003A7 RID: 935 RVA: 0x0000F128 File Offset: 0x0000D328
		// (set) Token: 0x060003A8 RID: 936 RVA: 0x0000F130 File Offset: 0x0000D330
		public int ConnectionTimeout { get; set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060003A9 RID: 937 RVA: 0x0000F139 File Offset: 0x0000D339
		// (set) Token: 0x060003AA RID: 938 RVA: 0x0000F141 File Offset: 0x0000D341
		public int CommandTimeout { get; set; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060003AB RID: 939 RVA: 0x0000F14A File Offset: 0x0000D34A
		// (set) Token: 0x060003AC RID: 940 RVA: 0x0000F152 File Offset: 0x0000D352
		public string DatabaseName { get; set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060003AD RID: 941 RVA: 0x0000F15B File Offset: 0x0000D35B
		// (set) Token: 0x060003AE RID: 942 RVA: 0x0000F163 File Offset: 0x0000D363
		public string DataSource { get; set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060003AF RID: 943 RVA: 0x0000F16C File Offset: 0x0000D36C
		// (set) Token: 0x060003B0 RID: 944 RVA: 0x0000F174 File Offset: 0x0000D374
		public string IdentityProvider { get; set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x0000F17D File Offset: 0x0000D37D
		// (set) Token: 0x060003B2 RID: 946 RVA: 0x0000F185 File Offset: 0x0000D385
		public string ImpersonatedRoles { get; set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x0000F18E File Offset: 0x0000D38E
		// (set) Token: 0x060003B4 RID: 948 RVA: 0x0000F196 File Offset: 0x0000D396
		public string TenantNameOrId { get; set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x0000F19F File Offset: 0x0000D39F
		// (set) Token: 0x060003B6 RID: 950 RVA: 0x0000F1A7 File Offset: 0x0000D3A7
		public string UserName { get; set; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060003B7 RID: 951 RVA: 0x0000F1B0 File Offset: 0x0000D3B0
		// (set) Token: 0x060003B8 RID: 952 RVA: 0x0000F1B8 File Offset: 0x0000D3B8
		public string UserPuid { get; set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060003B9 RID: 953 RVA: 0x0000F1C1 File Offset: 0x0000D3C1
		// (set) Token: 0x060003BA RID: 954 RVA: 0x0000F1C9 File Offset: 0x0000D3C9
		public string ContextualIdentity { get; set; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060003BB RID: 955 RVA: 0x0000F1D2 File Offset: 0x0000D3D2
		// (set) Token: 0x060003BC RID: 956 RVA: 0x0000F1DA File Offset: 0x0000D3DA
		public string ApplicationName { get; set; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060003BD RID: 957 RVA: 0x0000F1E3 File Offset: 0x0000D3E3
		// (set) Token: 0x060003BE RID: 958 RVA: 0x0000F1EB File Offset: 0x0000D3EB
		public bool IsAdmin { get; set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060003BF RID: 959 RVA: 0x0000F1F4 File Offset: 0x0000D3F4
		// (set) Token: 0x060003C0 RID: 960 RVA: 0x0000F1FC File Offset: 0x0000D3FC
		public bool IsSample { get; set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x0000F205 File Offset: 0x0000D405
		// (set) Token: 0x060003C2 RID: 962 RVA: 0x0000F20D File Offset: 0x0000D40D
		public string ConnectionActivityId { get; set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x0000F216 File Offset: 0x0000D416
		// (set) Token: 0x060003C4 RID: 964 RVA: 0x0000F21E File Offset: 0x0000D41E
		public IntendedUsage IntendedUsage { get; set; }
	}
}
