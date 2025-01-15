using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002516 RID: 9494
	[GeneratedCode("DomGen", "2.0")]
	internal class ShowHorizontalBorder : BooleanType
	{
		// Token: 0x1700545B RID: 21595
		// (get) Token: 0x06011AB1 RID: 72369 RVA: 0x002F1323 File Offset: 0x002EF523
		public override string LocalName
		{
			get
			{
				return "showHorzBorder";
			}
		}

		// Token: 0x1700545C RID: 21596
		// (get) Token: 0x06011AB2 RID: 72370 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x1700545D RID: 21597
		// (get) Token: 0x06011AB3 RID: 72371 RVA: 0x002F132A File Offset: 0x002EF52A
		internal override int ElementTypeId
		{
			get
			{
				return 10423;
			}
		}

		// Token: 0x06011AB4 RID: 72372 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011AB6 RID: 72374 RVA: 0x002F1331 File Offset: 0x002EF531
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ShowHorizontalBorder>(deep);
		}

		// Token: 0x04007BD6 RID: 31702
		private const string tagName = "showHorzBorder";

		// Token: 0x04007BD7 RID: 31703
		private const byte tagNsId = 11;

		// Token: 0x04007BD8 RID: 31704
		internal const int ElementTypeIdConst = 10423;
	}
}
