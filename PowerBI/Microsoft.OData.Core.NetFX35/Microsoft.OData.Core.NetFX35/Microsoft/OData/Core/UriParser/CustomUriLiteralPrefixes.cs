using System;
using System.Collections.Generic;
using Microsoft.OData.Core.UriParser.Parsers.UriParsers;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser
{
	// Token: 0x020002BA RID: 698
	public static class CustomUriLiteralPrefixes
	{
		// Token: 0x06001824 RID: 6180 RVA: 0x000522BC File Offset: 0x000504BC
		public static void AddCustomLiteralPrefix(string literalPrefix, IEdmTypeReference literalEdmTypeReference)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmTypeReference>(literalEdmTypeReference, "literalEdmTypeReference");
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(literalPrefix, "literalPrefix");
			UriParserHelper.ValidatePrefixLiteral(literalPrefix);
			lock (CustomUriLiteralPrefixes.Locker)
			{
				if (CustomUriLiteralPrefixes.CustomLiteralPrefixesOfEdmTypes.ContainsKey(literalPrefix))
				{
					throw new ODataException(Strings.CustomUriTypePrefixLiterals_AddCustomUriTypePrefixLiteralAlreadyExists(literalPrefix));
				}
				CustomUriLiteralPrefixes.CustomLiteralPrefixesOfEdmTypes.Add(literalPrefix, literalEdmTypeReference);
			}
		}

		// Token: 0x06001825 RID: 6181 RVA: 0x00052330 File Offset: 0x00050530
		public static bool RemoveCustomLiteralPrefix(string literalPrefix)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(literalPrefix, "literalPrefix");
			UriParserHelper.ValidatePrefixLiteral(literalPrefix);
			bool flag;
			lock (CustomUriLiteralPrefixes.Locker)
			{
				flag = CustomUriLiteralPrefixes.CustomLiteralPrefixesOfEdmTypes.Remove(literalPrefix);
			}
			return flag;
		}

		// Token: 0x06001826 RID: 6182 RVA: 0x00052380 File Offset: 0x00050580
		internal static IEdmTypeReference GetEdmTypeByCustomLiteralPrefix(string literalPrefix)
		{
			lock (CustomUriLiteralPrefixes.Locker)
			{
				IEdmTypeReference edmTypeReference;
				if (CustomUriLiteralPrefixes.CustomLiteralPrefixesOfEdmTypes.TryGetValue(literalPrefix, ref edmTypeReference))
				{
					return edmTypeReference;
				}
			}
			return null;
		}

		// Token: 0x04000A42 RID: 2626
		private static readonly object Locker = new object();

		// Token: 0x04000A43 RID: 2627
		private static Dictionary<string, IEdmTypeReference> CustomLiteralPrefixesOfEdmTypes = new Dictionary<string, IEdmTypeReference>(StringComparer.Ordinal);
	}
}
