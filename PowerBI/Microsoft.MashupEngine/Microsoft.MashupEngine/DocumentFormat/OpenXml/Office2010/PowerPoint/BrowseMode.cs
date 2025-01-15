using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.PowerPoint
{
	// Token: 0x020023AF RID: 9135
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class BrowseMode : OpenXmlLeafElement
	{
		// Token: 0x17004C4F RID: 19535
		// (get) Token: 0x060108A6 RID: 67750 RVA: 0x002E4863 File Offset: 0x002E2A63
		public override string LocalName
		{
			get
			{
				return "browseMode";
			}
		}

		// Token: 0x17004C50 RID: 19536
		// (get) Token: 0x060108A7 RID: 67751 RVA: 0x002E3C37 File Offset: 0x002E1E37
		internal override byte NamespaceId
		{
			get
			{
				return 49;
			}
		}

		// Token: 0x17004C51 RID: 19537
		// (get) Token: 0x060108A8 RID: 67752 RVA: 0x002E486A File Offset: 0x002E2A6A
		internal override int ElementTypeId
		{
			get
			{
				return 12790;
			}
		}

		// Token: 0x060108A9 RID: 67753 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004C52 RID: 19538
		// (get) Token: 0x060108AA RID: 67754 RVA: 0x002E4871 File Offset: 0x002E2A71
		internal override string[] AttributeTagNames
		{
			get
			{
				return BrowseMode.attributeTagNames;
			}
		}

		// Token: 0x17004C53 RID: 19539
		// (get) Token: 0x060108AB RID: 67755 RVA: 0x002E4878 File Offset: 0x002E2A78
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return BrowseMode.attributeNamespaceIds;
			}
		}

		// Token: 0x17004C54 RID: 19540
		// (get) Token: 0x060108AC RID: 67756 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x060108AD RID: 67757 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "showStatus")]
		public BooleanValue ShowStatus
		{
			get
			{
				return (BooleanValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x060108AF RID: 67759 RVA: 0x002E487F File Offset: 0x002E2A7F
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "showStatus" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060108B0 RID: 67760 RVA: 0x002E489F File Offset: 0x002E2A9F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BrowseMode>(deep);
		}

		// Token: 0x060108B1 RID: 67761 RVA: 0x002E48A8 File Offset: 0x002E2AA8
		// Note: this type is marked as 'beforefieldinit'.
		static BrowseMode()
		{
			byte[] array = new byte[1];
			BrowseMode.attributeNamespaceIds = array;
		}

		// Token: 0x04007527 RID: 29991
		private const string tagName = "browseMode";

		// Token: 0x04007528 RID: 29992
		private const byte tagNsId = 49;

		// Token: 0x04007529 RID: 29993
		internal const int ElementTypeIdConst = 12790;

		// Token: 0x0400752A RID: 29994
		private static string[] attributeTagNames = new string[] { "showStatus" };

		// Token: 0x0400752B RID: 29995
		private static byte[] attributeNamespaceIds;
	}
}
