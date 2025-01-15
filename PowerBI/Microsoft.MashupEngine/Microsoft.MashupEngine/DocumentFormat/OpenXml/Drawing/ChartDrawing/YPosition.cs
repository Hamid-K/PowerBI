using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.ChartDrawing
{
	// Token: 0x0200263E RID: 9790
	[GeneratedCode("DomGen", "2.0")]
	internal class YPosition : OpenXmlLeafTextElement
	{
		// Token: 0x17005ACF RID: 23247
		// (get) Token: 0x060128F5 RID: 76021 RVA: 0x002F2F05 File Offset: 0x002F1105
		public override string LocalName
		{
			get
			{
				return "y";
			}
		}

		// Token: 0x17005AD0 RID: 23248
		// (get) Token: 0x060128F6 RID: 76022 RVA: 0x001422C0 File Offset: 0x001404C0
		internal override byte NamespaceId
		{
			get
			{
				return 12;
			}
		}

		// Token: 0x17005AD1 RID: 23249
		// (get) Token: 0x060128F7 RID: 76023 RVA: 0x002FC974 File Offset: 0x002FAB74
		internal override int ElementTypeId
		{
			get
			{
				return 10609;
			}
		}

		// Token: 0x060128F8 RID: 76024 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060128F9 RID: 76025 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public YPosition()
		{
		}

		// Token: 0x060128FA RID: 76026 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public YPosition(string text)
			: base(text)
		{
		}

		// Token: 0x060128FB RID: 76027 RVA: 0x002FC97C File Offset: 0x002FAB7C
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new DoubleValue
			{
				InnerText = text
			};
		}

		// Token: 0x060128FC RID: 76028 RVA: 0x002FC997 File Offset: 0x002FAB97
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<YPosition>(deep);
		}

		// Token: 0x040080AA RID: 32938
		private const string tagName = "y";

		// Token: 0x040080AB RID: 32939
		private const byte tagNsId = 12;

		// Token: 0x040080AC RID: 32940
		internal const int ElementTypeIdConst = 10609;
	}
}
