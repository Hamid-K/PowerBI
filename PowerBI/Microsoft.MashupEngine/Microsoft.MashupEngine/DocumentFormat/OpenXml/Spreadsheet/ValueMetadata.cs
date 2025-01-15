using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BF9 RID: 11257
	[GeneratedCode("DomGen", "2.0")]
	internal class ValueMetadata : MetadataBlocksType
	{
		// Token: 0x17007EEB RID: 32491
		// (get) Token: 0x06017A48 RID: 96840 RVA: 0x00339691 File Offset: 0x00337891
		public override string LocalName
		{
			get
			{
				return "valueMetadata";
			}
		}

		// Token: 0x17007EEC RID: 32492
		// (get) Token: 0x06017A49 RID: 96841 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007EED RID: 32493
		// (get) Token: 0x06017A4A RID: 96842 RVA: 0x00339698 File Offset: 0x00337898
		internal override int ElementTypeId
		{
			get
			{
				return 11236;
			}
		}

		// Token: 0x06017A4B RID: 96843 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06017A4C RID: 96844 RVA: 0x00339665 File Offset: 0x00337865
		public ValueMetadata()
		{
		}

		// Token: 0x06017A4D RID: 96845 RVA: 0x0033966D File Offset: 0x0033786D
		public ValueMetadata(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017A4E RID: 96846 RVA: 0x00339676 File Offset: 0x00337876
		public ValueMetadata(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017A4F RID: 96847 RVA: 0x0033967F File Offset: 0x0033787F
		public ValueMetadata(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06017A50 RID: 96848 RVA: 0x0033969F File Offset: 0x0033789F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ValueMetadata>(deep);
		}

		// Token: 0x04009D06 RID: 40198
		private const string tagName = "valueMetadata";

		// Token: 0x04009D07 RID: 40199
		private const byte tagNsId = 22;

		// Token: 0x04009D08 RID: 40200
		internal const int ElementTypeIdConst = 11236;
	}
}
