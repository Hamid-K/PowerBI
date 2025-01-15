using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x0200256F RID: 9583
	[GeneratedCode("DomGen", "2.0")]
	internal class NumericValue : OpenXmlLeafTextElement
	{
		// Token: 0x170055D4 RID: 21972
		// (get) Token: 0x06011DEE RID: 73198 RVA: 0x002F33CF File Offset: 0x002F15CF
		public override string LocalName
		{
			get
			{
				return "v";
			}
		}

		// Token: 0x170055D5 RID: 21973
		// (get) Token: 0x06011DEF RID: 73199 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170055D6 RID: 21974
		// (get) Token: 0x06011DF0 RID: 73200 RVA: 0x002F33D6 File Offset: 0x002F15D6
		internal override int ElementTypeId
		{
			get
			{
				return 10390;
			}
		}

		// Token: 0x06011DF1 RID: 73201 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011DF2 RID: 73202 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public NumericValue()
		{
		}

		// Token: 0x06011DF3 RID: 73203 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public NumericValue(string text)
			: base(text)
		{
		}

		// Token: 0x06011DF4 RID: 73204 RVA: 0x002F33E0 File Offset: 0x002F15E0
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06011DF5 RID: 73205 RVA: 0x002F33FB File Offset: 0x002F15FB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NumericValue>(deep);
		}

		// Token: 0x04007D00 RID: 32000
		private const string tagName = "v";

		// Token: 0x04007D01 RID: 32001
		private const byte tagNsId = 11;

		// Token: 0x04007D02 RID: 32002
		internal const int ElementTypeIdConst = 10390;
	}
}
