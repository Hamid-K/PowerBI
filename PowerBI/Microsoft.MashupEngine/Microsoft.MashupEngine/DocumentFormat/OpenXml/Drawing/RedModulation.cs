using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020026E9 RID: 9961
	[GeneratedCode("DomGen", "2.0")]
	internal class RedModulation : PercentageType
	{
		// Token: 0x17005DE3 RID: 24035
		// (get) Token: 0x06012FF2 RID: 77810 RVA: 0x003018E5 File Offset: 0x002FFAE5
		public override string LocalName
		{
			get
			{
				return "redMod";
			}
		}

		// Token: 0x17005DE4 RID: 24036
		// (get) Token: 0x06012FF3 RID: 77811 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005DE5 RID: 24037
		// (get) Token: 0x06012FF4 RID: 77812 RVA: 0x003018EC File Offset: 0x002FFAEC
		internal override int ElementTypeId
		{
			get
			{
				return 10025;
			}
		}

		// Token: 0x06012FF5 RID: 77813 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012FF7 RID: 77815 RVA: 0x003018F3 File Offset: 0x002FFAF3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RedModulation>(deep);
		}

		// Token: 0x04008425 RID: 33829
		private const string tagName = "redMod";

		// Token: 0x04008426 RID: 33830
		private const byte tagNsId = 10;

		// Token: 0x04008427 RID: 33831
		internal const int ElementTypeIdConst = 10025;
	}
}
