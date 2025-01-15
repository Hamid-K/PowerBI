using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x020022F9 RID: 8953
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class RadioButtonBackstageItem : BackstageItemType
	{
		// Token: 0x17004716 RID: 18198
		// (get) Token: 0x0600FD39 RID: 64825 RVA: 0x002DC1D7 File Offset: 0x002DA3D7
		public override string LocalName
		{
			get
			{
				return "radioButton";
			}
		}

		// Token: 0x17004717 RID: 18199
		// (get) Token: 0x0600FD3A RID: 64826 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x17004718 RID: 18200
		// (get) Token: 0x0600FD3B RID: 64827 RVA: 0x002DC1DE File Offset: 0x002DA3DE
		internal override int ElementTypeId
		{
			get
			{
				return 13096;
			}
		}

		// Token: 0x0600FD3C RID: 64828 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x0600FD3E RID: 64830 RVA: 0x002DC1E5 File Offset: 0x002DA3E5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RadioButtonBackstageItem>(deep);
		}

		// Token: 0x040071FA RID: 29178
		private const string tagName = "radioButton";

		// Token: 0x040071FB RID: 29179
		private const byte tagNsId = 57;

		// Token: 0x040071FC RID: 29180
		internal const int ElementTypeIdConst = 13096;
	}
}
