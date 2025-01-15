using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025A2 RID: 9634
	[GeneratedCode("DomGen", "2.0")]
	internal class YValues : NumberDataSourceType
	{
		// Token: 0x170056EA RID: 22250
		// (get) Token: 0x06012067 RID: 73831 RVA: 0x002F4E12 File Offset: 0x002F3012
		public override string LocalName
		{
			get
			{
				return "yVal";
			}
		}

		// Token: 0x170056EB RID: 22251
		// (get) Token: 0x06012068 RID: 73832 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170056EC RID: 22252
		// (get) Token: 0x06012069 RID: 73833 RVA: 0x002F4E19 File Offset: 0x002F3019
		internal override int ElementTypeId
		{
			get
			{
				return 10538;
			}
		}

		// Token: 0x0601206A RID: 73834 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601206B RID: 73835 RVA: 0x002F4DBF File Offset: 0x002F2FBF
		public YValues()
		{
		}

		// Token: 0x0601206C RID: 73836 RVA: 0x002F4DC7 File Offset: 0x002F2FC7
		public YValues(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601206D RID: 73837 RVA: 0x002F4DD0 File Offset: 0x002F2FD0
		public YValues(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601206E RID: 73838 RVA: 0x002F4DD9 File Offset: 0x002F2FD9
		public YValues(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601206F RID: 73839 RVA: 0x002F4E20 File Offset: 0x002F3020
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<YValues>(deep);
		}

		// Token: 0x04007DCB RID: 32203
		private const string tagName = "yVal";

		// Token: 0x04007DCC RID: 32204
		private const byte tagNsId = 11;

		// Token: 0x04007DCD RID: 32205
		internal const int ElementTypeIdConst = 10538;
	}
}
