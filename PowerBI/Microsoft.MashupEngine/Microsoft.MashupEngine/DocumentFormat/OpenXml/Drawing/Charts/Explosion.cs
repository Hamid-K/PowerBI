using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002545 RID: 9541
	[GeneratedCode("DomGen", "2.0")]
	internal class Explosion : UnsignedIntegerType
	{
		// Token: 0x170054F4 RID: 21748
		// (get) Token: 0x06011C03 RID: 72707 RVA: 0x002F19C9 File Offset: 0x002EFBC9
		public override string LocalName
		{
			get
			{
				return "explosion";
			}
		}

		// Token: 0x170054F5 RID: 21749
		// (get) Token: 0x06011C04 RID: 72708 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170054F6 RID: 21750
		// (get) Token: 0x06011C05 RID: 72709 RVA: 0x002F19D0 File Offset: 0x002EFBD0
		internal override int ElementTypeId
		{
			get
			{
				return 10434;
			}
		}

		// Token: 0x06011C06 RID: 72710 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011C08 RID: 72712 RVA: 0x002F19D7 File Offset: 0x002EFBD7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Explosion>(deep);
		}

		// Token: 0x04007C65 RID: 31845
		private const string tagName = "explosion";

		// Token: 0x04007C66 RID: 31846
		private const byte tagNsId = 11;

		// Token: 0x04007C67 RID: 31847
		internal const int ElementTypeIdConst = 10434;
	}
}
