using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BCA RID: 11210
	[GeneratedCode("DomGen", "2.0")]
	internal class CellFormula : OpenXmlLeafTextElement
	{
		// Token: 0x17007CBC RID: 31932
		// (get) Token: 0x060175A9 RID: 95657 RVA: 0x002C81ED File Offset: 0x002C63ED
		public override string LocalName
		{
			get
			{
				return "f";
			}
		}

		// Token: 0x17007CBD RID: 31933
		// (get) Token: 0x060175AA RID: 95658 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007CBE RID: 31934
		// (get) Token: 0x060175AB RID: 95659 RVA: 0x00335B6B File Offset: 0x00333D6B
		internal override int ElementTypeId
		{
			get
			{
				return 11178;
			}
		}

		// Token: 0x060175AC RID: 95660 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007CBF RID: 31935
		// (get) Token: 0x060175AD RID: 95661 RVA: 0x00335B72 File Offset: 0x00333D72
		internal override string[] AttributeTagNames
		{
			get
			{
				return CellFormula.attributeTagNames;
			}
		}

		// Token: 0x17007CC0 RID: 31936
		// (get) Token: 0x060175AE RID: 95662 RVA: 0x00335B79 File Offset: 0x00333D79
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return CellFormula.attributeNamespaceIds;
			}
		}

		// Token: 0x17007CC1 RID: 31937
		// (get) Token: 0x060175AF RID: 95663 RVA: 0x00335B80 File Offset: 0x00333D80
		// (set) Token: 0x060175B0 RID: 95664 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "t")]
		public EnumValue<CellFormulaValues> FormulaType
		{
			get
			{
				return (EnumValue<CellFormulaValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17007CC2 RID: 31938
		// (get) Token: 0x060175B1 RID: 95665 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x060175B2 RID: 95666 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "aca")]
		public BooleanValue AlwaysCalculateArray
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

		// Token: 0x17007CC3 RID: 31939
		// (get) Token: 0x060175B3 RID: 95667 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x060175B4 RID: 95668 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "ref")]
		public StringValue Reference
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

		// Token: 0x17007CC4 RID: 31940
		// (get) Token: 0x060175B5 RID: 95669 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x060175B6 RID: 95670 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "dt2D")]
		public BooleanValue DataTable2D
		{
			get
			{
				return (BooleanValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17007CC5 RID: 31941
		// (get) Token: 0x060175B7 RID: 95671 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x060175B8 RID: 95672 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "dtr")]
		public BooleanValue DataTableRow
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

		// Token: 0x17007CC6 RID: 31942
		// (get) Token: 0x060175B9 RID: 95673 RVA: 0x002D7D70 File Offset: 0x002D5F70
		// (set) Token: 0x060175BA RID: 95674 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "del1")]
		public BooleanValue Input1Deleted
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

		// Token: 0x17007CC7 RID: 31943
		// (get) Token: 0x060175BB RID: 95675 RVA: 0x002CCC33 File Offset: 0x002CAE33
		// (set) Token: 0x060175BC RID: 95676 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "del2")]
		public BooleanValue Input2Deleted
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

		// Token: 0x17007CC8 RID: 31944
		// (get) Token: 0x060175BD RID: 95677 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x060175BE RID: 95678 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "r1")]
		public StringValue R1
		{
			get
			{
				return (StringValue)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x17007CC9 RID: 31945
		// (get) Token: 0x060175BF RID: 95679 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x060175C0 RID: 95680 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "r2")]
		public StringValue R2
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

		// Token: 0x17007CCA RID: 31946
		// (get) Token: 0x060175C1 RID: 95681 RVA: 0x002C92EA File Offset: 0x002C74EA
		// (set) Token: 0x060175C2 RID: 95682 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "ca")]
		public BooleanValue CalculateCell
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

		// Token: 0x17007CCB RID: 31947
		// (get) Token: 0x060175C3 RID: 95683 RVA: 0x0031EC49 File Offset: 0x0031CE49
		// (set) Token: 0x060175C4 RID: 95684 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "si")]
		public UInt32Value SharedIndex
		{
			get
			{
				return (UInt32Value)base.Attributes[10];
			}
			set
			{
				base.Attributes[10] = value;
			}
		}

		// Token: 0x17007CCC RID: 31948
		// (get) Token: 0x060175C5 RID: 95685 RVA: 0x002C92FA File Offset: 0x002C74FA
		// (set) Token: 0x060175C6 RID: 95686 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "bx")]
		public BooleanValue Bx
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

		// Token: 0x17007CCD RID: 31949
		// (get) Token: 0x060175C7 RID: 95687 RVA: 0x00335B8F File Offset: 0x00333D8F
		// (set) Token: 0x060175C8 RID: 95688 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(1, "space")]
		public EnumValue<SpaceProcessingModeValues> Space
		{
			get
			{
				return (EnumValue<SpaceProcessingModeValues>)base.Attributes[12];
			}
			set
			{
				base.Attributes[12] = value;
			}
		}

		// Token: 0x060175C9 RID: 95689 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public CellFormula()
		{
		}

		// Token: 0x060175CA RID: 95690 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public CellFormula(string text)
			: base(text)
		{
		}

		// Token: 0x060175CB RID: 95691 RVA: 0x00335BA0 File Offset: 0x00333DA0
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x060175CC RID: 95692 RVA: 0x00335BBC File Offset: 0x00333DBC
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "t" == name)
			{
				return new EnumValue<CellFormulaValues>();
			}
			if (namespaceId == 0 && "aca" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "ref" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "dt2D" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "dtr" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "del1" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "del2" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "r1" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "r2" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "ca" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "si" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "bx" == name)
			{
				return new BooleanValue();
			}
			if (1 == namespaceId && "space" == name)
			{
				return new EnumValue<SpaceProcessingModeValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060175CD RID: 95693 RVA: 0x00335CF0 File Offset: 0x00333EF0
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CellFormula>(deep);
		}

		// Token: 0x04009C0F RID: 39951
		private const string tagName = "f";

		// Token: 0x04009C10 RID: 39952
		private const byte tagNsId = 22;

		// Token: 0x04009C11 RID: 39953
		internal const int ElementTypeIdConst = 11178;

		// Token: 0x04009C12 RID: 39954
		private static string[] attributeTagNames = new string[]
		{
			"t", "aca", "ref", "dt2D", "dtr", "del1", "del2", "r1", "r2", "ca",
			"si", "bx", "space"
		};

		// Token: 0x04009C13 RID: 39955
		private static byte[] attributeNamespaceIds = new byte[]
		{
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 1
		};
	}
}
