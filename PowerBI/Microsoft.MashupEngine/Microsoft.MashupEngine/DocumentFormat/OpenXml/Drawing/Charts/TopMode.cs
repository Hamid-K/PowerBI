using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002589 RID: 9609
	[GeneratedCode("DomGen", "2.0")]
	internal class TopMode : LayoutModeType
	{
		// Token: 0x1700564D RID: 22093
		// (get) Token: 0x06011F17 RID: 73495 RVA: 0x002F3E72 File Offset: 0x002F2072
		public override string LocalName
		{
			get
			{
				return "yMode";
			}
		}

		// Token: 0x1700564E RID: 22094
		// (get) Token: 0x06011F18 RID: 73496 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x1700564F RID: 22095
		// (get) Token: 0x06011F19 RID: 73497 RVA: 0x002F3E79 File Offset: 0x002F2079
		internal override int ElementTypeId
		{
			get
			{
				return 10408;
			}
		}

		// Token: 0x06011F1A RID: 73498 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011F1C RID: 73500 RVA: 0x002F3E80 File Offset: 0x002F2080
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TopMode>(deep);
		}

		// Token: 0x04007D5D RID: 32093
		private const string tagName = "yMode";

		// Token: 0x04007D5E RID: 32094
		private const byte tagNsId = 11;

		// Token: 0x04007D5F RID: 32095
		internal const int ElementTypeIdConst = 10408;
	}
}
