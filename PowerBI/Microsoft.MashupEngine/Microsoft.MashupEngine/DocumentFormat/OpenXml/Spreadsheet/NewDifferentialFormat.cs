using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BC7 RID: 11207
	[GeneratedCode("DomGen", "2.0")]
	internal class NewDifferentialFormat : DifferentialFormatType
	{
		// Token: 0x17007CB0 RID: 31920
		// (get) Token: 0x0601758B RID: 95627 RVA: 0x00335AEE File Offset: 0x00333CEE
		public override string LocalName
		{
			get
			{
				return "ndxf";
			}
		}

		// Token: 0x17007CB1 RID: 31921
		// (get) Token: 0x0601758C RID: 95628 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007CB2 RID: 31922
		// (get) Token: 0x0601758D RID: 95629 RVA: 0x00335AF5 File Offset: 0x00333CF5
		internal override int ElementTypeId
		{
			get
			{
				return 11175;
			}
		}

		// Token: 0x0601758E RID: 95630 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601758F RID: 95631 RVA: 0x00335AC2 File Offset: 0x00333CC2
		public NewDifferentialFormat()
		{
		}

		// Token: 0x06017590 RID: 95632 RVA: 0x00335ACA File Offset: 0x00333CCA
		public NewDifferentialFormat(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017591 RID: 95633 RVA: 0x00335AD3 File Offset: 0x00333CD3
		public NewDifferentialFormat(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017592 RID: 95634 RVA: 0x00335ADC File Offset: 0x00333CDC
		public NewDifferentialFormat(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06017593 RID: 95635 RVA: 0x00335AFC File Offset: 0x00333CFC
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NewDifferentialFormat>(deep);
		}

		// Token: 0x04009C04 RID: 39940
		private const string tagName = "ndxf";

		// Token: 0x04009C05 RID: 39941
		private const byte tagNsId = 22;

		// Token: 0x04009C06 RID: 39942
		internal const int ElementTypeIdConst = 11175;
	}
}
