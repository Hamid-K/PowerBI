using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C15 RID: 11285
	[ChildElementInfo(typeof(ExtensionList))]
	[ChildElementInfo(typeof(Protection))]
	[ChildElementInfo(typeof(Alignment))]
	[GeneratedCode("DomGen", "2.0")]
	internal class CellFormat : OpenXmlCompositeElement
	{
		// Token: 0x17008012 RID: 32786
		// (get) Token: 0x06017CC3 RID: 97475 RVA: 0x0033B4E6 File Offset: 0x003396E6
		public override string LocalName
		{
			get
			{
				return "xf";
			}
		}

		// Token: 0x17008013 RID: 32787
		// (get) Token: 0x06017CC4 RID: 97476 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008014 RID: 32788
		// (get) Token: 0x06017CC5 RID: 97477 RVA: 0x0033B4ED File Offset: 0x003396ED
		internal override int ElementTypeId
		{
			get
			{
				return 11266;
			}
		}

		// Token: 0x06017CC6 RID: 97478 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008015 RID: 32789
		// (get) Token: 0x06017CC7 RID: 97479 RVA: 0x0033B4F4 File Offset: 0x003396F4
		internal override string[] AttributeTagNames
		{
			get
			{
				return CellFormat.attributeTagNames;
			}
		}

		// Token: 0x17008016 RID: 32790
		// (get) Token: 0x06017CC8 RID: 97480 RVA: 0x0033B4FB File Offset: 0x003396FB
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return CellFormat.attributeNamespaceIds;
			}
		}

		// Token: 0x17008017 RID: 32791
		// (get) Token: 0x06017CC9 RID: 97481 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06017CCA RID: 97482 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "numFmtId")]
		public UInt32Value NumberFormatId
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

		// Token: 0x17008018 RID: 32792
		// (get) Token: 0x06017CCB RID: 97483 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x06017CCC RID: 97484 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "fontId")]
		public UInt32Value FontId
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

		// Token: 0x17008019 RID: 32793
		// (get) Token: 0x06017CCD RID: 97485 RVA: 0x002E5814 File Offset: 0x002E3A14
		// (set) Token: 0x06017CCE RID: 97486 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "fillId")]
		public UInt32Value FillId
		{
			get
			{
				return (UInt32Value)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x1700801A RID: 32794
		// (get) Token: 0x06017CCF RID: 97487 RVA: 0x002E5B0D File Offset: 0x002E3D0D
		// (set) Token: 0x06017CD0 RID: 97488 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "borderId")]
		public UInt32Value BorderId
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

		// Token: 0x1700801B RID: 32795
		// (get) Token: 0x06017CD1 RID: 97489 RVA: 0x002E6C42 File Offset: 0x002E4E42
		// (set) Token: 0x06017CD2 RID: 97490 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "xfId")]
		public UInt32Value FormatId
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

		// Token: 0x1700801C RID: 32796
		// (get) Token: 0x06017CD3 RID: 97491 RVA: 0x002D7D70 File Offset: 0x002D5F70
		// (set) Token: 0x06017CD4 RID: 97492 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "quotePrefix")]
		public BooleanValue QuotePrefix
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

		// Token: 0x1700801D RID: 32797
		// (get) Token: 0x06017CD5 RID: 97493 RVA: 0x002CCC33 File Offset: 0x002CAE33
		// (set) Token: 0x06017CD6 RID: 97494 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "pivotButton")]
		public BooleanValue PivotButton
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

		// Token: 0x1700801E RID: 32798
		// (get) Token: 0x06017CD7 RID: 97495 RVA: 0x002CA736 File Offset: 0x002C8936
		// (set) Token: 0x06017CD8 RID: 97496 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "applyNumberFormat")]
		public BooleanValue ApplyNumberFormat
		{
			get
			{
				return (BooleanValue)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x1700801F RID: 32799
		// (get) Token: 0x06017CD9 RID: 97497 RVA: 0x002CB706 File Offset: 0x002C9906
		// (set) Token: 0x06017CDA RID: 97498 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "applyFont")]
		public BooleanValue ApplyFont
		{
			get
			{
				return (BooleanValue)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x17008020 RID: 32800
		// (get) Token: 0x06017CDB RID: 97499 RVA: 0x002C92EA File Offset: 0x002C74EA
		// (set) Token: 0x06017CDC RID: 97500 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "applyFill")]
		public BooleanValue ApplyFill
		{
			get
			{
				return (BooleanValue)base.Attributes[9];
			}
			set
			{
				base.Attributes[9] = value;
			}
		}

		// Token: 0x17008021 RID: 32801
		// (get) Token: 0x06017CDD RID: 97501 RVA: 0x002C8762 File Offset: 0x002C6962
		// (set) Token: 0x06017CDE RID: 97502 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "applyBorder")]
		public BooleanValue ApplyBorder
		{
			get
			{
				return (BooleanValue)base.Attributes[10];
			}
			set
			{
				base.Attributes[10] = value;
			}
		}

		// Token: 0x17008022 RID: 32802
		// (get) Token: 0x06017CDF RID: 97503 RVA: 0x002C92FA File Offset: 0x002C74FA
		// (set) Token: 0x06017CE0 RID: 97504 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "applyAlignment")]
		public BooleanValue ApplyAlignment
		{
			get
			{
				return (BooleanValue)base.Attributes[11];
			}
			set
			{
				base.Attributes[11] = value;
			}
		}

		// Token: 0x17008023 RID: 32803
		// (get) Token: 0x06017CE1 RID: 97505 RVA: 0x002CE196 File Offset: 0x002CC396
		// (set) Token: 0x06017CE2 RID: 97506 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "applyProtection")]
		public BooleanValue ApplyProtection
		{
			get
			{
				return (BooleanValue)base.Attributes[12];
			}
			set
			{
				base.Attributes[12] = value;
			}
		}

		// Token: 0x06017CE3 RID: 97507 RVA: 0x00293ECF File Offset: 0x002920CF
		public CellFormat()
		{
		}

		// Token: 0x06017CE4 RID: 97508 RVA: 0x00293ED7 File Offset: 0x002920D7
		public CellFormat(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017CE5 RID: 97509 RVA: 0x00293EE0 File Offset: 0x002920E0
		public CellFormat(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017CE6 RID: 97510 RVA: 0x00293EE9 File Offset: 0x002920E9
		public CellFormat(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06017CE7 RID: 97511 RVA: 0x0033B504 File Offset: 0x00339704
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "alignment" == name)
			{
				return new Alignment();
			}
			if (22 == namespaceId && "protection" == name)
			{
				return new Protection();
			}
			if (22 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17008024 RID: 32804
		// (get) Token: 0x06017CE8 RID: 97512 RVA: 0x0033B55A File Offset: 0x0033975A
		internal override string[] ElementTagNames
		{
			get
			{
				return CellFormat.eleTagNames;
			}
		}

		// Token: 0x17008025 RID: 32805
		// (get) Token: 0x06017CE9 RID: 97513 RVA: 0x0033B561 File Offset: 0x00339761
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return CellFormat.eleNamespaceIds;
			}
		}

		// Token: 0x17008026 RID: 32806
		// (get) Token: 0x06017CEA RID: 97514 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17008027 RID: 32807
		// (get) Token: 0x06017CEB RID: 97515 RVA: 0x0033B568 File Offset: 0x00339768
		// (set) Token: 0x06017CEC RID: 97516 RVA: 0x0033B571 File Offset: 0x00339771
		public Alignment Alignment
		{
			get
			{
				return base.GetElement<Alignment>(0);
			}
			set
			{
				base.SetElement<Alignment>(0, value);
			}
		}

		// Token: 0x17008028 RID: 32808
		// (get) Token: 0x06017CED RID: 97517 RVA: 0x0033B57B File Offset: 0x0033977B
		// (set) Token: 0x06017CEE RID: 97518 RVA: 0x0033B584 File Offset: 0x00339784
		public Protection Protection
		{
			get
			{
				return base.GetElement<Protection>(1);
			}
			set
			{
				base.SetElement<Protection>(1, value);
			}
		}

		// Token: 0x17008029 RID: 32809
		// (get) Token: 0x06017CEF RID: 97519 RVA: 0x00329822 File Offset: 0x00327A22
		// (set) Token: 0x06017CF0 RID: 97520 RVA: 0x0032982B File Offset: 0x00327A2B
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(2);
			}
			set
			{
				base.SetElement<ExtensionList>(2, value);
			}
		}

		// Token: 0x06017CF1 RID: 97521 RVA: 0x0033B590 File Offset: 0x00339790
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "numFmtId" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "fontId" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "fillId" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "borderId" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "xfId" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "quotePrefix" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "pivotButton" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "applyNumberFormat" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "applyFont" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "applyFill" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "applyBorder" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "applyAlignment" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "applyProtection" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017CF2 RID: 97522 RVA: 0x0033B6C3 File Offset: 0x003398C3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CellFormat>(deep);
		}

		// Token: 0x06017CF3 RID: 97523 RVA: 0x0033B6CC File Offset: 0x003398CC
		// Note: this type is marked as 'beforefieldinit'.
		static CellFormat()
		{
			byte[] array = new byte[13];
			CellFormat.attributeNamespaceIds = array;
			CellFormat.eleTagNames = new string[] { "alignment", "protection", "extLst" };
			CellFormat.eleNamespaceIds = new byte[] { 22, 22, 22 };
		}

		// Token: 0x04009D9A RID: 40346
		private const string tagName = "xf";

		// Token: 0x04009D9B RID: 40347
		private const byte tagNsId = 22;

		// Token: 0x04009D9C RID: 40348
		internal const int ElementTypeIdConst = 11266;

		// Token: 0x04009D9D RID: 40349
		private static string[] attributeTagNames = new string[]
		{
			"numFmtId", "fontId", "fillId", "borderId", "xfId", "quotePrefix", "pivotButton", "applyNumberFormat", "applyFont", "applyFill",
			"applyBorder", "applyAlignment", "applyProtection"
		};

		// Token: 0x04009D9E RID: 40350
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009D9F RID: 40351
		private static readonly string[] eleTagNames;

		// Token: 0x04009DA0 RID: 40352
		private static readonly byte[] eleNamespaceIds;
	}
}
