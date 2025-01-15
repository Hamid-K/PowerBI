using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BB7 RID: 11191
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(DifferentialFormat))]
	internal class RevisionFormat : OpenXmlCompositeElement
	{
		// Token: 0x17007BF8 RID: 31736
		// (get) Token: 0x06017402 RID: 95234 RVA: 0x003347B5 File Offset: 0x003329B5
		public override string LocalName
		{
			get
			{
				return "rfmt";
			}
		}

		// Token: 0x17007BF9 RID: 31737
		// (get) Token: 0x06017403 RID: 95235 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007BFA RID: 31738
		// (get) Token: 0x06017404 RID: 95236 RVA: 0x003347BC File Offset: 0x003329BC
		internal override int ElementTypeId
		{
			get
			{
				return 11162;
			}
		}

		// Token: 0x06017405 RID: 95237 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007BFB RID: 31739
		// (get) Token: 0x06017406 RID: 95238 RVA: 0x003347C3 File Offset: 0x003329C3
		internal override string[] AttributeTagNames
		{
			get
			{
				return RevisionFormat.attributeTagNames;
			}
		}

		// Token: 0x17007BFC RID: 31740
		// (get) Token: 0x06017407 RID: 95239 RVA: 0x003347CA File Offset: 0x003329CA
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return RevisionFormat.attributeNamespaceIds;
			}
		}

		// Token: 0x17007BFD RID: 31741
		// (get) Token: 0x06017408 RID: 95240 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06017409 RID: 95241 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "sheetId")]
		public UInt32Value SheetId
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

		// Token: 0x17007BFE RID: 31742
		// (get) Token: 0x0601740A RID: 95242 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x0601740B RID: 95243 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "xfDxf")]
		public BooleanValue RowOrColumnAffected
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

		// Token: 0x17007BFF RID: 31743
		// (get) Token: 0x0601740C RID: 95244 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x0601740D RID: 95245 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "s")]
		public BooleanValue StyleAffected
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

		// Token: 0x17007C00 RID: 31744
		// (get) Token: 0x0601740E RID: 95246 RVA: 0x003347D1 File Offset: 0x003329D1
		// (set) Token: 0x0601740F RID: 95247 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "sqref")]
		public ListValue<StringValue> SequenceOfReferences
		{
			get
			{
				return (ListValue<StringValue>)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17007C01 RID: 31745
		// (get) Token: 0x06017410 RID: 95248 RVA: 0x002E6C42 File Offset: 0x002E4E42
		// (set) Token: 0x06017411 RID: 95249 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "start")]
		public UInt32Value Start
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

		// Token: 0x17007C02 RID: 31746
		// (get) Token: 0x06017412 RID: 95250 RVA: 0x002E6EEB File Offset: 0x002E50EB
		// (set) Token: 0x06017413 RID: 95251 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "length")]
		public UInt32Value Length
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

		// Token: 0x06017414 RID: 95252 RVA: 0x00293ECF File Offset: 0x002920CF
		public RevisionFormat()
		{
		}

		// Token: 0x06017415 RID: 95253 RVA: 0x00293ED7 File Offset: 0x002920D7
		public RevisionFormat(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017416 RID: 95254 RVA: 0x00293EE0 File Offset: 0x002920E0
		public RevisionFormat(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017417 RID: 95255 RVA: 0x00293EE9 File Offset: 0x002920E9
		public RevisionFormat(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06017418 RID: 95256 RVA: 0x003347E0 File Offset: 0x003329E0
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "dxf" == name)
			{
				return new DifferentialFormat();
			}
			if (22 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17007C03 RID: 31747
		// (get) Token: 0x06017419 RID: 95257 RVA: 0x00334813 File Offset: 0x00332A13
		internal override string[] ElementTagNames
		{
			get
			{
				return RevisionFormat.eleTagNames;
			}
		}

		// Token: 0x17007C04 RID: 31748
		// (get) Token: 0x0601741A RID: 95258 RVA: 0x0033481A File Offset: 0x00332A1A
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return RevisionFormat.eleNamespaceIds;
			}
		}

		// Token: 0x17007C05 RID: 31749
		// (get) Token: 0x0601741B RID: 95259 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17007C06 RID: 31750
		// (get) Token: 0x0601741C RID: 95260 RVA: 0x00334821 File Offset: 0x00332A21
		// (set) Token: 0x0601741D RID: 95261 RVA: 0x0033482A File Offset: 0x00332A2A
		public DifferentialFormat DifferentialFormat
		{
			get
			{
				return base.GetElement<DifferentialFormat>(0);
			}
			set
			{
				base.SetElement<DifferentialFormat>(0, value);
			}
		}

		// Token: 0x17007C07 RID: 31751
		// (get) Token: 0x0601741E RID: 95262 RVA: 0x002E96EA File Offset: 0x002E78EA
		// (set) Token: 0x0601741F RID: 95263 RVA: 0x002E96F3 File Offset: 0x002E78F3
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(1);
			}
			set
			{
				base.SetElement<ExtensionList>(1, value);
			}
		}

		// Token: 0x06017420 RID: 95264 RVA: 0x00334834 File Offset: 0x00332A34
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "sheetId" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "xfDxf" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "s" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "sqref" == name)
			{
				return new ListValue<StringValue>();
			}
			if (namespaceId == 0 && "start" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "length" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017421 RID: 95265 RVA: 0x003348CD File Offset: 0x00332ACD
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RevisionFormat>(deep);
		}

		// Token: 0x06017422 RID: 95266 RVA: 0x003348D8 File Offset: 0x00332AD8
		// Note: this type is marked as 'beforefieldinit'.
		static RevisionFormat()
		{
			byte[] array = new byte[6];
			RevisionFormat.attributeNamespaceIds = array;
			RevisionFormat.eleTagNames = new string[] { "dxf", "extLst" };
			RevisionFormat.eleNamespaceIds = new byte[] { 22, 22 };
		}

		// Token: 0x04009BB8 RID: 39864
		private const string tagName = "rfmt";

		// Token: 0x04009BB9 RID: 39865
		private const byte tagNsId = 22;

		// Token: 0x04009BBA RID: 39866
		internal const int ElementTypeIdConst = 11162;

		// Token: 0x04009BBB RID: 39867
		private static string[] attributeTagNames = new string[] { "sheetId", "xfDxf", "s", "sqref", "start", "length" };

		// Token: 0x04009BBC RID: 39868
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009BBD RID: 39869
		private static readonly string[] eleTagNames;

		// Token: 0x04009BBE RID: 39870
		private static readonly byte[] eleNamespaceIds;
	}
}
