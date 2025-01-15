using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002757 RID: 10071
	[GeneratedCode("DomGen", "2.0")]
	internal class Outline : LinePropertiesType
	{
		// Token: 0x170060BA RID: 24762
		// (get) Token: 0x0601362F RID: 79407 RVA: 0x0030698D File Offset: 0x00304B8D
		public override string LocalName
		{
			get
			{
				return "ln";
			}
		}

		// Token: 0x170060BB RID: 24763
		// (get) Token: 0x06013630 RID: 79408 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170060BC RID: 24764
		// (get) Token: 0x06013631 RID: 79409 RVA: 0x00306994 File Offset: 0x00304B94
		internal override int ElementTypeId
		{
			get
			{
				return 10138;
			}
		}

		// Token: 0x06013632 RID: 79410 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013633 RID: 79411 RVA: 0x00306961 File Offset: 0x00304B61
		public Outline()
		{
		}

		// Token: 0x06013634 RID: 79412 RVA: 0x00306969 File Offset: 0x00304B69
		public Outline(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013635 RID: 79413 RVA: 0x00306972 File Offset: 0x00304B72
		public Outline(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013636 RID: 79414 RVA: 0x0030697B File Offset: 0x00304B7B
		public Outline(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013637 RID: 79415 RVA: 0x0030699B File Offset: 0x00304B9B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Outline>(deep);
		}

		// Token: 0x040085FB RID: 34299
		private const string tagName = "ln";

		// Token: 0x040085FC RID: 34300
		private const byte tagNsId = 10;

		// Token: 0x040085FD RID: 34301
		internal const int ElementTypeIdConst = 10138;
	}
}
