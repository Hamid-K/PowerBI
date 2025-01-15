using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A8B RID: 10891
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(CommonViewProperties))]
	[ChildElementInfo(typeof(ExtensionList))]
	internal class NotesTextViewProperties : OpenXmlCompositeElement
	{
		// Token: 0x170073A4 RID: 29604
		// (get) Token: 0x06016154 RID: 90452 RVA: 0x00326684 File Offset: 0x00324884
		public override string LocalName
		{
			get
			{
				return "notesTextViewPr";
			}
		}

		// Token: 0x170073A5 RID: 29605
		// (get) Token: 0x06016155 RID: 90453 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x170073A6 RID: 29606
		// (get) Token: 0x06016156 RID: 90454 RVA: 0x0032668B File Offset: 0x0032488B
		internal override int ElementTypeId
		{
			get
			{
				return 12304;
			}
		}

		// Token: 0x06016157 RID: 90455 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06016158 RID: 90456 RVA: 0x00293ECF File Offset: 0x002920CF
		public NotesTextViewProperties()
		{
		}

		// Token: 0x06016159 RID: 90457 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NotesTextViewProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601615A RID: 90458 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NotesTextViewProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601615B RID: 90459 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NotesTextViewProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601615C RID: 90460 RVA: 0x00326692 File Offset: 0x00324892
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "cViewPr" == name)
			{
				return new CommonViewProperties();
			}
			if (24 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x170073A7 RID: 29607
		// (get) Token: 0x0601615D RID: 90461 RVA: 0x003266C5 File Offset: 0x003248C5
		internal override string[] ElementTagNames
		{
			get
			{
				return NotesTextViewProperties.eleTagNames;
			}
		}

		// Token: 0x170073A8 RID: 29608
		// (get) Token: 0x0601615E RID: 90462 RVA: 0x003266CC File Offset: 0x003248CC
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NotesTextViewProperties.eleNamespaceIds;
			}
		}

		// Token: 0x170073A9 RID: 29609
		// (get) Token: 0x0601615F RID: 90463 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170073AA RID: 29610
		// (get) Token: 0x06016160 RID: 90464 RVA: 0x00326212 File Offset: 0x00324412
		// (set) Token: 0x06016161 RID: 90465 RVA: 0x0032621B File Offset: 0x0032441B
		public CommonViewProperties CommonViewProperties
		{
			get
			{
				return base.GetElement<CommonViewProperties>(0);
			}
			set
			{
				base.SetElement<CommonViewProperties>(0, value);
			}
		}

		// Token: 0x170073AB RID: 29611
		// (get) Token: 0x06016162 RID: 90466 RVA: 0x00323F93 File Offset: 0x00322193
		// (set) Token: 0x06016163 RID: 90467 RVA: 0x00323F9C File Offset: 0x0032219C
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

		// Token: 0x06016164 RID: 90468 RVA: 0x003266D3 File Offset: 0x003248D3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NotesTextViewProperties>(deep);
		}

		// Token: 0x04009620 RID: 38432
		private const string tagName = "notesTextViewPr";

		// Token: 0x04009621 RID: 38433
		private const byte tagNsId = 24;

		// Token: 0x04009622 RID: 38434
		internal const int ElementTypeIdConst = 12304;

		// Token: 0x04009623 RID: 38435
		private static readonly string[] eleTagNames = new string[] { "cViewPr", "extLst" };

		// Token: 0x04009624 RID: 38436
		private static readonly byte[] eleNamespaceIds = new byte[] { 24, 24 };
	}
}
