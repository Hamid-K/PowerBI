using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x0200265B RID: 9819
	[GeneratedCode("DomGen", "2.0")]
	internal class ColorTransformDescription : OpenXmlLeafElement
	{
		// Token: 0x17005B73 RID: 23411
		// (get) Token: 0x06012A98 RID: 76440 RVA: 0x002FDB8B File Offset: 0x002FBD8B
		public override string LocalName
		{
			get
			{
				return "desc";
			}
		}

		// Token: 0x17005B74 RID: 23412
		// (get) Token: 0x06012A99 RID: 76441 RVA: 0x0006808E File Offset: 0x0006628E
		internal override byte NamespaceId
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17005B75 RID: 23413
		// (get) Token: 0x06012A9A RID: 76442 RVA: 0x002FDB92 File Offset: 0x002FBD92
		internal override int ElementTypeId
		{
			get
			{
				return 10636;
			}
		}

		// Token: 0x06012A9B RID: 76443 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005B76 RID: 23414
		// (get) Token: 0x06012A9C RID: 76444 RVA: 0x002FDB99 File Offset: 0x002FBD99
		internal override string[] AttributeTagNames
		{
			get
			{
				return ColorTransformDescription.attributeTagNames;
			}
		}

		// Token: 0x17005B77 RID: 23415
		// (get) Token: 0x06012A9D RID: 76445 RVA: 0x002FDBA0 File Offset: 0x002FBDA0
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ColorTransformDescription.attributeNamespaceIds;
			}
		}

		// Token: 0x17005B78 RID: 23416
		// (get) Token: 0x06012A9E RID: 76446 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06012A9F RID: 76447 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17005B79 RID: 23417
		// (get) Token: 0x06012AA0 RID: 76448 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06012AA1 RID: 76449 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x06012AA3 RID: 76451 RVA: 0x002FDB15 File Offset: 0x002FBD15
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

		// Token: 0x06012AA4 RID: 76452 RVA: 0x002FDBA7 File Offset: 0x002FBDA7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ColorTransformDescription>(deep);
		}

		// Token: 0x06012AA5 RID: 76453 RVA: 0x002FDBB0 File Offset: 0x002FBDB0
		// Note: this type is marked as 'beforefieldinit'.
		static ColorTransformDescription()
		{
			byte[] array = new byte[2];
			ColorTransformDescription.attributeNamespaceIds = array;
		}

		// Token: 0x0400811D RID: 33053
		private const string tagName = "desc";

		// Token: 0x0400811E RID: 33054
		private const byte tagNsId = 14;

		// Token: 0x0400811F RID: 33055
		internal const int ElementTypeIdConst = 10636;

		// Token: 0x04008120 RID: 33056
		private static string[] attributeTagNames = new string[] { "lang", "val" };

		// Token: 0x04008121 RID: 33057
		private static byte[] attributeNamespaceIds;
	}
}
