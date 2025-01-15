using System;

namespace Microsoft.AnalysisServices.AdomdClient.Interop
{
	// Token: 0x0200013C RID: 316
	internal struct SecHandle
	{
		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x06000FE6 RID: 4070 RVA: 0x0003657A File Offset: 0x0003477A
		public bool IsInvalid
		{
			get
			{
				return this.dwLower == UIntPtr.Zero && this.dwUpper == UIntPtr.Zero;
			}
		}

		// Token: 0x06000FE7 RID: 4071 RVA: 0x000365A0 File Offset: 0x000347A0
		public void Reset()
		{
			this.dwLower = UIntPtr.Zero;
			this.dwUpper = UIntPtr.Zero;
		}

		// Token: 0x04000ADF RID: 2783
		public UIntPtr dwLower;

		// Token: 0x04000AE0 RID: 2784
		public UIntPtr dwUpper;
	}
}
