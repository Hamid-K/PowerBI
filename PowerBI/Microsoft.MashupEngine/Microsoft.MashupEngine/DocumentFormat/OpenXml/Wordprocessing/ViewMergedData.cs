using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DA2 RID: 11682
	[GeneratedCode("DomGen", "2.0")]
	internal class ViewMergedData : OnOffType
	{
		// Token: 0x17008782 RID: 34690
		// (get) Token: 0x06018DC4 RID: 101828 RVA: 0x00344E9B File Offset: 0x0034309B
		public override string LocalName
		{
			get
			{
				return "viewMergedData";
			}
		}

		// Token: 0x17008783 RID: 34691
		// (get) Token: 0x06018DC5 RID: 101829 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008784 RID: 34692
		// (get) Token: 0x06018DC6 RID: 101830 RVA: 0x00344EA2 File Offset: 0x003430A2
		internal override int ElementTypeId
		{
			get
			{
				return 11824;
			}
		}

		// Token: 0x06018DC7 RID: 101831 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018DC9 RID: 101833 RVA: 0x00344EA9 File Offset: 0x003430A9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ViewMergedData>(deep);
		}

		// Token: 0x0400A551 RID: 42321
		private const string tagName = "viewMergedData";

		// Token: 0x0400A552 RID: 42322
		private const byte tagNsId = 23;

		// Token: 0x0400A553 RID: 42323
		internal const int ElementTypeIdConst = 11824;
	}
}
