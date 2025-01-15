using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Bibliography
{
	// Token: 0x020028DF RID: 10463
	[GeneratedCode("DomGen", "2.0")]
	internal class Reporter : OpenXmlLeafTextElement
	{
		// Token: 0x17006945 RID: 26949
		// (get) Token: 0x060149EC RID: 84460 RVA: 0x00315020 File Offset: 0x00313220
		public override string LocalName
		{
			get
			{
				return "Reporter";
			}
		}

		// Token: 0x17006946 RID: 26950
		// (get) Token: 0x060149ED RID: 84461 RVA: 0x00142610 File Offset: 0x00140810
		internal override byte NamespaceId
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x17006947 RID: 26951
		// (get) Token: 0x060149EE RID: 84462 RVA: 0x00315027 File Offset: 0x00313227
		internal override int ElementTypeId
		{
			get
			{
				return 10817;
			}
		}

		// Token: 0x060149EF RID: 84463 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060149F0 RID: 84464 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public Reporter()
		{
		}

		// Token: 0x060149F1 RID: 84465 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public Reporter(string text)
			: base(text)
		{
		}

		// Token: 0x060149F2 RID: 84466 RVA: 0x00315030 File Offset: 0x00313230
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x060149F3 RID: 84467 RVA: 0x0031504B File Offset: 0x0031324B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Reporter>(deep);
		}

		// Token: 0x04008F29 RID: 36649
		private const string tagName = "Reporter";

		// Token: 0x04008F2A RID: 36650
		private const byte tagNsId = 9;

		// Token: 0x04008F2B RID: 36651
		internal const int ElementTypeIdConst = 10817;
	}
}
