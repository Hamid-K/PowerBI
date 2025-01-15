using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x0200251C RID: 9500
	[GeneratedCode("DomGen", "2.0")]
	internal class DisplayRSquaredValue : BooleanType
	{
		// Token: 0x1700546D RID: 21613
		// (get) Token: 0x06011AD5 RID: 72405 RVA: 0x002F13AD File Offset: 0x002EF5AD
		public override string LocalName
		{
			get
			{
				return "dispRSqr";
			}
		}

		// Token: 0x1700546E RID: 21614
		// (get) Token: 0x06011AD6 RID: 72406 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x1700546F RID: 21615
		// (get) Token: 0x06011AD7 RID: 72407 RVA: 0x002F13B4 File Offset: 0x002EF5B4
		internal override int ElementTypeId
		{
			get
			{
				return 10443;
			}
		}

		// Token: 0x06011AD8 RID: 72408 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011ADA RID: 72410 RVA: 0x002F13BB File Offset: 0x002EF5BB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DisplayRSquaredValue>(deep);
		}

		// Token: 0x04007BE8 RID: 31720
		private const string tagName = "dispRSqr";

		// Token: 0x04007BE9 RID: 31721
		private const byte tagNsId = 11;

		// Token: 0x04007BEA RID: 31722
		internal const int ElementTypeIdConst = 10443;
	}
}
