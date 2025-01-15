using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002AC7 RID: 10951
	[GeneratedCode("DomGen", "2.0")]
	internal class InkTarget : TimeListSubShapeIdType
	{
		// Token: 0x1700753D RID: 30013
		// (get) Token: 0x06016510 RID: 91408 RVA: 0x00328EF3 File Offset: 0x003270F3
		public override string LocalName
		{
			get
			{
				return "inkTgt";
			}
		}

		// Token: 0x1700753E RID: 30014
		// (get) Token: 0x06016511 RID: 91409 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x1700753F RID: 30015
		// (get) Token: 0x06016512 RID: 91410 RVA: 0x00328EFA File Offset: 0x003270FA
		internal override int ElementTypeId
		{
			get
			{
				return 12369;
			}
		}

		// Token: 0x06016513 RID: 91411 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06016515 RID: 91413 RVA: 0x00328F09 File Offset: 0x00327109
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<InkTarget>(deep);
		}

		// Token: 0x0400972A RID: 38698
		private const string tagName = "inkTgt";

		// Token: 0x0400972B RID: 38699
		private const byte tagNsId = 24;

		// Token: 0x0400972C RID: 38700
		internal const int ElementTypeIdConst = 12369;
	}
}
