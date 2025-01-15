using System;

namespace Microsoft.ReportingServices.RdlExpressions.SafeExpressions.Utils
{
	// Token: 0x02000034 RID: 52
	internal static class TypeUtils
	{
		// Token: 0x06000112 RID: 274 RVA: 0x00006584 File Offset: 0x00004784
		public static bool IsNumeric(TypeCode typeCode)
		{
			return typeCode - TypeCode.SByte <= 10;
		}
	}
}
