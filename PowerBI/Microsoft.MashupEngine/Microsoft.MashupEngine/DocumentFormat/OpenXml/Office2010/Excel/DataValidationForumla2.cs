using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x020023FA RID: 9210
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class DataValidationForumla2 : DataValidationFormulaType
	{
		// Token: 0x17004E72 RID: 20082
		// (get) Token: 0x06010D68 RID: 68968 RVA: 0x002E7D92 File Offset: 0x002E5F92
		public override string LocalName
		{
			get
			{
				return "formula2";
			}
		}

		// Token: 0x17004E73 RID: 20083
		// (get) Token: 0x06010D69 RID: 68969 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004E74 RID: 20084
		// (get) Token: 0x06010D6A RID: 68970 RVA: 0x002E7D99 File Offset: 0x002E5F99
		internal override int ElementTypeId
		{
			get
			{
				return 12935;
			}
		}

		// Token: 0x06010D6B RID: 68971 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06010D6C RID: 68972 RVA: 0x002E7D66 File Offset: 0x002E5F66
		public DataValidationForumla2()
		{
		}

		// Token: 0x06010D6D RID: 68973 RVA: 0x002E7D6E File Offset: 0x002E5F6E
		public DataValidationForumla2(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010D6E RID: 68974 RVA: 0x002E7D77 File Offset: 0x002E5F77
		public DataValidationForumla2(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010D6F RID: 68975 RVA: 0x002E7D80 File Offset: 0x002E5F80
		public DataValidationForumla2(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06010D70 RID: 68976 RVA: 0x002E7DA0 File Offset: 0x002E5FA0
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DataValidationForumla2>(deep);
		}

		// Token: 0x0400767A RID: 30330
		private const string tagName = "formula2";

		// Token: 0x0400767B RID: 30331
		private const byte tagNsId = 53;

		// Token: 0x0400767C RID: 30332
		internal const int ElementTypeIdConst = 12935;
	}
}
