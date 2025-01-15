using System;

namespace Microsoft.Spatial
{
	// Token: 0x02000073 RID: 115
	internal static class Strings
	{
		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600030E RID: 782 RVA: 0x00007660 File Offset: 0x00005860
		internal static string SpatialImplementation_NoRegisteredOperations
		{
			get
			{
				return TextRes.GetString("SpatialImplementation_NoRegisteredOperations");
			}
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000766C File Offset: 0x0000586C
		internal static string InvalidPointCoordinate(object p0, object p1)
		{
			return TextRes.GetString("InvalidPointCoordinate", new object[] { p0, p1 });
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000310 RID: 784 RVA: 0x00007686 File Offset: 0x00005886
		internal static string Point_AccessCoordinateWhenEmpty
		{
			get
			{
				return TextRes.GetString("Point_AccessCoordinateWhenEmpty");
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000311 RID: 785 RVA: 0x00007692 File Offset: 0x00005892
		internal static string SpatialBuilder_CannotCreateBeforeDrawn
		{
			get
			{
				return TextRes.GetString("SpatialBuilder_CannotCreateBeforeDrawn");
			}
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000769E File Offset: 0x0000589E
		internal static string GmlReader_UnexpectedElement(object p0)
		{
			return TextRes.GetString("GmlReader_UnexpectedElement", new object[] { p0 });
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000313 RID: 787 RVA: 0x000076B4 File Offset: 0x000058B4
		internal static string GmlReader_ExpectReaderAtElement
		{
			get
			{
				return TextRes.GetString("GmlReader_ExpectReaderAtElement");
			}
		}

		// Token: 0x06000314 RID: 788 RVA: 0x000076C0 File Offset: 0x000058C0
		internal static string GmlReader_InvalidSpatialType(object p0)
		{
			return TextRes.GetString("GmlReader_InvalidSpatialType", new object[] { p0 });
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000315 RID: 789 RVA: 0x000076D6 File Offset: 0x000058D6
		internal static string GmlReader_EmptyRingsNotAllowed
		{
			get
			{
				return TextRes.GetString("GmlReader_EmptyRingsNotAllowed");
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000316 RID: 790 RVA: 0x000076E2 File Offset: 0x000058E2
		internal static string GmlReader_PosNeedTwoNumbers
		{
			get
			{
				return TextRes.GetString("GmlReader_PosNeedTwoNumbers");
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000317 RID: 791 RVA: 0x000076EE File Offset: 0x000058EE
		internal static string GmlReader_PosListNeedsEvenCount
		{
			get
			{
				return TextRes.GetString("GmlReader_PosListNeedsEvenCount");
			}
		}

		// Token: 0x06000318 RID: 792 RVA: 0x000076FA File Offset: 0x000058FA
		internal static string GmlReader_InvalidSrsName(object p0)
		{
			return TextRes.GetString("GmlReader_InvalidSrsName", new object[] { p0 });
		}

		// Token: 0x06000319 RID: 793 RVA: 0x00007710 File Offset: 0x00005910
		internal static string GmlReader_InvalidAttribute(object p0, object p1)
		{
			return TextRes.GetString("GmlReader_InvalidAttribute", new object[] { p0, p1 });
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000772A File Offset: 0x0000592A
		internal static string WellKnownText_UnexpectedToken(object p0, object p1, object p2)
		{
			return TextRes.GetString("WellKnownText_UnexpectedToken", new object[] { p0, p1, p2 });
		}

		// Token: 0x0600031B RID: 795 RVA: 0x00007748 File Offset: 0x00005948
		internal static string WellKnownText_UnexpectedCharacter(object p0)
		{
			return TextRes.GetString("WellKnownText_UnexpectedCharacter", new object[] { p0 });
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000775E File Offset: 0x0000595E
		internal static string WellKnownText_UnknownTaggedText(object p0)
		{
			return TextRes.GetString("WellKnownText_UnknownTaggedText", new object[] { p0 });
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600031D RID: 797 RVA: 0x00007774 File Offset: 0x00005974
		internal static string WellKnownText_TooManyDimensions
		{
			get
			{
				return TextRes.GetString("WellKnownText_TooManyDimensions");
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600031E RID: 798 RVA: 0x00007780 File Offset: 0x00005980
		internal static string Validator_SridMismatch
		{
			get
			{
				return TextRes.GetString("Validator_SridMismatch");
			}
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000778C File Offset: 0x0000598C
		internal static string Validator_InvalidType(object p0)
		{
			return TextRes.GetString("Validator_InvalidType", new object[] { p0 });
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000320 RID: 800 RVA: 0x000077A2 File Offset: 0x000059A2
		internal static string Validator_FullGlobeInCollection
		{
			get
			{
				return TextRes.GetString("Validator_FullGlobeInCollection");
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000321 RID: 801 RVA: 0x000077AE File Offset: 0x000059AE
		internal static string Validator_LineStringNeedsTwoPoints
		{
			get
			{
				return TextRes.GetString("Validator_LineStringNeedsTwoPoints");
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000322 RID: 802 RVA: 0x000077BA File Offset: 0x000059BA
		internal static string Validator_FullGlobeCannotHaveElements
		{
			get
			{
				return TextRes.GetString("Validator_FullGlobeCannotHaveElements");
			}
		}

		// Token: 0x06000323 RID: 803 RVA: 0x000077C6 File Offset: 0x000059C6
		internal static string Validator_NestingOverflow(object p0)
		{
			return TextRes.GetString("Validator_NestingOverflow", new object[] { p0 });
		}

		// Token: 0x06000324 RID: 804 RVA: 0x000077DC File Offset: 0x000059DC
		internal static string Validator_InvalidPointCoordinate(object p0, object p1, object p2, object p3)
		{
			return TextRes.GetString("Validator_InvalidPointCoordinate", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06000325 RID: 805 RVA: 0x000077FE File Offset: 0x000059FE
		internal static string Validator_UnexpectedCall(object p0, object p1)
		{
			return TextRes.GetString("Validator_UnexpectedCall", new object[] { p0, p1 });
		}

		// Token: 0x06000326 RID: 806 RVA: 0x00007818 File Offset: 0x00005A18
		internal static string Validator_UnexpectedCall2(object p0, object p1, object p2)
		{
			return TextRes.GetString("Validator_UnexpectedCall2", new object[] { p0, p1, p2 });
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000327 RID: 807 RVA: 0x00007836 File Offset: 0x00005A36
		internal static string Validator_InvalidPolygonPoints
		{
			get
			{
				return TextRes.GetString("Validator_InvalidPolygonPoints");
			}
		}

		// Token: 0x06000328 RID: 808 RVA: 0x00007842 File Offset: 0x00005A42
		internal static string Validator_InvalidLatitudeCoordinate(object p0)
		{
			return TextRes.GetString("Validator_InvalidLatitudeCoordinate", new object[] { p0 });
		}

		// Token: 0x06000329 RID: 809 RVA: 0x00007858 File Offset: 0x00005A58
		internal static string Validator_InvalidLongitudeCoordinate(object p0)
		{
			return TextRes.GetString("Validator_InvalidLongitudeCoordinate", new object[] { p0 });
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600032A RID: 810 RVA: 0x0000786E File Offset: 0x00005A6E
		internal static string Validator_UnexpectedGeography
		{
			get
			{
				return TextRes.GetString("Validator_UnexpectedGeography");
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600032B RID: 811 RVA: 0x0000787A File Offset: 0x00005A7A
		internal static string Validator_UnexpectedGeometry
		{
			get
			{
				return TextRes.GetString("Validator_UnexpectedGeometry");
			}
		}

		// Token: 0x0600032C RID: 812 RVA: 0x00007886 File Offset: 0x00005A86
		internal static string GeoJsonReader_MissingRequiredMember(object p0)
		{
			return TextRes.GetString("GeoJsonReader_MissingRequiredMember", new object[] { p0 });
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600032D RID: 813 RVA: 0x0000789C File Offset: 0x00005A9C
		internal static string GeoJsonReader_InvalidPosition
		{
			get
			{
				return TextRes.GetString("GeoJsonReader_InvalidPosition");
			}
		}

		// Token: 0x0600032E RID: 814 RVA: 0x000078A8 File Offset: 0x00005AA8
		internal static string GeoJsonReader_InvalidTypeName(object p0)
		{
			return TextRes.GetString("GeoJsonReader_InvalidTypeName", new object[] { p0 });
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600032F RID: 815 RVA: 0x000078BE File Offset: 0x00005ABE
		internal static string GeoJsonReader_InvalidNullElement
		{
			get
			{
				return TextRes.GetString("GeoJsonReader_InvalidNullElement");
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000330 RID: 816 RVA: 0x000078CA File Offset: 0x00005ACA
		internal static string GeoJsonReader_ExpectedNumeric
		{
			get
			{
				return TextRes.GetString("GeoJsonReader_ExpectedNumeric");
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000331 RID: 817 RVA: 0x000078D6 File Offset: 0x00005AD6
		internal static string GeoJsonReader_ExpectedArray
		{
			get
			{
				return TextRes.GetString("GeoJsonReader_ExpectedArray");
			}
		}

		// Token: 0x06000332 RID: 818 RVA: 0x000078E2 File Offset: 0x00005AE2
		internal static string GeoJsonReader_InvalidCrsType(object p0)
		{
			return TextRes.GetString("GeoJsonReader_InvalidCrsType", new object[] { p0 });
		}

		// Token: 0x06000333 RID: 819 RVA: 0x000078F8 File Offset: 0x00005AF8
		internal static string GeoJsonReader_InvalidCrsName(object p0)
		{
			return TextRes.GetString("GeoJsonReader_InvalidCrsName", new object[] { p0 });
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0000790E File Offset: 0x00005B0E
		internal static string JsonReaderExtensions_CannotReadPropertyValueAsString(object p0, object p1)
		{
			return TextRes.GetString("JsonReaderExtensions_CannotReadPropertyValueAsString", new object[] { p0, p1 });
		}

		// Token: 0x06000335 RID: 821 RVA: 0x00007928 File Offset: 0x00005B28
		internal static string JsonReaderExtensions_CannotReadValueAsJsonObject(object p0)
		{
			return TextRes.GetString("JsonReaderExtensions_CannotReadValueAsJsonObject", new object[] { p0 });
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0000793E File Offset: 0x00005B3E
		internal static string PlatformHelper_DateTimeOffsetMustContainTimeZone(object p0)
		{
			return TextRes.GetString("PlatformHelper_DateTimeOffsetMustContainTimeZone", new object[] { p0 });
		}
	}
}
