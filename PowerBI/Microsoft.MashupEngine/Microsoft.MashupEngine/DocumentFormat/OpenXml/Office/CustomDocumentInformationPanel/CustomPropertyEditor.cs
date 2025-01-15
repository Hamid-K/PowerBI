using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office.CustomDocumentInformationPanel
{
	// Token: 0x020022AF RID: 8879
	[ChildElementInfo(typeof(PropertyEditorNamespace))]
	[ChildElementInfo(typeof(XsnFileLocation))]
	[GeneratedCode("DomGen", "2.0")]
	internal class CustomPropertyEditor : OpenXmlCompositeElement
	{
		// Token: 0x1700413F RID: 16703
		// (get) Token: 0x0600F0F5 RID: 61685 RVA: 0x002D0FC8 File Offset: 0x002CF1C8
		public override string LocalName
		{
			get
			{
				return "customPropertyEditor";
			}
		}

		// Token: 0x17004140 RID: 16704
		// (get) Token: 0x0600F0F6 RID: 61686 RVA: 0x002D0E0F File Offset: 0x002CF00F
		internal override byte NamespaceId
		{
			get
			{
				return 37;
			}
		}

		// Token: 0x17004141 RID: 16705
		// (get) Token: 0x0600F0F7 RID: 61687 RVA: 0x002D0FCF File Offset: 0x002CF1CF
		internal override int ElementTypeId
		{
			get
			{
				return 12633;
			}
		}

		// Token: 0x0600F0F8 RID: 61688 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600F0F9 RID: 61689 RVA: 0x00293ECF File Offset: 0x002920CF
		public CustomPropertyEditor()
		{
		}

		// Token: 0x0600F0FA RID: 61690 RVA: 0x00293ED7 File Offset: 0x002920D7
		public CustomPropertyEditor(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600F0FB RID: 61691 RVA: 0x00293EE0 File Offset: 0x002920E0
		public CustomPropertyEditor(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600F0FC RID: 61692 RVA: 0x00293EE9 File Offset: 0x002920E9
		public CustomPropertyEditor(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600F0FD RID: 61693 RVA: 0x002D0FD6 File Offset: 0x002CF1D6
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (37 == namespaceId && "XMLNamespace" == name)
			{
				return new PropertyEditorNamespace();
			}
			if (37 == namespaceId && "XSNLocation" == name)
			{
				return new XsnFileLocation();
			}
			return null;
		}

		// Token: 0x17004142 RID: 16706
		// (get) Token: 0x0600F0FE RID: 61694 RVA: 0x002D1009 File Offset: 0x002CF209
		internal override string[] ElementTagNames
		{
			get
			{
				return CustomPropertyEditor.eleTagNames;
			}
		}

		// Token: 0x17004143 RID: 16707
		// (get) Token: 0x0600F0FF RID: 61695 RVA: 0x002D1010 File Offset: 0x002CF210
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return CustomPropertyEditor.eleNamespaceIds;
			}
		}

		// Token: 0x17004144 RID: 16708
		// (get) Token: 0x0600F100 RID: 61696 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17004145 RID: 16709
		// (get) Token: 0x0600F101 RID: 61697 RVA: 0x002D1017 File Offset: 0x002CF217
		// (set) Token: 0x0600F102 RID: 61698 RVA: 0x002D1020 File Offset: 0x002CF220
		public PropertyEditorNamespace PropertyEditorNamespace
		{
			get
			{
				return base.GetElement<PropertyEditorNamespace>(0);
			}
			set
			{
				base.SetElement<PropertyEditorNamespace>(0, value);
			}
		}

		// Token: 0x17004146 RID: 16710
		// (get) Token: 0x0600F103 RID: 61699 RVA: 0x002D102A File Offset: 0x002CF22A
		// (set) Token: 0x0600F104 RID: 61700 RVA: 0x002D1033 File Offset: 0x002CF233
		public XsnFileLocation XsnFileLocation
		{
			get
			{
				return base.GetElement<XsnFileLocation>(1);
			}
			set
			{
				base.SetElement<XsnFileLocation>(1, value);
			}
		}

		// Token: 0x0600F105 RID: 61701 RVA: 0x002D103D File Offset: 0x002CF23D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CustomPropertyEditor>(deep);
		}

		// Token: 0x040070A5 RID: 28837
		private const string tagName = "customPropertyEditor";

		// Token: 0x040070A6 RID: 28838
		private const byte tagNsId = 37;

		// Token: 0x040070A7 RID: 28839
		internal const int ElementTypeIdConst = 12633;

		// Token: 0x040070A8 RID: 28840
		private static readonly string[] eleTagNames = new string[] { "XMLNamespace", "XSNLocation" };

		// Token: 0x040070A9 RID: 28841
		private static readonly byte[] eleNamespaceIds = new byte[] { 37, 37 };
	}
}
