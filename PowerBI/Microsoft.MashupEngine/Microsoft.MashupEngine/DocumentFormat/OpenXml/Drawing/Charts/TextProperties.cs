using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002506 RID: 9478
	[GeneratedCode("DomGen", "2.0")]
	internal class TextProperties : TextBodyType
	{
		// Token: 0x17005428 RID: 21544
		// (get) Token: 0x06011A44 RID: 72260 RVA: 0x002F10F0 File Offset: 0x002EF2F0
		public override string LocalName
		{
			get
			{
				return "txPr";
			}
		}

		// Token: 0x17005429 RID: 21545
		// (get) Token: 0x06011A45 RID: 72261 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x1700542A RID: 21546
		// (get) Token: 0x06011A46 RID: 72262 RVA: 0x002F10F7 File Offset: 0x002EF2F7
		internal override int ElementTypeId
		{
			get
			{
				return 10344;
			}
		}

		// Token: 0x06011A47 RID: 72263 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011A48 RID: 72264 RVA: 0x002F10FE File Offset: 0x002EF2FE
		public TextProperties()
		{
		}

		// Token: 0x06011A49 RID: 72265 RVA: 0x002F1106 File Offset: 0x002EF306
		public TextProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011A4A RID: 72266 RVA: 0x002F110F File Offset: 0x002EF30F
		public TextProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011A4B RID: 72267 RVA: 0x002F1118 File Offset: 0x002EF318
		public TextProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06011A4C RID: 72268 RVA: 0x002F1121 File Offset: 0x002EF321
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TextProperties>(deep);
		}

		// Token: 0x04007BA5 RID: 31653
		private const string tagName = "txPr";

		// Token: 0x04007BA6 RID: 31654
		private const byte tagNsId = 11;

		// Token: 0x04007BA7 RID: 31655
		internal const int ElementTypeIdConst = 10344;
	}
}
