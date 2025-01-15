using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x020024AF RID: 9391
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class ContextualAlternatives : OnOffType
	{
		// Token: 0x1700524B RID: 21067
		// (get) Token: 0x0601161F RID: 71199 RVA: 0x002EDF48 File Offset: 0x002EC148
		public override string LocalName
		{
			get
			{
				return "cntxtAlts";
			}
		}

		// Token: 0x1700524C RID: 21068
		// (get) Token: 0x06011620 RID: 71200 RVA: 0x002EC7B7 File Offset: 0x002EA9B7
		internal override byte NamespaceId
		{
			get
			{
				return 52;
			}
		}

		// Token: 0x1700524D RID: 21069
		// (get) Token: 0x06011621 RID: 71201 RVA: 0x002EDF4F File Offset: 0x002EC14F
		internal override int ElementTypeId
		{
			get
			{
				return 12864;
			}
		}

		// Token: 0x06011622 RID: 71202 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06011624 RID: 71204 RVA: 0x002EDF5E File Offset: 0x002EC15E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ContextualAlternatives>(deep);
		}

		// Token: 0x0400798E RID: 31118
		private const string tagName = "cntxtAlts";

		// Token: 0x0400798F RID: 31119
		private const byte tagNsId = 52;

		// Token: 0x04007990 RID: 31120
		internal const int ElementTypeIdConst = 12864;
	}
}
