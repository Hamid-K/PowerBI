using System;
using System.Collections.Generic;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200013A RID: 314
	public static class CustomUriLiteralPrefixes
	{
		// Token: 0x06001073 RID: 4211 RVA: 0x0002CABC File Offset: 0x0002ACBC
		public static void AddCustomLiteralPrefix(string literalPrefix, IEdmTypeReference literalEdmTypeReference)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmTypeReference>(literalEdmTypeReference, "literalEdmTypeReference");
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(literalPrefix, "literalPrefix");
			UriParserHelper.ValidatePrefixLiteral(literalPrefix);
			object locker = CustomUriLiteralPrefixes.Locker;
			lock (locker)
			{
				if (CustomUriLiteralPrefixes.CustomLiteralPrefixesOfEdmTypes.ContainsKey(literalPrefix))
				{
					throw new ODataException(Strings.CustomUriTypePrefixLiterals_AddCustomUriTypePrefixLiteralAlreadyExists(literalPrefix));
				}
				CustomUriLiteralPrefixes.CustomLiteralPrefixesOfEdmTypes.Add(literalPrefix, literalEdmTypeReference);
			}
		}

		// Token: 0x06001074 RID: 4212 RVA: 0x0002CB38 File Offset: 0x0002AD38
		public static bool RemoveCustomLiteralPrefix(string literalPrefix)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(literalPrefix, "literalPrefix");
			UriParserHelper.ValidatePrefixLiteral(literalPrefix);
			object locker = CustomUriLiteralPrefixes.Locker;
			bool flag2;
			lock (locker)
			{
				flag2 = CustomUriLiteralPrefixes.CustomLiteralPrefixesOfEdmTypes.Remove(literalPrefix);
			}
			return flag2;
		}

		// Token: 0x06001075 RID: 4213 RVA: 0x0002CB90 File Offset: 0x0002AD90
		internal static IEdmTypeReference GetEdmTypeByCustomLiteralPrefix(string literalPrefix)
		{
			object locker = CustomUriLiteralPrefixes.Locker;
			lock (locker)
			{
				IEdmTypeReference edmTypeReference;
				if (CustomUriLiteralPrefixes.CustomLiteralPrefixesOfEdmTypes.TryGetValue(literalPrefix, out edmTypeReference))
				{
					return edmTypeReference;
				}
			}
			return null;
		}

		// Token: 0x040007B5 RID: 1973
		private static readonly object Locker = new object();

		// Token: 0x040007B6 RID: 1974
		private static Dictionary<string, IEdmTypeReference> CustomLiteralPrefixesOfEdmTypes = new Dictionary<string, IEdmTypeReference>(StringComparer.Ordinal);
	}
}
