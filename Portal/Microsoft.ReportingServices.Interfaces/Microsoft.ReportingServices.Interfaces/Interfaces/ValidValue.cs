using System;

namespace Microsoft.ReportingServices.Interfaces
{
	// Token: 0x02000034 RID: 52
	public class ValidValue
	{
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000082 RID: 130 RVA: 0x000020D1 File Offset: 0x000002D1
		// (set) Token: 0x06000083 RID: 131 RVA: 0x000020D9 File Offset: 0x000002D9
		public string Value
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

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000084 RID: 132 RVA: 0x000020E2 File Offset: 0x000002E2
		// (set) Token: 0x06000085 RID: 133 RVA: 0x000020EA File Offset: 0x000002EA
		public string Label
		{
			get
			{
				return this.m_label;
			}
			set
			{
				this.m_label = value;
			}
		}

		// Token: 0x0400017B RID: 379
		private string m_value;

		// Token: 0x0400017C RID: 380
		private string m_label;
	}
}
