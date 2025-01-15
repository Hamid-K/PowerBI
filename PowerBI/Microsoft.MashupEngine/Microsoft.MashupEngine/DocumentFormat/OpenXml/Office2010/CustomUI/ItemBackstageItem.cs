using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x020022F8 RID: 8952
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class ItemBackstageItem : BackstageItemType
	{
		// Token: 0x17004713 RID: 18195
		// (get) Token: 0x0600FD33 RID: 64819 RVA: 0x002AD56D File Offset: 0x002AB76D
		public override string LocalName
		{
			get
			{
				return "item";
			}
		}

		// Token: 0x17004714 RID: 18196
		// (get) Token: 0x0600FD34 RID: 64820 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x17004715 RID: 18197
		// (get) Token: 0x0600FD35 RID: 64821 RVA: 0x002DC1BF File Offset: 0x002DA3BF
		internal override int ElementTypeId
		{
			get
			{
				return 13095;
			}
		}

		// Token: 0x0600FD36 RID: 64822 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x0600FD38 RID: 64824 RVA: 0x002DC1CE File Offset: 0x002DA3CE
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ItemBackstageItem>(deep);
		}

		// Token: 0x040071F7 RID: 29175
		private const string tagName = "item";

		// Token: 0x040071F8 RID: 29176
		private const byte tagNsId = 57;

		// Token: 0x040071F9 RID: 29177
		internal const int ElementTypeIdConst = 13095;
	}
}
