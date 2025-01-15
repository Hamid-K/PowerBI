using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x02002977 RID: 10615
	[GeneratedCode("DomGen", "2.0")]
	internal class GrowOperators : OnOffType
	{
		// Token: 0x17006C70 RID: 27760
		// (get) Token: 0x0601516E RID: 86382 RVA: 0x0031B516 File Offset: 0x00319716
		public override string LocalName
		{
			get
			{
				return "grow";
			}
		}

		// Token: 0x17006C71 RID: 27761
		// (get) Token: 0x0601516F RID: 86383 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006C72 RID: 27762
		// (get) Token: 0x06015170 RID: 86384 RVA: 0x0031B51D File Offset: 0x0031971D
		internal override int ElementTypeId
		{
			get
			{
				return 10892;
			}
		}

		// Token: 0x06015171 RID: 86385 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015173 RID: 86387 RVA: 0x0031B524 File Offset: 0x00319724
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<GrowOperators>(deep);
		}

		// Token: 0x04009171 RID: 37233
		private const string tagName = "grow";

		// Token: 0x04009172 RID: 37234
		private const byte tagNsId = 21;

		// Token: 0x04009173 RID: 37235
		internal const int ElementTypeIdConst = 10892;
	}
}
