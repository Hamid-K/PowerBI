using System;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x0200008B RID: 139
	internal sealed class ReturnValue
	{
		// Token: 0x1700018F RID: 399
		// (get) Token: 0x0600044C RID: 1100 RVA: 0x0000D27E File Offset: 0x0000B47E
		public object Value
		{
			get
			{
				return this.m_value;
			}
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x0000D286 File Offset: 0x0000B486
		public ReturnValue(object value)
		{
			this.m_value = value;
		}

		// Token: 0x0400028E RID: 654
		private readonly object m_value;
	}
}
