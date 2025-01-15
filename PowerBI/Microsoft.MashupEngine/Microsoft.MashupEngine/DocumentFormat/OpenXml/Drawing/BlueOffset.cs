using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020026EE RID: 9966
	[GeneratedCode("DomGen", "2.0")]
	internal class BlueOffset : PercentageType
	{
		// Token: 0x17005DF2 RID: 24050
		// (get) Token: 0x06013010 RID: 77840 RVA: 0x00301958 File Offset: 0x002FFB58
		public override string LocalName
		{
			get
			{
				return "blueOff";
			}
		}

		// Token: 0x17005DF3 RID: 24051
		// (get) Token: 0x06013011 RID: 77841 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005DF4 RID: 24052
		// (get) Token: 0x06013012 RID: 77842 RVA: 0x0030195F File Offset: 0x002FFB5F
		internal override int ElementTypeId
		{
			get
			{
				return 10030;
			}
		}

		// Token: 0x06013013 RID: 77843 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013015 RID: 77845 RVA: 0x00301966 File Offset: 0x002FFB66
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BlueOffset>(deep);
		}

		// Token: 0x04008434 RID: 33844
		private const string tagName = "blueOff";

		// Token: 0x04008435 RID: 33845
		private const byte tagNsId = 10;

		// Token: 0x04008436 RID: 33846
		internal const int ElementTypeIdConst = 10030;
	}
}
