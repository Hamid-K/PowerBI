using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C2B RID: 11307
	[ChildElementInfo(typeof(CalculatedColumnFormula))]
	[ChildElementInfo(typeof(TotalsRowFormula))]
	[ChildElementInfo(typeof(XmlColumnProperties))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ExtensionList))]
	internal class TableColumn : OpenXmlCompositeElement
	{
		// Token: 0x170080BD RID: 32957
		// (get) Token: 0x06017E42 RID: 97858 RVA: 0x0033C398 File Offset: 0x0033A598
		public override string LocalName
		{
			get
			{
				return "tableColumn";
			}
		}

		// Token: 0x170080BE RID: 32958
		// (get) Token: 0x06017E43 RID: 97859 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170080BF RID: 32959
		// (get) Token: 0x06017E44 RID: 97860 RVA: 0x0033C39F File Offset: 0x0033A59F
		internal override int ElementTypeId
		{
			get
			{
				return 11289;
			}
		}

		// Token: 0x06017E45 RID: 97861 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170080C0 RID: 32960
		// (get) Token: 0x06017E46 RID: 97862 RVA: 0x0033C3A6 File Offset: 0x0033A5A6
		internal override string[] AttributeTagNames
		{
			get
			{
				return TableColumn.attributeTagNames;
			}
		}

		// Token: 0x170080C1 RID: 32961
		// (get) Token: 0x06017E47 RID: 97863 RVA: 0x0033C3AD File Offset: 0x0033A5AD
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TableColumn.attributeNamespaceIds;
			}
		}

		// Token: 0x170080C2 RID: 32962
		// (get) Token: 0x06017E48 RID: 97864 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06017E49 RID: 97865 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "id")]
		public UInt32Value Id
		{
			get
			{
				return (UInt32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x170080C3 RID: 32963
		// (get) Token: 0x06017E4A RID: 97866 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06017E4B RID: 97867 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "uniqueName")]
		public StringValue UniqueName
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

		// Token: 0x170080C4 RID: 32964
		// (get) Token: 0x06017E4C RID: 97868 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x06017E4D RID: 97869 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "name")]
		public StringValue Name
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

		// Token: 0x170080C5 RID: 32965
		// (get) Token: 0x06017E4E RID: 97870 RVA: 0x0033C3B4 File Offset: 0x0033A5B4
		// (set) Token: 0x06017E4F RID: 97871 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "totalsRowFunction")]
		public EnumValue<TotalsRowFunctionValues> TotalsRowFunction
		{
			get
			{
				return (EnumValue<TotalsRowFunctionValues>)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x170080C6 RID: 32966
		// (get) Token: 0x06017E50 RID: 97872 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x06017E51 RID: 97873 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "totalsRowLabel")]
		public StringValue TotalsRowLabel
		{
			get
			{
				return (StringValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x170080C7 RID: 32967
		// (get) Token: 0x06017E52 RID: 97874 RVA: 0x002E6EEB File Offset: 0x002E50EB
		// (set) Token: 0x06017E53 RID: 97875 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "queryTableFieldId")]
		public UInt32Value QueryTableFieldId
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

		// Token: 0x170080C8 RID: 32968
		// (get) Token: 0x06017E54 RID: 97876 RVA: 0x002E6C60 File Offset: 0x002E4E60
		// (set) Token: 0x06017E55 RID: 97877 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "headerRowDxfId")]
		public UInt32Value HeaderRowDifferentialFormattingId
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

		// Token: 0x170080C9 RID: 32969
		// (get) Token: 0x06017E56 RID: 97878 RVA: 0x0032B268 File Offset: 0x00329468
		// (set) Token: 0x06017E57 RID: 97879 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "dataDxfId")]
		public UInt32Value DataFormatId
		{
			get
			{
				return (UInt32Value)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x170080CA RID: 32970
		// (get) Token: 0x06017E58 RID: 97880 RVA: 0x002F6806 File Offset: 0x002F4A06
		// (set) Token: 0x06017E59 RID: 97881 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "totalsRowDxfId")]
		public UInt32Value TotalsRowDifferentialFormattingId
		{
			get
			{
				return (UInt32Value)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x170080CB RID: 32971
		// (get) Token: 0x06017E5A RID: 97882 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x06017E5B RID: 97883 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "headerRowCellStyle")]
		public StringValue HeaderRowCellStyle
		{
			get
			{
				return (StringValue)base.Attributes[9];
			}
			set
			{
				base.Attributes[9] = value;
			}
		}

		// Token: 0x170080CC RID: 32972
		// (get) Token: 0x06017E5C RID: 97884 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x06017E5D RID: 97885 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "dataCellStyle")]
		public StringValue DataCellStyle
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

		// Token: 0x170080CD RID: 32973
		// (get) Token: 0x06017E5E RID: 97886 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x06017E5F RID: 97887 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "totalsRowCellStyle")]
		public StringValue TotalsRowCellStyle
		{
			get
			{
				return (StringValue)base.Attributes[11];
			}
			set
			{
				base.Attributes[11] = value;
			}
		}

		// Token: 0x06017E60 RID: 97888 RVA: 0x00293ECF File Offset: 0x002920CF
		public TableColumn()
		{
		}

		// Token: 0x06017E61 RID: 97889 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TableColumn(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017E62 RID: 97890 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TableColumn(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017E63 RID: 97891 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TableColumn(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06017E64 RID: 97892 RVA: 0x0033C3C4 File Offset: 0x0033A5C4
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "calculatedColumnFormula" == name)
			{
				return new CalculatedColumnFormula();
			}
			if (22 == namespaceId && "totalsRowFormula" == name)
			{
				return new TotalsRowFormula();
			}
			if (22 == namespaceId && "xmlColumnPr" == name)
			{
				return new XmlColumnProperties();
			}
			if (22 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x170080CE RID: 32974
		// (get) Token: 0x06017E65 RID: 97893 RVA: 0x0033C432 File Offset: 0x0033A632
		internal override string[] ElementTagNames
		{
			get
			{
				return TableColumn.eleTagNames;
			}
		}

		// Token: 0x170080CF RID: 32975
		// (get) Token: 0x06017E66 RID: 97894 RVA: 0x0033C439 File Offset: 0x0033A639
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return TableColumn.eleNamespaceIds;
			}
		}

		// Token: 0x170080D0 RID: 32976
		// (get) Token: 0x06017E67 RID: 97895 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170080D1 RID: 32977
		// (get) Token: 0x06017E68 RID: 97896 RVA: 0x0033C440 File Offset: 0x0033A640
		// (set) Token: 0x06017E69 RID: 97897 RVA: 0x0033C449 File Offset: 0x0033A649
		public CalculatedColumnFormula CalculatedColumnFormula
		{
			get
			{
				return base.GetElement<CalculatedColumnFormula>(0);
			}
			set
			{
				base.SetElement<CalculatedColumnFormula>(0, value);
			}
		}

		// Token: 0x170080D2 RID: 32978
		// (get) Token: 0x06017E6A RID: 97898 RVA: 0x0033C453 File Offset: 0x0033A653
		// (set) Token: 0x06017E6B RID: 97899 RVA: 0x0033C45C File Offset: 0x0033A65C
		public TotalsRowFormula TotalsRowFormula
		{
			get
			{
				return base.GetElement<TotalsRowFormula>(1);
			}
			set
			{
				base.SetElement<TotalsRowFormula>(1, value);
			}
		}

		// Token: 0x170080D3 RID: 32979
		// (get) Token: 0x06017E6C RID: 97900 RVA: 0x0033C466 File Offset: 0x0033A666
		// (set) Token: 0x06017E6D RID: 97901 RVA: 0x0033C46F File Offset: 0x0033A66F
		public XmlColumnProperties XmlColumnProperties
		{
			get
			{
				return base.GetElement<XmlColumnProperties>(2);
			}
			set
			{
				base.SetElement<XmlColumnProperties>(2, value);
			}
		}

		// Token: 0x170080D4 RID: 32980
		// (get) Token: 0x06017E6E RID: 97902 RVA: 0x00332E05 File Offset: 0x00331005
		// (set) Token: 0x06017E6F RID: 97903 RVA: 0x00332E0E File Offset: 0x0033100E
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

		// Token: 0x06017E70 RID: 97904 RVA: 0x0033C47C File Offset: 0x0033A67C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "id" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "uniqueName" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "totalsRowFunction" == name)
			{
				return new EnumValue<TotalsRowFunctionValues>();
			}
			if (namespaceId == 0 && "totalsRowLabel" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "queryTableFieldId" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "headerRowDxfId" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "dataDxfId" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "totalsRowDxfId" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "headerRowCellStyle" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "dataCellStyle" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "totalsRowCellStyle" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017E71 RID: 97905 RVA: 0x0033C599 File Offset: 0x0033A799
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TableColumn>(deep);
		}

		// Token: 0x06017E72 RID: 97906 RVA: 0x0033C5A4 File Offset: 0x0033A7A4
		// Note: this type is marked as 'beforefieldinit'.
		static TableColumn()
		{
			byte[] array = new byte[12];
			TableColumn.attributeNamespaceIds = array;
			TableColumn.eleTagNames = new string[] { "calculatedColumnFormula", "totalsRowFormula", "xmlColumnPr", "extLst" };
			TableColumn.eleNamespaceIds = new byte[] { 22, 22, 22, 22 };
		}

		// Token: 0x04009E0A RID: 40458
		private const string tagName = "tableColumn";

		// Token: 0x04009E0B RID: 40459
		private const byte tagNsId = 22;

		// Token: 0x04009E0C RID: 40460
		internal const int ElementTypeIdConst = 11289;

		// Token: 0x04009E0D RID: 40461
		private static string[] attributeTagNames = new string[]
		{
			"id", "uniqueName", "name", "totalsRowFunction", "totalsRowLabel", "queryTableFieldId", "headerRowDxfId", "dataDxfId", "totalsRowDxfId", "headerRowCellStyle",
			"dataCellStyle", "totalsRowCellStyle"
		};

		// Token: 0x04009E0E RID: 40462
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009E0F RID: 40463
		private static readonly string[] eleTagNames;

		// Token: 0x04009E10 RID: 40464
		private static readonly byte[] eleNamespaceIds;
	}
}
