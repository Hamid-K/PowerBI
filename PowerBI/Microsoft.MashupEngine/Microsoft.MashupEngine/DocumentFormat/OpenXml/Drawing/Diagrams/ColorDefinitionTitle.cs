using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x0200265A RID: 9818
	[GeneratedCode("DomGen", "2.0")]
	internal class ColorDefinitionTitle : OpenXmlLeafElement
	{
		// Token: 0x17005B6C RID: 23404
		// (get) Token: 0x06012A8A RID: 76426 RVA: 0x002F2B3B File Offset: 0x002F0D3B
		public override string LocalName
		{
			get
			{
				return "title";
			}
		}

		// Token: 0x17005B6D RID: 23405
		// (get) Token: 0x06012A8B RID: 76427 RVA: 0x0006808E File Offset: 0x0006628E
		internal override byte NamespaceId
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17005B6E RID: 23406
		// (get) Token: 0x06012A8C RID: 76428 RVA: 0x002FDB00 File Offset: 0x002FBD00
		internal override int ElementTypeId
		{
			get
			{
				return 10635;
			}
		}

		// Token: 0x06012A8D RID: 76429 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005B6F RID: 23407
		// (get) Token: 0x06012A8E RID: 76430 RVA: 0x002FDB07 File Offset: 0x002FBD07
		internal override string[] AttributeTagNames
		{
			get
			{
				return ColorDefinitionTitle.attributeTagNames;
			}
		}

		// Token: 0x17005B70 RID: 23408
		// (get) Token: 0x06012A8F RID: 76431 RVA: 0x002FDB0E File Offset: 0x002FBD0E
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ColorDefinitionTitle.attributeNamespaceIds;
			}
		}

		// Token: 0x17005B71 RID: 23409
		// (get) Token: 0x06012A90 RID: 76432 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06012A91 RID: 76433 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17005B72 RID: 23410
		// (get) Token: 0x06012A92 RID: 76434 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06012A93 RID: 76435 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x06012A95 RID: 76437 RVA: 0x002FDB15 File Offset: 0x002FBD15
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

		// Token: 0x06012A96 RID: 76438 RVA: 0x002FDB4B File Offset: 0x002FBD4B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ColorDefinitionTitle>(deep);
		}

		// Token: 0x06012A97 RID: 76439 RVA: 0x002FDB54 File Offset: 0x002FBD54
		// Note: this type is marked as 'beforefieldinit'.
		static ColorDefinitionTitle()
		{
			byte[] array = new byte[2];
			ColorDefinitionTitle.attributeNamespaceIds = array;
		}

		// Token: 0x04008118 RID: 33048
		private const string tagName = "title";

		// Token: 0x04008119 RID: 33049
		private const byte tagNsId = 14;

		// Token: 0x0400811A RID: 33050
		internal const int ElementTypeIdConst = 10635;

		// Token: 0x0400811B RID: 33051
		private static string[] attributeTagNames = new string[] { "lang", "val" };

		// Token: 0x0400811C RID: 33052
		private static byte[] attributeNamespaceIds;
	}
}
