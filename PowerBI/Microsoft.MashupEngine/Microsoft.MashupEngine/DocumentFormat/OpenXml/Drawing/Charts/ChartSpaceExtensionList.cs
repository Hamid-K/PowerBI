using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002606 RID: 9734
	[ChildElementInfo(typeof(ChartSpaceExtension))]
	[GeneratedCode("DomGen", "2.0")]
	internal class ChartSpaceExtensionList : OpenXmlCompositeElement
	{
		// Token: 0x170059DE RID: 23006
		// (get) Token: 0x060126E3 RID: 75491 RVA: 0x002DF2B7 File Offset: 0x002DD4B7
		public override string LocalName
		{
			get
			{
				return "extLst";
			}
		}

		// Token: 0x170059DF RID: 23007
		// (get) Token: 0x060126E4 RID: 75492 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170059E0 RID: 23008
		// (get) Token: 0x060126E5 RID: 75493 RVA: 0x002FB07C File Offset: 0x002F927C
		internal override int ElementTypeId
		{
			get
			{
				return 10582;
			}
		}

		// Token: 0x060126E6 RID: 75494 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060126E7 RID: 75495 RVA: 0x00293ECF File Offset: 0x002920CF
		public ChartSpaceExtensionList()
		{
		}

		// Token: 0x060126E8 RID: 75496 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ChartSpaceExtensionList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060126E9 RID: 75497 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ChartSpaceExtensionList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060126EA RID: 75498 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ChartSpaceExtensionList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060126EB RID: 75499 RVA: 0x002FB083 File Offset: 0x002F9283
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (11 == namespaceId && "ext" == name)
			{
				return new ChartSpaceExtension();
			}
			return null;
		}

		// Token: 0x060126EC RID: 75500 RVA: 0x002FB09E File Offset: 0x002F929E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ChartSpaceExtensionList>(deep);
		}

		// Token: 0x04007F86 RID: 32646
		private const string tagName = "extLst";

		// Token: 0x04007F87 RID: 32647
		private const byte tagNsId = 11;

		// Token: 0x04007F88 RID: 32648
		internal const int ElementTypeIdConst = 10582;
	}
}
