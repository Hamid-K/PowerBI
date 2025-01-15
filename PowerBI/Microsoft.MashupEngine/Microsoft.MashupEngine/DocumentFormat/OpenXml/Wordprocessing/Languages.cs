using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002EAE RID: 11950
	[GeneratedCode("DomGen", "2.0")]
	internal class Languages : LanguageType
	{
		// Token: 0x17008BB1 RID: 35761
		// (get) Token: 0x06019655 RID: 104021 RVA: 0x002FA5A7 File Offset: 0x002F87A7
		public override string LocalName
		{
			get
			{
				return "lang";
			}
		}

		// Token: 0x17008BB2 RID: 35762
		// (get) Token: 0x06019656 RID: 104022 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008BB3 RID: 35763
		// (get) Token: 0x06019657 RID: 104023 RVA: 0x00349360 File Offset: 0x00347560
		internal override int ElementTypeId
		{
			get
			{
				return 11608;
			}
		}

		// Token: 0x06019658 RID: 104024 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601965A RID: 104026 RVA: 0x0034936F File Offset: 0x0034756F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Languages>(deep);
		}

		// Token: 0x0400A8C5 RID: 43205
		private const string tagName = "lang";

		// Token: 0x0400A8C6 RID: 43206
		private const byte tagNsId = 23;

		// Token: 0x0400A8C7 RID: 43207
		internal const int ElementTypeIdConst = 11608;
	}
}
