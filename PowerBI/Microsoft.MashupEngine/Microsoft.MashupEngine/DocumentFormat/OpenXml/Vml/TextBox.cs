using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Wordprocessing;

namespace DocumentFormat.OpenXml.Vml
{
	// Token: 0x02002248 RID: 8776
	[ChildElementInfo(typeof(TextBoxContent))]
	[GeneratedCode("DomGen", "2.0")]
	internal class TextBox : OpenXmlCompositeElement
	{
		// Token: 0x170039F1 RID: 14833
		// (get) Token: 0x0600E19C RID: 57756 RVA: 0x002C0E57 File Offset: 0x002BF057
		public override string LocalName
		{
			get
			{
				return "textbox";
			}
		}

		// Token: 0x170039F2 RID: 14834
		// (get) Token: 0x0600E19D RID: 57757 RVA: 0x00243C87 File Offset: 0x00241E87
		internal override byte NamespaceId
		{
			get
			{
				return 26;
			}
		}

		// Token: 0x170039F3 RID: 14835
		// (get) Token: 0x0600E19E RID: 57758 RVA: 0x002C0E5E File Offset: 0x002BF05E
		internal override int ElementTypeId
		{
			get
			{
				return 12512;
			}
		}

		// Token: 0x0600E19F RID: 57759 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170039F4 RID: 14836
		// (get) Token: 0x0600E1A0 RID: 57760 RVA: 0x002C0E65 File Offset: 0x002BF065
		internal override string[] AttributeTagNames
		{
			get
			{
				return TextBox.attributeTagNames;
			}
		}

		// Token: 0x170039F5 RID: 14837
		// (get) Token: 0x0600E1A1 RID: 57761 RVA: 0x002C0E6C File Offset: 0x002BF06C
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TextBox.attributeNamespaceIds;
			}
		}

		// Token: 0x170039F6 RID: 14838
		// (get) Token: 0x0600E1A2 RID: 57762 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600E1A3 RID: 57763 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "id")]
		public StringValue Id
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

		// Token: 0x170039F7 RID: 14839
		// (get) Token: 0x0600E1A4 RID: 57764 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600E1A5 RID: 57765 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "style")]
		public StringValue Style
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

		// Token: 0x170039F8 RID: 14840
		// (get) Token: 0x0600E1A6 RID: 57766 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600E1A7 RID: 57767 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "inset")]
		public StringValue Inset
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

		// Token: 0x170039F9 RID: 14841
		// (get) Token: 0x0600E1A8 RID: 57768 RVA: 0x002BD49F File Offset: 0x002BB69F
		// (set) Token: 0x0600E1A9 RID: 57769 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(27, "singleclick")]
		public TrueFalseValue SingleClick
		{
			get
			{
				return (TrueFalseValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x0600E1AA RID: 57770 RVA: 0x00293ECF File Offset: 0x002920CF
		public TextBox()
		{
		}

		// Token: 0x0600E1AB RID: 57771 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TextBox(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600E1AC RID: 57772 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TextBox(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600E1AD RID: 57773 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TextBox(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600E1AE RID: 57774 RVA: 0x002C0E73 File Offset: 0x002BF073
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "txbxContent" == name)
			{
				return new TextBoxContent();
			}
			return null;
		}

		// Token: 0x0600E1AF RID: 57775 RVA: 0x002C0E90 File Offset: 0x002BF090
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "id" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "style" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "inset" == name)
			{
				return new StringValue();
			}
			if (27 == namespaceId && "singleclick" == name)
			{
				return new TrueFalseValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600E1B0 RID: 57776 RVA: 0x002C0EFF File Offset: 0x002BF0FF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TextBox>(deep);
		}

		// Token: 0x04006EAE RID: 28334
		private const string tagName = "textbox";

		// Token: 0x04006EAF RID: 28335
		private const byte tagNsId = 26;

		// Token: 0x04006EB0 RID: 28336
		internal const int ElementTypeIdConst = 12512;

		// Token: 0x04006EB1 RID: 28337
		private static string[] attributeTagNames = new string[] { "id", "style", "inset", "singleclick" };

		// Token: 0x04006EB2 RID: 28338
		private static byte[] attributeNamespaceIds = new byte[] { 0, 0, 0, 27 };
	}
}
