using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x02002445 RID: 9285
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class TabularSlicerCacheItem : OpenXmlLeafElement
	{
		// Token: 0x17005091 RID: 20625
		// (get) Token: 0x06011230 RID: 70192 RVA: 0x002EAA6B File Offset: 0x002E8C6B
		public override string LocalName
		{
			get
			{
				return "i";
			}
		}

		// Token: 0x17005092 RID: 20626
		// (get) Token: 0x06011231 RID: 70193 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17005093 RID: 20627
		// (get) Token: 0x06011232 RID: 70194 RVA: 0x002EAF13 File Offset: 0x002E9113
		internal override int ElementTypeId
		{
			get
			{
				return 13009;
			}
		}

		// Token: 0x06011233 RID: 70195 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17005094 RID: 20628
		// (get) Token: 0x06011234 RID: 70196 RVA: 0x002EAF1A File Offset: 0x002E911A
		internal override string[] AttributeTagNames
		{
			get
			{
				return TabularSlicerCacheItem.attributeTagNames;
			}
		}

		// Token: 0x17005095 RID: 20629
		// (get) Token: 0x06011235 RID: 70197 RVA: 0x002EAF21 File Offset: 0x002E9121
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TabularSlicerCacheItem.attributeNamespaceIds;
			}
		}

		// Token: 0x17005096 RID: 20630
		// (get) Token: 0x06011236 RID: 70198 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06011237 RID: 70199 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "x")]
		public UInt32Value Atom
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

		// Token: 0x17005097 RID: 20631
		// (get) Token: 0x06011238 RID: 70200 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x06011239 RID: 70201 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "s")]
		public BooleanValue IsSelected
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

		// Token: 0x17005098 RID: 20632
		// (get) Token: 0x0601123A RID: 70202 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x0601123B RID: 70203 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "nd")]
		public BooleanValue NonDisplay
		{
			get
			{
				return (BooleanValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x0601123D RID: 70205 RVA: 0x002EAF28 File Offset: 0x002E9128
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "x" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "s" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "nd" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601123E RID: 70206 RVA: 0x002EAF7F File Offset: 0x002E917F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TabularSlicerCacheItem>(deep);
		}

		// Token: 0x0601123F RID: 70207 RVA: 0x002EAF88 File Offset: 0x002E9188
		// Note: this type is marked as 'beforefieldinit'.
		static TabularSlicerCacheItem()
		{
			byte[] array = new byte[3];
			TabularSlicerCacheItem.attributeNamespaceIds = array;
		}

		// Token: 0x040077D2 RID: 30674
		private const string tagName = "i";

		// Token: 0x040077D3 RID: 30675
		private const byte tagNsId = 53;

		// Token: 0x040077D4 RID: 30676
		internal const int ElementTypeIdConst = 13009;

		// Token: 0x040077D5 RID: 30677
		private static string[] attributeTagNames = new string[] { "x", "s", "nd" };

		// Token: 0x040077D6 RID: 30678
		private static byte[] attributeNamespaceIds;
	}
}
