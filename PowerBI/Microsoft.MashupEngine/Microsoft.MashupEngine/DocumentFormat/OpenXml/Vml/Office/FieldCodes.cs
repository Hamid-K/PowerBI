using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Office
{
	// Token: 0x0200221D RID: 8733
	[GeneratedCode("DomGen", "2.0")]
	internal class FieldCodes : OpenXmlLeafTextElement
	{
		// Token: 0x17003938 RID: 14648
		// (get) Token: 0x0600E01D RID: 57373 RVA: 0x002BFA08 File Offset: 0x002BDC08
		public override string LocalName
		{
			get
			{
				return "FieldCodes";
			}
		}

		// Token: 0x17003939 RID: 14649
		// (get) Token: 0x0600E01E RID: 57374 RVA: 0x0012AF09 File Offset: 0x00129109
		internal override byte NamespaceId
		{
			get
			{
				return 27;
			}
		}

		// Token: 0x1700393A RID: 14650
		// (get) Token: 0x0600E01F RID: 57375 RVA: 0x002BFA0F File Offset: 0x002BDC0F
		internal override int ElementTypeId
		{
			get
			{
				return 12426;
			}
		}

		// Token: 0x0600E020 RID: 57376 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600E021 RID: 57377 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public FieldCodes()
		{
		}

		// Token: 0x0600E022 RID: 57378 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public FieldCodes(string text)
			: base(text)
		{
		}

		// Token: 0x0600E023 RID: 57379 RVA: 0x002BFA18 File Offset: 0x002BDC18
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x0600E024 RID: 57380 RVA: 0x002BFA33 File Offset: 0x002BDC33
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FieldCodes>(deep);
		}

		// Token: 0x04006DD1 RID: 28113
		private const string tagName = "FieldCodes";

		// Token: 0x04006DD2 RID: 28114
		private const byte tagNsId = 27;

		// Token: 0x04006DD3 RID: 28115
		internal const int ElementTypeIdConst = 12426;
	}
}
