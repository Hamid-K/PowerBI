using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Spreadsheet
{
	// Token: 0x020021ED RID: 8685
	[GeneratedCode("DomGen", "2.0")]
	internal class AcceleratorSecondary : OpenXmlLeafTextElement
	{
		// Token: 0x170037D4 RID: 14292
		// (get) Token: 0x0600DD1A RID: 56602 RVA: 0x002BD0CC File Offset: 0x002BB2CC
		public override string LocalName
		{
			get
			{
				return "Accel2";
			}
		}

		// Token: 0x170037D5 RID: 14293
		// (get) Token: 0x0600DD1B RID: 56603 RVA: 0x002BBFB1 File Offset: 0x002BA1B1
		internal override byte NamespaceId
		{
			get
			{
				return 29;
			}
		}

		// Token: 0x170037D6 RID: 14294
		// (get) Token: 0x0600DD1C RID: 56604 RVA: 0x002BD0D3 File Offset: 0x002BB2D3
		internal override int ElementTypeId
		{
			get
			{
				return 12458;
			}
		}

		// Token: 0x0600DD1D RID: 56605 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600DD1E RID: 56606 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public AcceleratorSecondary()
		{
		}

		// Token: 0x0600DD1F RID: 56607 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public AcceleratorSecondary(string text)
			: base(text)
		{
		}

		// Token: 0x0600DD20 RID: 56608 RVA: 0x002BD0DC File Offset: 0x002BB2DC
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new ByteValue
			{
				InnerText = text
			};
		}

		// Token: 0x0600DD21 RID: 56609 RVA: 0x002BD0F7 File Offset: 0x002BB2F7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AcceleratorSecondary>(deep);
		}

		// Token: 0x04006CFB RID: 27899
		private const string tagName = "Accel2";

		// Token: 0x04006CFC RID: 27900
		private const byte tagNsId = 29;

		// Token: 0x04006CFD RID: 27901
		internal const int ElementTypeIdConst = 12458;
	}
}
