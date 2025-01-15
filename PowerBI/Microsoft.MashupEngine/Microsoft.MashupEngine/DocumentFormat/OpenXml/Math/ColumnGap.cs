using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x020029A8 RID: 10664
	[GeneratedCode("DomGen", "2.0")]
	internal class ColumnGap : UnsignedShortType
	{
		// Token: 0x17006D3D RID: 27965
		// (get) Token: 0x06015339 RID: 86841 RVA: 0x0031CCC7 File Offset: 0x0031AEC7
		public override string LocalName
		{
			get
			{
				return "cGp";
			}
		}

		// Token: 0x17006D3E RID: 27966
		// (get) Token: 0x0601533A RID: 86842 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006D3F RID: 27967
		// (get) Token: 0x0601533B RID: 86843 RVA: 0x0031CCCE File Offset: 0x0031AECE
		internal override int ElementTypeId
		{
			get
			{
				return 10919;
			}
		}

		// Token: 0x0601533C RID: 86844 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601533E RID: 86846 RVA: 0x0031CCD5 File Offset: 0x0031AED5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ColumnGap>(deep);
		}

		// Token: 0x04009213 RID: 37395
		private const string tagName = "cGp";

		// Token: 0x04009214 RID: 37396
		private const byte tagNsId = 21;

		// Token: 0x04009215 RID: 37397
		internal const int ElementTypeIdConst = 10919;
	}
}
