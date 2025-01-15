using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D61 RID: 11617
	[GeneratedCode("DomGen", "2.0")]
	internal class FrameSize : StringType
	{
		// Token: 0x170086BF RID: 34495
		// (get) Token: 0x06018C3D RID: 101437 RVA: 0x0033352F File Offset: 0x0033172F
		public override string LocalName
		{
			get
			{
				return "sz";
			}
		}

		// Token: 0x170086C0 RID: 34496
		// (get) Token: 0x06018C3E RID: 101438 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170086C1 RID: 34497
		// (get) Token: 0x06018C3F RID: 101439 RVA: 0x003448CF File Offset: 0x00342ACF
		internal override int ElementTypeId
		{
			get
			{
				return 11848;
			}
		}

		// Token: 0x06018C40 RID: 101440 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018C42 RID: 101442 RVA: 0x003448D6 File Offset: 0x00342AD6
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FrameSize>(deep);
		}

		// Token: 0x0400A48F RID: 42127
		private const string tagName = "sz";

		// Token: 0x0400A490 RID: 42128
		private const byte tagNsId = 23;

		// Token: 0x0400A491 RID: 42129
		internal const int ElementTypeIdConst = 11848;
	}
}
