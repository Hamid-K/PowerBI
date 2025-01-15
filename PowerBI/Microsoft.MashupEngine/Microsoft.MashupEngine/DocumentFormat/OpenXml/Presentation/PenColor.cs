using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A31 RID: 10801
	[GeneratedCode("DomGen", "2.0")]
	internal class PenColor : ColorType
	{
		// Token: 0x170070CF RID: 28879
		// (get) Token: 0x06015B1B RID: 88859 RVA: 0x0032218E File Offset: 0x0032038E
		public override string LocalName
		{
			get
			{
				return "penClr";
			}
		}

		// Token: 0x170070D0 RID: 28880
		// (get) Token: 0x06015B1C RID: 88860 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x170070D1 RID: 28881
		// (get) Token: 0x06015B1D RID: 88861 RVA: 0x00322195 File Offset: 0x00320395
		internal override int ElementTypeId
		{
			get
			{
				return 12339;
			}
		}

		// Token: 0x06015B1E RID: 88862 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015B1F RID: 88863 RVA: 0x00322162 File Offset: 0x00320362
		public PenColor()
		{
		}

		// Token: 0x06015B20 RID: 88864 RVA: 0x0032216A File Offset: 0x0032036A
		public PenColor(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015B21 RID: 88865 RVA: 0x00322173 File Offset: 0x00320373
		public PenColor(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015B22 RID: 88866 RVA: 0x0032217C File Offset: 0x0032037C
		public PenColor(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015B23 RID: 88867 RVA: 0x0032219C File Offset: 0x0032039C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PenColor>(deep);
		}

		// Token: 0x0400946C RID: 37996
		private const string tagName = "penClr";

		// Token: 0x0400946D RID: 37997
		private const byte tagNsId = 24;

		// Token: 0x0400946E RID: 37998
		internal const int ElementTypeIdConst = 12339;
	}
}
