using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x0200249C RID: 9372
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class RoundEmpty : EmptyType
	{
		// Token: 0x170051B7 RID: 20919
		// (get) Token: 0x060114E3 RID: 70883 RVA: 0x002ECFB6 File Offset: 0x002EB1B6
		public override string LocalName
		{
			get
			{
				return "round";
			}
		}

		// Token: 0x170051B8 RID: 20920
		// (get) Token: 0x060114E4 RID: 70884 RVA: 0x002EC7B7 File Offset: 0x002EA9B7
		internal override byte NamespaceId
		{
			get
			{
				return 52;
			}
		}

		// Token: 0x170051B9 RID: 20921
		// (get) Token: 0x060114E5 RID: 70885 RVA: 0x002ECFBD File Offset: 0x002EB1BD
		internal override int ElementTypeId
		{
			get
			{
				return 12850;
			}
		}

		// Token: 0x060114E6 RID: 70886 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x060114E8 RID: 70888 RVA: 0x002ECFC4 File Offset: 0x002EB1C4
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RoundEmpty>(deep);
		}

		// Token: 0x04007934 RID: 31028
		private const string tagName = "round";

		// Token: 0x04007935 RID: 31029
		private const byte tagNsId = 52;

		// Token: 0x04007936 RID: 31030
		internal const int ElementTypeIdConst = 12850;
	}
}
