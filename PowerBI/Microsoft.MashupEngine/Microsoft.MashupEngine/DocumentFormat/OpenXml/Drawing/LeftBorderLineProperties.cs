using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002758 RID: 10072
	[GeneratedCode("DomGen", "2.0")]
	internal class LeftBorderLineProperties : LinePropertiesType
	{
		// Token: 0x170060BD RID: 24765
		// (get) Token: 0x06013638 RID: 79416 RVA: 0x003069A4 File Offset: 0x00304BA4
		public override string LocalName
		{
			get
			{
				return "lnL";
			}
		}

		// Token: 0x170060BE RID: 24766
		// (get) Token: 0x06013639 RID: 79417 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170060BF RID: 24767
		// (get) Token: 0x0601363A RID: 79418 RVA: 0x003069AB File Offset: 0x00304BAB
		internal override int ElementTypeId
		{
			get
			{
				return 10252;
			}
		}

		// Token: 0x0601363B RID: 79419 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601363C RID: 79420 RVA: 0x00306961 File Offset: 0x00304B61
		public LeftBorderLineProperties()
		{
		}

		// Token: 0x0601363D RID: 79421 RVA: 0x00306969 File Offset: 0x00304B69
		public LeftBorderLineProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601363E RID: 79422 RVA: 0x00306972 File Offset: 0x00304B72
		public LeftBorderLineProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601363F RID: 79423 RVA: 0x0030697B File Offset: 0x00304B7B
		public LeftBorderLineProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013640 RID: 79424 RVA: 0x003069B2 File Offset: 0x00304BB2
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LeftBorderLineProperties>(deep);
		}

		// Token: 0x040085FE RID: 34302
		private const string tagName = "lnL";

		// Token: 0x040085FF RID: 34303
		private const byte tagNsId = 10;

		// Token: 0x04008600 RID: 34304
		internal const int ElementTypeIdConst = 10252;
	}
}
