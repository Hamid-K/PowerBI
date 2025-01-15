using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.ChartDrawing
{
	// Token: 0x0200263D RID: 9789
	[GeneratedCode("DomGen", "2.0")]
	internal class XPosition : OpenXmlLeafTextElement
	{
		// Token: 0x17005ACC RID: 23244
		// (get) Token: 0x060128ED RID: 76013 RVA: 0x002F2EEE File Offset: 0x002F10EE
		public override string LocalName
		{
			get
			{
				return "x";
			}
		}

		// Token: 0x17005ACD RID: 23245
		// (get) Token: 0x060128EE RID: 76014 RVA: 0x001422C0 File Offset: 0x001404C0
		internal override byte NamespaceId
		{
			get
			{
				return 12;
			}
		}

		// Token: 0x17005ACE RID: 23246
		// (get) Token: 0x060128EF RID: 76015 RVA: 0x002FC949 File Offset: 0x002FAB49
		internal override int ElementTypeId
		{
			get
			{
				return 10608;
			}
		}

		// Token: 0x060128F0 RID: 76016 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060128F1 RID: 76017 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public XPosition()
		{
		}

		// Token: 0x060128F2 RID: 76018 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public XPosition(string text)
			: base(text)
		{
		}

		// Token: 0x060128F3 RID: 76019 RVA: 0x002FC950 File Offset: 0x002FAB50
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new DoubleValue
			{
				InnerText = text
			};
		}

		// Token: 0x060128F4 RID: 76020 RVA: 0x002FC96B File Offset: 0x002FAB6B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<XPosition>(deep);
		}

		// Token: 0x040080A7 RID: 32935
		private const string tagName = "x";

		// Token: 0x040080A8 RID: 32936
		private const byte tagNsId = 12;

		// Token: 0x040080A9 RID: 32937
		internal const int ElementTypeIdConst = 10608;
	}
}
