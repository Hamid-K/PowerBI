using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BF8 RID: 11256
	[GeneratedCode("DomGen", "2.0")]
	internal class CellMetadata : MetadataBlocksType
	{
		// Token: 0x17007EE8 RID: 32488
		// (get) Token: 0x06017A3F RID: 96831 RVA: 0x00339657 File Offset: 0x00337857
		public override string LocalName
		{
			get
			{
				return "cellMetadata";
			}
		}

		// Token: 0x17007EE9 RID: 32489
		// (get) Token: 0x06017A40 RID: 96832 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007EEA RID: 32490
		// (get) Token: 0x06017A41 RID: 96833 RVA: 0x0033965E File Offset: 0x0033785E
		internal override int ElementTypeId
		{
			get
			{
				return 11235;
			}
		}

		// Token: 0x06017A42 RID: 96834 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06017A43 RID: 96835 RVA: 0x00339665 File Offset: 0x00337865
		public CellMetadata()
		{
		}

		// Token: 0x06017A44 RID: 96836 RVA: 0x0033966D File Offset: 0x0033786D
		public CellMetadata(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017A45 RID: 96837 RVA: 0x00339676 File Offset: 0x00337876
		public CellMetadata(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017A46 RID: 96838 RVA: 0x0033967F File Offset: 0x0033787F
		public CellMetadata(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06017A47 RID: 96839 RVA: 0x00339688 File Offset: 0x00337888
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CellMetadata>(deep);
		}

		// Token: 0x04009D03 RID: 40195
		private const string tagName = "cellMetadata";

		// Token: 0x04009D04 RID: 40196
		private const byte tagNsId = 22;

		// Token: 0x04009D05 RID: 40197
		internal const int ElementTypeIdConst = 11235;
	}
}
