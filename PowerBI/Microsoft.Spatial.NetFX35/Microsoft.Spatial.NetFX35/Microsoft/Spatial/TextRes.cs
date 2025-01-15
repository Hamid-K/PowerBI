using System;
using System.Globalization;
using System.Resources;
using System.Threading;

namespace Microsoft.Spatial
{
	// Token: 0x0200008E RID: 142
	internal sealed class TextRes
	{
		// Token: 0x0600037F RID: 895 RVA: 0x000099F1 File Offset: 0x00007BF1
		internal TextRes()
		{
			this.resources = new ResourceManager("Microsoft.Spatial", base.GetType().Assembly);
		}

		// Token: 0x06000380 RID: 896 RVA: 0x00009A14 File Offset: 0x00007C14
		private static TextRes GetLoader()
		{
			if (TextRes.loader == null)
			{
				TextRes textRes = new TextRes();
				Interlocked.CompareExchange<TextRes>(ref TextRes.loader, textRes, null);
			}
			return TextRes.loader;
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000381 RID: 897 RVA: 0x00009A40 File Offset: 0x00007C40
		private static CultureInfo Culture
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000382 RID: 898 RVA: 0x00009A43 File Offset: 0x00007C43
		public static ResourceManager Resources
		{
			get
			{
				return TextRes.GetLoader().resources;
			}
		}

		// Token: 0x06000383 RID: 899 RVA: 0x00009A50 File Offset: 0x00007C50
		public static string GetString(string name, params object[] args)
		{
			TextRes textRes = TextRes.GetLoader();
			if (textRes == null)
			{
				return null;
			}
			string @string = textRes.resources.GetString(name, TextRes.Culture);
			if (args != null && args.Length > 0)
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

		// Token: 0x06000384 RID: 900 RVA: 0x00009AD4 File Offset: 0x00007CD4
		public static string GetString(string name)
		{
			TextRes textRes = TextRes.GetLoader();
			if (textRes == null)
			{
				return null;
			}
			return textRes.resources.GetString(name, TextRes.Culture);
		}

		// Token: 0x06000385 RID: 901 RVA: 0x00009AFD File Offset: 0x00007CFD
		public static string GetString(string name, out bool usedFallback)
		{
			usedFallback = false;
			return TextRes.GetString(name);
		}

		// Token: 0x06000386 RID: 902 RVA: 0x00009B08 File Offset: 0x00007D08
		public static object GetObject(string name)
		{
			TextRes textRes = TextRes.GetLoader();
			if (textRes == null)
			{
				return null;
			}
			return textRes.resources.GetObject(name, TextRes.Culture);
		}

		// Token: 0x04000117 RID: 279
		internal const string SpatialImplementation_NoRegisteredOperations = "SpatialImplementation_NoRegisteredOperations";

		// Token: 0x04000118 RID: 280
		internal const string InvalidPointCoordinate = "InvalidPointCoordinate";

		// Token: 0x04000119 RID: 281
		internal const string Point_AccessCoordinateWhenEmpty = "Point_AccessCoordinateWhenEmpty";

		// Token: 0x0400011A RID: 282
		internal const string SpatialBuilder_CannotCreateBeforeDrawn = "SpatialBuilder_CannotCreateBeforeDrawn";

		// Token: 0x0400011B RID: 283
		internal const string GmlReader_UnexpectedElement = "GmlReader_UnexpectedElement";

		// Token: 0x0400011C RID: 284
		internal const string GmlReader_ExpectReaderAtElement = "GmlReader_ExpectReaderAtElement";

		// Token: 0x0400011D RID: 285
		internal const string GmlReader_InvalidSpatialType = "GmlReader_InvalidSpatialType";

		// Token: 0x0400011E RID: 286
		internal const string GmlReader_EmptyRingsNotAllowed = "GmlReader_EmptyRingsNotAllowed";

		// Token: 0x0400011F RID: 287
		internal const string GmlReader_PosNeedTwoNumbers = "GmlReader_PosNeedTwoNumbers";

		// Token: 0x04000120 RID: 288
		internal const string GmlReader_PosListNeedsEvenCount = "GmlReader_PosListNeedsEvenCount";

		// Token: 0x04000121 RID: 289
		internal const string GmlReader_InvalidSrsName = "GmlReader_InvalidSrsName";

		// Token: 0x04000122 RID: 290
		internal const string GmlReader_InvalidAttribute = "GmlReader_InvalidAttribute";

		// Token: 0x04000123 RID: 291
		internal const string WellKnownText_UnexpectedToken = "WellKnownText_UnexpectedToken";

		// Token: 0x04000124 RID: 292
		internal const string WellKnownText_UnexpectedCharacter = "WellKnownText_UnexpectedCharacter";

		// Token: 0x04000125 RID: 293
		internal const string WellKnownText_UnknownTaggedText = "WellKnownText_UnknownTaggedText";

		// Token: 0x04000126 RID: 294
		internal const string WellKnownText_TooManyDimensions = "WellKnownText_TooManyDimensions";

		// Token: 0x04000127 RID: 295
		internal const string Validator_SridMismatch = "Validator_SridMismatch";

		// Token: 0x04000128 RID: 296
		internal const string Validator_InvalidType = "Validator_InvalidType";

		// Token: 0x04000129 RID: 297
		internal const string Validator_FullGlobeInCollection = "Validator_FullGlobeInCollection";

		// Token: 0x0400012A RID: 298
		internal const string Validator_LineStringNeedsTwoPoints = "Validator_LineStringNeedsTwoPoints";

		// Token: 0x0400012B RID: 299
		internal const string Validator_FullGlobeCannotHaveElements = "Validator_FullGlobeCannotHaveElements";

		// Token: 0x0400012C RID: 300
		internal const string Validator_NestingOverflow = "Validator_NestingOverflow";

		// Token: 0x0400012D RID: 301
		internal const string Validator_InvalidPointCoordinate = "Validator_InvalidPointCoordinate";

		// Token: 0x0400012E RID: 302
		internal const string Validator_UnexpectedCall = "Validator_UnexpectedCall";

		// Token: 0x0400012F RID: 303
		internal const string Validator_UnexpectedCall2 = "Validator_UnexpectedCall2";

		// Token: 0x04000130 RID: 304
		internal const string Validator_InvalidPolygonPoints = "Validator_InvalidPolygonPoints";

		// Token: 0x04000131 RID: 305
		internal const string Validator_InvalidLatitudeCoordinate = "Validator_InvalidLatitudeCoordinate";

		// Token: 0x04000132 RID: 306
		internal const string Validator_InvalidLongitudeCoordinate = "Validator_InvalidLongitudeCoordinate";

		// Token: 0x04000133 RID: 307
		internal const string Validator_UnexpectedGeography = "Validator_UnexpectedGeography";

		// Token: 0x04000134 RID: 308
		internal const string Validator_UnexpectedGeometry = "Validator_UnexpectedGeometry";

		// Token: 0x04000135 RID: 309
		internal const string GeoJsonReader_MissingRequiredMember = "GeoJsonReader_MissingRequiredMember";

		// Token: 0x04000136 RID: 310
		internal const string GeoJsonReader_InvalidPosition = "GeoJsonReader_InvalidPosition";

		// Token: 0x04000137 RID: 311
		internal const string GeoJsonReader_InvalidTypeName = "GeoJsonReader_InvalidTypeName";

		// Token: 0x04000138 RID: 312
		internal const string GeoJsonReader_InvalidNullElement = "GeoJsonReader_InvalidNullElement";

		// Token: 0x04000139 RID: 313
		internal const string GeoJsonReader_ExpectedNumeric = "GeoJsonReader_ExpectedNumeric";

		// Token: 0x0400013A RID: 314
		internal const string GeoJsonReader_ExpectedArray = "GeoJsonReader_ExpectedArray";

		// Token: 0x0400013B RID: 315
		internal const string GeoJsonReader_InvalidCrsType = "GeoJsonReader_InvalidCrsType";

		// Token: 0x0400013C RID: 316
		internal const string GeoJsonReader_InvalidCrsName = "GeoJsonReader_InvalidCrsName";

		// Token: 0x0400013D RID: 317
		internal const string JsonReaderExtensions_CannotReadPropertyValueAsString = "JsonReaderExtensions_CannotReadPropertyValueAsString";

		// Token: 0x0400013E RID: 318
		internal const string JsonReaderExtensions_CannotReadValueAsJsonObject = "JsonReaderExtensions_CannotReadValueAsJsonObject";

		// Token: 0x0400013F RID: 319
		internal const string PlatformHelper_DateTimeOffsetMustContainTimeZone = "PlatformHelper_DateTimeOffsetMustContainTimeZone";

		// Token: 0x04000140 RID: 320
		private static TextRes loader;

		// Token: 0x04000141 RID: 321
		private ResourceManager resources;
	}
}
