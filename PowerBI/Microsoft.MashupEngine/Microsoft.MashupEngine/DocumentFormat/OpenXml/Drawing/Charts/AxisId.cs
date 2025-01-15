using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002541 RID: 9537
	[GeneratedCode("DomGen", "2.0")]
	internal class AxisId : UnsignedIntegerType
	{
		// Token: 0x170054E8 RID: 21736
		// (get) Token: 0x06011BEB RID: 72683 RVA: 0x002F196D File Offset: 0x002EFB6D
		public override string LocalName
		{
			get
			{
				return "axId";
			}
		}

		// Token: 0x170054E9 RID: 21737
		// (get) Token: 0x06011BEC RID: 72684 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170054EA RID: 21738
		// (get) Token: 0x06011BED RID: 72685 RVA: 0x002F1974 File Offset: 0x002EFB74
		internal override int ElementTypeId
		{
			get
			{
				return 10373;
			}
		}

		// Token: 0x06011BEE RID: 72686 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011BF0 RID: 72688 RVA: 0x002F197B File Offset: 0x002EFB7B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AxisId>(deep);
		}

		// Token: 0x04007C59 RID: 31833
		private const string tagName = "axId";

		// Token: 0x04007C5A RID: 31834
		private const byte tagNsId = 11;

		// Token: 0x04007C5B RID: 31835
		internal const int ElementTypeIdConst = 10373;
	}
}
