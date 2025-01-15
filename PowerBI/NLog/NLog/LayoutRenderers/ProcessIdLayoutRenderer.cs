using System;
using System.Text;
using NLog.Config;
using NLog.Internal;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000DE RID: 222
	[LayoutRenderer("processid")]
	[AppDomainFixedOutput]
	[ThreadAgnostic]
	[ThreadSafe]
	public class ProcessIdLayoutRenderer : LayoutRenderer, IRawValue
	{
		// Token: 0x06000D2B RID: 3371 RVA: 0x0002199B File Offset: 0x0001FB9B
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			builder.AppendInvariant(this.GetValue());
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x000219A9 File Offset: 0x0001FBA9
		bool IRawValue.TryGetRawValue(LogEventInfo logEvent, out object value)
		{
			value = this.GetValue();
			return true;
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x000219B9 File Offset: 0x0001FBB9
		private int GetValue()
		{
			return ProcessIDHelper.Instance.CurrentProcessID;
		}
	}
}
