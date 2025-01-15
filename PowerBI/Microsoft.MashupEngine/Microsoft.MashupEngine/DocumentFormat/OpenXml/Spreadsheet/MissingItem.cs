using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B54 RID: 11092
	[ChildElementInfo(typeof(MemberPropertyIndex))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Tuples))]
	internal class MissingItem : OpenXmlCompositeElement
	{
		// Token: 0x17007863 RID: 30819
		// (get) Token: 0x06016C40 RID: 93248 RVA: 0x002E0FCF File Offset: 0x002DF1CF
		public override string LocalName
		{
			get
			{
				return "m";
			}
		}

		// Token: 0x17007864 RID: 30820
		// (get) Token: 0x06016C41 RID: 93249 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007865 RID: 30821
		// (get) Token: 0x06016C42 RID: 93250 RVA: 0x0032ECF0 File Offset: 0x0032CEF0
		internal override int ElementTypeId
		{
			get
			{
				return 11075;
			}
		}

		// Token: 0x06016C43 RID: 93251 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007866 RID: 30822
		// (get) Token: 0x06016C44 RID: 93252 RVA: 0x0032ECF7 File Offset: 0x0032CEF7
		internal override string[] AttributeTagNames
		{
			get
			{
				return MissingItem.attributeTagNames;
			}
		}

		// Token: 0x17007867 RID: 30823
		// (get) Token: 0x06016C45 RID: 93253 RVA: 0x0032ECFE File Offset: 0x0032CEFE
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return MissingItem.attributeNamespaceIds;
			}
		}

		// Token: 0x17007868 RID: 30824
		// (get) Token: 0x06016C46 RID: 93254 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x06016C47 RID: 93255 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "u")]
		public BooleanValue Unused
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

		// Token: 0x17007869 RID: 30825
		// (get) Token: 0x06016C48 RID: 93256 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x06016C49 RID: 93257 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "f")]
		public BooleanValue Calculated
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

		// Token: 0x1700786A RID: 30826
		// (get) Token: 0x06016C4A RID: 93258 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x06016C4B RID: 93259 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "c")]
		public StringValue Caption
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

		// Token: 0x1700786B RID: 30827
		// (get) Token: 0x06016C4C RID: 93260 RVA: 0x002E5B0D File Offset: 0x002E3D0D
		// (set) Token: 0x06016C4D RID: 93261 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "cp")]
		public UInt32Value PropertyCount
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

		// Token: 0x1700786C RID: 30828
		// (get) Token: 0x06016C4E RID: 93262 RVA: 0x002E6C42 File Offset: 0x002E4E42
		// (set) Token: 0x06016C4F RID: 93263 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "in")]
		public UInt32Value FormatIndex
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

		// Token: 0x1700786D RID: 30829
		// (get) Token: 0x06016C50 RID: 93264 RVA: 0x003137E6 File Offset: 0x003119E6
		// (set) Token: 0x06016C51 RID: 93265 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "bc")]
		public HexBinaryValue BackgroundColor
		{
			get
			{
				return (HexBinaryValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x1700786E RID: 30830
		// (get) Token: 0x06016C52 RID: 93266 RVA: 0x0032ED05 File Offset: 0x0032CF05
		// (set) Token: 0x06016C53 RID: 93267 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "fc")]
		public HexBinaryValue ForegroundColor
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

		// Token: 0x1700786F RID: 30831
		// (get) Token: 0x06016C54 RID: 93268 RVA: 0x002CA736 File Offset: 0x002C8936
		// (set) Token: 0x06016C55 RID: 93269 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "i")]
		public BooleanValue Italic
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

		// Token: 0x17007870 RID: 30832
		// (get) Token: 0x06016C56 RID: 93270 RVA: 0x002CB706 File Offset: 0x002C9906
		// (set) Token: 0x06016C57 RID: 93271 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "un")]
		public BooleanValue Underline
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

		// Token: 0x17007871 RID: 30833
		// (get) Token: 0x06016C58 RID: 93272 RVA: 0x002C92EA File Offset: 0x002C74EA
		// (set) Token: 0x06016C59 RID: 93273 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "st")]
		public BooleanValue Strikethrough
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

		// Token: 0x17007872 RID: 30834
		// (get) Token: 0x06016C5A RID: 93274 RVA: 0x002C8762 File Offset: 0x002C6962
		// (set) Token: 0x06016C5B RID: 93275 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "b")]
		public BooleanValue Bold
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

		// Token: 0x06016C5C RID: 93276 RVA: 0x00293ECF File Offset: 0x002920CF
		public MissingItem()
		{
		}

		// Token: 0x06016C5D RID: 93277 RVA: 0x00293ED7 File Offset: 0x002920D7
		public MissingItem(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016C5E RID: 93278 RVA: 0x00293EE0 File Offset: 0x002920E0
		public MissingItem(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016C5F RID: 93279 RVA: 0x00293EE9 File Offset: 0x002920E9
		public MissingItem(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016C60 RID: 93280 RVA: 0x0032ED14 File Offset: 0x0032CF14
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

		// Token: 0x06016C61 RID: 93281 RVA: 0x0032ED48 File Offset: 0x0032CF48
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
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

		// Token: 0x06016C62 RID: 93282 RVA: 0x0032EE4F File Offset: 0x0032D04F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MissingItem>(deep);
		}

		// Token: 0x06016C63 RID: 93283 RVA: 0x0032EE58 File Offset: 0x0032D058
		// Note: this type is marked as 'beforefieldinit'.
		static MissingItem()
		{
			byte[] array = new byte[11];
			MissingItem.attributeNamespaceIds = array;
		}

		// Token: 0x040099E2 RID: 39394
		private const string tagName = "m";

		// Token: 0x040099E3 RID: 39395
		private const byte tagNsId = 22;

		// Token: 0x040099E4 RID: 39396
		internal const int ElementTypeIdConst = 11075;

		// Token: 0x040099E5 RID: 39397
		private static string[] attributeTagNames = new string[]
		{
			"u", "f", "c", "cp", "in", "bc", "fc", "i", "un", "st",
			"b"
		};

		// Token: 0x040099E6 RID: 39398
		private static byte[] attributeNamespaceIds;
	}
}
