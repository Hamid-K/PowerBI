using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BDA RID: 11226
	[GeneratedCode("DomGen", "2.0")]
	internal class PageMargins : OpenXmlLeafElement
	{
		// Token: 0x17007D67 RID: 32103
		// (get) Token: 0x06017713 RID: 96019 RVA: 0x002F66A6 File Offset: 0x002F48A6
		public override string LocalName
		{
			get
			{
				return "pageMargins";
			}
		}

		// Token: 0x17007D68 RID: 32104
		// (get) Token: 0x06017714 RID: 96020 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007D69 RID: 32105
		// (get) Token: 0x06017715 RID: 96021 RVA: 0x00336CE0 File Offset: 0x00334EE0
		internal override int ElementTypeId
		{
			get
			{
				return 11198;
			}
		}

		// Token: 0x06017716 RID: 96022 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007D6A RID: 32106
		// (get) Token: 0x06017717 RID: 96023 RVA: 0x00336CE7 File Offset: 0x00334EE7
		internal override string[] AttributeTagNames
		{
			get
			{
				return PageMargins.attributeTagNames;
			}
		}

		// Token: 0x17007D6B RID: 32107
		// (get) Token: 0x06017718 RID: 96024 RVA: 0x00336CEE File Offset: 0x00334EEE
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PageMargins.attributeNamespaceIds;
			}
		}

		// Token: 0x17007D6C RID: 32108
		// (get) Token: 0x06017719 RID: 96025 RVA: 0x002E7DC5 File Offset: 0x002E5FC5
		// (set) Token: 0x0601771A RID: 96026 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "left")]
		public DoubleValue Left
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

		// Token: 0x17007D6D RID: 32109
		// (get) Token: 0x0601771B RID: 96027 RVA: 0x002E7DD4 File Offset: 0x002E5FD4
		// (set) Token: 0x0601771C RID: 96028 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "right")]
		public DoubleValue Right
		{
			get
			{
				return (DoubleValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17007D6E RID: 32110
		// (get) Token: 0x0601771D RID: 96029 RVA: 0x002E7DE3 File Offset: 0x002E5FE3
		// (set) Token: 0x0601771E RID: 96030 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "top")]
		public DoubleValue Top
		{
			get
			{
				return (DoubleValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17007D6F RID: 32111
		// (get) Token: 0x0601771F RID: 96031 RVA: 0x002F66C2 File Offset: 0x002F48C2
		// (set) Token: 0x06017720 RID: 96032 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "bottom")]
		public DoubleValue Bottom
		{
			get
			{
				return (DoubleValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17007D70 RID: 32112
		// (get) Token: 0x06017721 RID: 96033 RVA: 0x002E82DC File Offset: 0x002E64DC
		// (set) Token: 0x06017722 RID: 96034 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "header")]
		public DoubleValue Header
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

		// Token: 0x17007D71 RID: 32113
		// (get) Token: 0x06017723 RID: 96035 RVA: 0x002F66D1 File Offset: 0x002F48D1
		// (set) Token: 0x06017724 RID: 96036 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "footer")]
		public DoubleValue Footer
		{
			get
			{
				return (DoubleValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x06017726 RID: 96038 RVA: 0x00336CF8 File Offset: 0x00334EF8
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "left" == name)
			{
				return new DoubleValue();
			}
			if (namespaceId == 0 && "right" == name)
			{
				return new DoubleValue();
			}
			if (namespaceId == 0 && "top" == name)
			{
				return new DoubleValue();
			}
			if (namespaceId == 0 && "bottom" == name)
			{
				return new DoubleValue();
			}
			if (namespaceId == 0 && "header" == name)
			{
				return new DoubleValue();
			}
			if (namespaceId == 0 && "footer" == name)
			{
				return new DoubleValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017727 RID: 96039 RVA: 0x00336D91 File Offset: 0x00334F91
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PageMargins>(deep);
		}

		// Token: 0x06017728 RID: 96040 RVA: 0x00336D9C File Offset: 0x00334F9C
		// Note: this type is marked as 'beforefieldinit'.
		static PageMargins()
		{
			byte[] array = new byte[6];
			PageMargins.attributeNamespaceIds = array;
		}

		// Token: 0x04009C5E RID: 40030
		private const string tagName = "pageMargins";

		// Token: 0x04009C5F RID: 40031
		private const byte tagNsId = 22;

		// Token: 0x04009C60 RID: 40032
		internal const int ElementTypeIdConst = 11198;

		// Token: 0x04009C61 RID: 40033
		private static string[] attributeTagNames = new string[] { "left", "right", "top", "bottom", "header", "footer" };

		// Token: 0x04009C62 RID: 40034
		private static byte[] attributeNamespaceIds;
	}
}
