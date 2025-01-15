using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x020000B4 RID: 180
	public static class HRESULT
	{
		// Token: 0x060002EF RID: 751 RVA: 0x00008A47 File Offset: 0x00006C47
		public static bool IsError(int hresult)
		{
			return hresult < 0;
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x00008A4D File Offset: 0x00006C4D
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public static void Check(string errorMessage, int hresult)
		{
			if (HRESULT.IsError(hresult))
			{
				throw new COMException(errorMessage, hresult);
			}
		}

		// Token: 0x04000347 RID: 839
		public const int S_OK = 0;

		// Token: 0x04000348 RID: 840
		public const int S_FALSE = 1;

		// Token: 0x04000349 RID: 841
		public const int DB_S_NORESULT = 265929;

		// Token: 0x0400034A RID: 842
		public const int DB_S_ENDOFROWSET = 265926;

		// Token: 0x0400034B RID: 843
		public const int DB_S_ERRORSOCCURRED = 265946;

		// Token: 0x0400034C RID: 844
		public const int DB_S_DIALECTIGNORED = 265933;

		// Token: 0x0400034D RID: 845
		public const int DB_S_ASYNCHRONOUS = 265936;

		// Token: 0x0400034E RID: 846
		public const int DB_E_NOAGGREGATION = -2147217886;

		// Token: 0x0400034F RID: 847
		public const int DB_E_BADCHAPTER = -2147217914;

		// Token: 0x04000350 RID: 848
		public const int DB_E_CANTFETCHBACKWARDS = -2147217884;

		// Token: 0x04000351 RID: 849
		public const int DB_E_CANTSCROLLBACKWARDS = -2147217879;

		// Token: 0x04000352 RID: 850
		public const int DB_E_CANNOTRESTART = -2147217896;

		// Token: 0x04000353 RID: 851
		public const int DB_E_BADACCESSORHANDLE = -2147217920;

		// Token: 0x04000354 RID: 852
		public const int DB_E_BADACCESSORFLAGS = -2147217850;

		// Token: 0x04000355 RID: 853
		public const int DB_E_CANCELED = -2147217842;

		// Token: 0x04000356 RID: 854
		public const int DB_E_ERRORSOCCURRED = -2147217887;

		// Token: 0x04000357 RID: 855
		public const int DB_E_DIALECTNOTSUPPORTED = -2147217898;

		// Token: 0x04000358 RID: 856
		public const int DB_E_NULLACCESSORNOTSUPPORTED = -2147217847;

		// Token: 0x04000359 RID: 857
		public const int DB_E_BYREFACCESSORNOTSUPPORTED = -2147217848;

		// Token: 0x0400035A RID: 858
		public const int DB_E_ROWSNOTRELEASED = -2147217883;

		// Token: 0x0400035B RID: 859
		public const int DB_E_OBJECTOPEN = -2147217915;

		// Token: 0x0400035C RID: 860
		public const int MD_E_INVALIDAXIS = -2147217821;

		// Token: 0x0400035D RID: 861
		public const int MD_E_INVALIDCELLRANGE = -2147217820;

		// Token: 0x0400035E RID: 862
		public const int E_INVALIDARG = -2147024809;

		// Token: 0x0400035F RID: 863
		public const int E_NOINTERFACE = -2147467262;

		// Token: 0x04000360 RID: 864
		public const int E_FAIL = -2147467259;
	}
}
