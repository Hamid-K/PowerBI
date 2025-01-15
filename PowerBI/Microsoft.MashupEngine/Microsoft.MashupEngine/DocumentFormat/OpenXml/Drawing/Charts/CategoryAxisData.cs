using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025D4 RID: 9684
	[GeneratedCode("DomGen", "2.0")]
	internal class CategoryAxisData : AxisDataSourceType
	{
		// Token: 0x1700582C RID: 22572
		// (get) Token: 0x0601232F RID: 74543 RVA: 0x002F7174 File Offset: 0x002F5374
		public override string LocalName
		{
			get
			{
				return "cat";
			}
		}

		// Token: 0x1700582D RID: 22573
		// (get) Token: 0x06012330 RID: 74544 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x1700582E RID: 22574
		// (get) Token: 0x06012331 RID: 74545 RVA: 0x002F717B File Offset: 0x002F537B
		internal override int ElementTypeId
		{
			get
			{
				return 10524;
			}
		}

		// Token: 0x06012332 RID: 74546 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012333 RID: 74547 RVA: 0x002F7182 File Offset: 0x002F5382
		public CategoryAxisData()
		{
		}

		// Token: 0x06012334 RID: 74548 RVA: 0x002F718A File Offset: 0x002F538A
		public CategoryAxisData(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012335 RID: 74549 RVA: 0x002F7193 File Offset: 0x002F5393
		public CategoryAxisData(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012336 RID: 74550 RVA: 0x002F719C File Offset: 0x002F539C
		public CategoryAxisData(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012337 RID: 74551 RVA: 0x002F71A5 File Offset: 0x002F53A5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CategoryAxisData>(deep);
		}

		// Token: 0x04007E95 RID: 32405
		private const string tagName = "cat";

		// Token: 0x04007E96 RID: 32406
		private const byte tagNsId = 11;

		// Token: 0x04007E97 RID: 32407
		internal const int ElementTypeIdConst = 10524;
	}
}
