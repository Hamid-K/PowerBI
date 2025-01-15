using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002543 RID: 9539
	[GeneratedCode("DomGen", "2.0")]
	internal class PointCount : UnsignedIntegerType
	{
		// Token: 0x170054EE RID: 21742
		// (get) Token: 0x06011BF7 RID: 72695 RVA: 0x002F199B File Offset: 0x002EFB9B
		public override string LocalName
		{
			get
			{
				return "ptCount";
			}
		}

		// Token: 0x170054EF RID: 21743
		// (get) Token: 0x06011BF8 RID: 72696 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170054F0 RID: 21744
		// (get) Token: 0x06011BF9 RID: 72697 RVA: 0x002F19A2 File Offset: 0x002EFBA2
		internal override int ElementTypeId
		{
			get
			{
				return 10392;
			}
		}

		// Token: 0x06011BFA RID: 72698 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011BFC RID: 72700 RVA: 0x002F19A9 File Offset: 0x002EFBA9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PointCount>(deep);
		}

		// Token: 0x04007C5F RID: 31839
		private const string tagName = "ptCount";

		// Token: 0x04007C60 RID: 31840
		private const byte tagNsId = 11;

		// Token: 0x04007C61 RID: 31841
		internal const int ElementTypeIdConst = 10392;
	}
}
