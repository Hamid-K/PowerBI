using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x020024C0 RID: 9408
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class WordNonVisualContentPartShapeProperties : WordContentPartNonVisualType
	{
		// Token: 0x170052AA RID: 21162
		// (get) Token: 0x060116F0 RID: 71408 RVA: 0x002DFF82 File Offset: 0x002DE182
		public override string LocalName
		{
			get
			{
				return "nvContentPartPr";
			}
		}

		// Token: 0x170052AB RID: 21163
		// (get) Token: 0x060116F1 RID: 71409 RVA: 0x002EC7B7 File Offset: 0x002EA9B7
		internal override byte NamespaceId
		{
			get
			{
				return 52;
			}
		}

		// Token: 0x170052AC RID: 21164
		// (get) Token: 0x060116F2 RID: 71410 RVA: 0x002EE70F File Offset: 0x002EC90F
		internal override int ElementTypeId
		{
			get
			{
				return 12880;
			}
		}

		// Token: 0x060116F3 RID: 71411 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x060116F4 RID: 71412 RVA: 0x002EE6E3 File Offset: 0x002EC8E3
		public WordNonVisualContentPartShapeProperties()
		{
		}

		// Token: 0x060116F5 RID: 71413 RVA: 0x002EE6EB File Offset: 0x002EC8EB
		public WordNonVisualContentPartShapeProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060116F6 RID: 71414 RVA: 0x002EE6F4 File Offset: 0x002EC8F4
		public WordNonVisualContentPartShapeProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060116F7 RID: 71415 RVA: 0x002EE6FD File Offset: 0x002EC8FD
		public WordNonVisualContentPartShapeProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060116F8 RID: 71416 RVA: 0x002EE716 File Offset: 0x002EC916
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<WordNonVisualContentPartShapeProperties>(deep);
		}

		// Token: 0x040079CE RID: 31182
		private const string tagName = "nvContentPartPr";

		// Token: 0x040079CF RID: 31183
		private const byte tagNsId = 52;

		// Token: 0x040079D0 RID: 31184
		internal const int ElementTypeIdConst = 12880;
	}
}
