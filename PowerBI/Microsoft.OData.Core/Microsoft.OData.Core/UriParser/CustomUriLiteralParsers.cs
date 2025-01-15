using System;
using System.Linq;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000117 RID: 279
	public sealed class CustomUriLiteralParsers : IUriLiteralParser
	{
		// Token: 0x06000F83 RID: 3971 RVA: 0x000036A9 File Offset: 0x000018A9
		private CustomUriLiteralParsers()
		{
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000F84 RID: 3972 RVA: 0x00026643 File Offset: 0x00024843
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

		// Token: 0x06000F85 RID: 3973 RVA: 0x0002665C File Offset: 0x0002485C
		public object ParseUriStringToType(string text, IEdmTypeReference targetType, out UriLiteralParsingException parsingException)
		{
			IUriLiteralParser uriLiteralParserByEdmType = CustomUriLiteralParsers.GetUriLiteralParserByEdmType(targetType);
			if (uriLiteralParserByEdmType != null)
			{
				return uriLiteralParserByEdmType.ParseUriStringToType(text, targetType, out parsingException);
			}
			IUriLiteralParser[] array = CustomUriLiteralParsers.customUriLiteralParsers;
			foreach (IUriLiteralParser uriLiteralParser in array)
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
			parsingException = null;
			return null;
		}

		// Token: 0x06000F86 RID: 3974 RVA: 0x000266B8 File Offset: 0x000248B8
		public static void AddCustomUriLiteralParser(IUriLiteralParser customUriLiteralParser)
		{
			ExceptionUtils.CheckArgumentNotNull<IUriLiteralParser>(customUriLiteralParser, "customUriLiteralParser");
			object locker = CustomUriLiteralParsers.Locker;
			lock (locker)
			{
				if (CustomUriLiteralParsers.customUriLiteralParsers.Contains(customUriLiteralParser))
				{
					throw new ODataException(Strings.UriCustomTypeParsers_AddCustomUriTypeParserAlreadyExists);
				}
				CustomUriLiteralParsers.customUriLiteralParsers = CustomUriLiteralParsers.customUriLiteralParsers.Concat(new IUriLiteralParser[] { customUriLiteralParser }).ToArray<IUriLiteralParser>();
			}
		}

		// Token: 0x06000F87 RID: 3975 RVA: 0x00026734 File Offset: 0x00024934
		public static void AddCustomUriLiteralParser(IEdmTypeReference edmTypeReference, IUriLiteralParser customUriLiteralParser)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmTypeReference>(edmTypeReference, "edmTypeReference");
			ExceptionUtils.CheckArgumentNotNull<IUriLiteralParser>(customUriLiteralParser, "customUriLiteralParser");
			object locker = CustomUriLiteralParsers.Locker;
			lock (locker)
			{
				if (CustomUriLiteralParsers.IsEdmTypeAlreadyRegistered(edmTypeReference))
				{
					throw new ODataException(Strings.UriCustomTypeParsers_AddCustomUriTypeParserEdmTypeExists(edmTypeReference.FullName()));
				}
				CustomUriLiteralParsers.customUriLiteralParserPerEdmType = CustomUriLiteralParsers.customUriLiteralParserPerEdmType.Concat(new CustomUriLiteralParsers.UriLiteralParserPerEdmType[]
				{
					new CustomUriLiteralParsers.UriLiteralParserPerEdmType
					{
						EdmTypeOfUriParser = edmTypeReference,
						UriLiteralParser = customUriLiteralParser
					}
				}).ToArray<CustomUriLiteralParsers.UriLiteralParserPerEdmType>();
			}
		}

		// Token: 0x06000F88 RID: 3976 RVA: 0x000267D0 File Offset: 0x000249D0
		public static bool RemoveCustomUriLiteralParser(IUriLiteralParser customUriLiteralParser)
		{
			ExceptionUtils.CheckArgumentNotNull<IUriLiteralParser>(customUriLiteralParser, "customUriLiteralParser");
			object locker = CustomUriLiteralParsers.Locker;
			bool flag3;
			lock (locker)
			{
				CustomUriLiteralParsers.UriLiteralParserPerEdmType[] array = CustomUriLiteralParsers.customUriLiteralParserPerEdmType.Where((CustomUriLiteralParsers.UriLiteralParserPerEdmType parser) => !parser.UriLiteralParser.Equals(customUriLiteralParser)).ToArray<CustomUriLiteralParsers.UriLiteralParserPerEdmType>();
				IUriLiteralParser[] array2 = CustomUriLiteralParsers.customUriLiteralParsers.Where((IUriLiteralParser parser) => !parser.Equals(customUriLiteralParser)).ToArray<IUriLiteralParser>();
				bool flag2 = array.Length < CustomUriLiteralParsers.customUriLiteralParserPerEdmType.Length || array2.Length < CustomUriLiteralParsers.customUriLiteralParsers.Length;
				CustomUriLiteralParsers.customUriLiteralParserPerEdmType = array;
				CustomUriLiteralParsers.customUriLiteralParsers = array2;
				flag3 = flag2;
			}
			return flag3;
		}

		// Token: 0x06000F89 RID: 3977 RVA: 0x00026894 File Offset: 0x00024A94
		private static bool IsEdmTypeAlreadyRegistered(IEdmTypeReference edmTypeReference)
		{
			return CustomUriLiteralParsers.customUriLiteralParserPerEdmType.Any((CustomUriLiteralParsers.UriLiteralParserPerEdmType uriParserOfEdmType) => uriParserOfEdmType.EdmTypeOfUriParser.IsEquivalentTo(edmTypeReference));
		}

		// Token: 0x06000F8A RID: 3978 RVA: 0x000268C4 File Offset: 0x00024AC4
		private static IUriLiteralParser GetUriLiteralParserByEdmType(IEdmTypeReference edmTypeReference)
		{
			CustomUriLiteralParsers.UriLiteralParserPerEdmType uriLiteralParserPerEdmType = CustomUriLiteralParsers.customUriLiteralParserPerEdmType.FirstOrDefault((CustomUriLiteralParsers.UriLiteralParserPerEdmType uriParserOfEdmType) => uriParserOfEdmType.EdmTypeOfUriParser.IsEquivalentTo(edmTypeReference));
			if (uriLiteralParserPerEdmType == null)
			{
				return null;
			}
			return uriLiteralParserPerEdmType.UriLiteralParser;
		}

		// Token: 0x0400078E RID: 1934
		private static readonly object Locker = new object();

		// Token: 0x0400078F RID: 1935
		private static IUriLiteralParser[] customUriLiteralParsers = new IUriLiteralParser[0];

		// Token: 0x04000790 RID: 1936
		private static CustomUriLiteralParsers.UriLiteralParserPerEdmType[] customUriLiteralParserPerEdmType = new CustomUriLiteralParsers.UriLiteralParserPerEdmType[0];

		// Token: 0x04000791 RID: 1937
		private static CustomUriLiteralParsers singleInstance;

		// Token: 0x02000375 RID: 885
		private sealed class UriLiteralParserPerEdmType
		{
			// Token: 0x17000631 RID: 1585
			// (get) Token: 0x06001F24 RID: 7972 RVA: 0x0005A35C File Offset: 0x0005855C
			// (set) Token: 0x06001F25 RID: 7973 RVA: 0x0005A364 File Offset: 0x00058564
			internal IEdmTypeReference EdmTypeOfUriParser { get; set; }

			// Token: 0x17000632 RID: 1586
			// (get) Token: 0x06001F26 RID: 7974 RVA: 0x0005A36D File Offset: 0x0005856D
			// (set) Token: 0x06001F27 RID: 7975 RVA: 0x0005A375 File Offset: 0x00058575
			internal IUriLiteralParser UriLiteralParser { get; set; }
		}
	}
}
