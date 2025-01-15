using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office.CustomDocumentInformationPanel
{
	// Token: 0x020022AA RID: 8874
	[ChildElementInfo(typeof(DefaultPropertyEditorNamespace))]
	[ChildElementInfo(typeof(CustomPropertyEditor))]
	[ChildElementInfo(typeof(ShowOnOpen))]
	[GeneratedCode("DomGen", "2.0")]
	internal class CustomPropertyEditors : OpenXmlCompositeElement
	{
		// Token: 0x1700412B RID: 16683
		// (get) Token: 0x0600F0C3 RID: 61635 RVA: 0x002D0E08 File Offset: 0x002CF008
		public override string LocalName
		{
			get
			{
				return "customPropertyEditors";
			}
		}

		// Token: 0x1700412C RID: 16684
		// (get) Token: 0x0600F0C4 RID: 61636 RVA: 0x002D0E0F File Offset: 0x002CF00F
		internal override byte NamespaceId
		{
			get
			{
				return 37;
			}
		}

		// Token: 0x1700412D RID: 16685
		// (get) Token: 0x0600F0C5 RID: 61637 RVA: 0x002D0E13 File Offset: 0x002CF013
		internal override int ElementTypeId
		{
			get
			{
				return 12628;
			}
		}

		// Token: 0x0600F0C6 RID: 61638 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600F0C7 RID: 61639 RVA: 0x00293ECF File Offset: 0x002920CF
		public CustomPropertyEditors()
		{
		}

		// Token: 0x0600F0C8 RID: 61640 RVA: 0x00293ED7 File Offset: 0x002920D7
		public CustomPropertyEditors(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600F0C9 RID: 61641 RVA: 0x00293EE0 File Offset: 0x002920E0
		public CustomPropertyEditors(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600F0CA RID: 61642 RVA: 0x00293EE9 File Offset: 0x002920E9
		public CustomPropertyEditors(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600F0CB RID: 61643 RVA: 0x002D0E1C File Offset: 0x002CF01C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (37 == namespaceId && "showOnOpen" == name)
			{
				return new ShowOnOpen();
			}
			if (37 == namespaceId && "defaultPropertyEditorNamespace" == name)
			{
				return new DefaultPropertyEditorNamespace();
			}
			if (37 == namespaceId && "customPropertyEditor" == name)
			{
				return new CustomPropertyEditor();
			}
			return null;
		}

		// Token: 0x1700412E RID: 16686
		// (get) Token: 0x0600F0CC RID: 61644 RVA: 0x002D0E72 File Offset: 0x002CF072
		internal override string[] ElementTagNames
		{
			get
			{
				return CustomPropertyEditors.eleTagNames;
			}
		}

		// Token: 0x1700412F RID: 16687
		// (get) Token: 0x0600F0CD RID: 61645 RVA: 0x002D0E79 File Offset: 0x002CF079
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return CustomPropertyEditors.eleNamespaceIds;
			}
		}

		// Token: 0x17004130 RID: 16688
		// (get) Token: 0x0600F0CE RID: 61646 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17004131 RID: 16689
		// (get) Token: 0x0600F0CF RID: 61647 RVA: 0x002D0E80 File Offset: 0x002CF080
		// (set) Token: 0x0600F0D0 RID: 61648 RVA: 0x002D0E89 File Offset: 0x002CF089
		public ShowOnOpen ShowOnOpen
		{
			get
			{
				return base.GetElement<ShowOnOpen>(0);
			}
			set
			{
				base.SetElement<ShowOnOpen>(0, value);
			}
		}

		// Token: 0x17004132 RID: 16690
		// (get) Token: 0x0600F0D1 RID: 61649 RVA: 0x002D0E93 File Offset: 0x002CF093
		// (set) Token: 0x0600F0D2 RID: 61650 RVA: 0x002D0E9C File Offset: 0x002CF09C
		public DefaultPropertyEditorNamespace DefaultPropertyEditorNamespace
		{
			get
			{
				return base.GetElement<DefaultPropertyEditorNamespace>(1);
			}
			set
			{
				base.SetElement<DefaultPropertyEditorNamespace>(1, value);
			}
		}

		// Token: 0x0600F0D3 RID: 61651 RVA: 0x002D0EA6 File Offset: 0x002CF0A6
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CustomPropertyEditors>(deep);
		}

		// Token: 0x04007094 RID: 28820
		private const string tagName = "customPropertyEditors";

		// Token: 0x04007095 RID: 28821
		private const byte tagNsId = 37;

		// Token: 0x04007096 RID: 28822
		internal const int ElementTypeIdConst = 12628;

		// Token: 0x04007097 RID: 28823
		private static readonly string[] eleTagNames = new string[] { "showOnOpen", "defaultPropertyEditorNamespace", "customPropertyEditor" };

		// Token: 0x04007098 RID: 28824
		private static readonly byte[] eleNamespaceIds = new byte[] { 37, 37, 37 };
	}
}
