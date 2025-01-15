using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x02002980 RID: 10624
	[GeneratedCode("DomGen", "2.0")]
	internal class ZeroDescent : OnOffType
	{
		// Token: 0x17006C8B RID: 27787
		// (get) Token: 0x060151A4 RID: 86436 RVA: 0x0031B5E5 File Offset: 0x003197E5
		public override string LocalName
		{
			get
			{
				return "zeroDesc";
			}
		}

		// Token: 0x17006C8C RID: 27788
		// (get) Token: 0x060151A5 RID: 86437 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006C8D RID: 27789
		// (get) Token: 0x060151A6 RID: 86438 RVA: 0x0031B5EC File Offset: 0x003197EC
		internal override int ElementTypeId
		{
			get
			{
				return 10932;
			}
		}

		// Token: 0x060151A7 RID: 86439 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060151A9 RID: 86441 RVA: 0x0031B5F3 File Offset: 0x003197F3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ZeroDescent>(deep);
		}

		// Token: 0x0400918C RID: 37260
		private const string tagName = "zeroDesc";

		// Token: 0x0400918D RID: 37261
		private const byte tagNsId = 21;

		// Token: 0x0400918E RID: 37262
		internal const int ElementTypeIdConst = 10932;
	}
}
