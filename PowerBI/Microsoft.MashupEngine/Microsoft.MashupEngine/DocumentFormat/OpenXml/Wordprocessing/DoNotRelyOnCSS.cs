using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DA5 RID: 11685
	[GeneratedCode("DomGen", "2.0")]
	internal class DoNotRelyOnCSS : OnOffType
	{
		// Token: 0x1700878B RID: 34699
		// (get) Token: 0x06018DD6 RID: 101846 RVA: 0x00344EE0 File Offset: 0x003430E0
		public override string LocalName
		{
			get
			{
				return "doNotRelyOnCSS";
			}
		}

		// Token: 0x1700878C RID: 34700
		// (get) Token: 0x06018DD7 RID: 101847 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700878D RID: 34701
		// (get) Token: 0x06018DD8 RID: 101848 RVA: 0x00344EE7 File Offset: 0x003430E7
		internal override int ElementTypeId
		{
			get
			{
				return 11841;
			}
		}

		// Token: 0x06018DD9 RID: 101849 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018DDB RID: 101851 RVA: 0x00344EEE File Offset: 0x003430EE
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DoNotRelyOnCSS>(deep);
		}

		// Token: 0x0400A55A RID: 42330
		private const string tagName = "doNotRelyOnCSS";

		// Token: 0x0400A55B RID: 42331
		private const byte tagNsId = 23;

		// Token: 0x0400A55C RID: 42332
		internal const int ElementTypeIdConst = 11841;
	}
}
