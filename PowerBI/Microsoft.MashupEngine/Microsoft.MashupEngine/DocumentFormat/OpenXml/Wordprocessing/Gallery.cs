using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FC9 RID: 12233
	[GeneratedCode("DomGen", "2.0")]
	internal class Gallery : OpenXmlLeafElement
	{
		// Token: 0x17009413 RID: 37907
		// (get) Token: 0x0601A89F RID: 108703 RVA: 0x002C92B0 File Offset: 0x002C74B0
		public override string LocalName
		{
			get
			{
				return "gallery";
			}
		}

		// Token: 0x17009414 RID: 37908
		// (get) Token: 0x0601A8A0 RID: 108704 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17009415 RID: 37909
		// (get) Token: 0x0601A8A1 RID: 108705 RVA: 0x00363D48 File Offset: 0x00361F48
		internal override int ElementTypeId
		{
			get
			{
				return 11941;
			}
		}

		// Token: 0x0601A8A2 RID: 108706 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17009416 RID: 37910
		// (get) Token: 0x0601A8A3 RID: 108707 RVA: 0x00363D4F File Offset: 0x00361F4F
		internal override string[] AttributeTagNames
		{
			get
			{
				return Gallery.attributeTagNames;
			}
		}

		// Token: 0x17009417 RID: 37911
		// (get) Token: 0x0601A8A4 RID: 108708 RVA: 0x00363D56 File Offset: 0x00361F56
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Gallery.attributeNamespaceIds;
			}
		}

		// Token: 0x17009418 RID: 37912
		// (get) Token: 0x0601A8A5 RID: 108709 RVA: 0x00363D5D File Offset: 0x00361F5D
		// (set) Token: 0x0601A8A6 RID: 108710 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public EnumValue<DocPartGalleryValues> Val
		{
			get
			{
				return (EnumValue<DocPartGalleryValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0601A8A8 RID: 108712 RVA: 0x00363D6C File Offset: 0x00361F6C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new EnumValue<DocPartGalleryValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601A8A9 RID: 108713 RVA: 0x00363D8E File Offset: 0x00361F8E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Gallery>(deep);
		}

		// Token: 0x0400AD69 RID: 44393
		private const string tagName = "gallery";

		// Token: 0x0400AD6A RID: 44394
		private const byte tagNsId = 23;

		// Token: 0x0400AD6B RID: 44395
		internal const int ElementTypeIdConst = 11941;

		// Token: 0x0400AD6C RID: 44396
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400AD6D RID: 44397
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
