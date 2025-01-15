using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002821 RID: 10273
	[GeneratedCode("DomGen", "2.0")]
	internal class DefaultRunProperties : TextCharacterPropertiesType
	{
		// Token: 0x170065B8 RID: 26040
		// (get) Token: 0x060141CA RID: 82378 RVA: 0x0030F75E File Offset: 0x0030D95E
		public override string LocalName
		{
			get
			{
				return "defRPr";
			}
		}

		// Token: 0x170065B9 RID: 26041
		// (get) Token: 0x060141CB RID: 82379 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170065BA RID: 26042
		// (get) Token: 0x060141CC RID: 82380 RVA: 0x0030F765 File Offset: 0x0030D965
		internal override int ElementTypeId
		{
			get
			{
				return 10315;
			}
		}

		// Token: 0x060141CD RID: 82381 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060141CE RID: 82382 RVA: 0x0030F71B File Offset: 0x0030D91B
		public DefaultRunProperties()
		{
		}

		// Token: 0x060141CF RID: 82383 RVA: 0x0030F723 File Offset: 0x0030D923
		public DefaultRunProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060141D0 RID: 82384 RVA: 0x0030F72C File Offset: 0x0030D92C
		public DefaultRunProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060141D1 RID: 82385 RVA: 0x0030F735 File Offset: 0x0030D935
		public DefaultRunProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060141D2 RID: 82386 RVA: 0x0030F76C File Offset: 0x0030D96C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DefaultRunProperties>(deep);
		}

		// Token: 0x0400890E RID: 35086
		private const string tagName = "defRPr";

		// Token: 0x0400890F RID: 35087
		private const byte tagNsId = 10;

		// Token: 0x04008910 RID: 35088
		internal const int ElementTypeIdConst = 10315;
	}
}
