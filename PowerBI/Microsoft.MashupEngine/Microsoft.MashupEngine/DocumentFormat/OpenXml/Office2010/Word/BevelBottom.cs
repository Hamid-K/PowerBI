using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x020024CB RID: 9419
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class BevelBottom : BevelType
	{
		// Token: 0x170052F5 RID: 21237
		// (get) Token: 0x06011795 RID: 71573 RVA: 0x002EED67 File Offset: 0x002ECF67
		public override string LocalName
		{
			get
			{
				return "bevelB";
			}
		}

		// Token: 0x170052F6 RID: 21238
		// (get) Token: 0x06011796 RID: 71574 RVA: 0x002EC7B7 File Offset: 0x002EA9B7
		internal override byte NamespaceId
		{
			get
			{
				return 52;
			}
		}

		// Token: 0x170052F7 RID: 21239
		// (get) Token: 0x06011797 RID: 71575 RVA: 0x002EED6E File Offset: 0x002ECF6E
		internal override int ElementTypeId
		{
			get
			{
				return 12890;
			}
		}

		// Token: 0x06011798 RID: 71576 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x0601179A RID: 71578 RVA: 0x002EED75 File Offset: 0x002ECF75
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BevelBottom>(deep);
		}

		// Token: 0x04007A00 RID: 31232
		private const string tagName = "bevelB";

		// Token: 0x04007A01 RID: 31233
		private const byte tagNsId = 52;

		// Token: 0x04007A02 RID: 31234
		internal const int ElementTypeIdConst = 12890;
	}
}
