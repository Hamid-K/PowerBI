using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x02002434 RID: 9268
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class ListItem : OpenXmlLeafElement
	{
		// Token: 0x1700500B RID: 20491
		// (get) Token: 0x060110FA RID: 69882 RVA: 0x002AD56D File Offset: 0x002AB76D
		public override string LocalName
		{
			get
			{
				return "item";
			}
		}

		// Token: 0x1700500C RID: 20492
		// (get) Token: 0x060110FB RID: 69883 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x1700500D RID: 20493
		// (get) Token: 0x060110FC RID: 69884 RVA: 0x002EA34B File Offset: 0x002E854B
		internal override int ElementTypeId
		{
			get
			{
				return 12992;
			}
		}

		// Token: 0x060110FD RID: 69885 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x1700500E RID: 20494
		// (get) Token: 0x060110FE RID: 69886 RVA: 0x002EA352 File Offset: 0x002E8552
		internal override string[] AttributeTagNames
		{
			get
			{
				return ListItem.attributeTagNames;
			}
		}

		// Token: 0x1700500F RID: 20495
		// (get) Token: 0x060110FF RID: 69887 RVA: 0x002EA359 File Offset: 0x002E8559
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ListItem.attributeNamespaceIds;
			}
		}

		// Token: 0x17005010 RID: 20496
		// (get) Token: 0x06011100 RID: 69888 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06011101 RID: 69889 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
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

		// Token: 0x06011103 RID: 69891 RVA: 0x002E6B2F File Offset: 0x002E4D2F
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06011104 RID: 69892 RVA: 0x002EA360 File Offset: 0x002E8560
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ListItem>(deep);
		}

		// Token: 0x06011105 RID: 69893 RVA: 0x002EA36C File Offset: 0x002E856C
		// Note: this type is marked as 'beforefieldinit'.
		static ListItem()
		{
			byte[] array = new byte[1];
			ListItem.attributeNamespaceIds = array;
		}

		// Token: 0x0400777B RID: 30587
		private const string tagName = "item";

		// Token: 0x0400777C RID: 30588
		private const byte tagNsId = 53;

		// Token: 0x0400777D RID: 30589
		internal const int ElementTypeIdConst = 12992;

		// Token: 0x0400777E RID: 30590
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400777F RID: 30591
		private static byte[] attributeNamespaceIds;
	}
}
