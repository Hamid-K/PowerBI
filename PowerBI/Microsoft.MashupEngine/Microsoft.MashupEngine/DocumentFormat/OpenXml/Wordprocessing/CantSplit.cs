using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002EED RID: 12013
	[GeneratedCode("DomGen", "2.0")]
	internal class CantSplit : OnOffOnlyType
	{
		// Token: 0x17008D7B RID: 36219
		// (get) Token: 0x06019A46 RID: 105030 RVA: 0x003538AD File Offset: 0x00351AAD
		public override string LocalName
		{
			get
			{
				return "cantSplit";
			}
		}

		// Token: 0x17008D7C RID: 36220
		// (get) Token: 0x06019A47 RID: 105031 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008D7D RID: 36221
		// (get) Token: 0x06019A48 RID: 105032 RVA: 0x003538B4 File Offset: 0x00351AB4
		internal override int ElementTypeId
		{
			get
			{
				return 11667;
			}
		}

		// Token: 0x06019A49 RID: 105033 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019A4B RID: 105035 RVA: 0x003538BB File Offset: 0x00351ABB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CantSplit>(deep);
		}

		// Token: 0x0400A9D4 RID: 43476
		private const string tagName = "cantSplit";

		// Token: 0x0400A9D5 RID: 43477
		private const byte tagNsId = 23;

		// Token: 0x0400A9D6 RID: 43478
		internal const int ElementTypeIdConst = 11667;
	}
}
