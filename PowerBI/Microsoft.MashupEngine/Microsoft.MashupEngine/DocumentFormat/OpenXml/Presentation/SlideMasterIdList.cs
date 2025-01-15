using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002AAE RID: 10926
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(SlideMasterId))]
	internal class SlideMasterIdList : OpenXmlCompositeElement
	{
		// Token: 0x170074AB RID: 29867
		// (get) Token: 0x060163C0 RID: 91072 RVA: 0x00328109 File Offset: 0x00326309
		public override string LocalName
		{
			get
			{
				return "sldMasterIdLst";
			}
		}

		// Token: 0x170074AC RID: 29868
		// (get) Token: 0x060163C1 RID: 91073 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x170074AD RID: 29869
		// (get) Token: 0x060163C2 RID: 91074 RVA: 0x00328110 File Offset: 0x00326310
		internal override int ElementTypeId
		{
			get
			{
				return 12341;
			}
		}

		// Token: 0x060163C3 RID: 91075 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060163C4 RID: 91076 RVA: 0x00293ECF File Offset: 0x002920CF
		public SlideMasterIdList()
		{
		}

		// Token: 0x060163C5 RID: 91077 RVA: 0x00293ED7 File Offset: 0x002920D7
		public SlideMasterIdList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060163C6 RID: 91078 RVA: 0x00293EE0 File Offset: 0x002920E0
		public SlideMasterIdList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060163C7 RID: 91079 RVA: 0x00293EE9 File Offset: 0x002920E9
		public SlideMasterIdList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060163C8 RID: 91080 RVA: 0x00328117 File Offset: 0x00326317
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "sldMasterId" == name)
			{
				return new SlideMasterId();
			}
			return null;
		}

		// Token: 0x060163C9 RID: 91081 RVA: 0x00328132 File Offset: 0x00326332
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SlideMasterIdList>(deep);
		}

		// Token: 0x040096CA RID: 38602
		private const string tagName = "sldMasterIdLst";

		// Token: 0x040096CB RID: 38603
		private const byte tagNsId = 24;

		// Token: 0x040096CC RID: 38604
		internal const int ElementTypeIdConst = 12341;
	}
}
