using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002AB6 RID: 10934
	[ChildElementInfo(typeof(CustomShow))]
	[GeneratedCode("DomGen", "2.0")]
	internal class CustomShowList : OpenXmlCompositeElement
	{
		// Token: 0x170074D7 RID: 29911
		// (get) Token: 0x06016428 RID: 91176 RVA: 0x0032845E File Offset: 0x0032665E
		public override string LocalName
		{
			get
			{
				return "custShowLst";
			}
		}

		// Token: 0x170074D8 RID: 29912
		// (get) Token: 0x06016429 RID: 91177 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x170074D9 RID: 29913
		// (get) Token: 0x0601642A RID: 91178 RVA: 0x00328465 File Offset: 0x00326665
		internal override int ElementTypeId
		{
			get
			{
				return 12349;
			}
		}

		// Token: 0x0601642B RID: 91179 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601642C RID: 91180 RVA: 0x00293ECF File Offset: 0x002920CF
		public CustomShowList()
		{
		}

		// Token: 0x0601642D RID: 91181 RVA: 0x00293ED7 File Offset: 0x002920D7
		public CustomShowList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601642E RID: 91182 RVA: 0x00293EE0 File Offset: 0x002920E0
		public CustomShowList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601642F RID: 91183 RVA: 0x00293EE9 File Offset: 0x002920E9
		public CustomShowList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016430 RID: 91184 RVA: 0x0032846C File Offset: 0x0032666C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "custShow" == name)
			{
				return new CustomShow();
			}
			return null;
		}

		// Token: 0x06016431 RID: 91185 RVA: 0x00328487 File Offset: 0x00326687
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CustomShowList>(deep);
		}

		// Token: 0x040096EC RID: 38636
		private const string tagName = "custShowLst";

		// Token: 0x040096ED RID: 38637
		private const byte tagNsId = 24;

		// Token: 0x040096EE RID: 38638
		internal const int ElementTypeIdConst = 12349;
	}
}
