using System;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000013 RID: 19
	internal sealed class ReturnValue
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000067 RID: 103 RVA: 0x000032AA File Offset: 0x000014AA
		public object Value
		{
			get
			{
				return this.m_value;
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000032B2 File Offset: 0x000014B2
		public ReturnValue(object value)
		{
			this.m_value = value;
		}

		// Token: 0x0400005D RID: 93
		private readonly object m_value;
	}
}
