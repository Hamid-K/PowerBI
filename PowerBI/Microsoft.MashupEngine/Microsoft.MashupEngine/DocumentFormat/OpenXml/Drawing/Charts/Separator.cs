using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002532 RID: 9522
	[GeneratedCode("DomGen", "2.0")]
	internal class Separator : OpenXmlLeafTextElement
	{
		// Token: 0x170054AF RID: 21679
		// (get) Token: 0x06011B59 RID: 72537 RVA: 0x002CF519 File Offset: 0x002CD719
		public override string LocalName
		{
			get
			{
				return "separator";
			}
		}

		// Token: 0x170054B0 RID: 21680
		// (get) Token: 0x06011B5A RID: 72538 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170054B1 RID: 21681
		// (get) Token: 0x06011B5B RID: 72539 RVA: 0x002F1599 File Offset: 0x002EF799
		internal override int ElementTypeId
		{
			get
			{
				return 10352;
			}
		}

		// Token: 0x06011B5C RID: 72540 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011B5D RID: 72541 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public Separator()
		{
		}

		// Token: 0x06011B5E RID: 72542 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public Separator(string text)
			: base(text)
		{
		}

		// Token: 0x06011B5F RID: 72543 RVA: 0x002F15A0 File Offset: 0x002EF7A0
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06011B60 RID: 72544 RVA: 0x002F15BB File Offset: 0x002EF7BB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Separator>(deep);
		}

		// Token: 0x04007C2A RID: 31786
		private const string tagName = "separator";

		// Token: 0x04007C2B RID: 31787
		private const byte tagNsId = 11;

		// Token: 0x04007C2C RID: 31788
		internal const int ElementTypeIdConst = 10352;
	}
}
