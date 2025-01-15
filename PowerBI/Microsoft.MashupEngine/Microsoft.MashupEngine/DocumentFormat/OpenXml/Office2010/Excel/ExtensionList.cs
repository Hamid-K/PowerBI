using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Spreadsheet;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x020023F6 RID: 9206
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(Extension))]
	[GeneratedCode("DomGen", "2.0")]
	internal class ExtensionList : OpenXmlCompositeElement
	{
		// Token: 0x17004E51 RID: 20049
		// (get) Token: 0x06010D1B RID: 68891 RVA: 0x002DF2B7 File Offset: 0x002DD4B7
		public override string LocalName
		{
			get
			{
				return "extLst";
			}
		}

		// Token: 0x17004E52 RID: 20050
		// (get) Token: 0x06010D1C RID: 68892 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004E53 RID: 20051
		// (get) Token: 0x06010D1D RID: 68893 RVA: 0x002E79D2 File Offset: 0x002E5BD2
		internal override int ElementTypeId
		{
			get
			{
				return 12932;
			}
		}

		// Token: 0x06010D1E RID: 68894 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06010D1F RID: 68895 RVA: 0x00293ECF File Offset: 0x002920CF
		public ExtensionList()
		{
		}

		// Token: 0x06010D20 RID: 68896 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ExtensionList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010D21 RID: 68897 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ExtensionList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010D22 RID: 68898 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ExtensionList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06010D23 RID: 68899 RVA: 0x002E79D9 File Offset: 0x002E5BD9
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "ext" == name)
			{
				return new Extension();
			}
			return null;
		}

		// Token: 0x06010D24 RID: 68900 RVA: 0x002E79F4 File Offset: 0x002E5BF4
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ExtensionList>(deep);
		}

		// Token: 0x0400766B RID: 30315
		private const string tagName = "extLst";

		// Token: 0x0400766C RID: 30316
		private const byte tagNsId = 53;

		// Token: 0x0400766D RID: 30317
		internal const int ElementTypeIdConst = 12932;
	}
}
