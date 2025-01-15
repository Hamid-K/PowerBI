using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Bibliography
{
	// Token: 0x020028BB RID: 10427
	[GeneratedCode("DomGen", "2.0")]
	internal class Corporate : OpenXmlLeafTextElement
	{
		// Token: 0x170068D9 RID: 26841
		// (get) Token: 0x060148CC RID: 84172 RVA: 0x003148D0 File Offset: 0x00312AD0
		public override string LocalName
		{
			get
			{
				return "Corporate";
			}
		}

		// Token: 0x170068DA RID: 26842
		// (get) Token: 0x060148CD RID: 84173 RVA: 0x00142610 File Offset: 0x00140810
		internal override byte NamespaceId
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x170068DB RID: 26843
		// (get) Token: 0x060148CE RID: 84174 RVA: 0x003148D7 File Offset: 0x00312AD7
		internal override int ElementTypeId
		{
			get
			{
				return 10764;
			}
		}

		// Token: 0x060148CF RID: 84175 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060148D0 RID: 84176 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public Corporate()
		{
		}

		// Token: 0x060148D1 RID: 84177 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public Corporate(string text)
			: base(text)
		{
		}

		// Token: 0x060148D2 RID: 84178 RVA: 0x003148E0 File Offset: 0x00312AE0
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x060148D3 RID: 84179 RVA: 0x003148FB File Offset: 0x00312AFB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Corporate>(deep);
		}

		// Token: 0x04008EBD RID: 36541
		private const string tagName = "Corporate";

		// Token: 0x04008EBE RID: 36542
		private const byte tagNsId = 9;

		// Token: 0x04008EBF RID: 36543
		internal const int ElementTypeIdConst = 10764;
	}
}
