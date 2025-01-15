using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BC8 RID: 11208
	[GeneratedCode("DomGen", "2.0")]
	internal class DifferentialFormat : DifferentialFormatType
	{
		// Token: 0x17007CB3 RID: 31923
		// (get) Token: 0x06017594 RID: 95636 RVA: 0x002E8FB3 File Offset: 0x002E71B3
		public override string LocalName
		{
			get
			{
				return "dxf";
			}
		}

		// Token: 0x17007CB4 RID: 31924
		// (get) Token: 0x06017595 RID: 95637 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007CB5 RID: 31925
		// (get) Token: 0x06017596 RID: 95638 RVA: 0x00335B05 File Offset: 0x00333D05
		internal override int ElementTypeId
		{
			get
			{
				return 11176;
			}
		}

		// Token: 0x06017597 RID: 95639 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06017598 RID: 95640 RVA: 0x00335AC2 File Offset: 0x00333CC2
		public DifferentialFormat()
		{
		}

		// Token: 0x06017599 RID: 95641 RVA: 0x00335ACA File Offset: 0x00333CCA
		public DifferentialFormat(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601759A RID: 95642 RVA: 0x00335AD3 File Offset: 0x00333CD3
		public DifferentialFormat(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601759B RID: 95643 RVA: 0x00335ADC File Offset: 0x00333CDC
		public DifferentialFormat(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601759C RID: 95644 RVA: 0x00335B0C File Offset: 0x00333D0C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DifferentialFormat>(deep);
		}

		// Token: 0x04009C07 RID: 39943
		private const string tagName = "dxf";

		// Token: 0x04009C08 RID: 39944
		private const byte tagNsId = 22;

		// Token: 0x04009C09 RID: 39945
		internal const int ElementTypeIdConst = 11176;
	}
}
