using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020026E7 RID: 9959
	[GeneratedCode("DomGen", "2.0")]
	internal class Red : PercentageType
	{
		// Token: 0x17005DDD RID: 24029
		// (get) Token: 0x06012FE6 RID: 77798 RVA: 0x003018B7 File Offset: 0x002FFAB7
		public override string LocalName
		{
			get
			{
				return "red";
			}
		}

		// Token: 0x17005DDE RID: 24030
		// (get) Token: 0x06012FE7 RID: 77799 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005DDF RID: 24031
		// (get) Token: 0x06012FE8 RID: 77800 RVA: 0x003018BE File Offset: 0x002FFABE
		internal override int ElementTypeId
		{
			get
			{
				return 10023;
			}
		}

		// Token: 0x06012FE9 RID: 77801 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012FEB RID: 77803 RVA: 0x003018C5 File Offset: 0x002FFAC5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Red>(deep);
		}

		// Token: 0x0400841F RID: 33823
		private const string tagName = "red";

		// Token: 0x04008420 RID: 33824
		private const byte tagNsId = 10;

		// Token: 0x04008421 RID: 33825
		internal const int ElementTypeIdConst = 10023;
	}
}
