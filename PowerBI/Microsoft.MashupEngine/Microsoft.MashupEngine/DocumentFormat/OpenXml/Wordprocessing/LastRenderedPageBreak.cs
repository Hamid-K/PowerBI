using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E6D RID: 11885
	[GeneratedCode("DomGen", "2.0")]
	internal class LastRenderedPageBreak : EmptyType
	{
		// Token: 0x17008A88 RID: 35464
		// (get) Token: 0x060193F1 RID: 103409 RVA: 0x00347B76 File Offset: 0x00345D76
		public override string LocalName
		{
			get
			{
				return "lastRenderedPageBreak";
			}
		}

		// Token: 0x17008A89 RID: 35465
		// (get) Token: 0x060193F2 RID: 103410 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008A8A RID: 35466
		// (get) Token: 0x060193F3 RID: 103411 RVA: 0x00347B7D File Offset: 0x00345D7D
		internal override int ElementTypeId
		{
			get
			{
				return 11574;
			}
		}

		// Token: 0x060193F4 RID: 103412 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060193F6 RID: 103414 RVA: 0x00347B84 File Offset: 0x00345D84
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LastRenderedPageBreak>(deep);
		}

		// Token: 0x0400A7DF RID: 42975
		private const string tagName = "lastRenderedPageBreak";

		// Token: 0x0400A7E0 RID: 42976
		private const byte tagNsId = 23;

		// Token: 0x0400A7E1 RID: 42977
		internal const int ElementTypeIdConst = 11574;
	}
}
