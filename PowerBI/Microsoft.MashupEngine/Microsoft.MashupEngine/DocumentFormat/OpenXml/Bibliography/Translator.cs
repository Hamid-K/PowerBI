using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Bibliography
{
	// Token: 0x020028FC RID: 10492
	[GeneratedCode("DomGen", "2.0")]
	internal class Translator : NameType
	{
		// Token: 0x1700699D RID: 27037
		// (get) Token: 0x06014AE5 RID: 84709 RVA: 0x003154FF File Offset: 0x003136FF
		public override string LocalName
		{
			get
			{
				return "Translator";
			}
		}

		// Token: 0x1700699E RID: 27038
		// (get) Token: 0x06014AE6 RID: 84710 RVA: 0x00142610 File Offset: 0x00140810
		internal override byte NamespaceId
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x1700699F RID: 27039
		// (get) Token: 0x06014AE7 RID: 84711 RVA: 0x00315506 File Offset: 0x00313706
		internal override int ElementTypeId
		{
			get
			{
				return 10779;
			}
		}

		// Token: 0x06014AE8 RID: 84712 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014AE9 RID: 84713 RVA: 0x003153D6 File Offset: 0x003135D6
		public Translator()
		{
		}

		// Token: 0x06014AEA RID: 84714 RVA: 0x003153DE File Offset: 0x003135DE
		public Translator(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014AEB RID: 84715 RVA: 0x003153E7 File Offset: 0x003135E7
		public Translator(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014AEC RID: 84716 RVA: 0x003153F0 File Offset: 0x003135F0
		public Translator(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014AED RID: 84717 RVA: 0x0031550D File Offset: 0x0031370D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Translator>(deep);
		}

		// Token: 0x04008F7F RID: 36735
		private const string tagName = "Translator";

		// Token: 0x04008F80 RID: 36736
		private const byte tagNsId = 9;

		// Token: 0x04008F81 RID: 36737
		internal const int ElementTypeIdConst = 10779;
	}
}
