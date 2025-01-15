using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002519 RID: 9497
	[GeneratedCode("DomGen", "2.0")]
	internal class ShowKeys : BooleanType
	{
		// Token: 0x17005464 RID: 21604
		// (get) Token: 0x06011AC3 RID: 72387 RVA: 0x002F1368 File Offset: 0x002EF568
		public override string LocalName
		{
			get
			{
				return "showKeys";
			}
		}

		// Token: 0x17005465 RID: 21605
		// (get) Token: 0x06011AC4 RID: 72388 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005466 RID: 21606
		// (get) Token: 0x06011AC5 RID: 72389 RVA: 0x002F136F File Offset: 0x002EF56F
		internal override int ElementTypeId
		{
			get
			{
				return 10426;
			}
		}

		// Token: 0x06011AC6 RID: 72390 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011AC8 RID: 72392 RVA: 0x002F1376 File Offset: 0x002EF576
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ShowKeys>(deep);
		}

		// Token: 0x04007BDF RID: 31711
		private const string tagName = "showKeys";

		// Token: 0x04007BE0 RID: 31712
		private const byte tagNsId = 11;

		// Token: 0x04007BE1 RID: 31713
		internal const int ElementTypeIdConst = 10426;
	}
}
