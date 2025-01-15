using System;
using System.Diagnostics;

namespace Microsoft.InfoNav.Common.Internal
{
	// Token: 0x02000089 RID: 137
	public sealed class ThrowOnFailTraceListener : TraceListener
	{
		// Token: 0x060004ED RID: 1261 RVA: 0x0000CFA3 File Offset: 0x0000B1A3
		public override void Fail(string message, string details)
		{
			if (message != null)
			{
				message = StringUtil.FormatInvariant(": {0}", message);
			}
			if (details != null)
			{
				details = StringUtil.FormatInvariant(" - {0}", details);
			}
			message = StringUtil.FormatInvariant("Assertion failed{0}{1}", message, details);
			throw new ApplicationException(message);
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x0000CFD9 File Offset: 0x0000B1D9
		public override void Write(string message)
		{
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x0000CFDB File Offset: 0x0000B1DB
		public override void WriteLine(string message)
		{
		}
	}
}
