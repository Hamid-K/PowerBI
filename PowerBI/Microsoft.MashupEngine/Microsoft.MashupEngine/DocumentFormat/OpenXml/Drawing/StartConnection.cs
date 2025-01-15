using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002794 RID: 10132
	[GeneratedCode("DomGen", "2.0")]
	internal class StartConnection : ConnectionType
	{
		// Token: 0x17006217 RID: 25111
		// (get) Token: 0x0601396A RID: 80234 RVA: 0x00308947 File Offset: 0x00306B47
		public override string LocalName
		{
			get
			{
				return "stCxn";
			}
		}

		// Token: 0x17006218 RID: 25112
		// (get) Token: 0x0601396B RID: 80235 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006219 RID: 25113
		// (get) Token: 0x0601396C RID: 80236 RVA: 0x0030894E File Offset: 0x00306B4E
		internal override int ElementTypeId
		{
			get
			{
				return 10168;
			}
		}

		// Token: 0x0601396D RID: 80237 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601396F RID: 80239 RVA: 0x0030895D File Offset: 0x00306B5D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<StartConnection>(deep);
		}

		// Token: 0x040086DD RID: 34525
		private const string tagName = "stCxn";

		// Token: 0x040086DE RID: 34526
		private const byte tagNsId = 10;

		// Token: 0x040086DF RID: 34527
		internal const int ElementTypeIdConst = 10168;
	}
}
