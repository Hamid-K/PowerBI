using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002EE0 RID: 12000
	[GeneratedCode("DomGen", "2.0")]
	internal class TopMargin : TableWidthType
	{
		// Token: 0x17008D41 RID: 36161
		// (get) Token: 0x060199CF RID: 104911 RVA: 0x002BF37F File Offset: 0x002BD57F
		public override string LocalName
		{
			get
			{
				return "top";
			}
		}

		// Token: 0x17008D42 RID: 36162
		// (get) Token: 0x060199D0 RID: 104912 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008D43 RID: 36163
		// (get) Token: 0x060199D1 RID: 104913 RVA: 0x00353460 File Offset: 0x00351660
		internal override int ElementTypeId
		{
			get
			{
				return 12125;
			}
		}

		// Token: 0x060199D2 RID: 104914 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060199D4 RID: 104916 RVA: 0x00353467 File Offset: 0x00351667
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TopMargin>(deep);
		}

		// Token: 0x0400A9A8 RID: 43432
		private const string tagName = "top";

		// Token: 0x0400A9A9 RID: 43433
		private const byte tagNsId = 23;

		// Token: 0x0400A9AA RID: 43434
		internal const int ElementTypeIdConst = 12125;
	}
}
