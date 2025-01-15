using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D81 RID: 11649
	[GeneratedCode("DomGen", "2.0")]
	internal class BoldComplexScript : OnOffType
	{
		// Token: 0x1700871F RID: 34591
		// (get) Token: 0x06018CFE RID: 101630 RVA: 0x00344BE3 File Offset: 0x00342DE3
		public override string LocalName
		{
			get
			{
				return "bCs";
			}
		}

		// Token: 0x17008720 RID: 34592
		// (get) Token: 0x06018CFF RID: 101631 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008721 RID: 34593
		// (get) Token: 0x06018D00 RID: 101632 RVA: 0x00344BEA File Offset: 0x00342DEA
		internal override int ElementTypeId
		{
			get
			{
				return 11578;
			}
		}

		// Token: 0x06018D01 RID: 101633 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018D03 RID: 101635 RVA: 0x00344BF1 File Offset: 0x00342DF1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BoldComplexScript>(deep);
		}

		// Token: 0x0400A4EE RID: 42222
		private const string tagName = "bCs";

		// Token: 0x0400A4EF RID: 42223
		private const byte tagNsId = 23;

		// Token: 0x0400A4F0 RID: 42224
		internal const int ElementTypeIdConst = 11578;
	}
}
