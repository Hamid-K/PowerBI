using System;
using Microsoft.ReportingServices.Portal.Interfaces.Enums;

namespace Microsoft.ReportingServices.Portal.ODataWebApi.Utils
{
	// Token: 0x02000036 RID: 54
	public class IsRenderingSupportedResult
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x0000B593 File Offset: 0x00009793
		// (set) Token: 0x060002B3 RID: 691 RVA: 0x0000B59B File Offset: 0x0000979B
		public bool IsSupported { get; private set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x0000B5A4 File Offset: 0x000097A4
		// (set) Token: 0x060002B5 RID: 693 RVA: 0x0000B5AC File Offset: 0x000097AC
		public ErrorCode ErrorCode { get; private set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x0000B5B5 File Offset: 0x000097B5
		// (set) Token: 0x060002B7 RID: 695 RVA: 0x0000B5BD File Offset: 0x000097BD
		public string ErrorMessage { get; private set; }

		// Token: 0x060002B8 RID: 696 RVA: 0x0000B5C6 File Offset: 0x000097C6
		public static IsRenderingSupportedResult RenderingSupported()
		{
			return new IsRenderingSupportedResult
			{
				IsSupported = true
			};
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000B5D4 File Offset: 0x000097D4
		public static IsRenderingSupportedResult RenderingUnsupported(ErrorCode errorCode, string errorMessage)
		{
			return new IsRenderingSupportedResult
			{
				IsSupported = false,
				ErrorCode = errorCode,
				ErrorMessage = errorMessage
			};
		}
	}
}
