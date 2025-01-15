using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Bibliography
{
	// Token: 0x020028BC RID: 10428
	[GeneratedCode("DomGen", "2.0")]
	internal class AbbreviatedCaseNumber : OpenXmlLeafTextElement
	{
		// Token: 0x170068DC RID: 26844
		// (get) Token: 0x060148D4 RID: 84180 RVA: 0x00314904 File Offset: 0x00312B04
		public override string LocalName
		{
			get
			{
				return "AbbreviatedCaseNumber";
			}
		}

		// Token: 0x170068DD RID: 26845
		// (get) Token: 0x060148D5 RID: 84181 RVA: 0x00142610 File Offset: 0x00140810
		internal override byte NamespaceId
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x170068DE RID: 26846
		// (get) Token: 0x060148D6 RID: 84182 RVA: 0x0031490B File Offset: 0x00312B0B
		internal override int ElementTypeId
		{
			get
			{
				return 10781;
			}
		}

		// Token: 0x060148D7 RID: 84183 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060148D8 RID: 84184 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public AbbreviatedCaseNumber()
		{
		}

		// Token: 0x060148D9 RID: 84185 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public AbbreviatedCaseNumber(string text)
			: base(text)
		{
		}

		// Token: 0x060148DA RID: 84186 RVA: 0x00314914 File Offset: 0x00312B14
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x060148DB RID: 84187 RVA: 0x0031492F File Offset: 0x00312B2F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AbbreviatedCaseNumber>(deep);
		}

		// Token: 0x04008EC0 RID: 36544
		private const string tagName = "AbbreviatedCaseNumber";

		// Token: 0x04008EC1 RID: 36545
		private const byte tagNsId = 9;

		// Token: 0x04008EC2 RID: 36546
		internal const int ElementTypeIdConst = 10781;
	}
}
