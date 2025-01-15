using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A30 RID: 10800
	[GeneratedCode("DomGen", "2.0")]
	internal class ColorValue : ColorType
	{
		// Token: 0x170070CC RID: 28876
		// (get) Token: 0x06015B12 RID: 88850 RVA: 0x00322154 File Offset: 0x00320354
		public override string LocalName
		{
			get
			{
				return "clrVal";
			}
		}

		// Token: 0x170070CD RID: 28877
		// (get) Token: 0x06015B13 RID: 88851 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x170070CE RID: 28878
		// (get) Token: 0x06015B14 RID: 88852 RVA: 0x0032215B File Offset: 0x0032035B
		internal override int ElementTypeId
		{
			get
			{
				return 12221;
			}
		}

		// Token: 0x06015B15 RID: 88853 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015B16 RID: 88854 RVA: 0x00322162 File Offset: 0x00320362
		public ColorValue()
		{
		}

		// Token: 0x06015B17 RID: 88855 RVA: 0x0032216A File Offset: 0x0032036A
		public ColorValue(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015B18 RID: 88856 RVA: 0x00322173 File Offset: 0x00320373
		public ColorValue(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015B19 RID: 88857 RVA: 0x0032217C File Offset: 0x0032037C
		public ColorValue(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015B1A RID: 88858 RVA: 0x00322185 File Offset: 0x00320385
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ColorValue>(deep);
		}

		// Token: 0x04009469 RID: 37993
		private const string tagName = "clrVal";

		// Token: 0x0400946A RID: 37994
		private const byte tagNsId = 24;

		// Token: 0x0400946B RID: 37995
		internal const int ElementTypeIdConst = 12221;
	}
}
