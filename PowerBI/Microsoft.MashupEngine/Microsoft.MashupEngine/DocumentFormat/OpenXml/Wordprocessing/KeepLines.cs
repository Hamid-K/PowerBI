using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D6B RID: 11627
	[GeneratedCode("DomGen", "2.0")]
	internal class KeepLines : OnOffType
	{
		// Token: 0x170086DD RID: 34525
		// (get) Token: 0x06018C7A RID: 101498 RVA: 0x003449F7 File Offset: 0x00342BF7
		public override string LocalName
		{
			get
			{
				return "keepLines";
			}
		}

		// Token: 0x170086DE RID: 34526
		// (get) Token: 0x06018C7B RID: 101499 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170086DF RID: 34527
		// (get) Token: 0x06018C7C RID: 101500 RVA: 0x003449FE File Offset: 0x00342BFE
		internal override int ElementTypeId
		{
			get
			{
				return 11494;
			}
		}

		// Token: 0x06018C7D RID: 101501 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018C7F RID: 101503 RVA: 0x00344A05 File Offset: 0x00342C05
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<KeepLines>(deep);
		}

		// Token: 0x0400A4AC RID: 42156
		private const string tagName = "keepLines";

		// Token: 0x0400A4AD RID: 42157
		private const byte tagNsId = 23;

		// Token: 0x0400A4AE RID: 42158
		internal const int ElementTypeIdConst = 11494;
	}
}
