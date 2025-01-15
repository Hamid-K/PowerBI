using System;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x0200049A RID: 1178
	public sealed class EcfHttpMessageHeader
	{
		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x06002455 RID: 9301 RVA: 0x00082FF8 File Offset: 0x000811F8
		// (set) Token: 0x06002456 RID: 9302 RVA: 0x00083000 File Offset: 0x00081200
		public string Name { get; private set; }

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x06002457 RID: 9303 RVA: 0x00083009 File Offset: 0x00081209
		// (set) Token: 0x06002458 RID: 9304 RVA: 0x00083011 File Offset: 0x00081211
		public string Value { get; private set; }

		// Token: 0x06002459 RID: 9305 RVA: 0x0008301A File Offset: 0x0008121A
		public EcfHttpMessageHeader(string name, string value)
		{
			this.Name = name;
			this.Value = value;
		}
	}
}
