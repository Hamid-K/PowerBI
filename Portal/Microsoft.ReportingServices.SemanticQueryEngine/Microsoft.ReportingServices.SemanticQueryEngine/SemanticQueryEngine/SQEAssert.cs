using System;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.SemanticQueryEngine
{
	// Token: 0x0200000F RID: 15
	internal static class SQEAssert
	{
		// Token: 0x060000BA RID: 186 RVA: 0x00004BC1 File Offset: 0x00002DC1
		internal static Exception AssertFalseAndThrow()
		{
			return SQEAssert.AssertFalseAndThrow(null, Array.Empty<object>());
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00004BCE File Offset: 0x00002DCE
		internal static Exception AssertFalseAndThrow(Exception e)
		{
			return SQEAssert.AssertFalseAndThrow(e.ToString(), Array.Empty<object>());
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00004BE0 File Offset: 0x00002DE0
		internal static Exception AssertFalseAndThrow(string format, params object[] args)
		{
			string text = string.Empty;
			if (format != null)
			{
				try
				{
					text = Microsoft.ReportingServices.Common.StringUtil.FormatInvariant(format, args);
				}
				catch
				{
				}
			}
			RSTrace.SQETracer.Assert(false, text);
			throw new Exception(text);
		}
	}
}
