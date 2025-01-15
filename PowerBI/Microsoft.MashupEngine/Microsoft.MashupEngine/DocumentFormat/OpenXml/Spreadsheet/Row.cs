using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BCC RID: 11212
	[ChildElementInfo(typeof(Cell))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ExtensionList))]
	internal class Row : OpenXmlCompositeElement
	{
		// Token: 0x17007CDB RID: 31963
		// (get) Token: 0x060175EA RID: 95722 RVA: 0x002E3583 File Offset: 0x002E1783
		public override string LocalName
		{
			get
			{
				return "row";
			}
		}

		// Token: 0x17007CDC RID: 31964
		// (get) Token: 0x060175EB RID: 95723 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007CDD RID: 31965
		// (get) Token: 0x060175EC RID: 95724 RVA: 0x00335EB6 File Offset: 0x003340B6
		internal override int ElementTypeId
		{
			get
			{
				return 11184;
			}
		}

		// Token: 0x060175ED RID: 95725 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007CDE RID: 31966
		// (get) Token: 0x060175EE RID: 95726 RVA: 0x00335EBD File Offset: 0x003340BD
		internal override string[] AttributeTagNames
		{
			get
			{
				return Row.attributeTagNames;
			}
		}

		// Token: 0x17007CDF RID: 31967
		// (get) Token: 0x060175EF RID: 95727 RVA: 0x00335EC4 File Offset: 0x003340C4
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Row.attributeNamespaceIds;
			}
		}

		// Token: 0x17007CE0 RID: 31968
		// (get) Token: 0x060175F0 RID: 95728 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x060175F1 RID: 95729 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "r")]
		public UInt32Value RowIndex
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

		// Token: 0x17007CE1 RID: 31969
		// (get) Token: 0x060175F2 RID: 95730 RVA: 0x00335ECB File Offset: 0x003340CB
		// (set) Token: 0x060175F3 RID: 95731 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "spans")]
		public ListValue<StringValue> Spans
		{
			get
			{
				return (ListValue<StringValue>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17007CE2 RID: 31970
		// (get) Token: 0x060175F4 RID: 95732 RVA: 0x002E5814 File Offset: 0x002E3A14
		// (set) Token: 0x060175F5 RID: 95733 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "s")]
		public UInt32Value StyleIndex
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

		// Token: 0x17007CE3 RID: 31971
		// (get) Token: 0x060175F6 RID: 95734 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x060175F7 RID: 95735 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "customFormat")]
		public BooleanValue CustomFormat
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

		// Token: 0x17007CE4 RID: 31972
		// (get) Token: 0x060175F8 RID: 95736 RVA: 0x002E82DC File Offset: 0x002E64DC
		// (set) Token: 0x060175F9 RID: 95737 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "ht")]
		public DoubleValue Height
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

		// Token: 0x17007CE5 RID: 31973
		// (get) Token: 0x060175FA RID: 95738 RVA: 0x002D7D70 File Offset: 0x002D5F70
		// (set) Token: 0x060175FB RID: 95739 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "hidden")]
		public BooleanValue Hidden
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

		// Token: 0x17007CE6 RID: 31974
		// (get) Token: 0x060175FC RID: 95740 RVA: 0x002CCC33 File Offset: 0x002CAE33
		// (set) Token: 0x060175FD RID: 95741 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "customHeight")]
		public BooleanValue CustomHeight
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

		// Token: 0x17007CE7 RID: 31975
		// (get) Token: 0x060175FE RID: 95742 RVA: 0x00335EDA File Offset: 0x003340DA
		// (set) Token: 0x060175FF RID: 95743 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "outlineLevel")]
		public ByteValue OutlineLevel
		{
			get
			{
				return (ByteValue)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x17007CE8 RID: 31976
		// (get) Token: 0x06017600 RID: 95744 RVA: 0x002CB706 File Offset: 0x002C9906
		// (set) Token: 0x06017601 RID: 95745 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "collapsed")]
		public BooleanValue Collapsed
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

		// Token: 0x17007CE9 RID: 31977
		// (get) Token: 0x06017602 RID: 95746 RVA: 0x002C92EA File Offset: 0x002C74EA
		// (set) Token: 0x06017603 RID: 95747 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "thickTop")]
		public BooleanValue ThickTop
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

		// Token: 0x17007CEA RID: 31978
		// (get) Token: 0x06017604 RID: 95748 RVA: 0x002C8762 File Offset: 0x002C6962
		// (set) Token: 0x06017605 RID: 95749 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "thickBot")]
		public BooleanValue ThickBot
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

		// Token: 0x17007CEB RID: 31979
		// (get) Token: 0x06017606 RID: 95750 RVA: 0x002C92FA File Offset: 0x002C74FA
		// (set) Token: 0x06017607 RID: 95751 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "ph")]
		public BooleanValue ShowPhonetic
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

		// Token: 0x17007CEC RID: 31980
		// (get) Token: 0x06017608 RID: 95752 RVA: 0x00335EE9 File Offset: 0x003340E9
		// (set) Token: 0x06017609 RID: 95753 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[OfficeAvailability(FileFormatVersions.Office2010)]
		[SchemaAttr(55, "dyDescent")]
		public DoubleValue DyDescent
		{
			get
			{
				return (DoubleValue)base.Attributes[12];
			}
			set
			{
				base.Attributes[12] = value;
			}
		}

		// Token: 0x0601760A RID: 95754 RVA: 0x00293ECF File Offset: 0x002920CF
		public Row()
		{
		}

		// Token: 0x0601760B RID: 95755 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Row(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601760C RID: 95756 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Row(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601760D RID: 95757 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Row(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601760E RID: 95758 RVA: 0x00335EF9 File Offset: 0x003340F9
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "c" == name)
			{
				return new Cell();
			}
			if (22 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x0601760F RID: 95759 RVA: 0x00335F2C File Offset: 0x0033412C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "r" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "spans" == name)
			{
				return new ListValue<StringValue>();
			}
			if (namespaceId == 0 && "s" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "customFormat" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "ht" == name)
			{
				return new DoubleValue();
			}
			if (namespaceId == 0 && "hidden" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "customHeight" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "outlineLevel" == name)
			{
				return new ByteValue();
			}
			if (namespaceId == 0 && "collapsed" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "thickTop" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "thickBot" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "ph" == name)
			{
				return new BooleanValue();
			}
			if (55 == namespaceId && "dyDescent" == name)
			{
				return new DoubleValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017610 RID: 95760 RVA: 0x00336061 File Offset: 0x00334261
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Row>(deep);
		}

		// Token: 0x04009C1B RID: 39963
		private const string tagName = "row";

		// Token: 0x04009C1C RID: 39964
		private const byte tagNsId = 22;

		// Token: 0x04009C1D RID: 39965
		internal const int ElementTypeIdConst = 11184;

		// Token: 0x04009C1E RID: 39966
		private static string[] attributeTagNames = new string[]
		{
			"r", "spans", "s", "customFormat", "ht", "hidden", "customHeight", "outlineLevel", "collapsed", "thickTop",
			"thickBot", "ph", "dyDescent"
		};

		// Token: 0x04009C1F RID: 39967
		private static byte[] attributeNamespaceIds = new byte[]
		{
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 55
		};
	}
}
