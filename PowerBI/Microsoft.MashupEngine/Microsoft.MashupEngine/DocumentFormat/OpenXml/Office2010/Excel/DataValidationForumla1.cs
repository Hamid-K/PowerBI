using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x020023F9 RID: 9209
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class DataValidationForumla1 : DataValidationFormulaType
	{
		// Token: 0x17004E6F RID: 20079
		// (get) Token: 0x06010D5F RID: 68959 RVA: 0x002E7D58 File Offset: 0x002E5F58
		public override string LocalName
		{
			get
			{
				return "formula1";
			}
		}

		// Token: 0x17004E70 RID: 20080
		// (get) Token: 0x06010D60 RID: 68960 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004E71 RID: 20081
		// (get) Token: 0x06010D61 RID: 68961 RVA: 0x002E7D5F File Offset: 0x002E5F5F
		internal override int ElementTypeId
		{
			get
			{
				return 12934;
			}
		}

		// Token: 0x06010D62 RID: 68962 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06010D63 RID: 68963 RVA: 0x002E7D66 File Offset: 0x002E5F66
		public DataValidationForumla1()
		{
		}

		// Token: 0x06010D64 RID: 68964 RVA: 0x002E7D6E File Offset: 0x002E5F6E
		public DataValidationForumla1(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010D65 RID: 68965 RVA: 0x002E7D77 File Offset: 0x002E5F77
		public DataValidationForumla1(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010D66 RID: 68966 RVA: 0x002E7D80 File Offset: 0x002E5F80
		public DataValidationForumla1(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06010D67 RID: 68967 RVA: 0x002E7D89 File Offset: 0x002E5F89
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DataValidationForumla1>(deep);
		}

		// Token: 0x04007677 RID: 30327
		private const string tagName = "formula1";

		// Token: 0x04007678 RID: 30328
		private const byte tagNsId = 53;

		// Token: 0x04007679 RID: 30329
		internal const int ElementTypeIdConst = 12934;
	}
}
