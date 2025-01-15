using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F66 RID: 12134
	[GeneratedCode("DomGen", "2.0")]
	internal class NumberingFormat : OpenXmlLeafElement
	{
		// Token: 0x170090BB RID: 37051
		// (get) Token: 0x0601A191 RID: 106897 RVA: 0x002F0D56 File Offset: 0x002EEF56
		public override string LocalName
		{
			get
			{
				return "numFmt";
			}
		}

		// Token: 0x170090BC RID: 37052
		// (get) Token: 0x0601A192 RID: 106898 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170090BD RID: 37053
		// (get) Token: 0x0601A193 RID: 106899 RVA: 0x0035D73C File Offset: 0x0035B93C
		internal override int ElementTypeId
		{
			get
			{
				return 11792;
			}
		}

		// Token: 0x0601A194 RID: 106900 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170090BE RID: 37054
		// (get) Token: 0x0601A195 RID: 106901 RVA: 0x0035D743 File Offset: 0x0035B943
		internal override string[] AttributeTagNames
		{
			get
			{
				return NumberingFormat.attributeTagNames;
			}
		}

		// Token: 0x170090BF RID: 37055
		// (get) Token: 0x0601A196 RID: 106902 RVA: 0x0035D74A File Offset: 0x0035B94A
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return NumberingFormat.attributeNamespaceIds;
			}
		}

		// Token: 0x170090C0 RID: 37056
		// (get) Token: 0x0601A197 RID: 106903 RVA: 0x00347388 File Offset: 0x00345588
		// (set) Token: 0x0601A198 RID: 106904 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public EnumValue<NumberFormatValues> Val
		{
			get
			{
				return (EnumValue<NumberFormatValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x170090C1 RID: 37057
		// (get) Token: 0x0601A199 RID: 106905 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0601A19A RID: 106906 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "format")]
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public StringValue Format
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

		// Token: 0x0601A19C RID: 106908 RVA: 0x0035D751 File Offset: 0x0035B951
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new EnumValue<NumberFormatValues>();
			}
			if (23 == namespaceId && "format" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601A19D RID: 106909 RVA: 0x0035D78B File Offset: 0x0035B98B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NumberingFormat>(deep);
		}

		// Token: 0x0400ABC1 RID: 43969
		private const string tagName = "numFmt";

		// Token: 0x0400ABC2 RID: 43970
		private const byte tagNsId = 23;

		// Token: 0x0400ABC3 RID: 43971
		internal const int ElementTypeIdConst = 11792;

		// Token: 0x0400ABC4 RID: 43972
		private static string[] attributeTagNames = new string[] { "val", "format" };

		// Token: 0x0400ABC5 RID: 43973
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23 };
	}
}
