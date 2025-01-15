using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DAA RID: 11690
	[GeneratedCode("DomGen", "2.0")]
	internal class IsLegalNumberingStyle : OnOffType
	{
		// Token: 0x1700879A RID: 34714
		// (get) Token: 0x06018DF4 RID: 101876 RVA: 0x00344F53 File Offset: 0x00343153
		public override string LocalName
		{
			get
			{
				return "isLgl";
			}
		}

		// Token: 0x1700879B RID: 34715
		// (get) Token: 0x06018DF5 RID: 101877 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700879C RID: 34716
		// (get) Token: 0x06018DF6 RID: 101878 RVA: 0x00344F5A File Offset: 0x0034315A
		internal override int ElementTypeId
		{
			get
			{
				return 11866;
			}
		}

		// Token: 0x06018DF7 RID: 101879 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018DF9 RID: 101881 RVA: 0x00344F61 File Offset: 0x00343161
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<IsLegalNumberingStyle>(deep);
		}

		// Token: 0x0400A569 RID: 42345
		private const string tagName = "isLgl";

		// Token: 0x0400A56A RID: 42346
		private const byte tagNsId = 23;

		// Token: 0x0400A56B RID: 42347
		internal const int ElementTypeIdConst = 11866;
	}
}
