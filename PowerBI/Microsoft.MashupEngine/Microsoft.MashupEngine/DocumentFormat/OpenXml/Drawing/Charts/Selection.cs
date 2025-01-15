using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x0200252C RID: 9516
	[GeneratedCode("DomGen", "2.0")]
	internal class Selection : BooleanType
	{
		// Token: 0x1700549D RID: 21661
		// (get) Token: 0x06011B35 RID: 72501 RVA: 0x002EAE4F File Offset: 0x002E904F
		public override string LocalName
		{
			get
			{
				return "selection";
			}
		}

		// Token: 0x1700549E RID: 21662
		// (get) Token: 0x06011B36 RID: 72502 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x1700549F RID: 21663
		// (get) Token: 0x06011B37 RID: 72503 RVA: 0x002F1516 File Offset: 0x002EF716
		internal override int ElementTypeId
		{
			get
			{
				return 10508;
			}
		}

		// Token: 0x06011B38 RID: 72504 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011B3A RID: 72506 RVA: 0x002F151D File Offset: 0x002EF71D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Selection>(deep);
		}

		// Token: 0x04007C18 RID: 31768
		private const string tagName = "selection";

		// Token: 0x04007C19 RID: 31769
		private const byte tagNsId = 11;

		// Token: 0x04007C1A RID: 31770
		internal const int ElementTypeIdConst = 10508;
	}
}
