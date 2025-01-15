using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office.CustomUI
{
	// Token: 0x0200228C RID: 8844
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(UnsizedButton))]
	internal class DialogBoxLauncher : OpenXmlCompositeElement
	{
		// Token: 0x1700405C RID: 16476
		// (get) Token: 0x0600EEE2 RID: 61154 RVA: 0x002CF66F File Offset: 0x002CD86F
		public override string LocalName
		{
			get
			{
				return "dialogBoxLauncher";
			}
		}

		// Token: 0x1700405D RID: 16477
		// (get) Token: 0x0600EEE3 RID: 61155 RVA: 0x002C8749 File Offset: 0x002C6949
		internal override byte NamespaceId
		{
			get
			{
				return 34;
			}
		}

		// Token: 0x1700405E RID: 16478
		// (get) Token: 0x0600EEE4 RID: 61156 RVA: 0x002CF676 File Offset: 0x002CD876
		internal override int ElementTypeId
		{
			get
			{
				return 12603;
			}
		}

		// Token: 0x0600EEE5 RID: 61157 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600EEE6 RID: 61158 RVA: 0x00293ECF File Offset: 0x002920CF
		public DialogBoxLauncher()
		{
		}

		// Token: 0x0600EEE7 RID: 61159 RVA: 0x00293ED7 File Offset: 0x002920D7
		public DialogBoxLauncher(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600EEE8 RID: 61160 RVA: 0x00293EE0 File Offset: 0x002920E0
		public DialogBoxLauncher(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600EEE9 RID: 61161 RVA: 0x00293EE9 File Offset: 0x002920E9
		public DialogBoxLauncher(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600EEEA RID: 61162 RVA: 0x002CF67D File Offset: 0x002CD87D
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (34 == namespaceId && "button" == name)
			{
				return new UnsizedButton();
			}
			return null;
		}

		// Token: 0x1700405F RID: 16479
		// (get) Token: 0x0600EEEB RID: 61163 RVA: 0x002CF698 File Offset: 0x002CD898
		internal override string[] ElementTagNames
		{
			get
			{
				return DialogBoxLauncher.eleTagNames;
			}
		}

		// Token: 0x17004060 RID: 16480
		// (get) Token: 0x0600EEEC RID: 61164 RVA: 0x002CF69F File Offset: 0x002CD89F
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return DialogBoxLauncher.eleNamespaceIds;
			}
		}

		// Token: 0x17004061 RID: 16481
		// (get) Token: 0x0600EEED RID: 61165 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17004062 RID: 16482
		// (get) Token: 0x0600EEEE RID: 61166 RVA: 0x002CF6A6 File Offset: 0x002CD8A6
		// (set) Token: 0x0600EEEF RID: 61167 RVA: 0x002CF6AF File Offset: 0x002CD8AF
		public UnsizedButton UnsizedButton
		{
			get
			{
				return base.GetElement<UnsizedButton>(0);
			}
			set
			{
				base.SetElement<UnsizedButton>(0, value);
			}
		}

		// Token: 0x0600EEF0 RID: 61168 RVA: 0x002CF6B9 File Offset: 0x002CD8B9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DialogBoxLauncher>(deep);
		}

		// Token: 0x0400701D RID: 28701
		private const string tagName = "dialogBoxLauncher";

		// Token: 0x0400701E RID: 28702
		private const byte tagNsId = 34;

		// Token: 0x0400701F RID: 28703
		internal const int ElementTypeIdConst = 12603;

		// Token: 0x04007020 RID: 28704
		private static readonly string[] eleTagNames = new string[] { "button" };

		// Token: 0x04007021 RID: 28705
		private static readonly byte[] eleNamespaceIds = new byte[] { 34 };
	}
}
