using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Bibliography
{
	// Token: 0x020028D6 RID: 10454
	[GeneratedCode("DomGen", "2.0")]
	internal class NumberVolumes : OpenXmlLeafTextElement
	{
		// Token: 0x1700692A RID: 26922
		// (get) Token: 0x060149A4 RID: 84388 RVA: 0x00314E4C File Offset: 0x0031304C
		public override string LocalName
		{
			get
			{
				return "NumberVolumes";
			}
		}

		// Token: 0x1700692B RID: 26923
		// (get) Token: 0x060149A5 RID: 84389 RVA: 0x00142610 File Offset: 0x00140810
		internal override byte NamespaceId
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x1700692C RID: 26924
		// (get) Token: 0x060149A6 RID: 84390 RVA: 0x00314E53 File Offset: 0x00313053
		internal override int ElementTypeId
		{
			get
			{
				return 10808;
			}
		}

		// Token: 0x060149A7 RID: 84391 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060149A8 RID: 84392 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public NumberVolumes()
		{
		}

		// Token: 0x060149A9 RID: 84393 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public NumberVolumes(string text)
			: base(text)
		{
		}

		// Token: 0x060149AA RID: 84394 RVA: 0x00314E5C File Offset: 0x0031305C
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x060149AB RID: 84395 RVA: 0x00314E77 File Offset: 0x00313077
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NumberVolumes>(deep);
		}

		// Token: 0x04008F0E RID: 36622
		private const string tagName = "NumberVolumes";

		// Token: 0x04008F0F RID: 36623
		private const byte tagNsId = 9;

		// Token: 0x04008F10 RID: 36624
		internal const int ElementTypeIdConst = 10808;
	}
}
