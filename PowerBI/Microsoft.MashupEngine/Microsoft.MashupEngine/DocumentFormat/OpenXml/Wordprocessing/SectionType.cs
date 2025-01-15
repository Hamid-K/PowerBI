using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E42 RID: 11842
	[GeneratedCode("DomGen", "2.0")]
	internal class SectionType : OpenXmlLeafElement
	{
		// Token: 0x170089CA RID: 35274
		// (get) Token: 0x06019263 RID: 103011 RVA: 0x0031CE60 File Offset: 0x0031B060
		public override string LocalName
		{
			get
			{
				return "type";
			}
		}

		// Token: 0x170089CB RID: 35275
		// (get) Token: 0x06019264 RID: 103012 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170089CC RID: 35276
		// (get) Token: 0x06019265 RID: 103013 RVA: 0x00346D2C File Offset: 0x00344F2C
		internal override int ElementTypeId
		{
			get
			{
				return 11528;
			}
		}

		// Token: 0x06019266 RID: 103014 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170089CD RID: 35277
		// (get) Token: 0x06019267 RID: 103015 RVA: 0x00346D33 File Offset: 0x00344F33
		internal override string[] AttributeTagNames
		{
			get
			{
				return SectionType.attributeTagNames;
			}
		}

		// Token: 0x170089CE RID: 35278
		// (get) Token: 0x06019268 RID: 103016 RVA: 0x00346D3A File Offset: 0x00344F3A
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SectionType.attributeNamespaceIds;
			}
		}

		// Token: 0x170089CF RID: 35279
		// (get) Token: 0x06019269 RID: 103017 RVA: 0x00346D41 File Offset: 0x00344F41
		// (set) Token: 0x0601926A RID: 103018 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public EnumValue<SectionMarkValues> Val
		{
			get
			{
				return (EnumValue<SectionMarkValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0601926C RID: 103020 RVA: 0x00346D50 File Offset: 0x00344F50
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new EnumValue<SectionMarkValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601926D RID: 103021 RVA: 0x00346D72 File Offset: 0x00344F72
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SectionType>(deep);
		}

		// Token: 0x0400A74B RID: 42827
		private const string tagName = "type";

		// Token: 0x0400A74C RID: 42828
		private const byte tagNsId = 23;

		// Token: 0x0400A74D RID: 42829
		internal const int ElementTypeIdConst = 11528;

		// Token: 0x0400A74E RID: 42830
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400A74F RID: 42831
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
