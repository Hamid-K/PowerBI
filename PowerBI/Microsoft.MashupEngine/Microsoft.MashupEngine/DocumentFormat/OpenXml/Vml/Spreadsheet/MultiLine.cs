using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Spreadsheet
{
	// Token: 0x020021CE RID: 8654
	[GeneratedCode("DomGen", "2.0")]
	internal class MultiLine : OpenXmlLeafTextElement
	{
		// Token: 0x17003777 RID: 14199
		// (get) Token: 0x0600DC22 RID: 56354 RVA: 0x002BCA80 File Offset: 0x002BAC80
		public override string LocalName
		{
			get
			{
				return "MultiLine";
			}
		}

		// Token: 0x17003778 RID: 14200
		// (get) Token: 0x0600DC23 RID: 56355 RVA: 0x002BBFB1 File Offset: 0x002BA1B1
		internal override byte NamespaceId
		{
			get
			{
				return 29;
			}
		}

		// Token: 0x17003779 RID: 14201
		// (get) Token: 0x0600DC24 RID: 56356 RVA: 0x002BCA87 File Offset: 0x002BAC87
		internal override int ElementTypeId
		{
			get
			{
				return 12465;
			}
		}

		// Token: 0x0600DC25 RID: 56357 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600DC26 RID: 56358 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public MultiLine()
		{
		}

		// Token: 0x0600DC27 RID: 56359 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public MultiLine(string text)
			: base(text)
		{
		}

		// Token: 0x0600DC28 RID: 56360 RVA: 0x002BCA90 File Offset: 0x002BAC90
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new EnumValue<BooleanEntryWithBlankValues>
			{
				InnerText = text
			};
		}

		// Token: 0x0600DC29 RID: 56361 RVA: 0x002BCAAB File Offset: 0x002BACAB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MultiLine>(deep);
		}

		// Token: 0x04006C9E RID: 27806
		private const string tagName = "MultiLine";

		// Token: 0x04006C9F RID: 27807
		private const byte tagNsId = 29;

		// Token: 0x04006CA0 RID: 27808
		internal const int ElementTypeIdConst = 12465;
	}
}
