using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x0200268E RID: 9870
	[GeneratedCode("DomGen", "2.0")]
	internal class StyleLabelDescription : OpenXmlLeafElement
	{
		// Token: 0x17005D08 RID: 23816
		// (get) Token: 0x06012E20 RID: 77344 RVA: 0x002FDB8B File Offset: 0x002FBD8B
		public override string LocalName
		{
			get
			{
				return "desc";
			}
		}

		// Token: 0x17005D09 RID: 23817
		// (get) Token: 0x06012E21 RID: 77345 RVA: 0x0006808E File Offset: 0x0006628E
		internal override byte NamespaceId
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17005D0A RID: 23818
		// (get) Token: 0x06012E22 RID: 77346 RVA: 0x003005BB File Offset: 0x002FE7BB
		internal override int ElementTypeId
		{
			get
			{
				return 10685;
			}
		}

		// Token: 0x06012E23 RID: 77347 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005D0B RID: 23819
		// (get) Token: 0x06012E24 RID: 77348 RVA: 0x003005C2 File Offset: 0x002FE7C2
		internal override string[] AttributeTagNames
		{
			get
			{
				return StyleLabelDescription.attributeTagNames;
			}
		}

		// Token: 0x17005D0C RID: 23820
		// (get) Token: 0x06012E25 RID: 77349 RVA: 0x003005C9 File Offset: 0x002FE7C9
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return StyleLabelDescription.attributeNamespaceIds;
			}
		}

		// Token: 0x17005D0D RID: 23821
		// (get) Token: 0x06012E26 RID: 77350 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06012E27 RID: 77351 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17005D0E RID: 23822
		// (get) Token: 0x06012E28 RID: 77352 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06012E29 RID: 77353 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x06012E2B RID: 77355 RVA: 0x002FDB15 File Offset: 0x002FBD15
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

		// Token: 0x06012E2C RID: 77356 RVA: 0x003005D0 File Offset: 0x002FE7D0
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<StyleLabelDescription>(deep);
		}

		// Token: 0x06012E2D RID: 77357 RVA: 0x003005DC File Offset: 0x002FE7DC
		// Note: this type is marked as 'beforefieldinit'.
		static StyleLabelDescription()
		{
			byte[] array = new byte[2];
			StyleLabelDescription.attributeNamespaceIds = array;
		}

		// Token: 0x0400820E RID: 33294
		private const string tagName = "desc";

		// Token: 0x0400820F RID: 33295
		private const byte tagNsId = 14;

		// Token: 0x04008210 RID: 33296
		internal const int ElementTypeIdConst = 10685;

		// Token: 0x04008211 RID: 33297
		private static string[] attributeTagNames = new string[] { "lang", "val" };

		// Token: 0x04008212 RID: 33298
		private static byte[] attributeNamespaceIds;
	}
}
