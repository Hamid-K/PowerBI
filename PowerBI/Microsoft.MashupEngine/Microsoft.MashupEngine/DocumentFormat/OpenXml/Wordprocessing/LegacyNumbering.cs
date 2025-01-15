using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F98 RID: 12184
	[GeneratedCode("DomGen", "2.0")]
	internal class LegacyNumbering : OpenXmlLeafElement
	{
		// Token: 0x170091FC RID: 37372
		// (get) Token: 0x0601A43B RID: 107579 RVA: 0x0035FB9D File Offset: 0x0035DD9D
		public override string LocalName
		{
			get
			{
				return "legacy";
			}
		}

		// Token: 0x170091FD RID: 37373
		// (get) Token: 0x0601A43C RID: 107580 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170091FE RID: 37374
		// (get) Token: 0x0601A43D RID: 107581 RVA: 0x0035FBA4 File Offset: 0x0035DDA4
		internal override int ElementTypeId
		{
			get
			{
				return 11870;
			}
		}

		// Token: 0x0601A43E RID: 107582 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170091FF RID: 37375
		// (get) Token: 0x0601A43F RID: 107583 RVA: 0x0035FBAB File Offset: 0x0035DDAB
		internal override string[] AttributeTagNames
		{
			get
			{
				return LegacyNumbering.attributeTagNames;
			}
		}

		// Token: 0x17009200 RID: 37376
		// (get) Token: 0x0601A440 RID: 107584 RVA: 0x0035FBB2 File Offset: 0x0035DDB2
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return LegacyNumbering.attributeNamespaceIds;
			}
		}

		// Token: 0x17009201 RID: 37377
		// (get) Token: 0x0601A441 RID: 107585 RVA: 0x002EBFC4 File Offset: 0x002EA1C4
		// (set) Token: 0x0601A442 RID: 107586 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "legacy")]
		public OnOffValue Legacy
		{
			get
			{
				return (OnOffValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17009202 RID: 37378
		// (get) Token: 0x0601A443 RID: 107587 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0601A444 RID: 107588 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "legacySpace")]
		public StringValue LegacySpace
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

		// Token: 0x17009203 RID: 37379
		// (get) Token: 0x0601A445 RID: 107589 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0601A446 RID: 107590 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(23, "legacyIndent")]
		public StringValue LegacyIndent
		{
			get
			{
				return (StringValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x0601A448 RID: 107592 RVA: 0x0035FBBC File Offset: 0x0035DDBC
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "legacy" == name)
			{
				return new OnOffValue();
			}
			if (23 == namespaceId && "legacySpace" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "legacyIndent" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601A449 RID: 107593 RVA: 0x0035FC19 File Offset: 0x0035DE19
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LegacyNumbering>(deep);
		}

		// Token: 0x0400AC81 RID: 44161
		private const string tagName = "legacy";

		// Token: 0x0400AC82 RID: 44162
		private const byte tagNsId = 23;

		// Token: 0x0400AC83 RID: 44163
		internal const int ElementTypeIdConst = 11870;

		// Token: 0x0400AC84 RID: 44164
		private static string[] attributeTagNames = new string[] { "legacy", "legacySpace", "legacyIndent" };

		// Token: 0x0400AC85 RID: 44165
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23, 23 };
	}
}
