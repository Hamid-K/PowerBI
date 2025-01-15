using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002EC7 RID: 11975
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class ContentPart : OpenXmlLeafElement
	{
		// Token: 0x17008C81 RID: 35969
		// (get) Token: 0x0601982C RID: 104492 RVA: 0x002DF99D File Offset: 0x002DDB9D
		public override string LocalName
		{
			get
			{
				return "contentPart";
			}
		}

		// Token: 0x17008C82 RID: 35970
		// (get) Token: 0x0601982D RID: 104493 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008C83 RID: 35971
		// (get) Token: 0x0601982E RID: 104494 RVA: 0x0034CDE7 File Offset: 0x0034AFE7
		internal override int ElementTypeId
		{
			get
			{
				return 11630;
			}
		}

		// Token: 0x0601982F RID: 104495 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17008C84 RID: 35972
		// (get) Token: 0x06019830 RID: 104496 RVA: 0x0034CDEE File Offset: 0x0034AFEE
		internal override string[] AttributeTagNames
		{
			get
			{
				return ContentPart.attributeTagNames;
			}
		}

		// Token: 0x17008C85 RID: 35973
		// (get) Token: 0x06019831 RID: 104497 RVA: 0x0034CDF5 File Offset: 0x0034AFF5
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ContentPart.attributeNamespaceIds;
			}
		}

		// Token: 0x17008C86 RID: 35974
		// (get) Token: 0x06019832 RID: 104498 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06019833 RID: 104499 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(19, "id")]
		public StringValue Id
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

		// Token: 0x06019835 RID: 104501 RVA: 0x002D0AD5 File Offset: 0x002CECD5
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (19 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06019836 RID: 104502 RVA: 0x0034CDFC File Offset: 0x0034AFFC
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ContentPart>(deep);
		}

		// Token: 0x0400A92A RID: 43306
		private const string tagName = "contentPart";

		// Token: 0x0400A92B RID: 43307
		private const byte tagNsId = 23;

		// Token: 0x0400A92C RID: 43308
		internal const int ElementTypeIdConst = 11630;

		// Token: 0x0400A92D RID: 43309
		private static string[] attributeTagNames = new string[] { "id" };

		// Token: 0x0400A92E RID: 43310
		private static byte[] attributeNamespaceIds = new byte[] { 19 };
	}
}
