using System;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x02000814 RID: 2068
	public interface IDbExceptionInfo
	{
		// Token: 0x17000F72 RID: 3954
		// (get) Token: 0x06004193 RID: 16787
		IDbExceptionInfo InnerExceptionInfo { get; }

		// Token: 0x17000F73 RID: 3955
		// (get) Token: 0x06004194 RID: 16788
		string Message { get; }

		// Token: 0x17000F74 RID: 3956
		// (get) Token: 0x06004195 RID: 16789
		string Server { get; }

		// Token: 0x17000F75 RID: 3957
		// (get) Token: 0x06004196 RID: 16790
		string Procedure { get; }

		// Token: 0x17000F76 RID: 3958
		// (get) Token: 0x06004197 RID: 16791
		string SqlState { get; }

		// Token: 0x17000F77 RID: 3959
		// (get) Token: 0x06004198 RID: 16792
		SeverityCode SeverityCode { get; }

		// Token: 0x17000F78 RID: 3960
		// (get) Token: 0x06004199 RID: 16793
		int SqlCode { get; }

		// Token: 0x17000F79 RID: 3961
		// (get) Token: 0x0600419A RID: 16794
		int LineNumber { get; }

		// Token: 0x17000F7A RID: 3962
		// (get) Token: 0x0600419B RID: 16795
		int Number { get; }

		// Token: 0x17000F7B RID: 3963
		// (get) Token: 0x0600419C RID: 16796
		bool Mapped { get; }
	}
}
