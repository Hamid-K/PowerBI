using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025FA RID: 9722
	[GeneratedCode("DomGen", "2.0")]
	internal class MinorTimeUnit : TimeUnitType
	{
		// Token: 0x1700596D RID: 22893
		// (get) Token: 0x060125EB RID: 75243 RVA: 0x002FA455 File Offset: 0x002F8655
		public override string LocalName
		{
			get
			{
				return "minorTimeUnit";
			}
		}

		// Token: 0x1700596E RID: 22894
		// (get) Token: 0x060125EC RID: 75244 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x1700596F RID: 22895
		// (get) Token: 0x060125ED RID: 75245 RVA: 0x002FA45C File Offset: 0x002F865C
		internal override int ElementTypeId
		{
			get
			{
				return 10567;
			}
		}

		// Token: 0x060125EE RID: 75246 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060125F0 RID: 75248 RVA: 0x002FA463 File Offset: 0x002F8663
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MinorTimeUnit>(deep);
		}

		// Token: 0x04007F48 RID: 32584
		private const string tagName = "minorTimeUnit";

		// Token: 0x04007F49 RID: 32585
		private const byte tagNsId = 11;

		// Token: 0x04007F4A RID: 32586
		internal const int ElementTypeIdConst = 10567;
	}
}
