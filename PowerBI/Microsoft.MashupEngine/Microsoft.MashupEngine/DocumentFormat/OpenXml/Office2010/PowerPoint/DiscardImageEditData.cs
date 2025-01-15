using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.PowerPoint
{
	// Token: 0x020023B2 RID: 9138
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class DiscardImageEditData : OpenXmlLeafElement
	{
		// Token: 0x17004C67 RID: 19559
		// (get) Token: 0x060108D8 RID: 67800 RVA: 0x002E4AE7 File Offset: 0x002E2CE7
		public override string LocalName
		{
			get
			{
				return "discardImageEditData";
			}
		}

		// Token: 0x17004C68 RID: 19560
		// (get) Token: 0x060108D9 RID: 67801 RVA: 0x002E3C37 File Offset: 0x002E1E37
		internal override byte NamespaceId
		{
			get
			{
				return 49;
			}
		}

		// Token: 0x17004C69 RID: 19561
		// (get) Token: 0x060108DA RID: 67802 RVA: 0x002E4AEE File Offset: 0x002E2CEE
		internal override int ElementTypeId
		{
			get
			{
				return 12793;
			}
		}

		// Token: 0x060108DB RID: 67803 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004C6A RID: 19562
		// (get) Token: 0x060108DC RID: 67804 RVA: 0x002E4AF5 File Offset: 0x002E2CF5
		internal override string[] AttributeTagNames
		{
			get
			{
				return DiscardImageEditData.attributeTagNames;
			}
		}

		// Token: 0x17004C6B RID: 19563
		// (get) Token: 0x060108DD RID: 67805 RVA: 0x002E4AFC File Offset: 0x002E2CFC
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return DiscardImageEditData.attributeNamespaceIds;
			}
		}

		// Token: 0x17004C6C RID: 19564
		// (get) Token: 0x060108DE RID: 67806 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x060108DF RID: 67807 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public BooleanValue Val
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

		// Token: 0x060108E1 RID: 67809 RVA: 0x002DE6BC File Offset: 0x002DC8BC
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060108E2 RID: 67810 RVA: 0x002E4B03 File Offset: 0x002E2D03
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DiscardImageEditData>(deep);
		}

		// Token: 0x060108E3 RID: 67811 RVA: 0x002E4B0C File Offset: 0x002E2D0C
		// Note: this type is marked as 'beforefieldinit'.
		static DiscardImageEditData()
		{
			byte[] array = new byte[1];
			DiscardImageEditData.attributeNamespaceIds = array;
		}

		// Token: 0x04007536 RID: 30006
		private const string tagName = "discardImageEditData";

		// Token: 0x04007537 RID: 30007
		private const byte tagNsId = 49;

		// Token: 0x04007538 RID: 30008
		internal const int ElementTypeIdConst = 12793;

		// Token: 0x04007539 RID: 30009
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400753A RID: 30010
		private static byte[] attributeNamespaceIds;
	}
}
