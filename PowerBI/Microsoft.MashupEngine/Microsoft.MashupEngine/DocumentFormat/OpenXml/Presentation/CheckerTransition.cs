using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002ACE RID: 10958
	[GeneratedCode("DomGen", "2.0")]
	internal class CheckerTransition : OrientationTransitionType
	{
		// Token: 0x17007560 RID: 30048
		// (get) Token: 0x0601655B RID: 91483 RVA: 0x003291AA File Offset: 0x003273AA
		public override string LocalName
		{
			get
			{
				return "checker";
			}
		}

		// Token: 0x17007561 RID: 30049
		// (get) Token: 0x0601655C RID: 91484 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007562 RID: 30050
		// (get) Token: 0x0601655D RID: 91485 RVA: 0x003291B1 File Offset: 0x003273B1
		internal override int ElementTypeId
		{
			get
			{
				return 12376;
			}
		}

		// Token: 0x0601655E RID: 91486 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06016560 RID: 91488 RVA: 0x003291B8 File Offset: 0x003273B8
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CheckerTransition>(deep);
		}

		// Token: 0x04009744 RID: 38724
		private const string tagName = "checker";

		// Token: 0x04009745 RID: 38725
		private const byte tagNsId = 24;

		// Token: 0x04009746 RID: 38726
		internal const int ElementTypeIdConst = 12376;
	}
}
