using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025A0 RID: 9632
	[GeneratedCode("DomGen", "2.0")]
	internal class Minus : NumberDataSourceType
	{
		// Token: 0x170056E4 RID: 22244
		// (get) Token: 0x06012055 RID: 73813 RVA: 0x002F4DEB File Offset: 0x002F2FEB
		public override string LocalName
		{
			get
			{
				return "minus";
			}
		}

		// Token: 0x170056E5 RID: 22245
		// (get) Token: 0x06012056 RID: 73814 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170056E6 RID: 22246
		// (get) Token: 0x06012057 RID: 73815 RVA: 0x002F4DF2 File Offset: 0x002F2FF2
		internal override int ElementTypeId
		{
			get
			{
				return 10451;
			}
		}

		// Token: 0x06012058 RID: 73816 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012059 RID: 73817 RVA: 0x002F4DBF File Offset: 0x002F2FBF
		public Minus()
		{
		}

		// Token: 0x0601205A RID: 73818 RVA: 0x002F4DC7 File Offset: 0x002F2FC7
		public Minus(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601205B RID: 73819 RVA: 0x002F4DD0 File Offset: 0x002F2FD0
		public Minus(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601205C RID: 73820 RVA: 0x002F4DD9 File Offset: 0x002F2FD9
		public Minus(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601205D RID: 73821 RVA: 0x002F4DF9 File Offset: 0x002F2FF9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Minus>(deep);
		}

		// Token: 0x04007DC5 RID: 32197
		private const string tagName = "minus";

		// Token: 0x04007DC6 RID: 32198
		private const byte tagNsId = 11;

		// Token: 0x04007DC7 RID: 32199
		internal const int ElementTypeIdConst = 10451;
	}
}
