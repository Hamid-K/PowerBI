using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020026E6 RID: 9958
	[GeneratedCode("DomGen", "2.0")]
	internal class LuminanceModulation : PercentageType
	{
		// Token: 0x17005DDA RID: 24026
		// (get) Token: 0x06012FE0 RID: 77792 RVA: 0x002ECADF File Offset: 0x002EACDF
		public override string LocalName
		{
			get
			{
				return "lumMod";
			}
		}

		// Token: 0x17005DDB RID: 24027
		// (get) Token: 0x06012FE1 RID: 77793 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005DDC RID: 24028
		// (get) Token: 0x06012FE2 RID: 77794 RVA: 0x003018A7 File Offset: 0x002FFAA7
		internal override int ElementTypeId
		{
			get
			{
				return 10022;
			}
		}

		// Token: 0x06012FE3 RID: 77795 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012FE5 RID: 77797 RVA: 0x003018AE File Offset: 0x002FFAAE
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LuminanceModulation>(deep);
		}

		// Token: 0x0400841C RID: 33820
		private const string tagName = "lumMod";

		// Token: 0x0400841D RID: 33821
		private const byte tagNsId = 10;

		// Token: 0x0400841E RID: 33822
		internal const int ElementTypeIdConst = 10022;
	}
}
