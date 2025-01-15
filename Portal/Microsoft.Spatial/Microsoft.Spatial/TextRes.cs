using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Threading;

namespace Microsoft.Spatial
{
	// Token: 0x02000072 RID: 114
	internal sealed class TextRes
	{
		// Token: 0x06000306 RID: 774 RVA: 0x00007547 File Offset: 0x00005747
		internal TextRes()
		{
			this.resources = new ResourceManager("Microsoft.Spatial", base.GetType().GetTypeInfo().Assembly);
		}

		// Token: 0x06000307 RID: 775 RVA: 0x00007570 File Offset: 0x00005770
		private static TextRes GetLoader()
		{
			if (TextRes.loader == null)
			{
				TextRes textRes = new TextRes();
				Interlocked.CompareExchange<TextRes>(ref TextRes.loader, textRes, null);
			}
			return TextRes.loader;
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000308 RID: 776 RVA: 0x0000759C File Offset: 0x0000579C
		private static CultureInfo Culture
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000309 RID: 777 RVA: 0x0000759F File Offset: 0x0000579F
		public static ResourceManager Resources
		{
			get
			{
				return TextRes.GetLoader().resources;
			}
		}

		// Token: 0x0600030A RID: 778 RVA: 0x000075AC File Offset: 0x000057AC
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

		// Token: 0x0600030B RID: 779 RVA: 0x0000762C File Offset: 0x0000582C
		public static string GetString(string name)
		{
			TextRes textRes = TextRes.GetLoader();
			if (textRes == null)
			{
				return null;
			}
			return textRes.resources.GetString(name, TextRes.Culture);
		}

		// Token: 0x0600030C RID: 780 RVA: 0x00007655 File Offset: 0x00005855
		public static string GetString(string name, out bool usedFallback)
		{
			usedFallback = false;
			return TextRes.GetString(name);
		}

		// Token: 0x040000DC RID: 220
		internal const string SpatialImplementation_NoRegisteredOperations = "SpatialImplementation_NoRegisteredOperations";

		// Token: 0x040000DD RID: 221
		internal const string InvalidPointCoordinate = "InvalidPointCoordinate";

		// Token: 0x040000DE RID: 222
		internal const string Point_AccessCoordinateWhenEmpty = "Point_AccessCoordinateWhenEmpty";

		// Token: 0x040000DF RID: 223
		internal const string SpatialBuilder_CannotCreateBeforeDrawn = "SpatialBuilder_CannotCreateBeforeDrawn";

		// Token: 0x040000E0 RID: 224
		internal const string GmlReader_UnexpectedElement = "GmlReader_UnexpectedElement";

		// Token: 0x040000E1 RID: 225
		internal const string GmlReader_ExpectReaderAtElement = "GmlReader_ExpectReaderAtElement";

		// Token: 0x040000E2 RID: 226
		internal const string GmlReader_InvalidSpatialType = "GmlReader_InvalidSpatialType";

		// Token: 0x040000E3 RID: 227
		internal const string GmlReader_EmptyRingsNotAllowed = "GmlReader_EmptyRingsNotAllowed";

		// Token: 0x040000E4 RID: 228
		internal const string GmlReader_PosNeedTwoNumbers = "GmlReader_PosNeedTwoNumbers";

		// Token: 0x040000E5 RID: 229
		internal const string GmlReader_PosListNeedsEvenCount = "GmlReader_PosListNeedsEvenCount";

		// Token: 0x040000E6 RID: 230
		internal const string GmlReader_InvalidSrsName = "GmlReader_InvalidSrsName";

		// Token: 0x040000E7 RID: 231
		internal const string GmlReader_InvalidAttribute = "GmlReader_InvalidAttribute";

		// Token: 0x040000E8 RID: 232
		internal const string WellKnownText_UnexpectedToken = "WellKnownText_UnexpectedToken";

		// Token: 0x040000E9 RID: 233
		internal const string WellKnownText_UnexpectedCharacter = "WellKnownText_UnexpectedCharacter";

		// Token: 0x040000EA RID: 234
		internal const string WellKnownText_UnknownTaggedText = "WellKnownText_UnknownTaggedText";

		// Token: 0x040000EB RID: 235
		internal const string WellKnownText_TooManyDimensions = "WellKnownText_TooManyDimensions";

		// Token: 0x040000EC RID: 236
		internal const string Validator_SridMismatch = "Validator_SridMismatch";

		// Token: 0x040000ED RID: 237
		internal const string Validator_InvalidType = "Validator_InvalidType";

		// Token: 0x040000EE RID: 238
		internal const string Validator_FullGlobeInCollection = "Validator_FullGlobeInCollection";

		// Token: 0x040000EF RID: 239
		internal const string Validator_LineStringNeedsTwoPoints = "Validator_LineStringNeedsTwoPoints";

		// Token: 0x040000F0 RID: 240
		internal const string Validator_FullGlobeCannotHaveElements = "Validator_FullGlobeCannotHaveElements";

		// Token: 0x040000F1 RID: 241
		internal const string Validator_NestingOverflow = "Validator_NestingOverflow";

		// Token: 0x040000F2 RID: 242
		internal const string Validator_InvalidPointCoordinate = "Validator_InvalidPointCoordinate";

		// Token: 0x040000F3 RID: 243
		internal const string Validator_UnexpectedCall = "Validator_UnexpectedCall";

		// Token: 0x040000F4 RID: 244
		internal const string Validator_UnexpectedCall2 = "Validator_UnexpectedCall2";

		// Token: 0x040000F5 RID: 245
		internal const string Validator_InvalidPolygonPoints = "Validator_InvalidPolygonPoints";

		// Token: 0x040000F6 RID: 246
		internal const string Validator_InvalidLatitudeCoordinate = "Validator_InvalidLatitudeCoordinate";

		// Token: 0x040000F7 RID: 247
		internal const string Validator_InvalidLongitudeCoordinate = "Validator_InvalidLongitudeCoordinate";

		// Token: 0x040000F8 RID: 248
		internal const string Validator_UnexpectedGeography = "Validator_UnexpectedGeography";

		// Token: 0x040000F9 RID: 249
		internal const string Validator_UnexpectedGeometry = "Validator_UnexpectedGeometry";

		// Token: 0x040000FA RID: 250
		internal const string GeoJsonReader_MissingRequiredMember = "GeoJsonReader_MissingRequiredMember";

		// Token: 0x040000FB RID: 251
		internal const string GeoJsonReader_InvalidPosition = "GeoJsonReader_InvalidPosition";

		// Token: 0x040000FC RID: 252
		internal const string GeoJsonReader_InvalidTypeName = "GeoJsonReader_InvalidTypeName";

		// Token: 0x040000FD RID: 253
		internal const string GeoJsonReader_InvalidNullElement = "GeoJsonReader_InvalidNullElement";

		// Token: 0x040000FE RID: 254
		internal const string GeoJsonReader_ExpectedNumeric = "GeoJsonReader_ExpectedNumeric";

		// Token: 0x040000FF RID: 255
		internal const string GeoJsonReader_ExpectedArray = "GeoJsonReader_ExpectedArray";

		// Token: 0x04000100 RID: 256
		internal const string GeoJsonReader_InvalidCrsType = "GeoJsonReader_InvalidCrsType";

		// Token: 0x04000101 RID: 257
		internal const string GeoJsonReader_InvalidCrsName = "GeoJsonReader_InvalidCrsName";

		// Token: 0x04000102 RID: 258
		internal const string JsonReaderExtensions_CannotReadPropertyValueAsString = "JsonReaderExtensions_CannotReadPropertyValueAsString";

		// Token: 0x04000103 RID: 259
		internal const string JsonReaderExtensions_CannotReadValueAsJsonObject = "JsonReaderExtensions_CannotReadValueAsJsonObject";

		// Token: 0x04000104 RID: 260
		internal const string PlatformHelper_DateTimeOffsetMustContainTimeZone = "PlatformHelper_DateTimeOffsetMustContainTimeZone";

		// Token: 0x04000105 RID: 261
		private static TextRes loader;

		// Token: 0x04000106 RID: 262
		private ResourceManager resources;
	}
}
