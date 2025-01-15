using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x0200255C RID: 9564
	[GeneratedCode("DomGen", "2.0")]
	internal class Left : DoubleType
	{
		// Token: 0x17005590 RID: 21904
		// (get) Token: 0x06011D55 RID: 73045 RVA: 0x002F2EEE File Offset: 0x002F10EE
		public override string LocalName
		{
			get
			{
				return "x";
			}
		}

		// Token: 0x17005591 RID: 21905
		// (get) Token: 0x06011D56 RID: 73046 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005592 RID: 21906
		// (get) Token: 0x06011D57 RID: 73047 RVA: 0x002F2EF5 File Offset: 0x002F10F5
		internal override int ElementTypeId
		{
			get
			{
				return 10411;
			}
		}

		// Token: 0x06011D58 RID: 73048 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011D5A RID: 73050 RVA: 0x002F2EFC File Offset: 0x002F10FC
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Left>(deep);
		}

		// Token: 0x04007CC4 RID: 31940
		private const string tagName = "x";

		// Token: 0x04007CC5 RID: 31941
		private const byte tagNsId = 11;

		// Token: 0x04007CC6 RID: 31942
		internal const int ElementTypeIdConst = 10411;
	}
}
