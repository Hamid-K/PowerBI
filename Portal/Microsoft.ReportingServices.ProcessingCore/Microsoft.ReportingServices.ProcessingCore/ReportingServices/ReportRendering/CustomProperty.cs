using System;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x02000060 RID: 96
	public sealed class CustomProperty
	{
		// Token: 0x060006B0 RID: 1712 RVA: 0x00019A63 File Offset: 0x00017C63
		public CustomProperty(string name, object value)
		{
			this.m_name = name;
			this.m_value = value;
		}

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x060006B1 RID: 1713 RVA: 0x00019A79 File Offset: 0x00017C79
		public string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x060006B2 RID: 1714 RVA: 0x00019A81 File Offset: 0x00017C81
		public object Value
		{
			get
			{
				return this.m_value;
			}
		}

		// Token: 0x040001BE RID: 446
		private string m_name;

		// Token: 0x040001BF RID: 447
		private object m_value;
	}
}
