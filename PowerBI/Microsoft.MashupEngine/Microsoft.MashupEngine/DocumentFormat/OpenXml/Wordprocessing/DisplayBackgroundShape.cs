using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DB0 RID: 11696
	[GeneratedCode("DomGen", "2.0")]
	internal class DisplayBackgroundShape : OnOffType
	{
		// Token: 0x170087AC RID: 34732
		// (get) Token: 0x06018E18 RID: 101912 RVA: 0x00344FDD File Offset: 0x003431DD
		public override string LocalName
		{
			get
			{
				return "displayBackgroundShape";
			}
		}

		// Token: 0x170087AD RID: 34733
		// (get) Token: 0x06018E19 RID: 101913 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170087AE RID: 34734
		// (get) Token: 0x06018E1A RID: 101914 RVA: 0x00344FE4 File Offset: 0x003431E4
		internal override int ElementTypeId
		{
			get
			{
				return 11964;
			}
		}

		// Token: 0x06018E1B RID: 101915 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018E1D RID: 101917 RVA: 0x00344FEB File Offset: 0x003431EB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DisplayBackgroundShape>(deep);
		}

		// Token: 0x0400A57B RID: 42363
		private const string tagName = "displayBackgroundShape";

		// Token: 0x0400A57C RID: 42364
		private const byte tagNsId = 23;

		// Token: 0x0400A57D RID: 42365
		internal const int ElementTypeIdConst = 11964;
	}
}
