using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02003005 RID: 12293
	[ChildElementInfo(typeof(DocPartReference))]
	[GeneratedCode("DomGen", "2.0")]
	internal class SdtPlaceholder : OpenXmlCompositeElement
	{
		// Token: 0x17009630 RID: 38448
		// (get) Token: 0x0601AD24 RID: 109860 RVA: 0x003447E7 File Offset: 0x003429E7
		public override string LocalName
		{
			get
			{
				return "placeholder";
			}
		}

		// Token: 0x17009631 RID: 38449
		// (get) Token: 0x0601AD25 RID: 109861 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17009632 RID: 38450
		// (get) Token: 0x0601AD26 RID: 109862 RVA: 0x003681E8 File Offset: 0x003663E8
		internal override int ElementTypeId
		{
			get
			{
				return 12141;
			}
		}

		// Token: 0x0601AD27 RID: 109863 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601AD28 RID: 109864 RVA: 0x00293ECF File Offset: 0x002920CF
		public SdtPlaceholder()
		{
		}

		// Token: 0x0601AD29 RID: 109865 RVA: 0x00293ED7 File Offset: 0x002920D7
		public SdtPlaceholder(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601AD2A RID: 109866 RVA: 0x00293EE0 File Offset: 0x002920E0
		public SdtPlaceholder(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601AD2B RID: 109867 RVA: 0x00293EE9 File Offset: 0x002920E9
		public SdtPlaceholder(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601AD2C RID: 109868 RVA: 0x003681EF File Offset: 0x003663EF
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "docPart" == name)
			{
				return new DocPartReference();
			}
			return null;
		}

		// Token: 0x17009633 RID: 38451
		// (get) Token: 0x0601AD2D RID: 109869 RVA: 0x0036820A File Offset: 0x0036640A
		internal override string[] ElementTagNames
		{
			get
			{
				return SdtPlaceholder.eleTagNames;
			}
		}

		// Token: 0x17009634 RID: 38452
		// (get) Token: 0x0601AD2E RID: 109870 RVA: 0x00368211 File Offset: 0x00366411
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return SdtPlaceholder.eleNamespaceIds;
			}
		}

		// Token: 0x17009635 RID: 38453
		// (get) Token: 0x0601AD2F RID: 109871 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17009636 RID: 38454
		// (get) Token: 0x0601AD30 RID: 109872 RVA: 0x00368218 File Offset: 0x00366418
		// (set) Token: 0x0601AD31 RID: 109873 RVA: 0x00368221 File Offset: 0x00366421
		public DocPartReference DocPartReference
		{
			get
			{
				return base.GetElement<DocPartReference>(0);
			}
			set
			{
				base.SetElement<DocPartReference>(0, value);
			}
		}

		// Token: 0x0601AD32 RID: 109874 RVA: 0x0036822B File Offset: 0x0036642B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SdtPlaceholder>(deep);
		}

		// Token: 0x0400AE66 RID: 44646
		private const string tagName = "placeholder";

		// Token: 0x0400AE67 RID: 44647
		private const byte tagNsId = 23;

		// Token: 0x0400AE68 RID: 44648
		internal const int ElementTypeIdConst = 12141;

		// Token: 0x0400AE69 RID: 44649
		private static readonly string[] eleTagNames = new string[] { "docPart" };

		// Token: 0x0400AE6A RID: 44650
		private static readonly byte[] eleNamespaceIds = new byte[] { 23 };
	}
}
