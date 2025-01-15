using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002570 RID: 9584
	[GeneratedCode("DomGen", "2.0")]
	internal class FormatCode : OpenXmlLeafTextElement
	{
		// Token: 0x170055D7 RID: 21975
		// (get) Token: 0x06011DF6 RID: 73206 RVA: 0x002F3404 File Offset: 0x002F1604
		public override string LocalName
		{
			get
			{
				return "formatCode";
			}
		}

		// Token: 0x170055D8 RID: 21976
		// (get) Token: 0x06011DF7 RID: 73207 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170055D9 RID: 21977
		// (get) Token: 0x06011DF8 RID: 73208 RVA: 0x002F340B File Offset: 0x002F160B
		internal override int ElementTypeId
		{
			get
			{
				return 10391;
			}
		}

		// Token: 0x06011DF9 RID: 73209 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011DFA RID: 73210 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public FormatCode()
		{
		}

		// Token: 0x06011DFB RID: 73211 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public FormatCode(string text)
			: base(text)
		{
		}

		// Token: 0x06011DFC RID: 73212 RVA: 0x002F3414 File Offset: 0x002F1614
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06011DFD RID: 73213 RVA: 0x002F342F File Offset: 0x002F162F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FormatCode>(deep);
		}

		// Token: 0x04007D03 RID: 32003
		private const string tagName = "formatCode";

		// Token: 0x04007D04 RID: 32004
		private const byte tagNsId = 11;

		// Token: 0x04007D05 RID: 32005
		internal const int ElementTypeIdConst = 10391;
	}
}
