using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DF5 RID: 11765
	[GeneratedCode("DomGen", "2.0")]
	internal class SuppressSpacingAtTopOfPage : OnOffType
	{
		// Token: 0x1700887B RID: 34939
		// (get) Token: 0x06018FB6 RID: 102326 RVA: 0x00345610 File Offset: 0x00343810
		public override string LocalName
		{
			get
			{
				return "suppressSpacingAtTopOfPage";
			}
		}

		// Token: 0x1700887C RID: 34940
		// (get) Token: 0x06018FB7 RID: 102327 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700887D RID: 34941
		// (get) Token: 0x06018FB8 RID: 102328 RVA: 0x00345617 File Offset: 0x00343817
		internal override int ElementTypeId
		{
			get
			{
				return 12075;
			}
		}

		// Token: 0x06018FB9 RID: 102329 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018FBB RID: 102331 RVA: 0x0034561E File Offset: 0x0034381E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SuppressSpacingAtTopOfPage>(deep);
		}

		// Token: 0x0400A64A RID: 42570
		private const string tagName = "suppressSpacingAtTopOfPage";

		// Token: 0x0400A64B RID: 42571
		private const byte tagNsId = 23;

		// Token: 0x0400A64C RID: 42572
		internal const int ElementTypeIdConst = 12075;
	}
}
