using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C70 RID: 11376
	[GeneratedCode("DomGen", "2.0")]
	internal class HorizontalBorder : BorderPropertiesType
	{
		// Token: 0x170082CB RID: 33483
		// (get) Token: 0x06018304 RID: 99076 RVA: 0x0033F387 File Offset: 0x0033D587
		public override string LocalName
		{
			get
			{
				return "horizontal";
			}
		}

		// Token: 0x170082CC RID: 33484
		// (get) Token: 0x06018305 RID: 99077 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170082CD RID: 33485
		// (get) Token: 0x06018306 RID: 99078 RVA: 0x0033F38E File Offset: 0x0033D58E
		internal override int ElementTypeId
		{
			get
			{
				return 11356;
			}
		}

		// Token: 0x06018307 RID: 99079 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018308 RID: 99080 RVA: 0x0033F2DD File Offset: 0x0033D4DD
		public HorizontalBorder()
		{
		}

		// Token: 0x06018309 RID: 99081 RVA: 0x0033F2E5 File Offset: 0x0033D4E5
		public HorizontalBorder(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601830A RID: 99082 RVA: 0x0033F2EE File Offset: 0x0033D4EE
		public HorizontalBorder(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601830B RID: 99083 RVA: 0x0033F2F7 File Offset: 0x0033D4F7
		public HorizontalBorder(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601830C RID: 99084 RVA: 0x0033F395 File Offset: 0x0033D595
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<HorizontalBorder>(deep);
		}

		// Token: 0x04009F3E RID: 40766
		private const string tagName = "horizontal";

		// Token: 0x04009F3F RID: 40767
		private const byte tagNsId = 22;

		// Token: 0x04009F40 RID: 40768
		internal const int ElementTypeIdConst = 11356;
	}
}
