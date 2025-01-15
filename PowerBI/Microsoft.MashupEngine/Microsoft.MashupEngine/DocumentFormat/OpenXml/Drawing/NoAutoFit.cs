using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002739 RID: 10041
	[GeneratedCode("DomGen", "2.0")]
	internal class NoAutoFit : OpenXmlLeafElement
	{
		// Token: 0x1700603F RID: 24639
		// (get) Token: 0x06013513 RID: 79123 RVA: 0x003060B6 File Offset: 0x003042B6
		public override string LocalName
		{
			get
			{
				return "noAutofit";
			}
		}

		// Token: 0x17006040 RID: 24640
		// (get) Token: 0x06013514 RID: 79124 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006041 RID: 24641
		// (get) Token: 0x06013515 RID: 79125 RVA: 0x003060BD File Offset: 0x003042BD
		internal override int ElementTypeId
		{
			get
			{
				return 10099;
			}
		}

		// Token: 0x06013516 RID: 79126 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013518 RID: 79128 RVA: 0x003060C4 File Offset: 0x003042C4
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NoAutoFit>(deep);
		}

		// Token: 0x04008598 RID: 34200
		private const string tagName = "noAutofit";

		// Token: 0x04008599 RID: 34201
		private const byte tagNsId = 10;

		// Token: 0x0400859A RID: 34202
		internal const int ElementTypeIdConst = 10099;
	}
}
