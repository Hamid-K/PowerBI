using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002AD5 RID: 10965
	[GeneratedCode("DomGen", "2.0")]
	internal class CutTransition : OptionalBlackTransitionType
	{
		// Token: 0x17007575 RID: 30069
		// (get) Token: 0x06016587 RID: 91527 RVA: 0x003292C3 File Offset: 0x003274C3
		public override string LocalName
		{
			get
			{
				return "cut";
			}
		}

		// Token: 0x17007576 RID: 30070
		// (get) Token: 0x06016588 RID: 91528 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007577 RID: 30071
		// (get) Token: 0x06016589 RID: 91529 RVA: 0x003292CA File Offset: 0x003274CA
		internal override int ElementTypeId
		{
			get
			{
				return 12381;
			}
		}

		// Token: 0x0601658A RID: 91530 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601658C RID: 91532 RVA: 0x003292D9 File Offset: 0x003274D9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CutTransition>(deep);
		}

		// Token: 0x04009757 RID: 38743
		private const string tagName = "cut";

		// Token: 0x04009758 RID: 38744
		private const byte tagNsId = 24;

		// Token: 0x04009759 RID: 38745
		internal const int ElementTypeIdConst = 12381;
	}
}
