using System;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x020005C2 RID: 1474
	internal sealed class CommonDataComparerException : Exception, IDataComparisonError
	{
		// Token: 0x0600534B RID: 21323 RVA: 0x0015E91A File Offset: 0x0015CB1A
		internal CommonDataComparerException(string typeX, string typeY)
		{
			this.m_typeX = typeX;
			this.m_typeY = typeY;
		}

		// Token: 0x17001EE9 RID: 7913
		// (get) Token: 0x0600534C RID: 21324 RVA: 0x0015E930 File Offset: 0x0015CB30
		public string TypeX
		{
			get
			{
				return this.m_typeX;
			}
		}

		// Token: 0x17001EEA RID: 7914
		// (get) Token: 0x0600534D RID: 21325 RVA: 0x0015E938 File Offset: 0x0015CB38
		public string TypeY
		{
			get
			{
				return this.m_typeY;
			}
		}

		// Token: 0x040029F2 RID: 10738
		private string m_typeX;

		// Token: 0x040029F3 RID: 10739
		private string m_typeY;
	}
}
