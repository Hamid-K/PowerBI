using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B58 RID: 11096
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Tuples))]
	[ChildElementInfo(typeof(MemberPropertyIndex))]
	internal class StringItem : OpenXmlCompositeElement
	{
		// Token: 0x170078A3 RID: 30883
		// (get) Token: 0x06016CCD RID: 93389 RVA: 0x0032E52E File Offset: 0x0032C72E
		public override string LocalName
		{
			get
			{
				return "s";
			}
		}

		// Token: 0x170078A4 RID: 30884
		// (get) Token: 0x06016CCE RID: 93390 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170078A5 RID: 30885
		// (get) Token: 0x06016CCF RID: 93391 RVA: 0x0032F3F0 File Offset: 0x0032D5F0
		internal override int ElementTypeId
		{
			get
			{
				return 11079;
			}
		}

		// Token: 0x06016CD0 RID: 93392 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170078A6 RID: 30886
		// (get) Token: 0x06016CD1 RID: 93393 RVA: 0x0032F3F7 File Offset: 0x0032D5F7
		internal override string[] AttributeTagNames
		{
			get
			{
				return StringItem.attributeTagNames;
			}
		}

		// Token: 0x170078A7 RID: 30887
		// (get) Token: 0x06016CD2 RID: 93394 RVA: 0x0032F3FE File Offset: 0x0032D5FE
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return StringItem.attributeNamespaceIds;
			}
		}

		// Token: 0x170078A8 RID: 30888
		// (get) Token: 0x06016CD3 RID: 93395 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06016CD4 RID: 93396 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "v")]
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

		// Token: 0x170078A9 RID: 30889
		// (get) Token: 0x06016CD5 RID: 93397 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x06016CD6 RID: 93398 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "u")]
		public BooleanValue Unused
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

		// Token: 0x170078AA RID: 30890
		// (get) Token: 0x06016CD7 RID: 93399 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x06016CD8 RID: 93400 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "f")]
		public BooleanValue Calculated
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

		// Token: 0x170078AB RID: 30891
		// (get) Token: 0x06016CD9 RID: 93401 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x06016CDA RID: 93402 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "c")]
		public StringValue Caption
		{
			get
			{
				return (StringValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x170078AC RID: 30892
		// (get) Token: 0x06016CDB RID: 93403 RVA: 0x002E6C42 File Offset: 0x002E4E42
		// (set) Token: 0x06016CDC RID: 93404 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "cp")]
		public UInt32Value PropertyCount
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

		// Token: 0x170078AD RID: 30893
		// (get) Token: 0x06016CDD RID: 93405 RVA: 0x002E6EEB File Offset: 0x002E50EB
		// (set) Token: 0x06016CDE RID: 93406 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "in")]
		public UInt32Value FormatIndex
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

		// Token: 0x170078AE RID: 30894
		// (get) Token: 0x06016CDF RID: 93407 RVA: 0x0032ED05 File Offset: 0x0032CF05
		// (set) Token: 0x06016CE0 RID: 93408 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "bc")]
		public HexBinaryValue BackgroundColor
		{
			get
			{
				return (HexBinaryValue)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x170078AF RID: 30895
		// (get) Token: 0x06016CE1 RID: 93409 RVA: 0x0032EEF7 File Offset: 0x0032D0F7
		// (set) Token: 0x06016CE2 RID: 93410 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "fc")]
		public HexBinaryValue ForegroundColor
		{
			get
			{
				return (HexBinaryValue)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x170078B0 RID: 30896
		// (get) Token: 0x06016CE3 RID: 93411 RVA: 0x002CB706 File Offset: 0x002C9906
		// (set) Token: 0x06016CE4 RID: 93412 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "i")]
		public BooleanValue Italic
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

		// Token: 0x170078B1 RID: 30897
		// (get) Token: 0x06016CE5 RID: 93413 RVA: 0x002C92EA File Offset: 0x002C74EA
		// (set) Token: 0x06016CE6 RID: 93414 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "un")]
		public BooleanValue Underline
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

		// Token: 0x170078B2 RID: 30898
		// (get) Token: 0x06016CE7 RID: 93415 RVA: 0x002C8762 File Offset: 0x002C6962
		// (set) Token: 0x06016CE8 RID: 93416 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "st")]
		public BooleanValue Strikethrough
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

		// Token: 0x170078B3 RID: 30899
		// (get) Token: 0x06016CE9 RID: 93417 RVA: 0x002C92FA File Offset: 0x002C74FA
		// (set) Token: 0x06016CEA RID: 93418 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "b")]
		public BooleanValue Bold
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

		// Token: 0x06016CEB RID: 93419 RVA: 0x00293ECF File Offset: 0x002920CF
		public StringItem()
		{
		}

		// Token: 0x06016CEC RID: 93420 RVA: 0x00293ED7 File Offset: 0x002920D7
		public StringItem(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016CED RID: 93421 RVA: 0x00293EE0 File Offset: 0x002920E0
		public StringItem(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016CEE RID: 93422 RVA: 0x00293EE9 File Offset: 0x002920E9
		public StringItem(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016CEF RID: 93423 RVA: 0x0032ED14 File Offset: 0x0032CF14
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "tpls" == name)
			{
				return new Tuples();
			}
			if (22 == namespaceId && "x" == name)
			{
				return new MemberPropertyIndex();
			}
			return null;
		}

		// Token: 0x06016CF0 RID: 93424 RVA: 0x0032F408 File Offset: 0x0032D608
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "v" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "u" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "f" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "c" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "cp" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "in" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "bc" == name)
			{
				return new HexBinaryValue();
			}
			if (namespaceId == 0 && "fc" == name)
			{
				return new HexBinaryValue();
			}
			if (namespaceId == 0 && "i" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "un" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "st" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "b" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06016CF1 RID: 93425 RVA: 0x0032F525 File Offset: 0x0032D725
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<StringItem>(deep);
		}

		// Token: 0x06016CF2 RID: 93426 RVA: 0x0032F530 File Offset: 0x0032D730
		// Note: this type is marked as 'beforefieldinit'.
		static StringItem()
		{
			byte[] array = new byte[12];
			StringItem.attributeNamespaceIds = array;
		}

		// Token: 0x040099F8 RID: 39416
		private const string tagName = "s";

		// Token: 0x040099F9 RID: 39417
		private const byte tagNsId = 22;

		// Token: 0x040099FA RID: 39418
		internal const int ElementTypeIdConst = 11079;

		// Token: 0x040099FB RID: 39419
		private static string[] attributeTagNames = new string[]
		{
			"v", "u", "f", "c", "cp", "in", "bc", "fc", "i", "un",
			"st", "b"
		};

		// Token: 0x040099FC RID: 39420
		private static byte[] attributeNamespaceIds;
	}
}
