using System;
using System.Collections.Generic;
using System.Linq;
using DocumentFormat.OpenXml.Validation;

namespace DocumentFormat.OpenXml.Internal.SemanticValidation
{
	// Token: 0x020021B6 RID: 8630
	internal class SemanticConstraintRegistry
	{
		// Token: 0x0600DB65 RID: 56165 RVA: 0x002AEDD8 File Offset: 0x002ACFD8
		public void Initialize()
		{
			this.RegisterConstraint(12160, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new RelationshipTypeConstraint(2, "http://schemas.openxmlformats.org/officeDocument/2006/relationships/control")
			{
				ConstratintId = "2"
			});
			this.RegisterConstraint(11923, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new RelationshipTypeConstraint(2, "http://schemas.openxmlformats.org/officeDocument/2006/relationships/font")
			{
				ConstratintId = "23"
			});
			this.RegisterConstraint(11938, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(1, true, true, this)
			{
				ConstratintId = "28"
			});
			this.RegisterConstraint(11795, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(0, true, true, this)
			{
				ConstratintId = "29"
			});
			this.RegisterConstraint(11794, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(0, true, true, this)
			{
				ConstratintId = "32"
			});
			this.RegisterConstraint(11937, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(1, true, true, this)
			{
				ConstratintId = "33"
			});
			this.RegisterConstraint(11569, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new ReferenceExistConstraint(1, "FootnotesPart", 11937, "DocumentFormat.OpenXml.Wordprocessing.Footnote", 1)
			{
				ConstratintId = "35"
			});
			this.RegisterConstraint(12053, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValuePatternConstraint(0, ".{1}")
			{
				ConstratintId = "99"
			});
			this.RegisterConstraint(12054, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValuePatternConstraint(0, ".{1}")
			{
				ConstratintId = "100"
			});
			this.RegisterConstraint(11684, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new RelationshipTypeConstraint(0, "http://schemas.openxmlformats.org/officeDocument/2006/relationships/aFChunk")
			{
				ConstratintId = "108"
			});
			this.RegisterConstraint(11301, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(16, true, double.NegativeInfinity, true, 2147483647.0, true)
			{
				ConstratintId = "117"
			});
			this.RegisterConstraint(11301, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(18, true, double.NegativeInfinity, true, 1000.0, true)
			{
				ConstratintId = "119"
			});
			this.RegisterConstraint(11301, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(19, true, 1.0, true, 65534.0, true)
			{
				ConstratintId = "133"
			});
			this.RegisterConstraint(11326, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(1, 1, 54)
			{
				ConstratintId = "153"
			});
			this.RegisterConstraint(11325, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(0, 0, 65535)
			{
				ConstratintId = "155"
			});
			this.RegisterConstraint(11325, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(1, 0, 65535)
			{
				ConstratintId = "156"
			});
			this.RegisterConstraint(11325, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(2, 0, 65535)
			{
				ConstratintId = "159"
			});
			this.RegisterConstraint(11325, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(3, 0, 65535)
			{
				ConstratintId = "162"
			});
			this.RegisterConstraint(11331, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(0, true, double.NegativeInfinity, true, 255.0, true)
			{
				ConstratintId = "167"
			});
			this.RegisterConstraint(11302, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValuePatternConstraint(0, "[^'*\\[\\]/\\\\:?]{1}[^*\\[\\]/\\\\:?]*")
			{
				ConstratintId = "172"
			});
			this.RegisterConstraint(11302, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(0, 1, 31)
			{
				ConstratintId = "172"
			});
			this.RegisterConstraint(11302, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(0, false, true, this)
			{
				ConstratintId = "173"
			});
			this.RegisterConstraint(11302, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(1, true, true, this)
			{
				ConstratintId = "174"
			});
			this.RegisterConstraint(11302, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(1, true, 1.0, true, 65534.0, true)
			{
				ConstratintId = "175"
			});
			this.RegisterConstraint(11302, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(3, 0, 255)
			{
				ConstratintId = "176"
			});
			this.RegisterConstraint(11304, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(7, true, double.NegativeInfinity, true, 2147483647.0, true)
			{
				ConstratintId = "210"
			});
			this.RegisterConstraint(11304, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(8, true, double.NegativeInfinity, true, 2147483647.0, true)
			{
				ConstratintId = "211"
			});
			this.RegisterConstraint(11304, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(9, true, double.NegativeInfinity, true, 1000.0, true)
			{
				ConstratintId = "212"
			});
			this.RegisterConstraint(11304, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(11, true, 0.0, true, 32766.0, true)
			{
				ConstratintId = "213"
			});
			this.RegisterConstraint(11385, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(3, true, double.NegativeInfinity, true, 2147483647.0, true)
			{
				ConstratintId = "223"
			});
			this.RegisterConstraint(11385, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new IndexReferenceConstraint(3, "/WorkbookPart/CellMetadataPart", 11235, 11238, "DocumentFormat.OpenXml.Spreadsheet.MetadataBlock", 1)
			{
				ConstratintId = "224"
			});
			this.RegisterConstraint(11385, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(4, true, double.NegativeInfinity, true, 2147483648.0, true)
			{
				ConstratintId = "225"
			});
			this.RegisterConstraint(11385, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new IndexReferenceConstraint(4, "/WorkbookPart/CellMetadataPart", 11236, 11238, "DocumentFormat.OpenXml.Spreadsheet.MetadataBlock", 1)
			{
				ConstratintId = "226"
			});
			this.RegisterConstraint(11385, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(1, true, 0.0, true, 65490.0, true)
			{
				ConstratintId = "227"
			});
			this.RegisterConstraint(11385, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new IndexReferenceConstraint(1, "/WorkbookPart/WorkbookStylesPart", 11362, 11266, "DocumentFormat.OpenXml.Spreadsheet.CellFormat", 0)
			{
				ConstratintId = "228"
			});
			this.RegisterConstraint(11219, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(0, true, 0.0, true, 32768.0, true)
			{
				ConstratintId = "229"
			});
			this.RegisterConstraint(11195, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(0, 0, 255)
			{
				ConstratintId = "230"
			});
			this.RegisterConstraint(11195, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(1, 0, 255)
			{
				ConstratintId = "231"
			});
			this.RegisterConstraint(11207, 11403, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(0, true, false, this)
			{
				ConstratintId = "232"
			});
			this.RegisterConstraint(11202, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeRequiredConditionToValue(7, 0, new string[] { "cells" })
			{
				ConstratintId = "234"
			});
			this.RegisterConstraint(11202, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeRequiredConditionToValue(9, 0, new string[] { "timePeriod" })
			{
				ConstratintId = "235"
			});
			this.RegisterConstraint(11185, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(8, true, 0.0, true, 7.0, true)
			{
				ConstratintId = "236"
			});
			this.RegisterConstraint(11185, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(0, true, 1.0, true, 16384.0, true)
			{
				ConstratintId = "237"
			});
			this.RegisterConstraint(11185, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(1, true, 1.0, true, 16384.0, true)
			{
				ConstratintId = "238"
			});
			this.RegisterConstraint(11185, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(2, true, 0.0, true, 255.0, true)
			{
				ConstratintId = "239"
			});
			this.RegisterConstraint(11185, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(3, true, 0.0, true, 65429.0, true)
			{
				ConstratintId = "240"
			});
			this.RegisterConstraint(11197, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(0, true, double.NegativeInfinity, true, 1023.0, true)
			{
				ConstratintId = "242"
			});
			this.RegisterConstraint(11197, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(1, true, double.NegativeInfinity, true, 1023.0, true)
			{
				ConstratintId = "243"
			});
			this.RegisterConstraint(11399, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(1, 1, int.MaxValue)
			{
				ConstratintId = "250"
			});
			this.RegisterConstraint(11215, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(0, true, 1.0, true, 67098623.0, true)
			{
				ConstratintId = "251"
			});
			this.RegisterConstraint(11209, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(0, false, true, this)
			{
				ConstratintId = "257"
			});
			this.RegisterConstraint(11213, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(1, true, 10.0, true, 400.0, true)
			{
				ConstratintId = "260"
			});
			this.RegisterConstraint(11221, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(2, true, double.NegativeInfinity, true, 64.0, true)
			{
				ConstratintId = "262"
			});
			this.RegisterConstraint(11221, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueSetConstraint(0, false, new string[] { "00000000-0000-0000-0000-000000000000" })
			{
				ConstratintId = "264"
			});
			this.RegisterConstraint(11218, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(12, true, 1.0, true, 32767.0, true)
			{
				ConstratintId = "270"
			});
			this.RegisterConstraint(11218, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(8, 0, 32)
			{
				ConstratintId = "272"
			});
			this.RegisterConstraint(11218, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(10, 0, 32)
			{
				ConstratintId = "273"
			});
			this.RegisterConstraint(11178, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueSetConstraint(11, true, new string[] { "false" })
			{
				ConstratintId = "282"
			});
			this.RegisterConstraint(11178, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeRequiredConditionToValue(10, 0, new string[] { "shared" })
			{
				ConstratintId = "284"
			});
			this.RegisterConstraint(11203, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(2, 0, 2084)
			{
				ConstratintId = "289"
			});
			this.RegisterConstraint(11203, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(4, 0, 2084)
			{
				ConstratintId = "290"
			});
			this.RegisterConstraint(11203, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(3, 0, 255)
			{
				ConstratintId = "291"
			});
			this.RegisterConstraint(11216, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(0, true, double.NegativeInfinity, true, 2147483647.0, true)
			{
				ConstratintId = "294"
			});
			this.RegisterConstraint(11222, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValuePatternConstraint(0, "[^\\d].*")
			{
				ConstratintId = "302"
			});
			this.RegisterConstraint(11222, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(0, 0, 39)
			{
				ConstratintId = "302"
			});
			this.RegisterConstraint(11222, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(5, true, 1.0, true, 67098623.0, true)
			{
				ConstratintId = "304"
			});
			this.RegisterConstraint(11198, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(0, true, 0.0, true, 49.0, false)
			{
				ConstratintId = "307"
			});
			this.RegisterConstraint(11198, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(1, true, 0.0, true, 49.0, false)
			{
				ConstratintId = "308"
			});
			this.RegisterConstraint(11198, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(2, true, 0.0, true, 49.0, false)
			{
				ConstratintId = "309"
			});
			this.RegisterConstraint(11198, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(3, true, 0.0, true, 49.0, false)
			{
				ConstratintId = "310"
			});
			this.RegisterConstraint(11198, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(4, true, 0.0, true, 49.0, false)
			{
				ConstratintId = "311"
			});
			this.RegisterConstraint(11198, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(5, true, 0.0, true, 49.0, false)
			{
				ConstratintId = "312"
			});
			this.RegisterConstraint(11191, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueSetConstraint(6, false, new string[] { "axisValues" })
			{
				ConstratintId = "342"
			});
			this.RegisterConstraint(11206, 11394, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(6, true, false, this)
			{
				ConstratintId = "343"
			});
			this.RegisterConstraint(11206, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Word, new AttributeValueLengthConstraint(6, 1, 255)
			{
				ConstratintId = "345"
			});
			this.RegisterConstraint(11206, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(5, true, 1.0, true, double.PositiveInfinity, true)
			{
				ConstratintId = "346"
			});
			this.RegisterConstraint(11184, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(0, true, 1.0, true, 1048576.0, true)
			{
				ConstratintId = "347"
			});
			this.RegisterConstraint(11184, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(7, true, 0.0, true, 7.0, true)
			{
				ConstratintId = "349"
			});
			this.RegisterConstraint(11184, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(2, true, 0.0, true, 65490.0, true)
			{
				ConstratintId = "350"
			});
			this.RegisterConstraint(11196, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(0, true, double.NegativeInfinity, true, 1022.0, true)
			{
				ConstratintId = "352"
			});
			this.RegisterConstraint(11196, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(1, true, double.NegativeInfinity, true, 1022.0, true)
			{
				ConstratintId = "353"
			});
			this.RegisterConstraint(11205, 11039, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(0, true, false, this)
			{
				ConstratintId = "357"
			});
			this.RegisterConstraint(11389, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(8, true, 0.0, true, 7.0, true)
			{
				ConstratintId = "364"
			});
			this.RegisterConstraint(11389, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(7, true, 0.0, true, 7.0, true)
			{
				ConstratintId = "366"
			});
			this.RegisterConstraint(11389, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(0, true, double.NegativeInfinity, true, 255.0, true)
			{
				ConstratintId = "367"
			});
			this.RegisterConstraint(11389, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(1, true, 0.0, true, 65535.0, true)
			{
				ConstratintId = "368"
			});
			this.RegisterConstraint(11386, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValuePatternConstraint(6, "[\\p{L}\\P{IsBasicLatin}][_\\d\\p{L}\\P{IsBasicLatin}]*")
			{
				ConstratintId = "369"
			});
			this.RegisterConstraint(11370, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValuePatternConstraint(1, "[\\p{L}\\P{IsBasicLatin}][_\\d\\p{L}\\P{IsBasicLatin}]*")
			{
				ConstratintId = "371"
			});
			this.RegisterConstraint(11370, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(1, 0, 32)
			{
				ConstratintId = "371"
			});
			this.RegisterConstraint(11220, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(13, true, double.NegativeInfinity, true, 64.0, true)
			{
				ConstratintId = "373"
			});
			this.RegisterConstraint(11186, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeMutualExclusive(new byte[] { 0, 1, 2, 3 })
			{
				ConstratintId = "377"
			});
			this.RegisterConstraint(11210, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(6, 0, 255)
			{
				ConstratintId = "383"
			});
			this.RegisterConstraint(11210, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(5, 1, 255)
			{
				ConstratintId = "384"
			});
			this.RegisterConstraint(11210, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(1, 1, 255)
			{
				ConstratintId = "385"
			});
			this.RegisterConstraint(11210, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(0, true, 1.0, true, 2147483647.0, true)
			{
				ConstratintId = "387"
			});
			this.RegisterConstraint(11210, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(0, true, true, this)
			{
				ConstratintId = "388"
			});
			this.RegisterConstraint(11210, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeRequiredConditionToValue(3, 2, new string[] { "range" })
			{
				ConstratintId = "390"
			});
			this.RegisterConstraint(11210, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeAbsentConditionToNonValue(3, 2, new string[] { "range" })
			{
				ConstratintId = "390"
			});
			this.RegisterConstraint(11035, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(1, true, double.NegativeInfinity, true, 2147483647.0, true)
			{
				ConstratintId = "410"
			});
			this.RegisterConstraint(11035, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(0, true, double.NegativeInfinity, true, 2147483647.0, true)
			{
				ConstratintId = "411"
			});
			this.RegisterConstraint(11144, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(0, true, 1.0, true, 409.55, true)
			{
				ConstratintId = "412"
			});
			this.RegisterConstraint(11289, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(2, 0, 255)
			{
				ConstratintId = "427"
			});
			this.RegisterConstraint(11289, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeAbsentConditionToValue(4, 3, new string[] { "custom" })
			{
				ConstratintId = "429"
			});
			this.RegisterConstraint(11289, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(9, 1, 255)
			{
				ConstratintId = "430"
			});
			this.RegisterConstraint(11289, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(11, 1, 255)
			{
				ConstratintId = "432"
			});
			this.RegisterConstraint(11289, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(5, true, 1.0, true, double.PositiveInfinity, true)
			{
				ConstratintId = "433"
			});
			this.RegisterConstraint(11289, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(0, true, 1.0, true, double.PositiveInfinity, true)
			{
				ConstratintId = "434"
			});
			this.RegisterConstraint(11289, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(4, 0, 32767)
			{
				ConstratintId = "436"
			});
			this.RegisterConstraint(11289, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(10, 1, 255)
			{
				ConstratintId = "438"
			});
			this.RegisterConstraint(11292, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(1, 0, 32000)
			{
				ConstratintId = "442"
			});
			this.RegisterConstraint(11247, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(0, true, 1.0, true, 4294967294.0, true)
			{
				ConstratintId = "444"
			});
			this.RegisterConstraint(11247, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(2, true, double.NegativeInfinity, true, 2147483647.0, true)
			{
				ConstratintId = "445"
			});
			this.RegisterConstraint(11248, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueSetConstraint(0, true, new string[] { "1" })
			{
				ConstratintId = "449"
			});
			this.RegisterConstraint(11249, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(1, 0, 32000)
			{
				ConstratintId = "450"
			});
			this.RegisterConstraint(11249, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(0, true, 1.0, true, 2147483647.0, true)
			{
				ConstratintId = "451"
			});
			this.RegisterConstraint(11053, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeMutualExclusive(new byte[] { 3, 2 })
			{
				ConstratintId = "452"
			});
			this.RegisterConstraint(11265, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(0, 0, 255)
			{
				ConstratintId = "463"
			});
			this.RegisterConstraint(11265, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(2, true, 0.0, true, 53.0, true)
			{
				ConstratintId = "464"
			});
			this.RegisterConstraint(11265, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(3, true, 0.0, true, 7.0, true)
			{
				ConstratintId = "465"
			});
			this.RegisterConstraint(11265, 11363, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(1, true, false, this)
			{
				ConstratintId = "466"
			});
			this.RegisterConstraint(11046, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new IndexReferenceConstraint(13, "/WorkbookPart/WorkbookStylesPart", -1, 11176, "DocumentFormat.OpenXml.Spreadsheet.DifferentialFormat", 0)
			{
				ConstratintId = "473"
			});
			this.RegisterConstraint(11046, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new IndexReferenceConstraint(15, "/WorkbookPart/WorkbookStylesPart", -1, 11176, "DocumentFormat.OpenXml.Spreadsheet.DifferentialFormat", 0)
			{
				ConstratintId = "475"
			});
			this.RegisterConstraint(11046, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new IndexReferenceConstraint(12, "/WorkbookPart/WorkbookStylesPart", -1, 11176, "DocumentFormat.OpenXml.Spreadsheet.DifferentialFormat", 0)
			{
				ConstratintId = "476"
			});
			this.RegisterConstraint(11046, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new IndexReferenceConstraint(16, "/WorkbookPart/WorkbookStylesPart", -1, 11176, "DocumentFormat.OpenXml.Spreadsheet.DifferentialFormat", 0)
			{
				ConstratintId = "479"
			});
			this.RegisterConstraint(11046, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new IndexReferenceConstraint(17, "/WorkbookPart/WorkbookStylesPart", -1, 11176, "DocumentFormat.OpenXml.Spreadsheet.DifferentialFormat", 0)
			{
				ConstratintId = "480"
			});
			this.RegisterConstraint(11046, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new IndexReferenceConstraint(14, "/WorkbookPart/WorkbookStylesPart", -1, 11176, "DocumentFormat.OpenXml.Spreadsheet.DifferentialFormat", 0)
			{
				ConstratintId = "481"
			});
			this.RegisterConstraint(11251, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(4, true, 0.0, true, 1.0, true)
			{
				ConstratintId = "487"
			});
			this.RegisterConstraint(11251, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(5, true, 0.0, true, 1.0, true)
			{
				ConstratintId = "488"
			});
			this.RegisterConstraint(11251, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(2, true, 0.0, true, 1.0, true)
			{
				ConstratintId = "489"
			});
			this.RegisterConstraint(11251, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(3, true, 0.0, true, 1.0, true)
			{
				ConstratintId = "490"
			});
			this.RegisterConstraint(11251, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(1, true, -1.7E+308, true, 1.7E+308, true)
			{
				ConstratintId = "491"
			});
			this.RegisterConstraint(11255, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(1, 0, 255)
			{
				ConstratintId = "495"
			});
			this.RegisterConstraint(11254, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(0, true, 0.0, true, 1.0, true)
			{
				ConstratintId = "497"
			});
			this.RegisterConstraint(11270, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(1, true, 1.0, true, 9.0, true)
			{
				ConstratintId = "501"
			});
			this.RegisterConstraint(11365, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(1, 1, 255)
			{
				ConstratintId = "502"
			});
			this.RegisterConstraint(11239, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new IndexReferenceConstraint(0, ".", -1, 11237, "DocumentFormat.OpenXml.Spreadsheet.MetadataType", 1)
			{
				ConstratintId = "504"
			});
			this.RegisterConstraint(11239, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(0, true, double.NegativeInfinity, true, 2147483647.0, true)
			{
				ConstratintId = "505"
			});
			this.RegisterConstraint(11061, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(5, true, true, this)
			{
				ConstratintId = "507"
			});
			this.RegisterConstraint(11061, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(7, true, 1.0, true, 8.0, true)
			{
				ConstratintId = "508"
			});
			this.RegisterConstraint(11275, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(0, true, 0.0, true, 65533.0, true)
			{
				ConstratintId = "509"
			});
			this.RegisterConstraint(11060, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(0, true, true, this)
			{
				ConstratintId = "510"
			});
			this.RegisterConstraint(11060, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(3, true, true, this)
			{
				ConstratintId = "511"
			});
			this.RegisterConstraint(10715, 10173, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(0, true, false, this)
			{
				ConstratintId = "517"
			});
			this.RegisterConstraint(10709, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(0, true, true, this)
			{
				ConstratintId = "519"
			});
			this.RegisterConstraint(10748, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(0, true, true, this)
			{
				ConstratintId = "521"
			});
			this.RegisterConstraint(10593, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(0, true, true, this)
			{
				ConstratintId = "524"
			});
			this.RegisterConstraint(10623, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new RelationshipTypeConstraint(3, "http://schemas.openxmlformats.org/officeDocument/2006/relationships/diagramColors")
			{
				ConstratintId = "525"
			});
			this.RegisterConstraint(10623, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new RelationshipTypeConstraint(0, "http://schemas.openxmlformats.org/officeDocument/2006/relationships/diagramData")
			{
				ConstratintId = "526"
			});
			this.RegisterConstraint(10623, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new RelationshipTypeConstraint(1, "http://schemas.openxmlformats.org/officeDocument/2006/relationships/diagramLayout")
			{
				ConstratintId = "527"
			});
			this.RegisterConstraint(10623, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new RelationshipTypeConstraint(2, "http://schemas.openxmlformats.org/officeDocument/2006/relationships/diagramQuickStyle")
			{
				ConstratintId = "528"
			});
			this.RegisterConstraint(10651, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new RelationshipTypeConstraint(2, "http://schemas.openxmlformats.org/officeDocument/2006/relationships/image")
			{
				ConstratintId = "529"
			});
			this.RegisterConstraint(12519, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueSetConstraint(19, true, new string[] { "0", "1", "2", "3" })
			{
				ConstratintId = "531"
			});
			this.RegisterConstraint(12519, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueSetConstraint(21, true, new string[] { "0", "1", "2", "3" })
			{
				ConstratintId = "532"
			});
			this.RegisterConstraint(12519, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(10, true, 0.0, true, 1000.0, true)
			{
				ConstratintId = "555"
			});
			this.RegisterConstraint(12519, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(20, true, 0.0, true, 6.0, true)
			{
				ConstratintId = "562"
			});
			this.RegisterConstraint(12519, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueSetConstraint(29, true, new string[] { "19" })
			{
				ConstratintId = "563"
			});
			this.RegisterConstraint(12520, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueSetConstraint(30, true, new string[] { "0", "1", "2", "3" })
			{
				ConstratintId = "569"
			});
			this.RegisterConstraint(12520, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueSetConstraint(32, true, new string[] { "0", "1", "2", "3" })
			{
				ConstratintId = "570"
			});
			this.RegisterConstraint(12509, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new RelationshipTypeConstraint(25, "http://schemas.openxmlformats.org/officeDocument/2006/relationships/image")
			{
				ConstratintId = "577"
			});
			this.RegisterConstraint(12514, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new RelationshipTypeConstraint(18, "http://schemas.openxmlformats.org/officeDocument/2006/relationships/image")
			{
				ConstratintId = "581"
			});
			this.RegisterConstraint(12514, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new RelationshipTypeConstraint(16, "http://schemas.openxmlformats.org/officeDocument/2006/relationships/image")
			{
				ConstratintId = "582"
			});
			this.RegisterConstraint(12514, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new RelationshipTypeConstraint(17, "http://schemas.openxmlformats.org/officeDocument/2006/relationships/image")
			{
				ConstratintId = "583"
			});
			this.RegisterConstraint(12514, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new RelationshipTypeConstraint(15, "http://schemas.openxmlformats.org/officeDocument/2006/relationships/image")
			{
				ConstratintId = "584"
			});
			this.RegisterConstraint(12510, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new RelationshipTypeConstraint(26, "http://schemas.openxmlformats.org/officeDocument/2006/relationships/image")
			{
				ConstratintId = "585"
			});
			this.RegisterConstraint(12517, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueSetConstraint(31, true, new string[] { "1", "2", "3" })
			{
				ConstratintId = "592"
			});
			this.RegisterConstraint(11298, 11337, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(0, true, false, this)
			{
				ConstratintId = "596"
			});
			this.RegisterConstraint(11219, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new IndexReferenceConstraint(0, "/WorkbookPart", -1, 11303, "DocumentFormat.OpenXml.Spreadsheet.SmartTagType", 0)
			{
				ConstratintId = "606"
			});
			this.RegisterConstraint(11202, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new IndexReferenceConstraint(1, "/WorkbookPart/WorkbookStylesPart", -1, 11176, "DocumentFormat.OpenXml.Spreadsheet.DifferentialFormat", 0)
			{
				ConstratintId = "607"
			});
			this.RegisterConstraint(11185, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new IndexReferenceConstraint(3, "/WorkbookPart/WorkbookStylesPart", -1, 11266, "DocumentFormat.OpenXml.Spreadsheet.CellFormat", 0)
			{
				ConstratintId = "611"
			});
			this.RegisterConstraint(11101, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(11, true, 0.0, true, 255.0, true)
			{
				ConstratintId = "623"
			});
			this.RegisterConstraint(11220, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(14, true, 10.0, true, 400.0, true)
			{
				ConstratintId = "627"
			});
			this.RegisterConstraint(11220, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(15, true, 10.0, true, 400.0, true)
			{
				ConstratintId = "628"
			});
			this.RegisterConstraint(11220, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(17, true, 10.0, true, 400.0, true)
			{
				ConstratintId = "629"
			});
			this.RegisterConstraint(11220, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(16, true, 10.0, true, 400.0, true)
			{
				ConstratintId = "630"
			});
			this.RegisterConstraint(11212, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new IndexReferenceConstraint(2, "/WorkbookPart", -1, 11304, "DocumentFormat.OpenXml.Spreadsheet.WorkbookView", 0)
			{
				ConstratintId = "631"
			});
			this.RegisterConstraint(11212, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(1, true, 10.0, true, 400.0, true)
			{
				ConstratintId = "632"
			});
			this.RegisterConstraint(11186, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(4, true, -1.0, true, 1.0, true)
			{
				ConstratintId = "634"
			});
			this.RegisterConstraint(11471, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new IndexReferenceConstraint(0, "/WorkbookPart/WorkbookStylesPart", -1, 11176, "DocumentFormat.OpenXml.Spreadsheet.DifferentialFormat", 0)
			{
				ConstratintId = "635"
			});
			this.RegisterConstraint(11466, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(2, true, 1.0, true, 31.0, true)
			{
				ConstratintId = "636"
			});
			this.RegisterConstraint(11466, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(3, true, 0.0, true, 23.0, true)
			{
				ConstratintId = "637"
			});
			this.RegisterConstraint(11466, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(4, true, 0.0, true, 59.0, true)
			{
				ConstratintId = "638"
			});
			this.RegisterConstraint(11466, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(1, true, 1.0, true, 12.0, true)
			{
				ConstratintId = "639"
			});
			this.RegisterConstraint(11466, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(5, true, 0.0, true, 59.0, true)
			{
				ConstratintId = "640"
			});
			this.RegisterConstraint(11466, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(0, true, 1000.0, true, 9999.0, true)
			{
				ConstratintId = "641"
			});
			this.RegisterConstraint(11148, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(0, true, 0.0, true, 255.0, true)
			{
				ConstratintId = "642"
			});
			this.RegisterConstraint(11154, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new IndexReferenceConstraint(0, "/WorkbookPart/WorkbookStylesPart", -1, 11258, "DocumentFormat.OpenXml.Spreadsheet.Font", 0)
			{
				ConstratintId = "643"
			});
			this.RegisterConstraint(11146, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(0, 0, 31)
			{
				ConstratintId = "644"
			});
			this.RegisterConstraint(11153, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLessEqualToAnother(0, 1, false)
			{
				ConstratintId = "645"
			});
			this.RegisterConstraint(11046, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new ReferenceExistConstraint(21, "/WorkbookPart/ConnectionsPart", 11061, "DocumentFormat.OpenXml.Spreadsheet.Connection", 0)
			{
				ConstratintId = "647"
			});
			this.RegisterConstraint(11046, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueSetConstraint(0, false, new string[] { "0", "" })
			{
				ConstratintId = "653"
			});
			this.RegisterConstraint(11289, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new IndexReferenceConstraint(7, "/WorkbookPart/WorkbookStylesPart", 11364, 11176, "DocumentFormat.OpenXml.Spreadsheet.DifferentialFormat", 0)
			{
				ConstratintId = "658"
			});
			this.RegisterConstraint(11289, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new IndexReferenceConstraint(6, "/WorkbookPart/WorkbookStylesPart", -1, 11176, "DocumentFormat.OpenXml.Spreadsheet.DifferentialFormat", 0)
			{
				ConstratintId = "659"
			});
			this.RegisterConstraint(11289, 11046, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(0, true, false, this)
			{
				ConstratintId = "660"
			});
			this.RegisterConstraint(11289, 11046, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(2, true, false, this)
			{
				ConstratintId = "661"
			});
			this.RegisterConstraint(11289, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new IndexReferenceConstraint(8, "/WorkbookPart/WorkbookStylesPart", -1, 11176, "DocumentFormat.OpenXml.Spreadsheet.DifferentialFormat", 0)
			{
				ConstratintId = "662"
			});
			this.RegisterConstraint(11292, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new ReferenceExistConstraint(0, "CustomXmlMappingsPart", 11059, "DocumentFormat.OpenXml.Spreadsheet.Map", 0)
			{
				ConstratintId = "664"
			});
			this.RegisterConstraint(11247, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(0, true, true, this)
			{
				ConstratintId = "666"
			});
			this.RegisterConstraint(11248, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(1, true, true, this)
			{
				ConstratintId = "668"
			});
			this.RegisterConstraint(11053, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new ReferenceExistConstraint(1, "/WorkbookPart", 11302, "DocumentFormat.OpenXml.Spreadsheet.Sheet", 1)
			{
				ConstratintId = "670"
			});
			this.RegisterConstraint(11056, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new IndexReferenceConstraint(1, ".", -1, 11057, "DocumentFormat.OpenXml.Spreadsheet.Author", 0)
			{
				ConstratintId = "671"
			});
			this.RegisterConstraint(11056, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(2, false, true, this)
			{
				ConstratintId = "672"
			});
			this.RegisterConstraint(11256, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(8, true, 0.0, true, 2.0, true)
			{
				ConstratintId = "673"
			});
			this.RegisterConstraint(11253, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(4, true, -1.0, true, 1.0, true)
			{
				ConstratintId = "677"
			});
			this.RegisterConstraint(11265, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new IndexReferenceConstraint(1, "/WorkbookPart/WorkbookStylesPart", -1, 11266, "DocumentFormat.OpenXml.Spreadsheet.CellFormat", 0)
			{
				ConstratintId = "679"
			});
			this.RegisterConstraint(11252, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(4, true, -1.0, true, 1.0, true)
			{
				ConstratintId = "683"
			});
			this.RegisterConstraint(11270, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new IndexReferenceConstraint(2, "/WorkbookPart/WorkbookStylesPart", -1, 11176, "DocumentFormat.OpenXml.Spreadsheet.DifferentialFormat", 0)
			{
				ConstratintId = "692"
			});
			this.RegisterConstraint(11266, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new IndexReferenceConstraint(3, "/WorkbookPart/WorkbookStylesPart", -1, 11260, "DocumentFormat.OpenXml.Spreadsheet.Border", 0)
			{
				ConstratintId = "693"
			});
			this.RegisterConstraint(11266, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(3, true, 0.0, true, double.PositiveInfinity, true)
			{
				ConstratintId = "693"
			});
			this.RegisterConstraint(11266, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new IndexReferenceConstraint(2, "/WorkbookPart/WorkbookStylesPart", -1, 11259, "DocumentFormat.OpenXml.Spreadsheet.Fill", 0)
			{
				ConstratintId = "694"
			});
			this.RegisterConstraint(11266, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new IndexReferenceConstraint(1, "/WorkbookPart/WorkbookStylesPart", -1, 11258, "DocumentFormat.OpenXml.Spreadsheet.Font", 0)
			{
				ConstratintId = "695"
			});
			this.RegisterConstraint(11266, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new IndexReferenceConstraint(4, "/WorkbookPart/WorkbookStylesPart", 11361, 11266, "DocumentFormat.OpenXml.Spreadsheet.CellFormat", 0)
			{
				ConstratintId = "697"
			});
			this.RegisterConstraint(11235, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(0, true, double.NegativeInfinity, true, 2147483647.0, true)
			{
				ConstratintId = "699"
			});
			this.RegisterConstraint(11234, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(1, true, double.NegativeInfinity, true, 2147483647.0, true)
			{
				ConstratintId = "700"
			});
			this.RegisterConstraint(11234, 11042, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(0, true, false, this)
			{
				ConstratintId = "702"
			});
			this.RegisterConstraint(11234, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueSetConstraint(0, false, new string[] { "XLMDX" })
			{
				ConstratintId = "702"
			});
			this.RegisterConstraint(11234, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(0, 1, 65535)
			{
				ConstratintId = "703"
			});
			this.RegisterConstraint(11245, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new IndexReferenceConstraint(0, ".", 11232, 11065, "DocumentFormat.OpenXml.Spreadsheet.CharacterValue", 0)
			{
				ConstratintId = "704"
			});
			this.RegisterConstraint(11245, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(0, true, double.NegativeInfinity, true, 2147483647.0, true)
			{
				ConstratintId = "705"
			});
			this.RegisterConstraint(11245, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new IndexReferenceConstraint(1, "/WorkbookPart/CellMetadataPart", 11232, 11065, "DocumentFormat.OpenXml.Spreadsheet.CharacterValue", 0)
			{
				ConstratintId = "706"
			});
			this.RegisterConstraint(11245, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(1, true, double.NegativeInfinity, true, 2147483647.0, true)
			{
				ConstratintId = "707"
			});
			this.RegisterConstraint(11241, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new IndexReferenceConstraint(0, "/WorkbookPart/CellMetadataPart", 11232, 11065, "DocumentFormat.OpenXml.Spreadsheet.CharacterValue", 0)
			{
				ConstratintId = "708"
			});
			this.RegisterConstraint(11241, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(0, true, double.NegativeInfinity, true, 2147483647.0, true)
			{
				ConstratintId = "709"
			});
			this.RegisterConstraint(11233, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(0, true, double.NegativeInfinity, true, 2147483647.0, true)
			{
				ConstratintId = "710"
			});
			this.RegisterConstraint(11232, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(0, true, double.NegativeInfinity, true, 2147483647.0, true)
			{
				ConstratintId = "711"
			});
			this.RegisterConstraint(11237, 11231, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(0, true, false, this)
			{
				ConstratintId = "714"
			});
			this.RegisterConstraint(11237, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(0, 1, 65535)
			{
				ConstratintId = "715"
			});
			this.RegisterConstraint(11231, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(0, true, double.NegativeInfinity, true, 2147483647.0, true)
			{
				ConstratintId = "742"
			});
			this.RegisterConstraint(11243, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new IndexReferenceConstraint(0, "/WorkbookPart/CellMetadataPart", 11232, 11065, "DocumentFormat.OpenXml.Spreadsheet.CharacterValue", 0)
			{
				ConstratintId = "743"
			});
			this.RegisterConstraint(11243, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(0, true, 0.0, true, 2147483647.0, true)
			{
				ConstratintId = "744"
			});
			this.RegisterConstraint(11243, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(1, true, 0.0, true, 2147483647.0, true)
			{
				ConstratintId = "745"
			});
			this.RegisterConstraint(11246, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(0, true, 0.0, true, double.PositiveInfinity, true)
			{
				ConstratintId = "746"
			});
			this.RegisterConstraint(11246, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(0, true, double.NegativeInfinity, true, 2147483647.0, true)
			{
				ConstratintId = "747"
			});
			this.RegisterConstraint(11244, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new IndexReferenceConstraint(0, "/WorkbookPart/CellMetadataPart", 11232, 11065, "DocumentFormat.OpenXml.Spreadsheet.CharacterValue", 0)
			{
				ConstratintId = "748"
			});
			this.RegisterConstraint(11244, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(0, true, double.NegativeInfinity, true, 2147483647.0, true)
			{
				ConstratintId = "749"
			});
			this.RegisterConstraint(11244, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new IndexReferenceConstraint(1, "/WorkbookPart/CellMetadataPart", 11232, 11065, "DocumentFormat.OpenXml.Spreadsheet.CharacterValue", 0)
			{
				ConstratintId = "750"
			});
			this.RegisterConstraint(11244, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(1, true, double.NegativeInfinity, true, 2147483647.0, true)
			{
				ConstratintId = "751"
			});
			this.RegisterConstraint(11242, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(2, true, double.NegativeInfinity, true, 2147483647.0, true)
			{
				ConstratintId = "758"
			});
			this.RegisterConstraint(11242, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new IndexReferenceConstraint(2, "/WorkbookPart/CellMetadataPart", 11232, 11065, "DocumentFormat.OpenXml.Spreadsheet.CharacterValue", 0)
			{
				ConstratintId = "759"
			});
			this.RegisterConstraint(11242, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(0, true, double.NegativeInfinity, true, 2147483647.0, true)
			{
				ConstratintId = "760"
			});
			this.RegisterConstraint(11242, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(3, true, double.NegativeInfinity, true, 58.0, true)
			{
				ConstratintId = "761"
			});
			this.RegisterConstraint(11236, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(0, true, double.NegativeInfinity, true, 2147483647.0, true)
			{
				ConstratintId = "764"
			});
			this.RegisterConstraint(11069, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(0, true, true, this)
			{
				ConstratintId = "773"
			});
			this.RegisterConstraint(11121, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(10, true, true, this)
			{
				ConstratintId = "790"
			});
			this.RegisterConstraint(11121, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(9, true, true, this)
			{
				ConstratintId = "796"
			});
			this.RegisterConstraint(11121, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(0, true, true, this)
			{
				ConstratintId = "798"
			});
			this.RegisterConstraint(11445, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new ReferenceExistConstraint(1, "/WorkbookPart/ConnectionsPart", 11061, "DocumentFormat.OpenXml.Spreadsheet.Connection", 0)
			{
				ConstratintId = "821"
			});
			this.RegisterConstraint(11445, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(1, true, true, this)
			{
				ConstratintId = "822"
			});
			this.RegisterConstraint(11102, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(0, 1, 63999)
			{
				ConstratintId = "828"
			});
			this.RegisterConstraint(11102, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeAbsentConditionToValue(3, 6, new string[] { "1" })
			{
				ConstratintId = "833"
			});
			this.RegisterConstraint(11102, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeRequiredConditionToValue(3, 6, new string[] { "0" })
			{
				ConstratintId = "833"
			});
			this.RegisterConstraint(11102, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeAbsentConditionToValue(4, 6, new string[] { "1" })
			{
				ConstratintId = "835"
			});
			this.RegisterConstraint(11102, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeAbsentConditionToValue(2, 6, new string[] { "1" })
			{
				ConstratintId = "836"
			});
			this.RegisterConstraint(11102, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeRequiredConditionToValue(2, 6, new string[] { "0" })
			{
				ConstratintId = "836"
			});
			this.RegisterConstraint(11109, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueConditionToAnother(1, 0, new string[] { "none", "all" }, new string[] { "data", "selection" })
			{
				ConstratintId = "843"
			});
			this.RegisterConstraint(11105, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new IndexReferenceConstraint(1, ".", -1, 11103, "DocumentFormat.OpenXml.Spreadsheet.PivotField", 0)
			{
				ConstratintId = "856"
			});
			this.RegisterConstraint(11116, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(2, true, true, this)
			{
				ConstratintId = "861"
			});
			this.RegisterConstraint(11078, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new IndexReferenceConstraint(5, ".", -1, 11095, "DocumentFormat.OpenXml.Spreadsheet.ServerFormat", 0)
			{
				ConstratintId = "866"
			});
			this.RegisterConstraint(11107, 11421, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(0, true, false, this)
			{
				ConstratintId = "881"
			});
			this.RegisterConstraint(11085, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new IndexReferenceConstraint(0, ".", -1, 11069, "DocumentFormat.OpenXml.Spreadsheet.CacheField", 0)
			{
				ConstratintId = "889"
			});
			this.RegisterConstraint(11085, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(0, true, -1.0, true, double.PositiveInfinity, true)
			{
				ConstratintId = "889"
			});
			this.RegisterConstraint(11119, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new IndexReferenceConstraint(0, ".", -1, 11103, "DocumentFormat.OpenXml.Spreadsheet.PivotField", 0)
			{
				ConstratintId = "893"
			});
			this.RegisterConstraint(11119, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(4, false, true, this)
			{
				ConstratintId = "899"
			});
			this.RegisterConstraint(11119, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new IndexReferenceConstraint(6, ".", -1, 11103, "DocumentFormat.OpenXml.Spreadsheet.PivotField", 0)
			{
				ConstratintId = "900"
			});
			this.RegisterConstraint(11119, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new IndexReferenceConstraint(5, ".", -1, 11112, "DocumentFormat.OpenXml.Spreadsheet.PivotHierarchy", 0)
			{
				ConstratintId = "901"
			});
			this.RegisterConstraint(11108, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new IndexReferenceConstraint(1, "/WorkbookPart/WorkbookStylesPart", -1, 11176, "DocumentFormat.OpenXml.Spreadsheet.DifferentialFormat", 0)
			{
				ConstratintId = "903"
			});
			this.RegisterConstraint(11088, 11087, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(4, true, false, this)
			{
				ConstratintId = "905"
			});
			this.RegisterConstraint(11088, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(4, true, 1.0, true, double.PositiveInfinity, true)
			{
				ConstratintId = "906"
			});
			this.RegisterConstraint(11088, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(3, 0, 65535)
			{
				ConstratintId = "907"
			});
			this.RegisterConstraint(11088, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(1, true, true, this)
			{
				ConstratintId = "911"
			});
			this.RegisterConstraint(11086, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(0, true, true, this)
			{
				ConstratintId = "913"
			});
			this.RegisterConstraint(11090, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(0, true, true, this)
			{
				ConstratintId = "918"
			});
			this.RegisterConstraint(11106, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new IndexReferenceConstraint(2, ".", -1, 11105, "DocumentFormat.OpenXml.Spreadsheet.DataField", 0)
			{
				ConstratintId = "921"
			});
			this.RegisterConstraint(11104, 11103, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(0, true, false, this)
			{
				ConstratintId = "947"
			});
			this.RegisterConstraint(11104, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueSetConstraint(1, false, new string[] { "blank", "grand" })
			{
				ConstratintId = "952"
			});
			this.RegisterConstraint(11084, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(6, true, true, this)
			{
				ConstratintId = "953"
			});
			this.RegisterConstraint(11084, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(7, true, true, this)
			{
				ConstratintId = "954"
			});
			this.RegisterConstraint(11084, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(8, true, true, this)
			{
				ConstratintId = "956"
			});
			this.RegisterConstraint(11084, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(0, true, true, this)
			{
				ConstratintId = "957"
			});
			this.RegisterConstraint(11084, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(5, true, true, this)
			{
				ConstratintId = "958"
			});
			this.RegisterConstraint(11084, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(9, true, true, this)
			{
				ConstratintId = "959"
			});
			this.RegisterConstraint(11075, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new IndexReferenceConstraint(4, ".", -1, 11095, "DocumentFormat.OpenXml.Spreadsheet.ServerFormat", 0)
			{
				ConstratintId = "978"
			});
			this.RegisterConstraint(11114, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new IndexReferenceConstraint(8, "PivotTableCacheDefinitionPart", -1, 11069, "DocumentFormat.OpenXml.Spreadsheet.CacheField", 0)
			{
				ConstratintId = "999"
			});
			this.RegisterConstraint(11443, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new IndexReferenceConstraint(0, ".", -1, 11069, "DocumentFormat.OpenXml.Spreadsheet.CacheField", 0)
			{
				ConstratintId = "1016"
			});
			this.RegisterConstraint(11076, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new IndexReferenceConstraint(5, ".", -1, 11095, "DocumentFormat.OpenXml.Spreadsheet.ServerFormat", 0)
			{
				ConstratintId = "1017"
			});
			this.RegisterConstraint(11125, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new IndexReferenceConstraint(0, "PivotTableCacheDefinitionPart", -1, 11069, "DocumentFormat.OpenXml.Spreadsheet.CacheField", 0)
			{
				ConstratintId = "1029"
			});
			this.RegisterConstraint(11125, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(3, true, true, this)
			{
				ConstratintId = "1032"
			});
			this.RegisterConstraint(11031, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(6, 0, 255)
			{
				ConstratintId = "1042"
			});
			this.RegisterConstraint(11103, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueSetConstraint(1, false, new string[] { "axisValues" })
			{
				ConstratintId = "1101"
			});
			this.RegisterConstraint(11112, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(10, 0, 65535)
			{
				ConstratintId = "1142"
			});
			this.RegisterConstraint(11033, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(4, true, 0.0, true, 16.0, true)
			{
				ConstratintId = "1143"
			});
			this.RegisterConstraint(11033, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new ReferenceExistConstraint(1, "/WorkbookPart", 11298, "DocumentFormat.OpenXml.Spreadsheet.PivotCache", 0)
			{
				ConstratintId = "1144"
			});
			this.RegisterConstraint(11033, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(50, true, double.NegativeInfinity, true, 127.0, true)
			{
				ConstratintId = "1197"
			});
			this.RegisterConstraint(11033, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(40, true, double.NegativeInfinity, true, 255.0, true)
			{
				ConstratintId = "1198"
			});
			this.RegisterConstraint(11099, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(0, 0, 65535)
			{
				ConstratintId = "1204"
			});
			this.RegisterConstraint(11122, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLessEqualToAnother(3, 4, false)
			{
				ConstratintId = "1210"
			});
			this.RegisterConstraint(11074, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeMutualExclusive(new byte[] { 5, 4 })
			{
				ConstratintId = "1222"
			});
			this.RegisterConstraint(11074, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(6, 1, 31)
			{
				ConstratintId = "1235"
			});
			this.RegisterConstraint(11079, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new IndexReferenceConstraint(5, ".", -1, 11095, "DocumentFormat.OpenXml.Spreadsheet.ServerFormat", 0)
			{
				ConstratintId = "1238"
			});
			this.RegisterConstraint(11095, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeMutualExclusive(new byte[] { 0, 1 })
			{
				ConstratintId = "1253"
			});
			this.RegisterConstraint(11095, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(0, 0, 31)
			{
				ConstratintId = "1254"
			});
			this.RegisterConstraint(11095, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(1, 0, 65535)
			{
				ConstratintId = "1255"
			});
			this.RegisterConstraint(11441, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueSetConstraint(9, false, new string[] { "NaN", "INF", "-INF" })
			{
				ConstratintId = "1260"
			});
			this.RegisterConstraint(11441, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueSetConstraint(8, false, new string[] { "NaN", "INF", "-INF" })
			{
				ConstratintId = "1270"
			});
			this.RegisterConstraint(11441, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLessEqualToAnother(8, 9, true)
			{
				ConstratintId = "1272"
			});
			this.RegisterConstraint(11096, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new IndexReferenceConstraint(0, ".", -1, 11069, "DocumentFormat.OpenXml.Spreadsheet.CacheField", 0)
			{
				ConstratintId = "1288"
			});
			this.RegisterConstraint(11096, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new IndexReferenceConstraint(1, ".", -1, 11121, "DocumentFormat.OpenXml.Spreadsheet.CacheHierarchy", 0)
			{
				ConstratintId = "1289"
			});
			this.RegisterConstraint(11438, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(1, 1, 255)
			{
				ConstratintId = "1297"
			});
			this.RegisterConstraint(11438, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(2, 0, 31)
			{
				ConstratintId = "1298"
			});
			this.RegisterConstraint(11036, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(8, true, 1.0, true, 2147483647.0, true)
			{
				ConstratintId = "1328"
			});
			this.RegisterConstraint(11036, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(11, true, 0.0, true, 32768.0, true)
			{
				ConstratintId = "1329"
			});
			this.RegisterConstraint(11036, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(7, true, double.NegativeInfinity, true, 2147483647.0, true)
			{
				ConstratintId = "1330"
			});
			this.RegisterConstraint(11036, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueSetConstraint(0, false, new string[] { "00000000-0000-0000-0000-000000000000" })
			{
				ConstratintId = "1331"
			});
			this.RegisterConstraint(11172, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new IndexReferenceConstraint(3, "/WorkbookPart/CellMetadataPart", -1, 11235, "DocumentFormat.OpenXml.Spreadsheet.CellMetadata", 0)
			{
				ConstratintId = "1343"
			});
			this.RegisterConstraint(11172, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new IndexReferenceConstraint(1, "/WorkbookPart/WorkbookStylesPart", -1, 11265, "DocumentFormat.OpenXml.Spreadsheet.CellStyle", 0)
			{
				ConstratintId = "1344"
			});
			this.RegisterConstraint(11172, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new IndexReferenceConstraint(4, "/WorkbookPart/CellMetadataPart", -1, 11236, "DocumentFormat.OpenXml.Spreadsheet.ValueMetadata", 0)
			{
				ConstratintId = "1345"
			});
			this.RegisterConstraint(11163, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(1, true, 0.0, true, 16.0, true)
			{
				ConstratintId = "1347"
			});
			this.RegisterConstraint(11167, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(3, true, 0.0, true, 32767.0, true)
			{
				ConstratintId = "1354"
			});
			this.RegisterConstraint(11165, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(8, 1, 52)
			{
				ConstratintId = "1357"
			});
			this.RegisterConstraint(11164, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(8, true, 1.0, true, 14.0, true)
			{
				ConstratintId = "1358"
			});
			this.RegisterConstraint(11164, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(3, true, double.NegativeInfinity, true, 32767.0, true)
			{
				ConstratintId = "1360"
			});
			this.RegisterConstraint(11164, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(14, 0, 32767)
			{
				ConstratintId = "1361"
			});
			this.RegisterConstraint(11164, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(15, 0, 32767)
			{
				ConstratintId = "1362"
			});
			this.RegisterConstraint(11164, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(16, 0, 32767)
			{
				ConstratintId = "1363"
			});
			this.RegisterConstraint(11164, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(17, 0, 32767)
			{
				ConstratintId = "1364"
			});
			this.RegisterConstraint(11164, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(18, 0, 32767)
			{
				ConstratintId = "1365"
			});
			this.RegisterConstraint(11164, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(19, 0, 32767)
			{
				ConstratintId = "1366"
			});
			this.RegisterConstraint(11164, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(20, 0, 32767)
			{
				ConstratintId = "1367"
			});
			this.RegisterConstraint(11164, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(21, 0, 32767)
			{
				ConstratintId = "1368"
			});
			this.RegisterConstraint(11164, 11037, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(5, true, false, this)
			{
				ConstratintId = "1371"
			});
			this.RegisterConstraint(11164, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValuePatternConstraint(5, "[a-zA-Z_\\\\][a-zA-Z0-9_.]*")
			{
				ConstratintId = "1372"
			});
			this.RegisterConstraint(11170, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(0, true, double.NegativeInfinity, true, 2147483647.0, true)
			{
				ConstratintId = "1374"
			});
			this.RegisterConstraint(11162, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(0, true, 0.0, true, 32767.0, true)
			{
				ConstratintId = "1377"
			});
			this.RegisterConstraint(11157, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(3, true, double.NegativeInfinity, true, 32767.0, true)
			{
				ConstratintId = "1385"
			});
			this.RegisterConstraint(11157, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(6, true, double.NegativeInfinity, true, 32767.0, true)
			{
				ConstratintId = "1386"
			});
			this.RegisterConstraint(11156, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(3, 0, 32767)
			{
				ConstratintId = "1391"
			});
			this.RegisterConstraint(11159, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(3, 0, 32767)
			{
				ConstratintId = "1393"
			});
			this.RegisterConstraint(11177, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(0, true, double.NegativeInfinity, true, 65535.0, true)
			{
				ConstratintId = "1397"
			});
			this.RegisterConstraint(11171, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(10, true, double.NegativeInfinity, true, 32767.0, true)
			{
				ConstratintId = "1399"
			});
			this.RegisterConstraint(11171, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueConditionToAnother(2, 5, new string[] { "false" }, new string[] { "true" })
			{
				ConstratintId = "1402"
			});
			this.RegisterConstraint(11171, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeMutualExclusive(new byte[] { 8, 9 })
			{
				ConstratintId = "1404"
			});
			this.RegisterConstraint(11034, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueConditionToAnother(4, 5, new string[] { "true" }, new string[] { "true" })
			{
				ConstratintId = "1409"
			});
			this.RegisterConstraint(11034, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(14, true, 1.0, true, double.PositiveInfinity, true)
			{
				ConstratintId = "1411"
			});
			this.RegisterConstraint(11034, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new ReferenceExistConstraint(14, "/WorkbookPart/ConnectionsPart", 11061, "DocumentFormat.OpenXml.Spreadsheet.Connection", 0)
			{
				ConstratintId = "1412"
			});
			this.RegisterConstraint(11034, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(0, 1, 255)
			{
				ConstratintId = "1413"
			});
			this.RegisterConstraint(11133, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(0, true, true, this)
			{
				ConstratintId = "1415"
			});
			this.RegisterConstraint(11133, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(1, true, true, this)
			{
				ConstratintId = "1416"
			});
			this.RegisterConstraint(11133, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(1, 0, 255)
			{
				ConstratintId = "1419"
			});
			this.RegisterConstraint(11133, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueConditionToAnother(2, 5, new string[] { "true" }, new string[] { "true" })
			{
				ConstratintId = "1420"
			});
			this.RegisterConstraint(11133, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueConditionToAnother(2, 4, new string[] { "false" }, new string[] { "true" })
			{
				ConstratintId = "1421"
			});
			this.RegisterConstraint(11133, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueConditionToAnother(2, 3, new string[] { "true" }, new string[] { "true" })
			{
				ConstratintId = "1422"
			});
			this.RegisterConstraint(11129, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(5, true, double.NegativeInfinity, true, 16383.0, true)
			{
				ConstratintId = "1424"
			});
			this.RegisterConstraint(11129, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(6, true, double.NegativeInfinity, true, 16383.0, true)
			{
				ConstratintId = "1425"
			});
			this.RegisterConstraint(11129, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(4, true, double.NegativeInfinity, true, 65535.0, true)
			{
				ConstratintId = "1426"
			});
			this.RegisterConstraint(11129, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(3, true, 0.0, true, 31.0, true)
			{
				ConstratintId = "1428"
			});
			this.RegisterConstraint(11061, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(0, true, true, this)
			{
				ConstratintId = "1429"
			});
			this.RegisterConstraint(11456, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(3, true, 1.0, true, 5.0, true)
			{
				ConstratintId = "1441"
			});
			this.RegisterConstraint(11457, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(4, true, 1.0, true, 1048576.0, true)
			{
				ConstratintId = "1443"
			});
			this.RegisterConstraint(11063, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(0, 0, 255)
			{
				ConstratintId = "1445"
			});
			this.RegisterConstraint(11063, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueSetConstraint(1, true, new string[]
			{
				"-22", "-20", "-11", "-10", "-9", "-8", "-7", "-6", "-5", "-4",
				"-3", "-2", "-1", "0", "1", "2", "3", "4", "5", "6",
				"7", "8", "9", "10", "11", "12", "101", "102", "103", "104",
				"105", "106", "107", "108", "109", "110", "111", "112", "113"
			})
			{
				ConstratintId = "1446"
			});
			this.RegisterConstraint(11063, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(8, 0, 255)
			{
				ConstratintId = "1447"
			});
			this.RegisterConstraint(11063, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeRequiredConditionToValue(9, 2, new string[] { "cell" })
			{
				ConstratintId = "1448"
			});
			this.RegisterConstraint(11063, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(4, 0, 65535)
			{
				ConstratintId = "1449"
			});
			this.RegisterConstraint(11068, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(1, true, 0.0, true, 2147483647.0, true)
			{
				ConstratintId = "1452"
			});
			this.RegisterConstraint(11459, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(6, 1, 255)
			{
				ConstratintId = "1455"
			});
			this.RegisterConstraint(11459, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(7, 1, 255)
			{
				ConstratintId = "1456"
			});
			this.RegisterConstraint(11277, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueSetConstraint(1, false, new string[] { "s" })
			{
				ConstratintId = "1460"
			});
			this.RegisterConstraint(11277, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new IndexReferenceConstraint(2, "/WorkbookPart/CellMetadataPart", -1, 11236, "DocumentFormat.OpenXml.Spreadsheet.ValueMetadata", 0)
			{
				ConstratintId = "1461"
			});
			this.RegisterConstraint(11279, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(0, 0, 255)
			{
				ConstratintId = "1464"
			});
			this.RegisterConstraint(11279, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueConditionToAnother(0, 1, new string[] { "StdDocumentName" }, new string[] { "true" })
			{
				ConstratintId = "1465"
			});
			this.RegisterConstraint(11284, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(0, 1, 255)
			{
				ConstratintId = "1466"
			});
			this.RegisterConstraint(11284, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(1, 0, 255)
			{
				ConstratintId = "1467"
			});
			this.RegisterConstraint(11274, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(2, true, 0.0, true, 65533.0, true)
			{
				ConstratintId = "1472"
			});
			this.RegisterConstraint(11347, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(0, 0, 255)
			{
				ConstratintId = "1474"
			});
			this.RegisterConstraint(11285, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(1, 1, 255)
			{
				ConstratintId = "1475"
			});
			this.RegisterConstraint(11276, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(0, true, double.NegativeInfinity, true, 1048576.0, true)
			{
				ConstratintId = "1477"
			});
			this.RegisterConstraint(11458, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(8, 1, int.MaxValue)
			{
				ConstratintId = "1480"
			});
			this.RegisterConstraint(11286, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(0, 0, 31)
			{
				ConstratintId = "1484"
			});
			this.RegisterConstraint(11060, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(4, true, 0.0, true, 4.0, true)
			{
				ConstratintId = "1496"
			});
			this.RegisterConstraint(11060, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeAbsentConditionToValue(2, 1, new string[] { "false" })
			{
				ConstratintId = "1498"
			});
			this.RegisterConstraint(11060, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeRequiredConditionToValue(2, 1, new string[] { "true" })
			{
				ConstratintId = "1498"
			});
			this.RegisterConstraint(11059, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(3, true, true, this)
			{
				ConstratintId = "1504"
			});
			this.RegisterConstraint(11059, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(1, 0, 65535)
			{
				ConstratintId = "1510"
			});
			this.RegisterConstraint(11059, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(2, 0, 65535)
			{
				ConstratintId = "1511"
			});
			this.RegisterConstraint(11059, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(0, true, 1.0, true, 2147483647.0, true)
			{
				ConstratintId = "1512"
			});
			this.RegisterConstraint(11058, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(0, true, true, this)
			{
				ConstratintId = "1513"
			});
			this.RegisterConstraint(12163, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLessEqualToAnother(0, 1, true)
			{
				ConstratintId = "1569"
			});
			this.RegisterConstraint(12252, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(0, false, true, this)
			{
				ConstratintId = "1580"
			});
			this.RegisterConstraint(12286, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(0, true, true, this)
			{
				ConstratintId = "1592"
			});
			this.RegisterConstraint(12248, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(0, false, true, this)
			{
				ConstratintId = "1599"
			});
			this.RegisterConstraint(12248, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(0, true, 0.0, true, double.PositiveInfinity, true)
			{
				ConstratintId = "1599"
			});
			this.RegisterConstraint(12248, 12173, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(4, true, false, this)
			{
				ConstratintId = "1601"
			});
			this.RegisterConstraint(10340, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeCannotOmitConstraint(0)
			{
				ConstratintId = "1677"
			});
			this.RegisterConstraint(10020, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(0, true, 0.0, true, 100000.0, true)
			{
				ConstratintId = "1687"
			});
			this.RegisterConstraint(10293, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(0, true, true, this)
			{
				ConstratintId = "1719"
			});
			this.RegisterConstraint(10315, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new ReferenceExistConstraint(17, "/PresentationPart", 12347, "DocumentFormat.OpenXml.Presentation.SmartTags", 0)
			{
				ConstratintId = "1724"
			});
			this.RegisterConstraint(10172, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeCannotOmitConstraint(0)
			{
				ConstratintId = "1725"
			});
			this.RegisterConstraint(10326, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeCannotOmitConstraint(0)
			{
				ConstratintId = "1726"
			});
			this.RegisterConstraint(10308, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new ReferenceExistConstraint(17, "/PresentationPart", 12347, "DocumentFormat.OpenXml.Presentation.SmartTags", 0)
			{
				ConstratintId = "1727"
			});
			this.RegisterConstraint(10261, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(0, true, true, this)
			{
				ConstratintId = "1732"
			});
			this.RegisterConstraint(10260, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(0, true, 1.0, true, double.PositiveInfinity, true)
			{
				ConstratintId = "1738"
			});
			this.RegisterConstraint(10260, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(1, true, 1.0, true, double.PositiveInfinity, true)
			{
				ConstratintId = "1739"
			});
			this.RegisterConstraint(10234, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(0, true, 1.0, true, double.PositiveInfinity, true)
			{
				ConstratintId = "1745"
			});
			this.RegisterConstraint(10234, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(1, true, 1.0, true, double.PositiveInfinity, true)
			{
				ConstratintId = "1746"
			});
			this.RegisterConstraint(10615, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(0, true, true, this)
			{
				ConstratintId = "1753"
			});
			this.RegisterConstraint(10726, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(0, 0, 256)
			{
				ConstratintId = "1763"
			});
			this.RegisterConstraint(10725, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(0, 0, 256)
			{
				ConstratintId = "1765"
			});
			this.RegisterConstraint(10723, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(0, 0, 256)
			{
				ConstratintId = "1772"
			});
			this.RegisterConstraint(10373, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(0, true, double.NegativeInfinity, true, 2147483647.0, true)
			{
				ConstratintId = "1782"
			});
			this.RegisterConstraint(10441, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(0, true, 0.0, true, double.PositiveInfinity, true)
			{
				ConstratintId = "1784"
			});
			this.RegisterConstraint(10385, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueSetConstraint(0, false, new string[] { "INF", "-INF", "NaN" })
			{
				ConstratintId = "1796"
			});
			this.RegisterConstraint(10383, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(0, true, double.NegativeInfinity, true, 2147483647.0, true)
			{
				ConstratintId = "1797"
			});
			this.RegisterConstraint(10474, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueSetConstraint(0, false, new string[] { "INF", "-INF", "NaN" })
			{
				ConstratintId = "1798"
			});
			this.RegisterConstraint(10434, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(0, true, double.NegativeInfinity, true, 2147483647.0, true)
			{
				ConstratintId = "1809"
			});
			this.RegisterConstraint(10528, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(0, true, double.NegativeInfinity, true, 2147483647.0, true)
			{
				ConstratintId = "1813"
			});
			this.RegisterConstraint(10440, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueSetConstraint(0, false, new string[] { "INF", "-INF", "NaN" })
			{
				ConstratintId = "1815"
			});
			this.RegisterConstraint(10440, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(0, true, 0.0, true, double.PositiveInfinity, true)
			{
				ConstratintId = "1816"
			});
			this.RegisterConstraint(10357, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(0, true, double.NegativeInfinity, true, 2147483647.0, true)
			{
				ConstratintId = "1822"
			});
			this.RegisterConstraint(10358, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(0, true, double.NegativeInfinity, true, 2147483647.0, true)
			{
				ConstratintId = "1841"
			});
			this.RegisterConstraint(10518, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(3, true, 0.0, true, 49.0, false)
			{
				ConstratintId = "1845"
			});
			this.RegisterConstraint(10518, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(5, true, 0.0, true, 49.0, false)
			{
				ConstratintId = "1846"
			});
			this.RegisterConstraint(10518, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(4, true, 0.0, true, 49.0, false)
			{
				ConstratintId = "1847"
			});
			this.RegisterConstraint(10518, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(0, true, 0.0, true, 49.0, false)
			{
				ConstratintId = "1848"
			});
			this.RegisterConstraint(10518, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(1, true, 0.0, true, 49.0, false)
			{
				ConstratintId = "1849"
			});
			this.RegisterConstraint(10518, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(2, true, 0.0, true, 49.0, false)
			{
				ConstratintId = "1850"
			});
			this.RegisterConstraint(10519, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(8, true, double.NegativeInfinity, true, 2147483647.0, true)
			{
				ConstratintId = "1852"
			});
			this.RegisterConstraint(10393, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(0, true, double.NegativeInfinity, true, 2147483647.0, true)
			{
				ConstratintId = "1866"
			});
			this.RegisterConstraint(10399, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(0, true, 0.0, true, 2147483647.0, true)
			{
				ConstratintId = "1868"
			});
			this.RegisterConstraint(10392, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(0, true, double.NegativeInfinity, true, 2147483647.0, true)
			{
				ConstratintId = "1871"
			});
			this.RegisterConstraint(10427, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(0, true, double.NegativeInfinity, true, 2147483647.0, true)
			{
				ConstratintId = "1876"
			});
			this.RegisterConstraint(10464, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueSetConstraint(0, false, new string[] { "INF", "-INF", "NaN" })
			{
				ConstratintId = "1905"
			});
			this.RegisterConstraint(10591, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(0, 0, 256)
			{
				ConstratintId = "1927"
			});
			this.RegisterConstraint(10590, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(0, 0, 256)
			{
				ConstratintId = "1929"
			});
			this.RegisterConstraint(10592, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(0, 0, 256)
			{
				ConstratintId = "1930"
			});
			this.RegisterConstraint(10588, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(0, 0, 256)
			{
				ConstratintId = "1936"
			});
			this.RegisterConstraint(10658, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(0, true, true, this)
			{
				ConstratintId = "1942"
			});
			this.RegisterConstraint(10660, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(0, true, true, this)
			{
				ConstratintId = "1943"
			});
			this.RegisterConstraint(10656, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(0, true, true, this)
			{
				ConstratintId = "1945"
			});
			this.RegisterConstraint(10659, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(0, true, true, this)
			{
				ConstratintId = "1947"
			});
			this.RegisterConstraint(10657, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(0, true, true, this)
			{
				ConstratintId = "1953"
			});
			this.RegisterConstraint(10640, 10642, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(0, true, false, this)
			{
				ConstratintId = "1961"
			});
			this.RegisterConstraint(10640, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(6, true, true, this)
			{
				ConstratintId = "1963"
			});
			this.RegisterConstraint(10616, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueSetConstraint(1, true, new string[] { "12.0" })
			{
				ConstratintId = "1983"
			});
			this.RegisterConstraint(12519, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(40, true, true, this)
			{
				ConstratintId = "1994"
			});
			this.RegisterConstraint(12518, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(0, true, true, this)
			{
				ConstratintId = "2025"
			});
			this.RegisterConstraint(12520, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueSetConstraint(40, true, new string[] { "0" })
			{
				ConstratintId = "2028"
			});
			this.RegisterConstraint(12520, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(0, true, true, this)
			{
				ConstratintId = "2035"
			});
			this.RegisterConstraint(12509, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValuePatternConstraint(16, "-?(\\d{1,2}|100)%")
			{
				ConstratintId = "2040"
			});
			this.RegisterConstraint(12509, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(0, false, true, this)
			{
				ConstratintId = "2045"
			});
			this.RegisterConstraint(12509, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(14, true, -32767.0, true, 32767.0, true)
			{
				ConstratintId = "2052"
			});
			this.RegisterConstraint(12509, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(10, true, -32767.0, true, 32767.0, true)
			{
				ConstratintId = "2053"
			});
			this.RegisterConstraint(12509, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueSetConstraint(21, false, new string[] { "slashes", "colons" })
			{
				ConstratintId = "2056"
			});
			this.RegisterConstraint(12517, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueSetConstraint(26, true, new string[] { "0", "1", "2", "3" })
			{
				ConstratintId = "2059"
			});
			this.RegisterConstraint(12517, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueSetConstraint(28, true, new string[] { "0", "1", "2", "3" })
			{
				ConstratintId = "2060"
			});
			this.RegisterConstraint(12517, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(0, true, true, this)
			{
				ConstratintId = "2062"
			});
			this.RegisterConstraint(12521, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueSetConstraint(29, true, new string[] { "0", "1", "2", "3" })
			{
				ConstratintId = "2073"
			});
			this.RegisterConstraint(12521, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueSetConstraint(31, true, new string[] { "0", "1", "2", "3" })
			{
				ConstratintId = "2074"
			});
			this.RegisterConstraint(12521, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(0, true, true, this)
			{
				ConstratintId = "2076"
			});
			this.RegisterConstraint(12521, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueSetConstraint(39, true, new string[] { "75" })
			{
				ConstratintId = "2081"
			});
			this.RegisterConstraint(12514, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(0, false, true, this)
			{
				ConstratintId = "2094"
			});
			this.RegisterConstraint(12514, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(7, true, -0.5, true, 0.5, true)
			{
				ConstratintId = "2108"
			});
			this.RegisterConstraint(12522, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueSetConstraint(30, true, new string[] { "0", "1", "2", "3" })
			{
				ConstratintId = "2110"
			});
			this.RegisterConstraint(12522, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueSetConstraint(40, true, new string[] { "20" })
			{
				ConstratintId = "2116"
			});
			this.RegisterConstraint(12523, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueSetConstraint(30, true, new string[] { "0", "1", "2", "3" })
			{
				ConstratintId = "2122"
			});
			this.RegisterConstraint(12523, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueSetConstraint(32, true, new string[] { "0", "1", "2", "3" })
			{
				ConstratintId = "2123"
			});
			this.RegisterConstraint(12523, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(0, true, true, this)
			{
				ConstratintId = "2125"
			});
			this.RegisterConstraint(12523, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueSetConstraint(40, true, new string[] { "3" })
			{
				ConstratintId = "2129"
			});
			this.RegisterConstraint(12506, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(0, true, true, this)
			{
				ConstratintId = "2134"
			});
			this.RegisterConstraint(12524, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueSetConstraint(30, true, new string[] { "0", "1", "2", "3" })
			{
				ConstratintId = "2140"
			});
			this.RegisterConstraint(12524, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueSetConstraint(32, true, new string[] { "0", "1", "2", "3" })
			{
				ConstratintId = "2141"
			});
			this.RegisterConstraint(12524, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueSetConstraint(40, true, new string[] { "0" })
			{
				ConstratintId = "2146"
			});
			this.RegisterConstraint(12525, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueSetConstraint(30, true, new string[] { "0", "1", "2", "3" })
			{
				ConstratintId = "2150"
			});
			this.RegisterConstraint(12525, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueSetConstraint(32, true, new string[] { "0", "1", "2", "3" })
			{
				ConstratintId = "2151"
			});
			this.RegisterConstraint(12525, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(0, true, true, this)
			{
				ConstratintId = "2154"
			});
			this.RegisterConstraint(12525, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueSetConstraint(40, true, new string[] { "1" })
			{
				ConstratintId = "2159"
			});
			this.RegisterConstraint(12526, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueSetConstraint(29, true, new string[] { "0", "1", "2", "3" })
			{
				ConstratintId = "2164"
			});
			this.RegisterConstraint(12526, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueSetConstraint(31, true, new string[] { "0", "1", "2", "3" })
			{
				ConstratintId = "2165"
			});
			this.RegisterConstraint(12526, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(0, true, true, this)
			{
				ConstratintId = "2168"
			});
			this.RegisterConstraint(12526, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueSetConstraint(39, true, new string[] { "2" })
			{
				ConstratintId = "2173"
			});
			this.RegisterConstraint(12511, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(0, true, true, this)
			{
				ConstratintId = "2178"
			});
			this.RegisterConstraint(12515, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueSetConstraint(30, true, new string[] { "0", "1", "2", "3" })
			{
				ConstratintId = "2187"
			});
			this.RegisterConstraint(12515, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueSetConstraint(32, true, new string[] { "0", "1", "2", "3" })
			{
				ConstratintId = "2188"
			});
			this.RegisterConstraint(12515, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(0, true, true, this)
			{
				ConstratintId = "2191"
			});
			this.RegisterConstraint(12516, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueSetConstraint(30, true, new string[] { "0", "1", "2", "3" })
			{
				ConstratintId = "2200"
			});
			this.RegisterConstraint(12516, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueSetConstraint(32, true, new string[] { "0", "1", "2", "3" })
			{
				ConstratintId = "2201"
			});
			this.RegisterConstraint(12516, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(0, true, true, this)
			{
				ConstratintId = "2204"
			});
			this.RegisterConstraint(12510, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(0, false, true, this)
			{
				ConstratintId = "2215"
			});
			this.RegisterConstraint(12510, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(6, true, double.NegativeInfinity, true, 32767.0, true)
			{
				ConstratintId = "2221"
			});
			this.RegisterConstraint(12510, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(2, true, 0.0, true, 20116800.0, true)
			{
				ConstratintId = "2222"
			});
			this.RegisterConstraint(12512, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(0, true, true, this)
			{
				ConstratintId = "2226"
			});
			this.RegisterConstraint(12513, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(0, true, true, this)
			{
				ConstratintId = "2233"
			});
			this.RegisterConstraint(12413, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(2, true, 0.0, true, 20116800.0, true)
			{
				ConstratintId = "2243"
			});
			this.RegisterConstraint(12413, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(7, true, double.NegativeInfinity, true, 32767.0, true)
			{
				ConstratintId = "2244"
			});
			this.RegisterConstraint(12406, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueSetConstraint(2, true, new string[] { "rightAngle", "oneSegment", "twoSegment", "threeSegment" })
			{
				ConstratintId = "2245"
			});
			this.RegisterConstraint(12414, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueSetConstraint(10, true, new string[] { "solid", "shortdash", "shortdot", "shortdashdot", "shortdashdotdot", "dot", "dash", "longdash", "longdashdotdot", "dashdot" })
			{
				ConstratintId = "2251"
			});
			this.RegisterConstraint(12405, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(22, true, 1.0, true, 65536.0, true)
			{
				ConstratintId = "2257"
			});
			this.RegisterConstraint(12405, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValuePatternConstraint(21, "(\\d{1,5}|1[0-6][0-8]\\d{3}|1690[0-8]\\d|16909[0-3])pt")
			{
				ConstratintId = "2261"
			});
			this.RegisterConstraint(12405, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(11, true, -32767.0, true, 32767.0, true)
			{
				ConstratintId = "2262"
			});
			this.RegisterConstraint(12405, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(6, true, -32767.0, true, 32767.0, true)
			{
				ConstratintId = "2266"
			});
			this.RegisterConstraint(12408, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValuePatternConstraint(4, "_(\\d{1,9}|1\\d{9}|20\\d{8}|21[0-3]\\d{7}|214[0-6]\\d{6}|2147[0-3]\\d{5}|21474[0-7]\\d{4}|214748[0-2]\\d{3}|2147483[0-5]\\d{2}|21474836[0-3]\\d|214748364[0-7])")
			{
				ConstratintId = "2276"
			});
			this.RegisterConstraint(12408, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new ReferenceExistConstraint(2, ".", 12515, "DocumentFormat.OpenXml.Vml.Shape", 0)
			{
				ConstratintId = "2278"
			});
			this.RegisterConstraint(12401, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(2, true, true, this)
			{
				ConstratintId = "2304"
			});
			this.RegisterConstraint(10838, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(1, true, 2.0, true, double.PositiveInfinity, true)
			{
				ConstratintId = "2371"
			});
			this.RegisterConstraint(10838, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(2, false, true, this)
			{
				ConstratintId = "2377"
			});
			this.RegisterConstraint(10995, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(0, true, true, this)
			{
				ConstratintId = "2389"
			});
			this.RegisterConstraint(11885, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(0, true, true, this)
			{
				ConstratintId = "2403"
			});
			this.RegisterConstraint(11886, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Word, new AttributeValueLengthConstraint(0, 0, 32)
			{
				ConstratintId = "2418"
			});
			this.RegisterConstraint(11884, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(0, true, true, this)
			{
				ConstratintId = "2420"
			});
			this.RegisterConstraint(11492, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Word, new AttributeValueLengthConstraint(0, 0, 253)
			{
				ConstratintId = "2426"
			});
			this.RegisterConstraint(11525, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new RelationshipTypeConstraint(1, "http://schemas.openxmlformats.org/officeDocument/2006/relationships/footer")
			{
				ConstratintId = "2431"
			});
			this.RegisterConstraint(11524, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new RelationshipTypeConstraint(1, "http://schemas.openxmlformats.org/officeDocument/2006/relationships/header")
			{
				ConstratintId = "2433"
			});
			this.RegisterConstraint(11795, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new ReferenceExistConstraint(0, "/MainDocumentPart/EndnotesPart", 11938, "DocumentFormat.OpenXml.Wordprocessing.Endnote", 1)
			{
				ConstratintId = "2436"
			});
			this.RegisterConstraint(11794, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new ReferenceExistConstraint(0, "/MainDocumentPart/FootnotesPart", 11937, "DocumentFormat.OpenXml.Wordprocessing.Footnote", 1)
			{
				ConstratintId = "2441"
			});
			this.RegisterConstraint(11954, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(0, false, true, this)
			{
				ConstratintId = "2447"
			});
			this.RegisterConstraint(11936, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(3, true, true, this)
			{
				ConstratintId = "2450"
			});
			this.RegisterConstraint(11479, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(1, true, true, this)
			{
				ConstratintId = "2454"
			});
			this.RegisterConstraint(11479, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new ReferenceExistConstraint(1, "WordprocessingCommentsPart", 11936, "DocumentFormat.OpenXml.Wordprocessing.Comment", 3)
			{
				ConstratintId = "2454"
			});
			this.RegisterConstraint(11478, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(1, true, true, this)
			{
				ConstratintId = "2455"
			});
			this.RegisterConstraint(11478, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new ReferenceExistConstraint(1, "WordprocessingCommentsPart", 11936, "DocumentFormat.OpenXml.Wordprocessing.Comment", 3)
			{
				ConstratintId = "2455"
			});
			this.RegisterConstraint(11571, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(0, true, true, this)
			{
				ConstratintId = "2457"
			});
			this.RegisterConstraint(11571, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new ReferenceExistConstraint(0, "WordprocessingCommentsPart", 11936, "DocumentFormat.OpenXml.Wordprocessing.Comment", 3)
			{
				ConstratintId = "2457"
			});
			this.RegisterConstraint(11474, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(2, true, true, this)
			{
				ConstratintId = "2459"
			});
			this.RegisterConstraint(11473, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(2, true, true, this)
			{
				ConstratintId = "2461"
			});
			this.RegisterConstraint(11475, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(4, true, true, this)
			{
				ConstratintId = "2462"
			});
			this.RegisterConstraint(11487, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(0, false, true, this)
			{
				ConstratintId = "2463"
			});
			this.RegisterConstraint(11486, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(2, false, true, this)
			{
				ConstratintId = "2464"
			});
			this.RegisterConstraint(11485, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(0, true, true, this)
			{
				ConstratintId = "2468"
			});
			this.RegisterConstraint(11484, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(2, true, true, this)
			{
				ConstratintId = "2469"
			});
			this.RegisterConstraint(11489, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(0, true, true, this)
			{
				ConstratintId = "2473"
			});
			this.RegisterConstraint(11488, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(2, true, true, this)
			{
				ConstratintId = "2476"
			});
			this.RegisterConstraint(11490, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(2, true, true, this)
			{
				ConstratintId = "2493"
			});
			this.RegisterConstraint(11628, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(2, true, true, this)
			{
				ConstratintId = "2530"
			});
			this.RegisterConstraint(11481, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(1, true, true, this)
			{
				ConstratintId = "2540"
			});
			this.RegisterConstraint(11480, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(6, true, true, this)
			{
				ConstratintId = "2548"
			});
			this.RegisterConstraint(11629, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(2, true, true, this)
			{
				ConstratintId = "2557"
			});
			this.RegisterConstraint(11483, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(1, true, true, this)
			{
				ConstratintId = "2562"
			});
			this.RegisterConstraint(11482, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(6, false, true, this)
			{
				ConstratintId = "2571"
			});
			this.RegisterConstraint(11714, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(3, true, true, this)
			{
				ConstratintId = "2574"
			});
			this.RegisterConstraint(11714, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(0, 0, 15)
			{
				ConstratintId = "2575"
			});
			this.RegisterConstraint(11523, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(2, true, true, this)
			{
				ConstratintId = "2583"
			});
			this.RegisterConstraint(11748, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(2, true, true, this)
			{
				ConstratintId = "2592"
			});
			this.RegisterConstraint(11782, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(0, true, true, this)
			{
				ConstratintId = "2593"
			});
			this.RegisterConstraint(11787, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(2, true, true, this)
			{
				ConstratintId = "2596"
			});
			this.RegisterConstraint(11788, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(2, true, true, this)
			{
				ConstratintId = "2599"
			});
			this.RegisterConstraint(11783, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(2, true, true, this)
			{
				ConstratintId = "2602"
			});
			this.RegisterConstraint(12133, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(2, true, true, this)
			{
				ConstratintId = "2605"
			});
			this.RegisterConstraint(11477, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(1, true, true, this)
			{
				ConstratintId = "2608"
			});
			this.RegisterConstraint(11476, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(4, true, true, this)
			{
				ConstratintId = "2614"
			});
			this.RegisterConstraint(11625, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(0, true, true, this)
			{
				ConstratintId = "2615"
			});
			this.RegisterConstraint(11624, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(4, true, true, this)
			{
				ConstratintId = "2619"
			});
			this.RegisterConstraint(11817, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new RelationshipTypeConstraint(0, "http://schemas.openxmlformats.org/officeDocument/2006/relationships/mailMergeSource")
			{
				ConstratintId = "2624"
			});
			this.RegisterConstraint(11811, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new RelationshipTypeConstraint(0, "http://schemas.openxmlformats.org/officeDocument/2006/relationships/recipientData")
			{
				ConstratintId = "2628"
			});
			this.RegisterConstraint(11806, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new RelationshipTypeConstraint(0, "http://schemas.openxmlformats.org/officeDocument/2006/relationships/mailMergeSource")
			{
				ConstratintId = "2630"
			});
			this.RegisterConstraint(11982, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new RelationshipTypeConstraint(0, "http://schemas.openxmlformats.org/officeDocument/2006/relationships/attachedTemplate")
			{
				ConstratintId = "2636"
			});
			this.RegisterConstraint(11942, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(0, true, true, this)
			{
				ConstratintId = "2639"
			});
			this.RegisterConstraint(11992, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueSetConstraint(6, true, new string[] { "1", "2", "3", "4", "12", "13", "14" })
			{
				ConstratintId = "2653"
			});
			this.RegisterConstraint(12031, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new RelationshipTypeConstraint(0, "http://schemas.openxmlformats.org/officeDocument/2006/relationships/transform")
			{
				ConstratintId = "2660"
			});
			this.RegisterConstraint(11984, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Word, new AttributeValueSetConstraint(0, false, new string[] { "0x0040", "0x0080", "0x0800" })
			{
				ConstratintId = "2666"
			});
			this.RegisterConstraint(11935, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(0, true, true, this)
			{
				ConstratintId = "2668"
			});
			this.RegisterConstraint(11850, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new RelationshipTypeConstraint(0, "http://schemas.openxmlformats.org/officeDocument/2006/relationships/frame")
			{
				ConstratintId = "2674"
			});
			this.RegisterConstraint(11648, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new RelationshipTypeConstraint(0, "http://schemas.openxmlformats.org/officeDocument/2006/relationships/subDocument")
			{
				ConstratintId = "2695"
			});
			this.RegisterConstraint(11522, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(0, true, 0.0, true, 9.0, true)
			{
				ConstratintId = "2740"
			});
			this.RegisterConstraint(12160, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(0, true, true, this)
			{
				ConstratintId = "2791"
			});
			this.RegisterConstraint(12128, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Word, new AttributeCannotOmitConstraint(1)
			{
				ConstratintId = "2802"
			});
			this.RegisterConstraint(12128, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Word, new AttributeCannotOmitConstraint(0)
			{
				ConstratintId = "2803"
			});
			this.RegisterConstraint(11660, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Word, new AttributeValueSetConstraint(0, false, new string[] { "0" })
			{
				ConstratintId = "2805"
			});
			this.RegisterConstraint(11780, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Word, new AttributeValueSetConstraint(0, true, new string[] { "urn:schemas-microsoft-com:office:office" })
			{
				ConstratintId = "2837"
			});
			this.RegisterConstraint(11542, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new RelationshipTypeConstraint(0, "http://schemas.openxmlformats.org/officeDocument/2006/relationships/printerSettings")
			{
				ConstratintId = "2877"
			});
			this.RegisterConstraint(11801, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Word, new AttributeValuePatternConstraint(0, "[^,]*")
			{
				ConstratintId = "2898"
			});
			this.RegisterConstraint(11914, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(1, true, true, this)
			{
				ConstratintId = "2902"
			});
			this.RegisterConstraint(11925, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new RelationshipTypeConstraint(2, "http://schemas.openxmlformats.org/officeDocument/2006/relationships/font")
			{
				ConstratintId = "2927"
			});
			this.RegisterConstraint(11924, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new RelationshipTypeConstraint(2, "http://schemas.openxmlformats.org/officeDocument/2006/relationships/font")
			{
				ConstratintId = "2928"
			});
			this.RegisterConstraint(11922, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new RelationshipTypeConstraint(2, "http://schemas.openxmlformats.org/officeDocument/2006/relationships/font")
			{
				ConstratintId = "2929"
			});
			this.RegisterConstraint(12398, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Word, new AttributeValueLengthConstraint(1, 0, 2083)
			{
				ConstratintId = "2936"
			});
			this.RegisterConstraint(12398, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Word, new AttributeValueLengthConstraint(2, 0, 2083)
			{
				ConstratintId = "2937"
			});
			this.RegisterConstraint(12398, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Word, new AttributeValueLengthConstraint(0, 0, 255)
			{
				ConstratintId = "2939"
			});
			this.RegisterConstraint(11347, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(0, 1, int.MaxValue)
			{
				ConstratintId = "2957"
			});
			this.RegisterConstraint(12353, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Word | ApplicationType.Excel, new AttributeValueSetConstraint(3, true, new string[]
			{
				"1", "2", "3", "4", "5", "6", "7", "8", "9", "10",
				"11", "12", "13", "14"
			})
			{
				ConstratintId = "2974"
			});
			this.RegisterConstraint(11171, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeMutualExclusive(new byte[] { 8, 10 })
			{
				ConstratintId = "2975"
			});
			this.RegisterConstraint(11687, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(2, true, true, this)
			{
				ConstratintId = "50002"
			});
			this.RegisterConstraint(11615, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(2, true, true, this)
			{
				ConstratintId = "50002"
			});
			this.RegisterConstraint(11627, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(2, true, true, this)
			{
				ConstratintId = "50002"
			});
			this.RegisterConstraint(11686, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(2, true, true, this)
			{
				ConstratintId = "50003"
			});
			this.RegisterConstraint(11614, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(2, true, true, this)
			{
				ConstratintId = "50003"
			});
			this.RegisterConstraint(11626, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(2, true, true, this)
			{
				ConstratintId = "50003"
			});
			this.RegisterConstraint(11612, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(2, true, true, this)
			{
				ConstratintId = "50004"
			});
			this.RegisterConstraint(11749, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(2, true, true, this)
			{
				ConstratintId = "50004"
			});
			this.RegisterConstraint(12251, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(0, true, true, this)
			{
				ConstratintId = "62008"
			});
			this.RegisterConstraint(12286, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new RelationshipExistConstraint(1)
			{
				ConstratintId = "63001"
			});
			this.RegisterConstraint(12251, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new RelationshipExistConstraint(1)
			{
				ConstratintId = "63002"
			});
			this.RegisterConstraint(11302, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new RelationshipExistConstraint(3)
			{
				ConstratintId = "63003"
			});
			this.RegisterConstraint(12252, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new RelationshipExistConstraint(1)
			{
				ConstratintId = "63004"
			});
			this.RegisterConstraint(12510, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new RelationshipExistConstraint(26)
			{
				ConstratintId = "63005"
			});
			this.RegisterConstraint(10172, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new RelationshipExistConstraint(0)
			{
				ConstratintId = "63006"
			});
			this.RegisterConstraint(10388, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new RelationshipExistConstraint(0)
			{
				ConstratintId = "63007"
			});
			this.RegisterConstraint(11203, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new RelationshipExistConstraint(1)
			{
				ConstratintId = "63008"
			});
			this.RegisterConstraint(11374, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new RelationshipExistConstraint(0)
			{
				ConstratintId = "63009"
			});
			this.RegisterConstraint(11982, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new RelationshipExistConstraint(0)
			{
				ConstratintId = "63010"
			});
			this.RegisterConstraint(12509, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new RelationshipExistConstraint(25)
			{
				ConstratintId = "63011"
			});
			this.RegisterConstraint(12175, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new RelationshipExistConstraint(3)
			{
				ConstratintId = "63012"
			});
			this.RegisterConstraint(10579, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new RelationshipExistConstraint(0)
			{
				ConstratintId = "63013"
			});
			this.RegisterConstraint(11298, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new RelationshipExistConstraint(1)
			{
				ConstratintId = "63014"
			});
			this.RegisterConstraint(12253, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new RelationshipExistConstraint(0)
			{
				ConstratintId = "63015"
			});
			this.RegisterConstraint(12514, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new RelationshipExistConstraint(16)
			{
				ConstratintId = "63016"
			});
			this.RegisterConstraint(11525, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new RelationshipExistConstraint(1)
			{
				ConstratintId = "63017"
			});
			this.RegisterConstraint(11524, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new RelationshipExistConstraint(1)
			{
				ConstratintId = "63018"
			});
			this.RegisterConstraint(11620, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new RelationshipExistConstraint(5)
			{
				ConstratintId = "63019"
			});
			this.RegisterConstraint(11645, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new RelationshipExistConstraint(5)
			{
				ConstratintId = "63019"
			});
			this.RegisterConstraint(12254, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new RelationshipExistConstraint(0)
			{
				ConstratintId = "63020"
			});
			this.RegisterConstraint(11375, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new RelationshipExistConstraint(0)
			{
				ConstratintId = "63021"
			});
			this.RegisterConstraint(11922, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new RelationshipExistConstraint(2)
			{
				ConstratintId = "63023"
			});
			this.RegisterConstraint(12244, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new RelationshipExistConstraint(0)
			{
				ConstratintId = "63024"
			});
			this.RegisterConstraint(10326, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new RelationshipExistConstraint(0)
			{
				ConstratintId = "63025"
			});
			this.RegisterConstraint(11438, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new RelationshipExistConstraint(3)
			{
				ConstratintId = "63026"
			});
			this.RegisterConstraint(10651, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new RelationshipExistConstraint(2)
			{
				ConstratintId = "63029"
			});
			this.RegisterConstraint(10121, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new RelationshipExistConstraint(0)
			{
				ConstratintId = "63030"
			});
			this.RegisterConstraint(10121, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new RelationshipExistConstraint(1)
			{
				ConstratintId = "63031"
			});
			this.RegisterConstraint(10623, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new RelationshipExistConstraint(0)
			{
				ConstratintId = "63032"
			});
			this.RegisterConstraint(12514, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new RelationshipExistConstraint(18)
			{
				ConstratintId = "63033"
			});
			this.RegisterConstraint(11684, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new RelationshipExistConstraint(0)
			{
				ConstratintId = "63035"
			});
			this.RegisterConstraint(11301, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueSetConstraint(1, false, new string[] { "00000000-0000-0000-0000-000000000000" })
			{
				ConstratintId = "134"
			});
			this.RegisterConstraint(11301, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueRangeConstraint(17, true, double.NegativeInfinity, true, 2147483647.0, true)
			{
				ConstratintId = "136"
			});
			this.RegisterConstraint(11305, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueLengthConstraint(1, 0, 255)
			{
				ConstratintId = "139"
			});
			this.RegisterConstraint(11305, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueLengthConstraint(0, 1, 255)
			{
				ConstratintId = "140"
			});
			this.RegisterConstraint(11305, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueRangeConstraint(6, true, double.NegativeInfinity, true, 32766.0, true)
			{
				ConstratintId = "143"
			});
			this.RegisterConstraint(11306, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueLengthConstraint(0, 0, 32)
			{
				ConstratintId = "166"
			});
			this.RegisterConstraint(11303, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueLengthConstraint(0, 0, 2083)
			{
				ConstratintId = "179"
			});
			this.RegisterConstraint(11303, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueLengthConstraint(2, 0, 2083)
			{
				ConstratintId = "181"
			});
			this.RegisterConstraint(11303, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueLengthConstraint(1, 0, 255)
			{
				ConstratintId = "182"
			});
			this.RegisterConstraint(11299, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueLengthConstraint(4, 0, 255)
			{
				ConstratintId = "187"
			});
			this.RegisterConstraint(11299, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueLengthConstraint(2, 0, 255)
			{
				ConstratintId = "188"
			});
			this.RegisterConstraint(11299, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueLengthConstraint(3, 1, 255)
			{
				ConstratintId = "190"
			});
			this.RegisterConstraint(11299, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueLengthConstraint(1, 1, 255)
			{
				ConstratintId = "191"
			});
			this.RegisterConstraint(11299, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueRangeConstraint(0, true, 1.0, true, 2147483647.0, true)
			{
				ConstratintId = "192"
			});
			this.RegisterConstraint(11192, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueRangeConstraint(1, true, double.NegativeInfinity, true, 1048576.0, true)
			{
				ConstratintId = "220"
			});
			this.RegisterConstraint(11192, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueRangeConstraint(0, true, 1.0, true, 1048576.0, true)
			{
				ConstratintId = "221"
			});
			this.RegisterConstraint(11192, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueRangeConstraint(2, true, 1.0, true, 1048576.0, true)
			{
				ConstratintId = "222"
			});
			this.RegisterConstraint(11145, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueRangeConstraint(4, true, -1.0, true, 1.0, true)
			{
				ConstratintId = "244"
			});
			this.RegisterConstraint(11145, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueRangeConstraint(1, true, double.NegativeInfinity, true, 255.0, true)
			{
				ConstratintId = "245"
			});
			this.RegisterConstraint(11145, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueRangeConstraint(3, true, 0.0, true, 255.0, true)
			{
				ConstratintId = "246"
			});
			this.RegisterConstraint(11215, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new UniqueAttributeValueConstraint(2, true, true, this)
			{
				ConstratintId = "253"
			});
			this.RegisterConstraint(11215, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueLengthConstraint(2, 0, 32)
			{
				ConstratintId = "254"
			});
			this.RegisterConstraint(11381, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueRangeConstraint(1, true, double.NegativeInfinity, true, 100.0, true)
			{
				ConstratintId = "267"
			});
			this.RegisterConstraint(11381, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueRangeConstraint(0, true, double.NegativeInfinity, true, 100.0, true)
			{
				ConstratintId = "268"
			});
			this.RegisterConstraint(11400, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueRangeConstraint(1, true, double.NegativeInfinity, true, 65535.0, true)
			{
				ConstratintId = "274"
			});
			this.RegisterConstraint(11400, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueRangeConstraint(2, true, double.NegativeInfinity, true, 65535.0, true)
			{
				ConstratintId = "275"
			});
			this.RegisterConstraint(11400, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueRangeConstraint(3, true, double.NegativeInfinity, true, 65535.0, true)
			{
				ConstratintId = "276"
			});
			this.RegisterConstraint(11214, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueLengthConstraint(3, 0, 255)
			{
				ConstratintId = "296"
			});
			this.RegisterConstraint(11200, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueRangeConstraint(3, true, double.NegativeInfinity, true, 32767.0, true)
			{
				ConstratintId = "317"
			});
			this.RegisterConstraint(11200, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueRangeConstraint(4, true, double.NegativeInfinity, true, 32767.0, true)
			{
				ConstratintId = "318"
			});
			this.RegisterConstraint(11200, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueRangeConstraint(15, true, 1.0, true, 32767.0, true)
			{
				ConstratintId = "321"
			});
			this.RegisterConstraint(11208, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueRangeConstraint(9, true, 1.0, true, 32767.0, true)
			{
				ConstratintId = "321"
			});
			this.RegisterConstraint(11200, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueRangeConstraint(13, true, 1.0, true, double.PositiveInfinity, true)
			{
				ConstratintId = "322"
			});
			this.RegisterConstraint(11208, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueRangeConstraint(7, true, 1.0, true, double.PositiveInfinity, true)
			{
				ConstratintId = "322"
			});
			this.RegisterConstraint(11200, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueRangeConstraint(14, true, 1.0, true, double.PositiveInfinity, true)
			{
				ConstratintId = "323"
			});
			this.RegisterConstraint(11208, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueRangeConstraint(8, true, 1.0, true, double.PositiveInfinity, true)
			{
				ConstratintId = "323"
			});
			this.RegisterConstraint(11191, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueRangeConstraint(14, true, double.NegativeInfinity, true, 16383.0, true)
			{
				ConstratintId = "330"
			});
			this.RegisterConstraint(11191, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueRangeConstraint(13, true, double.NegativeInfinity, true, 1048575.0, true)
			{
				ConstratintId = "332"
			});
			this.RegisterConstraint(11205, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueRangeConstraint(3, true, 1.0, true, 32.0, true)
			{
				ConstratintId = "354"
			});
			this.RegisterConstraint(11205, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueLengthConstraint(0, 0, 255)
			{
				ConstratintId = "356"
			});
			this.RegisterConstraint(11205, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueLengthConstraint(4, 1, 54)
			{
				ConstratintId = "358"
			});
			this.RegisterConstraint(11205, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueLengthConstraint(5, 0, 255)
			{
				ConstratintId = "359"
			});
			this.RegisterConstraint(11190, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueRangeConstraint(1, true, double.NegativeInfinity, true, 8191.0, true)
			{
				ConstratintId = "362"
			});
			this.RegisterConstraint(11046, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueRangeConstraint(0, true, 1.0, true, 4294967294.0, true)
			{
				ConstratintId = "416"
			});
			this.RegisterConstraint(11046, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueLengthConstraint(1, 0, 255)
			{
				ConstratintId = "417"
			});
			this.RegisterConstraint(11046, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueLengthConstraint(3, 0, 255)
			{
				ConstratintId = "421"
			});
			this.RegisterConstraint(11046, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueLengthConstraint(19, 1, 255)
			{
				ConstratintId = "423"
			});
			this.RegisterConstraint(11046, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueLengthConstraint(18, 1, 255)
			{
				ConstratintId = "424"
			});
			this.RegisterConstraint(11046, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueLengthConstraint(20, 1, 255)
			{
				ConstratintId = "425"
			});
			this.RegisterConstraint(11046, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueRangeConstraint(21, true, double.NegativeInfinity, true, 2147483647.0, true)
			{
				ConstratintId = "426"
			});
			this.RegisterConstraint(11289, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueLengthConstraint(1, 0, 255)
			{
				ConstratintId = "428"
			});
			this.RegisterConstraint(11345, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueLengthConstraint(0, 1, 255)
			{
				ConstratintId = "441"
			});
			this.RegisterConstraint(11292, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueRangeConstraint(0, true, 1.0, true, 21474836477.0, true)
			{
				ConstratintId = "443"
			});
			this.RegisterConstraint(11248, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueLengthConstraint(1, 1, 255)
			{
				ConstratintId = "446"
			});
			this.RegisterConstraint(11053, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueRangeConstraint(1, true, 1.0, true, 65534.0, true)
			{
				ConstratintId = "455"
			});
			this.RegisterConstraint(11256, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueRangeConstraint(4, true, double.NegativeInfinity, true, 255.0, true)
			{
				ConstratintId = "460"
			});
			this.RegisterConstraint(11256, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueSetConstraint(8, true, new string[] { "0", "1", "2" })
			{
				ConstratintId = "461"
			});
			this.RegisterConstraint(11147, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueRangeConstraint(0, true, 0.0, true, 5.0, true)
			{
				ConstratintId = "483"
			});
			this.RegisterConstraint(11267, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueLengthConstraint(0, 1, 31)
			{
				ConstratintId = "494"
			});
			this.RegisterConstraint(11263, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueLengthConstraint(0, 1, 255)
			{
				ConstratintId = "499"
			});
			this.RegisterConstraint(11185, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueLessEqualToAnother(0, 1, true)
			{
				ConstratintId = "609"
			});
			this.RegisterConstraint(11077, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueLengthConstraint(3, 0, 65535)
			{
				ConstratintId = "768"
			});
			this.RegisterConstraint(11121, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueLengthConstraint(13, 0, 65535)
			{
				ConstratintId = "803"
			});
			this.RegisterConstraint(11121, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueLengthConstraint(14, 0, 65535)
			{
				ConstratintId = "804"
			});
			this.RegisterConstraint(11121, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueRangeConstraint(5, true, 0.0, true, 11.0, true)
			{
				ConstratintId = "819"
			});
			this.RegisterConstraint(11102, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueLengthConstraint(2, 1, 65535)
			{
				ConstratintId = "831"
			});
			this.RegisterConstraint(11102, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueLengthConstraint(3, 1, 65535)
			{
				ConstratintId = "832"
			});
			this.RegisterConstraint(11102, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueLengthConstraint(4, 1, 65535)
			{
				ConstratintId = "834"
			});
			this.RegisterConstraint(11080, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueLengthConstraint(3, 0, 65535)
			{
				ConstratintId = "845"
			});
			this.RegisterConstraint(11116, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueLengthConstraint(3, 1, 65535)
			{
				ConstratintId = "862"
			});
			this.RegisterConstraint(11116, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueLengthConstraint(1, 1, 65535)
			{
				ConstratintId = "863"
			});
			this.RegisterConstraint(11116, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueLengthConstraint(2, 1, 32767)
			{
				ConstratintId = "864"
			});
			this.RegisterConstraint(11078, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueLengthConstraint(3, 0, 65535)
			{
				ConstratintId = "868"
			});
			this.RegisterConstraint(11090, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueLengthConstraint(0, 1, 65535)
			{
				ConstratintId = "919"
			});
			this.RegisterConstraint(11104, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeRequiredConditionToValue(8, 1, new string[] { "data" })
			{
				ConstratintId = "927"
			});
			this.RegisterConstraint(11084, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueLengthConstraint(1, 1, 32767)
			{
				ConstratintId = "962"
			});
			this.RegisterConstraint(11084, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueLengthConstraint(2, 0, 65535)
			{
				ConstratintId = "963"
			});
			this.RegisterConstraint(11084, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueLengthConstraint(3, 0, 65535)
			{
				ConstratintId = "964"
			});
			this.RegisterConstraint(11084, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueLengthConstraint(4, 0, 32767)
			{
				ConstratintId = "965"
			});
			this.RegisterConstraint(11075, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueLengthConstraint(2, 0, 65535)
			{
				ConstratintId = "979"
			});
			this.RegisterConstraint(11117, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueLengthConstraint(1, 1, 65535)
			{
				ConstratintId = "996"
			});
			this.RegisterConstraint(11117, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueLengthConstraint(0, 1, 65535)
			{
				ConstratintId = "997"
			});
			this.RegisterConstraint(11076, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueLengthConstraint(3, 0, 65535)
			{
				ConstratintId = "1019"
			});
			this.RegisterConstraint(11076, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueSetConstraint(0, false, new string[] { "INF", "-INF", "NaN" })
			{
				ConstratintId = "1028"
			});
			this.RegisterConstraint(11033, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueLengthConstraint(11, 0, 255)
			{
				ConstratintId = "1185"
			});
			this.RegisterConstraint(11033, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueLengthConstraint(12, 0, 255)
			{
				ConstratintId = "1186"
			});
			this.RegisterConstraint(11033, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueLengthConstraint(13, 0, 255)
			{
				ConstratintId = "1187"
			});
			this.RegisterConstraint(11033, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueLengthConstraint(15, 0, 255)
			{
				ConstratintId = "1188"
			});
			this.RegisterConstraint(11033, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueLengthConstraint(17, 0, 255)
			{
				ConstratintId = "1189"
			});
			this.RegisterConstraint(11033, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueLengthConstraint(18, 0, 255)
			{
				ConstratintId = "1190"
			});
			this.RegisterConstraint(11033, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueLengthConstraint(19, 0, 255)
			{
				ConstratintId = "1191"
			});
			this.RegisterConstraint(11033, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueLengthConstraint(0, 0, 255)
			{
				ConstratintId = "1192"
			});
			this.RegisterConstraint(11033, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueLengthConstraint(20, 0, 255)
			{
				ConstratintId = "1193"
			});
			this.RegisterConstraint(11429, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueLengthConstraint(0, 0, 255)
			{
				ConstratintId = "1203"
			});
			this.RegisterConstraint(11079, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueLengthConstraint(3, 0, 65535)
			{
				ConstratintId = "1239"
			});
			this.RegisterConstraint(11097, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueLengthConstraint(2, 0, 65535)
			{
				ConstratintId = "1257"
			});
			this.RegisterConstraint(11097, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueRangeConstraint(1, true, 0.0, true, 1048576.0, true)
			{
				ConstratintId = "1258"
			});
			this.RegisterConstraint(11096, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeMutualExclusive(new byte[] { 0, 1 })
			{
				ConstratintId = "1291"
			});
			this.RegisterConstraint(11155, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueRangeConstraint(2, true, double.NegativeInfinity, true, 32767.0, true)
			{
				ConstratintId = "1323"
			});
			this.RegisterConstraint(11155, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueLengthConstraint(3, 1, 54)
			{
				ConstratintId = "1324"
			});
			this.RegisterConstraint(11155, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueRangeConstraint(5, true, double.NegativeInfinity, true, 2147483647.0, true)
			{
				ConstratintId = "1326"
			});
			this.RegisterConstraint(11173, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueSetConstraint(3, true, new string[] { "0" })
			{
				ConstratintId = "1335"
			});
			this.RegisterConstraint(11173, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueSetConstraint(4, true, new string[] { "0" })
			{
				ConstratintId = "1337"
			});
			this.RegisterConstraint(11173, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueSetConstraint(1, true, new string[] { "0" })
			{
				ConstratintId = "1338"
			});
			this.RegisterConstraint(11163, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueRangeConstraint(0, true, double.NegativeInfinity, true, 32767.0, true)
			{
				ConstratintId = "1349"
			});
			this.RegisterConstraint(11161, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueRangeConstraint(3, true, double.NegativeInfinity, true, 32767.0, true)
			{
				ConstratintId = "1353"
			});
			this.RegisterConstraint(11164, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueLengthConstraint(22, 0, 255)
			{
				ConstratintId = "1369"
			});
			this.RegisterConstraint(11164, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueLengthConstraint(23, 0, 255)
			{
				ConstratintId = "1370"
			});
			this.RegisterConstraint(11164, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueRangeConstraint(0, true, double.NegativeInfinity, true, 2147483647.0, true)
			{
				ConstratintId = "1373"
			});
			this.RegisterConstraint(11160, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueRangeConstraint(3, true, double.NegativeInfinity, true, 32767.0, true)
			{
				ConstratintId = "1380"
			});
			this.RegisterConstraint(11160, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueRangeConstraint(5, true, double.NegativeInfinity, true, 65533.0, true)
			{
				ConstratintId = "1381"
			});
			this.RegisterConstraint(11038, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueRangeConstraint(0, true, double.NegativeInfinity, true, 256.0, true)
			{
				ConstratintId = "1406"
			});
			this.RegisterConstraint(11132, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueLengthConstraint(0, 1, 255)
			{
				ConstratintId = "1407"
			});
			this.RegisterConstraint(11061, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueRangeConstraint(4, true, double.NegativeInfinity, true, 32767.0, true)
			{
				ConstratintId = "1432"
			});
			this.RegisterConstraint(11061, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueLengthConstraint(5, 1, 255)
			{
				ConstratintId = "1433"
			});
			this.RegisterConstraint(11061, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueLengthConstraint(6, 0, 255)
			{
				ConstratintId = "1435"
			});
			this.RegisterConstraint(11061, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueLengthConstraint(19, 0, 255)
			{
				ConstratintId = "1436"
			});
			this.RegisterConstraint(11061, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueLengthConstraint(2, 0, 255)
			{
				ConstratintId = "1437"
			});
			this.RegisterConstraint(11061, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueLengthConstraint(1, 0, 255)
			{
				ConstratintId = "1438"
			});
			this.RegisterConstraint(11456, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueLengthConstraint(0, 0, 65535)
			{
				ConstratintId = "1440"
			});
			this.RegisterConstraint(11459, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueRangeConstraint(3, true, double.NegativeInfinity, true, 2147483647.0, true)
			{
				ConstratintId = "1454"
			});
			this.RegisterConstraint(11459, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueLengthConstraint(4, 1, 218)
			{
				ConstratintId = "1458"
			});
			this.RegisterConstraint(11459, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeRequiredConditionToValue(4, 0, new string[] { "false" })
			{
				ConstratintId = "1459"
			});
			this.RegisterConstraint(11280, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueRangeConstraint(1, true, 1.0, true, 16384.0, true)
			{
				ConstratintId = "1488"
			});
			this.RegisterConstraint(11280, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueRangeConstraint(0, true, 1.0, true, 1048576.0, true)
			{
				ConstratintId = "1489"
			});
			this.RegisterConstraint(11294, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueLengthConstraint(0, 1, 255)
			{
				ConstratintId = "1491"
			});
			this.RegisterConstraint(11297, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueRangeConstraint(1, true, 1.0, true, 65534.0, true)
			{
				ConstratintId = "1494"
			});
			this.RegisterConstraint(11060, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueLengthConstraint(0, 0, 65535)
			{
				ConstratintId = "1499"
			});
			this.RegisterConstraint(11060, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueLengthConstraint(3, 0, 65535)
			{
				ConstratintId = "1500"
			});
			this.RegisterConstraint(11060, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueRangeConstraint(2, true, double.NegativeInfinity, true, 2147483647.0, true)
			{
				ConstratintId = "1502"
			});
			this.RegisterConstraint(11060, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeAbsentConditionToValue(3, 1, new string[] { "false" })
			{
				ConstratintId = "1503"
			});
			this.RegisterConstraint(11058, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueLengthConstraint(0, 0, 65535)
			{
				ConstratintId = "1514"
			});
			this.RegisterConstraint(11058, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueLengthConstraint(1, 0, 65535)
			{
				ConstratintId = "1515"
			});
			this.RegisterConstraint(11058, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueLengthConstraint(2, 0, 65535)
			{
				ConstratintId = "1516"
			});
			this.RegisterConstraint(10519, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueRangeConstraint(0, true, double.NegativeInfinity, true, 2147483647.0, true)
			{
				ConstratintId = "1851"
			});
			this.RegisterConstraint(12436, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueSetConstraint(0, false, new string[] { "Movie" })
			{
				ConstratintId = "2320"
			});
			this.RegisterConstraint(12436, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueSetConstraint(0, false, new string[] { "LineA", "RectA" })
			{
				ConstratintId = "2321"
			});
			this.RegisterConstraint(12164, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.PowerPoint, new UniqueAttributeValueConstraint(0, false, true, this)
			{
				ConstratintId = "1566"
			});
			this.RegisterConstraint(12353, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.PowerPoint, new AttributeValueSetConstraint(3, true, new string[] { "1", "2", "3", "4", "12", "13", "14" })
			{
				ConstratintId = "1576"
			});
			this.RegisterConstraint(12353, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.PowerPoint, new AttributeValueSetConstraint(11, true, new string[] { "wincrypt", "" })
			{
				ConstratintId = "1577"
			});
			this.RegisterConstraint(12353, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.PowerPoint, new AttributeValueSetConstraint(9, true, new string[] { "wincrypt", "" })
			{
				ConstratintId = "1578"
			});
			this.RegisterConstraint(12176, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.PowerPoint, new AttributeValueRangeConstraint(1, true, 0.0, true, 9999.0, true)
			{
				ConstratintId = "1579"
			});
			this.RegisterConstraint(12280, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.PowerPoint, new AttributeValueRangeConstraint(3, true, 0.0, true, 2147483647.0, true)
			{
				ConstratintId = "1593"
			});
			this.RegisterConstraint(12290, 12185, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.PowerPoint, new UniqueAttributeValueConstraint(0, false, false, this)
			{
				ConstratintId = "1596"
			});
			this.RegisterConstraint(12205, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.PowerPoint, new AttributeValueRangeConstraint(3, true, -2147483554.0, true, 2147483554.0, true)
			{
				ConstratintId = "1604"
			});
			this.RegisterConstraint(12206, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.PowerPoint, new AttributeValueRangeConstraint(0, true, -2147483554.0, true, 2147483554.0, true)
			{
				ConstratintId = "1605"
			});
			this.RegisterConstraint(12206, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.PowerPoint, new AttributeValueRangeConstraint(1, true, -2147483554.0, true, 2147483554.0, true)
			{
				ConstratintId = "1606"
			});
			this.RegisterConstraint(12206, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.PowerPoint, new AttributeValueRangeConstraint(2, true, -2147483554.0, true, 2147483554.0, true)
			{
				ConstratintId = "1607"
			});
			this.RegisterConstraint(12235, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.PowerPoint, new ReferenceExistConstraint(1, ".", 12212, "DocumentFormat.OpenXml.Presentation.CommonTimeNode", 19)
			{
				ConstratintId = "1609"
			});
			this.RegisterConstraint(12235, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.PowerPoint, new ReferenceExistConstraint(0, ".", 12263, "DocumentFormat.OpenXml.Presentation.NonVisualDrawingProperties", 0)
			{
				ConstratintId = "1610"
			});
			this.RegisterConstraint(12237, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.PowerPoint, new ReferenceExistConstraint(0, ".", 12263, "DocumentFormat.OpenXml.Presentation.NonVisualDrawingProperties", 0)
			{
				ConstratintId = "1612"
			});
			this.RegisterConstraint(12237, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.PowerPoint, new ReferenceExistConstraint(1, ".", 12212, "DocumentFormat.OpenXml.Presentation.CommonTimeNode", 19)
			{
				ConstratintId = "1613"
			});
			this.RegisterConstraint(12236, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.PowerPoint, new ReferenceExistConstraint(0, ".", 12263, "DocumentFormat.OpenXml.Presentation.NonVisualDrawingProperties", 0)
			{
				ConstratintId = "1615"
			});
			this.RegisterConstraint(12236, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.PowerPoint, new ReferenceExistConstraint(1, ".", 12212, "DocumentFormat.OpenXml.Presentation.CommonTimeNode", 19)
			{
				ConstratintId = "1616"
			});
			this.RegisterConstraint(12234, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.PowerPoint, new ReferenceExistConstraint(0, ".", 12263, "DocumentFormat.OpenXml.Presentation.NonVisualDrawingProperties", 0)
			{
				ConstratintId = "1619"
			});
			this.RegisterConstraint(12212, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.PowerPoint, new AttributeValueSetConstraint(7, false, new string[] { "0" })
			{
				ConstratintId = "1630"
			});
			this.RegisterConstraint(12192, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.PowerPoint, new AttributeValueRangeConstraint(0, true, double.NegativeInfinity, true, 2147483625.0, true)
			{
				ConstratintId = "1660"
			});
			this.RegisterConstraint(12230, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.PowerPoint, new AttributeValueRangeConstraint(0, true, double.NegativeInfinity, true, 9.0, true)
			{
				ConstratintId = "1661"
			});
			this.RegisterConstraint(12230, 12231, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.PowerPoint, new UniqueAttributeValueConstraint(0, true, false, this)
			{
				ConstratintId = "1662"
			});
			this.RegisterConstraint(12866, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(0, true, 0.0, false, 2147483648.0, false)
			{
				ConstratintId = "60000"
			});
			this.RegisterConstraint(11635, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(5, true, 0.0, false, 2147483648.0, false)
			{
				ConstratintId = "60014"
			});
			this.RegisterConstraint(11637, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(4, true, 0.0, false, 2147483648.0, false)
			{
				ConstratintId = "60014"
			});
			this.RegisterConstraint(11635, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(6, true, 0.0, false, 2147483648.0, false)
			{
				ConstratintId = "60016"
			});
			this.RegisterConstraint(11637, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(5, true, 0.0, false, 2147483648.0, false)
			{
				ConstratintId = "60016"
			});
			this.RegisterConstraint(12893, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(0, true, 1.0, true, 20.0, true)
			{
				ConstratintId = "60017"
			});
			this.RegisterConstraint(12120, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueConditionToAnother(2, 0, new string[] { "11", "12", "14" }, new string[] { "compatibilityMode" })
			{
				ConstratintId = "60018"
			});
			this.RegisterConstraint(12802, 12805, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(0, true, false, this)
			{
				ConstratintId = "60025"
			});
			this.RegisterConstraint(12802, 12805, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(1, true, false, this)
			{
				ConstratintId = "60026"
			});
			this.RegisterConstraint(12648, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueSetConstraint(8, true, new string[] { "dev", "in", "cm", "deg", "rad", "s", "lb", "g" })
			{
				ConstratintId = "60037"
			});
			this.RegisterConstraint(12674, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValuePatternConstraint(0, "[a-fA-F0-9]{8}-[a-fA-F0-9]{4}-[a-fA-F0-9]{4}-[a-fA-F0-9]{4}-[a-fA-F0-9]{12}")
			{
				ConstratintId = "60039"
			});
			this.RegisterConstraint(12674, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueSetConstraint(16, true, new string[] { "ink" })
			{
				ConstratintId = "60040"
			});
			this.RegisterConstraint(12675, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueSetConstraint(0, true, new string[] { "recognition" })
			{
				ConstratintId = "60043"
			});
			this.RegisterConstraint(12712, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValuePatternConstraint(1, "_x0000_s(102[5-9]|10[3-9][0-9]|1[1-9][0-9]{2}|[1-9][0-9]{3,7}|1[0-9]{8}|2[0-5][0-9]{7}|26[0-7][0-9]{6}|268[0-3][0-9]{5}|2684[0-2][0-9]{4}|26843[0-4][0-9]{3}|268435[0-3][0-9]{2}|2684354[0-4][0-9]|26843545[0-6])")
			{
				ConstratintId = "60068"
			});
			this.RegisterConstraint(12713, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValuePatternConstraint(0, "_x0000_s(102[5-9]|10[3-9][0-9]|1[1-9][0-9]{2}|[1-9][0-9]{3,7}|1[0-9]{8}|2[0-5][0-9]{7}|26[0-7][0-9]{6}|268[0-3][0-9]{5}|2684[0-2][0-9]{4}|26843[0-4][0-9]{3}|268435[0-3][0-9]{2}|2684354[0-4][0-9]|26843545[0-6])")
			{
				ConstratintId = "60069"
			});
			this.RegisterConstraint(12898, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(1, true, double.NegativeInfinity, true, 65535.0, true)
			{
				ConstratintId = "60100"
			});
			this.RegisterConstraint(12933, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(9, 0, 225)
			{
				ConstratintId = "60103"
			});
			this.RegisterConstraint(12933, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(10, 0, 32)
			{
				ConstratintId = "60104"
			});
			this.RegisterConstraint(12933, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(11, 0, 225)
			{
				ConstratintId = "60105"
			});
			this.RegisterConstraint(12937, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueSetConstraint(0, true, new string[] { "false" })
			{
				ConstratintId = "60112"
			});
			this.RegisterConstraint(12938, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueSetConstraint(0, true, new string[] { "false" })
			{
				ConstratintId = "60113"
			});
			this.RegisterConstraint(12939, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueSetConstraint(0, true, new string[] { "false" })
			{
				ConstratintId = "60114"
			});
			this.RegisterConstraint(12940, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueSetConstraint(0, true, new string[] { "false" })
			{
				ConstratintId = "60115"
			});
			this.RegisterConstraint(12941, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueSetConstraint(0, true, new string[] { "false" })
			{
				ConstratintId = "60116"
			});
			this.RegisterConstraint(12942, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueSetConstraint(0, true, new string[] { "false" })
			{
				ConstratintId = "60117"
			});
			this.RegisterConstraint(12943, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueSetConstraint(0, true, new string[] { "false" })
			{
				ConstratintId = "60118"
			});
			this.RegisterConstraint(12936, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueConditionToAnother(1, 14, new string[] { "0" }, new string[] { "individual", "group" })
			{
				ConstratintId = "60122"
			});
			this.RegisterConstraint(12936, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(2, true, 0.0, true, 1584.0, true)
			{
				ConstratintId = "60123"
			});
			this.RegisterConstraint(12906, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueSetConstraint(0, true, new string[] { "96", "150", "220" })
			{
				ConstratintId = "60125"
			});
			this.RegisterConstraint(12907, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(0, 0, 65535)
			{
				ConstratintId = "60129"
			});
			this.RegisterConstraint(12907, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(4, 32766, 1073741822)
			{
				ConstratintId = "60134"
			});
			this.RegisterConstraint(12955, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(0, 0, 65535)
			{
				ConstratintId = "60138"
			});
			this.RegisterConstraint(12955, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(1, 0, 65535)
			{
				ConstratintId = "60139"
			});
			this.RegisterConstraint(12957, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(0, 0, 65535)
			{
				ConstratintId = "60141"
			});
			this.RegisterConstraint(12957, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(1, 0, 65535)
			{
				ConstratintId = "60142"
			});
			this.RegisterConstraint(12958, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(0, true, -2.0, true, double.PositiveInfinity, true)
			{
				ConstratintId = "60143"
			});
			this.RegisterConstraint(12909, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(2, true, true, this)
			{
				ConstratintId = "60158"
			});
			this.RegisterConstraint(12909, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(2, 0, 65535)
			{
				ConstratintId = "60159"
			});
			this.RegisterConstraint(12931, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(1, true, 0.0, false, double.PositiveInfinity, true)
			{
				ConstratintId = "60166"
			});
			this.RegisterConstraint(12931, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(1, true, true, this)
			{
				ConstratintId = "60167"
			});
			this.RegisterConstraint(12931, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeAbsentConditionToNonValue(3, 0, new string[] { "aboveAverage" })
			{
				ConstratintId = "60170"
			});
			this.RegisterConstraint(12931, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeAbsentConditionToNonValue(4, 0, new string[] { "top10" })
			{
				ConstratintId = "60171"
			});
			this.RegisterConstraint(12931, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeAbsentConditionToNonValue(5, 0, new string[] { "top10" })
			{
				ConstratintId = "60172"
			});
			this.RegisterConstraint(12931, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeAbsentConditionToNonValue(7, 0, new string[] { "beginsWith", "containsText", "endsWith", "notContainsText" })
			{
				ConstratintId = "60174"
			});
			this.RegisterConstraint(12931, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeAbsentConditionToNonValue(8, 0, new string[] { "timePeriod" })
			{
				ConstratintId = "60175"
			});
			this.RegisterConstraint(12931, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeAbsentConditionToNonValue(10, 0, new string[] { "aboveAverage" })
			{
				ConstratintId = "60177"
			});
			this.RegisterConstraint(12931, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeAbsentConditionToNonValue(11, 0, new string[] { "aboveAverage" })
			{
				ConstratintId = "60178"
			});
			this.RegisterConstraint(12961, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLessEqualToAnother(0, 1, true)
			{
				ConstratintId = "60189"
			});
			this.RegisterConstraint(12961, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(1, true, double.NegativeInfinity, true, 100.0, true)
			{
				ConstratintId = "60192"
			});
			this.RegisterConstraint(12911, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(3, 0, 2000)
			{
				ConstratintId = "60199"
			});
			this.RegisterConstraint(12911, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(4, 0, 2000)
			{
				ConstratintId = "60200"
			});
			this.RegisterConstraint(12911, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(8, 0, 65535)
			{
				ConstratintId = "60201"
			});
			this.RegisterConstraint(12913, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(0, 0, 84)
			{
				ConstratintId = "60206"
			});
			this.RegisterConstraint(12913, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(1, 0, 65535)
			{
				ConstratintId = "60208"
			});
			this.RegisterConstraint(12914, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(0, 0, 25000)
			{
				ConstratintId = "60210"
			});
			this.RegisterConstraint(12914, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(1, 0, 50000)
			{
				ConstratintId = "60211"
			});
			this.RegisterConstraint(12980, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(1, 1, 65535)
			{
				ConstratintId = "60217"
			});
			this.RegisterConstraint(12981, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(0, 1, 32767)
			{
				ConstratintId = "60220"
			});
			this.RegisterConstraint(12987, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(0, 1, 255)
			{
				ConstratintId = "60223"
			});
			this.RegisterConstraint(12987, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new ReferenceExistConstraint(0, ".", 11263, "DocumentFormat.OpenXml.Spreadsheet.TableStyle", 0)
			{
				ConstratintId = "60224"
			});
			this.RegisterConstraint(12987, 12915, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(0, true, false, this)
			{
				ConstratintId = "60225"
			});
			this.RegisterConstraint(12988, 12983, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(0, true, false, this)
			{
				ConstratintId = "60227"
			});
			this.RegisterConstraint(12988, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new IndexReferenceConstraint(1, ".", -1, 11176, "DocumentFormat.OpenXml.Spreadsheet.DifferentialFormat", 0)
			{
				ConstratintId = "60228"
			});
			this.RegisterConstraint(12985, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueSetConstraint(1, true, new string[] { "none" })
			{
				ConstratintId = "60244"
			});
			this.RegisterConstraint(12985, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(2, true, 1.0, true, double.PositiveInfinity, true)
			{
				ConstratintId = "60245"
			});
			this.RegisterConstraint(12985, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new ReferenceExistConstraint(2, "..", 12931, "DocumentFormat.OpenXml.Office2010.Excel.ConditionalFormattingRule", 1)
			{
				ConstratintId = "60246"
			});
			this.RegisterConstraint(12915, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(0, 1, 255)
			{
				ConstratintId = "60249"
			});
			this.RegisterConstraint(12990, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeMutualExclusive(new byte[] { 0, 1 })
			{
				ConstratintId = "60252"
			});
			this.RegisterConstraint(12990, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(4, true, double.NegativeInfinity, true, 10000000.0, true)
			{
				ConstratintId = "60256"
			});
			this.RegisterConstraint(12990, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(5, 1, 255)
			{
				ConstratintId = "60258"
			});
			this.RegisterConstraint(12924, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeAbsentConditionToValue(4, 1, new string[] { "icon", "value" })
			{
				ConstratintId = "60266"
			});
			this.RegisterConstraint(12924, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeAbsentConditionToNonValue(5, 1, new string[] { "icon" })
			{
				ConstratintId = "60267"
			});
			this.RegisterConstraint(12924, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeAbsentConditionToNonValue(6, 1, new string[] { "icon" })
			{
				ConstratintId = "60272"
			});
			this.RegisterConstraint(12927, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(3, true, 0.0, true, 30000.0, true)
			{
				ConstratintId = "60273"
			});
			this.RegisterConstraint(12927, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(12, true, 0.0, true, 30000.0, true)
			{
				ConstratintId = "60276"
			});
			this.RegisterConstraint(12927, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(15, true, 0.0, true, 30000.0, true)
			{
				ConstratintId = "60277"
			});
			this.RegisterConstraint(12927, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(16, true, 0.0, true, 30000.0, true)
			{
				ConstratintId = "60278"
			});
			this.RegisterConstraint(12927, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(20, true, 0.0, true, 30000.0, true)
			{
				ConstratintId = "60279"
			});
			this.RegisterConstraint(12926, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(0, 0, 65535)
			{
				ConstratintId = "60282"
			});
			this.RegisterConstraint(12994, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(0, false, true, this)
			{
				ConstratintId = "60283"
			});
			this.RegisterConstraint(12994, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(0, 1, 32767)
			{
				ConstratintId = "60284"
			});
			this.RegisterConstraint(12994, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(2, 1, int.MaxValue)
			{
				ConstratintId = "60286"
			});
			this.RegisterConstraint(12994, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueRangeConstraint(4, true, 1.0, true, 20000.0, true)
			{
				ConstratintId = "60287"
			});
			this.RegisterConstraint(12999, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new ReferenceExistConstraint(0, "/WorkbookPart", 11302, "DocumentFormat.OpenXml.Spreadsheet.Sheet", 1)
			{
				ConstratintId = "60302"
			});
			this.RegisterConstraint(13002, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValuePatternConstraint(0, "(0|[1-9][0-9]*000)")
			{
				ConstratintId = "60307"
			});
			this.RegisterConstraint(13004, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(0, 1, 32767)
			{
				ConstratintId = "60309"
			});
			this.RegisterConstraint(13009, 13008, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(0, true, false, this)
			{
				ConstratintId = "60312"
			});
			this.RegisterConstraint(12548, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueLengthConstraint(1, 0, 255)
			{
				ConstratintId = "60319"
			});
			this.RegisterConstraint(12548, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueSetConstraint(3, true, new string[] { "0" })
			{
				ConstratintId = "60320"
			});
			this.RegisterConstraint(12548, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueSetConstraint(4, true, new string[] { "56" })
			{
				ConstratintId = "60321"
			});
			this.RegisterConstraint(12949, 12903, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(0, true, false, this)
			{
				ConstratintId = "60322"
			});
			this.RegisterConstraint(12951, 12950, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new UniqueAttributeValueConstraint(0, true, false, this)
			{
				ConstratintId = "60325"
			});
			this.RegisterConstraint(13047, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeMutualExclusive(new byte[] { 0, 1 })
			{
				ConstratintId = "60413"
			});
			this.RegisterConstraint(13085, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeMutualExclusive(new byte[] { 5, 6 })
			{
				ConstratintId = "60413"
			});
			this.RegisterConstraint(13036, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeMutualExclusive(new byte[] { 18, 20, 19, 21 })
			{
				ConstratintId = "60510"
			});
			this.RegisterConstraint(13049, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeMutualExclusive(new byte[] { 0, 1 })
			{
				ConstratintId = "60522"
			});
			this.RegisterConstraint(11921, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValuePatternConstraint(4, "[0-9a-fA-F]{8}")
			{
				ConstratintId = "61604"
			});
			this.RegisterConstraint(11921, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValuePatternConstraint(5, "[0-9a-fA-F]{8}")
			{
				ConstratintId = "61605"
			});
			this.RegisterConstraint(12654, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueSetConstraint(3, true, new string[] { "dev", "in", "cm", "deg", "rad", "s", "lb", "g" })
			{
				ConstratintId = "62001"
			});
			this.RegisterConstraint(12655, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueSetConstraint(2, true, new string[] { "dev", "in", "cm", "deg", "rad", "s", "lb", "g" })
			{
				ConstratintId = "62002"
			});
			this.RegisterConstraint(12650, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueSetConstraint(3, true, new string[] { "dev", "in", "cm", "deg", "rad", "s", "lb", "g" })
			{
				ConstratintId = "62003"
			});
			this.RegisterConstraint(12659, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new AttributeValueSetConstraint(2, true, new string[] { "dev", "in", "cm", "deg", "rad", "s", "lb", "g" })
			{
				ConstratintId = "62004"
			});
			this.RegisterConstraint(12187, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new RelationshipExistConstraint(1)
			{
				ConstratintId = "63022"
			});
			this.RegisterConstraint(12948, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new RelationshipExistConstraint(0)
			{
				ConstratintId = "63027"
			});
			this.RegisterConstraint(12947, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new RelationshipExistConstraint(0)
			{
				ConstratintId = "63028"
			});
			this.RegisterConstraint(12767, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new RelationshipExistConstraint(1)
			{
				ConstratintId = "63034"
			});
			this.RegisterConstraint(12767, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.All, new RelationshipExistConstraint(0)
			{
				ConstratintId = "63036"
			});
			this.RegisterConstraint(12898, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueRangeConstraint(2, true, double.NegativeInfinity, true, 65535.0, true)
			{
				ConstratintId = "60101"
			});
			this.RegisterConstraint(12908, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueConditionToAnother(0, 3, new string[] { "false" }, new string[] { "true" })
			{
				ConstratintId = "60145"
			});
			this.RegisterConstraint(12908, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueConditionToAnother(1, 3, new string[] { "false" }, new string[] { "true" })
			{
				ConstratintId = "60147"
			});
			this.RegisterConstraint(12908, -1, FileFormatVersions.Office2007 | FileFormatVersions.Office2010, ApplicationType.Excel, new AttributeValueConditionToAnother(2, 3, new string[] { "false" }, new string[] { "true" })
			{
				ConstratintId = "60148"
			});
		}

		// Token: 0x0600DB66 RID: 56166 RVA: 0x002BB336 File Offset: 0x002B9536
		public SemanticConstraintRegistry(FileFormatVersions format, ApplicationType appType)
		{
			this._format = format;
			this._appType = appType;
			this.Initialize();
		}

		// Token: 0x0600DB67 RID: 56167 RVA: 0x002BB373 File Offset: 0x002B9573
		public void RegisterConstraint(int elementTypeID, int ancestorTypeID, FileFormatVersions fileFormat, ApplicationType appType, SemanticConstraint constraint)
		{
			if ((fileFormat & this._format) == this._format && (appType & this._appType) == this._appType)
			{
				SemanticConstraintRegistry.AddConstraintToDic(constraint, ancestorTypeID, this._cleanList);
				SemanticConstraintRegistry.AddConstraintToDic(constraint, elementTypeID, this._semConstraintMap);
			}
		}

		// Token: 0x0600DB68 RID: 56168 RVA: 0x002BB3B4 File Offset: 0x002B95B4
		public void AddCallBackMethod(OpenXmlElement element, CallBackMethod method)
		{
			if (!this._callBackMethods.Keys.Contains(element.ElementTypeId))
			{
				this._callBackMethods.Add(element.ElementTypeId, new List<CallBackMethod>());
			}
			this._callBackMethods[element.ElementTypeId].Add(method);
		}

		// Token: 0x0600DB69 RID: 56169 RVA: 0x002BB408 File Offset: 0x002B9608
		private static void AddConstraintToDic(SemanticConstraint constraint, int key, Dictionary<int, List<SemanticConstraint>> dic)
		{
			if (key < 0)
			{
				return;
			}
			List<SemanticConstraint> list;
			if (dic.ContainsKey(key))
			{
				list = dic[key];
			}
			else
			{
				list = new List<SemanticConstraint>();
				dic.Add(key, list);
			}
			list.Add(constraint);
		}

		// Token: 0x0600DB6A RID: 56170 RVA: 0x002BB444 File Offset: 0x002B9644
		public IEnumerable<ValidationErrorInfo> CheckConstraints(ValidationContext context)
		{
			SemanticValidationLevel level = SemanticValidationLevel.Element;
			if (context.Part != null)
			{
				level = SemanticValidationLevel.Part;
			}
			if (context.Package != null)
			{
				level = SemanticValidationLevel.PackageOnly;
			}
			int elementTypeID = context.Element.ElementTypeId;
			if (this._cleanList.Keys.Contains(elementTypeID))
			{
				foreach (SemanticConstraint semanticConstraint in this._cleanList[elementTypeID])
				{
					semanticConstraint.ClearState(context);
				}
			}
			if (this._semConstraintMap.ContainsKey(elementTypeID))
			{
				foreach (SemanticConstraint constraint in this._semConstraintMap[elementTypeID])
				{
					if ((constraint.SemanticValidationLevel & level) == level)
					{
						ValidationErrorInfo err = constraint.Validate(context);
						if (err != null)
						{
							yield return err;
						}
					}
				}
			}
			yield break;
		}

		// Token: 0x0600DB6B RID: 56171 RVA: 0x002BB468 File Offset: 0x002B9668
		public void ActCallBack(int elementId)
		{
			if (this._callBackMethods.ContainsKey(elementId))
			{
				foreach (CallBackMethod callBackMethod in this._callBackMethods[elementId])
				{
					callBackMethod();
				}
			}
		}

		// Token: 0x0600DB6C RID: 56172 RVA: 0x002BB4D0 File Offset: 0x002B96D0
		public void ClearConstraintState(SemanticValidationLevel level)
		{
			foreach (List<SemanticConstraint> list in this._semConstraintMap.Values)
			{
				foreach (SemanticConstraint semanticConstraint in list.Where((SemanticConstraint c) => (c.StateScope & level) != (SemanticValidationLevel)0))
				{
					semanticConstraint.ClearState(null);
				}
			}
		}

		// Token: 0x04006C4F RID: 27727
		protected Dictionary<int, List<SemanticConstraint>> _semConstraintMap = new Dictionary<int, List<SemanticConstraint>>();

		// Token: 0x04006C50 RID: 27728
		protected Dictionary<int, List<SemanticConstraint>> _cleanList = new Dictionary<int, List<SemanticConstraint>>();

		// Token: 0x04006C51 RID: 27729
		protected Dictionary<int, List<CallBackMethod>> _callBackMethods = new Dictionary<int, List<CallBackMethod>>();

		// Token: 0x04006C52 RID: 27730
		private FileFormatVersions _format;

		// Token: 0x04006C53 RID: 27731
		private ApplicationType _appType;
	}
}
