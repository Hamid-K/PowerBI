using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x02002658 RID: 9816
	[GeneratedCode("DomGen", "2.0")]
	internal class TextEffectColorList : ColorsType
	{
		// Token: 0x17005B66 RID: 23398
		// (get) Token: 0x06012A77 RID: 76407 RVA: 0x002FDAD9 File Offset: 0x002FBCD9
		public override string LocalName
		{
			get
			{
				return "txEffectClrLst";
			}
		}

		// Token: 0x17005B67 RID: 23399
		// (get) Token: 0x06012A78 RID: 76408 RVA: 0x0006808E File Offset: 0x0006628E
		internal override byte NamespaceId
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17005B68 RID: 23400
		// (get) Token: 0x06012A79 RID: 76409 RVA: 0x002FDAE0 File Offset: 0x002FBCE0
		internal override int ElementTypeId
		{
			get
			{
				return 10633;
			}
		}

		// Token: 0x06012A7A RID: 76410 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012A7B RID: 76411 RVA: 0x002FDA51 File Offset: 0x002FBC51
		public TextEffectColorList()
		{
		}

		// Token: 0x06012A7C RID: 76412 RVA: 0x002FDA59 File Offset: 0x002FBC59
		public TextEffectColorList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012A7D RID: 76413 RVA: 0x002FDA62 File Offset: 0x002FBC62
		public TextEffectColorList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012A7E RID: 76414 RVA: 0x002FDA6B File Offset: 0x002FBC6B
		public TextEffectColorList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012A7F RID: 76415 RVA: 0x002FDAE7 File Offset: 0x002FBCE7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TextEffectColorList>(deep);
		}

		// Token: 0x04008112 RID: 33042
		private const string tagName = "txEffectClrLst";

		// Token: 0x04008113 RID: 33043
		private const byte tagNsId = 14;

		// Token: 0x04008114 RID: 33044
		internal const int ElementTypeIdConst = 10633;
	}
}
