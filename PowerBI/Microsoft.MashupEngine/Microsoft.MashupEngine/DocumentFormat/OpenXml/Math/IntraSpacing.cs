using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x020029BB RID: 10683
	[GeneratedCode("DomGen", "2.0")]
	internal class IntraSpacing : TwipsMeasureType
	{
		// Token: 0x17006DA9 RID: 28073
		// (get) Token: 0x06015422 RID: 87074 RVA: 0x0031D516 File Offset: 0x0031B716
		public override string LocalName
		{
			get
			{
				return "intraSp";
			}
		}

		// Token: 0x17006DAA RID: 28074
		// (get) Token: 0x06015423 RID: 87075 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006DAB RID: 28075
		// (get) Token: 0x06015424 RID: 87076 RVA: 0x0031D51D File Offset: 0x0031B71D
		internal override int ElementTypeId
		{
			get
			{
				return 10957;
			}
		}

		// Token: 0x06015425 RID: 87077 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015427 RID: 87079 RVA: 0x0031D524 File Offset: 0x0031B724
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<IntraSpacing>(deep);
		}

		// Token: 0x04009261 RID: 37473
		private const string tagName = "intraSp";

		// Token: 0x04009262 RID: 37474
		private const byte tagNsId = 21;

		// Token: 0x04009263 RID: 37475
		internal const int ElementTypeIdConst = 10957;
	}
}
