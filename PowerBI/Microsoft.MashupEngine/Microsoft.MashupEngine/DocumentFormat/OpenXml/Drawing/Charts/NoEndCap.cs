using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x0200251E RID: 9502
	[GeneratedCode("DomGen", "2.0")]
	internal class NoEndCap : BooleanType
	{
		// Token: 0x17005473 RID: 21619
		// (get) Token: 0x06011AE1 RID: 72417 RVA: 0x002F13DB File Offset: 0x002EF5DB
		public override string LocalName
		{
			get
			{
				return "noEndCap";
			}
		}

		// Token: 0x17005474 RID: 21620
		// (get) Token: 0x06011AE2 RID: 72418 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005475 RID: 21621
		// (get) Token: 0x06011AE3 RID: 72419 RVA: 0x002F13E2 File Offset: 0x002EF5E2
		internal override int ElementTypeId
		{
			get
			{
				return 10449;
			}
		}

		// Token: 0x06011AE4 RID: 72420 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011AE6 RID: 72422 RVA: 0x002F13E9 File Offset: 0x002EF5E9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NoEndCap>(deep);
		}

		// Token: 0x04007BEE RID: 31726
		private const string tagName = "noEndCap";

		// Token: 0x04007BEF RID: 31727
		private const byte tagNsId = 11;

		// Token: 0x04007BF0 RID: 31728
		internal const int ElementTypeIdConst = 10449;
	}
}
