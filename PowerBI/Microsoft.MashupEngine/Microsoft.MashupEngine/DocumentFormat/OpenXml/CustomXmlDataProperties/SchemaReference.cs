using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.CustomXmlDataProperties
{
	// Token: 0x0200290B RID: 10507
	[GeneratedCode("DomGen", "2.0")]
	internal class SchemaReference : OpenXmlLeafElement
	{
		// Token: 0x17006A4A RID: 27210
		// (get) Token: 0x06014C67 RID: 85095 RVA: 0x00316F7F File Offset: 0x0031517F
		public override string LocalName
		{
			get
			{
				return "schemaRef";
			}
		}

		// Token: 0x17006A4B RID: 27211
		// (get) Token: 0x06014C68 RID: 85096 RVA: 0x002435AE File Offset: 0x002417AE
		internal override byte NamespaceId
		{
			get
			{
				return 20;
			}
		}

		// Token: 0x17006A4C RID: 27212
		// (get) Token: 0x06014C69 RID: 85097 RVA: 0x00316F86 File Offset: 0x00315186
		internal override int ElementTypeId
		{
			get
			{
				return 10835;
			}
		}

		// Token: 0x06014C6A RID: 85098 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17006A4D RID: 27213
		// (get) Token: 0x06014C6B RID: 85099 RVA: 0x00316F8D File Offset: 0x0031518D
		internal override string[] AttributeTagNames
		{
			get
			{
				return SchemaReference.attributeTagNames;
			}
		}

		// Token: 0x17006A4E RID: 27214
		// (get) Token: 0x06014C6C RID: 85100 RVA: 0x00316F94 File Offset: 0x00315194
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SchemaReference.attributeNamespaceIds;
			}
		}

		// Token: 0x17006A4F RID: 27215
		// (get) Token: 0x06014C6D RID: 85101 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06014C6E RID: 85102 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(20, "uri")]
		public StringValue Uri
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

		// Token: 0x06014C70 RID: 85104 RVA: 0x00316F9B File Offset: 0x0031519B
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (20 == namespaceId && "uri" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06014C71 RID: 85105 RVA: 0x00316FBD File Offset: 0x003151BD
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SchemaReference>(deep);
		}

		// Token: 0x04008FCB RID: 36811
		private const string tagName = "schemaRef";

		// Token: 0x04008FCC RID: 36812
		private const byte tagNsId = 20;

		// Token: 0x04008FCD RID: 36813
		internal const int ElementTypeIdConst = 10835;

		// Token: 0x04008FCE RID: 36814
		private static string[] attributeTagNames = new string[] { "uri" };

		// Token: 0x04008FCF RID: 36815
		private static byte[] attributeNamespaceIds = new byte[] { 20 };
	}
}
