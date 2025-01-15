using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x02002674 RID: 9844
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
	internal class DiagramChooseIf : OpenXmlCompositeElement
	{
		// Token: 0x17005C4E RID: 23630
		// (get) Token: 0x06012C8C RID: 76940 RVA: 0x002FF537 File Offset: 0x002FD737
		public override string LocalName
		{
			get
			{
				return "if";
			}
		}

		// Token: 0x17005C4F RID: 23631
		// (get) Token: 0x06012C8D RID: 76941 RVA: 0x0006808E File Offset: 0x0006628E
		internal override byte NamespaceId
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17005C50 RID: 23632
		// (get) Token: 0x06012C8E RID: 76942 RVA: 0x002FF53E File Offset: 0x002FD73E
		internal override int ElementTypeId
		{
			get
			{
				return 10659;
			}
		}

		// Token: 0x06012C8F RID: 76943 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005C51 RID: 23633
		// (get) Token: 0x06012C90 RID: 76944 RVA: 0x002FF545 File Offset: 0x002FD745
		internal override string[] AttributeTagNames
		{
			get
			{
				return DiagramChooseIf.attributeTagNames;
			}
		}

		// Token: 0x17005C52 RID: 23634
		// (get) Token: 0x06012C91 RID: 76945 RVA: 0x002FF54C File Offset: 0x002FD74C
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return DiagramChooseIf.attributeNamespaceIds;
			}
		}

		// Token: 0x17005C53 RID: 23635
		// (get) Token: 0x06012C92 RID: 76946 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06012C93 RID: 76947 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17005C54 RID: 23636
		// (get) Token: 0x06012C94 RID: 76948 RVA: 0x002FF553 File Offset: 0x002FD753
		// (set) Token: 0x06012C95 RID: 76949 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "axis")]
		public ListValue<EnumValue<AxisValues>> Axis
		{
			get
			{
				return (ListValue<EnumValue<AxisValues>>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17005C55 RID: 23637
		// (get) Token: 0x06012C96 RID: 76950 RVA: 0x002FF562 File Offset: 0x002FD762
		// (set) Token: 0x06012C97 RID: 76951 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "ptType")]
		public ListValue<EnumValue<ElementValues>> PointType
		{
			get
			{
				return (ListValue<EnumValue<ElementValues>>)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17005C56 RID: 23638
		// (get) Token: 0x06012C98 RID: 76952 RVA: 0x002FF571 File Offset: 0x002FD771
		// (set) Token: 0x06012C99 RID: 76953 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "hideLastTrans")]
		public ListValue<BooleanValue> HideLastTrans
		{
			get
			{
				return (ListValue<BooleanValue>)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17005C57 RID: 23639
		// (get) Token: 0x06012C9A RID: 76954 RVA: 0x002FF580 File Offset: 0x002FD780
		// (set) Token: 0x06012C9B RID: 76955 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "st")]
		public ListValue<Int32Value> Start
		{
			get
			{
				return (ListValue<Int32Value>)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17005C58 RID: 23640
		// (get) Token: 0x06012C9C RID: 76956 RVA: 0x002FF58F File Offset: 0x002FD78F
		// (set) Token: 0x06012C9D RID: 76957 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "cnt")]
		public ListValue<UInt32Value> Count
		{
			get
			{
				return (ListValue<UInt32Value>)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x17005C59 RID: 23641
		// (get) Token: 0x06012C9E RID: 76958 RVA: 0x002FF59E File Offset: 0x002FD79E
		// (set) Token: 0x06012C9F RID: 76959 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "step")]
		public ListValue<Int32Value> Step
		{
			get
			{
				return (ListValue<Int32Value>)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x17005C5A RID: 23642
		// (get) Token: 0x06012CA0 RID: 76960 RVA: 0x002FF5AD File Offset: 0x002FD7AD
		// (set) Token: 0x06012CA1 RID: 76961 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "func")]
		public EnumValue<FunctionValues> Function
		{
			get
			{
				return (EnumValue<FunctionValues>)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x17005C5B RID: 23643
		// (get) Token: 0x06012CA2 RID: 76962 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x06012CA3 RID: 76963 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "arg")]
		public StringValue Argument
		{
			get
			{
				return (StringValue)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x17005C5C RID: 23644
		// (get) Token: 0x06012CA4 RID: 76964 RVA: 0x002FF5BC File Offset: 0x002FD7BC
		// (set) Token: 0x06012CA5 RID: 76965 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "op")]
		public EnumValue<FunctionOperatorValues> Operator
		{
			get
			{
				return (EnumValue<FunctionOperatorValues>)base.Attributes[9];
			}
			set
			{
				base.Attributes[9] = value;
			}
		}

		// Token: 0x17005C5D RID: 23645
		// (get) Token: 0x06012CA6 RID: 76966 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x06012CA7 RID: 76967 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "val")]
		public StringValue Val
		{
			get
			{
				return (StringValue)base.Attributes[10];
			}
			set
			{
				base.Attributes[10] = value;
			}
		}

		// Token: 0x06012CA8 RID: 76968 RVA: 0x00293ECF File Offset: 0x002920CF
		public DiagramChooseIf()
		{
		}

		// Token: 0x06012CA9 RID: 76969 RVA: 0x00293ED7 File Offset: 0x002920D7
		public DiagramChooseIf(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012CAA RID: 76970 RVA: 0x00293EE0 File Offset: 0x002920E0
		public DiagramChooseIf(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012CAB RID: 76971 RVA: 0x00293EE9 File Offset: 0x002920E9
		public DiagramChooseIf(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012CAC RID: 76972 RVA: 0x002FF5CC File Offset: 0x002FD7CC
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

		// Token: 0x06012CAD RID: 76973 RVA: 0x002FF6B4 File Offset: 0x002FD8B4
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "name" == name)
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
			if (namespaceId == 0 && "func" == name)
			{
				return new EnumValue<FunctionValues>();
			}
			if (namespaceId == 0 && "arg" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "op" == name)
			{
				return new EnumValue<FunctionOperatorValues>();
			}
			if (namespaceId == 0 && "val" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06012CAE RID: 76974 RVA: 0x002FF7BB File Offset: 0x002FD9BB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DiagramChooseIf>(deep);
		}

		// Token: 0x06012CAF RID: 76975 RVA: 0x002FF7C4 File Offset: 0x002FD9C4
		// Note: this type is marked as 'beforefieldinit'.
		static DiagramChooseIf()
		{
			byte[] array = new byte[11];
			DiagramChooseIf.attributeNamespaceIds = array;
		}

		// Token: 0x04008193 RID: 33171
		private const string tagName = "if";

		// Token: 0x04008194 RID: 33172
		private const byte tagNsId = 14;

		// Token: 0x04008195 RID: 33173
		internal const int ElementTypeIdConst = 10659;

		// Token: 0x04008196 RID: 33174
		private static string[] attributeTagNames = new string[]
		{
			"name", "axis", "ptType", "hideLastTrans", "st", "cnt", "step", "func", "arg", "op",
			"val"
		};

		// Token: 0x04008197 RID: 33175
		private static byte[] attributeNamespaceIds;
	}
}
