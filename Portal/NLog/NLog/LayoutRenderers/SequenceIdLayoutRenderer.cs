using System;
using System.Text;
using NLog.Config;
using NLog.Internal;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000E5 RID: 229
	[LayoutRenderer("sequenceid")]
	[ThreadAgnostic]
	[ThreadSafe]
	public class SequenceIdLayoutRenderer : LayoutRenderer, IRawValue
	{
		// Token: 0x06000D61 RID: 3425 RVA: 0x00022138 File Offset: 0x00020338
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			builder.AppendInvariant(SequenceIdLayoutRenderer.GetValue(logEvent));
		}

		// Token: 0x06000D62 RID: 3426 RVA: 0x00022146 File Offset: 0x00020346
		bool IRawValue.TryGetRawValue(LogEventInfo logEvent, out object value)
		{
			value = SequenceIdLayoutRenderer.GetValue(logEvent);
			return true;
		}

		// Token: 0x06000D63 RID: 3427 RVA: 0x00022156 File Offset: 0x00020356
		private static int GetValue(LogEventInfo logEvent)
		{
			return logEvent.SequenceID;
		}
	}
}
