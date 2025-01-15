using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A9F RID: 10911
	[GeneratedCode("DomGen", "2.0")]
	internal class GroupShape : GroupShapeType
	{
		// Token: 0x17007433 RID: 29747
		// (get) Token: 0x060162A7 RID: 90791 RVA: 0x002DF94C File Offset: 0x002DDB4C
		public override string LocalName
		{
			get
			{
				return "grpSp";
			}
		}

		// Token: 0x17007434 RID: 29748
		// (get) Token: 0x060162A8 RID: 90792 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007435 RID: 29749
		// (get) Token: 0x060162A9 RID: 90793 RVA: 0x00327329 File Offset: 0x00325529
		internal override int ElementTypeId
		{
			get
			{
				return 12330;
			}
		}

		// Token: 0x060162AA RID: 90794 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060162AB RID: 90795 RVA: 0x003272FD File Offset: 0x003254FD
		public GroupShape()
		{
		}

		// Token: 0x060162AC RID: 90796 RVA: 0x00327305 File Offset: 0x00325505
		public GroupShape(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060162AD RID: 90797 RVA: 0x0032730E File Offset: 0x0032550E
		public GroupShape(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060162AE RID: 90798 RVA: 0x00327317 File Offset: 0x00325517
		public GroupShape(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060162AF RID: 90799 RVA: 0x00327330 File Offset: 0x00325530
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<GroupShape>(deep);
		}

		// Token: 0x04009683 RID: 38531
		private const string tagName = "grpSp";

		// Token: 0x04009684 RID: 38532
		private const byte tagNsId = 24;

		// Token: 0x04009685 RID: 38533
		internal const int ElementTypeIdConst = 12330;
	}
}
