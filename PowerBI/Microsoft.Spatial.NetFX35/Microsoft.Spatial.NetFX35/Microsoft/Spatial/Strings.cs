using System;

namespace Microsoft.Spatial
{
	// Token: 0x0200008F RID: 143
	internal static class Strings
	{
		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000388 RID: 904 RVA: 0x00009B33 File Offset: 0x00007D33
		internal static string SpatialImplementation_NoRegisteredOperations
		{
			get
			{
				return TextRes.GetString("SpatialImplementation_NoRegisteredOperations");
			}
		}

		// Token: 0x06000389 RID: 905 RVA: 0x00009B40 File Offset: 0x00007D40
		internal static string InvalidPointCoordinate(object p0, object p1)
		{
			return TextRes.GetString("InvalidPointCoordinate", new object[] { p0, p1 });
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600038A RID: 906 RVA: 0x00009B67 File Offset: 0x00007D67
		internal static string Point_AccessCoordinateWhenEmpty
		{
			get
			{
				return TextRes.GetString("Point_AccessCoordinateWhenEmpty");
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600038B RID: 907 RVA: 0x00009B73 File Offset: 0x00007D73
		internal static string SpatialBuilder_CannotCreateBeforeDrawn
		{
			get
			{
				return TextRes.GetString("SpatialBuilder_CannotCreateBeforeDrawn");
			}
		}

		// Token: 0x0600038C RID: 908 RVA: 0x00009B80 File Offset: 0x00007D80
		internal static string GmlReader_UnexpectedElement(object p0)
		{
			return TextRes.GetString("GmlReader_UnexpectedElement", new object[] { p0 });
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600038D RID: 909 RVA: 0x00009BA3 File Offset: 0x00007DA3
		internal static string GmlReader_ExpectReaderAtElement
		{
			get
			{
				return TextRes.GetString("GmlReader_ExpectReaderAtElement");
			}
		}

		// Token: 0x0600038E RID: 910 RVA: 0x00009BB0 File Offset: 0x00007DB0
		internal static string GmlReader_InvalidSpatialType(object p0)
		{
			return TextRes.GetString("GmlReader_InvalidSpatialType", new object[] { p0 });
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600038F RID: 911 RVA: 0x00009BD3 File Offset: 0x00007DD3
		internal static string GmlReader_EmptyRingsNotAllowed
		{
			get
			{
				return TextRes.GetString("GmlReader_EmptyRingsNotAllowed");
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000390 RID: 912 RVA: 0x00009BDF File Offset: 0x00007DDF
		internal static string GmlReader_PosNeedTwoNumbers
		{
			get
			{
				return TextRes.GetString("GmlReader_PosNeedTwoNumbers");
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000391 RID: 913 RVA: 0x00009BEB File Offset: 0x00007DEB
		internal static string GmlReader_PosListNeedsEvenCount
		{
			get
			{
				return TextRes.GetString("GmlReader_PosListNeedsEvenCount");
			}
		}

		// Token: 0x06000392 RID: 914 RVA: 0x00009BF8 File Offset: 0x00007DF8
		internal static string GmlReader_InvalidSrsName(object p0)
		{
			return TextRes.GetString("GmlReader_InvalidSrsName", new object[] { p0 });
		}

		// Token: 0x06000393 RID: 915 RVA: 0x00009C1C File Offset: 0x00007E1C
		internal static string GmlReader_InvalidAttribute(object p0, object p1)
		{
			return TextRes.GetString("GmlReader_InvalidAttribute", new object[] { p0, p1 });
		}

		// Token: 0x06000394 RID: 916 RVA: 0x00009C44 File Offset: 0x00007E44
		internal static string WellKnownText_UnexpectedToken(object p0, object p1, object p2)
		{
			return TextRes.GetString("WellKnownText_UnexpectedToken", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000395 RID: 917 RVA: 0x00009C70 File Offset: 0x00007E70
		internal static string WellKnownText_UnexpectedCharacter(object p0)
		{
			return TextRes.GetString("WellKnownText_UnexpectedCharacter", new object[] { p0 });
		}

		// Token: 0x06000396 RID: 918 RVA: 0x00009C94 File Offset: 0x00007E94
		internal static string WellKnownText_UnknownTaggedText(object p0)
		{
			return TextRes.GetString("WellKnownText_UnknownTaggedText", new object[] { p0 });
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000397 RID: 919 RVA: 0x00009CB7 File Offset: 0x00007EB7
		internal static string WellKnownText_TooManyDimensions
		{
			get
			{
				return TextRes.GetString("WellKnownText_TooManyDimensions");
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000398 RID: 920 RVA: 0x00009CC3 File Offset: 0x00007EC3
		internal static string Validator_SridMismatch
		{
			get
			{
				return TextRes.GetString("Validator_SridMismatch");
			}
		}

		// Token: 0x06000399 RID: 921 RVA: 0x00009CD0 File Offset: 0x00007ED0
		internal static string Validator_InvalidType(object p0)
		{
			return TextRes.GetString("Validator_InvalidType", new object[] { p0 });
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600039A RID: 922 RVA: 0x00009CF3 File Offset: 0x00007EF3
		internal static string Validator_FullGlobeInCollection
		{
			get
			{
				return TextRes.GetString("Validator_FullGlobeInCollection");
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600039B RID: 923 RVA: 0x00009CFF File Offset: 0x00007EFF
		internal static string Validator_LineStringNeedsTwoPoints
		{
			get
			{
				return TextRes.GetString("Validator_LineStringNeedsTwoPoints");
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600039C RID: 924 RVA: 0x00009D0B File Offset: 0x00007F0B
		internal static string Validator_FullGlobeCannotHaveElements
		{
			get
			{
				return TextRes.GetString("Validator_FullGlobeCannotHaveElements");
			}
		}

		// Token: 0x0600039D RID: 925 RVA: 0x00009D18 File Offset: 0x00007F18
		internal static string Validator_NestingOverflow(object p0)
		{
			return TextRes.GetString("Validator_NestingOverflow", new object[] { p0 });
		}

		// Token: 0x0600039E RID: 926 RVA: 0x00009D3C File Offset: 0x00007F3C
		internal static string Validator_InvalidPointCoordinate(object p0, object p1, object p2, object p3)
		{
			return TextRes.GetString("Validator_InvalidPointCoordinate", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x0600039F RID: 927 RVA: 0x00009D6C File Offset: 0x00007F6C
		internal static string Validator_UnexpectedCall(object p0, object p1)
		{
			return TextRes.GetString("Validator_UnexpectedCall", new object[] { p0, p1 });
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x00009D94 File Offset: 0x00007F94
		internal static string Validator_UnexpectedCall2(object p0, object p1, object p2)
		{
			return TextRes.GetString("Validator_UnexpectedCall2", new object[] { p0, p1, p2 });
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x00009DBF File Offset: 0x00007FBF
		internal static string Validator_InvalidPolygonPoints
		{
			get
			{
				return TextRes.GetString("Validator_InvalidPolygonPoints");
			}
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x00009DCC File Offset: 0x00007FCC
		internal static string Validator_InvalidLatitudeCoordinate(object p0)
		{
			return TextRes.GetString("Validator_InvalidLatitudeCoordinate", new object[] { p0 });
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x00009DF0 File Offset: 0x00007FF0
		internal static string Validator_InvalidLongitudeCoordinate(object p0)
		{
			return TextRes.GetString("Validator_InvalidLongitudeCoordinate", new object[] { p0 });
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060003A4 RID: 932 RVA: 0x00009E13 File Offset: 0x00008013
		internal static string Validator_UnexpectedGeography
		{
			get
			{
				return TextRes.GetString("Validator_UnexpectedGeography");
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x00009E1F File Offset: 0x0000801F
		internal static string Validator_UnexpectedGeometry
		{
			get
			{
				return TextRes.GetString("Validator_UnexpectedGeometry");
			}
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x00009E2C File Offset: 0x0000802C
		internal static string GeoJsonReader_MissingRequiredMember(object p0)
		{
			return TextRes.GetString("GeoJsonReader_MissingRequiredMember", new object[] { p0 });
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060003A7 RID: 935 RVA: 0x00009E4F File Offset: 0x0000804F
		internal static string GeoJsonReader_InvalidPosition
		{
			get
			{
				return TextRes.GetString("GeoJsonReader_InvalidPosition");
			}
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x00009E5C File Offset: 0x0000805C
		internal static string GeoJsonReader_InvalidTypeName(object p0)
		{
			return TextRes.GetString("GeoJsonReader_InvalidTypeName", new object[] { p0 });
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060003A9 RID: 937 RVA: 0x00009E7F File Offset: 0x0000807F
		internal static string GeoJsonReader_InvalidNullElement
		{
			get
			{
				return TextRes.GetString("GeoJsonReader_InvalidNullElement");
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060003AA RID: 938 RVA: 0x00009E8B File Offset: 0x0000808B
		internal static string GeoJsonReader_ExpectedNumeric
		{
			get
			{
				return TextRes.GetString("GeoJsonReader_ExpectedNumeric");
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060003AB RID: 939 RVA: 0x00009E97 File Offset: 0x00008097
		internal static string GeoJsonReader_ExpectedArray
		{
			get
			{
				return TextRes.GetString("GeoJsonReader_ExpectedArray");
			}
		}

		// Token: 0x060003AC RID: 940 RVA: 0x00009EA4 File Offset: 0x000080A4
		internal static string GeoJsonReader_InvalidCrsType(object p0)
		{
			return TextRes.GetString("GeoJsonReader_InvalidCrsType", new object[] { p0 });
		}

		// Token: 0x060003AD RID: 941 RVA: 0x00009EC8 File Offset: 0x000080C8
		internal static string GeoJsonReader_InvalidCrsName(object p0)
		{
			return TextRes.GetString("GeoJsonReader_InvalidCrsName", new object[] { p0 });
		}

		// Token: 0x060003AE RID: 942 RVA: 0x00009EEC File Offset: 0x000080EC
		internal static string JsonReaderExtensions_CannotReadPropertyValueAsString(object p0, object p1)
		{
			return TextRes.GetString("JsonReaderExtensions_CannotReadPropertyValueAsString", new object[] { p0, p1 });
		}

		// Token: 0x060003AF RID: 943 RVA: 0x00009F14 File Offset: 0x00008114
		internal static string JsonReaderExtensions_CannotReadValueAsJsonObject(object p0)
		{
			return TextRes.GetString("JsonReaderExtensions_CannotReadValueAsJsonObject", new object[] { p0 });
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x00009F38 File Offset: 0x00008138
		internal static string PlatformHelper_DateTimeOffsetMustContainTimeZone(object p0)
		{
			return TextRes.GetString("PlatformHelper_DateTimeOffsetMustContainTimeZone", new object[] { p0 });
		}
	}
}
