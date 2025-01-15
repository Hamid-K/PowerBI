using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x020024CF RID: 9423
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class StyleSet : OpenXmlLeafElement
	{
		// Token: 0x17005303 RID: 21251
		// (get) Token: 0x060117BA RID: 71610 RVA: 0x002EEE1E File Offset: 0x002ED01E
		public override string LocalName
		{
			get
			{
				return "styleSet";
			}
		}

		// Token: 0x17005304 RID: 21252
		// (get) Token: 0x060117BB RID: 71611 RVA: 0x002EC7B7 File Offset: 0x002EA9B7
		internal override byte NamespaceId
		{
			get
			{
				return 52;
			}
		}

		// Token: 0x17005305 RID: 21253
		// (get) Token: 0x060117BC RID: 71612 RVA: 0x002EEE25 File Offset: 0x002ED025
		internal override int ElementTypeId
		{
			get
			{
				return 12893;
			}
		}

		// Token: 0x060117BD RID: 71613 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17005306 RID: 21254
		// (get) Token: 0x060117BE RID: 71614 RVA: 0x002EEE2C File Offset: 0x002ED02C
		internal override string[] AttributeTagNames
		{
			get
			{
				return StyleSet.attributeTagNames;
			}
		}

		// Token: 0x17005307 RID: 21255
		// (get) Token: 0x060117BF RID: 71615 RVA: 0x002EEE33 File Offset: 0x002ED033
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return StyleSet.attributeNamespaceIds;
			}
		}

		// Token: 0x17005308 RID: 21256
		// (get) Token: 0x060117C0 RID: 71616 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x060117C1 RID: 71617 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(52, "id")]
		public UInt32Value Id
		{
			get
			{
				return (UInt32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17005309 RID: 21257
		// (get) Token: 0x060117C2 RID: 71618 RVA: 0x002ECE1C File Offset: 0x002EB01C
		// (set) Token: 0x060117C3 RID: 71619 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(52, "val")]
		public EnumValue<OnOffValues> Val
		{
			get
			{
				return (EnumValue<OnOffValues>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x060117C5 RID: 71621 RVA: 0x002EEE3A File Offset: 0x002ED03A
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (52 == namespaceId && "id" == name)
			{
				return new UInt32Value();
			}
			if (52 == namespaceId && "val" == name)
			{
				return new EnumValue<OnOffValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060117C6 RID: 71622 RVA: 0x002EEE74 File Offset: 0x002ED074
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<StyleSet>(deep);
		}

		// Token: 0x04007A0B RID: 31243
		private const string tagName = "styleSet";

		// Token: 0x04007A0C RID: 31244
		private const byte tagNsId = 52;

		// Token: 0x04007A0D RID: 31245
		internal const int ElementTypeIdConst = 12893;

		// Token: 0x04007A0E RID: 31246
		private static string[] attributeTagNames = new string[] { "id", "val" };

		// Token: 0x04007A0F RID: 31247
		private static byte[] attributeNamespaceIds = new byte[] { 52, 52 };
	}
}
