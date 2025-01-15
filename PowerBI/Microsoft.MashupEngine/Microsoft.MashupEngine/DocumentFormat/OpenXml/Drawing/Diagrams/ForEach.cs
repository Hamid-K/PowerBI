using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x02002671 RID: 9841
	[ChildElementInfo(typeof(Constraints))]
	[ChildElementInfo(typeof(LayoutNode))]
	[ChildElementInfo(typeof(Choose))]
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Shape))]
	[ChildElementInfo(typeof(PresentationOf))]
	[ChildElementInfo(typeof(Algorithm))]
	[ChildElementInfo(typeof(RuleList))]
	[ChildElementInfo(typeof(ForEach))]
	internal class ForEach : OpenXmlCompositeElement
	{
		// Token: 0x17005C32 RID: 23602
		// (get) Token: 0x06012C48 RID: 76872 RVA: 0x002FF03B File Offset: 0x002FD23B
		public override string LocalName
		{
			get
			{
				return "forEach";
			}
		}

		// Token: 0x17005C33 RID: 23603
		// (get) Token: 0x06012C49 RID: 76873 RVA: 0x0006808E File Offset: 0x0006628E
		internal override byte NamespaceId
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17005C34 RID: 23604
		// (get) Token: 0x06012C4A RID: 76874 RVA: 0x002FF042 File Offset: 0x002FD242
		internal override int ElementTypeId
		{
			get
			{
				return 10656;
			}
		}

		// Token: 0x06012C4B RID: 76875 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005C35 RID: 23605
		// (get) Token: 0x06012C4C RID: 76876 RVA: 0x002FF049 File Offset: 0x002FD249
		internal override string[] AttributeTagNames
		{
			get
			{
				return ForEach.attributeTagNames;
			}
		}

		// Token: 0x17005C36 RID: 23606
		// (get) Token: 0x06012C4D RID: 76877 RVA: 0x002FF050 File Offset: 0x002FD250
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ForEach.attributeNamespaceIds;
			}
		}

		// Token: 0x17005C37 RID: 23607
		// (get) Token: 0x06012C4E RID: 76878 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06012C4F RID: 76879 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "name")]
		public StringValue Name
		{
			get
			{
				return (StringValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17005C38 RID: 23608
		// (get) Token: 0x06012C50 RID: 76880 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06012C51 RID: 76881 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "ref")]
		public StringValue Reference
		{
			get
			{
				return (StringValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17005C39 RID: 23609
		// (get) Token: 0x06012C52 RID: 76882 RVA: 0x002FF057 File Offset: 0x002FD257
		// (set) Token: 0x06012C53 RID: 76883 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "axis")]
		public ListValue<EnumValue<AxisValues>> Axis
		{
			get
			{
				return (ListValue<EnumValue<AxisValues>>)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17005C3A RID: 23610
		// (get) Token: 0x06012C54 RID: 76884 RVA: 0x002FF066 File Offset: 0x002FD266
		// (set) Token: 0x06012C55 RID: 76885 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "ptType")]
		public ListValue<EnumValue<ElementValues>> PointType
		{
			get
			{
				return (ListValue<EnumValue<ElementValues>>)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17005C3B RID: 23611
		// (get) Token: 0x06012C56 RID: 76886 RVA: 0x002FF075 File Offset: 0x002FD275
		// (set) Token: 0x06012C57 RID: 76887 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "hideLastTrans")]
		public ListValue<BooleanValue> HideLastTrans
		{
			get
			{
				return (ListValue<BooleanValue>)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17005C3C RID: 23612
		// (get) Token: 0x06012C58 RID: 76888 RVA: 0x002FEC2B File Offset: 0x002FCE2B
		// (set) Token: 0x06012C59 RID: 76889 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "st")]
		public ListValue<Int32Value> Start
		{
			get
			{
				return (ListValue<Int32Value>)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x17005C3D RID: 23613
		// (get) Token: 0x06012C5A RID: 76890 RVA: 0x002FF084 File Offset: 0x002FD284
		// (set) Token: 0x06012C5B RID: 76891 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "cnt")]
		public ListValue<UInt32Value> Count
		{
			get
			{
				return (ListValue<UInt32Value>)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x17005C3E RID: 23614
		// (get) Token: 0x06012C5C RID: 76892 RVA: 0x002FF093 File Offset: 0x002FD293
		// (set) Token: 0x06012C5D RID: 76893 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "step")]
		public ListValue<Int32Value> Step
		{
			get
			{
				return (ListValue<Int32Value>)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x06012C5E RID: 76894 RVA: 0x00293ECF File Offset: 0x002920CF
		public ForEach()
		{
		}

		// Token: 0x06012C5F RID: 76895 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ForEach(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012C60 RID: 76896 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ForEach(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012C61 RID: 76897 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ForEach(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012C62 RID: 76898 RVA: 0x002FF0A4 File Offset: 0x002FD2A4
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (14 == namespaceId && "alg" == name)
			{
				return new Algorithm();
			}
			if (14 == namespaceId && "shape" == name)
			{
				return new Shape();
			}
			if (14 == namespaceId && "presOf" == name)
			{
				return new PresentationOf();
			}
			if (14 == namespaceId && "constrLst" == name)
			{
				return new Constraints();
			}
			if (14 == namespaceId && "ruleLst" == name)
			{
				return new RuleList();
			}
			if (14 == namespaceId && "forEach" == name)
			{
				return new ForEach();
			}
			if (14 == namespaceId && "layoutNode" == name)
			{
				return new LayoutNode();
			}
			if (14 == namespaceId && "choose" == name)
			{
				return new Choose();
			}
			if (14 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x06012C63 RID: 76899 RVA: 0x002FF18C File Offset: 0x002FD38C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "ref" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "axis" == name)
			{
				return new ListValue<EnumValue<AxisValues>>();
			}
			if (namespaceId == 0 && "ptType" == name)
			{
				return new ListValue<EnumValue<ElementValues>>();
			}
			if (namespaceId == 0 && "hideLastTrans" == name)
			{
				return new ListValue<BooleanValue>();
			}
			if (namespaceId == 0 && "st" == name)
			{
				return new ListValue<Int32Value>();
			}
			if (namespaceId == 0 && "cnt" == name)
			{
				return new ListValue<UInt32Value>();
			}
			if (namespaceId == 0 && "step" == name)
			{
				return new ListValue<Int32Value>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06012C64 RID: 76900 RVA: 0x002FF251 File Offset: 0x002FD451
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ForEach>(deep);
		}

		// Token: 0x06012C65 RID: 76901 RVA: 0x002FF25C File Offset: 0x002FD45C
		// Note: this type is marked as 'beforefieldinit'.
		static ForEach()
		{
			byte[] array = new byte[8];
			ForEach.attributeNamespaceIds = array;
		}

		// Token: 0x04008184 RID: 33156
		private const string tagName = "forEach";

		// Token: 0x04008185 RID: 33157
		private const byte tagNsId = 14;

		// Token: 0x04008186 RID: 33158
		internal const int ElementTypeIdConst = 10656;

		// Token: 0x04008187 RID: 33159
		private static string[] attributeTagNames = new string[] { "name", "ref", "axis", "ptType", "hideLastTrans", "st", "cnt", "step" };

		// Token: 0x04008188 RID: 33160
		private static byte[] attributeNamespaceIds;
	}
}
