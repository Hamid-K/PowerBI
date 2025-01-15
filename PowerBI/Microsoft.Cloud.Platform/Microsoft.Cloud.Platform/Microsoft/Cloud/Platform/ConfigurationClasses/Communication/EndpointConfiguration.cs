using System;
using System.ServiceModel;
using Microsoft.Cloud.Platform.Communication;
using Microsoft.Cloud.Platform.Configuration;

namespace Microsoft.Cloud.Platform.ConfigurationClasses.Communication
{
	// Token: 0x02000453 RID: 1107
	[Serializable]
	public sealed class EndpointConfiguration : ConfigurationClass
	{
		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x06002265 RID: 8805 RVA: 0x0007E117 File Offset: 0x0007C317
		// (set) Token: 0x06002266 RID: 8806 RVA: 0x0007E11F File Offset: 0x0007C31F
		[ConfigurationProperty]
		public string Contract { get; set; }

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x06002267 RID: 8807 RVA: 0x0007E128 File Offset: 0x0007C328
		// (set) Token: 0x06002268 RID: 8808 RVA: 0x0007E130 File Offset: 0x0007C330
		[ConfigurationProperty]
		public BindingType BindingType { get; set; }

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06002269 RID: 8809 RVA: 0x0007E139 File Offset: 0x0007C339
		// (set) Token: 0x0600226A RID: 8810 RVA: 0x0007E141 File Offset: 0x0007C341
		[ConfigurationProperty]
		public int Port { get; set; }

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x0600226B RID: 8811 RVA: 0x0007E14A File Offset: 0x0007C34A
		// (set) Token: 0x0600226C RID: 8812 RVA: 0x0007E152 File Offset: 0x0007C352
		[ConfigurationProperty]
		public BindingSecurityMode BindingSecurityMode { get; set; }

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x0600226D RID: 8813 RVA: 0x0007E15B File Offset: 0x0007C35B
		// (set) Token: 0x0600226E RID: 8814 RVA: 0x0007E163 File Offset: 0x0007C363
		[ConfigurationProperty]
		public long MaxMessageSize { get; set; }

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x0600226F RID: 8815 RVA: 0x0007E16C File Offset: 0x0007C36C
		// (set) Token: 0x06002270 RID: 8816 RVA: 0x0007E174 File Offset: 0x0007C374
		[ConfigurationProperty]
		public double ReceiveTimeout { get; set; }

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x06002271 RID: 8817 RVA: 0x0007E17D File Offset: 0x0007C37D
		// (set) Token: 0x06002272 RID: 8818 RVA: 0x0007E185 File Offset: 0x0007C385
		[ConfigurationProperty]
		public double SendTimeout { get; set; }

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x06002273 RID: 8819 RVA: 0x0007E18E File Offset: 0x0007C38E
		// (set) Token: 0x06002274 RID: 8820 RVA: 0x0007E196 File Offset: 0x0007C396
		[ConfigurationProperty]
		public double OpenTimeout { get; set; }

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x06002275 RID: 8821 RVA: 0x0007E19F File Offset: 0x0007C39F
		// (set) Token: 0x06002276 RID: 8822 RVA: 0x0007E1A7 File Offset: 0x0007C3A7
		[ConfigurationProperty]
		public double CloseTimeout { get; set; }

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x06002277 RID: 8823 RVA: 0x0007E1B0 File Offset: 0x0007C3B0
		// (set) Token: 0x06002278 RID: 8824 RVA: 0x0007E1B8 File Offset: 0x0007C3B8
		[ConfigurationProperty]
		public double OperationTimeout { get; set; }

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x06002279 RID: 8825 RVA: 0x0007E1C1 File Offset: 0x0007C3C1
		// (set) Token: 0x0600227A RID: 8826 RVA: 0x0007E1C9 File Offset: 0x0007C3C9
		[ConfigurationProperty]
		public bool ReliableSessionEnabled { get; set; }

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x0600227B RID: 8827 RVA: 0x0007E1D2 File Offset: 0x0007C3D2
		// (set) Token: 0x0600227C RID: 8828 RVA: 0x0007E1DA File Offset: 0x0007C3DA
		[ConfigurationProperty]
		public int MaxConnections { get; set; }

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x0600227D RID: 8829 RVA: 0x0007E1E3 File Offset: 0x0007C3E3
		// (set) Token: 0x0600227E RID: 8830 RVA: 0x0007E1EB File Offset: 0x0007C3EB
		[ConfigurationProperty]
		public bool ReliableSessionOrderedMessages { get; set; }

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x0600227F RID: 8831 RVA: 0x0007E1F4 File Offset: 0x0007C3F4
		// (set) Token: 0x06002280 RID: 8832 RVA: 0x0007E1FC File Offset: 0x0007C3FC
		[ConfigurationProperty]
		public string ClientCredentialType { get; set; }

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x06002281 RID: 8833 RVA: 0x0007E205 File Offset: 0x0007C405
		// (set) Token: 0x06002282 RID: 8834 RVA: 0x0007E20D File Offset: 0x0007C40D
		[ConfigurationProperty]
		public int MaxStringContentLength { get; set; }

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x06002283 RID: 8835 RVA: 0x0007E216 File Offset: 0x0007C416
		// (set) Token: 0x06002284 RID: 8836 RVA: 0x0007E21E File Offset: 0x0007C41E
		[ConfigurationProperty]
		public int MaxArrayLength { get; set; }

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x06002285 RID: 8837 RVA: 0x0007E227 File Offset: 0x0007C427
		// (set) Token: 0x06002286 RID: 8838 RVA: 0x0007E22F File Offset: 0x0007C42F
		[ConfigurationProperty]
		public TransferMode TransferDataMode { get; set; }

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x06002287 RID: 8839 RVA: 0x0007E238 File Offset: 0x0007C438
		// (set) Token: 0x06002288 RID: 8840 RVA: 0x0007E240 File Offset: 0x0007C440
		[ConfigurationProperty]
		public long MaxBufferPoolSize { get; set; }

		// Token: 0x06002289 RID: 8841 RVA: 0x0007E24C File Offset: 0x0007C44C
		public EndpointConfiguration()
		{
			this.BindingSecurityMode = BindingSecurityMode.None;
			this.MaxMessageSize = 1048576L;
			this.ReceiveTimeout = 10.0;
			this.SendTimeout = 1.0;
			this.OpenTimeout = 1.0;
			this.CloseTimeout = 1.0;
			this.OperationTimeout = 1.0;
			this.ReliableSessionEnabled = false;
			this.ReliableSessionOrderedMessages = false;
			this.ClientCredentialType = null;
			this.MaxStringContentLength = 65536;
			this.MaxArrayLength = 16384;
			this.TransferDataMode = TransferMode.Buffered;
			this.MaxBufferPoolSize = 524288L;
			this.MaxConnections = 10;
		}
	}
}
