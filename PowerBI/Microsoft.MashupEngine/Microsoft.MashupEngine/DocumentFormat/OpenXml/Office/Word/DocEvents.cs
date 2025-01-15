using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office.Word
{
	// Token: 0x02002477 RID: 9335
	[ChildElementInfo(typeof(EventDocXmlBeforeDeleteXsdString))]
	[ChildElementInfo(typeof(EventDocOpenXsdString))]
	[ChildElementInfo(typeof(EventDocCloseXsdString))]
	[ChildElementInfo(typeof(EventDocSyncXsdString))]
	[ChildElementInfo(typeof(EventDocXmlAfterInsertXsdString))]
	[ChildElementInfo(typeof(EventDocNewXsdString))]
	[ChildElementInfo(typeof(EventDocContentControlAfterInsertXsdString))]
	[ChildElementInfo(typeof(EventDocContentControlBeforeDeleteXsdString))]
	[ChildElementInfo(typeof(EventDocContentControlOnExistXsdString))]
	[ChildElementInfo(typeof(EventDocContentControlOnEnterXsdString))]
	[ChildElementInfo(typeof(EventDocStoreUpdateXsdString))]
	[ChildElementInfo(typeof(EventDocContentControlUpdateXsdString))]
	[ChildElementInfo(typeof(EventDocBuildingBlockAfterInsertXsdString))]
	[GeneratedCode("DomGen", "2.0")]
	internal class DocEvents : OpenXmlCompositeElement
	{
		// Token: 0x17005118 RID: 20760
		// (get) Token: 0x06011372 RID: 70514 RVA: 0x002EBB28 File Offset: 0x002E9D28
		public override string LocalName
		{
			get
			{
				return "docEvents";
			}
		}

		// Token: 0x17005119 RID: 20761
		// (get) Token: 0x06011373 RID: 70515 RVA: 0x002EAFCE File Offset: 0x002E91CE
		internal override byte NamespaceId
		{
			get
			{
				return 33;
			}
		}

		// Token: 0x1700511A RID: 20762
		// (get) Token: 0x06011374 RID: 70516 RVA: 0x002EBB2F File Offset: 0x002E9D2F
		internal override int ElementTypeId
		{
			get
			{
				return 12562;
			}
		}

		// Token: 0x06011375 RID: 70517 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011376 RID: 70518 RVA: 0x00293ECF File Offset: 0x002920CF
		public DocEvents()
		{
		}

		// Token: 0x06011377 RID: 70519 RVA: 0x00293ED7 File Offset: 0x002920D7
		public DocEvents(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011378 RID: 70520 RVA: 0x00293EE0 File Offset: 0x002920E0
		public DocEvents(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011379 RID: 70521 RVA: 0x00293EE9 File Offset: 0x002920E9
		public DocEvents(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601137A RID: 70522 RVA: 0x002EBB38 File Offset: 0x002E9D38
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (33 == namespaceId && "eventDocNew" == name)
			{
				return new EventDocNewXsdString();
			}
			if (33 == namespaceId && "eventDocOpen" == name)
			{
				return new EventDocOpenXsdString();
			}
			if (33 == namespaceId && "eventDocClose" == name)
			{
				return new EventDocCloseXsdString();
			}
			if (33 == namespaceId && "eventDocSync" == name)
			{
				return new EventDocSyncXsdString();
			}
			if (33 == namespaceId && "eventDocXmlAfterInsert" == name)
			{
				return new EventDocXmlAfterInsertXsdString();
			}
			if (33 == namespaceId && "eventDocXmlBeforeDelete" == name)
			{
				return new EventDocXmlBeforeDeleteXsdString();
			}
			if (33 == namespaceId && "eventDocContentControlAfterInsert" == name)
			{
				return new EventDocContentControlAfterInsertXsdString();
			}
			if (33 == namespaceId && "eventDocContentControlBeforeDelete" == name)
			{
				return new EventDocContentControlBeforeDeleteXsdString();
			}
			if (33 == namespaceId && "eventDocContentControlOnExit" == name)
			{
				return new EventDocContentControlOnExistXsdString();
			}
			if (33 == namespaceId && "eventDocContentControlOnEnter" == name)
			{
				return new EventDocContentControlOnEnterXsdString();
			}
			if (33 == namespaceId && "eventDocStoreUpdate" == name)
			{
				return new EventDocStoreUpdateXsdString();
			}
			if (33 == namespaceId && "eventDocContentControlContentUpdate" == name)
			{
				return new EventDocContentControlUpdateXsdString();
			}
			if (33 == namespaceId && "eventDocBuildingBlockAfterInsert" == name)
			{
				return new EventDocBuildingBlockAfterInsertXsdString();
			}
			return null;
		}

		// Token: 0x1700511B RID: 20763
		// (get) Token: 0x0601137B RID: 70523 RVA: 0x002EBC7E File Offset: 0x002E9E7E
		internal override string[] ElementTagNames
		{
			get
			{
				return DocEvents.eleTagNames;
			}
		}

		// Token: 0x1700511C RID: 20764
		// (get) Token: 0x0601137C RID: 70524 RVA: 0x002EBC85 File Offset: 0x002E9E85
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return DocEvents.eleNamespaceIds;
			}
		}

		// Token: 0x1700511D RID: 20765
		// (get) Token: 0x0601137D RID: 70525 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700511E RID: 20766
		// (get) Token: 0x0601137E RID: 70526 RVA: 0x002EBC8C File Offset: 0x002E9E8C
		// (set) Token: 0x0601137F RID: 70527 RVA: 0x002EBC95 File Offset: 0x002E9E95
		public EventDocNewXsdString EventDocNewXsdString
		{
			get
			{
				return base.GetElement<EventDocNewXsdString>(0);
			}
			set
			{
				base.SetElement<EventDocNewXsdString>(0, value);
			}
		}

		// Token: 0x1700511F RID: 20767
		// (get) Token: 0x06011380 RID: 70528 RVA: 0x002EBC9F File Offset: 0x002E9E9F
		// (set) Token: 0x06011381 RID: 70529 RVA: 0x002EBCA8 File Offset: 0x002E9EA8
		public EventDocOpenXsdString EventDocOpenXsdString
		{
			get
			{
				return base.GetElement<EventDocOpenXsdString>(1);
			}
			set
			{
				base.SetElement<EventDocOpenXsdString>(1, value);
			}
		}

		// Token: 0x17005120 RID: 20768
		// (get) Token: 0x06011382 RID: 70530 RVA: 0x002EBCB2 File Offset: 0x002E9EB2
		// (set) Token: 0x06011383 RID: 70531 RVA: 0x002EBCBB File Offset: 0x002E9EBB
		public EventDocCloseXsdString EventDocCloseXsdString
		{
			get
			{
				return base.GetElement<EventDocCloseXsdString>(2);
			}
			set
			{
				base.SetElement<EventDocCloseXsdString>(2, value);
			}
		}

		// Token: 0x17005121 RID: 20769
		// (get) Token: 0x06011384 RID: 70532 RVA: 0x002EBCC5 File Offset: 0x002E9EC5
		// (set) Token: 0x06011385 RID: 70533 RVA: 0x002EBCCE File Offset: 0x002E9ECE
		public EventDocSyncXsdString EventDocSyncXsdString
		{
			get
			{
				return base.GetElement<EventDocSyncXsdString>(3);
			}
			set
			{
				base.SetElement<EventDocSyncXsdString>(3, value);
			}
		}

		// Token: 0x17005122 RID: 20770
		// (get) Token: 0x06011386 RID: 70534 RVA: 0x002EBCD8 File Offset: 0x002E9ED8
		// (set) Token: 0x06011387 RID: 70535 RVA: 0x002EBCE1 File Offset: 0x002E9EE1
		public EventDocXmlAfterInsertXsdString EventDocXmlAfterInsertXsdString
		{
			get
			{
				return base.GetElement<EventDocXmlAfterInsertXsdString>(4);
			}
			set
			{
				base.SetElement<EventDocXmlAfterInsertXsdString>(4, value);
			}
		}

		// Token: 0x17005123 RID: 20771
		// (get) Token: 0x06011388 RID: 70536 RVA: 0x002EBCEB File Offset: 0x002E9EEB
		// (set) Token: 0x06011389 RID: 70537 RVA: 0x002EBCF4 File Offset: 0x002E9EF4
		public EventDocXmlBeforeDeleteXsdString EventDocXmlBeforeDeleteXsdString
		{
			get
			{
				return base.GetElement<EventDocXmlBeforeDeleteXsdString>(5);
			}
			set
			{
				base.SetElement<EventDocXmlBeforeDeleteXsdString>(5, value);
			}
		}

		// Token: 0x17005124 RID: 20772
		// (get) Token: 0x0601138A RID: 70538 RVA: 0x002EBCFE File Offset: 0x002E9EFE
		// (set) Token: 0x0601138B RID: 70539 RVA: 0x002EBD07 File Offset: 0x002E9F07
		public EventDocContentControlAfterInsertXsdString EventDocContentControlAfterInsertXsdString
		{
			get
			{
				return base.GetElement<EventDocContentControlAfterInsertXsdString>(6);
			}
			set
			{
				base.SetElement<EventDocContentControlAfterInsertXsdString>(6, value);
			}
		}

		// Token: 0x17005125 RID: 20773
		// (get) Token: 0x0601138C RID: 70540 RVA: 0x002EBD11 File Offset: 0x002E9F11
		// (set) Token: 0x0601138D RID: 70541 RVA: 0x002EBD1A File Offset: 0x002E9F1A
		public EventDocContentControlBeforeDeleteXsdString EventDocContentControlBeforeDeleteXsdString
		{
			get
			{
				return base.GetElement<EventDocContentControlBeforeDeleteXsdString>(7);
			}
			set
			{
				base.SetElement<EventDocContentControlBeforeDeleteXsdString>(7, value);
			}
		}

		// Token: 0x17005126 RID: 20774
		// (get) Token: 0x0601138E RID: 70542 RVA: 0x002EBD24 File Offset: 0x002E9F24
		// (set) Token: 0x0601138F RID: 70543 RVA: 0x002EBD2D File Offset: 0x002E9F2D
		public EventDocContentControlOnExistXsdString EventDocContentControlOnExistXsdString
		{
			get
			{
				return base.GetElement<EventDocContentControlOnExistXsdString>(8);
			}
			set
			{
				base.SetElement<EventDocContentControlOnExistXsdString>(8, value);
			}
		}

		// Token: 0x17005127 RID: 20775
		// (get) Token: 0x06011390 RID: 70544 RVA: 0x002EBD37 File Offset: 0x002E9F37
		// (set) Token: 0x06011391 RID: 70545 RVA: 0x002EBD41 File Offset: 0x002E9F41
		public EventDocContentControlOnEnterXsdString EventDocContentControlOnEnterXsdString
		{
			get
			{
				return base.GetElement<EventDocContentControlOnEnterXsdString>(9);
			}
			set
			{
				base.SetElement<EventDocContentControlOnEnterXsdString>(9, value);
			}
		}

		// Token: 0x17005128 RID: 20776
		// (get) Token: 0x06011392 RID: 70546 RVA: 0x002EBD4C File Offset: 0x002E9F4C
		// (set) Token: 0x06011393 RID: 70547 RVA: 0x002EBD56 File Offset: 0x002E9F56
		public EventDocStoreUpdateXsdString EventDocStoreUpdateXsdString
		{
			get
			{
				return base.GetElement<EventDocStoreUpdateXsdString>(10);
			}
			set
			{
				base.SetElement<EventDocStoreUpdateXsdString>(10, value);
			}
		}

		// Token: 0x17005129 RID: 20777
		// (get) Token: 0x06011394 RID: 70548 RVA: 0x002EBD61 File Offset: 0x002E9F61
		// (set) Token: 0x06011395 RID: 70549 RVA: 0x002EBD6B File Offset: 0x002E9F6B
		public EventDocContentControlUpdateXsdString EventDocContentControlUpdateXsdString
		{
			get
			{
				return base.GetElement<EventDocContentControlUpdateXsdString>(11);
			}
			set
			{
				base.SetElement<EventDocContentControlUpdateXsdString>(11, value);
			}
		}

		// Token: 0x1700512A RID: 20778
		// (get) Token: 0x06011396 RID: 70550 RVA: 0x002EBD76 File Offset: 0x002E9F76
		// (set) Token: 0x06011397 RID: 70551 RVA: 0x002EBD80 File Offset: 0x002E9F80
		public EventDocBuildingBlockAfterInsertXsdString EventDocBuildingBlockAfterInsertXsdString
		{
			get
			{
				return base.GetElement<EventDocBuildingBlockAfterInsertXsdString>(12);
			}
			set
			{
				base.SetElement<EventDocBuildingBlockAfterInsertXsdString>(12, value);
			}
		}

		// Token: 0x06011398 RID: 70552 RVA: 0x002EBD8B File Offset: 0x002E9F8B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DocEvents>(deep);
		}

		// Token: 0x040078B9 RID: 30905
		private const string tagName = "docEvents";

		// Token: 0x040078BA RID: 30906
		private const byte tagNsId = 33;

		// Token: 0x040078BB RID: 30907
		internal const int ElementTypeIdConst = 12562;

		// Token: 0x040078BC RID: 30908
		private static readonly string[] eleTagNames = new string[]
		{
			"eventDocNew", "eventDocOpen", "eventDocClose", "eventDocSync", "eventDocXmlAfterInsert", "eventDocXmlBeforeDelete", "eventDocContentControlAfterInsert", "eventDocContentControlBeforeDelete", "eventDocContentControlOnExit", "eventDocContentControlOnEnter",
			"eventDocStoreUpdate", "eventDocContentControlContentUpdate", "eventDocBuildingBlockAfterInsert"
		};

		// Token: 0x040078BD RID: 30909
		private static readonly byte[] eleNamespaceIds = new byte[]
		{
			33, 33, 33, 33, 33, 33, 33, 33, 33, 33,
			33, 33, 33
		};
	}
}
