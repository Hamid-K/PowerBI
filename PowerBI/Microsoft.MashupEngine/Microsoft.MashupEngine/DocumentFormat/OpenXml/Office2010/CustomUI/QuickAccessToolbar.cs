using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x020022F3 RID: 8947
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(DocumentControlsQatItems), FileFormatVersions.Office2010)]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(SharedControlsQatItems), FileFormatVersions.Office2010)]
	internal class QuickAccessToolbar : OpenXmlCompositeElement
	{
		// Token: 0x170046FA RID: 18170
		// (get) Token: 0x0600FCF2 RID: 64754 RVA: 0x002D04FB File Offset: 0x002CE6FB
		public override string LocalName
		{
			get
			{
				return "qat";
			}
		}

		// Token: 0x170046FB RID: 18171
		// (get) Token: 0x0600FCF3 RID: 64755 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x170046FC RID: 18172
		// (get) Token: 0x0600FCF4 RID: 64756 RVA: 0x002DBEC7 File Offset: 0x002DA0C7
		internal override int ElementTypeId
		{
			get
			{
				return 13091;
			}
		}

		// Token: 0x0600FCF5 RID: 64757 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x0600FCF6 RID: 64758 RVA: 0x00293ECF File Offset: 0x002920CF
		public QuickAccessToolbar()
		{
		}

		// Token: 0x0600FCF7 RID: 64759 RVA: 0x00293ED7 File Offset: 0x002920D7
		public QuickAccessToolbar(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600FCF8 RID: 64760 RVA: 0x00293EE0 File Offset: 0x002920E0
		public QuickAccessToolbar(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600FCF9 RID: 64761 RVA: 0x00293EE9 File Offset: 0x002920E9
		public QuickAccessToolbar(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600FCFA RID: 64762 RVA: 0x002DBECE File Offset: 0x002DA0CE
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (57 == namespaceId && "sharedControls" == name)
			{
				return new SharedControlsQatItems();
			}
			if (57 == namespaceId && "documentControls" == name)
			{
				return new DocumentControlsQatItems();
			}
			return null;
		}

		// Token: 0x170046FD RID: 18173
		// (get) Token: 0x0600FCFB RID: 64763 RVA: 0x002DBF01 File Offset: 0x002DA101
		internal override string[] ElementTagNames
		{
			get
			{
				return QuickAccessToolbar.eleTagNames;
			}
		}

		// Token: 0x170046FE RID: 18174
		// (get) Token: 0x0600FCFC RID: 64764 RVA: 0x002DBF08 File Offset: 0x002DA108
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return QuickAccessToolbar.eleNamespaceIds;
			}
		}

		// Token: 0x170046FF RID: 18175
		// (get) Token: 0x0600FCFD RID: 64765 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17004700 RID: 18176
		// (get) Token: 0x0600FCFE RID: 64766 RVA: 0x002DBF0F File Offset: 0x002DA10F
		// (set) Token: 0x0600FCFF RID: 64767 RVA: 0x002DBF18 File Offset: 0x002DA118
		public SharedControlsQatItems SharedControlsQatItems
		{
			get
			{
				return base.GetElement<SharedControlsQatItems>(0);
			}
			set
			{
				base.SetElement<SharedControlsQatItems>(0, value);
			}
		}

		// Token: 0x17004701 RID: 18177
		// (get) Token: 0x0600FD00 RID: 64768 RVA: 0x002DBF22 File Offset: 0x002DA122
		// (set) Token: 0x0600FD01 RID: 64769 RVA: 0x002DBF2B File Offset: 0x002DA12B
		public DocumentControlsQatItems DocumentControlsQatItems
		{
			get
			{
				return base.GetElement<DocumentControlsQatItems>(1);
			}
			set
			{
				base.SetElement<DocumentControlsQatItems>(1, value);
			}
		}

		// Token: 0x0600FD02 RID: 64770 RVA: 0x002DBF35 File Offset: 0x002DA135
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<QuickAccessToolbar>(deep);
		}

		// Token: 0x040071E5 RID: 29157
		private const string tagName = "qat";

		// Token: 0x040071E6 RID: 29158
		private const byte tagNsId = 57;

		// Token: 0x040071E7 RID: 29159
		internal const int ElementTypeIdConst = 13091;

		// Token: 0x040071E8 RID: 29160
		private static readonly string[] eleTagNames = new string[] { "sharedControls", "documentControls" };

		// Token: 0x040071E9 RID: 29161
		private static readonly byte[] eleNamespaceIds = new byte[] { 57, 57 };
	}
}
