using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Bibliography
{
	// Token: 0x020028C1 RID: 10433
	[GeneratedCode("DomGen", "2.0")]
	internal class CaseNumber : OpenXmlLeafTextElement
	{
		// Token: 0x170068EB RID: 26859
		// (get) Token: 0x060148FC RID: 84220 RVA: 0x00314A08 File Offset: 0x00312C08
		public override string LocalName
		{
			get
			{
				return "CaseNumber";
			}
		}

		// Token: 0x170068EC RID: 26860
		// (get) Token: 0x060148FD RID: 84221 RVA: 0x00142610 File Offset: 0x00140810
		internal override byte NamespaceId
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x170068ED RID: 26861
		// (get) Token: 0x060148FE RID: 84222 RVA: 0x00314A0F File Offset: 0x00312C0F
		internal override int ElementTypeId
		{
			get
			{
				return 10787;
			}
		}

		// Token: 0x060148FF RID: 84223 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014900 RID: 84224 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public CaseNumber()
		{
		}

		// Token: 0x06014901 RID: 84225 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public CaseNumber(string text)
			: base(text)
		{
		}

		// Token: 0x06014902 RID: 84226 RVA: 0x00314A18 File Offset: 0x00312C18
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06014903 RID: 84227 RVA: 0x00314A33 File Offset: 0x00312C33
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CaseNumber>(deep);
		}

		// Token: 0x04008ECF RID: 36559
		private const string tagName = "CaseNumber";

		// Token: 0x04008ED0 RID: 36560
		private const byte tagNsId = 9;

		// Token: 0x04008ED1 RID: 36561
		internal const int ElementTypeIdConst = 10787;
	}
}
