using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B8E RID: 11150
	[ChildElementInfo(typeof(SortState))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(QueryTableDeletedFields))]
	[ChildElementInfo(typeof(ExtensionList))]
	[ChildElementInfo(typeof(QueryTableFields))]
	internal class QueryTableRefresh : OpenXmlCompositeElement
	{
		// Token: 0x17007ADF RID: 31455
		// (get) Token: 0x060171A5 RID: 94629 RVA: 0x00332D25 File Offset: 0x00330F25
		public override string LocalName
		{
			get
			{
				return "queryTableRefresh";
			}
		}

		// Token: 0x17007AE0 RID: 31456
		// (get) Token: 0x060171A6 RID: 94630 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007AE1 RID: 31457
		// (get) Token: 0x060171A7 RID: 94631 RVA: 0x00332D2C File Offset: 0x00330F2C
		internal override int ElementTypeId
		{
			get
			{
				return 11129;
			}
		}

		// Token: 0x060171A8 RID: 94632 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007AE2 RID: 31458
		// (get) Token: 0x060171A9 RID: 94633 RVA: 0x00332D33 File Offset: 0x00330F33
		internal override string[] AttributeTagNames
		{
			get
			{
				return QueryTableRefresh.attributeTagNames;
			}
		}

		// Token: 0x17007AE3 RID: 31459
		// (get) Token: 0x060171AA RID: 94634 RVA: 0x00332D3A File Offset: 0x00330F3A
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return QueryTableRefresh.attributeNamespaceIds;
			}
		}

		// Token: 0x17007AE4 RID: 31460
		// (get) Token: 0x060171AB RID: 94635 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x060171AC RID: 94636 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "preserveSortFilterLayout")]
		public BooleanValue PreserveSortFilterLayout
		{
			get
			{
				return (BooleanValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17007AE5 RID: 31461
		// (get) Token: 0x060171AD RID: 94637 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x060171AE RID: 94638 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "fieldIdWrapped")]
		public BooleanValue FieldIdWrapped
		{
			get
			{
				return (BooleanValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17007AE6 RID: 31462
		// (get) Token: 0x060171AF RID: 94639 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x060171B0 RID: 94640 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "headersInLastRefresh")]
		public BooleanValue HeadersInLastRefresh
		{
			get
			{
				return (BooleanValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17007AE7 RID: 31463
		// (get) Token: 0x060171B1 RID: 94641 RVA: 0x00332D41 File Offset: 0x00330F41
		// (set) Token: 0x060171B2 RID: 94642 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "minimumVersion")]
		public ByteValue MinimumVersion
		{
			get
			{
				return (ByteValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17007AE8 RID: 31464
		// (get) Token: 0x060171B3 RID: 94643 RVA: 0x002E6C42 File Offset: 0x002E4E42
		// (set) Token: 0x060171B4 RID: 94644 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "nextId")]
		public UInt32Value NextId
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

		// Token: 0x17007AE9 RID: 31465
		// (get) Token: 0x060171B5 RID: 94645 RVA: 0x002E6EEB File Offset: 0x002E50EB
		// (set) Token: 0x060171B6 RID: 94646 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "unboundColumnsLeft")]
		public UInt32Value UnboundColumnsLeft
		{
			get
			{
				return (UInt32Value)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x17007AEA RID: 31466
		// (get) Token: 0x060171B7 RID: 94647 RVA: 0x002E6C60 File Offset: 0x002E4E60
		// (set) Token: 0x060171B8 RID: 94648 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "unboundColumnsRight")]
		public UInt32Value UnboundColumnsRight
		{
			get
			{
				return (UInt32Value)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x060171B9 RID: 94649 RVA: 0x00293ECF File Offset: 0x002920CF
		public QueryTableRefresh()
		{
		}

		// Token: 0x060171BA RID: 94650 RVA: 0x00293ED7 File Offset: 0x002920D7
		public QueryTableRefresh(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060171BB RID: 94651 RVA: 0x00293EE0 File Offset: 0x002920E0
		public QueryTableRefresh(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060171BC RID: 94652 RVA: 0x00293EE9 File Offset: 0x002920E9
		public QueryTableRefresh(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060171BD RID: 94653 RVA: 0x00332D50 File Offset: 0x00330F50
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "queryTableFields" == name)
			{
				return new QueryTableFields();
			}
			if (22 == namespaceId && "queryTableDeletedFields" == name)
			{
				return new QueryTableDeletedFields();
			}
			if (22 == namespaceId && "sortState" == name)
			{
				return new SortState();
			}
			if (22 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17007AEB RID: 31467
		// (get) Token: 0x060171BE RID: 94654 RVA: 0x00332DBE File Offset: 0x00330FBE
		internal override string[] ElementTagNames
		{
			get
			{
				return QueryTableRefresh.eleTagNames;
			}
		}

		// Token: 0x17007AEC RID: 31468
		// (get) Token: 0x060171BF RID: 94655 RVA: 0x00332DC5 File Offset: 0x00330FC5
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return QueryTableRefresh.eleNamespaceIds;
			}
		}

		// Token: 0x17007AED RID: 31469
		// (get) Token: 0x060171C0 RID: 94656 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17007AEE RID: 31470
		// (get) Token: 0x060171C1 RID: 94657 RVA: 0x00332DCC File Offset: 0x00330FCC
		// (set) Token: 0x060171C2 RID: 94658 RVA: 0x00332DD5 File Offset: 0x00330FD5
		public QueryTableFields QueryTableFields
		{
			get
			{
				return base.GetElement<QueryTableFields>(0);
			}
			set
			{
				base.SetElement<QueryTableFields>(0, value);
			}
		}

		// Token: 0x17007AEF RID: 31471
		// (get) Token: 0x060171C3 RID: 94659 RVA: 0x00332DDF File Offset: 0x00330FDF
		// (set) Token: 0x060171C4 RID: 94660 RVA: 0x00332DE8 File Offset: 0x00330FE8
		public QueryTableDeletedFields QueryTableDeletedFields
		{
			get
			{
				return base.GetElement<QueryTableDeletedFields>(1);
			}
			set
			{
				base.SetElement<QueryTableDeletedFields>(1, value);
			}
		}

		// Token: 0x17007AF0 RID: 31472
		// (get) Token: 0x060171C5 RID: 94661 RVA: 0x00332DF2 File Offset: 0x00330FF2
		// (set) Token: 0x060171C6 RID: 94662 RVA: 0x00332DFB File Offset: 0x00330FFB
		public SortState SortState
		{
			get
			{
				return base.GetElement<SortState>(2);
			}
			set
			{
				base.SetElement<SortState>(2, value);
			}
		}

		// Token: 0x17007AF1 RID: 31473
		// (get) Token: 0x060171C7 RID: 94663 RVA: 0x00332E05 File Offset: 0x00331005
		// (set) Token: 0x060171C8 RID: 94664 RVA: 0x00332E0E File Offset: 0x0033100E
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

		// Token: 0x060171C9 RID: 94665 RVA: 0x00332E18 File Offset: 0x00331018
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "preserveSortFilterLayout" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "fieldIdWrapped" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "headersInLastRefresh" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "minimumVersion" == name)
			{
				return new ByteValue();
			}
			if (namespaceId == 0 && "nextId" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "unboundColumnsLeft" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "unboundColumnsRight" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060171CA RID: 94666 RVA: 0x00332EC7 File Offset: 0x003310C7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<QueryTableRefresh>(deep);
		}

		// Token: 0x060171CB RID: 94667 RVA: 0x00332ED0 File Offset: 0x003310D0
		// Note: this type is marked as 'beforefieldinit'.
		static QueryTableRefresh()
		{
			byte[] array = new byte[7];
			QueryTableRefresh.attributeNamespaceIds = array;
			QueryTableRefresh.eleTagNames = new string[] { "queryTableFields", "queryTableDeletedFields", "sortState", "extLst" };
			QueryTableRefresh.eleNamespaceIds = new byte[] { 22, 22, 22, 22 };
		}

		// Token: 0x04009B0D RID: 39693
		private const string tagName = "queryTableRefresh";

		// Token: 0x04009B0E RID: 39694
		private const byte tagNsId = 22;

		// Token: 0x04009B0F RID: 39695
		internal const int ElementTypeIdConst = 11129;

		// Token: 0x04009B10 RID: 39696
		private static string[] attributeTagNames = new string[] { "preserveSortFilterLayout", "fieldIdWrapped", "headersInLastRefresh", "minimumVersion", "nextId", "unboundColumnsLeft", "unboundColumnsRight" };

		// Token: 0x04009B11 RID: 39697
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009B12 RID: 39698
		private static readonly string[] eleTagNames;

		// Token: 0x04009B13 RID: 39699
		private static readonly byte[] eleNamespaceIds;
	}
}
