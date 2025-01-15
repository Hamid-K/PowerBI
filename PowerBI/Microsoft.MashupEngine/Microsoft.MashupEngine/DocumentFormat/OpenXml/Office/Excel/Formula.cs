using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office.Excel
{
	// Token: 0x02002380 RID: 9088
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class Formula : OpenXmlLeafTextElement
	{
		// Token: 0x17004B4D RID: 19277
		// (get) Token: 0x0601066B RID: 67179 RVA: 0x002C81ED File Offset: 0x002C63ED
		public override string LocalName
		{
			get
			{
				return "f";
			}
		}

		// Token: 0x17004B4E RID: 19278
		// (get) Token: 0x0601066C RID: 67180 RVA: 0x0022706E File Offset: 0x0022526E
		internal override byte NamespaceId
		{
			get
			{
				return 32;
			}
		}

		// Token: 0x17004B4F RID: 19279
		// (get) Token: 0x0601066D RID: 67181 RVA: 0x002E33A4 File Offset: 0x002E15A4
		internal override int ElementTypeId
		{
			get
			{
				return 12532;
			}
		}

		// Token: 0x0601066E RID: 67182 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x0601066F RID: 67183 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public Formula()
		{
		}

		// Token: 0x06010670 RID: 67184 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public Formula(string text)
			: base(text)
		{
		}

		// Token: 0x06010671 RID: 67185 RVA: 0x002E33AC File Offset: 0x002E15AC
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06010672 RID: 67186 RVA: 0x002E33C7 File Offset: 0x002E15C7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Formula>(deep);
		}

		// Token: 0x04007471 RID: 29809
		private const string tagName = "f";

		// Token: 0x04007472 RID: 29810
		private const byte tagNsId = 32;

		// Token: 0x04007473 RID: 29811
		internal const int ElementTypeIdConst = 12532;
	}
}
