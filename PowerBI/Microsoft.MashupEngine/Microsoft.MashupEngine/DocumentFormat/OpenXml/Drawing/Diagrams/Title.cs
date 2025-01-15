using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x02002678 RID: 9848
	[GeneratedCode("DomGen", "2.0")]
	internal class Title : OpenXmlLeafElement
	{
		// Token: 0x17005C76 RID: 23670
		// (get) Token: 0x06012CE6 RID: 77030 RVA: 0x002F2B3B File Offset: 0x002F0D3B
		public override string LocalName
		{
			get
			{
				return "title";
			}
		}

		// Token: 0x17005C77 RID: 23671
		// (get) Token: 0x06012CE7 RID: 77031 RVA: 0x0006808E File Offset: 0x0006628E
		internal override byte NamespaceId
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17005C78 RID: 23672
		// (get) Token: 0x06012CE8 RID: 77032 RVA: 0x002FFAEB File Offset: 0x002FDCEB
		internal override int ElementTypeId
		{
			get
			{
				return 10663;
			}
		}

		// Token: 0x06012CE9 RID: 77033 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005C79 RID: 23673
		// (get) Token: 0x06012CEA RID: 77034 RVA: 0x002FFAF2 File Offset: 0x002FDCF2
		internal override string[] AttributeTagNames
		{
			get
			{
				return Title.attributeTagNames;
			}
		}

		// Token: 0x17005C7A RID: 23674
		// (get) Token: 0x06012CEB RID: 77035 RVA: 0x002FFAF9 File Offset: 0x002FDCF9
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Title.attributeNamespaceIds;
			}
		}

		// Token: 0x17005C7B RID: 23675
		// (get) Token: 0x06012CEC RID: 77036 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06012CED RID: 77037 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "lang")]
		public StringValue Language
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

		// Token: 0x17005C7C RID: 23676
		// (get) Token: 0x06012CEE RID: 77038 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06012CEF RID: 77039 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "val")]
		public StringValue Val
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

		// Token: 0x06012CF1 RID: 77041 RVA: 0x002FDB15 File Offset: 0x002FBD15
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "lang" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "val" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06012CF2 RID: 77042 RVA: 0x002FFB00 File Offset: 0x002FDD00
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Title>(deep);
		}

		// Token: 0x06012CF3 RID: 77043 RVA: 0x002FFB0C File Offset: 0x002FDD0C
		// Note: this type is marked as 'beforefieldinit'.
		static Title()
		{
			byte[] array = new byte[2];
			Title.attributeNamespaceIds = array;
		}

		// Token: 0x040081A7 RID: 33191
		private const string tagName = "title";

		// Token: 0x040081A8 RID: 33192
		private const byte tagNsId = 14;

		// Token: 0x040081A9 RID: 33193
		internal const int ElementTypeIdConst = 10663;

		// Token: 0x040081AA RID: 33194
		private static string[] attributeTagNames = new string[] { "lang", "val" };

		// Token: 0x040081AB RID: 33195
		private static byte[] attributeNamespaceIds;
	}
}
