using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002515 RID: 9493
	[GeneratedCode("DomGen", "2.0")]
	internal class RightAngleAxes : BooleanType
	{
		// Token: 0x17005458 RID: 21592
		// (get) Token: 0x06011AAB RID: 72363 RVA: 0x002F130C File Offset: 0x002EF50C
		public override string LocalName
		{
			get
			{
				return "rAngAx";
			}
		}

		// Token: 0x17005459 RID: 21593
		// (get) Token: 0x06011AAC RID: 72364 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x1700545A RID: 21594
		// (get) Token: 0x06011AAD RID: 72365 RVA: 0x002F1313 File Offset: 0x002EF513
		internal override int ElementTypeId
		{
			get
			{
				return 10421;
			}
		}

		// Token: 0x06011AAE RID: 72366 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011AB0 RID: 72368 RVA: 0x002F131A File Offset: 0x002EF51A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RightAngleAxes>(deep);
		}

		// Token: 0x04007BD3 RID: 31699
		private const string tagName = "rAngAx";

		// Token: 0x04007BD4 RID: 31700
		private const byte tagNsId = 11;

		// Token: 0x04007BD5 RID: 31701
		internal const int ElementTypeIdConst = 10421;
	}
}
