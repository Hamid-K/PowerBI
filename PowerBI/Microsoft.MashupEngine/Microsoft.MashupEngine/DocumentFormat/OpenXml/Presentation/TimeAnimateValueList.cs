using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002AC0 RID: 10944
	[ChildElementInfo(typeof(TimeAnimateValue))]
	[GeneratedCode("DomGen", "2.0")]
	internal class TimeAnimateValueList : OpenXmlCompositeElement
	{
		// Token: 0x17007517 RID: 29975
		// (get) Token: 0x060164B2 RID: 91314 RVA: 0x00328A1A File Offset: 0x00326C1A
		public override string LocalName
		{
			get
			{
				return "tavLst";
			}
		}

		// Token: 0x17007518 RID: 29976
		// (get) Token: 0x060164B3 RID: 91315 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007519 RID: 29977
		// (get) Token: 0x060164B4 RID: 91316 RVA: 0x00328A21 File Offset: 0x00326C21
		internal override int ElementTypeId
		{
			get
			{
				return 12359;
			}
		}

		// Token: 0x060164B5 RID: 91317 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060164B6 RID: 91318 RVA: 0x00293ECF File Offset: 0x002920CF
		public TimeAnimateValueList()
		{
		}

		// Token: 0x060164B7 RID: 91319 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TimeAnimateValueList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060164B8 RID: 91320 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TimeAnimateValueList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060164B9 RID: 91321 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TimeAnimateValueList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060164BA RID: 91322 RVA: 0x00328A28 File Offset: 0x00326C28
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "tav" == name)
			{
				return new TimeAnimateValue();
			}
			return null;
		}

		// Token: 0x060164BB RID: 91323 RVA: 0x00328A43 File Offset: 0x00326C43
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TimeAnimateValueList>(deep);
		}

		// Token: 0x04009711 RID: 38673
		private const string tagName = "tavLst";

		// Token: 0x04009712 RID: 38674
		private const byte tagNsId = 24;

		// Token: 0x04009713 RID: 38675
		internal const int ElementTypeIdConst = 12359;
	}
}
