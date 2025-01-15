using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002533 RID: 9523
	[GeneratedCode("DomGen", "2.0")]
	internal class Formula : OpenXmlLeafTextElement
	{
		// Token: 0x170054B2 RID: 21682
		// (get) Token: 0x06011B61 RID: 72545 RVA: 0x002C81ED File Offset: 0x002C63ED
		public override string LocalName
		{
			get
			{
				return "f";
			}
		}

		// Token: 0x170054B3 RID: 21683
		// (get) Token: 0x06011B62 RID: 72546 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170054B4 RID: 21684
		// (get) Token: 0x06011B63 RID: 72547 RVA: 0x002F15C4 File Offset: 0x002EF7C4
		internal override int ElementTypeId
		{
			get
			{
				return 10395;
			}
		}

		// Token: 0x06011B64 RID: 72548 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011B65 RID: 72549 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public Formula()
		{
		}

		// Token: 0x06011B66 RID: 72550 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public Formula(string text)
			: base(text)
		{
		}

		// Token: 0x06011B67 RID: 72551 RVA: 0x002F15CC File Offset: 0x002EF7CC
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06011B68 RID: 72552 RVA: 0x002F15E7 File Offset: 0x002EF7E7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Formula>(deep);
		}

		// Token: 0x04007C2D RID: 31789
		private const string tagName = "f";

		// Token: 0x04007C2E RID: 31790
		private const byte tagNsId = 11;

		// Token: 0x04007C2F RID: 31791
		internal const int ElementTypeIdConst = 10395;
	}
}
