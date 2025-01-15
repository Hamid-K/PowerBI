using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x020023D5 RID: 9173
	[ChildElementInfo(typeof(SparklineGroup), FileFormatVersions.Office2010)]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class SparklineGroups : OpenXmlCompositeElement
	{
		// Token: 0x17004D2C RID: 19756
		// (get) Token: 0x06010A87 RID: 68231 RVA: 0x002E5BF7 File Offset: 0x002E3DF7
		public override string LocalName
		{
			get
			{
				return "sparklineGroups";
			}
		}

		// Token: 0x17004D2D RID: 19757
		// (get) Token: 0x06010A88 RID: 68232 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004D2E RID: 19758
		// (get) Token: 0x06010A89 RID: 68233 RVA: 0x002E5BFE File Offset: 0x002E3DFE
		internal override int ElementTypeId
		{
			get
			{
				return 12899;
			}
		}

		// Token: 0x06010A8A RID: 68234 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06010A8B RID: 68235 RVA: 0x00293ECF File Offset: 0x002920CF
		public SparklineGroups()
		{
		}

		// Token: 0x06010A8C RID: 68236 RVA: 0x00293ED7 File Offset: 0x002920D7
		public SparklineGroups(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010A8D RID: 68237 RVA: 0x00293EE0 File Offset: 0x002920E0
		public SparklineGroups(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010A8E RID: 68238 RVA: 0x00293EE9 File Offset: 0x002920E9
		public SparklineGroups(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06010A8F RID: 68239 RVA: 0x002E5C05 File Offset: 0x002E3E05
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (53 == namespaceId && "sparklineGroup" == name)
			{
				return new SparklineGroup();
			}
			return null;
		}

		// Token: 0x06010A90 RID: 68240 RVA: 0x002E5C20 File Offset: 0x002E3E20
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SparklineGroups>(deep);
		}

		// Token: 0x040075C8 RID: 30152
		private const string tagName = "sparklineGroups";

		// Token: 0x040075C9 RID: 30153
		private const byte tagNsId = 53;

		// Token: 0x040075CA RID: 30154
		internal const int ElementTypeIdConst = 12899;
	}
}
