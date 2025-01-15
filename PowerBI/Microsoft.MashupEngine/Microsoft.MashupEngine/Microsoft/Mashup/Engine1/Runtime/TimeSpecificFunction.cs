using System;
using Microsoft.Mashup.Engine.Host;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001671 RID: 5745
	internal static class TimeSpecificFunction
	{
		// Token: 0x04004E06 RID: 19974
		public static FunctionValue DateTimeLocalNow = new Library.DateTime.LocalNowFunctionValue(EngineHost.Empty);

		// Token: 0x04004E07 RID: 19975
		public static FunctionValue DateTimeFixedLocalNow = new Library.DateTime.FixedLocalNowFunctionValue(EngineHost.Empty);

		// Token: 0x04004E08 RID: 19976
		public static FunctionValue DateTimeZoneLocalNow = new Library.DateTimeZone.LocalNowFunctionValue(EngineHost.Empty);

		// Token: 0x04004E09 RID: 19977
		public static FunctionValue DateTimeZoneUtcNow = new Library.DateTimeZone.UtcNowFunctionValue(EngineHost.Empty);

		// Token: 0x04004E0A RID: 19978
		public static FunctionValue DateTimeZoneFixedLocalNow = new Library.DateTimeZone.FixedLocalNowFunctionValue(EngineHost.Empty);

		// Token: 0x04004E0B RID: 19979
		public static FunctionValue DateTimeZoneFixedUtcNow = new Library.DateTimeZone.FixedUtcNowFunctionValue(EngineHost.Empty);
	}
}
