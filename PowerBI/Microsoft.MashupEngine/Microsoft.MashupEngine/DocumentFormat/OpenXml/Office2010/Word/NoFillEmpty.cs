using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x0200249B RID: 9371
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class NoFillEmpty : EmptyType
	{
		// Token: 0x170051B4 RID: 20916
		// (get) Token: 0x060114DD RID: 70877 RVA: 0x002ECF97 File Offset: 0x002EB197
		public override string LocalName
		{
			get
			{
				return "noFill";
			}
		}

		// Token: 0x170051B5 RID: 20917
		// (get) Token: 0x060114DE RID: 70878 RVA: 0x002EC7B7 File Offset: 0x002EA9B7
		internal override byte NamespaceId
		{
			get
			{
				return 52;
			}
		}

		// Token: 0x170051B6 RID: 20918
		// (get) Token: 0x060114DF RID: 70879 RVA: 0x002ECF9E File Offset: 0x002EB19E
		internal override int ElementTypeId
		{
			get
			{
				return 12846;
			}
		}

		// Token: 0x060114E0 RID: 70880 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x060114E2 RID: 70882 RVA: 0x002ECFAD File Offset: 0x002EB1AD
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NoFillEmpty>(deep);
		}

		// Token: 0x04007931 RID: 31025
		private const string tagName = "noFill";

		// Token: 0x04007932 RID: 31026
		private const byte tagNsId = 52;

		// Token: 0x04007933 RID: 31027
		internal const int ElementTypeIdConst = 12846;
	}
}
