using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A7D RID: 10877
	[GeneratedCode("DomGen", "2.0")]
	internal class RestoredTop : NormalViewPortionType
	{
		// Token: 0x17007342 RID: 29506
		// (get) Token: 0x0601607E RID: 90238 RVA: 0x00325DDA File Offset: 0x00323FDA
		public override string LocalName
		{
			get
			{
				return "restoredTop";
			}
		}

		// Token: 0x17007343 RID: 29507
		// (get) Token: 0x0601607F RID: 90239 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007344 RID: 29508
		// (get) Token: 0x06016080 RID: 90240 RVA: 0x00325DE1 File Offset: 0x00323FE1
		internal override int ElementTypeId
		{
			get
			{
				return 12292;
			}
		}

		// Token: 0x06016081 RID: 90241 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06016083 RID: 90243 RVA: 0x00325DE8 File Offset: 0x00323FE8
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RestoredTop>(deep);
		}

		// Token: 0x040095E1 RID: 38369
		private const string tagName = "restoredTop";

		// Token: 0x040095E2 RID: 38370
		private const byte tagNsId = 24;

		// Token: 0x040095E3 RID: 38371
		internal const int ElementTypeIdConst = 12292;
	}
}
