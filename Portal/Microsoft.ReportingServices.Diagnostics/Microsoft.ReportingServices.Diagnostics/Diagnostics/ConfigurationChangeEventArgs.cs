using System;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x0200001F RID: 31
	[Serializable]
	internal sealed class ConfigurationChangeEventArgs : EventArgs
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00003220 File Offset: 0x00001420
		// (set) Token: 0x0600007A RID: 122 RVA: 0x00003228 File Offset: 0x00001428
		public bool ResetProperties
		{
			get
			{
				return this.m_resetProperties;
			}
			set
			{
				this.m_resetProperties = value;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00003231 File Offset: 0x00001431
		// (set) Token: 0x0600007C RID: 124 RVA: 0x00003239 File Offset: 0x00001439
		public ConfigurationPropertyBag Properties
		{
			get
			{
				return this.m_properties;
			}
			set
			{
				this.m_properties = value;
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003242 File Offset: 0x00001442
		public ConfigurationChangeEventArgs(ConfigurationPropertyBag properties, bool resetProperties)
		{
			this.m_properties = properties;
			this.m_resetProperties = resetProperties;
		}

		// Token: 0x0400006F RID: 111
		private ConfigurationPropertyBag m_properties;

		// Token: 0x04000070 RID: 112
		private bool m_resetProperties;
	}
}
