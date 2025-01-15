using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BC4 RID: 11204
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(CellValue))]
	[ChildElementInfo(typeof(InlineString))]
	[ChildElementInfo(typeof(CellFormula))]
	internal class NewCell : OpenXmlCompositeElement
	{
		// Token: 0x17007C91 RID: 31889
		// (get) Token: 0x06017546 RID: 95558 RVA: 0x003357B0 File Offset: 0x003339B0
		public override string LocalName
		{
			get
			{
				return "nc";
			}
		}

		// Token: 0x17007C92 RID: 31890
		// (get) Token: 0x06017547 RID: 95559 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007C93 RID: 31891
		// (get) Token: 0x06017548 RID: 95560 RVA: 0x003357B7 File Offset: 0x003339B7
		internal override int ElementTypeId
		{
			get
			{
				return 11173;
			}
		}

		// Token: 0x06017549 RID: 95561 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007C94 RID: 31892
		// (get) Token: 0x0601754A RID: 95562 RVA: 0x003357BE File Offset: 0x003339BE
		internal override string[] AttributeTagNames
		{
			get
			{
				return NewCell.attributeTagNames;
			}
		}

		// Token: 0x17007C95 RID: 31893
		// (get) Token: 0x0601754B RID: 95563 RVA: 0x003357C5 File Offset: 0x003339C5
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return NewCell.attributeNamespaceIds;
			}
		}

		// Token: 0x17007C96 RID: 31894
		// (get) Token: 0x0601754C RID: 95564 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601754D RID: 95565 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "r")]
		public StringValue CellReference
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

		// Token: 0x17007C97 RID: 31895
		// (get) Token: 0x0601754E RID: 95566 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x0601754F RID: 95567 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "s")]
		public UInt32Value StyleIndex
		{
			get
			{
				return (UInt32Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17007C98 RID: 31896
		// (get) Token: 0x06017550 RID: 95568 RVA: 0x00335569 File Offset: 0x00333769
		// (set) Token: 0x06017551 RID: 95569 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "t")]
		public EnumValue<CellValues> DataType
		{
			get
			{
				return (EnumValue<CellValues>)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17007C99 RID: 31897
		// (get) Token: 0x06017552 RID: 95570 RVA: 0x002E5B0D File Offset: 0x002E3D0D
		// (set) Token: 0x06017553 RID: 95571 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "cm")]
		public UInt32Value CellMetaIndex
		{
			get
			{
				return (UInt32Value)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17007C9A RID: 31898
		// (get) Token: 0x06017554 RID: 95572 RVA: 0x002E6C42 File Offset: 0x002E4E42
		// (set) Token: 0x06017555 RID: 95573 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "vm")]
		public UInt32Value ValueMetaIndex
		{
			get
			{
				return (UInt32Value)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17007C9B RID: 31899
		// (get) Token: 0x06017556 RID: 95574 RVA: 0x002D7D70 File Offset: 0x002D5F70
		// (set) Token: 0x06017557 RID: 95575 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "ph")]
		public BooleanValue ShowPhonetic
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

		// Token: 0x06017558 RID: 95576 RVA: 0x00293ECF File Offset: 0x002920CF
		public NewCell()
		{
		}

		// Token: 0x06017559 RID: 95577 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NewCell(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601755A RID: 95578 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NewCell(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601755B RID: 95579 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NewCell(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601755C RID: 95580 RVA: 0x003357CC File Offset: 0x003339CC
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "f" == name)
			{
				return new CellFormula();
			}
			if (22 == namespaceId && "v" == name)
			{
				return new CellValue();
			}
			if (22 == namespaceId && "is" == name)
			{
				return new InlineString();
			}
			if (22 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17007C9C RID: 31900
		// (get) Token: 0x0601755D RID: 95581 RVA: 0x0033583A File Offset: 0x00333A3A
		internal override string[] ElementTagNames
		{
			get
			{
				return NewCell.eleTagNames;
			}
		}

		// Token: 0x17007C9D RID: 31901
		// (get) Token: 0x0601755E RID: 95582 RVA: 0x00335841 File Offset: 0x00333A41
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NewCell.eleNamespaceIds;
			}
		}

		// Token: 0x17007C9E RID: 31902
		// (get) Token: 0x0601755F RID: 95583 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17007C9F RID: 31903
		// (get) Token: 0x06017560 RID: 95584 RVA: 0x003355F4 File Offset: 0x003337F4
		// (set) Token: 0x06017561 RID: 95585 RVA: 0x003355FD File Offset: 0x003337FD
		public CellFormula CellFormula
		{
			get
			{
				return base.GetElement<CellFormula>(0);
			}
			set
			{
				base.SetElement<CellFormula>(0, value);
			}
		}

		// Token: 0x17007CA0 RID: 31904
		// (get) Token: 0x06017562 RID: 95586 RVA: 0x00335607 File Offset: 0x00333807
		// (set) Token: 0x06017563 RID: 95587 RVA: 0x00335610 File Offset: 0x00333810
		public CellValue CellValue
		{
			get
			{
				return base.GetElement<CellValue>(1);
			}
			set
			{
				base.SetElement<CellValue>(1, value);
			}
		}

		// Token: 0x17007CA1 RID: 31905
		// (get) Token: 0x06017564 RID: 95588 RVA: 0x0033561A File Offset: 0x0033381A
		// (set) Token: 0x06017565 RID: 95589 RVA: 0x00335623 File Offset: 0x00333823
		public InlineString InlineString
		{
			get
			{
				return base.GetElement<InlineString>(2);
			}
			set
			{
				base.SetElement<InlineString>(2, value);
			}
		}

		// Token: 0x17007CA2 RID: 31906
		// (get) Token: 0x06017566 RID: 95590 RVA: 0x00332E05 File Offset: 0x00331005
		// (set) Token: 0x06017567 RID: 95591 RVA: 0x00332E0E File Offset: 0x0033100E
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(3);
			}
			set
			{
				base.SetElement<ExtensionList>(3, value);
			}
		}

		// Token: 0x06017568 RID: 95592 RVA: 0x00335848 File Offset: 0x00333A48
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "r" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "s" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "t" == name)
			{
				return new EnumValue<CellValues>();
			}
			if (namespaceId == 0 && "cm" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "vm" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "ph" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017569 RID: 95593 RVA: 0x003358E1 File Offset: 0x00333AE1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NewCell>(deep);
		}

		// Token: 0x0601756A RID: 95594 RVA: 0x003358EC File Offset: 0x00333AEC
		// Note: this type is marked as 'beforefieldinit'.
		static NewCell()
		{
			byte[] array = new byte[6];
			NewCell.attributeNamespaceIds = array;
			NewCell.eleTagNames = new string[] { "f", "v", "is", "extLst" };
			NewCell.eleNamespaceIds = new byte[] { 22, 22, 22, 22 };
		}

		// Token: 0x04009BF8 RID: 39928
		private const string tagName = "nc";

		// Token: 0x04009BF9 RID: 39929
		private const byte tagNsId = 22;

		// Token: 0x04009BFA RID: 39930
		internal const int ElementTypeIdConst = 11173;

		// Token: 0x04009BFB RID: 39931
		private static string[] attributeTagNames = new string[] { "r", "s", "t", "cm", "vm", "ph" };

		// Token: 0x04009BFC RID: 39932
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009BFD RID: 39933
		private static readonly string[] eleTagNames;

		// Token: 0x04009BFE RID: 39934
		private static readonly byte[] eleNamespaceIds;
	}
}
