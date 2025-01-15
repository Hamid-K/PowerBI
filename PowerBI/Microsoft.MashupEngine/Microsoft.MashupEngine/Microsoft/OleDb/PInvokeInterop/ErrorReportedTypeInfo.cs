using System;

namespace Microsoft.OleDb.PInvokeInterop
{
	// Token: 0x02001F70 RID: 8048
	internal class ErrorReportedTypeInfo : InterfaceTypeInfo<IErrorReported>
	{
		// Token: 0x0600C4AC RID: 50348 RVA: 0x00274430 File Offset: 0x00272630
		private static int IsReported(IntPtr objHandle)
		{
			return InterfaceTypeInfo<IErrorReported>.InvokeAndReturnHResult(() => InterfaceTypeInfo<IErrorReported>.FromIntPtr(objHandle).IsReported(), objHandle);
		}

		// Token: 0x0600C4AD RID: 50349 RVA: 0x00274461 File Offset: 0x00272661
		protected override Delegate[] CreateDelegates()
		{
			return new Delegate[]
			{
				new ErrorReportedTypeInfo.IsReportedCallback(ErrorReportedTypeInfo.IsReported)
			};
		}

		// Token: 0x02001F71 RID: 8049
		// (Invoke) Token: 0x0600C4B0 RID: 50352
		private delegate int IsReportedCallback(IntPtr objHandle);
	}
}
