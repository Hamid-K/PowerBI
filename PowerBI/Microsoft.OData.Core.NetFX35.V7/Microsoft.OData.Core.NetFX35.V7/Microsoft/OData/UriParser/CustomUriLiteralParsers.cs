using System;
using System.Linq;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001A1 RID: 417
	public sealed class CustomUriLiteralParsers : IUriLiteralParser
	{
		// Token: 0x060010E4 RID: 4324 RVA: 0x00002CFE File Offset: 0x00000EFE
		private CustomUriLiteralParsers()
		{
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x060010E5 RID: 4325 RVA: 0x0002EE3A File Offset: 0x0002D03A
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

		// Token: 0x060010E6 RID: 4326 RVA: 0x0002EE54 File Offset: 0x0002D054
		public object ParseUriStringToType(string text, IEdmTypeReference targetType, out UriLiteralParsingException parsingException)
		{
			object locker = CustomUriLiteralParsers.Locker;
			IUriLiteralParser uriLiteralParserByEdmType;
			IUriLiteralParser[] array;
			lock (locker)
			{
				uriLiteralParserByEdmType = CustomUriLiteralParsers.GetUriLiteralParserByEdmType(targetType);
				array = CustomUriLiteralParsers.customUriLiteralParsers;
			}
			if (uriLiteralParserByEdmType != null)
			{
				return uriLiteralParserByEdmType.ParseUriStringToType(text, targetType, out parsingException);
			}
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

		// Token: 0x060010E7 RID: 4327 RVA: 0x0002EED8 File Offset: 0x0002D0D8
		public static void AddCustomUriLiteralParser(IUriLiteralParser customUriLiteralParser)
		{
			ExceptionUtils.CheckArgumentNotNull<IUriLiteralParser>(customUriLiteralParser, "customUriLiteralParser");
			object locker = CustomUriLiteralParsers.Locker;
			lock (locker)
			{
				if (Enumerable.Contains<IUriLiteralParser>(CustomUriLiteralParsers.customUriLiteralParsers, customUriLiteralParser))
				{
					throw new ODataException(Strings.UriCustomTypeParsers_AddCustomUriTypeParserAlreadyExists);
				}
				CustomUriLiteralParsers.customUriLiteralParsers = Enumerable.ToArray<IUriLiteralParser>(Enumerable.Concat<IUriLiteralParser>(CustomUriLiteralParsers.customUriLiteralParsers, new IUriLiteralParser[] { customUriLiteralParser }));
			}
		}

		// Token: 0x060010E8 RID: 4328 RVA: 0x0002EF4C File Offset: 0x0002D14C
		public static void AddCustomUriLiteralParser(IEdmTypeReference edmTypeReference, IUriLiteralParser customUriLiteralParser)
		{
			ExceptionUtils.CheckArgumentNotNull<IUriLiteralParser>(customUriLiteralParser, "customUriLiteralParser");
			ExceptionUtils.CheckArgumentNotNull<IEdmTypeReference>(edmTypeReference, "edmTypeReference");
			object locker = CustomUriLiteralParsers.Locker;
			lock (locker)
			{
				if (CustomUriLiteralParsers.IsEdmTypeAlreadyRegistered(edmTypeReference))
				{
					throw new ODataException(Strings.UriCustomTypeParsers_AddCustomUriTypeParserEdmTypeExists(edmTypeReference.FullName()));
				}
				CustomUriLiteralParsers.customUriLiteralParserPerEdmType = Enumerable.ToArray<CustomUriLiteralParsers.UriLiteralParserPerEdmType>(Enumerable.Concat<CustomUriLiteralParsers.UriLiteralParserPerEdmType>(CustomUriLiteralParsers.customUriLiteralParserPerEdmType, new CustomUriLiteralParsers.UriLiteralParserPerEdmType[]
				{
					new CustomUriLiteralParsers.UriLiteralParserPerEdmType
					{
						EdmTypeOfUriParser = edmTypeReference,
						UriLiteralParser = customUriLiteralParser
					}
				}));
			}
		}

		// Token: 0x060010E9 RID: 4329 RVA: 0x0002EFE0 File Offset: 0x0002D1E0
		public static bool RemoveCustomUriLiteralParser(IUriLiteralParser customUriLiteralParser)
		{
			ExceptionUtils.CheckArgumentNotNull<IUriLiteralParser>(customUriLiteralParser, "customUriLiteralParser");
			object locker = CustomUriLiteralParsers.Locker;
			bool flag2;
			lock (locker)
			{
				CustomUriLiteralParsers.UriLiteralParserPerEdmType[] array = Enumerable.ToArray<CustomUriLiteralParsers.UriLiteralParserPerEdmType>(Enumerable.Where<CustomUriLiteralParsers.UriLiteralParserPerEdmType>(CustomUriLiteralParsers.customUriLiteralParserPerEdmType, (CustomUriLiteralParsers.UriLiteralParserPerEdmType parser) => !parser.UriLiteralParser.Equals(customUriLiteralParser)));
				IUriLiteralParser[] array2 = Enumerable.ToArray<IUriLiteralParser>(Enumerable.Where<IUriLiteralParser>(CustomUriLiteralParsers.customUriLiteralParsers, (IUriLiteralParser parser) => !parser.Equals(customUriLiteralParser)));
				bool flag = array.Length < CustomUriLiteralParsers.customUriLiteralParserPerEdmType.Length || array2.Length < CustomUriLiteralParsers.customUriLiteralParsers.Length;
				CustomUriLiteralParsers.customUriLiteralParserPerEdmType = array;
				CustomUriLiteralParsers.customUriLiteralParsers = array2;
				flag2 = flag;
			}
			return flag2;
		}

		// Token: 0x060010EA RID: 4330 RVA: 0x0002F098 File Offset: 0x0002D298
		private static bool IsEdmTypeAlreadyRegistered(IEdmTypeReference edmTypeReference)
		{
			return Enumerable.Any<CustomUriLiteralParsers.UriLiteralParserPerEdmType>(CustomUriLiteralParsers.customUriLiteralParserPerEdmType, (CustomUriLiteralParsers.UriLiteralParserPerEdmType uriParserOfEdmType) => uriParserOfEdmType.EdmTypeOfUriParser.IsEquivalentTo(edmTypeReference));
		}

		// Token: 0x060010EB RID: 4331 RVA: 0x0002F0C8 File Offset: 0x0002D2C8
		private static IUriLiteralParser GetUriLiteralParserByEdmType(IEdmTypeReference edmTypeReference)
		{
			CustomUriLiteralParsers.UriLiteralParserPerEdmType uriLiteralParserPerEdmType = Enumerable.FirstOrDefault<CustomUriLiteralParsers.UriLiteralParserPerEdmType>(CustomUriLiteralParsers.customUriLiteralParserPerEdmType, (CustomUriLiteralParsers.UriLiteralParserPerEdmType uriParserOfEdmType) => uriParserOfEdmType.EdmTypeOfUriParser.IsEquivalentTo(edmTypeReference));
			if (uriLiteralParserPerEdmType == null)
			{
				return null;
			}
			return uriLiteralParserPerEdmType.UriLiteralParser;
		}

		// Token: 0x040008B7 RID: 2231
		private static readonly object Locker = new object();

		// Token: 0x040008B8 RID: 2232
		private static IUriLiteralParser[] customUriLiteralParsers = new IUriLiteralParser[0];

		// Token: 0x040008B9 RID: 2233
		private static CustomUriLiteralParsers.UriLiteralParserPerEdmType[] customUriLiteralParserPerEdmType = new CustomUriLiteralParsers.UriLiteralParserPerEdmType[0];

		// Token: 0x040008BA RID: 2234
		private static CustomUriLiteralParsers singleInstance;

		// Token: 0x020002E8 RID: 744
		private sealed class UriLiteralParserPerEdmType
		{
			// Token: 0x17000584 RID: 1412
			// (get) Token: 0x0600197B RID: 6523 RVA: 0x00049F06 File Offset: 0x00048106
			// (set) Token: 0x0600197C RID: 6524 RVA: 0x00049F0E File Offset: 0x0004810E
			internal IEdmTypeReference EdmTypeOfUriParser { get; set; }

			// Token: 0x17000585 RID: 1413
			// (get) Token: 0x0600197D RID: 6525 RVA: 0x00049F17 File Offset: 0x00048117
			// (set) Token: 0x0600197E RID: 6526 RVA: 0x00049F1F File Offset: 0x0004811F
			internal IUriLiteralParser UriLiteralParser { get; set; }
		}
	}
}
