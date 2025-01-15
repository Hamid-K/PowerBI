using System;

namespace Microsoft.AnalysisServices.Interop
{
	// Token: 0x02000131 RID: 305
	internal struct SecHandle
	{
		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x06001081 RID: 4225 RVA: 0x000391AE File Offset: 0x000373AE
		public bool IsInvalid
		{
			get
			{
				return this.dwLower == UIntPtr.Zero && this.dwUpper == UIntPtr.Zero;
			}
		}

		// Token: 0x06001082 RID: 4226 RVA: 0x000391D4 File Offset: 0x000373D4
		public void Reset()
		{
			this.dwLower = UIntPtr.Zero;
			this.dwUpper = UIntPtr.Zero;
		}

		// Token: 0x04000AA5 RID: 2725
		public UIntPtr dwLower;

		// Token: 0x04000AA6 RID: 2726
		public UIntPtr dwUpper;
	}
}
