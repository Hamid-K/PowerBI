using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E97 RID: 11927
	[GeneratedCode("DomGen", "2.0")]
	internal class PhoneticGuideTextFontSize : HpsMeasureType
	{
		// Token: 0x17008B4B RID: 35659
		// (get) Token: 0x06019587 RID: 103815 RVA: 0x00348C23 File Offset: 0x00346E23
		public override string LocalName
		{
			get
			{
				return "hps";
			}
		}

		// Token: 0x17008B4C RID: 35660
		// (get) Token: 0x06019588 RID: 103816 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008B4D RID: 35661
		// (get) Token: 0x06019589 RID: 103817 RVA: 0x00348C2A File Offset: 0x00346E2A
		internal override int ElementTypeId
		{
			get
			{
				return 11753;
			}
		}

		// Token: 0x0601958A RID: 103818 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601958C RID: 103820 RVA: 0x00348C31 File Offset: 0x00346E31
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PhoneticGuideTextFontSize>(deep);
		}

		// Token: 0x0400A876 RID: 43126
		private const string tagName = "hps";

		// Token: 0x0400A877 RID: 43127
		private const byte tagNsId = 23;

		// Token: 0x0400A878 RID: 43128
		internal const int ElementTypeIdConst = 11753;
	}
}
