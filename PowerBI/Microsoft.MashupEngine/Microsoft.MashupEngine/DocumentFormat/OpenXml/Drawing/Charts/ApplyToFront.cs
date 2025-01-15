using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002521 RID: 9505
	[GeneratedCode("DomGen", "2.0")]
	internal class ApplyToFront : BooleanType
	{
		// Token: 0x1700547C RID: 21628
		// (get) Token: 0x06011AF3 RID: 72435 RVA: 0x002F1420 File Offset: 0x002EF620
		public override string LocalName
		{
			get
			{
				return "applyToFront";
			}
		}

		// Token: 0x1700547D RID: 21629
		// (get) Token: 0x06011AF4 RID: 72436 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x1700547E RID: 21630
		// (get) Token: 0x06011AF5 RID: 72437 RVA: 0x002F1427 File Offset: 0x002EF627
		internal override int ElementTypeId
		{
			get
			{
				return 10469;
			}
		}

		// Token: 0x06011AF6 RID: 72438 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011AF8 RID: 72440 RVA: 0x002F142E File Offset: 0x002EF62E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ApplyToFront>(deep);
		}

		// Token: 0x04007BF7 RID: 31735
		private const string tagName = "applyToFront";

		// Token: 0x04007BF8 RID: 31736
		private const byte tagNsId = 11;

		// Token: 0x04007BF9 RID: 31737
		internal const int ElementTypeIdConst = 10469;
	}
}
