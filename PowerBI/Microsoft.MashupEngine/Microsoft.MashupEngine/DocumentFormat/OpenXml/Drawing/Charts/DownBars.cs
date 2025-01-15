using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025A9 RID: 9641
	[GeneratedCode("DomGen", "2.0")]
	internal class DownBars : UpDownBarType
	{
		// Token: 0x17005700 RID: 22272
		// (get) Token: 0x060120A0 RID: 73888 RVA: 0x002F4F32 File Offset: 0x002F3132
		public override string LocalName
		{
			get
			{
				return "downBars";
			}
		}

		// Token: 0x17005701 RID: 22273
		// (get) Token: 0x060120A1 RID: 73889 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005702 RID: 22274
		// (get) Token: 0x060120A2 RID: 73890 RVA: 0x002F4F39 File Offset: 0x002F3139
		internal override int ElementTypeId
		{
			get
			{
				return 10455;
			}
		}

		// Token: 0x060120A3 RID: 73891 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060120A4 RID: 73892 RVA: 0x002F4F06 File Offset: 0x002F3106
		public DownBars()
		{
		}

		// Token: 0x060120A5 RID: 73893 RVA: 0x002F4F0E File Offset: 0x002F310E
		public DownBars(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060120A6 RID: 73894 RVA: 0x002F4F17 File Offset: 0x002F3117
		public DownBars(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060120A7 RID: 73895 RVA: 0x002F4F20 File Offset: 0x002F3120
		public DownBars(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060120A8 RID: 73896 RVA: 0x002F4F40 File Offset: 0x002F3140
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DownBars>(deep);
		}

		// Token: 0x04007DDE RID: 32222
		private const string tagName = "downBars";

		// Token: 0x04007DDF RID: 32223
		private const byte tagNsId = 11;

		// Token: 0x04007DE0 RID: 32224
		internal const int ElementTypeIdConst = 10455;
	}
}
