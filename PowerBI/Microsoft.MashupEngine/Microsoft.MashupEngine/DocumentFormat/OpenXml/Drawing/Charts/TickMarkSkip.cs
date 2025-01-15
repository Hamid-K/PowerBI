using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025BB RID: 9659
	[GeneratedCode("DomGen", "2.0")]
	internal class TickMarkSkip : SkipType
	{
		// Token: 0x1700576A RID: 22378
		// (get) Token: 0x06012182 RID: 74114 RVA: 0x002F5786 File Offset: 0x002F3986
		public override string LocalName
		{
			get
			{
				return "tickMarkSkip";
			}
		}

		// Token: 0x1700576B RID: 22379
		// (get) Token: 0x06012183 RID: 74115 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x1700576C RID: 22380
		// (get) Token: 0x06012184 RID: 74116 RVA: 0x002F578D File Offset: 0x002F398D
		internal override int ElementTypeId
		{
			get
			{
				return 10485;
			}
		}

		// Token: 0x06012185 RID: 74117 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012187 RID: 74119 RVA: 0x002F5794 File Offset: 0x002F3994
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TickMarkSkip>(deep);
		}

		// Token: 0x04007E2F RID: 32303
		private const string tagName = "tickMarkSkip";

		// Token: 0x04007E30 RID: 32304
		private const byte tagNsId = 11;

		// Token: 0x04007E31 RID: 32305
		internal const int ElementTypeIdConst = 10485;
	}
}
