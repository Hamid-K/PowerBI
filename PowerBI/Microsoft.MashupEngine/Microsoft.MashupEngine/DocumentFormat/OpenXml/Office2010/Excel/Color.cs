using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x02002405 RID: 9221
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class Color : ColorType
	{
		// Token: 0x17004EB7 RID: 20151
		// (get) Token: 0x06010DF7 RID: 69111 RVA: 0x002E847F File Offset: 0x002E667F
		public override string LocalName
		{
			get
			{
				return "color";
			}
		}

		// Token: 0x17004EB8 RID: 20152
		// (get) Token: 0x06010DF8 RID: 69112 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004EB9 RID: 20153
		// (get) Token: 0x06010DF9 RID: 69113 RVA: 0x002E8486 File Offset: 0x002E6686
		internal override int ElementTypeId
		{
			get
			{
				return 12966;
			}
		}

		// Token: 0x06010DFA RID: 69114 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06010DFC RID: 69116 RVA: 0x002E848D File Offset: 0x002E668D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Color>(deep);
		}

		// Token: 0x0400769E RID: 30366
		private const string tagName = "color";

		// Token: 0x0400769F RID: 30367
		private const byte tagNsId = 53;

		// Token: 0x040076A0 RID: 30368
		internal const int ElementTypeIdConst = 12966;
	}
}
