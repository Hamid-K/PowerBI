using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office.CustomUI
{
	// Token: 0x02002296 RID: 8854
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(SharedQatControls))]
	[ChildElementInfo(typeof(DocumentSpecificQuickAccessToolbarControls))]
	internal class QuickAccessToolbar : OpenXmlCompositeElement
	{
		// Token: 0x170040CD RID: 16589
		// (get) Token: 0x0600EFE1 RID: 61409 RVA: 0x002D04FB File Offset: 0x002CE6FB
		public override string LocalName
		{
			get
			{
				return "qat";
			}
		}

		// Token: 0x170040CE RID: 16590
		// (get) Token: 0x0600EFE2 RID: 61410 RVA: 0x002C8749 File Offset: 0x002C6949
		internal override byte NamespaceId
		{
			get
			{
				return 34;
			}
		}

		// Token: 0x170040CF RID: 16591
		// (get) Token: 0x0600EFE3 RID: 61411 RVA: 0x002D0502 File Offset: 0x002CE702
		internal override int ElementTypeId
		{
			get
			{
				return 12612;
			}
		}

		// Token: 0x0600EFE4 RID: 61412 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600EFE5 RID: 61413 RVA: 0x00293ECF File Offset: 0x002920CF
		public QuickAccessToolbar()
		{
		}

		// Token: 0x0600EFE6 RID: 61414 RVA: 0x00293ED7 File Offset: 0x002920D7
		public QuickAccessToolbar(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600EFE7 RID: 61415 RVA: 0x00293EE0 File Offset: 0x002920E0
		public QuickAccessToolbar(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600EFE8 RID: 61416 RVA: 0x00293EE9 File Offset: 0x002920E9
		public QuickAccessToolbar(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600EFE9 RID: 61417 RVA: 0x002D0509 File Offset: 0x002CE709
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (34 == namespaceId && "sharedControls" == name)
			{
				return new SharedQatControls();
			}
			if (34 == namespaceId && "documentControls" == name)
			{
				return new DocumentSpecificQuickAccessToolbarControls();
			}
			return null;
		}

		// Token: 0x170040D0 RID: 16592
		// (get) Token: 0x0600EFEA RID: 61418 RVA: 0x002D053C File Offset: 0x002CE73C
		internal override string[] ElementTagNames
		{
			get
			{
				return QuickAccessToolbar.eleTagNames;
			}
		}

		// Token: 0x170040D1 RID: 16593
		// (get) Token: 0x0600EFEB RID: 61419 RVA: 0x002D0543 File Offset: 0x002CE743
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return QuickAccessToolbar.eleNamespaceIds;
			}
		}

		// Token: 0x170040D2 RID: 16594
		// (get) Token: 0x0600EFEC RID: 61420 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170040D3 RID: 16595
		// (get) Token: 0x0600EFED RID: 61421 RVA: 0x002D054A File Offset: 0x002CE74A
		// (set) Token: 0x0600EFEE RID: 61422 RVA: 0x002D0553 File Offset: 0x002CE753
		public SharedQatControls SharedQatControls
		{
			get
			{
				return base.GetElement<SharedQatControls>(0);
			}
			set
			{
				base.SetElement<SharedQatControls>(0, value);
			}
		}

		// Token: 0x170040D4 RID: 16596
		// (get) Token: 0x0600EFEF RID: 61423 RVA: 0x002D055D File Offset: 0x002CE75D
		// (set) Token: 0x0600EFF0 RID: 61424 RVA: 0x002D0566 File Offset: 0x002CE766
		public DocumentSpecificQuickAccessToolbarControls DocumentSpecificQuickAccessToolbarControls
		{
			get
			{
				return base.GetElement<DocumentSpecificQuickAccessToolbarControls>(1);
			}
			set
			{
				base.SetElement<DocumentSpecificQuickAccessToolbarControls>(1, value);
			}
		}

		// Token: 0x0600EFF1 RID: 61425 RVA: 0x002D0570 File Offset: 0x002CE770
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<QuickAccessToolbar>(deep);
		}

		// Token: 0x04007044 RID: 28740
		private const string tagName = "qat";

		// Token: 0x04007045 RID: 28741
		private const byte tagNsId = 34;

		// Token: 0x04007046 RID: 28742
		internal const int ElementTypeIdConst = 12612;

		// Token: 0x04007047 RID: 28743
		private static readonly string[] eleTagNames = new string[] { "sharedControls", "documentControls" };

		// Token: 0x04007048 RID: 28744
		private static readonly byte[] eleNamespaceIds = new byte[] { 34, 34 };
	}
}
