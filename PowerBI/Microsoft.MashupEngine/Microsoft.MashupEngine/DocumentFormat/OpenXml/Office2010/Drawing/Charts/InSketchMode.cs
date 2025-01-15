using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Drawing.Charts
{
	// Token: 0x02002322 RID: 8994
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class InSketchMode : BooleanFalseType
	{
		// Token: 0x17004862 RID: 18530
		// (get) Token: 0x06010014 RID: 65556 RVA: 0x002DE786 File Offset: 0x002DC986
		public override string LocalName
		{
			get
			{
				return "inSketchMode";
			}
		}

		// Token: 0x17004863 RID: 18531
		// (get) Token: 0x06010015 RID: 65557 RVA: 0x002DE0C4 File Offset: 0x002DC2C4
		internal override byte NamespaceId
		{
			get
			{
				return 46;
			}
		}

		// Token: 0x17004864 RID: 18532
		// (get) Token: 0x06010016 RID: 65558 RVA: 0x002DE78D File Offset: 0x002DC98D
		internal override int ElementTypeId
		{
			get
			{
				return 12701;
			}
		}

		// Token: 0x06010017 RID: 65559 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06010019 RID: 65561 RVA: 0x002DE794 File Offset: 0x002DC994
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<InSketchMode>(deep);
		}

		// Token: 0x040072A8 RID: 29352
		private const string tagName = "inSketchMode";

		// Token: 0x040072A9 RID: 29353
		private const byte tagNsId = 46;

		// Token: 0x040072AA RID: 29354
		internal const int ElementTypeIdConst = 12701;
	}
}
