using System;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x0200006C RID: 108
	public sealed class DataValue
	{
		// Token: 0x060006F2 RID: 1778 RVA: 0x0001AC13 File Offset: 0x00018E13
		internal DataValue(string name, object value)
		{
			this.m_name = name;
			this.m_value = value;
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x060006F3 RID: 1779 RVA: 0x0001AC29 File Offset: 0x00018E29
		public string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x060006F4 RID: 1780 RVA: 0x0001AC31 File Offset: 0x00018E31
		public object Value
		{
			get
			{
				return this.m_value;
			}
		}

		// Token: 0x040001F1 RID: 497
		private string m_name;

		// Token: 0x040001F2 RID: 498
		private object m_value;
	}
}
