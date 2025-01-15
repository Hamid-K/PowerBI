using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BB6 RID: 11190
	[ChildElementInfo(typeof(NewCell))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ExtensionList))]
	[ChildElementInfo(typeof(NewDifferentialFormat))]
	[ChildElementInfo(typeof(OldCell))]
	[ChildElementInfo(typeof(OldDifferentialFormat))]
	internal class RevisionCellChange : OpenXmlCompositeElement
	{
		// Token: 0x17007BDD RID: 31709
		// (get) Token: 0x060173CB RID: 95179 RVA: 0x00334467 File Offset: 0x00332667
		public override string LocalName
		{
			get
			{
				return "rcc";
			}
		}

		// Token: 0x17007BDE RID: 31710
		// (get) Token: 0x060173CC RID: 95180 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007BDF RID: 31711
		// (get) Token: 0x060173CD RID: 95181 RVA: 0x0033446E File Offset: 0x0033266E
		internal override int ElementTypeId
		{
			get
			{
				return 11161;
			}
		}

		// Token: 0x060173CE RID: 95182 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007BE0 RID: 31712
		// (get) Token: 0x060173CF RID: 95183 RVA: 0x00334475 File Offset: 0x00332675
		internal override string[] AttributeTagNames
		{
			get
			{
				return RevisionCellChange.attributeTagNames;
			}
		}

		// Token: 0x17007BE1 RID: 31713
		// (get) Token: 0x060173D0 RID: 95184 RVA: 0x0033447C File Offset: 0x0033267C
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return RevisionCellChange.attributeNamespaceIds;
			}
		}

		// Token: 0x17007BE2 RID: 31714
		// (get) Token: 0x060173D1 RID: 95185 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x060173D2 RID: 95186 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "rId")]
		public UInt32Value RevisionId
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

		// Token: 0x17007BE3 RID: 31715
		// (get) Token: 0x060173D3 RID: 95187 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x060173D4 RID: 95188 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "ua")]
		public BooleanValue Ua
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

		// Token: 0x17007BE4 RID: 31716
		// (get) Token: 0x060173D5 RID: 95189 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x060173D6 RID: 95190 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "ra")]
		public BooleanValue Ra
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

		// Token: 0x17007BE5 RID: 31717
		// (get) Token: 0x060173D7 RID: 95191 RVA: 0x002E5B0D File Offset: 0x002E3D0D
		// (set) Token: 0x060173D8 RID: 95192 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "sId")]
		public UInt32Value SheetId
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

		// Token: 0x17007BE6 RID: 31718
		// (get) Token: 0x060173D9 RID: 95193 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x060173DA RID: 95194 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "odxf")]
		public BooleanValue OldFormatting
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

		// Token: 0x17007BE7 RID: 31719
		// (get) Token: 0x060173DB RID: 95195 RVA: 0x002D7D70 File Offset: 0x002D5F70
		// (set) Token: 0x060173DC RID: 95196 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "xfDxf")]
		public BooleanValue RowColumnFormattingAffected
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

		// Token: 0x17007BE8 RID: 31720
		// (get) Token: 0x060173DD RID: 95197 RVA: 0x002CCC33 File Offset: 0x002CAE33
		// (set) Token: 0x060173DE RID: 95198 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "s")]
		public BooleanValue StyleRevision
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

		// Token: 0x17007BE9 RID: 31721
		// (get) Token: 0x060173DF RID: 95199 RVA: 0x002CA736 File Offset: 0x002C8936
		// (set) Token: 0x060173E0 RID: 95200 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "dxf")]
		public BooleanValue Format
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

		// Token: 0x17007BEA RID: 31722
		// (get) Token: 0x060173E1 RID: 95201 RVA: 0x002F6806 File Offset: 0x002F4A06
		// (set) Token: 0x060173E2 RID: 95202 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "numFmtId")]
		public UInt32Value NumberFormatId
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

		// Token: 0x17007BEB RID: 31723
		// (get) Token: 0x060173E3 RID: 95203 RVA: 0x002C92EA File Offset: 0x002C74EA
		// (set) Token: 0x060173E4 RID: 95204 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "quotePrefix")]
		public BooleanValue QuotePrefix
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

		// Token: 0x17007BEC RID: 31724
		// (get) Token: 0x060173E5 RID: 95205 RVA: 0x002C8762 File Offset: 0x002C6962
		// (set) Token: 0x060173E6 RID: 95206 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "oldQuotePrefix")]
		public BooleanValue OldQuotePrefix
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

		// Token: 0x17007BED RID: 31725
		// (get) Token: 0x060173E7 RID: 95207 RVA: 0x002C92FA File Offset: 0x002C74FA
		// (set) Token: 0x060173E8 RID: 95208 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "ph")]
		public BooleanValue HasPhoneticText
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

		// Token: 0x17007BEE RID: 31726
		// (get) Token: 0x060173E9 RID: 95209 RVA: 0x002CE196 File Offset: 0x002CC396
		// (set) Token: 0x060173EA RID: 95210 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "oldPh")]
		public BooleanValue OldPhoneticText
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

		// Token: 0x17007BEF RID: 31727
		// (get) Token: 0x060173EB RID: 95211 RVA: 0x002CD15F File Offset: 0x002CB35F
		// (set) Token: 0x060173EC RID: 95212 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "endOfListFormulaUpdate")]
		public BooleanValue EndOfListFormulaUpdate
		{
			get
			{
				return (BooleanValue)base.Attributes[13];
			}
			set
			{
				base.Attributes[13] = value;
			}
		}

		// Token: 0x060173ED RID: 95213 RVA: 0x00293ECF File Offset: 0x002920CF
		public RevisionCellChange()
		{
		}

		// Token: 0x060173EE RID: 95214 RVA: 0x00293ED7 File Offset: 0x002920D7
		public RevisionCellChange(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060173EF RID: 95215 RVA: 0x00293EE0 File Offset: 0x002920E0
		public RevisionCellChange(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060173F0 RID: 95216 RVA: 0x00293EE9 File Offset: 0x002920E9
		public RevisionCellChange(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060173F1 RID: 95217 RVA: 0x00334484 File Offset: 0x00332684
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "oc" == name)
			{
				return new OldCell();
			}
			if (22 == namespaceId && "nc" == name)
			{
				return new NewCell();
			}
			if (22 == namespaceId && "odxf" == name)
			{
				return new OldDifferentialFormat();
			}
			if (22 == namespaceId && "ndxf" == name)
			{
				return new NewDifferentialFormat();
			}
			if (22 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17007BF0 RID: 31728
		// (get) Token: 0x060173F2 RID: 95218 RVA: 0x0033450A File Offset: 0x0033270A
		internal override string[] ElementTagNames
		{
			get
			{
				return RevisionCellChange.eleTagNames;
			}
		}

		// Token: 0x17007BF1 RID: 31729
		// (get) Token: 0x060173F3 RID: 95219 RVA: 0x00334511 File Offset: 0x00332711
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return RevisionCellChange.eleNamespaceIds;
			}
		}

		// Token: 0x17007BF2 RID: 31730
		// (get) Token: 0x060173F4 RID: 95220 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17007BF3 RID: 31731
		// (get) Token: 0x060173F5 RID: 95221 RVA: 0x00334518 File Offset: 0x00332718
		// (set) Token: 0x060173F6 RID: 95222 RVA: 0x00334521 File Offset: 0x00332721
		public OldCell OldCell
		{
			get
			{
				return base.GetElement<OldCell>(0);
			}
			set
			{
				base.SetElement<OldCell>(0, value);
			}
		}

		// Token: 0x17007BF4 RID: 31732
		// (get) Token: 0x060173F7 RID: 95223 RVA: 0x0033452B File Offset: 0x0033272B
		// (set) Token: 0x060173F8 RID: 95224 RVA: 0x00334534 File Offset: 0x00332734
		public NewCell NewCell
		{
			get
			{
				return base.GetElement<NewCell>(1);
			}
			set
			{
				base.SetElement<NewCell>(1, value);
			}
		}

		// Token: 0x17007BF5 RID: 31733
		// (get) Token: 0x060173F9 RID: 95225 RVA: 0x0033453E File Offset: 0x0033273E
		// (set) Token: 0x060173FA RID: 95226 RVA: 0x00334547 File Offset: 0x00332747
		public OldDifferentialFormat OldDifferentialFormat
		{
			get
			{
				return base.GetElement<OldDifferentialFormat>(2);
			}
			set
			{
				base.SetElement<OldDifferentialFormat>(2, value);
			}
		}

		// Token: 0x17007BF6 RID: 31734
		// (get) Token: 0x060173FB RID: 95227 RVA: 0x00334551 File Offset: 0x00332751
		// (set) Token: 0x060173FC RID: 95228 RVA: 0x0033455A File Offset: 0x0033275A
		public NewDifferentialFormat NewDifferentialFormat
		{
			get
			{
				return base.GetElement<NewDifferentialFormat>(3);
			}
			set
			{
				base.SetElement<NewDifferentialFormat>(3, value);
			}
		}

		// Token: 0x17007BF7 RID: 31735
		// (get) Token: 0x060173FD RID: 95229 RVA: 0x00334564 File Offset: 0x00332764
		// (set) Token: 0x060173FE RID: 95230 RVA: 0x0033456D File Offset: 0x0033276D
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(4);
			}
			set
			{
				base.SetElement<ExtensionList>(4, value);
			}
		}

		// Token: 0x060173FF RID: 95231 RVA: 0x00334578 File Offset: 0x00332778
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "rId" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "ua" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "ra" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "sId" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "odxf" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "xfDxf" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "s" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "dxf" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "numFmtId" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "quotePrefix" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "oldQuotePrefix" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "ph" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "oldPh" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "endOfListFormulaUpdate" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017400 RID: 95232 RVA: 0x003346C1 File Offset: 0x003328C1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RevisionCellChange>(deep);
		}

		// Token: 0x06017401 RID: 95233 RVA: 0x003346CC File Offset: 0x003328CC
		// Note: this type is marked as 'beforefieldinit'.
		static RevisionCellChange()
		{
			byte[] array = new byte[14];
			RevisionCellChange.attributeNamespaceIds = array;
			RevisionCellChange.eleTagNames = new string[] { "oc", "nc", "odxf", "ndxf", "extLst" };
			RevisionCellChange.eleNamespaceIds = new byte[] { 22, 22, 22, 22, 22 };
		}

		// Token: 0x04009BB1 RID: 39857
		private const string tagName = "rcc";

		// Token: 0x04009BB2 RID: 39858
		private const byte tagNsId = 22;

		// Token: 0x04009BB3 RID: 39859
		internal const int ElementTypeIdConst = 11161;

		// Token: 0x04009BB4 RID: 39860
		private static string[] attributeTagNames = new string[]
		{
			"rId", "ua", "ra", "sId", "odxf", "xfDxf", "s", "dxf", "numFmtId", "quotePrefix",
			"oldQuotePrefix", "ph", "oldPh", "endOfListFormulaUpdate"
		};

		// Token: 0x04009BB5 RID: 39861
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009BB6 RID: 39862
		private static readonly string[] eleTagNames;

		// Token: 0x04009BB7 RID: 39863
		private static readonly byte[] eleNamespaceIds;
	}
}
