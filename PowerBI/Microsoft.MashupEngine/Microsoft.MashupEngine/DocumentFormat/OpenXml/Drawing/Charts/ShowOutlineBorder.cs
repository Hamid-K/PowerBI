using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002518 RID: 9496
	[GeneratedCode("DomGen", "2.0")]
	internal class ShowOutlineBorder : BooleanType
	{
		// Token: 0x17005461 RID: 21601
		// (get) Token: 0x06011ABD RID: 72381 RVA: 0x002F1351 File Offset: 0x002EF551
		public override string LocalName
		{
			get
			{
				return "showOutline";
			}
		}

		// Token: 0x17005462 RID: 21602
		// (get) Token: 0x06011ABE RID: 72382 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005463 RID: 21603
		// (get) Token: 0x06011ABF RID: 72383 RVA: 0x002F1358 File Offset: 0x002EF558
		internal override int ElementTypeId
		{
			get
			{
				return 10425;
			}
		}

		// Token: 0x06011AC0 RID: 72384 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011AC2 RID: 72386 RVA: 0x002F135F File Offset: 0x002EF55F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ShowOutlineBorder>(deep);
		}

		// Token: 0x04007BDC RID: 31708
		private const string tagName = "showOutline";

		// Token: 0x04007BDD RID: 31709
		private const byte tagNsId = 11;

		// Token: 0x04007BDE RID: 31710
		internal const int ElementTypeIdConst = 10425;
	}
}
