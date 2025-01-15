using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x020024BC RID: 9404
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class NonVisualInkProperties : NonVisualInkContentPartPropertiesType
	{
		// Token: 0x1700529B RID: 21147
		// (get) Token: 0x060116C6 RID: 71366 RVA: 0x002DFE0F File Offset: 0x002DE00F
		public override string LocalName
		{
			get
			{
				return "cNvInkPr";
			}
		}

		// Token: 0x1700529C RID: 21148
		// (get) Token: 0x060116C7 RID: 71367 RVA: 0x002EC7B7 File Offset: 0x002EA9B7
		internal override byte NamespaceId
		{
			get
			{
				return 52;
			}
		}

		// Token: 0x1700529D RID: 21149
		// (get) Token: 0x060116C8 RID: 71368 RVA: 0x002EE5AF File Offset: 0x002EC7AF
		internal override int ElementTypeId
		{
			get
			{
				return 12877;
			}
		}

		// Token: 0x060116C9 RID: 71369 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x060116CA RID: 71370 RVA: 0x002EE5B6 File Offset: 0x002EC7B6
		public NonVisualInkProperties()
		{
		}

		// Token: 0x060116CB RID: 71371 RVA: 0x002EE5BE File Offset: 0x002EC7BE
		public NonVisualInkProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060116CC RID: 71372 RVA: 0x002EE5C7 File Offset: 0x002EC7C7
		public NonVisualInkProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060116CD RID: 71373 RVA: 0x002EE5D0 File Offset: 0x002EC7D0
		public NonVisualInkProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060116CE RID: 71374 RVA: 0x002EE5D9 File Offset: 0x002EC7D9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualInkProperties>(deep);
		}

		// Token: 0x040079C3 RID: 31171
		private const string tagName = "cNvInkPr";

		// Token: 0x040079C4 RID: 31172
		private const byte tagNsId = 52;

		// Token: 0x040079C5 RID: 31173
		internal const int ElementTypeIdConst = 12877;
	}
}
