using System;
using System.Text;
using NLog.Config;
using NLog.Internal;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000BD RID: 189
	[LayoutRenderer("db-null")]
	[ThreadSafe]
	[ThreadAgnostic]
	public class DbNullLayoutRenderer : LayoutRenderer, IRawValue
	{
		// Token: 0x06000BF0 RID: 3056 RVA: 0x0001EE13 File Offset: 0x0001D013
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x0001EE15 File Offset: 0x0001D015
		bool IRawValue.TryGetRawValue(LogEventInfo logEvent, out object value)
		{
			value = DBNull.Value;
			return true;
		}
	}
}
