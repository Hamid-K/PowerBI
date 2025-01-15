using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002EE3 RID: 12003
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class EndMargin : TableWidthType
	{
		// Token: 0x17008D4A RID: 36170
		// (get) Token: 0x060199E1 RID: 104929 RVA: 0x0030761A File Offset: 0x0030581A
		public override string LocalName
		{
			get
			{
				return "end";
			}
		}

		// Token: 0x17008D4B RID: 36171
		// (get) Token: 0x060199E2 RID: 104930 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008D4C RID: 36172
		// (get) Token: 0x060199E3 RID: 104931 RVA: 0x00353490 File Offset: 0x00351690
		internal override int ElementTypeId
		{
			get
			{
				return 12130;
			}
		}

		// Token: 0x060199E4 RID: 104932 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x060199E6 RID: 104934 RVA: 0x00353497 File Offset: 0x00351697
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<EndMargin>(deep);
		}

		// Token: 0x0400A9B1 RID: 43441
		private const string tagName = "end";

		// Token: 0x0400A9B2 RID: 43442
		private const byte tagNsId = 23;

		// Token: 0x0400A9B3 RID: 43443
		internal const int ElementTypeIdConst = 12130;
	}
}
