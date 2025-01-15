using System;

namespace Microsoft.PowerBI.DataExtension.Contracts.Internal
{
	// Token: 0x02000021 RID: 33
	public static class TelemetryIdsExtensions
	{
		// Token: 0x0600008F RID: 143 RVA: 0x00002F3C File Offset: 0x0000113C
		public static void SetTelemetryIds(this IDbCommand command, ITelemetryIdsProvider telemetryIdsProvider)
		{
			string text;
			string text2;
			string text3;
			string text4;
			if (telemetryIdsProvider.TryGetTelemetryIds(out text, out text2, out text3, out text4))
			{
				command.SetTelemetryIds(text, text2, text3);
				if (!string.IsNullOrEmpty(text4))
				{
					command.SetApplicationContext(text4);
				}
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00002F74 File Offset: 0x00001174
		public static void SetTelemetryIds(this IDbSchemaCommand command, ITelemetryIdsProvider telemetryIdsProvider)
		{
			string text;
			string text2;
			string text3;
			string text4;
			if (telemetryIdsProvider.TryGetTelemetryIds(out text, out text2, out text3, out text4))
			{
				command.SetTelemetryIds(text, text2, text3);
				if (!string.IsNullOrEmpty(text4))
				{
					command.SetApplicationContext(text4);
				}
			}
		}
	}
}
