using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office.CustomXsn
{
	// Token: 0x020022B1 RID: 8881
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(XsnLocation))]
	[ChildElementInfo(typeof(OpenByDefault))]
	[ChildElementInfo(typeof(CachedView))]
	[ChildElementInfo(typeof(Scope))]
	internal class CustomXsn : OpenXmlCompositeElement
	{
		// Token: 0x17004154 RID: 16724
		// (get) Token: 0x0600F125 RID: 61733 RVA: 0x002D11FC File Offset: 0x002CF3FC
		public override string LocalName
		{
			get
			{
				return "customXsn";
			}
		}

		// Token: 0x17004155 RID: 16725
		// (get) Token: 0x0600F126 RID: 61734 RVA: 0x002D1203 File Offset: 0x002CF403
		internal override byte NamespaceId
		{
			get
			{
				return 39;
			}
		}

		// Token: 0x17004156 RID: 16726
		// (get) Token: 0x0600F127 RID: 61735 RVA: 0x002D1207 File Offset: 0x002CF407
		internal override int ElementTypeId
		{
			get
			{
				return 12635;
			}
		}

		// Token: 0x0600F128 RID: 61736 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600F129 RID: 61737 RVA: 0x00293ECF File Offset: 0x002920CF
		public CustomXsn()
		{
		}

		// Token: 0x0600F12A RID: 61738 RVA: 0x00293ED7 File Offset: 0x002920D7
		public CustomXsn(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600F12B RID: 61739 RVA: 0x00293EE0 File Offset: 0x002920E0
		public CustomXsn(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600F12C RID: 61740 RVA: 0x00293EE9 File Offset: 0x002920E9
		public CustomXsn(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600F12D RID: 61741 RVA: 0x002D1210 File Offset: 0x002CF410
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (39 == namespaceId && "xsnLocation" == name)
			{
				return new XsnLocation();
			}
			if (39 == namespaceId && "cached" == name)
			{
				return new CachedView();
			}
			if (39 == namespaceId && "openByDefault" == name)
			{
				return new OpenByDefault();
			}
			if (39 == namespaceId && "xsnScope" == name)
			{
				return new Scope();
			}
			return null;
		}

		// Token: 0x17004157 RID: 16727
		// (get) Token: 0x0600F12E RID: 61742 RVA: 0x002D127E File Offset: 0x002CF47E
		internal override string[] ElementTagNames
		{
			get
			{
				return CustomXsn.eleTagNames;
			}
		}

		// Token: 0x17004158 RID: 16728
		// (get) Token: 0x0600F12F RID: 61743 RVA: 0x002D1285 File Offset: 0x002CF485
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return CustomXsn.eleNamespaceIds;
			}
		}

		// Token: 0x17004159 RID: 16729
		// (get) Token: 0x0600F130 RID: 61744 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700415A RID: 16730
		// (get) Token: 0x0600F131 RID: 61745 RVA: 0x002D128C File Offset: 0x002CF48C
		// (set) Token: 0x0600F132 RID: 61746 RVA: 0x002D1295 File Offset: 0x002CF495
		public XsnLocation XsnLocation
		{
			get
			{
				return base.GetElement<XsnLocation>(0);
			}
			set
			{
				base.SetElement<XsnLocation>(0, value);
			}
		}

		// Token: 0x1700415B RID: 16731
		// (get) Token: 0x0600F133 RID: 61747 RVA: 0x002D129F File Offset: 0x002CF49F
		// (set) Token: 0x0600F134 RID: 61748 RVA: 0x002D12A8 File Offset: 0x002CF4A8
		public CachedView CachedView
		{
			get
			{
				return base.GetElement<CachedView>(1);
			}
			set
			{
				base.SetElement<CachedView>(1, value);
			}
		}

		// Token: 0x1700415C RID: 16732
		// (get) Token: 0x0600F135 RID: 61749 RVA: 0x002D12B2 File Offset: 0x002CF4B2
		// (set) Token: 0x0600F136 RID: 61750 RVA: 0x002D12BB File Offset: 0x002CF4BB
		public OpenByDefault OpenByDefault
		{
			get
			{
				return base.GetElement<OpenByDefault>(2);
			}
			set
			{
				base.SetElement<OpenByDefault>(2, value);
			}
		}

		// Token: 0x1700415D RID: 16733
		// (get) Token: 0x0600F137 RID: 61751 RVA: 0x002D12C5 File Offset: 0x002CF4C5
		// (set) Token: 0x0600F138 RID: 61752 RVA: 0x002D12CE File Offset: 0x002CF4CE
		public Scope Scope
		{
			get
			{
				return base.GetElement<Scope>(3);
			}
			set
			{
				base.SetElement<Scope>(3, value);
			}
		}

		// Token: 0x0600F139 RID: 61753 RVA: 0x002D12D8 File Offset: 0x002CF4D8
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CustomXsn>(deep);
		}

		// Token: 0x040070AF RID: 28847
		private const string tagName = "customXsn";

		// Token: 0x040070B0 RID: 28848
		private const byte tagNsId = 39;

		// Token: 0x040070B1 RID: 28849
		internal const int ElementTypeIdConst = 12635;

		// Token: 0x040070B2 RID: 28850
		private static readonly string[] eleTagNames = new string[] { "xsnLocation", "cached", "openByDefault", "xsnScope" };

		// Token: 0x040070B3 RID: 28851
		private static readonly byte[] eleNamespaceIds = new byte[] { 39, 39, 39, 39 };
	}
}
