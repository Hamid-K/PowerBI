using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x020029CF RID: 10703
	[GeneratedCode("DomGen", "2.0")]
	internal class DefaultJustification : OfficeMathJustificationType
	{
		// Token: 0x17006E20 RID: 28192
		// (get) Token: 0x0601552C RID: 87340 RVA: 0x0031DF3B File Offset: 0x0031C13B
		public override string LocalName
		{
			get
			{
				return "defJc";
			}
		}

		// Token: 0x17006E21 RID: 28193
		// (get) Token: 0x0601552D RID: 87341 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006E22 RID: 28194
		// (get) Token: 0x0601552E RID: 87342 RVA: 0x0031DF42 File Offset: 0x0031C142
		internal override int ElementTypeId
		{
			get
			{
				return 10953;
			}
		}

		// Token: 0x0601552F RID: 87343 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015531 RID: 87345 RVA: 0x0031DF49 File Offset: 0x0031C149
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DefaultJustification>(deep);
		}

		// Token: 0x040092AF RID: 37551
		private const string tagName = "defJc";

		// Token: 0x040092B0 RID: 37552
		private const byte tagNsId = 21;

		// Token: 0x040092B1 RID: 37553
		internal const int ElementTypeIdConst = 10953;
	}
}
