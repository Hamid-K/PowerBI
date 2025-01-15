using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Wordprocessing;

namespace DocumentFormat.OpenXml.Office2010.Word.DrawingShape
{
	// Token: 0x02002500 RID: 9472
	[ChildElementInfo(typeof(TextBoxContent))]
	[ChildElementInfo(typeof(OfficeArtExtensionList), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class TextBoxInfo2 : OpenXmlCompositeElement
	{
		// Token: 0x170053E0 RID: 21472
		// (get) Token: 0x060119AD RID: 72109 RVA: 0x002F06E8 File Offset: 0x002EE8E8
		public override string LocalName
		{
			get
			{
				return "txbx";
			}
		}

		// Token: 0x170053E1 RID: 21473
		// (get) Token: 0x060119AE RID: 72110 RVA: 0x002EFE53 File Offset: 0x002EE053
		internal override byte NamespaceId
		{
			get
			{
				return 61;
			}
		}

		// Token: 0x170053E2 RID: 21474
		// (get) Token: 0x060119AF RID: 72111 RVA: 0x002F06EF File Offset: 0x002EE8EF
		internal override int ElementTypeId
		{
			get
			{
				return 13138;
			}
		}

		// Token: 0x060119B0 RID: 72112 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x170053E3 RID: 21475
		// (get) Token: 0x060119B1 RID: 72113 RVA: 0x002F06F6 File Offset: 0x002EE8F6
		internal override string[] AttributeTagNames
		{
			get
			{
				return TextBoxInfo2.attributeTagNames;
			}
		}

		// Token: 0x170053E4 RID: 21476
		// (get) Token: 0x060119B2 RID: 72114 RVA: 0x002F06FD File Offset: 0x002EE8FD
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TextBoxInfo2.attributeNamespaceIds;
			}
		}

		// Token: 0x170053E5 RID: 21477
		// (get) Token: 0x060119B3 RID: 72115 RVA: 0x002F0704 File Offset: 0x002EE904
		// (set) Token: 0x060119B4 RID: 72116 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "id")]
		public UInt16Value Id
		{
			get
			{
				return (UInt16Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x060119B5 RID: 72117 RVA: 0x00293ECF File Offset: 0x002920CF
		public TextBoxInfo2()
		{
		}

		// Token: 0x060119B6 RID: 72118 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TextBoxInfo2(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060119B7 RID: 72119 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TextBoxInfo2(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060119B8 RID: 72120 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TextBoxInfo2(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060119B9 RID: 72121 RVA: 0x002F0713 File Offset: 0x002EE913
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "txbxContent" == name)
			{
				return new TextBoxContent();
			}
			if (61 == namespaceId && "extLst" == name)
			{
				return new OfficeArtExtensionList();
			}
			return null;
		}

		// Token: 0x170053E6 RID: 21478
		// (get) Token: 0x060119BA RID: 72122 RVA: 0x002F0746 File Offset: 0x002EE946
		internal override string[] ElementTagNames
		{
			get
			{
				return TextBoxInfo2.eleTagNames;
			}
		}

		// Token: 0x170053E7 RID: 21479
		// (get) Token: 0x060119BB RID: 72123 RVA: 0x002F074D File Offset: 0x002EE94D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return TextBoxInfo2.eleNamespaceIds;
			}
		}

		// Token: 0x170053E8 RID: 21480
		// (get) Token: 0x060119BC RID: 72124 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170053E9 RID: 21481
		// (get) Token: 0x060119BD RID: 72125 RVA: 0x002F0754 File Offset: 0x002EE954
		// (set) Token: 0x060119BE RID: 72126 RVA: 0x002F075D File Offset: 0x002EE95D
		public TextBoxContent TextBoxContent
		{
			get
			{
				return base.GetElement<TextBoxContent>(0);
			}
			set
			{
				base.SetElement<TextBoxContent>(0, value);
			}
		}

		// Token: 0x170053EA RID: 21482
		// (get) Token: 0x060119BF RID: 72127 RVA: 0x002F0767 File Offset: 0x002EE967
		// (set) Token: 0x060119C0 RID: 72128 RVA: 0x002F0770 File Offset: 0x002EE970
		public OfficeArtExtensionList OfficeArtExtensionList
		{
			get
			{
				return base.GetElement<OfficeArtExtensionList>(1);
			}
			set
			{
				base.SetElement<OfficeArtExtensionList>(1, value);
			}
		}

		// Token: 0x060119C1 RID: 72129 RVA: 0x002F077A File Offset: 0x002EE97A
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "id" == name)
			{
				return new UInt16Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060119C2 RID: 72130 RVA: 0x002F079A File Offset: 0x002EE99A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TextBoxInfo2>(deep);
		}

		// Token: 0x060119C3 RID: 72131 RVA: 0x002F07A4 File Offset: 0x002EE9A4
		// Note: this type is marked as 'beforefieldinit'.
		static TextBoxInfo2()
		{
			byte[] array = new byte[1];
			TextBoxInfo2.attributeNamespaceIds = array;
			TextBoxInfo2.eleTagNames = new string[] { "txbxContent", "extLst" };
			TextBoxInfo2.eleNamespaceIds = new byte[] { 23, 61 };
		}

		// Token: 0x04007B82 RID: 31618
		private const string tagName = "txbx";

		// Token: 0x04007B83 RID: 31619
		private const byte tagNsId = 61;

		// Token: 0x04007B84 RID: 31620
		internal const int ElementTypeIdConst = 13138;

		// Token: 0x04007B85 RID: 31621
		private static string[] attributeTagNames = new string[] { "id" };

		// Token: 0x04007B86 RID: 31622
		private static byte[] attributeNamespaceIds;

		// Token: 0x04007B87 RID: 31623
		private static readonly string[] eleTagNames;

		// Token: 0x04007B88 RID: 31624
		private static readonly byte[] eleNamespaceIds;
	}
}
