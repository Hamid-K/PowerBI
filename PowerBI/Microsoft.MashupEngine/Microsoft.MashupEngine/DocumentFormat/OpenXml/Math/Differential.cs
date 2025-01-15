using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x0200296E RID: 10606
	[GeneratedCode("DomGen", "2.0")]
	internal class Differential : OnOffType
	{
		// Token: 0x17006C55 RID: 27733
		// (get) Token: 0x06015138 RID: 86328 RVA: 0x0031B447 File Offset: 0x00319647
		public override string LocalName
		{
			get
			{
				return "diff";
			}
		}

		// Token: 0x17006C56 RID: 27734
		// (get) Token: 0x06015139 RID: 86329 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006C57 RID: 27735
		// (get) Token: 0x0601513A RID: 86330 RVA: 0x0031B44E File Offset: 0x0031964E
		internal override int ElementTypeId
		{
			get
			{
				return 10878;
			}
		}

		// Token: 0x0601513B RID: 86331 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601513D RID: 86333 RVA: 0x0031B455 File Offset: 0x00319655
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Differential>(deep);
		}

		// Token: 0x04009156 RID: 37206
		private const string tagName = "diff";

		// Token: 0x04009157 RID: 37207
		private const byte tagNsId = 21;

		// Token: 0x04009158 RID: 37208
		internal const int ElementTypeIdConst = 10878;
	}
}
