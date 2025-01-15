using System;
using System.Collections.Generic;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001BE RID: 446
	public static class CustomUriLiteralPrefixes
	{
		// Token: 0x060011A5 RID: 4517 RVA: 0x0003105C File Offset: 0x0002F25C
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

		// Token: 0x060011A6 RID: 4518 RVA: 0x000310D0 File Offset: 0x0002F2D0
		public static bool RemoveCustomLiteralPrefix(string literalPrefix)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(literalPrefix, "literalPrefix");
			UriParserHelper.ValidatePrefixLiteral(literalPrefix);
			object locker = CustomUriLiteralPrefixes.Locker;
			bool flag;
			lock (locker)
			{
				flag = CustomUriLiteralPrefixes.CustomLiteralPrefixesOfEdmTypes.Remove(literalPrefix);
			}
			return flag;
		}

		// Token: 0x060011A7 RID: 4519 RVA: 0x00031120 File Offset: 0x0002F320
		internal static IEdmTypeReference GetEdmTypeByCustomLiteralPrefix(string literalPrefix)
		{
			object locker = CustomUriLiteralPrefixes.Locker;
			lock (locker)
			{
				IEdmTypeReference edmTypeReference;
				if (CustomUriLiteralPrefixes.CustomLiteralPrefixesOfEdmTypes.TryGetValue(literalPrefix, ref edmTypeReference))
				{
					return edmTypeReference;
				}
			}
			return null;
		}

		// Token: 0x040008F4 RID: 2292
		private static readonly object Locker = new object();

		// Token: 0x040008F5 RID: 2293
		private static Dictionary<string, IEdmTypeReference> CustomLiteralPrefixesOfEdmTypes = new Dictionary<string, IEdmTypeReference>(StringComparer.Ordinal);
	}
}
