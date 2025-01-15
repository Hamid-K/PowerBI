using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A8D RID: 10893
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(CommonSlideViewProperties))]
	internal class NotesViewProperties : OpenXmlCompositeElement
	{
		// Token: 0x170073B7 RID: 29623
		// (get) Token: 0x0601617D RID: 90493 RVA: 0x003267D3 File Offset: 0x003249D3
		public override string LocalName
		{
			get
			{
				return "notesViewPr";
			}
		}

		// Token: 0x170073B8 RID: 29624
		// (get) Token: 0x0601617E RID: 90494 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x170073B9 RID: 29625
		// (get) Token: 0x0601617F RID: 90495 RVA: 0x003267DA File Offset: 0x003249DA
		internal override int ElementTypeId
		{
			get
			{
				return 12306;
			}
		}

		// Token: 0x06016180 RID: 90496 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06016181 RID: 90497 RVA: 0x00293ECF File Offset: 0x002920CF
		public NotesViewProperties()
		{
		}

		// Token: 0x06016182 RID: 90498 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NotesViewProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016183 RID: 90499 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NotesViewProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016184 RID: 90500 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NotesViewProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016185 RID: 90501 RVA: 0x0032650C File Offset: 0x0032470C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "cSldViewPr" == name)
			{
				return new CommonSlideViewProperties();
			}
			if (24 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x170073BA RID: 29626
		// (get) Token: 0x06016186 RID: 90502 RVA: 0x003267E1 File Offset: 0x003249E1
		internal override string[] ElementTagNames
		{
			get
			{
				return NotesViewProperties.eleTagNames;
			}
		}

		// Token: 0x170073BB RID: 29627
		// (get) Token: 0x06016187 RID: 90503 RVA: 0x003267E8 File Offset: 0x003249E8
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NotesViewProperties.eleNamespaceIds;
			}
		}

		// Token: 0x170073BC RID: 29628
		// (get) Token: 0x06016188 RID: 90504 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170073BD RID: 29629
		// (get) Token: 0x06016189 RID: 90505 RVA: 0x0032654D File Offset: 0x0032474D
		// (set) Token: 0x0601618A RID: 90506 RVA: 0x00326556 File Offset: 0x00324756
		public CommonSlideViewProperties CommonSlideViewProperties
		{
			get
			{
				return base.GetElement<CommonSlideViewProperties>(0);
			}
			set
			{
				base.SetElement<CommonSlideViewProperties>(0, value);
			}
		}

		// Token: 0x170073BE RID: 29630
		// (get) Token: 0x0601618B RID: 90507 RVA: 0x00323F93 File Offset: 0x00322193
		// (set) Token: 0x0601618C RID: 90508 RVA: 0x00323F9C File Offset: 0x0032219C
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(1);
			}
			set
			{
				base.SetElement<ExtensionList>(1, value);
			}
		}

		// Token: 0x0601618D RID: 90509 RVA: 0x003267EF File Offset: 0x003249EF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NotesViewProperties>(deep);
		}

		// Token: 0x0400962C RID: 38444
		private const string tagName = "notesViewPr";

		// Token: 0x0400962D RID: 38445
		private const byte tagNsId = 24;

		// Token: 0x0400962E RID: 38446
		internal const int ElementTypeIdConst = 12306;

		// Token: 0x0400962F RID: 38447
		private static readonly string[] eleTagNames = new string[] { "cSldViewPr", "extLst" };

		// Token: 0x04009630 RID: 38448
		private static readonly byte[] eleNamespaceIds = new byte[] { 24, 24 };
	}
}
