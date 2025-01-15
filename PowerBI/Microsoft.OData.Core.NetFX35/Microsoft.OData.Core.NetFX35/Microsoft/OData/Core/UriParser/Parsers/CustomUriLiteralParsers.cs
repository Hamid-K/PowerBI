using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Core.UriParser.Parsers.Common;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Parsers
{
	// Token: 0x020002BE RID: 702
	public sealed class CustomUriLiteralParsers : IUriLiteralParser
	{
		// Token: 0x06001830 RID: 6192 RVA: 0x000524B8 File Offset: 0x000506B8
		private CustomUriLiteralParsers()
		{
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x06001831 RID: 6193 RVA: 0x000524C0 File Offset: 0x000506C0
		internal static CustomUriLiteralParsers Instance
		{
			get
			{
				if (CustomUriLiteralParsers.singleInstance == null)
				{
					CustomUriLiteralParsers.singleInstance = new CustomUriLiteralParsers();
				}
				return CustomUriLiteralParsers.singleInstance;
			}
		}

		// Token: 0x06001832 RID: 6194 RVA: 0x000524D8 File Offset: 0x000506D8
		public object ParseUriStringToType(string text, IEdmTypeReference targetType, out UriLiteralParsingException parsingException)
		{
			lock (CustomUriLiteralParsers.Locker)
			{
				IUriLiteralParser uriLiteralParserByEdmType = CustomUriLiteralParsers.GetUriLiteralParserByEdmType(targetType);
				if (uriLiteralParserByEdmType != null)
				{
					return uriLiteralParserByEdmType.ParseUriStringToType(text, targetType, out parsingException);
				}
				foreach (IUriLiteralParser uriLiteralParser in CustomUriLiteralParsers.customUriLiteralParsers)
				{
					object obj = uriLiteralParser.ParseUriStringToType(text, targetType, out parsingException);
					if (parsingException != null)
					{
						return null;
					}
					if (obj != null)
					{
						return obj;
					}
				}
			}
			parsingException = null;
			return null;
		}

		// Token: 0x06001833 RID: 6195 RVA: 0x0005257C File Offset: 0x0005077C
		public static void AddCustomUriLiteralParser(IUriLiteralParser customUriLiteralParser)
		{
			ExceptionUtils.CheckArgumentNotNull<IUriLiteralParser>(customUriLiteralParser, "customUriLiteralParser");
			lock (CustomUriLiteralParsers.Locker)
			{
				if (CustomUriLiteralParsers.customUriLiteralParsers.Contains(customUriLiteralParser))
				{
					throw new ODataException(Strings.UriCustomTypeParsers_AddCustomUriTypeParserAlreadyExists);
				}
				CustomUriLiteralParsers.customUriLiteralParsers.Add(customUriLiteralParser);
			}
		}

		// Token: 0x06001834 RID: 6196 RVA: 0x000525DC File Offset: 0x000507DC
		public static void AddCustomUriLiteralParser(IEdmTypeReference edmTypeReference, IUriLiteralParser customUriLiteralParser)
		{
			ExceptionUtils.CheckArgumentNotNull<IUriLiteralParser>(customUriLiteralParser, "customUriLiteralParser");
			ExceptionUtils.CheckArgumentNotNull<IEdmTypeReference>(edmTypeReference, "edmTypeReference");
			lock (CustomUriLiteralParsers.Locker)
			{
				if (CustomUriLiteralParsers.IsEdmTypeAlreadyRegistered(edmTypeReference))
				{
					throw new ODataException(Strings.UriCustomTypeParsers_AddCustomUriTypeParserEdmTypeExists(edmTypeReference.FullName()));
				}
				CustomUriLiteralParsers.customUriLiteralParserPerEdmType.Add(new CustomUriLiteralParsers.UriLiteralParserPerEdmType
				{
					EdmTypeOfUriParser = edmTypeReference,
					UriLiteralParser = customUriLiteralParser
				});
			}
		}

		// Token: 0x06001835 RID: 6197 RVA: 0x00052678 File Offset: 0x00050878
		public static bool RemoveCustomUriLiteralParser(IUriLiteralParser customUriLiteralParser)
		{
			ExceptionUtils.CheckArgumentNotNull<IUriLiteralParser>(customUriLiteralParser, "customUriLiteralParser");
			bool flag2;
			lock (CustomUriLiteralParsers.Locker)
			{
				int num = CustomUriLiteralParsers.customUriLiteralParserPerEdmType.RemoveAll((CustomUriLiteralParsers.UriLiteralParserPerEdmType parser) => parser.UriLiteralParser.Equals(customUriLiteralParser));
				bool flag = CustomUriLiteralParsers.customUriLiteralParsers.Remove(customUriLiteralParser);
				flag2 = num > 0 || flag;
			}
			return flag2;
		}

		// Token: 0x06001836 RID: 6198 RVA: 0x00052720 File Offset: 0x00050920
		private static bool IsEdmTypeAlreadyRegistered(IEdmTypeReference edmTypeReference)
		{
			return Enumerable.Any<CustomUriLiteralParsers.UriLiteralParserPerEdmType>(CustomUriLiteralParsers.customUriLiteralParserPerEdmType, (CustomUriLiteralParsers.UriLiteralParserPerEdmType uriParserOfEdmType) => uriParserOfEdmType.EdmTypeOfUriParser.IsEquivalentTo(edmTypeReference));
		}

		// Token: 0x06001837 RID: 6199 RVA: 0x0005276C File Offset: 0x0005096C
		private static IUriLiteralParser GetUriLiteralParserByEdmType(IEdmTypeReference edmTypeReference)
		{
			CustomUriLiteralParsers.UriLiteralParserPerEdmType uriLiteralParserPerEdmType = Enumerable.FirstOrDefault<CustomUriLiteralParsers.UriLiteralParserPerEdmType>(CustomUriLiteralParsers.customUriLiteralParserPerEdmType, (CustomUriLiteralParsers.UriLiteralParserPerEdmType uriParserOfEdmType) => uriParserOfEdmType.EdmTypeOfUriParser.IsEquivalentTo(edmTypeReference));
			if (uriLiteralParserPerEdmType == null)
			{
				return null;
			}
			return uriLiteralParserPerEdmType.UriLiteralParser;
		}

		// Token: 0x04000A46 RID: 2630
		private static readonly object Locker = new object();

		// Token: 0x04000A47 RID: 2631
		private static List<IUriLiteralParser> customUriLiteralParsers = new List<IUriLiteralParser>();

		// Token: 0x04000A48 RID: 2632
		private static List<CustomUriLiteralParsers.UriLiteralParserPerEdmType> customUriLiteralParserPerEdmType = new List<CustomUriLiteralParsers.UriLiteralParserPerEdmType>();

		// Token: 0x04000A49 RID: 2633
		private static CustomUriLiteralParsers singleInstance;

		// Token: 0x020002BF RID: 703
		private sealed class UriLiteralParserPerEdmType
		{
			// Token: 0x1700053B RID: 1339
			// (get) Token: 0x06001839 RID: 6201 RVA: 0x000527C8 File Offset: 0x000509C8
			// (set) Token: 0x0600183A RID: 6202 RVA: 0x000527D0 File Offset: 0x000509D0
			internal IEdmTypeReference EdmTypeOfUriParser { get; set; }

			// Token: 0x1700053C RID: 1340
			// (get) Token: 0x0600183B RID: 6203 RVA: 0x000527D9 File Offset: 0x000509D9
			// (set) Token: 0x0600183C RID: 6204 RVA: 0x000527E1 File Offset: 0x000509E1
			internal IUriLiteralParser UriLiteralParser { get; set; }
		}
	}
}
