using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.Word;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F52 RID: 12114
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(RunProperties))]
	[ChildElementInfo(typeof(SdtAlias))]
	[ChildElementInfo(typeof(Lock))]
	[ChildElementInfo(typeof(SdtPlaceholder))]
	[ChildElementInfo(typeof(ShowingPlaceholder))]
	[ChildElementInfo(typeof(DataBinding))]
	[ChildElementInfo(typeof(TemporarySdt))]
	[ChildElementInfo(typeof(SdtId))]
	[ChildElementInfo(typeof(Tag))]
	[ChildElementInfo(typeof(SdtContentEquation))]
	[ChildElementInfo(typeof(SdtContentComboBox))]
	[ChildElementInfo(typeof(SdtContentDate))]
	[ChildElementInfo(typeof(SdtContentDocPartObject))]
	[ChildElementInfo(typeof(SdtContentDocPartList))]
	[ChildElementInfo(typeof(SdtContentDropDownList))]
	[ChildElementInfo(typeof(SdtContentPicture))]
	[ChildElementInfo(typeof(SdtContentRichText))]
	[ChildElementInfo(typeof(SdtContentText))]
	[ChildElementInfo(typeof(SdtContentCitation))]
	[ChildElementInfo(typeof(SdtContentGroup))]
	[ChildElementInfo(typeof(SdtContentBibliography))]
	[ChildElementInfo(typeof(EntityPickerEmpty), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(SdtContentCheckBox), FileFormatVersions.Office2010)]
	internal class SdtProperties : OpenXmlCompositeElement
	{
		// Token: 0x17009026 RID: 36902
		// (get) Token: 0x0601A039 RID: 106553 RVA: 0x0035B2C1 File Offset: 0x003594C1
		public override string LocalName
		{
			get
			{
				return "sdtPr";
			}
		}

		// Token: 0x17009027 RID: 36903
		// (get) Token: 0x0601A03A RID: 106554 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17009028 RID: 36904
		// (get) Token: 0x0601A03B RID: 106555 RVA: 0x0035B2C8 File Offset: 0x003594C8
		internal override int ElementTypeId
		{
			get
			{
				return 11769;
			}
		}

		// Token: 0x0601A03C RID: 106556 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601A03D RID: 106557 RVA: 0x00293ECF File Offset: 0x002920CF
		public SdtProperties()
		{
		}

		// Token: 0x0601A03E RID: 106558 RVA: 0x00293ED7 File Offset: 0x002920D7
		public SdtProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A03F RID: 106559 RVA: 0x00293EE0 File Offset: 0x002920E0
		public SdtProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A040 RID: 106560 RVA: 0x00293EE9 File Offset: 0x002920E9
		public SdtProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601A041 RID: 106561 RVA: 0x0035B2D0 File Offset: 0x003594D0
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "rPr" == name)
			{
				return new RunProperties();
			}
			if (23 == namespaceId && "alias" == name)
			{
				return new SdtAlias();
			}
			if (23 == namespaceId && "lock" == name)
			{
				return new Lock();
			}
			if (23 == namespaceId && "placeholder" == name)
			{
				return new SdtPlaceholder();
			}
			if (23 == namespaceId && "showingPlcHdr" == name)
			{
				return new ShowingPlaceholder();
			}
			if (23 == namespaceId && "dataBinding" == name)
			{
				return new DataBinding();
			}
			if (23 == namespaceId && "temporary" == name)
			{
				return new TemporarySdt();
			}
			if (23 == namespaceId && "id" == name)
			{
				return new SdtId();
			}
			if (23 == namespaceId && "tag" == name)
			{
				return new Tag();
			}
			if (23 == namespaceId && "equation" == name)
			{
				return new SdtContentEquation();
			}
			if (23 == namespaceId && "comboBox" == name)
			{
				return new SdtContentComboBox();
			}
			if (23 == namespaceId && "date" == name)
			{
				return new SdtContentDate();
			}
			if (23 == namespaceId && "docPartObj" == name)
			{
				return new SdtContentDocPartObject();
			}
			if (23 == namespaceId && "docPartList" == name)
			{
				return new SdtContentDocPartList();
			}
			if (23 == namespaceId && "dropDownList" == name)
			{
				return new SdtContentDropDownList();
			}
			if (23 == namespaceId && "picture" == name)
			{
				return new SdtContentPicture();
			}
			if (23 == namespaceId && "richText" == name)
			{
				return new SdtContentRichText();
			}
			if (23 == namespaceId && "text" == name)
			{
				return new SdtContentText();
			}
			if (23 == namespaceId && "citation" == name)
			{
				return new SdtContentCitation();
			}
			if (23 == namespaceId && "group" == name)
			{
				return new SdtContentGroup();
			}
			if (23 == namespaceId && "bibliography" == name)
			{
				return new SdtContentBibliography();
			}
			if (52 == namespaceId && "entityPicker" == name)
			{
				return new EntityPickerEmpty();
			}
			if (52 == namespaceId && "checkbox" == name)
			{
				return new SdtContentCheckBox();
			}
			return null;
		}

		// Token: 0x0601A042 RID: 106562 RVA: 0x0035B506 File Offset: 0x00359706
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SdtProperties>(deep);
		}

		// Token: 0x0400AB67 RID: 43879
		private const string tagName = "sdtPr";

		// Token: 0x0400AB68 RID: 43880
		private const byte tagNsId = 23;

		// Token: 0x0400AB69 RID: 43881
		internal const int ElementTypeIdConst = 11769;
	}
}
