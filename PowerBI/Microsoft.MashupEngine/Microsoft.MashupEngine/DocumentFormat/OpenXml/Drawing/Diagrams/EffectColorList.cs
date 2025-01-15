using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x02002655 RID: 9813
	[GeneratedCode("DomGen", "2.0")]
	internal class EffectColorList : ColorsType
	{
		// Token: 0x17005B5D RID: 23389
		// (get) Token: 0x06012A5C RID: 76380 RVA: 0x002FDA94 File Offset: 0x002FBC94
		public override string LocalName
		{
			get
			{
				return "effectClrLst";
			}
		}

		// Token: 0x17005B5E RID: 23390
		// (get) Token: 0x06012A5D RID: 76381 RVA: 0x0006808E File Offset: 0x0006628E
		internal override byte NamespaceId
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17005B5F RID: 23391
		// (get) Token: 0x06012A5E RID: 76382 RVA: 0x002FDA9B File Offset: 0x002FBC9B
		internal override int ElementTypeId
		{
			get
			{
				return 10630;
			}
		}

		// Token: 0x06012A5F RID: 76383 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012A60 RID: 76384 RVA: 0x002FDA51 File Offset: 0x002FBC51
		public EffectColorList()
		{
		}

		// Token: 0x06012A61 RID: 76385 RVA: 0x002FDA59 File Offset: 0x002FBC59
		public EffectColorList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012A62 RID: 76386 RVA: 0x002FDA62 File Offset: 0x002FBC62
		public EffectColorList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012A63 RID: 76387 RVA: 0x002FDA6B File Offset: 0x002FBC6B
		public EffectColorList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012A64 RID: 76388 RVA: 0x002FDAA2 File Offset: 0x002FBCA2
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<EffectColorList>(deep);
		}

		// Token: 0x04008109 RID: 33033
		private const string tagName = "effectClrLst";

		// Token: 0x0400810A RID: 33034
		private const byte tagNsId = 14;

		// Token: 0x0400810B RID: 33035
		internal const int ElementTypeIdConst = 10630;
	}
}
