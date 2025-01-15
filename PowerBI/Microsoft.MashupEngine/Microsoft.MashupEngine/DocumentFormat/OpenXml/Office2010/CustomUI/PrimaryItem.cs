using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x020022FD RID: 8957
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(BackstageRegularButton), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(BackstagePrimaryMenu), FileFormatVersions.Office2010)]
	internal class PrimaryItem : OpenXmlCompositeElement
	{
		// Token: 0x17004754 RID: 18260
		// (get) Token: 0x0600FDBD RID: 64957 RVA: 0x002DC8CF File Offset: 0x002DAACF
		public override string LocalName
		{
			get
			{
				return "primaryItem";
			}
		}

		// Token: 0x17004755 RID: 18261
		// (get) Token: 0x0600FDBE RID: 64958 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x17004756 RID: 18262
		// (get) Token: 0x0600FDBF RID: 64959 RVA: 0x002DC8D6 File Offset: 0x002DAAD6
		internal override int ElementTypeId
		{
			get
			{
				return 13100;
			}
		}

		// Token: 0x0600FDC0 RID: 64960 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x0600FDC1 RID: 64961 RVA: 0x00293ECF File Offset: 0x002920CF
		public PrimaryItem()
		{
		}

		// Token: 0x0600FDC2 RID: 64962 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PrimaryItem(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600FDC3 RID: 64963 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PrimaryItem(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600FDC4 RID: 64964 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PrimaryItem(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600FDC5 RID: 64965 RVA: 0x002DC8DD File Offset: 0x002DAADD
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (57 == namespaceId && "button" == name)
			{
				return new BackstageRegularButton();
			}
			if (57 == namespaceId && "menu" == name)
			{
				return new BackstagePrimaryMenu();
			}
			return null;
		}

		// Token: 0x17004757 RID: 18263
		// (get) Token: 0x0600FDC6 RID: 64966 RVA: 0x002DC910 File Offset: 0x002DAB10
		internal override string[] ElementTagNames
		{
			get
			{
				return PrimaryItem.eleTagNames;
			}
		}

		// Token: 0x17004758 RID: 18264
		// (get) Token: 0x0600FDC7 RID: 64967 RVA: 0x002DC917 File Offset: 0x002DAB17
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return PrimaryItem.eleNamespaceIds;
			}
		}

		// Token: 0x17004759 RID: 18265
		// (get) Token: 0x0600FDC8 RID: 64968 RVA: 0x000023C4 File Offset: 0x000005C4
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneChoice;
			}
		}

		// Token: 0x1700475A RID: 18266
		// (get) Token: 0x0600FDC9 RID: 64969 RVA: 0x002DC91E File Offset: 0x002DAB1E
		// (set) Token: 0x0600FDCA RID: 64970 RVA: 0x002DC927 File Offset: 0x002DAB27
		public BackstageRegularButton BackstageRegularButton
		{
			get
			{
				return base.GetElement<BackstageRegularButton>(0);
			}
			set
			{
				base.SetElement<BackstageRegularButton>(0, value);
			}
		}

		// Token: 0x1700475B RID: 18267
		// (get) Token: 0x0600FDCB RID: 64971 RVA: 0x002DC931 File Offset: 0x002DAB31
		// (set) Token: 0x0600FDCC RID: 64972 RVA: 0x002DC93A File Offset: 0x002DAB3A
		public BackstagePrimaryMenu BackstagePrimaryMenu
		{
			get
			{
				return base.GetElement<BackstagePrimaryMenu>(1);
			}
			set
			{
				base.SetElement<BackstagePrimaryMenu>(1, value);
			}
		}

		// Token: 0x0600FDCD RID: 64973 RVA: 0x002DC944 File Offset: 0x002DAB44
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PrimaryItem>(deep);
		}

		// Token: 0x0400720C RID: 29196
		private const string tagName = "primaryItem";

		// Token: 0x0400720D RID: 29197
		private const byte tagNsId = 57;

		// Token: 0x0400720E RID: 29198
		internal const int ElementTypeIdConst = 13100;

		// Token: 0x0400720F RID: 29199
		private static readonly string[] eleTagNames = new string[] { "button", "menu" };

		// Token: 0x04007210 RID: 29200
		private static readonly byte[] eleNamespaceIds = new byte[] { 57, 57 };
	}
}
