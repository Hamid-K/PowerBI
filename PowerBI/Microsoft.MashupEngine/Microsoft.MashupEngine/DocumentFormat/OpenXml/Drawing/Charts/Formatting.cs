using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x0200252B RID: 9515
	[GeneratedCode("DomGen", "2.0")]
	internal class Formatting : BooleanType
	{
		// Token: 0x1700549A RID: 21658
		// (get) Token: 0x06011B2F RID: 72495 RVA: 0x002F14FF File Offset: 0x002EF6FF
		public override string LocalName
		{
			get
			{
				return "formatting";
			}
		}

		// Token: 0x1700549B RID: 21659
		// (get) Token: 0x06011B30 RID: 72496 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x1700549C RID: 21660
		// (get) Token: 0x06011B31 RID: 72497 RVA: 0x002F1506 File Offset: 0x002EF706
		internal override int ElementTypeId
		{
			get
			{
				return 10507;
			}
		}

		// Token: 0x06011B32 RID: 72498 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011B34 RID: 72500 RVA: 0x002F150D File Offset: 0x002EF70D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Formatting>(deep);
		}

		// Token: 0x04007C15 RID: 31765
		private const string tagName = "formatting";

		// Token: 0x04007C16 RID: 31766
		private const byte tagNsId = 11;

		// Token: 0x04007C17 RID: 31767
		internal const int ElementTypeIdConst = 10507;
	}
}
