using System;
using System.ServiceModel;
using Microsoft.Cloud.Platform.ConfigurationClasses.Communication;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x02000489 RID: 1161
	public class EndpointInfo
	{
		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x060023BE RID: 9150 RVA: 0x00080C91 File Offset: 0x0007EE91
		// (set) Token: 0x060023BF RID: 9151 RVA: 0x00080C99 File Offset: 0x0007EE99
		internal BindingType BindingType { get; private set; }

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x060023C0 RID: 9152 RVA: 0x00080CA2 File Offset: 0x0007EEA2
		// (set) Token: 0x060023C1 RID: 9153 RVA: 0x00080CAA File Offset: 0x0007EEAA
		internal int Port { get; private set; }

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x060023C2 RID: 9154 RVA: 0x00080CB3 File Offset: 0x0007EEB3
		// (set) Token: 0x060023C3 RID: 9155 RVA: 0x00080CBB File Offset: 0x0007EEBB
		internal string Contract { get; private set; }

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x060023C4 RID: 9156 RVA: 0x00080CC4 File Offset: 0x0007EEC4
		// (set) Token: 0x060023C5 RID: 9157 RVA: 0x00080CCC File Offset: 0x0007EECC
		public BindingSecurityMode SecurityMode { get; set; }

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x060023C6 RID: 9158 RVA: 0x00080CD5 File Offset: 0x0007EED5
		// (set) Token: 0x060023C7 RID: 9159 RVA: 0x00080CDD File Offset: 0x0007EEDD
		public long MaxMessageSize { get; set; }

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x060023C8 RID: 9160 RVA: 0x00080CE6 File Offset: 0x0007EEE6
		// (set) Token: 0x060023C9 RID: 9161 RVA: 0x00080CEE File Offset: 0x0007EEEE
		public TimeSpan ReceiveTimeout { get; set; }

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x060023CA RID: 9162 RVA: 0x00080CF7 File Offset: 0x0007EEF7
		// (set) Token: 0x060023CB RID: 9163 RVA: 0x00080CFF File Offset: 0x0007EEFF
		public TimeSpan SendTimeout { get; set; }

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x060023CC RID: 9164 RVA: 0x00080D08 File Offset: 0x0007EF08
		// (set) Token: 0x060023CD RID: 9165 RVA: 0x00080D10 File Offset: 0x0007EF10
		public TimeSpan OpenTimeout { get; set; }

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x060023CE RID: 9166 RVA: 0x00080D19 File Offset: 0x0007EF19
		// (set) Token: 0x060023CF RID: 9167 RVA: 0x00080D21 File Offset: 0x0007EF21
		public TimeSpan CloseTimeout { get; set; }

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x060023D0 RID: 9168 RVA: 0x00080D2A File Offset: 0x0007EF2A
		// (set) Token: 0x060023D1 RID: 9169 RVA: 0x00080D32 File Offset: 0x0007EF32
		public TimeSpan OperationTimeout { get; set; }

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x060023D2 RID: 9170 RVA: 0x00080D3B File Offset: 0x0007EF3B
		// (set) Token: 0x060023D3 RID: 9171 RVA: 0x00080D43 File Offset: 0x0007EF43
		internal bool ReliableSessionEnabled { get; set; }

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x060023D4 RID: 9172 RVA: 0x00080D4C File Offset: 0x0007EF4C
		// (set) Token: 0x060023D5 RID: 9173 RVA: 0x00080D54 File Offset: 0x0007EF54
		internal bool ReliableSessionOrderedMessages { get; set; }

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x060023D6 RID: 9174 RVA: 0x00080D5D File Offset: 0x0007EF5D
		// (set) Token: 0x060023D7 RID: 9175 RVA: 0x00080D65 File Offset: 0x0007EF65
		internal TransferMode TransferDataMode { get; set; }

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x060023D8 RID: 9176 RVA: 0x00080D6E File Offset: 0x0007EF6E
		// (set) Token: 0x060023D9 RID: 9177 RVA: 0x00080D76 File Offset: 0x0007EF76
		internal long MaxBufferPoolSize { get; set; }

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x060023DA RID: 9178 RVA: 0x00080D7F File Offset: 0x0007EF7F
		// (set) Token: 0x060023DB RID: 9179 RVA: 0x00080D87 File Offset: 0x0007EF87
		internal int MaxConnections { get; set; }

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x060023DC RID: 9180 RVA: 0x00080D90 File Offset: 0x0007EF90
		// (set) Token: 0x060023DD RID: 9181 RVA: 0x00080D98 File Offset: 0x0007EF98
		internal int MaxDepth { get; set; }

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x060023DE RID: 9182 RVA: 0x00080DA1 File Offset: 0x0007EFA1
		// (set) Token: 0x060023DF RID: 9183 RVA: 0x00080DA9 File Offset: 0x0007EFA9
		public string ClientCredentialType { get; set; }

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x060023E0 RID: 9184 RVA: 0x00080DB2 File Offset: 0x0007EFB2
		// (set) Token: 0x060023E1 RID: 9185 RVA: 0x00080DBA File Offset: 0x0007EFBA
		public bool UsePrefix { get; private set; }

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x060023E2 RID: 9186 RVA: 0x00080DC3 File Offset: 0x0007EFC3
		// (set) Token: 0x060023E3 RID: 9187 RVA: 0x00080DCB File Offset: 0x0007EFCB
		internal int MaxArrayLength { get; set; }

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x060023E4 RID: 9188 RVA: 0x00080DD4 File Offset: 0x0007EFD4
		// (set) Token: 0x060023E5 RID: 9189 RVA: 0x00080DDC File Offset: 0x0007EFDC
		public int MaxStringContentLength { get; set; }

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x060023E6 RID: 9190 RVA: 0x00080DE5 File Offset: 0x0007EFE5
		// (set) Token: 0x060023E7 RID: 9191 RVA: 0x00080DED File Offset: 0x0007EFED
		public string SpnIdentityName { get; set; }

		// Token: 0x060023E8 RID: 9192 RVA: 0x00080DF6 File Offset: 0x0007EFF6
		public EndpointInfo(BindingType bindingType, string contract)
			: this(bindingType, 0, contract)
		{
		}

		// Token: 0x060023E9 RID: 9193 RVA: 0x00080E04 File Offset: 0x0007F004
		public EndpointInfo(BindingType bindingType, int port, string contract)
		{
			this.BindingType = bindingType;
			this.Port = port;
			this.Contract = contract;
			this.SecurityMode = BindingSecurityMode.None;
			this.MaxMessageSize = 1048576L;
			this.ReceiveTimeout = TimeSpan.FromMinutes(10.0);
			this.SendTimeout = TimeSpan.FromMinutes(1.0);
			this.OpenTimeout = TimeSpan.FromMinutes(1.0);
			this.CloseTimeout = TimeSpan.FromMinutes(1.0);
			this.OperationTimeout = TimeSpan.FromMinutes(1.0);
			this.ReliableSessionEnabled = false;
			this.ReliableSessionOrderedMessages = false;
			this.MaxArrayLength = 16384;
			this.MaxStringContentLength = 65536;
			this.MaxDepth = 64;
			this.ClientCredentialType = null;
			this.UsePrefix = true;
			this.TransferDataMode = TransferMode.Buffered;
			this.MaxBufferPoolSize = 524288L;
			this.SpnIdentityName = EndpointInfo.DefaultSpnIdentityName;
			this.MaxConnections = 10;
		}

		// Token: 0x04000C7F RID: 3199
		internal const BindingSecurityMode DefaultSecurityMode = BindingSecurityMode.None;

		// Token: 0x04000C80 RID: 3200
		internal const long DefaultMaxConcurrentCalls = 512L;

		// Token: 0x04000C81 RID: 3201
		internal const long DefaultMaxMessageSize = 1048576L;

		// Token: 0x04000C82 RID: 3202
		internal const double DefaultReceiveTimeout = 10.0;

		// Token: 0x04000C83 RID: 3203
		internal const double DefaultSendTimeout = 1.0;

		// Token: 0x04000C84 RID: 3204
		internal const double DefaultOpenTimeout = 1.0;

		// Token: 0x04000C85 RID: 3205
		internal const double DefaultCloseTimeout = 1.0;

		// Token: 0x04000C86 RID: 3206
		internal const double DefaultOperationTimeout = 1.0;

		// Token: 0x04000C87 RID: 3207
		internal const bool DefaultReliableSessionEnabled = false;

		// Token: 0x04000C88 RID: 3208
		internal const bool DefaultReliableSessionOrderedMessages = false;

		// Token: 0x04000C89 RID: 3209
		internal const string DefaultClientCredentialType = null;

		// Token: 0x04000C8A RID: 3210
		internal const int DefaultMaxArrayLength = 16384;

		// Token: 0x04000C8B RID: 3211
		internal const int DefaultMaxStringContentLength = 65536;

		// Token: 0x04000C8C RID: 3212
		internal const int DefaultMaxDepth = 64;

		// Token: 0x04000C8D RID: 3213
		internal const TransferMode DefaultTransferDataMode = TransferMode.Buffered;

		// Token: 0x04000C8E RID: 3214
		internal const long DefaultMaxBufferPoolSize = 524288L;

		// Token: 0x04000C8F RID: 3215
		internal static readonly string DefaultSpnIdentityName = string.Empty;

		// Token: 0x04000C90 RID: 3216
		internal const int DefaultMaxConnections = 10;
	}
}
