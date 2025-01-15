using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DB3 RID: 11699
	[GeneratedCode("DomGen", "2.0")]
	internal class PrintFormsData : OnOffType
	{
		// Token: 0x170087B5 RID: 34741
		// (get) Token: 0x06018E2A RID: 101930 RVA: 0x00345022 File Offset: 0x00343222
		public override string LocalName
		{
			get
			{
				return "printFormsData";
			}
		}

		// Token: 0x170087B6 RID: 34742
		// (get) Token: 0x06018E2B RID: 101931 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170087B7 RID: 34743
		// (get) Token: 0x06018E2C RID: 101932 RVA: 0x00345029 File Offset: 0x00343229
		internal override int ElementTypeId
		{
			get
			{
				return 11967;
			}
		}

		// Token: 0x06018E2D RID: 101933 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018E2F RID: 101935 RVA: 0x00345030 File Offset: 0x00343230
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PrintFormsData>(deep);
		}

		// Token: 0x0400A584 RID: 42372
		private const string tagName = "printFormsData";

		// Token: 0x0400A585 RID: 42373
		private const byte tagNsId = 23;

		// Token: 0x0400A586 RID: 42374
		internal const int ElementTypeIdConst = 11967;
	}
}
