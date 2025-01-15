using System;
using Microsoft.AnalysisServices.Tabular.Json;
using Microsoft.AnalysisServices.Tabular.Json.Linq;

namespace Microsoft.AnalysisServices.Tabular.Extensions
{
	// Token: 0x020001C4 RID: 452
	internal static class JTokenExtensions
	{
		// Token: 0x06001BCC RID: 7116 RVA: 0x000C2FB4 File Offset: 0x000C11B4
		internal static void VerifyTokenType(this JToken token, JTokenType expectedType)
		{
			if (token.Type != expectedType)
			{
				throw JsonSerializationUtil.CreateException(TomSR.Exception_UnexpectedJsonToken(expectedType.ToString(), token.Type.ToString()), token, null);
			}
		}
	}
}
