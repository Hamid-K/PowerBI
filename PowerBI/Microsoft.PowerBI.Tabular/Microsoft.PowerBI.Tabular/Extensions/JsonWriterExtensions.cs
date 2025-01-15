using System;
using System.IO;
using Microsoft.AnalysisServices.Tabular.Json;

namespace Microsoft.AnalysisServices.Tabular.Extensions
{
	// Token: 0x020001C3 RID: 451
	internal static class JsonWriterExtensions
	{
		// Token: 0x06001BCB RID: 7115 RVA: 0x000C2F60 File Offset: 0x000C1160
		internal static void WriteRawJson(this JsonWriter writer, string json)
		{
			using (StringReader stringReader = new StringReader(json))
			{
				using (JsonTextReader jsonTextReader = new JsonTextReader(stringReader))
				{
					writer.WriteToken(jsonTextReader);
				}
			}
		}
	}
}
