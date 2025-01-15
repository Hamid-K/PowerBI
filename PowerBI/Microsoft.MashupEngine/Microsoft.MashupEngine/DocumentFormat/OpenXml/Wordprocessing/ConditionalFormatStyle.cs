using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002ED9 RID: 11993
	[GeneratedCode("DomGen", "2.0")]
	internal class ConditionalFormatStyle : OpenXmlLeafElement
	{
		// Token: 0x17008D1C RID: 36124
		// (get) Token: 0x06019984 RID: 104836 RVA: 0x003530E8 File Offset: 0x003512E8
		public override string LocalName
		{
			get
			{
				return "cnfStyle";
			}
		}

		// Token: 0x17008D1D RID: 36125
		// (get) Token: 0x06019985 RID: 104837 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008D1E RID: 36126
		// (get) Token: 0x06019986 RID: 104838 RVA: 0x003530EF File Offset: 0x003512EF
		internal override int ElementTypeId
		{
			get
			{
				return 11649;
			}
		}

		// Token: 0x06019987 RID: 104839 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008D1F RID: 36127
		// (get) Token: 0x06019988 RID: 104840 RVA: 0x003530F6 File Offset: 0x003512F6
		internal override string[] AttributeTagNames
		{
			get
			{
				return ConditionalFormatStyle.attributeTagNames;
			}
		}

		// Token: 0x17008D20 RID: 36128
		// (get) Token: 0x06019989 RID: 104841 RVA: 0x003530FD File Offset: 0x003512FD
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ConditionalFormatStyle.attributeNamespaceIds;
			}
		}

		// Token: 0x17008D21 RID: 36129
		// (get) Token: 0x0601998A RID: 104842 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601998B RID: 104843 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public StringValue Val
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

		// Token: 0x17008D22 RID: 36130
		// (get) Token: 0x0601998C RID: 104844 RVA: 0x003480EF File Offset: 0x003462EF
		// (set) Token: 0x0601998D RID: 104845 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "firstRow")]
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public OnOffValue FirstRow
		{
			get
			{
				return (OnOffValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17008D23 RID: 36131
		// (get) Token: 0x0601998E RID: 104846 RVA: 0x003461ED File Offset: 0x003443ED
		// (set) Token: 0x0601998F RID: 104847 RVA: 0x002BD494 File Offset: 0x002BB694
		[OfficeAvailability(FileFormatVersions.Office2010)]
		[SchemaAttr(23, "lastRow")]
		public OnOffValue LastRow
		{
			get
			{
				return (OnOffValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17008D24 RID: 36132
		// (get) Token: 0x06019990 RID: 104848 RVA: 0x003474AC File Offset: 0x003456AC
		// (set) Token: 0x06019991 RID: 104849 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(23, "firstColumn")]
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public OnOffValue FirstColumn
		{
			get
			{
				return (OnOffValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17008D25 RID: 36133
		// (get) Token: 0x06019992 RID: 104850 RVA: 0x002EB443 File Offset: 0x002E9643
		// (set) Token: 0x06019993 RID: 104851 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(23, "lastColumn")]
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public OnOffValue LastColumn
		{
			get
			{
				return (OnOffValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17008D26 RID: 36134
		// (get) Token: 0x06019994 RID: 104852 RVA: 0x003461FC File Offset: 0x003443FC
		// (set) Token: 0x06019995 RID: 104853 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[OfficeAvailability(FileFormatVersions.Office2010)]
		[SchemaAttr(23, "oddVBand")]
		public OnOffValue OddVerticalBand
		{
			get
			{
				return (OnOffValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x17008D27 RID: 36135
		// (get) Token: 0x06019996 RID: 104854 RVA: 0x00353104 File Offset: 0x00351304
		// (set) Token: 0x06019997 RID: 104855 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(23, "evenVBand")]
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public OnOffValue EvenVerticalBand
		{
			get
			{
				return (OnOffValue)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x17008D28 RID: 36136
		// (get) Token: 0x06019998 RID: 104856 RVA: 0x00348E89 File Offset: 0x00347089
		// (set) Token: 0x06019999 RID: 104857 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(23, "oddHBand")]
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public OnOffValue OddHorizontalBand
		{
			get
			{
				return (OnOffValue)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x17008D29 RID: 36137
		// (get) Token: 0x0601999A RID: 104858 RVA: 0x00348E98 File Offset: 0x00347098
		// (set) Token: 0x0601999B RID: 104859 RVA: 0x002BD530 File Offset: 0x002BB730
		[OfficeAvailability(FileFormatVersions.Office2010)]
		[SchemaAttr(23, "evenHBand")]
		public OnOffValue EvenHorizontalBand
		{
			get
			{
				return (OnOffValue)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x17008D2A RID: 36138
		// (get) Token: 0x0601999C RID: 104860 RVA: 0x00353113 File Offset: 0x00351313
		// (set) Token: 0x0601999D RID: 104861 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(23, "firstRowFirstColumn")]
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public OnOffValue FirstRowFirstColumn
		{
			get
			{
				return (OnOffValue)base.Attributes[9];
			}
			set
			{
				base.Attributes[9] = value;
			}
		}

		// Token: 0x17008D2B RID: 36139
		// (get) Token: 0x0601999E RID: 104862 RVA: 0x00353123 File Offset: 0x00351323
		// (set) Token: 0x0601999F RID: 104863 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(23, "firstRowLastColumn")]
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public OnOffValue FirstRowLastColumn
		{
			get
			{
				return (OnOffValue)base.Attributes[10];
			}
			set
			{
				base.Attributes[10] = value;
			}
		}

		// Token: 0x17008D2C RID: 36140
		// (get) Token: 0x060199A0 RID: 104864 RVA: 0x00353133 File Offset: 0x00351333
		// (set) Token: 0x060199A1 RID: 104865 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(23, "lastRowFirstColumn")]
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public OnOffValue LastRowFirstColumn
		{
			get
			{
				return (OnOffValue)base.Attributes[11];
			}
			set
			{
				base.Attributes[11] = value;
			}
		}

		// Token: 0x17008D2D RID: 36141
		// (get) Token: 0x060199A2 RID: 104866 RVA: 0x00353143 File Offset: 0x00351343
		// (set) Token: 0x060199A3 RID: 104867 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(23, "lastRowLastColumn")]
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public OnOffValue LastRowLastColumn
		{
			get
			{
				return (OnOffValue)base.Attributes[12];
			}
			set
			{
				base.Attributes[12] = value;
			}
		}

		// Token: 0x060199A5 RID: 104869 RVA: 0x00353154 File Offset: 0x00351354
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "firstRow" == name)
			{
				return new OnOffValue();
			}
			if (23 == namespaceId && "lastRow" == name)
			{
				return new OnOffValue();
			}
			if (23 == namespaceId && "firstColumn" == name)
			{
				return new OnOffValue();
			}
			if (23 == namespaceId && "lastColumn" == name)
			{
				return new OnOffValue();
			}
			if (23 == namespaceId && "oddVBand" == name)
			{
				return new OnOffValue();
			}
			if (23 == namespaceId && "evenVBand" == name)
			{
				return new OnOffValue();
			}
			if (23 == namespaceId && "oddHBand" == name)
			{
				return new OnOffValue();
			}
			if (23 == namespaceId && "evenHBand" == name)
			{
				return new OnOffValue();
			}
			if (23 == namespaceId && "firstRowFirstColumn" == name)
			{
				return new OnOffValue();
			}
			if (23 == namespaceId && "firstRowLastColumn" == name)
			{
				return new OnOffValue();
			}
			if (23 == namespaceId && "lastRowFirstColumn" == name)
			{
				return new OnOffValue();
			}
			if (23 == namespaceId && "lastRowLastColumn" == name)
			{
				return new OnOffValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060199A6 RID: 104870 RVA: 0x003532A1 File Offset: 0x003514A1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ConditionalFormatStyle>(deep);
		}

		// Token: 0x0400A992 RID: 43410
		private const string tagName = "cnfStyle";

		// Token: 0x0400A993 RID: 43411
		private const byte tagNsId = 23;

		// Token: 0x0400A994 RID: 43412
		internal const int ElementTypeIdConst = 11649;

		// Token: 0x0400A995 RID: 43413
		private static string[] attributeTagNames = new string[]
		{
			"val", "firstRow", "lastRow", "firstColumn", "lastColumn", "oddVBand", "evenVBand", "oddHBand", "evenHBand", "firstRowFirstColumn",
			"firstRowLastColumn", "lastRowFirstColumn", "lastRowLastColumn"
		};

		// Token: 0x0400A996 RID: 43414
		private static byte[] attributeNamespaceIds = new byte[]
		{
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23
		};
	}
}
