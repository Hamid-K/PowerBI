using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FEE RID: 12270
	[GeneratedCode("DomGen", "2.0")]
	internal class NoLineBreaksBeforeKinsoku : OpenXmlLeafElement
	{
		// Token: 0x17009531 RID: 38193
		// (get) Token: 0x0601AB01 RID: 109313 RVA: 0x00365EBD File Offset: 0x003640BD
		public override string LocalName
		{
			get
			{
				return "noLineBreaksBefore";
			}
		}

		// Token: 0x17009532 RID: 38194
		// (get) Token: 0x0601AB02 RID: 109314 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17009533 RID: 38195
		// (get) Token: 0x0601AB03 RID: 109315 RVA: 0x00365EC4 File Offset: 0x003640C4
		internal override int ElementTypeId
		{
			get
			{
				return 12022;
			}
		}

		// Token: 0x0601AB04 RID: 109316 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17009534 RID: 38196
		// (get) Token: 0x0601AB05 RID: 109317 RVA: 0x00365ECB File Offset: 0x003640CB
		internal override string[] AttributeTagNames
		{
			get
			{
				return NoLineBreaksBeforeKinsoku.attributeTagNames;
			}
		}

		// Token: 0x17009535 RID: 38197
		// (get) Token: 0x0601AB06 RID: 109318 RVA: 0x00365ED2 File Offset: 0x003640D2
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return NoLineBreaksBeforeKinsoku.attributeNamespaceIds;
			}
		}

		// Token: 0x17009536 RID: 38198
		// (get) Token: 0x0601AB07 RID: 109319 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601AB08 RID: 109320 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "lang")]
		public StringValue Language
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

		// Token: 0x17009537 RID: 38199
		// (get) Token: 0x0601AB09 RID: 109321 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0601AB0A RID: 109322 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "val")]
		public StringValue Val
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

		// Token: 0x0601AB0C RID: 109324 RVA: 0x00365E38 File Offset: 0x00364038
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "lang" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "val" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601AB0D RID: 109325 RVA: 0x00365ED9 File Offset: 0x003640D9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NoLineBreaksBeforeKinsoku>(deep);
		}

		// Token: 0x0400AE07 RID: 44551
		private const string tagName = "noLineBreaksBefore";

		// Token: 0x0400AE08 RID: 44552
		private const byte tagNsId = 23;

		// Token: 0x0400AE09 RID: 44553
		internal const int ElementTypeIdConst = 12022;

		// Token: 0x0400AE0A RID: 44554
		private static string[] attributeTagNames = new string[] { "lang", "val" };

		// Token: 0x0400AE0B RID: 44555
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23 };
	}
}
