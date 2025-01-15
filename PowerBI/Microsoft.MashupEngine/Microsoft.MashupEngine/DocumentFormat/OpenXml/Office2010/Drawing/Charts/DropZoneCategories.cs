using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Drawing.Charts
{
	// Token: 0x0200231E RID: 8990
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class DropZoneCategories : BooleanFalseType
	{
		// Token: 0x17004856 RID: 18518
		// (get) Token: 0x0600FFFC RID: 65532 RVA: 0x002DE72A File Offset: 0x002DC92A
		public override string LocalName
		{
			get
			{
				return "dropZoneCategories";
			}
		}

		// Token: 0x17004857 RID: 18519
		// (get) Token: 0x0600FFFD RID: 65533 RVA: 0x002DE0C4 File Offset: 0x002DC2C4
		internal override byte NamespaceId
		{
			get
			{
				return 46;
			}
		}

		// Token: 0x17004858 RID: 18520
		// (get) Token: 0x0600FFFE RID: 65534 RVA: 0x002DE731 File Offset: 0x002DC931
		internal override int ElementTypeId
		{
			get
			{
				return 12697;
			}
		}

		// Token: 0x0600FFFF RID: 65535 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06010001 RID: 65537 RVA: 0x002DE738 File Offset: 0x002DC938
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DropZoneCategories>(deep);
		}

		// Token: 0x0400729C RID: 29340
		private const string tagName = "dropZoneCategories";

		// Token: 0x0400729D RID: 29341
		private const byte tagNsId = 46;

		// Token: 0x0400729E RID: 29342
		internal const int ElementTypeIdConst = 12697;
	}
}
