using System;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x0200049B RID: 1179
	public sealed class EcfSoapMessageHeader
	{
		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x0600245A RID: 9306 RVA: 0x00083030 File Offset: 0x00081230
		// (set) Token: 0x0600245B RID: 9307 RVA: 0x00083038 File Offset: 0x00081238
		public string Name { get; private set; }

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x0600245C RID: 9308 RVA: 0x00083041 File Offset: 0x00081241
		// (set) Token: 0x0600245D RID: 9309 RVA: 0x00083049 File Offset: 0x00081249
		public string Namespace { get; private set; }

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x0600245E RID: 9310 RVA: 0x00083052 File Offset: 0x00081252
		// (set) Token: 0x0600245F RID: 9311 RVA: 0x0008305A File Offset: 0x0008125A
		public object Value { get; private set; }

		// Token: 0x06002460 RID: 9312 RVA: 0x00083063 File Offset: 0x00081263
		public EcfSoapMessageHeader(string name, string msgNamespace, object value)
		{
			this.Name = name;
			this.Value = value;
			this.Namespace = msgNamespace;
		}
	}
}
