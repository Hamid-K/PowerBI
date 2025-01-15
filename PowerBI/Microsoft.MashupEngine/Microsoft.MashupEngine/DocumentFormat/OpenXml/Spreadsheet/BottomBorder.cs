using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C6D RID: 11373
	[GeneratedCode("DomGen", "2.0")]
	internal class BottomBorder : BorderPropertiesType
	{
		// Token: 0x170082C2 RID: 33474
		// (get) Token: 0x060182E9 RID: 99049 RVA: 0x002BF3AD File Offset: 0x002BD5AD
		public override string LocalName
		{
			get
			{
				return "bottom";
			}
		}

		// Token: 0x170082C3 RID: 33475
		// (get) Token: 0x060182EA RID: 99050 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170082C4 RID: 33476
		// (get) Token: 0x060182EB RID: 99051 RVA: 0x0033F349 File Offset: 0x0033D549
		internal override int ElementTypeId
		{
			get
			{
				return 11353;
			}
		}

		// Token: 0x060182EC RID: 99052 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060182ED RID: 99053 RVA: 0x0033F2DD File Offset: 0x0033D4DD
		public BottomBorder()
		{
		}

		// Token: 0x060182EE RID: 99054 RVA: 0x0033F2E5 File Offset: 0x0033D4E5
		public BottomBorder(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060182EF RID: 99055 RVA: 0x0033F2EE File Offset: 0x0033D4EE
		public BottomBorder(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060182F0 RID: 99056 RVA: 0x0033F2F7 File Offset: 0x0033D4F7
		public BottomBorder(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060182F1 RID: 99057 RVA: 0x0033F350 File Offset: 0x0033D550
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BottomBorder>(deep);
		}

		// Token: 0x04009F35 RID: 40757
		private const string tagName = "bottom";

		// Token: 0x04009F36 RID: 40758
		private const byte tagNsId = 22;

		// Token: 0x04009F37 RID: 40759
		internal const int ElementTypeIdConst = 11353;
	}
}
