using System;
using Microsoft.AnalysisServices.Tabular.DataRefresh;
using Microsoft.AnalysisServices.Tabular.Json;

namespace Microsoft.AnalysisServices.Tabular.Extensions
{
	// Token: 0x020001C5 RID: 453
	internal static class ObjectOverrideExtensions
	{
		// Token: 0x06001BCD RID: 7117 RVA: 0x000C2FF8 File Offset: 0x000C11F8
		internal static void ReadFromJson(this IObjectOverride overrideObject, JsonTextReader jsonReader)
		{
			jsonReader.VerifyToken(1);
			jsonReader.Read();
			while (jsonReader.TokenType != 13)
			{
				jsonReader.VerifyToken(4);
				if (!overrideObject.ReadPropertyFromJson(jsonReader))
				{
					throw JsonSerializationUtil.CreateException(TomSR.Exception_UnexpectedJsonProperty((string)jsonReader.Value), null);
				}
			}
			jsonReader.Read();
		}
	}
}
