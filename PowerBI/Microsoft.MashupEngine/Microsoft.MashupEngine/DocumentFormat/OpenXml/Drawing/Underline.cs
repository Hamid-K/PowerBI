using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002756 RID: 10070
	[GeneratedCode("DomGen", "2.0")]
	internal class Underline : LinePropertiesType
	{
		// Token: 0x170060B7 RID: 24759
		// (get) Token: 0x06013626 RID: 79398 RVA: 0x00306953 File Offset: 0x00304B53
		public override string LocalName
		{
			get
			{
				return "uLn";
			}
		}

		// Token: 0x170060B8 RID: 24760
		// (get) Token: 0x06013627 RID: 79399 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170060B9 RID: 24761
		// (get) Token: 0x06013628 RID: 79400 RVA: 0x0030695A File Offset: 0x00304B5A
		internal override int ElementTypeId
		{
			get
			{
				return 10114;
			}
		}

		// Token: 0x06013629 RID: 79401 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601362A RID: 79402 RVA: 0x00306961 File Offset: 0x00304B61
		public Underline()
		{
		}

		// Token: 0x0601362B RID: 79403 RVA: 0x00306969 File Offset: 0x00304B69
		public Underline(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601362C RID: 79404 RVA: 0x00306972 File Offset: 0x00304B72
		public Underline(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601362D RID: 79405 RVA: 0x0030697B File Offset: 0x00304B7B
		public Underline(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601362E RID: 79406 RVA: 0x00306984 File Offset: 0x00304B84
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Underline>(deep);
		}

		// Token: 0x040085F8 RID: 34296
		private const string tagName = "uLn";

		// Token: 0x040085F9 RID: 34297
		private const byte tagNsId = 10;

		// Token: 0x040085FA RID: 34298
		internal const int ElementTypeIdConst = 10114;
	}
}
