using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.PowerPoint
{
	// Token: 0x020023BE RID: 9150
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class MediaFade : OpenXmlLeafElement
	{
		// Token: 0x17004CC0 RID: 19648
		// (get) Token: 0x06010994 RID: 67988 RVA: 0x002E532B File Offset: 0x002E352B
		public override string LocalName
		{
			get
			{
				return "fade";
			}
		}

		// Token: 0x17004CC1 RID: 19649
		// (get) Token: 0x06010995 RID: 67989 RVA: 0x002E3C37 File Offset: 0x002E1E37
		internal override byte NamespaceId
		{
			get
			{
				return 49;
			}
		}

		// Token: 0x17004CC2 RID: 19650
		// (get) Token: 0x06010996 RID: 67990 RVA: 0x002E5332 File Offset: 0x002E3532
		internal override int ElementTypeId
		{
			get
			{
				return 12804;
			}
		}

		// Token: 0x06010997 RID: 67991 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004CC3 RID: 19651
		// (get) Token: 0x06010998 RID: 67992 RVA: 0x002E5339 File Offset: 0x002E3539
		internal override string[] AttributeTagNames
		{
			get
			{
				return MediaFade.attributeTagNames;
			}
		}

		// Token: 0x17004CC4 RID: 19652
		// (get) Token: 0x06010999 RID: 67993 RVA: 0x002E5340 File Offset: 0x002E3540
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return MediaFade.attributeNamespaceIds;
			}
		}

		// Token: 0x17004CC5 RID: 19653
		// (get) Token: 0x0601099A RID: 67994 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601099B RID: 67995 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "in")]
		public StringValue InDuration
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

		// Token: 0x17004CC6 RID: 19654
		// (get) Token: 0x0601099C RID: 67996 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0601099D RID: 67997 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "out")]
		public StringValue OutDuration
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

		// Token: 0x0601099F RID: 67999 RVA: 0x002E5347 File Offset: 0x002E3547
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "in" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "out" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060109A0 RID: 68000 RVA: 0x002E537D File Offset: 0x002E357D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MediaFade>(deep);
		}

		// Token: 0x060109A1 RID: 68001 RVA: 0x002E5388 File Offset: 0x002E3588
		// Note: this type is marked as 'beforefieldinit'.
		static MediaFade()
		{
			byte[] array = new byte[2];
			MediaFade.attributeNamespaceIds = array;
		}

		// Token: 0x0400756F RID: 30063
		private const string tagName = "fade";

		// Token: 0x04007570 RID: 30064
		private const byte tagNsId = 49;

		// Token: 0x04007571 RID: 30065
		internal const int ElementTypeIdConst = 12804;

		// Token: 0x04007572 RID: 30066
		private static string[] attributeTagNames = new string[] { "in", "out" };

		// Token: 0x04007573 RID: 30067
		private static byte[] attributeNamespaceIds;
	}
}
