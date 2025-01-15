using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Drawing.Charts
{
	// Token: 0x0200231D RID: 8989
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class DropZoneFilter : BooleanFalseType
	{
		// Token: 0x17004853 RID: 18515
		// (get) Token: 0x0600FFF6 RID: 65526 RVA: 0x002DE70B File Offset: 0x002DC90B
		public override string LocalName
		{
			get
			{
				return "dropZoneFilter";
			}
		}

		// Token: 0x17004854 RID: 18516
		// (get) Token: 0x0600FFF7 RID: 65527 RVA: 0x002DE0C4 File Offset: 0x002DC2C4
		internal override byte NamespaceId
		{
			get
			{
				return 46;
			}
		}

		// Token: 0x17004855 RID: 18517
		// (get) Token: 0x0600FFF8 RID: 65528 RVA: 0x002DE712 File Offset: 0x002DC912
		internal override int ElementTypeId
		{
			get
			{
				return 12696;
			}
		}

		// Token: 0x0600FFF9 RID: 65529 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x0600FFFB RID: 65531 RVA: 0x002DE721 File Offset: 0x002DC921
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DropZoneFilter>(deep);
		}

		// Token: 0x04007299 RID: 29337
		private const string tagName = "dropZoneFilter";

		// Token: 0x0400729A RID: 29338
		private const byte tagNsId = 46;

		// Token: 0x0400729B RID: 29339
		internal const int ElementTypeIdConst = 12696;
	}
}
