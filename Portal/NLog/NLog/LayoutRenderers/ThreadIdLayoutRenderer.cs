using System;
using System.Text;
using NLog.Common;
using NLog.Config;
using NLog.Internal;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000EB RID: 235
	[LayoutRenderer("threadid")]
	[ThreadSafe]
	public class ThreadIdLayoutRenderer : LayoutRenderer
	{
		// Token: 0x06000D8A RID: 3466 RVA: 0x00022574 File Offset: 0x00020774
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			builder.AppendInvariant(ThreadIdLayoutRenderer.GetValue());
		}

		// Token: 0x06000D8B RID: 3467 RVA: 0x00022581 File Offset: 0x00020781
		private static int GetValue()
		{
			return AsyncHelpers.GetManagedThreadId();
		}
	}
}
