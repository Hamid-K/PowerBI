using System;
using Microsoft.AnalysisServices.Tabular.Json;

namespace Microsoft.AnalysisServices.Tabular.Extensions
{
	// Token: 0x020001C2 RID: 450
	internal static class JsonTextReaderExtensions
	{
		// Token: 0x06001BCA RID: 7114 RVA: 0x000C2F1C File Offset: 0x000C111C
		internal static void VerifyToken(this JsonTextReader reader, JsonToken expectedToken)
		{
			if (reader.TokenType != expectedToken)
			{
				throw JsonSerializationUtil.CreateException(TomSR.Exception_UnexpectedJsonToken(expectedToken.ToString(), reader.TokenType.ToString()), reader, null);
			}
		}
	}
}
