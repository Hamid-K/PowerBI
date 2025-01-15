using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002507 RID: 9479
	[GeneratedCode("DomGen", "2.0")]
	internal class RichText : TextBodyType
	{
		// Token: 0x1700542B RID: 21547
		// (get) Token: 0x06011A4D RID: 72269 RVA: 0x002F112A File Offset: 0x002EF32A
		public override string LocalName
		{
			get
			{
				return "rich";
			}
		}

		// Token: 0x1700542C RID: 21548
		// (get) Token: 0x06011A4E RID: 72270 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x1700542D RID: 21549
		// (get) Token: 0x06011A4F RID: 72271 RVA: 0x002F1131 File Offset: 0x002EF331
		internal override int ElementTypeId
		{
			get
			{
				return 10563;
			}
		}

		// Token: 0x06011A50 RID: 72272 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011A51 RID: 72273 RVA: 0x002F10FE File Offset: 0x002EF2FE
		public RichText()
		{
		}

		// Token: 0x06011A52 RID: 72274 RVA: 0x002F1106 File Offset: 0x002EF306
		public RichText(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011A53 RID: 72275 RVA: 0x002F110F File Offset: 0x002EF30F
		public RichText(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011A54 RID: 72276 RVA: 0x002F1118 File Offset: 0x002EF318
		public RichText(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06011A55 RID: 72277 RVA: 0x002F1138 File Offset: 0x002EF338
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RichText>(deep);
		}

		// Token: 0x04007BA8 RID: 31656
		private const string tagName = "rich";

		// Token: 0x04007BA9 RID: 31657
		private const byte tagNsId = 11;

		// Token: 0x04007BAA RID: 31658
		internal const int ElementTypeIdConst = 10563;
	}
}
