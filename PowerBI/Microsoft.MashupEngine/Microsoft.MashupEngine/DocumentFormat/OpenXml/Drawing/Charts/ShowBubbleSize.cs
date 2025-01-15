using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x0200250F RID: 9487
	[GeneratedCode("DomGen", "2.0")]
	internal class ShowBubbleSize : BooleanType
	{
		// Token: 0x17005446 RID: 21574
		// (get) Token: 0x06011A87 RID: 72327 RVA: 0x002F1282 File Offset: 0x002EF482
		public override string LocalName
		{
			get
			{
				return "showBubbleSize";
			}
		}

		// Token: 0x17005447 RID: 21575
		// (get) Token: 0x06011A88 RID: 72328 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005448 RID: 21576
		// (get) Token: 0x06011A89 RID: 72329 RVA: 0x002F1289 File Offset: 0x002EF489
		internal override int ElementTypeId
		{
			get
			{
				return 10351;
			}
		}

		// Token: 0x06011A8A RID: 72330 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011A8C RID: 72332 RVA: 0x002F1290 File Offset: 0x002EF490
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ShowBubbleSize>(deep);
		}

		// Token: 0x04007BC1 RID: 31681
		private const string tagName = "showBubbleSize";

		// Token: 0x04007BC2 RID: 31682
		private const byte tagNsId = 11;

		// Token: 0x04007BC3 RID: 31683
		internal const int ElementTypeIdConst = 10351;
	}
}
