using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027DB RID: 10203
	[GeneratedCode("DomGen", "2.0")]
	internal class HeadEnd : LineEndPropertiesType
	{
		// Token: 0x170063FF RID: 25599
		// (get) Token: 0x06013D9C RID: 81308 RVA: 0x0030C4D7 File Offset: 0x0030A6D7
		public override string LocalName
		{
			get
			{
				return "headEnd";
			}
		}

		// Token: 0x17006400 RID: 25600
		// (get) Token: 0x06013D9D RID: 81309 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006401 RID: 25601
		// (get) Token: 0x06013D9E RID: 81310 RVA: 0x0030C4DE File Offset: 0x0030A6DE
		internal override int ElementTypeId
		{
			get
			{
				return 10235;
			}
		}

		// Token: 0x06013D9F RID: 81311 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013DA1 RID: 81313 RVA: 0x0030C4ED File Offset: 0x0030A6ED
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<HeadEnd>(deep);
		}

		// Token: 0x04008811 RID: 34833
		private const string tagName = "headEnd";

		// Token: 0x04008812 RID: 34834
		private const byte tagNsId = 10;

		// Token: 0x04008813 RID: 34835
		internal const int ElementTypeIdConst = 10235;
	}
}
