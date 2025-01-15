using System;
using System.Globalization;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x020000AD RID: 173
	public abstract class ANTraceSourceBase<T> : TraceSourceBase<T> where T : TraceSourceBase<T>, new()
	{
		// Token: 0x0600060F RID: 1551 RVA: 0x00010DE3 File Offset: 0x0000EFE3
		public void TraceExpectedError(Exception exception)
		{
			base.TraceError("Expected error occured: {0}", new object[] { exception });
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x00010DFC File Offset: 0x0000EFFC
		public void TraceExpectedError(Exception exception, string format, params object[] args)
		{
			if (base.ShouldTrace(TraceVerbosity.Warning))
			{
				string text = string.Format(CultureInfo.InvariantCulture, format, args);
				if (exception != null)
				{
					text = "Expected error occured: {0}: {1}".FormatWithInvariantCulture(new object[] { text, exception.Message });
				}
				base.TraceWarning(text);
			}
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x00010E47 File Offset: 0x0000F047
		public void TraceUnexpectedError(Exception exception)
		{
			base.TraceError("Unexpected error occured: {0}", new object[] { exception });
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x00010E60 File Offset: 0x0000F060
		public void TraceUnexpectedError(Exception exception, string format, params object[] args)
		{
			if (base.ShouldTrace(TraceVerbosity.Error))
			{
				string text = string.Format(CultureInfo.InvariantCulture, format, args);
				if (exception != null)
				{
					text = string.Format(CultureInfo.InvariantCulture, "UnExpected error occured: {0}: {1}", text, exception);
				}
				base.TraceError(text);
			}
		}
	}
}
