using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002534 RID: 9524
	[GeneratedCode("DomGen", "2.0")]
	internal class TrendlineName : OpenXmlLeafTextElement
	{
		// Token: 0x170054B5 RID: 21685
		// (get) Token: 0x06011B69 RID: 72553 RVA: 0x002F15F0 File Offset: 0x002EF7F0
		public override string LocalName
		{
			get
			{
				return "name";
			}
		}

		// Token: 0x170054B6 RID: 21686
		// (get) Token: 0x06011B6A RID: 72554 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170054B7 RID: 21687
		// (get) Token: 0x06011B6B RID: 72555 RVA: 0x002F15F7 File Offset: 0x002EF7F7
		internal override int ElementTypeId
		{
			get
			{
				return 10436;
			}
		}

		// Token: 0x06011B6C RID: 72556 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011B6D RID: 72557 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public TrendlineName()
		{
		}

		// Token: 0x06011B6E RID: 72558 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public TrendlineName(string text)
			: base(text)
		{
		}

		// Token: 0x06011B6F RID: 72559 RVA: 0x002F1600 File Offset: 0x002EF800
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06011B70 RID: 72560 RVA: 0x002F161B File Offset: 0x002EF81B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TrendlineName>(deep);
		}

		// Token: 0x04007C30 RID: 31792
		private const string tagName = "name";

		// Token: 0x04007C31 RID: 31793
		private const byte tagNsId = 11;

		// Token: 0x04007C32 RID: 31794
		internal const int ElementTypeIdConst = 10436;
	}
}
