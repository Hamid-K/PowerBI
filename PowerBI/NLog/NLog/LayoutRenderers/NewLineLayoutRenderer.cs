using System;
using System.Text;
using NLog.Config;
using NLog.Internal;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000DB RID: 219
	[LayoutRenderer("newline")]
	[ThreadAgnostic]
	[ThreadSafe]
	[AppDomainFixedOutput]
	public class NewLineLayoutRenderer : LayoutRenderer
	{
		// Token: 0x06000D0C RID: 3340 RVA: 0x0002158E File Offset: 0x0001F78E
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			builder.Append(EnvironmentHelper.NewLine);
		}
	}
}
