using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Drawing.Charts
{
	// Token: 0x02002321 RID: 8993
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class DropZonesVisible : BooleanFalseType
	{
		// Token: 0x1700485F RID: 18527
		// (get) Token: 0x0601000E RID: 65550 RVA: 0x002DE76F File Offset: 0x002DC96F
		public override string LocalName
		{
			get
			{
				return "dropZonesVisible";
			}
		}

		// Token: 0x17004860 RID: 18528
		// (get) Token: 0x0601000F RID: 65551 RVA: 0x002DE0C4 File Offset: 0x002DC2C4
		internal override byte NamespaceId
		{
			get
			{
				return 46;
			}
		}

		// Token: 0x17004861 RID: 18529
		// (get) Token: 0x06010010 RID: 65552 RVA: 0x002DE776 File Offset: 0x002DC976
		internal override int ElementTypeId
		{
			get
			{
				return 12700;
			}
		}

		// Token: 0x06010011 RID: 65553 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06010013 RID: 65555 RVA: 0x002DE77D File Offset: 0x002DC97D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DropZonesVisible>(deep);
		}

		// Token: 0x040072A5 RID: 29349
		private const string tagName = "dropZonesVisible";

		// Token: 0x040072A6 RID: 29350
		private const byte tagNsId = 46;

		// Token: 0x040072A7 RID: 29351
		internal const int ElementTypeIdConst = 12700;
	}
}
