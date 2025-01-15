using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x02002439 RID: 9273
	[ChildElementInfo(typeof(OlapSlicerCacheLevelsData), FileFormatVersions.Office2010)]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(OlapSlicerCacheSelections), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ExtensionList), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class OlapSlicerCache : OpenXmlCompositeElement
	{
		// Token: 0x17005032 RID: 20530
		// (get) Token: 0x06011153 RID: 69971 RVA: 0x002EA691 File Offset: 0x002E8891
		public override string LocalName
		{
			get
			{
				return "olap";
			}
		}

		// Token: 0x17005033 RID: 20531
		// (get) Token: 0x06011154 RID: 69972 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17005034 RID: 20532
		// (get) Token: 0x06011155 RID: 69973 RVA: 0x002EA698 File Offset: 0x002E8898
		internal override int ElementTypeId
		{
			get
			{
				return 12997;
			}
		}

		// Token: 0x06011156 RID: 69974 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17005035 RID: 20533
		// (get) Token: 0x06011157 RID: 69975 RVA: 0x002EA69F File Offset: 0x002E889F
		internal override string[] AttributeTagNames
		{
			get
			{
				return OlapSlicerCache.attributeTagNames;
			}
		}

		// Token: 0x17005036 RID: 20534
		// (get) Token: 0x06011158 RID: 69976 RVA: 0x002EA6A6 File Offset: 0x002E88A6
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return OlapSlicerCache.attributeNamespaceIds;
			}
		}

		// Token: 0x17005037 RID: 20535
		// (get) Token: 0x06011159 RID: 69977 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x0601115A RID: 69978 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "pivotCacheId")]
		public UInt32Value PivotCacheId
		{
			get
			{
				return (UInt32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0601115B RID: 69979 RVA: 0x00293ECF File Offset: 0x002920CF
		public OlapSlicerCache()
		{
		}

		// Token: 0x0601115C RID: 69980 RVA: 0x00293ED7 File Offset: 0x002920D7
		public OlapSlicerCache(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601115D RID: 69981 RVA: 0x00293EE0 File Offset: 0x002920E0
		public OlapSlicerCache(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601115E RID: 69982 RVA: 0x00293EE9 File Offset: 0x002920E9
		public OlapSlicerCache(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601115F RID: 69983 RVA: 0x002EA6B0 File Offset: 0x002E88B0
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (53 == namespaceId && "levels" == name)
			{
				return new OlapSlicerCacheLevelsData();
			}
			if (53 == namespaceId && "selections" == name)
			{
				return new OlapSlicerCacheSelections();
			}
			if (53 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17005038 RID: 20536
		// (get) Token: 0x06011160 RID: 69984 RVA: 0x002EA706 File Offset: 0x002E8906
		internal override string[] ElementTagNames
		{
			get
			{
				return OlapSlicerCache.eleTagNames;
			}
		}

		// Token: 0x17005039 RID: 20537
		// (get) Token: 0x06011161 RID: 69985 RVA: 0x002EA70D File Offset: 0x002E890D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return OlapSlicerCache.eleNamespaceIds;
			}
		}

		// Token: 0x1700503A RID: 20538
		// (get) Token: 0x06011162 RID: 69986 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700503B RID: 20539
		// (get) Token: 0x06011163 RID: 69987 RVA: 0x002EA714 File Offset: 0x002E8914
		// (set) Token: 0x06011164 RID: 69988 RVA: 0x002EA71D File Offset: 0x002E891D
		public OlapSlicerCacheLevelsData OlapSlicerCacheLevelsData
		{
			get
			{
				return base.GetElement<OlapSlicerCacheLevelsData>(0);
			}
			set
			{
				base.SetElement<OlapSlicerCacheLevelsData>(0, value);
			}
		}

		// Token: 0x1700503C RID: 20540
		// (get) Token: 0x06011165 RID: 69989 RVA: 0x002EA727 File Offset: 0x002E8927
		// (set) Token: 0x06011166 RID: 69990 RVA: 0x002EA730 File Offset: 0x002E8930
		public OlapSlicerCacheSelections OlapSlicerCacheSelections
		{
			get
			{
				return base.GetElement<OlapSlicerCacheSelections>(1);
			}
			set
			{
				base.SetElement<OlapSlicerCacheSelections>(1, value);
			}
		}

		// Token: 0x1700503D RID: 20541
		// (get) Token: 0x06011167 RID: 69991 RVA: 0x002E7546 File Offset: 0x002E5746
		// (set) Token: 0x06011168 RID: 69992 RVA: 0x002E754F File Offset: 0x002E574F
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(2);
			}
			set
			{
				base.SetElement<ExtensionList>(2, value);
			}
		}

		// Token: 0x06011169 RID: 69993 RVA: 0x002EA73A File Offset: 0x002E893A
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "pivotCacheId" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601116A RID: 69994 RVA: 0x002EA75A File Offset: 0x002E895A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<OlapSlicerCache>(deep);
		}

		// Token: 0x0601116B RID: 69995 RVA: 0x002EA764 File Offset: 0x002E8964
		// Note: this type is marked as 'beforefieldinit'.
		static OlapSlicerCache()
		{
			byte[] array = new byte[1];
			OlapSlicerCache.attributeNamespaceIds = array;
			OlapSlicerCache.eleTagNames = new string[] { "levels", "selections", "extLst" };
			OlapSlicerCache.eleNamespaceIds = new byte[] { 53, 53, 53 };
		}

		// Token: 0x04007792 RID: 30610
		private const string tagName = "olap";

		// Token: 0x04007793 RID: 30611
		private const byte tagNsId = 53;

		// Token: 0x04007794 RID: 30612
		internal const int ElementTypeIdConst = 12997;

		// Token: 0x04007795 RID: 30613
		private static string[] attributeTagNames = new string[] { "pivotCacheId" };

		// Token: 0x04007796 RID: 30614
		private static byte[] attributeNamespaceIds;

		// Token: 0x04007797 RID: 30615
		private static readonly string[] eleTagNames;

		// Token: 0x04007798 RID: 30616
		private static readonly byte[] eleNamespaceIds;
	}
}
