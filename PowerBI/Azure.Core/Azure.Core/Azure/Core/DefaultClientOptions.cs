using System;
using System.Runtime.CompilerServices;

namespace Azure.Core
{
	// Token: 0x02000040 RID: 64
	internal class DefaultClientOptions : ClientOptions
	{
		// Token: 0x060001D1 RID: 465 RVA: 0x00005C50 File Offset: 0x00003E50
		public DefaultClientOptions()
			: base(null, null)
		{
			base.Diagnostics.IsTelemetryEnabled = (!DefaultClientOptions.EnvironmentVariableToBool(Environment.GetEnvironmentVariable("AZURE_TELEMETRY_DISABLED"))) ?? true;
			base.Diagnostics.IsDistributedTracingEnabled = (!DefaultClientOptions.EnvironmentVariableToBool(Environment.GetEnvironmentVariable("AZURE_TRACING_DISABLED"))) ?? true;
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00005D0C File Offset: 0x00003F0C
		[NullableContext(2)]
		private static bool? EnvironmentVariableToBool(string value)
		{
			if (string.Equals(bool.TrueString, value, StringComparison.OrdinalIgnoreCase) || string.Equals("1", value, StringComparison.OrdinalIgnoreCase))
			{
				return new bool?(true);
			}
			if (string.Equals(bool.FalseString, value, StringComparison.OrdinalIgnoreCase) || string.Equals("0", value, StringComparison.OrdinalIgnoreCase))
			{
				return new bool?(false);
			}
			return null;
		}
	}
}
