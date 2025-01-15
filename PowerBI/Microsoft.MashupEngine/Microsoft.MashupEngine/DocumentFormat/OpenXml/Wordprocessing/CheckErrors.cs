using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E36 RID: 11830
	[GeneratedCode("DomGen", "2.0")]
	internal class CheckErrors : DecimalNumberType
	{
		// Token: 0x1700898E RID: 35214
		// (get) Token: 0x060191E5 RID: 102885 RVA: 0x0034687A File Offset: 0x00344A7A
		public override string LocalName
		{
			get
			{
				return "checkErrors";
			}
		}

		// Token: 0x1700898F RID: 35215
		// (get) Token: 0x060191E6 RID: 102886 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008990 RID: 35216
		// (get) Token: 0x060191E7 RID: 102887 RVA: 0x00346881 File Offset: 0x00344A81
		internal override int ElementTypeId
		{
			get
			{
				return 11826;
			}
		}

		// Token: 0x060191E8 RID: 102888 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060191EA RID: 102890 RVA: 0x00346888 File Offset: 0x00344A88
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CheckErrors>(deep);
		}

		// Token: 0x0400A720 RID: 42784
		private const string tagName = "checkErrors";

		// Token: 0x0400A721 RID: 42785
		private const byte tagNsId = 23;

		// Token: 0x0400A722 RID: 42786
		internal const int ElementTypeIdConst = 11826;
	}
}
