using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.DataExtensions
{
	// Token: 0x020005B7 RID: 1463
	public sealed class DataSourceFaultContext
	{
		// Token: 0x06005300 RID: 21248 RVA: 0x0015D597 File Offset: 0x0015B797
		internal DataSourceFaultContext(ErrorCode errorCode, string errorString)
		{
			this.m_errorCode = errorCode;
			this.m_errorString = errorString;
		}

		// Token: 0x17001ED6 RID: 7894
		// (get) Token: 0x06005301 RID: 21249 RVA: 0x0015D5AD File Offset: 0x0015B7AD
		public ErrorCode ErrorCode
		{
			get
			{
				return this.m_errorCode;
			}
		}

		// Token: 0x17001ED7 RID: 7895
		// (get) Token: 0x06005302 RID: 21250 RVA: 0x0015D5B5 File Offset: 0x0015B7B5
		public string ErrorString
		{
			get
			{
				return this.m_errorString;
			}
		}

		// Token: 0x040029D5 RID: 10709
		public readonly ErrorCode m_errorCode;

		// Token: 0x040029D6 RID: 10710
		public readonly string m_errorString;
	}
}
