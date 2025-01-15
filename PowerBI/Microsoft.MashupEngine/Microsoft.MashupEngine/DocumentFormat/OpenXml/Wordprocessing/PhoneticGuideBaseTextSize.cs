using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E98 RID: 11928
	[GeneratedCode("DomGen", "2.0")]
	internal class PhoneticGuideBaseTextSize : HpsMeasureType
	{
		// Token: 0x17008B4E RID: 35662
		// (get) Token: 0x0601958D RID: 103821 RVA: 0x00348C3A File Offset: 0x00346E3A
		public override string LocalName
		{
			get
			{
				return "hpsBaseText";
			}
		}

		// Token: 0x17008B4F RID: 35663
		// (get) Token: 0x0601958E RID: 103822 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008B50 RID: 35664
		// (get) Token: 0x0601958F RID: 103823 RVA: 0x00348C41 File Offset: 0x00346E41
		internal override int ElementTypeId
		{
			get
			{
				return 11755;
			}
		}

		// Token: 0x06019590 RID: 103824 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019592 RID: 103826 RVA: 0x00348C48 File Offset: 0x00346E48
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PhoneticGuideBaseTextSize>(deep);
		}

		// Token: 0x0400A879 RID: 43129
		private const string tagName = "hpsBaseText";

		// Token: 0x0400A87A RID: 43130
		private const byte tagNsId = 23;

		// Token: 0x0400A87B RID: 43131
		internal const int ElementTypeIdConst = 11755;
	}
}
