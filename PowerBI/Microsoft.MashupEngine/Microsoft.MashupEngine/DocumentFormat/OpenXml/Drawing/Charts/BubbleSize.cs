using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025A3 RID: 9635
	[GeneratedCode("DomGen", "2.0")]
	internal class BubbleSize : NumberDataSourceType
	{
		// Token: 0x170056ED RID: 22253
		// (get) Token: 0x06012070 RID: 73840 RVA: 0x002F4E29 File Offset: 0x002F3029
		public override string LocalName
		{
			get
			{
				return "bubbleSize";
			}
		}

		// Token: 0x170056EE RID: 22254
		// (get) Token: 0x06012071 RID: 73841 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170056EF RID: 22255
		// (get) Token: 0x06012072 RID: 73842 RVA: 0x002F4E30 File Offset: 0x002F3030
		internal override int ElementTypeId
		{
			get
			{
				return 10583;
			}
		}

		// Token: 0x06012073 RID: 73843 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012074 RID: 73844 RVA: 0x002F4DBF File Offset: 0x002F2FBF
		public BubbleSize()
		{
		}

		// Token: 0x06012075 RID: 73845 RVA: 0x002F4DC7 File Offset: 0x002F2FC7
		public BubbleSize(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012076 RID: 73846 RVA: 0x002F4DD0 File Offset: 0x002F2FD0
		public BubbleSize(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012077 RID: 73847 RVA: 0x002F4DD9 File Offset: 0x002F2FD9
		public BubbleSize(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012078 RID: 73848 RVA: 0x002F4E37 File Offset: 0x002F3037
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BubbleSize>(deep);
		}

		// Token: 0x04007DCE RID: 32206
		private const string tagName = "bubbleSize";

		// Token: 0x04007DCF RID: 32207
		private const byte tagNsId = 11;

		// Token: 0x04007DD0 RID: 32208
		internal const int ElementTypeIdConst = 10583;
	}
}
