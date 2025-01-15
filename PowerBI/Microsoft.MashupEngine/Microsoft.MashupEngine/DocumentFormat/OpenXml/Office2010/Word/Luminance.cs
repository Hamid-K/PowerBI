using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x02002493 RID: 9363
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class Luminance : PercentageType
	{
		// Token: 0x1700518E RID: 20878
		// (get) Token: 0x06011487 RID: 70791 RVA: 0x002ECAB1 File Offset: 0x002EACB1
		public override string LocalName
		{
			get
			{
				return "lum";
			}
		}

		// Token: 0x1700518F RID: 20879
		// (get) Token: 0x06011488 RID: 70792 RVA: 0x002EC7B7 File Offset: 0x002EA9B7
		internal override byte NamespaceId
		{
			get
			{
				return 52;
			}
		}

		// Token: 0x17005190 RID: 20880
		// (get) Token: 0x06011489 RID: 70793 RVA: 0x002ECAB8 File Offset: 0x002EACB8
		internal override int ElementTypeId
		{
			get
			{
				return 12839;
			}
		}

		// Token: 0x0601148A RID: 70794 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x0601148C RID: 70796 RVA: 0x002ECABF File Offset: 0x002EACBF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Luminance>(deep);
		}

		// Token: 0x04007912 RID: 30994
		private const string tagName = "lum";

		// Token: 0x04007913 RID: 30995
		private const byte tagNsId = 52;

		// Token: 0x04007914 RID: 30996
		internal const int ElementTypeIdConst = 12839;
	}
}
