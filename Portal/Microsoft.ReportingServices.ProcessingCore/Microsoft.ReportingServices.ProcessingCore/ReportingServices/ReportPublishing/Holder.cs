using System;

namespace Microsoft.ReportingServices.ReportPublishing
{
	// Token: 0x0200039D RID: 925
	internal class Holder<T> where T : struct
	{
		// Token: 0x170013CA RID: 5066
		// (get) Token: 0x060025B3 RID: 9651 RVA: 0x000B46C1 File Offset: 0x000B28C1
		// (set) Token: 0x060025B4 RID: 9652 RVA: 0x000B46C9 File Offset: 0x000B28C9
		internal T Value
		{
			get
			{
				return this.m_t;
			}
			set
			{
				this.m_t = value;
			}
		}

		// Token: 0x04001600 RID: 5632
		private T m_t;
	}
}
