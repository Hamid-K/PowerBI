using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BB4 RID: 11188
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ExtensionList))]
	internal class RevisionSheetName : OpenXmlCompositeElement
	{
		// Token: 0x17007BC3 RID: 31683
		// (get) Token: 0x06017396 RID: 95126 RVA: 0x003341FF File Offset: 0x003323FF
		public override string LocalName
		{
			get
			{
				return "rsnm";
			}
		}

		// Token: 0x17007BC4 RID: 31684
		// (get) Token: 0x06017397 RID: 95127 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007BC5 RID: 31685
		// (get) Token: 0x06017398 RID: 95128 RVA: 0x00334206 File Offset: 0x00332406
		internal override int ElementTypeId
		{
			get
			{
				return 11159;
			}
		}

		// Token: 0x06017399 RID: 95129 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007BC6 RID: 31686
		// (get) Token: 0x0601739A RID: 95130 RVA: 0x0033420D File Offset: 0x0033240D
		internal override string[] AttributeTagNames
		{
			get
			{
				return RevisionSheetName.attributeTagNames;
			}
		}

		// Token: 0x17007BC7 RID: 31687
		// (get) Token: 0x0601739B RID: 95131 RVA: 0x00334214 File Offset: 0x00332414
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return RevisionSheetName.attributeNamespaceIds;
			}
		}

		// Token: 0x17007BC8 RID: 31688
		// (get) Token: 0x0601739C RID: 95132 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x0601739D RID: 95133 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17007BC9 RID: 31689
		// (get) Token: 0x0601739E RID: 95134 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x0601739F RID: 95135 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17007BCA RID: 31690
		// (get) Token: 0x060173A0 RID: 95136 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x060173A1 RID: 95137 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x17007BCB RID: 31691
		// (get) Token: 0x060173A2 RID: 95138 RVA: 0x002E5B0D File Offset: 0x002E3D0D
		// (set) Token: 0x060173A3 RID: 95139 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "sheetId")]
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

		// Token: 0x17007BCC RID: 31692
		// (get) Token: 0x060173A4 RID: 95140 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x060173A5 RID: 95141 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "oldName")]
		public StringValue OldName
		{
			get
			{
				return (StringValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17007BCD RID: 31693
		// (get) Token: 0x060173A6 RID: 95142 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x060173A7 RID: 95143 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "newName")]
		public StringValue NewName
		{
			get
			{
				return (StringValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x060173A8 RID: 95144 RVA: 0x00293ECF File Offset: 0x002920CF
		public RevisionSheetName()
		{
		}

		// Token: 0x060173A9 RID: 95145 RVA: 0x00293ED7 File Offset: 0x002920D7
		public RevisionSheetName(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060173AA RID: 95146 RVA: 0x00293EE0 File Offset: 0x002920E0
		public RevisionSheetName(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060173AB RID: 95147 RVA: 0x00293EE9 File Offset: 0x002920E9
		public RevisionSheetName(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060173AC RID: 95148 RVA: 0x003328DF File Offset: 0x00330ADF
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17007BCE RID: 31694
		// (get) Token: 0x060173AD RID: 95149 RVA: 0x0033421B File Offset: 0x0033241B
		internal override string[] ElementTagNames
		{
			get
			{
				return RevisionSheetName.eleTagNames;
			}
		}

		// Token: 0x17007BCF RID: 31695
		// (get) Token: 0x060173AE RID: 95150 RVA: 0x00334222 File Offset: 0x00332422
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return RevisionSheetName.eleNamespaceIds;
			}
		}

		// Token: 0x17007BD0 RID: 31696
		// (get) Token: 0x060173AF RID: 95151 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17007BD1 RID: 31697
		// (get) Token: 0x060173B0 RID: 95152 RVA: 0x00332908 File Offset: 0x00330B08
		// (set) Token: 0x060173B1 RID: 95153 RVA: 0x00332911 File Offset: 0x00330B11
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(0);
			}
			set
			{
				base.SetElement<ExtensionList>(0, value);
			}
		}

		// Token: 0x060173B2 RID: 95154 RVA: 0x0033422C File Offset: 0x0033242C
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
			if (namespaceId == 0 && "sheetId" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "oldName" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "newName" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060173B3 RID: 95155 RVA: 0x003342C5 File Offset: 0x003324C5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RevisionSheetName>(deep);
		}

		// Token: 0x060173B4 RID: 95156 RVA: 0x003342D0 File Offset: 0x003324D0
		// Note: this type is marked as 'beforefieldinit'.
		static RevisionSheetName()
		{
			byte[] array = new byte[6];
			RevisionSheetName.attributeNamespaceIds = array;
			RevisionSheetName.eleTagNames = new string[] { "extLst" };
			RevisionSheetName.eleNamespaceIds = new byte[] { 22 };
		}

		// Token: 0x04009BA5 RID: 39845
		private const string tagName = "rsnm";

		// Token: 0x04009BA6 RID: 39846
		private const byte tagNsId = 22;

		// Token: 0x04009BA7 RID: 39847
		internal const int ElementTypeIdConst = 11159;

		// Token: 0x04009BA8 RID: 39848
		private static string[] attributeTagNames = new string[] { "rId", "ua", "ra", "sheetId", "oldName", "newName" };

		// Token: 0x04009BA9 RID: 39849
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009BAA RID: 39850
		private static readonly string[] eleTagNames;

		// Token: 0x04009BAB RID: 39851
		private static readonly byte[] eleNamespaceIds;
	}
}
