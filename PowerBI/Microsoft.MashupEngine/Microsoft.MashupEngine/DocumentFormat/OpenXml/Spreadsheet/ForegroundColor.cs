using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BA5 RID: 11173
	[GeneratedCode("DomGen", "2.0")]
	internal class ForegroundColor : ColorType
	{
		// Token: 0x17007B58 RID: 31576
		// (get) Token: 0x060172AF RID: 94895 RVA: 0x00333696 File Offset: 0x00331896
		public override string LocalName
		{
			get
			{
				return "fgColor";
			}
		}

		// Token: 0x17007B59 RID: 31577
		// (get) Token: 0x060172B0 RID: 94896 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007B5A RID: 31578
		// (get) Token: 0x060172B1 RID: 94897 RVA: 0x0033369D File Offset: 0x0033189D
		internal override int ElementTypeId
		{
			get
			{
				return 11252;
			}
		}

		// Token: 0x060172B2 RID: 94898 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060172B4 RID: 94900 RVA: 0x003336A4 File Offset: 0x003318A4
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ForegroundColor>(deep);
		}

		// Token: 0x04009B63 RID: 39779
		private const string tagName = "fgColor";

		// Token: 0x04009B64 RID: 39780
		private const byte tagNsId = 22;

		// Token: 0x04009B65 RID: 39781
		internal const int ElementTypeIdConst = 11252;
	}
}
