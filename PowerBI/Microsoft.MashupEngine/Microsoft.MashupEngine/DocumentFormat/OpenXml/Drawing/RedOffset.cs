using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020026E8 RID: 9960
	[GeneratedCode("DomGen", "2.0")]
	internal class RedOffset : PercentageType
	{
		// Token: 0x17005DE0 RID: 24032
		// (get) Token: 0x06012FEC RID: 77804 RVA: 0x003018CE File Offset: 0x002FFACE
		public override string LocalName
		{
			get
			{
				return "redOff";
			}
		}

		// Token: 0x17005DE1 RID: 24033
		// (get) Token: 0x06012FED RID: 77805 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005DE2 RID: 24034
		// (get) Token: 0x06012FEE RID: 77806 RVA: 0x003018D5 File Offset: 0x002FFAD5
		internal override int ElementTypeId
		{
			get
			{
				return 10024;
			}
		}

		// Token: 0x06012FEF RID: 77807 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012FF1 RID: 77809 RVA: 0x003018DC File Offset: 0x002FFADC
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RedOffset>(deep);
		}

		// Token: 0x04008422 RID: 33826
		private const string tagName = "redOff";

		// Token: 0x04008423 RID: 33827
		private const byte tagNsId = 10;

		// Token: 0x04008424 RID: 33828
		internal const int ElementTypeIdConst = 10024;
	}
}
