using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x020029F4 RID: 10740
	[GeneratedCode("DomGen", "2.0")]
	internal class BrowseSlideMode : OpenXmlLeafElement
	{
		// Token: 0x17006E7C RID: 28284
		// (get) Token: 0x060155EC RID: 87532 RVA: 0x0031E3E3 File Offset: 0x0031C5E3
		public override string LocalName
		{
			get
			{
				return "browse";
			}
		}

		// Token: 0x17006E7D RID: 28285
		// (get) Token: 0x060155ED RID: 87533 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17006E7E RID: 28286
		// (get) Token: 0x060155EE RID: 87534 RVA: 0x0031E3EA File Offset: 0x0031C5EA
		internal override int ElementTypeId
		{
			get
			{
				return 12167;
			}
		}

		// Token: 0x060155EF RID: 87535 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17006E7F RID: 28287
		// (get) Token: 0x060155F0 RID: 87536 RVA: 0x0031E3F1 File Offset: 0x0031C5F1
		internal override string[] AttributeTagNames
		{
			get
			{
				return BrowseSlideMode.attributeTagNames;
			}
		}

		// Token: 0x17006E80 RID: 28288
		// (get) Token: 0x060155F1 RID: 87537 RVA: 0x0031E3F8 File Offset: 0x0031C5F8
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return BrowseSlideMode.attributeNamespaceIds;
			}
		}

		// Token: 0x17006E81 RID: 28289
		// (get) Token: 0x060155F2 RID: 87538 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x060155F3 RID: 87539 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "showScrollbar")]
		public BooleanValue ShowScrollbar
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

		// Token: 0x060155F5 RID: 87541 RVA: 0x0031E3FF File Offset: 0x0031C5FF
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "showScrollbar" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060155F6 RID: 87542 RVA: 0x0031E41F File Offset: 0x0031C61F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BrowseSlideMode>(deep);
		}

		// Token: 0x060155F7 RID: 87543 RVA: 0x0031E428 File Offset: 0x0031C628
		// Note: this type is marked as 'beforefieldinit'.
		static BrowseSlideMode()
		{
			byte[] array = new byte[1];
			BrowseSlideMode.attributeNamespaceIds = array;
		}

		// Token: 0x04009339 RID: 37689
		private const string tagName = "browse";

		// Token: 0x0400933A RID: 37690
		private const byte tagNsId = 24;

		// Token: 0x0400933B RID: 37691
		internal const int ElementTypeIdConst = 12167;

		// Token: 0x0400933C RID: 37692
		private static string[] attributeTagNames = new string[] { "showScrollbar" };

		// Token: 0x0400933D RID: 37693
		private static byte[] attributeNamespaceIds;
	}
}
