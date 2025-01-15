using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.PowerPoint
{
	// Token: 0x020023A5 RID: 9125
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class WindowTransition : OrientationTransitionType
	{
		// Token: 0x17004C17 RID: 19479
		// (get) Token: 0x0601082E RID: 67630 RVA: 0x002E43D2 File Offset: 0x002E25D2
		public override string LocalName
		{
			get
			{
				return "window";
			}
		}

		// Token: 0x17004C18 RID: 19480
		// (get) Token: 0x0601082F RID: 67631 RVA: 0x002E3C37 File Offset: 0x002E1E37
		internal override byte NamespaceId
		{
			get
			{
				return 49;
			}
		}

		// Token: 0x17004C19 RID: 19481
		// (get) Token: 0x06010830 RID: 67632 RVA: 0x002E43D9 File Offset: 0x002E25D9
		internal override int ElementTypeId
		{
			get
			{
				return 12775;
			}
		}

		// Token: 0x06010831 RID: 67633 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06010833 RID: 67635 RVA: 0x002E43E0 File Offset: 0x002E25E0
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<WindowTransition>(deep);
		}

		// Token: 0x040074FB RID: 29947
		private const string tagName = "window";

		// Token: 0x040074FC RID: 29948
		private const byte tagNsId = 49;

		// Token: 0x040074FD RID: 29949
		internal const int ElementTypeIdConst = 12775;
	}
}
