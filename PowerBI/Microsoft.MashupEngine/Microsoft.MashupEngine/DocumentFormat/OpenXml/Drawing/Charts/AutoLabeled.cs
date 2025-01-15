using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002524 RID: 9508
	[GeneratedCode("DomGen", "2.0")]
	internal class AutoLabeled : BooleanType
	{
		// Token: 0x17005485 RID: 21637
		// (get) Token: 0x06011B05 RID: 72453 RVA: 0x002F1465 File Offset: 0x002EF665
		public override string LocalName
		{
			get
			{
				return "auto";
			}
		}

		// Token: 0x17005486 RID: 21638
		// (get) Token: 0x06011B06 RID: 72454 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005487 RID: 21639
		// (get) Token: 0x06011B07 RID: 72455 RVA: 0x002F146C File Offset: 0x002EF66C
		internal override int ElementTypeId
		{
			get
			{
				return 10481;
			}
		}

		// Token: 0x06011B08 RID: 72456 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011B0A RID: 72458 RVA: 0x002F1473 File Offset: 0x002EF673
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AutoLabeled>(deep);
		}

		// Token: 0x04007C00 RID: 31744
		private const string tagName = "auto";

		// Token: 0x04007C01 RID: 31745
		private const byte tagNsId = 11;

		// Token: 0x04007C02 RID: 31746
		internal const int ElementTypeIdConst = 10481;
	}
}
