using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F4F RID: 12111
	[GeneratedCode("DomGen", "2.0")]
	internal class SdtDateMappingType : OpenXmlLeafElement
	{
		// Token: 0x17009013 RID: 36883
		// (get) Token: 0x0601A013 RID: 106515 RVA: 0x0035B107 File Offset: 0x00359307
		public override string LocalName
		{
			get
			{
				return "storeMappedDataAs";
			}
		}

		// Token: 0x17009014 RID: 36884
		// (get) Token: 0x0601A014 RID: 106516 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17009015 RID: 36885
		// (get) Token: 0x0601A015 RID: 106517 RVA: 0x0035B10E File Offset: 0x0035930E
		internal override int ElementTypeId
		{
			get
			{
				return 11762;
			}
		}

		// Token: 0x0601A016 RID: 106518 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17009016 RID: 36886
		// (get) Token: 0x0601A017 RID: 106519 RVA: 0x0035B115 File Offset: 0x00359315
		internal override string[] AttributeTagNames
		{
			get
			{
				return SdtDateMappingType.attributeTagNames;
			}
		}

		// Token: 0x17009017 RID: 36887
		// (get) Token: 0x0601A018 RID: 106520 RVA: 0x0035B11C File Offset: 0x0035931C
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SdtDateMappingType.attributeNamespaceIds;
			}
		}

		// Token: 0x17009018 RID: 36888
		// (get) Token: 0x0601A019 RID: 106521 RVA: 0x0035B123 File Offset: 0x00359323
		// (set) Token: 0x0601A01A RID: 106522 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public EnumValue<DateFormatValues> Val
		{
			get
			{
				return (EnumValue<DateFormatValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0601A01C RID: 106524 RVA: 0x0035B132 File Offset: 0x00359332
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new EnumValue<DateFormatValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601A01D RID: 106525 RVA: 0x0035B154 File Offset: 0x00359354
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SdtDateMappingType>(deep);
		}

		// Token: 0x0400AB58 RID: 43864
		private const string tagName = "storeMappedDataAs";

		// Token: 0x0400AB59 RID: 43865
		private const byte tagNsId = 23;

		// Token: 0x0400AB5A RID: 43866
		internal const int ElementTypeIdConst = 11762;

		// Token: 0x0400AB5B RID: 43867
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400AB5C RID: 43868
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
