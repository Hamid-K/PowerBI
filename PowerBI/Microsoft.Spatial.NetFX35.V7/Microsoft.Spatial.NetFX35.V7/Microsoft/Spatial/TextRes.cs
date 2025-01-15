using System;
using System.Globalization;
using System.Resources;
using System.Threading;

namespace Microsoft.Spatial
{
	// Token: 0x0200006F RID: 111
	internal sealed class TextRes
	{
		// Token: 0x06000294 RID: 660 RVA: 0x00006911 File Offset: 0x00004B11
		internal TextRes()
		{
			this.resources = new ResourceManager("Microsoft.Spatial", base.GetType().Assembly);
		}

		// Token: 0x06000295 RID: 661 RVA: 0x00006934 File Offset: 0x00004B34
		private static TextRes GetLoader()
		{
			if (TextRes.loader == null)
			{
				TextRes textRes = new TextRes();
				Interlocked.CompareExchange<TextRes>(ref TextRes.loader, textRes, null);
			}
			return TextRes.loader;
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000296 RID: 662 RVA: 0x00006960 File Offset: 0x00004B60
		private static CultureInfo Culture
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000297 RID: 663 RVA: 0x00006963 File Offset: 0x00004B63
		public static ResourceManager Resources
		{
			get
			{
				return TextRes.GetLoader().resources;
			}
		}

		// Token: 0x06000298 RID: 664 RVA: 0x00006970 File Offset: 0x00004B70
		public static string GetString(string name, params object[] args)
		{
			TextRes textRes = TextRes.GetLoader();
			if (textRes == null)
			{
				return null;
			}
			string @string = textRes.resources.GetString(name, TextRes.Culture);
			if (args != null && args.Length != 0)
			{
				for (int i = 0; i < args.Length; i++)
				{
					string text = args[i] as string;
					if (text != null && text.Length > 1024)
					{
						args[i] = text.Substring(0, 1021) + "...";
					}
				}
				return string.Format(CultureInfo.CurrentCulture, @string, args);
			}
			return @string;
		}

		// Token: 0x06000299 RID: 665 RVA: 0x000069F0 File Offset: 0x00004BF0
		public static string GetString(string name)
		{
			TextRes textRes = TextRes.GetLoader();
			if (textRes == null)
			{
				return null;
			}
			return textRes.resources.GetString(name, TextRes.Culture);
		}

		// Token: 0x0600029A RID: 666 RVA: 0x00006A19 File Offset: 0x00004C19
		public static string GetString(string name, out bool usedFallback)
		{
			usedFallback = false;
			return TextRes.GetString(name);
		}

		// Token: 0x0600029B RID: 667 RVA: 0x00006A24 File Offset: 0x00004C24
		public static object GetObject(string name)
		{
			TextRes textRes = TextRes.GetLoader();
			if (textRes == null)
			{
				return null;
			}
			return textRes.resources.GetObject(name, TextRes.Culture);
		}

		// Token: 0x040000D3 RID: 211
		internal const string SpatialImplementation_NoRegisteredOperations = "SpatialImplementation_NoRegisteredOperations";

		// Token: 0x040000D4 RID: 212
		internal const string InvalidPointCoordinate = "InvalidPointCoordinate";

		// Token: 0x040000D5 RID: 213
		internal const string Point_AccessCoordinateWhenEmpty = "Point_AccessCoordinateWhenEmpty";

		// Token: 0x040000D6 RID: 214
		internal const string SpatialBuilder_CannotCreateBeforeDrawn = "SpatialBuilder_CannotCreateBeforeDrawn";

		// Token: 0x040000D7 RID: 215
		internal const string GmlReader_UnexpectedElement = "GmlReader_UnexpectedElement";

		// Token: 0x040000D8 RID: 216
		internal const string GmlReader_ExpectReaderAtElement = "GmlReader_ExpectReaderAtElement";

		// Token: 0x040000D9 RID: 217
		internal const string GmlReader_InvalidSpatialType = "GmlReader_InvalidSpatialType";

		// Token: 0x040000DA RID: 218
		internal const string GmlReader_EmptyRingsNotAllowed = "GmlReader_EmptyRingsNotAllowed";

		// Token: 0x040000DB RID: 219
		internal const string GmlReader_PosNeedTwoNumbers = "GmlReader_PosNeedTwoNumbers";

		// Token: 0x040000DC RID: 220
		internal const string GmlReader_PosListNeedsEvenCount = "GmlReader_PosListNeedsEvenCount";

		// Token: 0x040000DD RID: 221
		internal const string GmlReader_InvalidSrsName = "GmlReader_InvalidSrsName";

		// Token: 0x040000DE RID: 222
		internal const string GmlReader_InvalidAttribute = "GmlReader_InvalidAttribute";

		// Token: 0x040000DF RID: 223
		internal const string WellKnownText_UnexpectedToken = "WellKnownText_UnexpectedToken";

		// Token: 0x040000E0 RID: 224
		internal const string WellKnownText_UnexpectedCharacter = "WellKnownText_UnexpectedCharacter";

		// Token: 0x040000E1 RID: 225
		internal const string WellKnownText_UnknownTaggedText = "WellKnownText_UnknownTaggedText";

		// Token: 0x040000E2 RID: 226
		internal const string WellKnownText_TooManyDimensions = "WellKnownText_TooManyDimensions";

		// Token: 0x040000E3 RID: 227
		internal const string Validator_SridMismatch = "Validator_SridMismatch";

		// Token: 0x040000E4 RID: 228
		internal const string Validator_InvalidType = "Validator_InvalidType";

		// Token: 0x040000E5 RID: 229
		internal const string Validator_FullGlobeInCollection = "Validator_FullGlobeInCollection";

		// Token: 0x040000E6 RID: 230
		internal const string Validator_LineStringNeedsTwoPoints = "Validator_LineStringNeedsTwoPoints";

		// Token: 0x040000E7 RID: 231
		internal const string Validator_FullGlobeCannotHaveElements = "Validator_FullGlobeCannotHaveElements";

		// Token: 0x040000E8 RID: 232
		internal const string Validator_NestingOverflow = "Validator_NestingOverflow";

		// Token: 0x040000E9 RID: 233
		internal const string Validator_InvalidPointCoordinate = "Validator_InvalidPointCoordinate";

		// Token: 0x040000EA RID: 234
		internal const string Validator_UnexpectedCall = "Validator_UnexpectedCall";

		// Token: 0x040000EB RID: 235
		internal const string Validator_UnexpectedCall2 = "Validator_UnexpectedCall2";

		// Token: 0x040000EC RID: 236
		internal const string Validator_InvalidPolygonPoints = "Validator_InvalidPolygonPoints";

		// Token: 0x040000ED RID: 237
		internal const string Validator_InvalidLatitudeCoordinate = "Validator_InvalidLatitudeCoordinate";

		// Token: 0x040000EE RID: 238
		internal const string Validator_InvalidLongitudeCoordinate = "Validator_InvalidLongitudeCoordinate";

		// Token: 0x040000EF RID: 239
		internal const string Validator_UnexpectedGeography = "Validator_UnexpectedGeography";

		// Token: 0x040000F0 RID: 240
		internal const string Validator_UnexpectedGeometry = "Validator_UnexpectedGeometry";

		// Token: 0x040000F1 RID: 241
		internal const string GeoJsonReader_MissingRequiredMember = "GeoJsonReader_MissingRequiredMember";

		// Token: 0x040000F2 RID: 242
		internal const string GeoJsonReader_InvalidPosition = "GeoJsonReader_InvalidPosition";

		// Token: 0x040000F3 RID: 243
		internal const string GeoJsonReader_InvalidTypeName = "GeoJsonReader_InvalidTypeName";

		// Token: 0x040000F4 RID: 244
		internal const string GeoJsonReader_InvalidNullElement = "GeoJsonReader_InvalidNullElement";

		// Token: 0x040000F5 RID: 245
		internal const string GeoJsonReader_ExpectedNumeric = "GeoJsonReader_ExpectedNumeric";

		// Token: 0x040000F6 RID: 246
		internal const string GeoJsonReader_ExpectedArray = "GeoJsonReader_ExpectedArray";

		// Token: 0x040000F7 RID: 247
		internal const string GeoJsonReader_InvalidCrsType = "GeoJsonReader_InvalidCrsType";

		// Token: 0x040000F8 RID: 248
		internal const string GeoJsonReader_InvalidCrsName = "GeoJsonReader_InvalidCrsName";

		// Token: 0x040000F9 RID: 249
		internal const string JsonReaderExtensions_CannotReadPropertyValueAsString = "JsonReaderExtensions_CannotReadPropertyValueAsString";

		// Token: 0x040000FA RID: 250
		internal const string JsonReaderExtensions_CannotReadValueAsJsonObject = "JsonReaderExtensions_CannotReadValueAsJsonObject";

		// Token: 0x040000FB RID: 251
		internal const string PlatformHelper_DateTimeOffsetMustContainTimeZone = "PlatformHelper_DateTimeOffsetMustContainTimeZone";

		// Token: 0x040000FC RID: 252
		private static TextRes loader;

		// Token: 0x040000FD RID: 253
		private ResourceManager resources;
	}
}
