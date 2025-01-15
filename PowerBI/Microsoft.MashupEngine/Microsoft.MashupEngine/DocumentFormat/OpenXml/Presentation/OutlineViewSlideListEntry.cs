using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A83 RID: 10883
	[GeneratedCode("DomGen", "2.0")]
	internal class OutlineViewSlideListEntry : OpenXmlLeafElement
	{
		// Token: 0x17007362 RID: 29538
		// (get) Token: 0x060160C2 RID: 90306 RVA: 0x0031F324 File Offset: 0x0031D524
		public override string LocalName
		{
			get
			{
				return "sld";
			}
		}

		// Token: 0x17007363 RID: 29539
		// (get) Token: 0x060160C3 RID: 90307 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007364 RID: 29540
		// (get) Token: 0x060160C4 RID: 90308 RVA: 0x0032602F File Offset: 0x0032422F
		internal override int ElementTypeId
		{
			get
			{
				return 12296;
			}
		}

		// Token: 0x060160C5 RID: 90309 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007365 RID: 29541
		// (get) Token: 0x060160C6 RID: 90310 RVA: 0x00326036 File Offset: 0x00324236
		internal override string[] AttributeTagNames
		{
			get
			{
				return OutlineViewSlideListEntry.attributeTagNames;
			}
		}

		// Token: 0x17007366 RID: 29542
		// (get) Token: 0x060160C7 RID: 90311 RVA: 0x0032603D File Offset: 0x0032423D
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return OutlineViewSlideListEntry.attributeNamespaceIds;
			}
		}

		// Token: 0x17007367 RID: 29543
		// (get) Token: 0x060160C8 RID: 90312 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x060160C9 RID: 90313 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17007368 RID: 29544
		// (get) Token: 0x060160CA RID: 90314 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x060160CB RID: 90315 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "collapse")]
		public BooleanValue Collapse
		{
			get
			{
				return (BooleanValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x060160CD RID: 90317 RVA: 0x00326044 File Offset: 0x00324244
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (19 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "collapse" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060160CE RID: 90318 RVA: 0x0032607C File Offset: 0x0032427C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<OutlineViewSlideListEntry>(deep);
		}

		// Token: 0x060160CF RID: 90319 RVA: 0x00326088 File Offset: 0x00324288
		// Note: this type is marked as 'beforefieldinit'.
		static OutlineViewSlideListEntry()
		{
			byte[] array = new byte[2];
			array[0] = 19;
			OutlineViewSlideListEntry.attributeNamespaceIds = array;
		}

		// Token: 0x040095F8 RID: 38392
		private const string tagName = "sld";

		// Token: 0x040095F9 RID: 38393
		private const byte tagNsId = 24;

		// Token: 0x040095FA RID: 38394
		internal const int ElementTypeIdConst = 12296;

		// Token: 0x040095FB RID: 38395
		private static string[] attributeTagNames = new string[] { "id", "collapse" };

		// Token: 0x040095FC RID: 38396
		private static byte[] attributeNamespaceIds;
	}
}
