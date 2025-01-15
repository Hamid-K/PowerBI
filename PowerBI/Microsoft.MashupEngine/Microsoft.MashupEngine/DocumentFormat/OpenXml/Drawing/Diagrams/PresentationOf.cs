using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x0200266B RID: 9835
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	internal class PresentationOf : OpenXmlCompositeElement
	{
		// Token: 0x17005C0B RID: 23563
		// (get) Token: 0x06012BE8 RID: 76776 RVA: 0x002FEBC4 File Offset: 0x002FCDC4
		public override string LocalName
		{
			get
			{
				return "presOf";
			}
		}

		// Token: 0x17005C0C RID: 23564
		// (get) Token: 0x06012BE9 RID: 76777 RVA: 0x0006808E File Offset: 0x0006628E
		internal override byte NamespaceId
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17005C0D RID: 23565
		// (get) Token: 0x06012BEA RID: 76778 RVA: 0x002FEBCB File Offset: 0x002FCDCB
		internal override int ElementTypeId
		{
			get
			{
				return 10652;
			}
		}

		// Token: 0x06012BEB RID: 76779 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005C0E RID: 23566
		// (get) Token: 0x06012BEC RID: 76780 RVA: 0x002FEBD2 File Offset: 0x002FCDD2
		internal override string[] AttributeTagNames
		{
			get
			{
				return PresentationOf.attributeTagNames;
			}
		}

		// Token: 0x17005C0F RID: 23567
		// (get) Token: 0x06012BED RID: 76781 RVA: 0x002FEBD9 File Offset: 0x002FCDD9
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PresentationOf.attributeNamespaceIds;
			}
		}

		// Token: 0x17005C10 RID: 23568
		// (get) Token: 0x06012BEE RID: 76782 RVA: 0x002FEBE0 File Offset: 0x002FCDE0
		// (set) Token: 0x06012BEF RID: 76783 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "axis")]
		public ListValue<EnumValue<AxisValues>> Axis
		{
			get
			{
				return (ListValue<EnumValue<AxisValues>>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17005C11 RID: 23569
		// (get) Token: 0x06012BF0 RID: 76784 RVA: 0x002FEBEF File Offset: 0x002FCDEF
		// (set) Token: 0x06012BF1 RID: 76785 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "ptType")]
		public ListValue<EnumValue<ElementValues>> PointType
		{
			get
			{
				return (ListValue<EnumValue<ElementValues>>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17005C12 RID: 23570
		// (get) Token: 0x06012BF2 RID: 76786 RVA: 0x002FEBFE File Offset: 0x002FCDFE
		// (set) Token: 0x06012BF3 RID: 76787 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "hideLastTrans")]
		public ListValue<BooleanValue> HideLastTrans
		{
			get
			{
				return (ListValue<BooleanValue>)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17005C13 RID: 23571
		// (get) Token: 0x06012BF4 RID: 76788 RVA: 0x002FEC0D File Offset: 0x002FCE0D
		// (set) Token: 0x06012BF5 RID: 76789 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "st")]
		public ListValue<Int32Value> Start
		{
			get
			{
				return (ListValue<Int32Value>)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17005C14 RID: 23572
		// (get) Token: 0x06012BF6 RID: 76790 RVA: 0x002FEC1C File Offset: 0x002FCE1C
		// (set) Token: 0x06012BF7 RID: 76791 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "cnt")]
		public ListValue<UInt32Value> Count
		{
			get
			{
				return (ListValue<UInt32Value>)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17005C15 RID: 23573
		// (get) Token: 0x06012BF8 RID: 76792 RVA: 0x002FEC2B File Offset: 0x002FCE2B
		// (set) Token: 0x06012BF9 RID: 76793 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "step")]
		public ListValue<Int32Value> Step
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

		// Token: 0x06012BFA RID: 76794 RVA: 0x00293ECF File Offset: 0x002920CF
		public PresentationOf()
		{
		}

		// Token: 0x06012BFB RID: 76795 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PresentationOf(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012BFC RID: 76796 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PresentationOf(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012BFD RID: 76797 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PresentationOf(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012BFE RID: 76798 RVA: 0x002FE011 File Offset: 0x002FC211
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (14 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17005C16 RID: 23574
		// (get) Token: 0x06012BFF RID: 76799 RVA: 0x002FEC3A File Offset: 0x002FCE3A
		internal override string[] ElementTagNames
		{
			get
			{
				return PresentationOf.eleTagNames;
			}
		}

		// Token: 0x17005C17 RID: 23575
		// (get) Token: 0x06012C00 RID: 76800 RVA: 0x002FEC41 File Offset: 0x002FCE41
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return PresentationOf.eleNamespaceIds;
			}
		}

		// Token: 0x17005C18 RID: 23576
		// (get) Token: 0x06012C01 RID: 76801 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005C19 RID: 23577
		// (get) Token: 0x06012C02 RID: 76802 RVA: 0x002FE03A File Offset: 0x002FC23A
		// (set) Token: 0x06012C03 RID: 76803 RVA: 0x002FE043 File Offset: 0x002FC243
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(0);
			}
			set
			{
				base.SetElement<ExtensionList>(0, value);
			}
		}

		// Token: 0x06012C04 RID: 76804 RVA: 0x002FEC48 File Offset: 0x002FCE48
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
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

		// Token: 0x06012C05 RID: 76805 RVA: 0x002FECE1 File Offset: 0x002FCEE1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PresentationOf>(deep);
		}

		// Token: 0x06012C06 RID: 76806 RVA: 0x002FECEC File Offset: 0x002FCEEC
		// Note: this type is marked as 'beforefieldinit'.
		static PresentationOf()
		{
			byte[] array = new byte[6];
			PresentationOf.attributeNamespaceIds = array;
			PresentationOf.eleTagNames = new string[] { "extLst" };
			PresentationOf.eleNamespaceIds = new byte[] { 14 };
		}

		// Token: 0x0400816F RID: 33135
		private const string tagName = "presOf";

		// Token: 0x04008170 RID: 33136
		private const byte tagNsId = 14;

		// Token: 0x04008171 RID: 33137
		internal const int ElementTypeIdConst = 10652;

		// Token: 0x04008172 RID: 33138
		private static string[] attributeTagNames = new string[] { "axis", "ptType", "hideLastTrans", "st", "cnt", "step" };

		// Token: 0x04008173 RID: 33139
		private static byte[] attributeNamespaceIds;

		// Token: 0x04008174 RID: 33140
		private static readonly string[] eleTagNames;

		// Token: 0x04008175 RID: 33141
		private static readonly byte[] eleNamespaceIds;
	}
}
