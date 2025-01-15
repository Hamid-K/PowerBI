using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002574 RID: 9588
	[GeneratedCode("DomGen", "2.0")]
	internal class EvenFooter : OpenXmlLeafTextElement
	{
		// Token: 0x170055E3 RID: 21987
		// (get) Token: 0x06011E16 RID: 73238 RVA: 0x002F34D4 File Offset: 0x002F16D4
		public override string LocalName
		{
			get
			{
				return "evenFooter";
			}
		}

		// Token: 0x170055E4 RID: 21988
		// (get) Token: 0x06011E17 RID: 73239 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170055E5 RID: 21989
		// (get) Token: 0x06011E18 RID: 73240 RVA: 0x002F34DB File Offset: 0x002F16DB
		internal override int ElementTypeId
		{
			get
			{
				return 10513;
			}
		}

		// Token: 0x06011E19 RID: 73241 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011E1A RID: 73242 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public EvenFooter()
		{
		}

		// Token: 0x06011E1B RID: 73243 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public EvenFooter(string text)
			: base(text)
		{
		}

		// Token: 0x06011E1C RID: 73244 RVA: 0x002F34E4 File Offset: 0x002F16E4
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06011E1D RID: 73245 RVA: 0x002F34FF File Offset: 0x002F16FF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<EvenFooter>(deep);
		}

		// Token: 0x04007D0F RID: 32015
		private const string tagName = "evenFooter";

		// Token: 0x04007D10 RID: 32016
		private const byte tagNsId = 11;

		// Token: 0x04007D11 RID: 32017
		internal const int ElementTypeIdConst = 10513;
	}
}
