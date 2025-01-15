using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x020024A4 RID: 9380
	[ChildElementInfo(typeof(SchemeColor), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(RgbColorModelHex), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class Shadow : OpenXmlCompositeElement
	{
		// Token: 0x170051E6 RID: 20966
		// (get) Token: 0x06011546 RID: 70982 RVA: 0x002C0C98 File Offset: 0x002BEE98
		public override string LocalName
		{
			get
			{
				return "shadow";
			}
		}

		// Token: 0x170051E7 RID: 20967
		// (get) Token: 0x06011547 RID: 70983 RVA: 0x002EC7B7 File Offset: 0x002EA9B7
		internal override byte NamespaceId
		{
			get
			{
				return 52;
			}
		}

		// Token: 0x170051E8 RID: 20968
		// (get) Token: 0x06011548 RID: 70984 RVA: 0x002ED35C File Offset: 0x002EB55C
		internal override int ElementTypeId
		{
			get
			{
				return 12854;
			}
		}

		// Token: 0x06011549 RID: 70985 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x170051E9 RID: 20969
		// (get) Token: 0x0601154A RID: 70986 RVA: 0x002ED363 File Offset: 0x002EB563
		internal override string[] AttributeTagNames
		{
			get
			{
				return Shadow.attributeTagNames;
			}
		}

		// Token: 0x170051EA RID: 20970
		// (get) Token: 0x0601154B RID: 70987 RVA: 0x002ED36A File Offset: 0x002EB56A
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Shadow.attributeNamespaceIds;
			}
		}

		// Token: 0x170051EB RID: 20971
		// (get) Token: 0x0601154C RID: 70988 RVA: 0x002E0CB4 File Offset: 0x002DEEB4
		// (set) Token: 0x0601154D RID: 70989 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(52, "blurRad")]
		public Int64Value BlurRadius
		{
			get
			{
				return (Int64Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x170051EC RID: 20972
		// (get) Token: 0x0601154E RID: 70990 RVA: 0x002E0CC3 File Offset: 0x002DEEC3
		// (set) Token: 0x0601154F RID: 70991 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(52, "dist")]
		public Int64Value DistanceFromText
		{
			get
			{
				return (Int64Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x170051ED RID: 20973
		// (get) Token: 0x06011550 RID: 70992 RVA: 0x002E1683 File Offset: 0x002DF883
		// (set) Token: 0x06011551 RID: 70993 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(52, "dir")]
		public Int32Value DirectionAngle
		{
			get
			{
				return (Int32Value)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x170051EE RID: 20974
		// (get) Token: 0x06011552 RID: 70994 RVA: 0x002BFA76 File Offset: 0x002BDC76
		// (set) Token: 0x06011553 RID: 70995 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(52, "sx")]
		public Int32Value HorizontalScalingFactor
		{
			get
			{
				return (Int32Value)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x170051EF RID: 20975
		// (get) Token: 0x06011554 RID: 70996 RVA: 0x002C8292 File Offset: 0x002C6492
		// (set) Token: 0x06011555 RID: 70997 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(52, "sy")]
		public Int32Value VerticalScalingFactor
		{
			get
			{
				return (Int32Value)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x170051F0 RID: 20976
		// (get) Token: 0x06011556 RID: 70998 RVA: 0x002ED371 File Offset: 0x002EB571
		// (set) Token: 0x06011557 RID: 70999 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(52, "kx")]
		public Int32Value HorizontalSkewAngle
		{
			get
			{
				return (Int32Value)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x170051F1 RID: 20977
		// (get) Token: 0x06011558 RID: 71000 RVA: 0x002ED380 File Offset: 0x002EB580
		// (set) Token: 0x06011559 RID: 71001 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(52, "ky")]
		public Int32Value VerticalSkewAngle
		{
			get
			{
				return (Int32Value)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x170051F2 RID: 20978
		// (get) Token: 0x0601155A RID: 71002 RVA: 0x002ED38F File Offset: 0x002EB58F
		// (set) Token: 0x0601155B RID: 71003 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(52, "algn")]
		public EnumValue<RectangleAlignmentValues> Alignment
		{
			get
			{
				return (EnumValue<RectangleAlignmentValues>)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x0601155C RID: 71004 RVA: 0x00293ECF File Offset: 0x002920CF
		public Shadow()
		{
		}

		// Token: 0x0601155D RID: 71005 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Shadow(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601155E RID: 71006 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Shadow(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601155F RID: 71007 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Shadow(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06011560 RID: 71008 RVA: 0x002ED009 File Offset: 0x002EB209
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (52 == namespaceId && "srgbClr" == name)
			{
				return new RgbColorModelHex();
			}
			if (52 == namespaceId && "schemeClr" == name)
			{
				return new SchemeColor();
			}
			return null;
		}

		// Token: 0x170051F3 RID: 20979
		// (get) Token: 0x06011561 RID: 71009 RVA: 0x002ED39E File Offset: 0x002EB59E
		internal override string[] ElementTagNames
		{
			get
			{
				return Shadow.eleTagNames;
			}
		}

		// Token: 0x170051F4 RID: 20980
		// (get) Token: 0x06011562 RID: 71010 RVA: 0x002ED3A5 File Offset: 0x002EB5A5
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Shadow.eleNamespaceIds;
			}
		}

		// Token: 0x170051F5 RID: 20981
		// (get) Token: 0x06011563 RID: 71011 RVA: 0x000023C4 File Offset: 0x000005C4
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneChoice;
			}
		}

		// Token: 0x170051F6 RID: 20982
		// (get) Token: 0x06011564 RID: 71012 RVA: 0x002ED04A File Offset: 0x002EB24A
		// (set) Token: 0x06011565 RID: 71013 RVA: 0x002ED053 File Offset: 0x002EB253
		public RgbColorModelHex RgbColorModelHex
		{
			get
			{
				return base.GetElement<RgbColorModelHex>(0);
			}
			set
			{
				base.SetElement<RgbColorModelHex>(0, value);
			}
		}

		// Token: 0x170051F7 RID: 20983
		// (get) Token: 0x06011566 RID: 71014 RVA: 0x002ED05D File Offset: 0x002EB25D
		// (set) Token: 0x06011567 RID: 71015 RVA: 0x002ED066 File Offset: 0x002EB266
		public SchemeColor SchemeColor
		{
			get
			{
				return base.GetElement<SchemeColor>(1);
			}
			set
			{
				base.SetElement<SchemeColor>(1, value);
			}
		}

		// Token: 0x06011568 RID: 71016 RVA: 0x002ED3AC File Offset: 0x002EB5AC
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (52 == namespaceId && "blurRad" == name)
			{
				return new Int64Value();
			}
			if (52 == namespaceId && "dist" == name)
			{
				return new Int64Value();
			}
			if (52 == namespaceId && "dir" == name)
			{
				return new Int32Value();
			}
			if (52 == namespaceId && "sx" == name)
			{
				return new Int32Value();
			}
			if (52 == namespaceId && "sy" == name)
			{
				return new Int32Value();
			}
			if (52 == namespaceId && "kx" == name)
			{
				return new Int32Value();
			}
			if (52 == namespaceId && "ky" == name)
			{
				return new Int32Value();
			}
			if (52 == namespaceId && "algn" == name)
			{
				return new EnumValue<RectangleAlignmentValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06011569 RID: 71017 RVA: 0x002ED481 File Offset: 0x002EB681
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Shadow>(deep);
		}

		// Token: 0x04007958 RID: 31064
		private const string tagName = "shadow";

		// Token: 0x04007959 RID: 31065
		private const byte tagNsId = 52;

		// Token: 0x0400795A RID: 31066
		internal const int ElementTypeIdConst = 12854;

		// Token: 0x0400795B RID: 31067
		private static string[] attributeTagNames = new string[] { "blurRad", "dist", "dir", "sx", "sy", "kx", "ky", "algn" };

		// Token: 0x0400795C RID: 31068
		private static byte[] attributeNamespaceIds = new byte[] { 52, 52, 52, 52, 52, 52, 52, 52 };

		// Token: 0x0400795D RID: 31069
		private static readonly string[] eleTagNames = new string[] { "srgbClr", "schemeClr" };

		// Token: 0x0400795E RID: 31070
		private static readonly byte[] eleNamespaceIds = new byte[] { 52, 52 };
	}
}
