using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025D5 RID: 9685
	[GeneratedCode("DomGen", "2.0")]
	internal class XValues : AxisDataSourceType
	{
		// Token: 0x1700582F RID: 22575
		// (get) Token: 0x06012338 RID: 74552 RVA: 0x002F71AE File Offset: 0x002F53AE
		public override string LocalName
		{
			get
			{
				return "xVal";
			}
		}

		// Token: 0x17005830 RID: 22576
		// (get) Token: 0x06012339 RID: 74553 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005831 RID: 22577
		// (get) Token: 0x0601233A RID: 74554 RVA: 0x002F71B5 File Offset: 0x002F53B5
		internal override int ElementTypeId
		{
			get
			{
				return 10537;
			}
		}

		// Token: 0x0601233B RID: 74555 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601233C RID: 74556 RVA: 0x002F7182 File Offset: 0x002F5382
		public XValues()
		{
		}

		// Token: 0x0601233D RID: 74557 RVA: 0x002F718A File Offset: 0x002F538A
		public XValues(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601233E RID: 74558 RVA: 0x002F7193 File Offset: 0x002F5393
		public XValues(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601233F RID: 74559 RVA: 0x002F719C File Offset: 0x002F539C
		public XValues(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012340 RID: 74560 RVA: 0x002F71BC File Offset: 0x002F53BC
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<XValues>(deep);
		}

		// Token: 0x04007E98 RID: 32408
		private const string tagName = "xVal";

		// Token: 0x04007E99 RID: 32409
		private const byte tagNsId = 11;

		// Token: 0x04007E9A RID: 32410
		internal const int ElementTypeIdConst = 10537;
	}
}
