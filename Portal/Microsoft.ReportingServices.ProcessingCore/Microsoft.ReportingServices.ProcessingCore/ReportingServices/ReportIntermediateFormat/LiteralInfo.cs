using System;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004CA RID: 1226
	internal sealed class LiteralInfo
	{
		// Token: 0x06003E43 RID: 15939 RVA: 0x0010A4D1 File Offset: 0x001086D1
		public LiteralInfo(object value)
		{
			this.m_value = value;
		}

		// Token: 0x17001A77 RID: 6775
		// (get) Token: 0x06003E44 RID: 15940 RVA: 0x0010A4E0 File Offset: 0x001086E0
		public object Value
		{
			get
			{
				return this.m_value;
			}
		}

		// Token: 0x04001D01 RID: 7425
		private readonly object m_value;
	}
}
