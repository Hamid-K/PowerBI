using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BC6 RID: 11206
	[GeneratedCode("DomGen", "2.0")]
	internal class OldDifferentialFormat : DifferentialFormatType
	{
		// Token: 0x17007CAD RID: 31917
		// (get) Token: 0x06017582 RID: 95618 RVA: 0x00335AB4 File Offset: 0x00333CB4
		public override string LocalName
		{
			get
			{
				return "odxf";
			}
		}

		// Token: 0x17007CAE RID: 31918
		// (get) Token: 0x06017583 RID: 95619 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007CAF RID: 31919
		// (get) Token: 0x06017584 RID: 95620 RVA: 0x00335ABB File Offset: 0x00333CBB
		internal override int ElementTypeId
		{
			get
			{
				return 11174;
			}
		}

		// Token: 0x06017585 RID: 95621 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06017586 RID: 95622 RVA: 0x00335AC2 File Offset: 0x00333CC2
		public OldDifferentialFormat()
		{
		}

		// Token: 0x06017587 RID: 95623 RVA: 0x00335ACA File Offset: 0x00333CCA
		public OldDifferentialFormat(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017588 RID: 95624 RVA: 0x00335AD3 File Offset: 0x00333CD3
		public OldDifferentialFormat(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017589 RID: 95625 RVA: 0x00335ADC File Offset: 0x00333CDC
		public OldDifferentialFormat(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601758A RID: 95626 RVA: 0x00335AE5 File Offset: 0x00333CE5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<OldDifferentialFormat>(deep);
		}

		// Token: 0x04009C01 RID: 39937
		private const string tagName = "odxf";

		// Token: 0x04009C02 RID: 39938
		private const byte tagNsId = 22;

		// Token: 0x04009C03 RID: 39939
		internal const int ElementTypeIdConst = 11174;
	}
}
