using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Drawing.Charts
{
	// Token: 0x0200231F RID: 8991
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class DropZoneData : BooleanFalseType
	{
		// Token: 0x17004859 RID: 18521
		// (get) Token: 0x06010002 RID: 65538 RVA: 0x002DE741 File Offset: 0x002DC941
		public override string LocalName
		{
			get
			{
				return "dropZoneData";
			}
		}

		// Token: 0x1700485A RID: 18522
		// (get) Token: 0x06010003 RID: 65539 RVA: 0x002DE0C4 File Offset: 0x002DC2C4
		internal override byte NamespaceId
		{
			get
			{
				return 46;
			}
		}

		// Token: 0x1700485B RID: 18523
		// (get) Token: 0x06010004 RID: 65540 RVA: 0x002DE748 File Offset: 0x002DC948
		internal override int ElementTypeId
		{
			get
			{
				return 12698;
			}
		}

		// Token: 0x06010005 RID: 65541 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06010007 RID: 65543 RVA: 0x002DE74F File Offset: 0x002DC94F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DropZoneData>(deep);
		}

		// Token: 0x0400729F RID: 29343
		private const string tagName = "dropZoneData";

		// Token: 0x040072A0 RID: 29344
		private const byte tagNsId = 46;

		// Token: 0x040072A1 RID: 29345
		internal const int ElementTypeIdConst = 12698;
	}
}
