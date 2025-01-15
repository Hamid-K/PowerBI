using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B55 RID: 11093
	[ChildElementInfo(typeof(Tuples))]
	[ChildElementInfo(typeof(MemberPropertyIndex))]
	[GeneratedCode("DomGen", "2.0")]
	internal class NumberItem : OpenXmlCompositeElement
	{
		// Token: 0x17007873 RID: 30835
		// (get) Token: 0x06016C64 RID: 93284 RVA: 0x0032EEDB File Offset: 0x0032D0DB
		public override string LocalName
		{
			get
			{
				return "n";
			}
		}

		// Token: 0x17007874 RID: 30836
		// (get) Token: 0x06016C65 RID: 93285 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007875 RID: 30837
		// (get) Token: 0x06016C66 RID: 93286 RVA: 0x0032EEE2 File Offset: 0x0032D0E2
		internal override int ElementTypeId
		{
			get
			{
				return 11076;
			}
		}

		// Token: 0x06016C67 RID: 93287 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007876 RID: 30838
		// (get) Token: 0x06016C68 RID: 93288 RVA: 0x0032EEE9 File Offset: 0x0032D0E9
		internal override string[] AttributeTagNames
		{
			get
			{
				return NumberItem.attributeTagNames;
			}
		}

		// Token: 0x17007877 RID: 30839
		// (get) Token: 0x06016C69 RID: 93289 RVA: 0x0032EEF0 File Offset: 0x0032D0F0
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return NumberItem.attributeNamespaceIds;
			}
		}

		// Token: 0x17007878 RID: 30840
		// (get) Token: 0x06016C6A RID: 93290 RVA: 0x002E7DC5 File Offset: 0x002E5FC5
		// (set) Token: 0x06016C6B RID: 93291 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "v")]
		public DoubleValue Val
		{
			get
			{
				return (DoubleValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17007879 RID: 30841
		// (get) Token: 0x06016C6C RID: 93292 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x06016C6D RID: 93293 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x1700787A RID: 30842
		// (get) Token: 0x06016C6E RID: 93294 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x06016C6F RID: 93295 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x1700787B RID: 30843
		// (get) Token: 0x06016C70 RID: 93296 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x06016C71 RID: 93297 RVA: 0x002BD4AE File Offset: 0x002BB6AE
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

		// Token: 0x1700787C RID: 30844
		// (get) Token: 0x06016C72 RID: 93298 RVA: 0x002E6C42 File Offset: 0x002E4E42
		// (set) Token: 0x06016C73 RID: 93299 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
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

		// Token: 0x1700787D RID: 30845
		// (get) Token: 0x06016C74 RID: 93300 RVA: 0x002E6EEB File Offset: 0x002E50EB
		// (set) Token: 0x06016C75 RID: 93301 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
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

		// Token: 0x1700787E RID: 30846
		// (get) Token: 0x06016C76 RID: 93302 RVA: 0x0032ED05 File Offset: 0x0032CF05
		// (set) Token: 0x06016C77 RID: 93303 RVA: 0x002BD4FC File Offset: 0x002BB6FC
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

		// Token: 0x1700787F RID: 30847
		// (get) Token: 0x06016C78 RID: 93304 RVA: 0x0032EEF7 File Offset: 0x0032D0F7
		// (set) Token: 0x06016C79 RID: 93305 RVA: 0x002BD516 File Offset: 0x002BB716
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

		// Token: 0x17007880 RID: 30848
		// (get) Token: 0x06016C7A RID: 93306 RVA: 0x002CB706 File Offset: 0x002C9906
		// (set) Token: 0x06016C7B RID: 93307 RVA: 0x002BD530 File Offset: 0x002BB730
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

		// Token: 0x17007881 RID: 30849
		// (get) Token: 0x06016C7C RID: 93308 RVA: 0x002C92EA File Offset: 0x002C74EA
		// (set) Token: 0x06016C7D RID: 93309 RVA: 0x002BD54B File Offset: 0x002BB74B
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

		// Token: 0x17007882 RID: 30850
		// (get) Token: 0x06016C7E RID: 93310 RVA: 0x002C8762 File Offset: 0x002C6962
		// (set) Token: 0x06016C7F RID: 93311 RVA: 0x002BDB45 File Offset: 0x002BBD45
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

		// Token: 0x17007883 RID: 30851
		// (get) Token: 0x06016C80 RID: 93312 RVA: 0x002C92FA File Offset: 0x002C74FA
		// (set) Token: 0x06016C81 RID: 93313 RVA: 0x002BDB61 File Offset: 0x002BBD61
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

		// Token: 0x06016C82 RID: 93314 RVA: 0x00293ECF File Offset: 0x002920CF
		public NumberItem()
		{
		}

		// Token: 0x06016C83 RID: 93315 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NumberItem(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016C84 RID: 93316 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NumberItem(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016C85 RID: 93317 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NumberItem(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016C86 RID: 93318 RVA: 0x0032ED14 File Offset: 0x0032CF14
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

		// Token: 0x06016C87 RID: 93319 RVA: 0x0032EF08 File Offset: 0x0032D108
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "v" == name)
			{
				return new DoubleValue();
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

		// Token: 0x06016C88 RID: 93320 RVA: 0x0032F025 File Offset: 0x0032D225
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NumberItem>(deep);
		}

		// Token: 0x06016C89 RID: 93321 RVA: 0x0032F030 File Offset: 0x0032D230
		// Note: this type is marked as 'beforefieldinit'.
		static NumberItem()
		{
			byte[] array = new byte[12];
			NumberItem.attributeNamespaceIds = array;
		}

		// Token: 0x040099E7 RID: 39399
		private const string tagName = "n";

		// Token: 0x040099E8 RID: 39400
		private const byte tagNsId = 22;

		// Token: 0x040099E9 RID: 39401
		internal const int ElementTypeIdConst = 11076;

		// Token: 0x040099EA RID: 39402
		private static string[] attributeTagNames = new string[]
		{
			"v", "u", "f", "c", "cp", "in", "bc", "fc", "i", "un",
			"st", "b"
		};

		// Token: 0x040099EB RID: 39403
		private static byte[] attributeNamespaceIds;
	}
}
