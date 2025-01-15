using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E39 RID: 11833
	[GeneratedCode("DomGen", "2.0")]
	internal class StartOverrideNumberingValue : DecimalNumberType
	{
		// Token: 0x17008997 RID: 35223
		// (get) Token: 0x060191F7 RID: 102903 RVA: 0x003468BF File Offset: 0x00344ABF
		public override string LocalName
		{
			get
			{
				return "startOverride";
			}
		}

		// Token: 0x17008998 RID: 35224
		// (get) Token: 0x060191F8 RID: 102904 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008999 RID: 35225
		// (get) Token: 0x060191F9 RID: 102905 RVA: 0x003468C6 File Offset: 0x00344AC6
		internal override int ElementTypeId
		{
			get
			{
				return 11881;
			}
		}

		// Token: 0x060191FA RID: 102906 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060191FC RID: 102908 RVA: 0x003468CD File Offset: 0x00344ACD
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<StartOverrideNumberingValue>(deep);
		}

		// Token: 0x0400A729 RID: 42793
		private const string tagName = "startOverride";

		// Token: 0x0400A72A RID: 42794
		private const byte tagNsId = 23;

		// Token: 0x0400A72B RID: 42795
		internal const int ElementTypeIdConst = 11881;
	}
}
