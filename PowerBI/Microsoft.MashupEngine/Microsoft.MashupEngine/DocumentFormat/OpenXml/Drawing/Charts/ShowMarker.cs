using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x0200251F RID: 9503
	[GeneratedCode("DomGen", "2.0")]
	internal class ShowMarker : BooleanType
	{
		// Token: 0x17005476 RID: 21622
		// (get) Token: 0x06011AE7 RID: 72423 RVA: 0x002F13F2 File Offset: 0x002EF5F2
		public override string LocalName
		{
			get
			{
				return "marker";
			}
		}

		// Token: 0x17005477 RID: 21623
		// (get) Token: 0x06011AE8 RID: 72424 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005478 RID: 21624
		// (get) Token: 0x06011AE9 RID: 72425 RVA: 0x002F13F9 File Offset: 0x002EF5F9
		internal override int ElementTypeId
		{
			get
			{
				return 10458;
			}
		}

		// Token: 0x06011AEA RID: 72426 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011AEC RID: 72428 RVA: 0x002F1400 File Offset: 0x002EF600
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ShowMarker>(deep);
		}

		// Token: 0x04007BF1 RID: 31729
		private const string tagName = "marker";

		// Token: 0x04007BF2 RID: 31730
		private const byte tagNsId = 11;

		// Token: 0x04007BF3 RID: 31731
		internal const int ElementTypeIdConst = 10458;
	}
}
