using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002540 RID: 9536
	[GeneratedCode("DomGen", "2.0")]
	internal class Order : UnsignedIntegerType
	{
		// Token: 0x170054E5 RID: 21733
		// (get) Token: 0x06011BE5 RID: 72677 RVA: 0x002F1956 File Offset: 0x002EFB56
		public override string LocalName
		{
			get
			{
				return "order";
			}
		}

		// Token: 0x170054E6 RID: 21734
		// (get) Token: 0x06011BE6 RID: 72678 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170054E7 RID: 21735
		// (get) Token: 0x06011BE7 RID: 72679 RVA: 0x002F195D File Offset: 0x002EFB5D
		internal override int ElementTypeId
		{
			get
			{
				return 10358;
			}
		}

		// Token: 0x06011BE8 RID: 72680 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011BEA RID: 72682 RVA: 0x002F1964 File Offset: 0x002EFB64
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Order>(deep);
		}

		// Token: 0x04007C56 RID: 31830
		private const string tagName = "order";

		// Token: 0x04007C57 RID: 31831
		private const byte tagNsId = 11;

		// Token: 0x04007C58 RID: 31832
		internal const int ElementTypeIdConst = 10358;
	}
}
