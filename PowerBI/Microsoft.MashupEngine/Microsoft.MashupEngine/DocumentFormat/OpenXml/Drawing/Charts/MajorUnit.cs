using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025BE RID: 9662
	[GeneratedCode("DomGen", "2.0")]
	internal class MajorUnit : AxisUnitType
	{
		// Token: 0x17005776 RID: 22390
		// (get) Token: 0x0601219B RID: 74139 RVA: 0x002F5863 File Offset: 0x002F3A63
		public override string LocalName
		{
			get
			{
				return "majorUnit";
			}
		}

		// Token: 0x17005777 RID: 22391
		// (get) Token: 0x0601219C RID: 74140 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005778 RID: 22392
		// (get) Token: 0x0601219D RID: 74141 RVA: 0x002F586A File Offset: 0x002F3A6A
		internal override int ElementTypeId
		{
			get
			{
				return 10488;
			}
		}

		// Token: 0x0601219E RID: 74142 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060121A0 RID: 74144 RVA: 0x002F5879 File Offset: 0x002F3A79
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MajorUnit>(deep);
		}

		// Token: 0x04007E39 RID: 32313
		private const string tagName = "majorUnit";

		// Token: 0x04007E3A RID: 32314
		private const byte tagNsId = 11;

		// Token: 0x04007E3B RID: 32315
		internal const int ElementTypeIdConst = 10488;
	}
}
