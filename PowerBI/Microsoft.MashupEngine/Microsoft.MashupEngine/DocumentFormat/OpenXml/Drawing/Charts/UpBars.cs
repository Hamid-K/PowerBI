using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025A8 RID: 9640
	[GeneratedCode("DomGen", "2.0")]
	internal class UpBars : UpDownBarType
	{
		// Token: 0x170056FD RID: 22269
		// (get) Token: 0x06012097 RID: 73879 RVA: 0x002F4EF8 File Offset: 0x002F30F8
		public override string LocalName
		{
			get
			{
				return "upBars";
			}
		}

		// Token: 0x170056FE RID: 22270
		// (get) Token: 0x06012098 RID: 73880 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170056FF RID: 22271
		// (get) Token: 0x06012099 RID: 73881 RVA: 0x002F4EFF File Offset: 0x002F30FF
		internal override int ElementTypeId
		{
			get
			{
				return 10454;
			}
		}

		// Token: 0x0601209A RID: 73882 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601209B RID: 73883 RVA: 0x002F4F06 File Offset: 0x002F3106
		public UpBars()
		{
		}

		// Token: 0x0601209C RID: 73884 RVA: 0x002F4F0E File Offset: 0x002F310E
		public UpBars(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601209D RID: 73885 RVA: 0x002F4F17 File Offset: 0x002F3117
		public UpBars(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601209E RID: 73886 RVA: 0x002F4F20 File Offset: 0x002F3120
		public UpBars(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601209F RID: 73887 RVA: 0x002F4F29 File Offset: 0x002F3129
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<UpBars>(deep);
		}

		// Token: 0x04007DDB RID: 32219
		private const string tagName = "upBars";

		// Token: 0x04007DDC RID: 32220
		private const byte tagNsId = 11;

		// Token: 0x04007DDD RID: 32221
		internal const int ElementTypeIdConst = 10454;
	}
}
