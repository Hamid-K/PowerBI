using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200276F RID: 10095
	[GeneratedCode("DomGen", "2.0")]
	internal class EndTime : AudioCDTimeType
	{
		// Token: 0x17006141 RID: 24897
		// (get) Token: 0x06013777 RID: 79735 RVA: 0x0030761A File Offset: 0x0030581A
		public override string LocalName
		{
			get
			{
				return "end";
			}
		}

		// Token: 0x17006142 RID: 24898
		// (get) Token: 0x06013778 RID: 79736 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006143 RID: 24899
		// (get) Token: 0x06013779 RID: 79737 RVA: 0x00307621 File Offset: 0x00305821
		internal override int ElementTypeId
		{
			get
			{
				return 10129;
			}
		}

		// Token: 0x0601377A RID: 79738 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601377C RID: 79740 RVA: 0x00307628 File Offset: 0x00305828
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<EndTime>(deep);
		}

		// Token: 0x04008657 RID: 34391
		private const string tagName = "end";

		// Token: 0x04008658 RID: 34392
		private const byte tagNsId = 10;

		// Token: 0x04008659 RID: 34393
		internal const int ElementTypeIdConst = 10129;
	}
}
