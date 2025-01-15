using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A54 RID: 10836
	[GeneratedCode("DomGen", "2.0")]
	internal class Font : OpenXmlLeafElement
	{
		// Token: 0x170071FC RID: 29180
		// (get) Token: 0x06015DAF RID: 89519 RVA: 0x002AD88F File Offset: 0x002ABA8F
		public override string LocalName
		{
			get
			{
				return "font";
			}
		}

		// Token: 0x170071FD RID: 29181
		// (get) Token: 0x06015DB0 RID: 89520 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x170071FE RID: 29182
		// (get) Token: 0x06015DB1 RID: 89521 RVA: 0x00323C17 File Offset: 0x00321E17
		internal override int ElementTypeId
		{
			get
			{
				return 12255;
			}
		}

		// Token: 0x06015DB2 RID: 89522 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170071FF RID: 29183
		// (get) Token: 0x06015DB3 RID: 89523 RVA: 0x00323C1E File Offset: 0x00321E1E
		internal override string[] AttributeTagNames
		{
			get
			{
				return Font.attributeTagNames;
			}
		}

		// Token: 0x17007200 RID: 29184
		// (get) Token: 0x06015DB4 RID: 89524 RVA: 0x00323C25 File Offset: 0x00321E25
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Font.attributeNamespaceIds;
			}
		}

		// Token: 0x17007201 RID: 29185
		// (get) Token: 0x06015DB5 RID: 89525 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06015DB6 RID: 89526 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "typeface")]
		public StringValue Typeface
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

		// Token: 0x17007202 RID: 29186
		// (get) Token: 0x06015DB7 RID: 89527 RVA: 0x002EB1A4 File Offset: 0x002E93A4
		// (set) Token: 0x06015DB8 RID: 89528 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "panose")]
		public HexBinaryValue Panose
		{
			get
			{
				return (HexBinaryValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17007203 RID: 29187
		// (get) Token: 0x06015DB9 RID: 89529 RVA: 0x00306450 File Offset: 0x00304650
		// (set) Token: 0x06015DBA RID: 89530 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "pitchFamily")]
		public SByteValue PitchFamily
		{
			get
			{
				return (SByteValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17007204 RID: 29188
		// (get) Token: 0x06015DBB RID: 89531 RVA: 0x0030645F File Offset: 0x0030465F
		// (set) Token: 0x06015DBC RID: 89532 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "charset")]
		public SByteValue CharacterSet
		{
			get
			{
				return (SByteValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x06015DBE RID: 89534 RVA: 0x00323C2C File Offset: 0x00321E2C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "typeface" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "panose" == name)
			{
				return new HexBinaryValue();
			}
			if (namespaceId == 0 && "pitchFamily" == name)
			{
				return new SByteValue();
			}
			if (namespaceId == 0 && "charset" == name)
			{
				return new SByteValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06015DBF RID: 89535 RVA: 0x00323C99 File Offset: 0x00321E99
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Font>(deep);
		}

		// Token: 0x06015DC0 RID: 89536 RVA: 0x00323CA4 File Offset: 0x00321EA4
		// Note: this type is marked as 'beforefieldinit'.
		static Font()
		{
			byte[] array = new byte[4];
			Font.attributeNamespaceIds = array;
		}

		// Token: 0x0400951F RID: 38175
		private const string tagName = "font";

		// Token: 0x04009520 RID: 38176
		private const byte tagNsId = 24;

		// Token: 0x04009521 RID: 38177
		internal const int ElementTypeIdConst = 12255;

		// Token: 0x04009522 RID: 38178
		private static string[] attributeTagNames = new string[] { "typeface", "panose", "pitchFamily", "charset" };

		// Token: 0x04009523 RID: 38179
		private static byte[] attributeNamespaceIds;
	}
}
