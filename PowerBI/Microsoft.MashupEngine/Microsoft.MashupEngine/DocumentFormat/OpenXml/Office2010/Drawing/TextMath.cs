using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Drawing
{
	// Token: 0x0200234F RID: 9039
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class TextMath : OpenXmlLeafElement
	{
		// Token: 0x170049D5 RID: 18901
		// (get) Token: 0x06010343 RID: 66371 RVA: 0x002E0FCF File Offset: 0x002DF1CF
		public override string LocalName
		{
			get
			{
				return "m";
			}
		}

		// Token: 0x170049D6 RID: 18902
		// (get) Token: 0x06010344 RID: 66372 RVA: 0x002E03A2 File Offset: 0x002DE5A2
		internal override byte NamespaceId
		{
			get
			{
				return 48;
			}
		}

		// Token: 0x170049D7 RID: 18903
		// (get) Token: 0x06010345 RID: 66373 RVA: 0x002E0FD6 File Offset: 0x002DF1D6
		internal override int ElementTypeId
		{
			get
			{
				return 12724;
			}
		}

		// Token: 0x06010346 RID: 66374 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06010348 RID: 66376 RVA: 0x002E0FDD File Offset: 0x002DF1DD
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TextMath>(deep);
		}

		// Token: 0x04007388 RID: 29576
		private const string tagName = "m";

		// Token: 0x04007389 RID: 29577
		private const byte tagNsId = 48;

		// Token: 0x0400738A RID: 29578
		internal const int ElementTypeIdConst = 12724;
	}
}
