using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025A1 RID: 9633
	[GeneratedCode("DomGen", "2.0")]
	internal class Values : NumberDataSourceType
	{
		// Token: 0x170056E7 RID: 22247
		// (get) Token: 0x0601205E RID: 73822 RVA: 0x002F2F88 File Offset: 0x002F1188
		public override string LocalName
		{
			get
			{
				return "val";
			}
		}

		// Token: 0x170056E8 RID: 22248
		// (get) Token: 0x0601205F RID: 73823 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170056E9 RID: 22249
		// (get) Token: 0x06012060 RID: 73824 RVA: 0x002F4E02 File Offset: 0x002F3002
		internal override int ElementTypeId
		{
			get
			{
				return 10525;
			}
		}

		// Token: 0x06012061 RID: 73825 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012062 RID: 73826 RVA: 0x002F4DBF File Offset: 0x002F2FBF
		public Values()
		{
		}

		// Token: 0x06012063 RID: 73827 RVA: 0x002F4DC7 File Offset: 0x002F2FC7
		public Values(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012064 RID: 73828 RVA: 0x002F4DD0 File Offset: 0x002F2FD0
		public Values(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012065 RID: 73829 RVA: 0x002F4DD9 File Offset: 0x002F2FD9
		public Values(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012066 RID: 73830 RVA: 0x002F4E09 File Offset: 0x002F3009
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Values>(deep);
		}

		// Token: 0x04007DC8 RID: 32200
		private const string tagName = "val";

		// Token: 0x04007DC9 RID: 32201
		private const byte tagNsId = 11;

		// Token: 0x04007DCA RID: 32202
		internal const int ElementTypeIdConst = 10525;
	}
}
