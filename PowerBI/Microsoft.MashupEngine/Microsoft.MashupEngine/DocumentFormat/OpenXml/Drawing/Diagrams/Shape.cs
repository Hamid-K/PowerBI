using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x0200266A RID: 9834
	[ChildElementInfo(typeof(AdjustList))]
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	internal class Shape : OpenXmlCompositeElement
	{
		// Token: 0x17005BFA RID: 23546
		// (get) Token: 0x06012BC5 RID: 76741 RVA: 0x002C1364 File Offset: 0x002BF564
		public override string LocalName
		{
			get
			{
				return "shape";
			}
		}

		// Token: 0x17005BFB RID: 23547
		// (get) Token: 0x06012BC6 RID: 76742 RVA: 0x0006808E File Offset: 0x0006628E
		internal override byte NamespaceId
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17005BFC RID: 23548
		// (get) Token: 0x06012BC7 RID: 76743 RVA: 0x002FE9F3 File Offset: 0x002FCBF3
		internal override int ElementTypeId
		{
			get
			{
				return 10651;
			}
		}

		// Token: 0x06012BC8 RID: 76744 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005BFD RID: 23549
		// (get) Token: 0x06012BC9 RID: 76745 RVA: 0x002FE9FA File Offset: 0x002FCBFA
		internal override string[] AttributeTagNames
		{
			get
			{
				return Shape.attributeTagNames;
			}
		}

		// Token: 0x17005BFE RID: 23550
		// (get) Token: 0x06012BCA RID: 76746 RVA: 0x002FEA01 File Offset: 0x002FCC01
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Shape.attributeNamespaceIds;
			}
		}

		// Token: 0x17005BFF RID: 23551
		// (get) Token: 0x06012BCB RID: 76747 RVA: 0x002E7DC5 File Offset: 0x002E5FC5
		// (set) Token: 0x06012BCC RID: 76748 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "rot")]
		public DoubleValue Rotation
		{
			get
			{
				return (DoubleValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17005C00 RID: 23552
		// (get) Token: 0x06012BCD RID: 76749 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06012BCE RID: 76750 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "type")]
		public StringValue Type
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

		// Token: 0x17005C01 RID: 23553
		// (get) Token: 0x06012BCF RID: 76751 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x06012BD0 RID: 76752 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(19, "blip")]
		public StringValue Blip
		{
			get
			{
				return (StringValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17005C02 RID: 23554
		// (get) Token: 0x06012BD1 RID: 76753 RVA: 0x002BFA76 File Offset: 0x002BDC76
		// (set) Token: 0x06012BD2 RID: 76754 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "zOrderOff")]
		public Int32Value ZOrderOffset
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

		// Token: 0x17005C03 RID: 23555
		// (get) Token: 0x06012BD3 RID: 76755 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x06012BD4 RID: 76756 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "hideGeom")]
		public BooleanValue HideGeometry
		{
			get
			{
				return (BooleanValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17005C04 RID: 23556
		// (get) Token: 0x06012BD5 RID: 76757 RVA: 0x002D7D70 File Offset: 0x002D5F70
		// (set) Token: 0x06012BD6 RID: 76758 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "lkTxEntry")]
		public BooleanValue LockedText
		{
			get
			{
				return (BooleanValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x17005C05 RID: 23557
		// (get) Token: 0x06012BD7 RID: 76759 RVA: 0x002CCC33 File Offset: 0x002CAE33
		// (set) Token: 0x06012BD8 RID: 76760 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "blipPhldr")]
		public BooleanValue BlipPlaceholder
		{
			get
			{
				return (BooleanValue)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x06012BD9 RID: 76761 RVA: 0x00293ECF File Offset: 0x002920CF
		public Shape()
		{
		}

		// Token: 0x06012BDA RID: 76762 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Shape(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012BDB RID: 76763 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Shape(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012BDC RID: 76764 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Shape(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012BDD RID: 76765 RVA: 0x002FEA08 File Offset: 0x002FCC08
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (14 == namespaceId && "adjLst" == name)
			{
				return new AdjustList();
			}
			if (14 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17005C06 RID: 23558
		// (get) Token: 0x06012BDE RID: 76766 RVA: 0x002FEA3B File Offset: 0x002FCC3B
		internal override string[] ElementTagNames
		{
			get
			{
				return Shape.eleTagNames;
			}
		}

		// Token: 0x17005C07 RID: 23559
		// (get) Token: 0x06012BDF RID: 76767 RVA: 0x002FEA42 File Offset: 0x002FCC42
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Shape.eleNamespaceIds;
			}
		}

		// Token: 0x17005C08 RID: 23560
		// (get) Token: 0x06012BE0 RID: 76768 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005C09 RID: 23561
		// (get) Token: 0x06012BE1 RID: 76769 RVA: 0x002FEA49 File Offset: 0x002FCC49
		// (set) Token: 0x06012BE2 RID: 76770 RVA: 0x002FEA52 File Offset: 0x002FCC52
		public AdjustList AdjustList
		{
			get
			{
				return base.GetElement<AdjustList>(0);
			}
			set
			{
				base.SetElement<AdjustList>(0, value);
			}
		}

		// Token: 0x17005C0A RID: 23562
		// (get) Token: 0x06012BE3 RID: 76771 RVA: 0x002FEA5C File Offset: 0x002FCC5C
		// (set) Token: 0x06012BE4 RID: 76772 RVA: 0x002FEA65 File Offset: 0x002FCC65
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(1);
			}
			set
			{
				base.SetElement<ExtensionList>(1, value);
			}
		}

		// Token: 0x06012BE5 RID: 76773 RVA: 0x002FEA70 File Offset: 0x002FCC70
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "rot" == name)
			{
				return new DoubleValue();
			}
			if (namespaceId == 0 && "type" == name)
			{
				return new StringValue();
			}
			if (19 == namespaceId && "blip" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "zOrderOff" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "hideGeom" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "lkTxEntry" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "blipPhldr" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06012BE6 RID: 76774 RVA: 0x002FEB21 File Offset: 0x002FCD21
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Shape>(deep);
		}

		// Token: 0x06012BE7 RID: 76775 RVA: 0x002FEB2C File Offset: 0x002FCD2C
		// Note: this type is marked as 'beforefieldinit'.
		static Shape()
		{
			byte[] array = new byte[7];
			array[2] = 19;
			Shape.attributeNamespaceIds = array;
			Shape.eleTagNames = new string[] { "adjLst", "extLst" };
			Shape.eleNamespaceIds = new byte[] { 14, 14 };
		}

		// Token: 0x04008168 RID: 33128
		private const string tagName = "shape";

		// Token: 0x04008169 RID: 33129
		private const byte tagNsId = 14;

		// Token: 0x0400816A RID: 33130
		internal const int ElementTypeIdConst = 10651;

		// Token: 0x0400816B RID: 33131
		private static string[] attributeTagNames = new string[] { "rot", "type", "blip", "zOrderOff", "hideGeom", "lkTxEntry", "blipPhldr" };

		// Token: 0x0400816C RID: 33132
		private static byte[] attributeNamespaceIds;

		// Token: 0x0400816D RID: 33133
		private static readonly string[] eleTagNames;

		// Token: 0x0400816E RID: 33134
		private static readonly byte[] eleNamespaceIds;
	}
}
