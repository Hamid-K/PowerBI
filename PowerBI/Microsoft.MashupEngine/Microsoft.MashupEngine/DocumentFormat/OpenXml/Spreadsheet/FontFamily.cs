using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BA9 RID: 11177
	[GeneratedCode("DomGen", "2.0")]
	internal class FontFamily : InternationalPropertyType
	{
		// Token: 0x17007B67 RID: 31591
		// (get) Token: 0x060172CE RID: 94926 RVA: 0x0033375B File Offset: 0x0033195B
		public override string LocalName
		{
			get
			{
				return "family";
			}
		}

		// Token: 0x17007B68 RID: 31592
		// (get) Token: 0x060172CF RID: 94927 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007B69 RID: 31593
		// (get) Token: 0x060172D0 RID: 94928 RVA: 0x00333762 File Offset: 0x00331962
		internal override int ElementTypeId
		{
			get
			{
				return 11147;
			}
		}

		// Token: 0x060172D1 RID: 94929 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060172D3 RID: 94931 RVA: 0x00333771 File Offset: 0x00331971
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FontFamily>(deep);
		}

		// Token: 0x04009B70 RID: 39792
		private const string tagName = "family";

		// Token: 0x04009B71 RID: 39793
		private const byte tagNsId = 22;

		// Token: 0x04009B72 RID: 39794
		internal const int ElementTypeIdConst = 11147;
	}
}
