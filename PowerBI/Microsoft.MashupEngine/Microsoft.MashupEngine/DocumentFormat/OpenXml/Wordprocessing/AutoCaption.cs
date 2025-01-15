using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FCA RID: 12234
	[GeneratedCode("DomGen", "2.0")]
	internal class AutoCaption : OpenXmlLeafElement
	{
		// Token: 0x17009419 RID: 37913
		// (get) Token: 0x0601A8AB RID: 108715 RVA: 0x00363DCC File Offset: 0x00361FCC
		public override string LocalName
		{
			get
			{
				return "autoCaption";
			}
		}

		// Token: 0x1700941A RID: 37914
		// (get) Token: 0x0601A8AC RID: 108716 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700941B RID: 37915
		// (get) Token: 0x0601A8AD RID: 108717 RVA: 0x00363DD3 File Offset: 0x00361FD3
		internal override int ElementTypeId
		{
			get
			{
				return 11942;
			}
		}

		// Token: 0x0601A8AE RID: 108718 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700941C RID: 37916
		// (get) Token: 0x0601A8AF RID: 108719 RVA: 0x00363DDA File Offset: 0x00361FDA
		internal override string[] AttributeTagNames
		{
			get
			{
				return AutoCaption.attributeTagNames;
			}
		}

		// Token: 0x1700941D RID: 37917
		// (get) Token: 0x0601A8B0 RID: 108720 RVA: 0x00363DE1 File Offset: 0x00361FE1
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return AutoCaption.attributeNamespaceIds;
			}
		}

		// Token: 0x1700941E RID: 37918
		// (get) Token: 0x0601A8B1 RID: 108721 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601A8B2 RID: 108722 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "name")]
		public StringValue Name
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

		// Token: 0x1700941F RID: 37919
		// (get) Token: 0x0601A8B3 RID: 108723 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0601A8B4 RID: 108724 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "caption")]
		public StringValue Caption
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

		// Token: 0x0601A8B6 RID: 108726 RVA: 0x00363DE8 File Offset: 0x00361FE8
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "name" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "caption" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601A8B7 RID: 108727 RVA: 0x00363E22 File Offset: 0x00362022
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AutoCaption>(deep);
		}

		// Token: 0x0400AD6E RID: 44398
		private const string tagName = "autoCaption";

		// Token: 0x0400AD6F RID: 44399
		private const byte tagNsId = 23;

		// Token: 0x0400AD70 RID: 44400
		internal const int ElementTypeIdConst = 11942;

		// Token: 0x0400AD71 RID: 44401
		private static string[] attributeTagNames = new string[] { "name", "caption" };

		// Token: 0x0400AD72 RID: 44402
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23 };
	}
}
