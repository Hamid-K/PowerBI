using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x020024BD RID: 9405
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class NonVisualInkContentPartProperties : NonVisualInkContentPartPropertiesType
	{
		// Token: 0x1700529E RID: 21150
		// (get) Token: 0x060116CF RID: 71375 RVA: 0x002DFE49 File Offset: 0x002DE049
		public override string LocalName
		{
			get
			{
				return "cNvContentPartPr";
			}
		}

		// Token: 0x1700529F RID: 21151
		// (get) Token: 0x060116D0 RID: 71376 RVA: 0x002EC7B7 File Offset: 0x002EA9B7
		internal override byte NamespaceId
		{
			get
			{
				return 52;
			}
		}

		// Token: 0x170052A0 RID: 21152
		// (get) Token: 0x060116D1 RID: 71377 RVA: 0x002EE5E2 File Offset: 0x002EC7E2
		internal override int ElementTypeId
		{
			get
			{
				return 12878;
			}
		}

		// Token: 0x060116D2 RID: 71378 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x060116D3 RID: 71379 RVA: 0x002EE5B6 File Offset: 0x002EC7B6
		public NonVisualInkContentPartProperties()
		{
		}

		// Token: 0x060116D4 RID: 71380 RVA: 0x002EE5BE File Offset: 0x002EC7BE
		public NonVisualInkContentPartProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060116D5 RID: 71381 RVA: 0x002EE5C7 File Offset: 0x002EC7C7
		public NonVisualInkContentPartProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060116D6 RID: 71382 RVA: 0x002EE5D0 File Offset: 0x002EC7D0
		public NonVisualInkContentPartProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060116D7 RID: 71383 RVA: 0x002EE5E9 File Offset: 0x002EC7E9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualInkContentPartProperties>(deep);
		}

		// Token: 0x040079C6 RID: 31174
		private const string tagName = "cNvContentPartPr";

		// Token: 0x040079C7 RID: 31175
		private const byte tagNsId = 52;

		// Token: 0x040079C8 RID: 31176
		internal const int ElementTypeIdConst = 12878;
	}
}
