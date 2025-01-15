using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E6E RID: 11886
	[GeneratedCode("DomGen", "2.0")]
	internal class OptimizeForBrowser : EmptyType
	{
		// Token: 0x17008A8B RID: 35467
		// (get) Token: 0x060193F7 RID: 103415 RVA: 0x00347B8D File Offset: 0x00345D8D
		public override string LocalName
		{
			get
			{
				return "optimizeForBrowser";
			}
		}

		// Token: 0x17008A8C RID: 35468
		// (get) Token: 0x060193F8 RID: 103416 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008A8D RID: 35469
		// (get) Token: 0x060193F9 RID: 103417 RVA: 0x00347B94 File Offset: 0x00345D94
		internal override int ElementTypeId
		{
			get
			{
				return 11838;
			}
		}

		// Token: 0x060193FA RID: 103418 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060193FC RID: 103420 RVA: 0x00347B9B File Offset: 0x00345D9B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<OptimizeForBrowser>(deep);
		}

		// Token: 0x0400A7E2 RID: 42978
		private const string tagName = "optimizeForBrowser";

		// Token: 0x0400A7E3 RID: 42979
		private const byte tagNsId = 23;

		// Token: 0x0400A7E4 RID: 42980
		internal const int ElementTypeIdConst = 11838;
	}
}
