using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E89 RID: 11913
	[GeneratedCode("DomGen", "2.0")]
	internal class NextParagraphStyle : String253Type
	{
		// Token: 0x17008B04 RID: 35588
		// (get) Token: 0x060194F8 RID: 103672 RVA: 0x003486DB File Offset: 0x003468DB
		public override string LocalName
		{
			get
			{
				return "next";
			}
		}

		// Token: 0x17008B05 RID: 35589
		// (get) Token: 0x060194F9 RID: 103673 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008B06 RID: 35590
		// (get) Token: 0x060194FA RID: 103674 RVA: 0x003486E2 File Offset: 0x003468E2
		internal override int ElementTypeId
		{
			get
			{
				return 11895;
			}
		}

		// Token: 0x060194FB RID: 103675 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060194FD RID: 103677 RVA: 0x003486E9 File Offset: 0x003468E9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NextParagraphStyle>(deep);
		}

		// Token: 0x0400A841 RID: 43073
		private const string tagName = "next";

		// Token: 0x0400A842 RID: 43074
		private const byte tagNsId = 23;

		// Token: 0x0400A843 RID: 43075
		internal const int ElementTypeIdConst = 11895;
	}
}
