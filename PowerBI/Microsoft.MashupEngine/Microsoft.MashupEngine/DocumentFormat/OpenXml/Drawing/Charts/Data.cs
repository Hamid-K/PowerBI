using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x0200252A RID: 9514
	[GeneratedCode("DomGen", "2.0")]
	internal class Data : BooleanType
	{
		// Token: 0x17005497 RID: 21655
		// (get) Token: 0x06011B29 RID: 72489 RVA: 0x002958E1 File Offset: 0x00293AE1
		public override string LocalName
		{
			get
			{
				return "data";
			}
		}

		// Token: 0x17005498 RID: 21656
		// (get) Token: 0x06011B2A RID: 72490 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005499 RID: 21657
		// (get) Token: 0x06011B2B RID: 72491 RVA: 0x002F14EF File Offset: 0x002EF6EF
		internal override int ElementTypeId
		{
			get
			{
				return 10506;
			}
		}

		// Token: 0x06011B2C RID: 72492 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011B2E RID: 72494 RVA: 0x002F14F6 File Offset: 0x002EF6F6
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Data>(deep);
		}

		// Token: 0x04007C12 RID: 31762
		private const string tagName = "data";

		// Token: 0x04007C13 RID: 31763
		private const byte tagNsId = 11;

		// Token: 0x04007C14 RID: 31764
		internal const int ElementTypeIdConst = 10506;
	}
}
