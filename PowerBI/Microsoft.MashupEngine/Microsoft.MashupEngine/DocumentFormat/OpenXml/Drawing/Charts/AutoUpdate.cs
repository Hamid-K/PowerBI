using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x0200252E RID: 9518
	[GeneratedCode("DomGen", "2.0")]
	internal class AutoUpdate : BooleanType
	{
		// Token: 0x170054A3 RID: 21667
		// (get) Token: 0x06011B41 RID: 72513 RVA: 0x002F153D File Offset: 0x002EF73D
		public override string LocalName
		{
			get
			{
				return "autoUpdate";
			}
		}

		// Token: 0x170054A4 RID: 21668
		// (get) Token: 0x06011B42 RID: 72514 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170054A5 RID: 21669
		// (get) Token: 0x06011B43 RID: 72515 RVA: 0x002F1544 File Offset: 0x002EF744
		internal override int ElementTypeId
		{
			get
			{
				return 10516;
			}
		}

		// Token: 0x06011B44 RID: 72516 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011B46 RID: 72518 RVA: 0x002F154B File Offset: 0x002EF74B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AutoUpdate>(deep);
		}

		// Token: 0x04007C1E RID: 31774
		private const string tagName = "autoUpdate";

		// Token: 0x04007C1F RID: 31775
		private const byte tagNsId = 11;

		// Token: 0x04007C20 RID: 31776
		internal const int ElementTypeIdConst = 10516;
	}
}
