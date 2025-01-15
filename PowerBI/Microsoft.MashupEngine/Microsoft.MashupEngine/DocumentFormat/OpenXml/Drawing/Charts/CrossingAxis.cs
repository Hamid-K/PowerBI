using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002542 RID: 9538
	[GeneratedCode("DomGen", "2.0")]
	internal class CrossingAxis : UnsignedIntegerType
	{
		// Token: 0x170054EB RID: 21739
		// (get) Token: 0x06011BF1 RID: 72689 RVA: 0x002F1984 File Offset: 0x002EFB84
		public override string LocalName
		{
			get
			{
				return "crossAx";
			}
		}

		// Token: 0x170054EC RID: 21740
		// (get) Token: 0x06011BF2 RID: 72690 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170054ED RID: 21741
		// (get) Token: 0x06011BF3 RID: 72691 RVA: 0x002F198B File Offset: 0x002EFB8B
		internal override int ElementTypeId
		{
			get
			{
				return 10383;
			}
		}

		// Token: 0x06011BF4 RID: 72692 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011BF6 RID: 72694 RVA: 0x002F1992 File Offset: 0x002EFB92
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CrossingAxis>(deep);
		}

		// Token: 0x04007C5C RID: 31836
		private const string tagName = "crossAx";

		// Token: 0x04007C5D RID: 31837
		private const byte tagNsId = 11;

		// Token: 0x04007C5E RID: 31838
		internal const int ElementTypeIdConst = 10383;
	}
}
