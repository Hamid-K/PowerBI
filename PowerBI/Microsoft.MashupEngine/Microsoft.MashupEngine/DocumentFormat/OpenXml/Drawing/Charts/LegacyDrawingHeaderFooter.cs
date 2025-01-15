using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x0200256C RID: 9580
	[GeneratedCode("DomGen", "2.0")]
	internal class LegacyDrawingHeaderFooter : RelationshipIdType
	{
		// Token: 0x170055C8 RID: 21960
		// (get) Token: 0x06011DD2 RID: 73170 RVA: 0x002F3334 File Offset: 0x002F1534
		public override string LocalName
		{
			get
			{
				return "legacyDrawingHF";
			}
		}

		// Token: 0x170055C9 RID: 21961
		// (get) Token: 0x06011DD3 RID: 73171 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170055CA RID: 21962
		// (get) Token: 0x06011DD4 RID: 73172 RVA: 0x002F333B File Offset: 0x002F153B
		internal override int ElementTypeId
		{
			get
			{
				return 10520;
			}
		}

		// Token: 0x06011DD5 RID: 73173 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011DD7 RID: 73175 RVA: 0x002F3342 File Offset: 0x002F1542
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LegacyDrawingHeaderFooter>(deep);
		}

		// Token: 0x04007CF5 RID: 31989
		private const string tagName = "legacyDrawingHF";

		// Token: 0x04007CF6 RID: 31990
		private const byte tagNsId = 11;

		// Token: 0x04007CF7 RID: 31991
		internal const int ElementTypeIdConst = 10520;
	}
}
