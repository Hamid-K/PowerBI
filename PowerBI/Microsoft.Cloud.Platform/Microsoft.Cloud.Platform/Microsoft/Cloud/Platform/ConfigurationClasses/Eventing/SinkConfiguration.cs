using System;
using Microsoft.Cloud.Platform.Configuration;

namespace Microsoft.Cloud.Platform.ConfigurationClasses.Eventing
{
	// Token: 0x0200044C RID: 1100
	[Serializable]
	public sealed class SinkConfiguration : ConfigurationClass
	{
		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x06002239 RID: 8761 RVA: 0x0007DFB0 File Offset: 0x0007C1B0
		// (set) Token: 0x0600223A RID: 8762 RVA: 0x0007DFB8 File Offset: 0x0007C1B8
		[ConfigurationProperty]
		public string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				base.ValidateStringNotNullEmptyOrWhiteSpace(value, "Name");
				this.m_name = value;
			}
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x0600223B RID: 8763 RVA: 0x0007DFCD File Offset: 0x0007C1CD
		// (set) Token: 0x0600223C RID: 8764 RVA: 0x0007DFD5 File Offset: 0x0007C1D5
		[ConfigurationProperty]
		public string Assembly
		{
			get
			{
				return this.m_assembly;
			}
			set
			{
				base.ValidateStringNotNullEmptyOrWhiteSpace(value, "Assembly");
				this.m_assembly = value;
			}
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x0600223D RID: 8765 RVA: 0x0007DFEA File Offset: 0x0007C1EA
		// (set) Token: 0x0600223E RID: 8766 RVA: 0x0007DFF2 File Offset: 0x0007C1F2
		[ConfigurationProperty]
		public string Type
		{
			get
			{
				return this.m_type;
			}
			set
			{
				base.ValidateStringNotNullEmptyOrWhiteSpace(value, "Type");
				this.m_type = value;
			}
		}

		// Token: 0x04000BDA RID: 3034
		private string m_name;

		// Token: 0x04000BDB RID: 3035
		private string m_assembly;

		// Token: 0x04000BDC RID: 3036
		private string m_type;
	}
}
