using System;
using System.ComponentModel;
using System.Globalization;
using System.Threading;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation.Tracing
{
	// Token: 0x0200009D RID: 157
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class Extensions
	{
		// Token: 0x060004EE RID: 1262 RVA: 0x00014EDC File Offset: 0x000130DC
		public static string ToInvariantString(this Exception exception)
		{
			CultureInfo currentUICulture = Thread.CurrentThread.CurrentUICulture;
			string text;
			try
			{
				Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
				text = exception.ToString();
			}
			finally
			{
				Thread.CurrentThread.CurrentUICulture = currentUICulture;
			}
			return text;
		}
	}
}
