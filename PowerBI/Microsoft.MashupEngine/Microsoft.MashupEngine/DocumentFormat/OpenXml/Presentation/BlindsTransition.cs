using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002ACD RID: 10957
	[GeneratedCode("DomGen", "2.0")]
	internal class BlindsTransition : OrientationTransitionType
	{
		// Token: 0x1700755D RID: 30045
		// (get) Token: 0x06016555 RID: 91477 RVA: 0x0032918B File Offset: 0x0032738B
		public override string LocalName
		{
			get
			{
				return "blinds";
			}
		}

		// Token: 0x1700755E RID: 30046
		// (get) Token: 0x06016556 RID: 91478 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x1700755F RID: 30047
		// (get) Token: 0x06016557 RID: 91479 RVA: 0x00329192 File Offset: 0x00327392
		internal override int ElementTypeId
		{
			get
			{
				return 12375;
			}
		}

		// Token: 0x06016558 RID: 91480 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601655A RID: 91482 RVA: 0x003291A1 File Offset: 0x003273A1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BlindsTransition>(deep);
		}

		// Token: 0x04009741 RID: 38721
		private const string tagName = "blinds";

		// Token: 0x04009742 RID: 38722
		private const byte tagNsId = 24;

		// Token: 0x04009743 RID: 38723
		internal const int ElementTypeIdConst = 12375;
	}
}
