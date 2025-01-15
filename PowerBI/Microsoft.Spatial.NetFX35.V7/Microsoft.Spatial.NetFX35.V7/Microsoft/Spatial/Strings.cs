using System;

namespace Microsoft.Spatial
{
	// Token: 0x02000070 RID: 112
	internal static class Strings
	{
		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600029D RID: 669 RVA: 0x00006A4D File Offset: 0x00004C4D
		internal static string SpatialImplementation_NoRegisteredOperations
		{
			get
			{
				return TextRes.GetString("SpatialImplementation_NoRegisteredOperations");
			}
		}

		// Token: 0x0600029E RID: 670 RVA: 0x00006A59 File Offset: 0x00004C59
		internal static string InvalidPointCoordinate(object p0, object p1)
		{
			return TextRes.GetString("InvalidPointCoordinate", new object[] { p0, p1 });
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600029F RID: 671 RVA: 0x00006A73 File Offset: 0x00004C73
		internal static string Point_AccessCoordinateWhenEmpty
		{
			get
			{
				return TextRes.GetString("Point_AccessCoordinateWhenEmpty");
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x00006A7F File Offset: 0x00004C7F
		internal static string SpatialBuilder_CannotCreateBeforeDrawn
		{
			get
			{
				return TextRes.GetString("SpatialBuilder_CannotCreateBeforeDrawn");
			}
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00006A8B File Offset: 0x00004C8B
		internal static string GmlReader_UnexpectedElement(object p0)
		{
			return TextRes.GetString("GmlReader_UnexpectedElement", new object[] { p0 });
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x00006AA1 File Offset: 0x00004CA1
		internal static string GmlReader_ExpectReaderAtElement
		{
			get
			{
				return TextRes.GetString("GmlReader_ExpectReaderAtElement");
			}
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x00006AAD File Offset: 0x00004CAD
		internal static string GmlReader_InvalidSpatialType(object p0)
		{
			return TextRes.GetString("GmlReader_InvalidSpatialType", new object[] { p0 });
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x00006AC3 File Offset: 0x00004CC3
		internal static string GmlReader_EmptyRingsNotAllowed
		{
			get
			{
				return TextRes.GetString("GmlReader_EmptyRingsNotAllowed");
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x00006ACF File Offset: 0x00004CCF
		internal static string GmlReader_PosNeedTwoNumbers
		{
			get
			{
				return TextRes.GetString("GmlReader_PosNeedTwoNumbers");
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x00006ADB File Offset: 0x00004CDB
		internal static string GmlReader_PosListNeedsEvenCount
		{
			get
			{
				return TextRes.GetString("GmlReader_PosListNeedsEvenCount");
			}
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x00006AE7 File Offset: 0x00004CE7
		internal static string GmlReader_InvalidSrsName(object p0)
		{
			return TextRes.GetString("GmlReader_InvalidSrsName", new object[] { p0 });
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x00006AFD File Offset: 0x00004CFD
		internal static string GmlReader_InvalidAttribute(object p0, object p1)
		{
			return TextRes.GetString("GmlReader_InvalidAttribute", new object[] { p0, p1 });
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x00006B17 File Offset: 0x00004D17
		internal static string WellKnownText_UnexpectedToken(object p0, object p1, object p2)
		{
			return TextRes.GetString("WellKnownText_UnexpectedToken", new object[] { p0, p1, p2 });
		}

		// Token: 0x060002AA RID: 682 RVA: 0x00006B35 File Offset: 0x00004D35
		internal static string WellKnownText_UnexpectedCharacter(object p0)
		{
			return TextRes.GetString("WellKnownText_UnexpectedCharacter", new object[] { p0 });
		}

		// Token: 0x060002AB RID: 683 RVA: 0x00006B4B File Offset: 0x00004D4B
		internal static string WellKnownText_UnknownTaggedText(object p0)
		{
			return TextRes.GetString("WellKnownText_UnknownTaggedText", new object[] { p0 });
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060002AC RID: 684 RVA: 0x00006B61 File Offset: 0x00004D61
		internal static string WellKnownText_TooManyDimensions
		{
			get
			{
				return TextRes.GetString("WellKnownText_TooManyDimensions");
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060002AD RID: 685 RVA: 0x00006B6D File Offset: 0x00004D6D
		internal static string Validator_SridMismatch
		{
			get
			{
				return TextRes.GetString("Validator_SridMismatch");
			}
		}

		// Token: 0x060002AE RID: 686 RVA: 0x00006B79 File Offset: 0x00004D79
		internal static string Validator_InvalidType(object p0)
		{
			return TextRes.GetString("Validator_InvalidType", new object[] { p0 });
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060002AF RID: 687 RVA: 0x00006B8F File Offset: 0x00004D8F
		internal static string Validator_FullGlobeInCollection
		{
			get
			{
				return TextRes.GetString("Validator_FullGlobeInCollection");
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x00006B9B File Offset: 0x00004D9B
		internal static string Validator_LineStringNeedsTwoPoints
		{
			get
			{
				return TextRes.GetString("Validator_LineStringNeedsTwoPoints");
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x00006BA7 File Offset: 0x00004DA7
		internal static string Validator_FullGlobeCannotHaveElements
		{
			get
			{
				return TextRes.GetString("Validator_FullGlobeCannotHaveElements");
			}
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x00006BB3 File Offset: 0x00004DB3
		internal static string Validator_NestingOverflow(object p0)
		{
			return TextRes.GetString("Validator_NestingOverflow", new object[] { p0 });
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x00006BC9 File Offset: 0x00004DC9
		internal static string Validator_InvalidPointCoordinate(object p0, object p1, object p2, object p3)
		{
			return TextRes.GetString("Validator_InvalidPointCoordinate", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x00006BEB File Offset: 0x00004DEB
		internal static string Validator_UnexpectedCall(object p0, object p1)
		{
			return TextRes.GetString("Validator_UnexpectedCall", new object[] { p0, p1 });
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x00006C05 File Offset: 0x00004E05
		internal static string Validator_UnexpectedCall2(object p0, object p1, object p2)
		{
			return TextRes.GetString("Validator_UnexpectedCall2", new object[] { p0, p1, p2 });
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x00006C23 File Offset: 0x00004E23
		internal static string Validator_InvalidPolygonPoints
		{
			get
			{
				return TextRes.GetString("Validator_InvalidPolygonPoints");
			}
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x00006C2F File Offset: 0x00004E2F
		internal static string Validator_InvalidLatitudeCoordinate(object p0)
		{
			return TextRes.GetString("Validator_InvalidLatitudeCoordinate", new object[] { p0 });
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x00006C45 File Offset: 0x00004E45
		internal static string Validator_InvalidLongitudeCoordinate(object p0)
		{
			return TextRes.GetString("Validator_InvalidLongitudeCoordinate", new object[] { p0 });
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x00006C5B File Offset: 0x00004E5B
		internal static string Validator_UnexpectedGeography
		{
			get
			{
				return TextRes.GetString("Validator_UnexpectedGeography");
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060002BA RID: 698 RVA: 0x00006C67 File Offset: 0x00004E67
		internal static string Validator_UnexpectedGeometry
		{
			get
			{
				return TextRes.GetString("Validator_UnexpectedGeometry");
			}
		}

		// Token: 0x060002BB RID: 699 RVA: 0x00006C73 File Offset: 0x00004E73
		internal static string GeoJsonReader_MissingRequiredMember(object p0)
		{
			return TextRes.GetString("GeoJsonReader_MissingRequiredMember", new object[] { p0 });
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060002BC RID: 700 RVA: 0x00006C89 File Offset: 0x00004E89
		internal static string GeoJsonReader_InvalidPosition
		{
			get
			{
				return TextRes.GetString("GeoJsonReader_InvalidPosition");
			}
		}

		// Token: 0x060002BD RID: 701 RVA: 0x00006C95 File Offset: 0x00004E95
		internal static string GeoJsonReader_InvalidTypeName(object p0)
		{
			return TextRes.GetString("GeoJsonReader_InvalidTypeName", new object[] { p0 });
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060002BE RID: 702 RVA: 0x00006CAB File Offset: 0x00004EAB
		internal static string GeoJsonReader_InvalidNullElement
		{
			get
			{
				return TextRes.GetString("GeoJsonReader_InvalidNullElement");
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060002BF RID: 703 RVA: 0x00006CB7 File Offset: 0x00004EB7
		internal static string GeoJsonReader_ExpectedNumeric
		{
			get
			{
				return TextRes.GetString("GeoJsonReader_ExpectedNumeric");
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x00006CC3 File Offset: 0x00004EC3
		internal static string GeoJsonReader_ExpectedArray
		{
			get
			{
				return TextRes.GetString("GeoJsonReader_ExpectedArray");
			}
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00006CCF File Offset: 0x00004ECF
		internal static string GeoJsonReader_InvalidCrsType(object p0)
		{
			return TextRes.GetString("GeoJsonReader_InvalidCrsType", new object[] { p0 });
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00006CE5 File Offset: 0x00004EE5
		internal static string GeoJsonReader_InvalidCrsName(object p0)
		{
			return TextRes.GetString("GeoJsonReader_InvalidCrsName", new object[] { p0 });
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00006CFB File Offset: 0x00004EFB
		internal static string JsonReaderExtensions_CannotReadPropertyValueAsString(object p0, object p1)
		{
			return TextRes.GetString("JsonReaderExtensions_CannotReadPropertyValueAsString", new object[] { p0, p1 });
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00006D15 File Offset: 0x00004F15
		internal static string JsonReaderExtensions_CannotReadValueAsJsonObject(object p0)
		{
			return TextRes.GetString("JsonReaderExtensions_CannotReadValueAsJsonObject", new object[] { p0 });
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00006D2B File Offset: 0x00004F2B
		internal static string PlatformHelper_DateTimeOffsetMustContainTimeZone(object p0)
		{
			return TextRes.GetString("PlatformHelper_DateTimeOffsetMustContainTimeZone", new object[] { p0 });
		}
	}
}
