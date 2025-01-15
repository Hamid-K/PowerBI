using System;

namespace Microsoft.AnalysisServices.AdomdClient.Interop
{
	// Token: 0x0200013C RID: 316
	internal struct SecHandle
	{
		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x06000FF3 RID: 4083 RVA: 0x000368AA File Offset: 0x00034AAA
		public bool IsInvalid
		{
			get
			{
				return this.dwLower == UIntPtr.Zero && this.dwUpper == UIntPtr.Zero;
			}
		}

		// Token: 0x06000FF4 RID: 4084 RVA: 0x000368D0 File Offset: 0x00034AD0
		public void Reset()
		{
			this.dwLower = UIntPtr.Zero;
			this.dwUpper = UIntPtr.Zero;
		}

		// Token: 0x04000AEC RID: 2796
		public UIntPtr dwLower;

		// Token: 0x04000AED RID: 2797
		public UIntPtr dwUpper;
	}
}
