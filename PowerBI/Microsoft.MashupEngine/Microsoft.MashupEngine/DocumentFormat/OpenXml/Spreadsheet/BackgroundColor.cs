using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BA6 RID: 11174
	[GeneratedCode("DomGen", "2.0")]
	internal class BackgroundColor : ColorType
	{
		// Token: 0x17007B5B RID: 31579
		// (get) Token: 0x060172B5 RID: 94901 RVA: 0x003336AD File Offset: 0x003318AD
		public override string LocalName
		{
			get
			{
				return "bgColor";
			}
		}

		// Token: 0x17007B5C RID: 31580
		// (get) Token: 0x060172B6 RID: 94902 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007B5D RID: 31581
		// (get) Token: 0x060172B7 RID: 94903 RVA: 0x003336B4 File Offset: 0x003318B4
		internal override int ElementTypeId
		{
			get
			{
				return 11253;
			}
		}

		// Token: 0x060172B8 RID: 94904 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060172BA RID: 94906 RVA: 0x003336BB File Offset: 0x003318BB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BackgroundColor>(deep);
		}

		// Token: 0x04009B66 RID: 39782
		private const string tagName = "bgColor";

		// Token: 0x04009B67 RID: 39783
		private const byte tagNsId = 22;

		// Token: 0x04009B68 RID: 39784
		internal const int ElementTypeIdConst = 11253;
	}
}
