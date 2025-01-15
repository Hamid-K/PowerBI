using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E10 RID: 11792
	[GeneratedCode("DomGen", "2.0")]
	internal class UseWord2002TableStyleRules : OnOffType
	{
		// Token: 0x170088CC RID: 35020
		// (get) Token: 0x06019058 RID: 102488 RVA: 0x0034587D File Offset: 0x00343A7D
		public override string LocalName
		{
			get
			{
				return "useWord2002TableStyleRules";
			}
		}

		// Token: 0x170088CD RID: 35021
		// (get) Token: 0x06019059 RID: 102489 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170088CE RID: 35022
		// (get) Token: 0x0601905A RID: 102490 RVA: 0x00345884 File Offset: 0x00343A84
		internal override int ElementTypeId
		{
			get
			{
				return 12102;
			}
		}

		// Token: 0x0601905B RID: 102491 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601905D RID: 102493 RVA: 0x0034588B File Offset: 0x00343A8B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<UseWord2002TableStyleRules>(deep);
		}

		// Token: 0x0400A69B RID: 42651
		private const string tagName = "useWord2002TableStyleRules";

		// Token: 0x0400A69C RID: 42652
		private const byte tagNsId = 23;

		// Token: 0x0400A69D RID: 42653
		internal const int ElementTypeIdConst = 12102;
	}
}
