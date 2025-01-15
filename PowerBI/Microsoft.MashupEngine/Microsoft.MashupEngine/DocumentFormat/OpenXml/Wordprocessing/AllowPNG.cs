using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DA4 RID: 11684
	[GeneratedCode("DomGen", "2.0")]
	internal class AllowPNG : OnOffType
	{
		// Token: 0x17008788 RID: 34696
		// (get) Token: 0x06018DD0 RID: 101840 RVA: 0x00344EC9 File Offset: 0x003430C9
		public override string LocalName
		{
			get
			{
				return "allowPNG";
			}
		}

		// Token: 0x17008789 RID: 34697
		// (get) Token: 0x06018DD1 RID: 101841 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700878A RID: 34698
		// (get) Token: 0x06018DD2 RID: 101842 RVA: 0x00344ED0 File Offset: 0x003430D0
		internal override int ElementTypeId
		{
			get
			{
				return 11840;
			}
		}

		// Token: 0x06018DD3 RID: 101843 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018DD5 RID: 101845 RVA: 0x00344ED7 File Offset: 0x003430D7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AllowPNG>(deep);
		}

		// Token: 0x0400A557 RID: 42327
		private const string tagName = "allowPNG";

		// Token: 0x0400A558 RID: 42328
		private const byte tagNsId = 23;

		// Token: 0x0400A559 RID: 42329
		internal const int ElementTypeIdConst = 11840;
	}
}
