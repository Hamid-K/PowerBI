using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B57 RID: 11095
	[ChildElementInfo(typeof(Tuples))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(MemberPropertyIndex))]
	internal class ErrorItem : OpenXmlCompositeElement
	{
		// Token: 0x1700788E RID: 30862
		// (get) Token: 0x06016CA2 RID: 93346 RVA: 0x0031C318 File Offset: 0x0031A518
		public override string LocalName
		{
			get
			{
				return "e";
			}
		}

		// Token: 0x1700788F RID: 30863
		// (get) Token: 0x06016CA3 RID: 93347 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007890 RID: 30864
		// (get) Token: 0x06016CA4 RID: 93348 RVA: 0x0032F1CF File Offset: 0x0032D3CF
		internal override int ElementTypeId
		{
			get
			{
				return 11078;
			}
		}

		// Token: 0x06016CA5 RID: 93349 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007891 RID: 30865
		// (get) Token: 0x06016CA6 RID: 93350 RVA: 0x0032F1D6 File Offset: 0x0032D3D6
		internal override string[] AttributeTagNames
		{
			get
			{
				return ErrorItem.attributeTagNames;
			}
		}

		// Token: 0x17007892 RID: 30866
		// (get) Token: 0x06016CA7 RID: 93351 RVA: 0x0032F1DD File Offset: 0x0032D3DD
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ErrorItem.attributeNamespaceIds;
			}
		}

		// Token: 0x17007893 RID: 30867
		// (get) Token: 0x06016CA8 RID: 93352 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06016CA9 RID: 93353 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17007894 RID: 30868
		// (get) Token: 0x06016CAA RID: 93354 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x06016CAB RID: 93355 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17007895 RID: 30869
		// (get) Token: 0x06016CAC RID: 93356 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x06016CAD RID: 93357 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x17007896 RID: 30870
		// (get) Token: 0x06016CAE RID: 93358 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x06016CAF RID: 93359 RVA: 0x002BD4AE File Offset: 0x002BB6AE
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

		// Token: 0x17007897 RID: 30871
		// (get) Token: 0x06016CB0 RID: 93360 RVA: 0x002E6C42 File Offset: 0x002E4E42
		// (set) Token: 0x06016CB1 RID: 93361 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
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

		// Token: 0x17007898 RID: 30872
		// (get) Token: 0x06016CB2 RID: 93362 RVA: 0x002E6EEB File Offset: 0x002E50EB
		// (set) Token: 0x06016CB3 RID: 93363 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
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

		// Token: 0x17007899 RID: 30873
		// (get) Token: 0x06016CB4 RID: 93364 RVA: 0x0032ED05 File Offset: 0x0032CF05
		// (set) Token: 0x06016CB5 RID: 93365 RVA: 0x002BD4FC File Offset: 0x002BB6FC
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

		// Token: 0x1700789A RID: 30874
		// (get) Token: 0x06016CB6 RID: 93366 RVA: 0x0032EEF7 File Offset: 0x0032D0F7
		// (set) Token: 0x06016CB7 RID: 93367 RVA: 0x002BD516 File Offset: 0x002BB716
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

		// Token: 0x1700789B RID: 30875
		// (get) Token: 0x06016CB8 RID: 93368 RVA: 0x002CB706 File Offset: 0x002C9906
		// (set) Token: 0x06016CB9 RID: 93369 RVA: 0x002BD530 File Offset: 0x002BB730
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

		// Token: 0x1700789C RID: 30876
		// (get) Token: 0x06016CBA RID: 93370 RVA: 0x002C92EA File Offset: 0x002C74EA
		// (set) Token: 0x06016CBB RID: 93371 RVA: 0x002BD54B File Offset: 0x002BB74B
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

		// Token: 0x1700789D RID: 30877
		// (get) Token: 0x06016CBC RID: 93372 RVA: 0x002C8762 File Offset: 0x002C6962
		// (set) Token: 0x06016CBD RID: 93373 RVA: 0x002BDB45 File Offset: 0x002BBD45
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

		// Token: 0x1700789E RID: 30878
		// (get) Token: 0x06016CBE RID: 93374 RVA: 0x002C92FA File Offset: 0x002C74FA
		// (set) Token: 0x06016CBF RID: 93375 RVA: 0x002BDB61 File Offset: 0x002BBD61
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

		// Token: 0x06016CC0 RID: 93376 RVA: 0x00293ECF File Offset: 0x002920CF
		public ErrorItem()
		{
		}

		// Token: 0x06016CC1 RID: 93377 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ErrorItem(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016CC2 RID: 93378 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ErrorItem(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016CC3 RID: 93379 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ErrorItem(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016CC4 RID: 93380 RVA: 0x0032ED14 File Offset: 0x0032CF14
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

		// Token: 0x1700789F RID: 30879
		// (get) Token: 0x06016CC5 RID: 93381 RVA: 0x0032F1E4 File Offset: 0x0032D3E4
		internal override string[] ElementTagNames
		{
			get
			{
				return ErrorItem.eleTagNames;
			}
		}

		// Token: 0x170078A0 RID: 30880
		// (get) Token: 0x06016CC6 RID: 93382 RVA: 0x0032F1EB File Offset: 0x0032D3EB
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ErrorItem.eleNamespaceIds;
			}
		}

		// Token: 0x170078A1 RID: 30881
		// (get) Token: 0x06016CC7 RID: 93383 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170078A2 RID: 30882
		// (get) Token: 0x06016CC8 RID: 93384 RVA: 0x0032F1F2 File Offset: 0x0032D3F2
		// (set) Token: 0x06016CC9 RID: 93385 RVA: 0x0032F1FB File Offset: 0x0032D3FB
		public Tuples Tuples
		{
			get
			{
				return base.GetElement<Tuples>(0);
			}
			set
			{
				base.SetElement<Tuples>(0, value);
			}
		}

		// Token: 0x06016CCA RID: 93386 RVA: 0x0032F208 File Offset: 0x0032D408
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

		// Token: 0x06016CCB RID: 93387 RVA: 0x0032F325 File Offset: 0x0032D525
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ErrorItem>(deep);
		}

		// Token: 0x06016CCC RID: 93388 RVA: 0x0032F330 File Offset: 0x0032D530
		// Note: this type is marked as 'beforefieldinit'.
		static ErrorItem()
		{
			byte[] array = new byte[12];
			ErrorItem.attributeNamespaceIds = array;
			ErrorItem.eleTagNames = new string[] { "tpls", "x" };
			ErrorItem.eleNamespaceIds = new byte[] { 22, 22 };
		}

		// Token: 0x040099F1 RID: 39409
		private const string tagName = "e";

		// Token: 0x040099F2 RID: 39410
		private const byte tagNsId = 22;

		// Token: 0x040099F3 RID: 39411
		internal const int ElementTypeIdConst = 11078;

		// Token: 0x040099F4 RID: 39412
		private static string[] attributeTagNames = new string[]
		{
			"v", "u", "f", "c", "cp", "in", "bc", "fc", "i", "un",
			"st", "b"
		};

		// Token: 0x040099F5 RID: 39413
		private static byte[] attributeNamespaceIds;

		// Token: 0x040099F6 RID: 39414
		private static readonly string[] eleTagNames;

		// Token: 0x040099F7 RID: 39415
		private static readonly byte[] eleNamespaceIds;
	}
}
