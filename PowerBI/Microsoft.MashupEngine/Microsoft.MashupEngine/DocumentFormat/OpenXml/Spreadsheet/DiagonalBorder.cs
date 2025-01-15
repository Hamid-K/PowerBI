using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C6E RID: 11374
	[GeneratedCode("DomGen", "2.0")]
	internal class DiagonalBorder : BorderPropertiesType
	{
		// Token: 0x170082C5 RID: 33477
		// (get) Token: 0x060182F2 RID: 99058 RVA: 0x0033F359 File Offset: 0x0033D559
		public override string LocalName
		{
			get
			{
				return "diagonal";
			}
		}

		// Token: 0x170082C6 RID: 33478
		// (get) Token: 0x060182F3 RID: 99059 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170082C7 RID: 33479
		// (get) Token: 0x060182F4 RID: 99060 RVA: 0x0033F360 File Offset: 0x0033D560
		internal override int ElementTypeId
		{
			get
			{
				return 11354;
			}
		}

		// Token: 0x060182F5 RID: 99061 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060182F6 RID: 99062 RVA: 0x0033F2DD File Offset: 0x0033D4DD
		public DiagonalBorder()
		{
		}

		// Token: 0x060182F7 RID: 99063 RVA: 0x0033F2E5 File Offset: 0x0033D4E5
		public DiagonalBorder(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060182F8 RID: 99064 RVA: 0x0033F2EE File Offset: 0x0033D4EE
		public DiagonalBorder(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060182F9 RID: 99065 RVA: 0x0033F2F7 File Offset: 0x0033D4F7
		public DiagonalBorder(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060182FA RID: 99066 RVA: 0x0033F367 File Offset: 0x0033D567
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DiagonalBorder>(deep);
		}

		// Token: 0x04009F38 RID: 40760
		private const string tagName = "diagonal";

		// Token: 0x04009F39 RID: 40761
		private const byte tagNsId = 22;

		// Token: 0x04009F3A RID: 40762
		internal const int ElementTypeIdConst = 11354;
	}
}
