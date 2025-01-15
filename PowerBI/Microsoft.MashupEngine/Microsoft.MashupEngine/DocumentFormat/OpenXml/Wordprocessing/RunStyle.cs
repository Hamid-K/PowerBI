using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E81 RID: 11905
	[GeneratedCode("DomGen", "2.0")]
	internal class RunStyle : String253Type
	{
		// Token: 0x17008AEC RID: 35564
		// (get) Token: 0x060194C8 RID: 103624 RVA: 0x00348630 File Offset: 0x00346830
		public override string LocalName
		{
			get
			{
				return "rStyle";
			}
		}

		// Token: 0x17008AED RID: 35565
		// (get) Token: 0x060194C9 RID: 103625 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008AEE RID: 35566
		// (get) Token: 0x060194CA RID: 103626 RVA: 0x00348637 File Offset: 0x00346837
		internal override int ElementTypeId
		{
			get
			{
				return 11575;
			}
		}

		// Token: 0x060194CB RID: 103627 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060194CD RID: 103629 RVA: 0x00348646 File Offset: 0x00346846
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RunStyle>(deep);
		}

		// Token: 0x0400A829 RID: 43049
		private const string tagName = "rStyle";

		// Token: 0x0400A82A RID: 43050
		private const byte tagNsId = 23;

		// Token: 0x0400A82B RID: 43051
		internal const int ElementTypeIdConst = 11575;
	}
}
