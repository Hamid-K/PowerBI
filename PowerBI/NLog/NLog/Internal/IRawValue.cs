using System;

namespace NLog.Internal
{
	// Token: 0x02000120 RID: 288
	internal interface IRawValue
	{
		// Token: 0x06000ED0 RID: 3792
		bool TryGetRawValue(LogEventInfo logEvent, out object value);
	}
}
