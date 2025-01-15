using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F97 RID: 12183
	[GeneratedCode("DomGen", "2.0")]
	internal class LevelText : OpenXmlLeafElement
	{
		// Token: 0x170091F5 RID: 37365
		// (get) Token: 0x0601A42D RID: 107565 RVA: 0x0035FAFC File Offset: 0x0035DCFC
		public override string LocalName
		{
			get
			{
				return "lvlText";
			}
		}

		// Token: 0x170091F6 RID: 37366
		// (get) Token: 0x0601A42E RID: 107566 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170091F7 RID: 37367
		// (get) Token: 0x0601A42F RID: 107567 RVA: 0x0035FB03 File Offset: 0x0035DD03
		internal override int ElementTypeId
		{
			get
			{
				return 11868;
			}
		}

		// Token: 0x0601A430 RID: 107568 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170091F8 RID: 37368
		// (get) Token: 0x0601A431 RID: 107569 RVA: 0x0035FB0A File Offset: 0x0035DD0A
		internal override string[] AttributeTagNames
		{
			get
			{
				return LevelText.attributeTagNames;
			}
		}

		// Token: 0x170091F9 RID: 37369
		// (get) Token: 0x0601A432 RID: 107570 RVA: 0x0035FB11 File Offset: 0x0035DD11
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return LevelText.attributeNamespaceIds;
			}
		}

		// Token: 0x170091FA RID: 37370
		// (get) Token: 0x0601A433 RID: 107571 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601A434 RID: 107572 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public StringValue Val
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

		// Token: 0x170091FB RID: 37371
		// (get) Token: 0x0601A435 RID: 107573 RVA: 0x003480EF File Offset: 0x003462EF
		// (set) Token: 0x0601A436 RID: 107574 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "null")]
		public OnOffValue Null
		{
			get
			{
				return (OnOffValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x0601A438 RID: 107576 RVA: 0x0035FB18 File Offset: 0x0035DD18
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "null" == name)
			{
				return new OnOffValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601A439 RID: 107577 RVA: 0x0035FB52 File Offset: 0x0035DD52
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LevelText>(deep);
		}

		// Token: 0x0400AC7C RID: 44156
		private const string tagName = "lvlText";

		// Token: 0x0400AC7D RID: 44157
		private const byte tagNsId = 23;

		// Token: 0x0400AC7E RID: 44158
		internal const int ElementTypeIdConst = 11868;

		// Token: 0x0400AC7F RID: 44159
		private static string[] attributeTagNames = new string[] { "val", "null" };

		// Token: 0x0400AC80 RID: 44160
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23 };
	}
}
