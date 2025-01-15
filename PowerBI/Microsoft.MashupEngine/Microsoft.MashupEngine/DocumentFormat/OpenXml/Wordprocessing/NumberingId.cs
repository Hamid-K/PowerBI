using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F29 RID: 12073
	[GeneratedCode("DomGen", "2.0")]
	internal class NumberingId : NonNegativeDecimalNumberType
	{
		// Token: 0x17008F3D RID: 36669
		// (get) Token: 0x06019E40 RID: 106048 RVA: 0x00358FF4 File Offset: 0x003571F4
		public override string LocalName
		{
			get
			{
				return "numId";
			}
		}

		// Token: 0x17008F3E RID: 36670
		// (get) Token: 0x06019E41 RID: 106049 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008F3F RID: 36671
		// (get) Token: 0x06019E42 RID: 106050 RVA: 0x00358FFB File Offset: 0x003571FB
		internal override int ElementTypeId
		{
			get
			{
				return 11713;
			}
		}

		// Token: 0x06019E43 RID: 106051 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019E45 RID: 106053 RVA: 0x0035900A File Offset: 0x0035720A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NumberingId>(deep);
		}

		// Token: 0x0400AABD RID: 43709
		private const string tagName = "numId";

		// Token: 0x0400AABE RID: 43710
		private const byte tagNsId = 23;

		// Token: 0x0400AABF RID: 43711
		internal const int ElementTypeIdConst = 11713;
	}
}
