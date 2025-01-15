using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x0200249D RID: 9373
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class BevelEmpty : EmptyType
	{
		// Token: 0x170051BA RID: 20922
		// (get) Token: 0x060114E9 RID: 70889 RVA: 0x002ECFCD File Offset: 0x002EB1CD
		public override string LocalName
		{
			get
			{
				return "bevel";
			}
		}

		// Token: 0x170051BB RID: 20923
		// (get) Token: 0x060114EA RID: 70890 RVA: 0x002EC7B7 File Offset: 0x002EA9B7
		internal override byte NamespaceId
		{
			get
			{
				return 52;
			}
		}

		// Token: 0x170051BC RID: 20924
		// (get) Token: 0x060114EB RID: 70891 RVA: 0x002ECFD4 File Offset: 0x002EB1D4
		internal override int ElementTypeId
		{
			get
			{
				return 12851;
			}
		}

		// Token: 0x060114EC RID: 70892 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x060114EE RID: 70894 RVA: 0x002ECFDB File Offset: 0x002EB1DB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BevelEmpty>(deep);
		}

		// Token: 0x04007937 RID: 31031
		private const string tagName = "bevel";

		// Token: 0x04007938 RID: 31032
		private const byte tagNsId = 52;

		// Token: 0x04007939 RID: 31033
		internal const int ElementTypeIdConst = 12851;
	}
}
