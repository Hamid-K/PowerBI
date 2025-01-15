using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x0200259F RID: 9631
	[GeneratedCode("DomGen", "2.0")]
	internal class Plus : NumberDataSourceType
	{
		// Token: 0x170056E1 RID: 22241
		// (get) Token: 0x0601204C RID: 73804 RVA: 0x002F4DB1 File Offset: 0x002F2FB1
		public override string LocalName
		{
			get
			{
				return "plus";
			}
		}

		// Token: 0x170056E2 RID: 22242
		// (get) Token: 0x0601204D RID: 73805 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170056E3 RID: 22243
		// (get) Token: 0x0601204E RID: 73806 RVA: 0x002F4DB8 File Offset: 0x002F2FB8
		internal override int ElementTypeId
		{
			get
			{
				return 10450;
			}
		}

		// Token: 0x0601204F RID: 73807 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012050 RID: 73808 RVA: 0x002F4DBF File Offset: 0x002F2FBF
		public Plus()
		{
		}

		// Token: 0x06012051 RID: 73809 RVA: 0x002F4DC7 File Offset: 0x002F2FC7
		public Plus(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012052 RID: 73810 RVA: 0x002F4DD0 File Offset: 0x002F2FD0
		public Plus(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012053 RID: 73811 RVA: 0x002F4DD9 File Offset: 0x002F2FD9
		public Plus(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012054 RID: 73812 RVA: 0x002F4DE2 File Offset: 0x002F2FE2
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Plus>(deep);
		}

		// Token: 0x04007DC2 RID: 32194
		private const string tagName = "plus";

		// Token: 0x04007DC3 RID: 32195
		private const byte tagNsId = 11;

		// Token: 0x04007DC4 RID: 32196
		internal const int ElementTypeIdConst = 10450;
	}
}
