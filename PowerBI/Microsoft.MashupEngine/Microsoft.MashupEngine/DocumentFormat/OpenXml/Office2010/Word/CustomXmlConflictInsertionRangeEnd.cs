using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x020024B6 RID: 9398
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class CustomXmlConflictInsertionRangeEnd : MarkupType
	{
		// Token: 0x1700526E RID: 21102
		// (get) Token: 0x06011667 RID: 71271 RVA: 0x002EE214 File Offset: 0x002EC414
		public override string LocalName
		{
			get
			{
				return "customXmlConflictInsRangeEnd";
			}
		}

		// Token: 0x1700526F RID: 21103
		// (get) Token: 0x06011668 RID: 71272 RVA: 0x002EC7B7 File Offset: 0x002EA9B7
		internal override byte NamespaceId
		{
			get
			{
				return 52;
			}
		}

		// Token: 0x17005270 RID: 21104
		// (get) Token: 0x06011669 RID: 71273 RVA: 0x002EE21B File Offset: 0x002EC41B
		internal override int ElementTypeId
		{
			get
			{
				return 12869;
			}
		}

		// Token: 0x0601166A RID: 71274 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x0601166C RID: 71276 RVA: 0x002EE22A File Offset: 0x002EC42A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CustomXmlConflictInsertionRangeEnd>(deep);
		}

		// Token: 0x040079A8 RID: 31144
		private const string tagName = "customXmlConflictInsRangeEnd";

		// Token: 0x040079A9 RID: 31145
		private const byte tagNsId = 52;

		// Token: 0x040079AA RID: 31146
		internal const int ElementTypeIdConst = 12869;
	}
}
