using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A99 RID: 10905
	[ChildElementInfo(typeof(CustomerData))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(CustomerDataTags))]
	internal class CustomerDataList : OpenXmlCompositeElement
	{
		// Token: 0x17007417 RID: 29719
		// (get) Token: 0x0601625C RID: 90716 RVA: 0x00326FD6 File Offset: 0x003251D6
		public override string LocalName
		{
			get
			{
				return "custDataLst";
			}
		}

		// Token: 0x17007418 RID: 29720
		// (get) Token: 0x0601625D RID: 90717 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007419 RID: 29721
		// (get) Token: 0x0601625E RID: 90718 RVA: 0x00326FDD File Offset: 0x003251DD
		internal override int ElementTypeId
		{
			get
			{
				return 12320;
			}
		}

		// Token: 0x0601625F RID: 90719 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06016260 RID: 90720 RVA: 0x00293ECF File Offset: 0x002920CF
		public CustomerDataList()
		{
		}

		// Token: 0x06016261 RID: 90721 RVA: 0x00293ED7 File Offset: 0x002920D7
		public CustomerDataList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016262 RID: 90722 RVA: 0x00293EE0 File Offset: 0x002920E0
		public CustomerDataList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016263 RID: 90723 RVA: 0x00293EE9 File Offset: 0x002920E9
		public CustomerDataList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016264 RID: 90724 RVA: 0x00326FE4 File Offset: 0x003251E4
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "custData" == name)
			{
				return new CustomerData();
			}
			if (24 == namespaceId && "tags" == name)
			{
				return new CustomerDataTags();
			}
			return null;
		}

		// Token: 0x06016265 RID: 90725 RVA: 0x00327017 File Offset: 0x00325217
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CustomerDataList>(deep);
		}

		// Token: 0x0400966E RID: 38510
		private const string tagName = "custDataLst";

		// Token: 0x0400966F RID: 38511
		private const byte tagNsId = 24;

		// Token: 0x04009670 RID: 38512
		internal const int ElementTypeIdConst = 12320;
	}
}
