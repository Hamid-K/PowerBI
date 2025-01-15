using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Bibliography
{
	// Token: 0x020028C7 RID: 10439
	[GeneratedCode("DomGen", "2.0")]
	internal class Court : OpenXmlLeafTextElement
	{
		// Token: 0x170068FD RID: 26877
		// (get) Token: 0x0601492C RID: 84268 RVA: 0x00314B40 File Offset: 0x00312D40
		public override string LocalName
		{
			get
			{
				return "Court";
			}
		}

		// Token: 0x170068FE RID: 26878
		// (get) Token: 0x0601492D RID: 84269 RVA: 0x00142610 File Offset: 0x00140810
		internal override byte NamespaceId
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x170068FF RID: 26879
		// (get) Token: 0x0601492E RID: 84270 RVA: 0x00314B47 File Offset: 0x00312D47
		internal override int ElementTypeId
		{
			get
			{
				return 10793;
			}
		}

		// Token: 0x0601492F RID: 84271 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014930 RID: 84272 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public Court()
		{
		}

		// Token: 0x06014931 RID: 84273 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public Court(string text)
			: base(text)
		{
		}

		// Token: 0x06014932 RID: 84274 RVA: 0x00314B50 File Offset: 0x00312D50
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06014933 RID: 84275 RVA: 0x00314B6B File Offset: 0x00312D6B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Court>(deep);
		}

		// Token: 0x04008EE1 RID: 36577
		private const string tagName = "Court";

		// Token: 0x04008EE2 RID: 36578
		private const byte tagNsId = 9;

		// Token: 0x04008EE3 RID: 36579
		internal const int ElementTypeIdConst = 10793;
	}
}
