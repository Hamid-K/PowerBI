using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F65 RID: 12133
	[GeneratedCode("DomGen", "2.0")]
	internal class FootnotePosition : OpenXmlLeafElement
	{
		// Token: 0x170090B5 RID: 37045
		// (get) Token: 0x0601A185 RID: 106885 RVA: 0x0030BA47 File Offset: 0x00309C47
		public override string LocalName
		{
			get
			{
				return "pos";
			}
		}

		// Token: 0x170090B6 RID: 37046
		// (get) Token: 0x0601A186 RID: 106886 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170090B7 RID: 37047
		// (get) Token: 0x0601A187 RID: 106887 RVA: 0x0035D6B8 File Offset: 0x0035B8B8
		internal override int ElementTypeId
		{
			get
			{
				return 11791;
			}
		}

		// Token: 0x0601A188 RID: 106888 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170090B8 RID: 37048
		// (get) Token: 0x0601A189 RID: 106889 RVA: 0x0035D6BF File Offset: 0x0035B8BF
		internal override string[] AttributeTagNames
		{
			get
			{
				return FootnotePosition.attributeTagNames;
			}
		}

		// Token: 0x170090B9 RID: 37049
		// (get) Token: 0x0601A18A RID: 106890 RVA: 0x0035D6C6 File Offset: 0x0035B8C6
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return FootnotePosition.attributeNamespaceIds;
			}
		}

		// Token: 0x170090BA RID: 37050
		// (get) Token: 0x0601A18B RID: 106891 RVA: 0x0035D6CD File Offset: 0x0035B8CD
		// (set) Token: 0x0601A18C RID: 106892 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public EnumValue<FootnotePositionValues> Val
		{
			get
			{
				return (EnumValue<FootnotePositionValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0601A18E RID: 106894 RVA: 0x0035D6DC File Offset: 0x0035B8DC
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new EnumValue<FootnotePositionValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601A18F RID: 106895 RVA: 0x0035D6FE File Offset: 0x0035B8FE
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FootnotePosition>(deep);
		}

		// Token: 0x0400ABBC RID: 43964
		private const string tagName = "pos";

		// Token: 0x0400ABBD RID: 43965
		private const byte tagNsId = 23;

		// Token: 0x0400ABBE RID: 43966
		internal const int ElementTypeIdConst = 11791;

		// Token: 0x0400ABBF RID: 43967
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400ABC0 RID: 43968
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
