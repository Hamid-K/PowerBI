using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002EE1 RID: 12001
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class StartMargin : TableWidthType
	{
		// Token: 0x17008D44 RID: 36164
		// (get) Token: 0x060199D5 RID: 104917 RVA: 0x00313F27 File Offset: 0x00312127
		public override string LocalName
		{
			get
			{
				return "start";
			}
		}

		// Token: 0x17008D45 RID: 36165
		// (get) Token: 0x060199D6 RID: 104918 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008D46 RID: 36166
		// (get) Token: 0x060199D7 RID: 104919 RVA: 0x00353470 File Offset: 0x00351670
		internal override int ElementTypeId
		{
			get
			{
				return 12127;
			}
		}

		// Token: 0x060199D8 RID: 104920 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x060199DA RID: 104922 RVA: 0x00353477 File Offset: 0x00351677
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<StartMargin>(deep);
		}

		// Token: 0x0400A9AB RID: 43435
		private const string tagName = "start";

		// Token: 0x0400A9AC RID: 43436
		private const byte tagNsId = 23;

		// Token: 0x0400A9AD RID: 43437
		internal const int ElementTypeIdConst = 12127;
	}
}
