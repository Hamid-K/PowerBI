using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x02002665 RID: 9829
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	internal class Rule : OpenXmlCompositeElement
	{
		// Token: 0x17005BD2 RID: 23506
		// (get) Token: 0x06012B6C RID: 76652 RVA: 0x002FE63E File Offset: 0x002FC83E
		public override string LocalName
		{
			get
			{
				return "rule";
			}
		}

		// Token: 0x17005BD3 RID: 23507
		// (get) Token: 0x06012B6D RID: 76653 RVA: 0x0006808E File Offset: 0x0006628E
		internal override byte NamespaceId
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17005BD4 RID: 23508
		// (get) Token: 0x06012B6E RID: 76654 RVA: 0x002FE645 File Offset: 0x002FC845
		internal override int ElementTypeId
		{
			get
			{
				return 10646;
			}
		}

		// Token: 0x06012B6F RID: 76655 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005BD5 RID: 23509
		// (get) Token: 0x06012B70 RID: 76656 RVA: 0x002FE64C File Offset: 0x002FC84C
		internal override string[] AttributeTagNames
		{
			get
			{
				return Rule.attributeTagNames;
			}
		}

		// Token: 0x17005BD6 RID: 23510
		// (get) Token: 0x06012B71 RID: 76657 RVA: 0x002FE653 File Offset: 0x002FC853
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Rule.attributeNamespaceIds;
			}
		}

		// Token: 0x17005BD7 RID: 23511
		// (get) Token: 0x06012B72 RID: 76658 RVA: 0x002FE3EC File Offset: 0x002FC5EC
		// (set) Token: 0x06012B73 RID: 76659 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "type")]
		public EnumValue<ConstraintValues> Type
		{
			get
			{
				return (EnumValue<ConstraintValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17005BD8 RID: 23512
		// (get) Token: 0x06012B74 RID: 76660 RVA: 0x002FE3FB File Offset: 0x002FC5FB
		// (set) Token: 0x06012B75 RID: 76661 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "for")]
		public EnumValue<ConstraintRelationshipValues> For
		{
			get
			{
				return (EnumValue<ConstraintRelationshipValues>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17005BD9 RID: 23513
		// (get) Token: 0x06012B76 RID: 76662 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x06012B77 RID: 76663 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "forName")]
		public StringValue ForName
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

		// Token: 0x17005BDA RID: 23514
		// (get) Token: 0x06012B78 RID: 76664 RVA: 0x002FE40A File Offset: 0x002FC60A
		// (set) Token: 0x06012B79 RID: 76665 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "ptType")]
		public EnumValue<ElementValues> PointType
		{
			get
			{
				return (EnumValue<ElementValues>)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17005BDB RID: 23515
		// (get) Token: 0x06012B7A RID: 76666 RVA: 0x002E82DC File Offset: 0x002E64DC
		// (set) Token: 0x06012B7B RID: 76667 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "val")]
		public DoubleValue Val
		{
			get
			{
				return (DoubleValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17005BDC RID: 23516
		// (get) Token: 0x06012B7C RID: 76668 RVA: 0x002F66D1 File Offset: 0x002F48D1
		// (set) Token: 0x06012B7D RID: 76669 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "fact")]
		public DoubleValue Fact
		{
			get
			{
				return (DoubleValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x17005BDD RID: 23517
		// (get) Token: 0x06012B7E RID: 76670 RVA: 0x002FE65A File Offset: 0x002FC85A
		// (set) Token: 0x06012B7F RID: 76671 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "max")]
		public DoubleValue Max
		{
			get
			{
				return (DoubleValue)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x06012B80 RID: 76672 RVA: 0x00293ECF File Offset: 0x002920CF
		public Rule()
		{
		}

		// Token: 0x06012B81 RID: 76673 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Rule(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012B82 RID: 76674 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Rule(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012B83 RID: 76675 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Rule(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012B84 RID: 76676 RVA: 0x002FE011 File Offset: 0x002FC211
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (14 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17005BDE RID: 23518
		// (get) Token: 0x06012B85 RID: 76677 RVA: 0x002FE669 File Offset: 0x002FC869
		internal override string[] ElementTagNames
		{
			get
			{
				return Rule.eleTagNames;
			}
		}

		// Token: 0x17005BDF RID: 23519
		// (get) Token: 0x06012B86 RID: 76678 RVA: 0x002FE670 File Offset: 0x002FC870
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Rule.eleNamespaceIds;
			}
		}

		// Token: 0x17005BE0 RID: 23520
		// (get) Token: 0x06012B87 RID: 76679 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005BE1 RID: 23521
		// (get) Token: 0x06012B88 RID: 76680 RVA: 0x002FE03A File Offset: 0x002FC23A
		// (set) Token: 0x06012B89 RID: 76681 RVA: 0x002FE043 File Offset: 0x002FC243
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

		// Token: 0x06012B8A RID: 76682 RVA: 0x002FE678 File Offset: 0x002FC878
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "type" == name)
			{
				return new EnumValue<ConstraintValues>();
			}
			if (namespaceId == 0 && "for" == name)
			{
				return new EnumValue<ConstraintRelationshipValues>();
			}
			if (namespaceId == 0 && "forName" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "ptType" == name)
			{
				return new EnumValue<ElementValues>();
			}
			if (namespaceId == 0 && "val" == name)
			{
				return new DoubleValue();
			}
			if (namespaceId == 0 && "fact" == name)
			{
				return new DoubleValue();
			}
			if (namespaceId == 0 && "max" == name)
			{
				return new DoubleValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06012B8B RID: 76683 RVA: 0x002FE727 File Offset: 0x002FC927
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Rule>(deep);
		}

		// Token: 0x06012B8C RID: 76684 RVA: 0x002FE730 File Offset: 0x002FC930
		// Note: this type is marked as 'beforefieldinit'.
		static Rule()
		{
			byte[] array = new byte[7];
			Rule.attributeNamespaceIds = array;
			Rule.eleTagNames = new string[] { "extLst" };
			Rule.eleNamespaceIds = new byte[] { 14 };
		}

		// Token: 0x0400814F RID: 33103
		private const string tagName = "rule";

		// Token: 0x04008150 RID: 33104
		private const byte tagNsId = 14;

		// Token: 0x04008151 RID: 33105
		internal const int ElementTypeIdConst = 10646;

		// Token: 0x04008152 RID: 33106
		private static string[] attributeTagNames = new string[] { "type", "for", "forName", "ptType", "val", "fact", "max" };

		// Token: 0x04008153 RID: 33107
		private static byte[] attributeNamespaceIds;

		// Token: 0x04008154 RID: 33108
		private static readonly string[] eleTagNames;

		// Token: 0x04008155 RID: 33109
		private static readonly byte[] eleNamespaceIds;
	}
}
