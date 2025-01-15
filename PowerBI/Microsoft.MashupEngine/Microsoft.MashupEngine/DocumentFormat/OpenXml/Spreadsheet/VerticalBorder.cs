using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C6F RID: 11375
	[GeneratedCode("DomGen", "2.0")]
	internal class VerticalBorder : BorderPropertiesType
	{
		// Token: 0x170082C8 RID: 33480
		// (get) Token: 0x060182FB RID: 99067 RVA: 0x0033F370 File Offset: 0x0033D570
		public override string LocalName
		{
			get
			{
				return "vertical";
			}
		}

		// Token: 0x170082C9 RID: 33481
		// (get) Token: 0x060182FC RID: 99068 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170082CA RID: 33482
		// (get) Token: 0x060182FD RID: 99069 RVA: 0x0033F377 File Offset: 0x0033D577
		internal override int ElementTypeId
		{
			get
			{
				return 11355;
			}
		}

		// Token: 0x060182FE RID: 99070 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060182FF RID: 99071 RVA: 0x0033F2DD File Offset: 0x0033D4DD
		public VerticalBorder()
		{
		}

		// Token: 0x06018300 RID: 99072 RVA: 0x0033F2E5 File Offset: 0x0033D4E5
		public VerticalBorder(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018301 RID: 99073 RVA: 0x0033F2EE File Offset: 0x0033D4EE
		public VerticalBorder(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018302 RID: 99074 RVA: 0x0033F2F7 File Offset: 0x0033D4F7
		public VerticalBorder(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06018303 RID: 99075 RVA: 0x0033F37E File Offset: 0x0033D57E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<VerticalBorder>(deep);
		}

		// Token: 0x04009F3B RID: 40763
		private const string tagName = "vertical";

		// Token: 0x04009F3C RID: 40764
		private const byte tagNsId = 22;

		// Token: 0x04009F3D RID: 40765
		internal const int ElementTypeIdConst = 11355;
	}
}
