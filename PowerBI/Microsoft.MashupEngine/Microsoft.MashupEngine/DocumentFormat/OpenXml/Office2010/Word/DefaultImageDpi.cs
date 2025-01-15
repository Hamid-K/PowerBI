using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x020024B8 RID: 9400
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class DefaultImageDpi : OpenXmlLeafElement
	{
		// Token: 0x17005274 RID: 21108
		// (get) Token: 0x06011673 RID: 71283 RVA: 0x002E4A70 File Offset: 0x002E2C70
		public override string LocalName
		{
			get
			{
				return "defaultImageDpi";
			}
		}

		// Token: 0x17005275 RID: 21109
		// (get) Token: 0x06011674 RID: 71284 RVA: 0x002EC7B7 File Offset: 0x002EA9B7
		internal override byte NamespaceId
		{
			get
			{
				return 52;
			}
		}

		// Token: 0x17005276 RID: 21110
		// (get) Token: 0x06011675 RID: 71285 RVA: 0x002EE24A File Offset: 0x002EC44A
		internal override int ElementTypeId
		{
			get
			{
				return 12873;
			}
		}

		// Token: 0x06011676 RID: 71286 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17005277 RID: 21111
		// (get) Token: 0x06011677 RID: 71287 RVA: 0x002EE251 File Offset: 0x002EC451
		internal override string[] AttributeTagNames
		{
			get
			{
				return DefaultImageDpi.attributeTagNames;
			}
		}

		// Token: 0x17005278 RID: 21112
		// (get) Token: 0x06011678 RID: 71288 RVA: 0x002EE258 File Offset: 0x002EC458
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return DefaultImageDpi.attributeNamespaceIds;
			}
		}

		// Token: 0x17005279 RID: 21113
		// (get) Token: 0x06011679 RID: 71289 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x0601167A RID: 71290 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(52, "val")]
		public Int32Value Val
		{
			get
			{
				return (Int32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0601167C RID: 71292 RVA: 0x002EC920 File Offset: 0x002EAB20
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (52 == namespaceId && "val" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601167D RID: 71293 RVA: 0x002EE25F File Offset: 0x002EC45F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DefaultImageDpi>(deep);
		}

		// Token: 0x040079AE RID: 31150
		private const string tagName = "defaultImageDpi";

		// Token: 0x040079AF RID: 31151
		private const byte tagNsId = 52;

		// Token: 0x040079B0 RID: 31152
		internal const int ElementTypeIdConst = 12873;

		// Token: 0x040079B1 RID: 31153
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x040079B2 RID: 31154
		private static byte[] attributeNamespaceIds = new byte[] { 52 };
	}
}
