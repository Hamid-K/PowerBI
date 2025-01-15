using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200281F RID: 10271
	[GeneratedCode("DomGen", "2.0")]
	internal class EndParagraphRunProperties : TextCharacterPropertiesType
	{
		// Token: 0x170065B2 RID: 26034
		// (get) Token: 0x060141B8 RID: 82360 RVA: 0x0030F70D File Offset: 0x0030D90D
		public override string LocalName
		{
			get
			{
				return "endParaRPr";
			}
		}

		// Token: 0x170065B3 RID: 26035
		// (get) Token: 0x060141B9 RID: 82361 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170065B4 RID: 26036
		// (get) Token: 0x060141BA RID: 82362 RVA: 0x0030F714 File Offset: 0x0030D914
		internal override int ElementTypeId
		{
			get
			{
				return 10295;
			}
		}

		// Token: 0x060141BB RID: 82363 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060141BC RID: 82364 RVA: 0x0030F71B File Offset: 0x0030D91B
		public EndParagraphRunProperties()
		{
		}

		// Token: 0x060141BD RID: 82365 RVA: 0x0030F723 File Offset: 0x0030D923
		public EndParagraphRunProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060141BE RID: 82366 RVA: 0x0030F72C File Offset: 0x0030D92C
		public EndParagraphRunProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060141BF RID: 82367 RVA: 0x0030F735 File Offset: 0x0030D935
		public EndParagraphRunProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060141C0 RID: 82368 RVA: 0x0030F73E File Offset: 0x0030D93E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<EndParagraphRunProperties>(deep);
		}

		// Token: 0x04008908 RID: 35080
		private const string tagName = "endParaRPr";

		// Token: 0x04008909 RID: 35081
		private const byte tagNsId = 10;

		// Token: 0x0400890A RID: 35082
		internal const int ElementTypeIdConst = 10295;
	}
}
