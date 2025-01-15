using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Spreadsheet
{
	// Token: 0x020021D6 RID: 8662
	[GeneratedCode("DomGen", "2.0")]
	internal class MapOcxControl : OpenXmlLeafTextElement
	{
		// Token: 0x1700378F RID: 14223
		// (get) Token: 0x0600DC62 RID: 56418 RVA: 0x002BCC20 File Offset: 0x002BAE20
		public override string LocalName
		{
			get
			{
				return "MapOCX";
			}
		}

		// Token: 0x17003790 RID: 14224
		// (get) Token: 0x0600DC63 RID: 56419 RVA: 0x002BBFB1 File Offset: 0x002BA1B1
		internal override byte NamespaceId
		{
			get
			{
				return 29;
			}
		}

		// Token: 0x17003791 RID: 14225
		// (get) Token: 0x0600DC64 RID: 56420 RVA: 0x002BCC27 File Offset: 0x002BAE27
		internal override int ElementTypeId
		{
			get
			{
				return 12492;
			}
		}

		// Token: 0x0600DC65 RID: 56421 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600DC66 RID: 56422 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public MapOcxControl()
		{
		}

		// Token: 0x0600DC67 RID: 56423 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public MapOcxControl(string text)
			: base(text)
		{
		}

		// Token: 0x0600DC68 RID: 56424 RVA: 0x002BCC30 File Offset: 0x002BAE30
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new EnumValue<BooleanEntryWithBlankValues>
			{
				InnerText = text
			};
		}

		// Token: 0x0600DC69 RID: 56425 RVA: 0x002BCC4B File Offset: 0x002BAE4B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MapOcxControl>(deep);
		}

		// Token: 0x04006CB6 RID: 27830
		private const string tagName = "MapOCX";

		// Token: 0x04006CB7 RID: 27831
		private const byte tagNsId = 29;

		// Token: 0x04006CB8 RID: 27832
		internal const int ElementTypeIdConst = 12492;
	}
}
