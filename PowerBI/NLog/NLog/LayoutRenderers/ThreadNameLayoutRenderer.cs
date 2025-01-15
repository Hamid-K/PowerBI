using System;
using System.Text;
using System.Threading;
using NLog.Config;
using NLog.Internal;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000EC RID: 236
	[LayoutRenderer("threadname")]
	[ThreadSafe]
	public class ThreadNameLayoutRenderer : LayoutRenderer, IStringValueRenderer
	{
		// Token: 0x06000D8D RID: 3469 RVA: 0x00022590 File Offset: 0x00020790
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			builder.Append(ThreadNameLayoutRenderer.GetStringValue());
		}

		// Token: 0x06000D8E RID: 3470 RVA: 0x0002259E File Offset: 0x0002079E
		private static string GetStringValue()
		{
			return Thread.CurrentThread.Name;
		}

		// Token: 0x06000D8F RID: 3471 RVA: 0x000225AA File Offset: 0x000207AA
		string IStringValueRenderer.GetFormattedString(LogEventInfo logEvent)
		{
			return ThreadNameLayoutRenderer.GetStringValue();
		}
	}
}
