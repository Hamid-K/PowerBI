using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B49 RID: 11081
	[GeneratedCode("DomGen", "2.0")]
	internal class MissingTable : OpenXmlLeafElement
	{
		// Token: 0x1700780B RID: 30731
		// (get) Token: 0x06016B7F RID: 93055 RVA: 0x002E0FCF File Offset: 0x002DF1CF
		public override string LocalName
		{
			get
			{
				return "m";
			}
		}

		// Token: 0x1700780C RID: 30732
		// (get) Token: 0x06016B80 RID: 93056 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x1700780D RID: 30733
		// (get) Token: 0x06016B81 RID: 93057 RVA: 0x0032E51E File Offset: 0x0032C71E
		internal override int ElementTypeId
		{
			get
			{
				return 11064;
			}
		}

		// Token: 0x06016B82 RID: 93058 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06016B84 RID: 93060 RVA: 0x0032E525 File Offset: 0x0032C725
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MissingTable>(deep);
		}

		// Token: 0x040099AB RID: 39339
		private const string tagName = "m";

		// Token: 0x040099AC RID: 39340
		private const byte tagNsId = 22;

		// Token: 0x040099AD RID: 39341
		internal const int ElementTypeIdConst = 11064;
	}
}
