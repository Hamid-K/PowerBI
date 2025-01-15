using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C95 RID: 11413
	[GeneratedCode("DomGen", "2.0")]
	internal class SheetProtection : OpenXmlLeafElement
	{
		// Token: 0x170083E4 RID: 33764
		// (get) Token: 0x06018594 RID: 99732 RVA: 0x0033FEB8 File Offset: 0x0033E0B8
		public override string LocalName
		{
			get
			{
				return "sheetProtection";
			}
		}

		// Token: 0x170083E5 RID: 33765
		// (get) Token: 0x06018595 RID: 99733 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170083E6 RID: 33766
		// (get) Token: 0x06018596 RID: 99734 RVA: 0x00340C13 File Offset: 0x0033EE13
		internal override int ElementTypeId
		{
			get
			{
				return 11393;
			}
		}

		// Token: 0x06018597 RID: 99735 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170083E7 RID: 33767
		// (get) Token: 0x06018598 RID: 99736 RVA: 0x00340C1A File Offset: 0x0033EE1A
		internal override string[] AttributeTagNames
		{
			get
			{
				return SheetProtection.attributeTagNames;
			}
		}

		// Token: 0x170083E8 RID: 33768
		// (get) Token: 0x06018599 RID: 99737 RVA: 0x00340C21 File Offset: 0x0033EE21
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SheetProtection.attributeNamespaceIds;
			}
		}

		// Token: 0x170083E9 RID: 33769
		// (get) Token: 0x0601859A RID: 99738 RVA: 0x002EA130 File Offset: 0x002E8330
		// (set) Token: 0x0601859B RID: 99739 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "password")]
		public HexBinaryValue Password
		{
			get
			{
				return (HexBinaryValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x170083EA RID: 33770
		// (get) Token: 0x0601859C RID: 99740 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0601859D RID: 99741 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "algorithmName")]
		public StringValue AlgorithmName
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

		// Token: 0x170083EB RID: 33771
		// (get) Token: 0x0601859E RID: 99742 RVA: 0x002EA13F File Offset: 0x002E833F
		// (set) Token: 0x0601859F RID: 99743 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "hashValue")]
		public Base64BinaryValue HashValue
		{
			get
			{
				return (Base64BinaryValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x170083EC RID: 33772
		// (get) Token: 0x060185A0 RID: 99744 RVA: 0x002EA14E File Offset: 0x002E834E
		// (set) Token: 0x060185A1 RID: 99745 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "saltValue")]
		public Base64BinaryValue SaltValue
		{
			get
			{
				return (Base64BinaryValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x170083ED RID: 33773
		// (get) Token: 0x060185A2 RID: 99746 RVA: 0x002E6C42 File Offset: 0x002E4E42
		// (set) Token: 0x060185A3 RID: 99747 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "spinCount")]
		public UInt32Value SpinCount
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

		// Token: 0x170083EE RID: 33774
		// (get) Token: 0x060185A4 RID: 99748 RVA: 0x002D7D70 File Offset: 0x002D5F70
		// (set) Token: 0x060185A5 RID: 99749 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "sheet")]
		public BooleanValue Sheet
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

		// Token: 0x170083EF RID: 33775
		// (get) Token: 0x060185A6 RID: 99750 RVA: 0x002CCC33 File Offset: 0x002CAE33
		// (set) Token: 0x060185A7 RID: 99751 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "objects")]
		public BooleanValue Objects
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

		// Token: 0x170083F0 RID: 33776
		// (get) Token: 0x060185A8 RID: 99752 RVA: 0x002CA736 File Offset: 0x002C8936
		// (set) Token: 0x060185A9 RID: 99753 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "scenarios")]
		public BooleanValue Scenarios
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

		// Token: 0x170083F1 RID: 33777
		// (get) Token: 0x060185AA RID: 99754 RVA: 0x002CB706 File Offset: 0x002C9906
		// (set) Token: 0x060185AB RID: 99755 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "formatCells")]
		public BooleanValue FormatCells
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

		// Token: 0x170083F2 RID: 33778
		// (get) Token: 0x060185AC RID: 99756 RVA: 0x002C92EA File Offset: 0x002C74EA
		// (set) Token: 0x060185AD RID: 99757 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "formatColumns")]
		public BooleanValue FormatColumns
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

		// Token: 0x170083F3 RID: 33779
		// (get) Token: 0x060185AE RID: 99758 RVA: 0x002C8762 File Offset: 0x002C6962
		// (set) Token: 0x060185AF RID: 99759 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "formatRows")]
		public BooleanValue FormatRows
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

		// Token: 0x170083F4 RID: 33780
		// (get) Token: 0x060185B0 RID: 99760 RVA: 0x002C92FA File Offset: 0x002C74FA
		// (set) Token: 0x060185B1 RID: 99761 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "insertColumns")]
		public BooleanValue InsertColumns
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

		// Token: 0x170083F5 RID: 33781
		// (get) Token: 0x060185B2 RID: 99762 RVA: 0x002CE196 File Offset: 0x002CC396
		// (set) Token: 0x060185B3 RID: 99763 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "insertRows")]
		public BooleanValue InsertRows
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

		// Token: 0x170083F6 RID: 33782
		// (get) Token: 0x060185B4 RID: 99764 RVA: 0x002CD15F File Offset: 0x002CB35F
		// (set) Token: 0x060185B5 RID: 99765 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "insertHyperlinks")]
		public BooleanValue InsertHyperlinks
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

		// Token: 0x170083F7 RID: 33783
		// (get) Token: 0x060185B6 RID: 99766 RVA: 0x002C9F8A File Offset: 0x002C818A
		// (set) Token: 0x060185B7 RID: 99767 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "deleteColumns")]
		public BooleanValue DeleteColumns
		{
			get
			{
				return (BooleanValue)base.Attributes[14];
			}
			set
			{
				base.Attributes[14] = value;
			}
		}

		// Token: 0x170083F8 RID: 33784
		// (get) Token: 0x060185B8 RID: 99768 RVA: 0x002CA745 File Offset: 0x002C8945
		// (set) Token: 0x060185B9 RID: 99769 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "deleteRows")]
		public BooleanValue DeleteRows
		{
			get
			{
				return (BooleanValue)base.Attributes[15];
			}
			set
			{
				base.Attributes[15] = value;
			}
		}

		// Token: 0x170083F9 RID: 33785
		// (get) Token: 0x060185BA RID: 99770 RVA: 0x002C930A File Offset: 0x002C750A
		// (set) Token: 0x060185BB RID: 99771 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(0, "selectLockedCells")]
		public BooleanValue SelectLockedCells
		{
			get
			{
				return (BooleanValue)base.Attributes[16];
			}
			set
			{
				base.Attributes[16] = value;
			}
		}

		// Token: 0x170083FA RID: 33786
		// (get) Token: 0x060185BC RID: 99772 RVA: 0x002CDD31 File Offset: 0x002CBF31
		// (set) Token: 0x060185BD RID: 99773 RVA: 0x002BE279 File Offset: 0x002BC479
		[SchemaAttr(0, "sort")]
		public BooleanValue Sort
		{
			get
			{
				return (BooleanValue)base.Attributes[17];
			}
			set
			{
				base.Attributes[17] = value;
			}
		}

		// Token: 0x170083FB RID: 33787
		// (get) Token: 0x060185BE RID: 99774 RVA: 0x002C8772 File Offset: 0x002C6972
		// (set) Token: 0x060185BF RID: 99775 RVA: 0x002BE295 File Offset: 0x002BC495
		[SchemaAttr(0, "autoFilter")]
		public BooleanValue AutoFilter
		{
			get
			{
				return (BooleanValue)base.Attributes[18];
			}
			set
			{
				base.Attributes[18] = value;
			}
		}

		// Token: 0x170083FC RID: 33788
		// (get) Token: 0x060185C0 RID: 99776 RVA: 0x002D6080 File Offset: 0x002D4280
		// (set) Token: 0x060185C1 RID: 99777 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
		[SchemaAttr(0, "pivotTables")]
		public BooleanValue PivotTables
		{
			get
			{
				return (BooleanValue)base.Attributes[19];
			}
			set
			{
				base.Attributes[19] = value;
			}
		}

		// Token: 0x170083FD RID: 33789
		// (get) Token: 0x060185C2 RID: 99778 RVA: 0x002C8F75 File Offset: 0x002C7175
		// (set) Token: 0x060185C3 RID: 99779 RVA: 0x002BE2CD File Offset: 0x002BC4CD
		[SchemaAttr(0, "selectUnlockedCells")]
		public BooleanValue SelectUnlockedCells
		{
			get
			{
				return (BooleanValue)base.Attributes[20];
			}
			set
			{
				base.Attributes[20] = value;
			}
		}

		// Token: 0x060185C5 RID: 99781 RVA: 0x00340C28 File Offset: 0x0033EE28
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "password" == name)
			{
				return new HexBinaryValue();
			}
			if (namespaceId == 0 && "algorithmName" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "hashValue" == name)
			{
				return new Base64BinaryValue();
			}
			if (namespaceId == 0 && "saltValue" == name)
			{
				return new Base64BinaryValue();
			}
			if (namespaceId == 0 && "spinCount" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "sheet" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "objects" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "scenarios" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "formatCells" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "formatColumns" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "formatRows" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "insertColumns" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "insertRows" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "insertHyperlinks" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "deleteColumns" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "deleteRows" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "selectLockedCells" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "sort" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "autoFilter" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "pivotTables" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "selectUnlockedCells" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060185C6 RID: 99782 RVA: 0x00340E0B File Offset: 0x0033F00B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SheetProtection>(deep);
		}

		// Token: 0x060185C7 RID: 99783 RVA: 0x00340E14 File Offset: 0x0033F014
		// Note: this type is marked as 'beforefieldinit'.
		static SheetProtection()
		{
			byte[] array = new byte[21];
			SheetProtection.attributeNamespaceIds = array;
		}

		// Token: 0x04009FE6 RID: 40934
		private const string tagName = "sheetProtection";

		// Token: 0x04009FE7 RID: 40935
		private const byte tagNsId = 22;

		// Token: 0x04009FE8 RID: 40936
		internal const int ElementTypeIdConst = 11393;

		// Token: 0x04009FE9 RID: 40937
		private static string[] attributeTagNames = new string[]
		{
			"password", "algorithmName", "hashValue", "saltValue", "spinCount", "sheet", "objects", "scenarios", "formatCells", "formatColumns",
			"formatRows", "insertColumns", "insertRows", "insertHyperlinks", "deleteColumns", "deleteRows", "selectLockedCells", "sort", "autoFilter", "pivotTables",
			"selectUnlockedCells"
		};

		// Token: 0x04009FEA RID: 40938
		private static byte[] attributeNamespaceIds;
	}
}
