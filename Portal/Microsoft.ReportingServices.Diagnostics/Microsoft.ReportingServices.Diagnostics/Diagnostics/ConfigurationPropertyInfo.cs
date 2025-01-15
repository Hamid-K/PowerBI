using System;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000023 RID: 35
	[Serializable]
	internal class ConfigurationPropertyInfo
	{
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000087 RID: 135 RVA: 0x0000331C File Offset: 0x0000151C
		// (set) Token: 0x06000088 RID: 136 RVA: 0x00003324 File Offset: 0x00001524
		public object Value
		{
			get
			{
				return this.m_value;
			}
			set
			{
				this.m_value = value;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000089 RID: 137 RVA: 0x0000332D File Offset: 0x0000152D
		// (set) Token: 0x0600008A RID: 138 RVA: 0x00003335 File Offset: 0x00001535
		public string SpecifiedValue
		{
			get
			{
				return this.m_specifiedValue;
			}
			set
			{
				this.m_specifiedValue = value;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600008B RID: 139 RVA: 0x0000333E File Offset: 0x0000153E
		// (set) Token: 0x0600008C RID: 140 RVA: 0x00003346 File Offset: 0x00001546
		public string Message
		{
			get
			{
				return this.m_message;
			}
			set
			{
				this.m_message = value;
			}
		}

		// Token: 0x040000E9 RID: 233
		private object m_value;

		// Token: 0x040000EA RID: 234
		private string m_specifiedValue;

		// Token: 0x040000EB RID: 235
		private string m_message;
	}
}
