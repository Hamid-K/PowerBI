using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x020023F3 RID: 9203
	[ChildElementInfo(typeof(SlicerCachePivotTables), FileFormatVersions.Office2010)]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(SlicerCacheData), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ExtensionList), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class SlicerCacheDefinition : OpenXmlPartRootElement
	{
		// Token: 0x17004E2A RID: 20010
		// (get) Token: 0x06010CC1 RID: 68801 RVA: 0x002E7492 File Offset: 0x002E5692
		public override string LocalName
		{
			get
			{
				return "slicerCacheDefinition";
			}
		}

		// Token: 0x17004E2B RID: 20011
		// (get) Token: 0x06010CC2 RID: 68802 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004E2C RID: 20012
		// (get) Token: 0x06010CC3 RID: 68803 RVA: 0x002E7499 File Offset: 0x002E5699
		internal override int ElementTypeId
		{
			get
			{
				return 12929;
			}
		}

		// Token: 0x06010CC4 RID: 68804 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004E2D RID: 20013
		// (get) Token: 0x06010CC5 RID: 68805 RVA: 0x002E74A0 File Offset: 0x002E56A0
		internal override string[] AttributeTagNames
		{
			get
			{
				return SlicerCacheDefinition.attributeTagNames;
			}
		}

		// Token: 0x17004E2E RID: 20014
		// (get) Token: 0x06010CC6 RID: 68806 RVA: 0x002E74A7 File Offset: 0x002E56A7
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SlicerCacheDefinition.attributeNamespaceIds;
			}
		}

		// Token: 0x17004E2F RID: 20015
		// (get) Token: 0x06010CC7 RID: 68807 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06010CC8 RID: 68808 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "name")]
		public StringValue Name
		{
			get
			{
				return (StringValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17004E30 RID: 20016
		// (get) Token: 0x06010CC9 RID: 68809 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06010CCA RID: 68810 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "sourceName")]
		public StringValue SourceName
		{
			get
			{
				return (StringValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x06010CCB RID: 68811 RVA: 0x002CEBD9 File Offset: 0x002CCDD9
		internal SlicerCacheDefinition(SlicerCachePart ownerPart)
			: base(ownerPart)
		{
		}

		// Token: 0x06010CCC RID: 68812 RVA: 0x002CEBE2 File Offset: 0x002CCDE2
		public void Load(SlicerCachePart openXmlPart)
		{
			base.LoadFromPart(openXmlPart);
		}

		// Token: 0x17004E31 RID: 20017
		// (get) Token: 0x06010CCD RID: 68813 RVA: 0x002E74AE File Offset: 0x002E56AE
		// (set) Token: 0x06010CCE RID: 68814 RVA: 0x002CEC01 File Offset: 0x002CCE01
		public SlicerCachePart SlicerCachePart
		{
			get
			{
				return base.OpenXmlPart as SlicerCachePart;
			}
			internal set
			{
				base.OpenXmlPart = value;
			}
		}

		// Token: 0x06010CCF RID: 68815 RVA: 0x002CEB18 File Offset: 0x002CCD18
		public SlicerCacheDefinition(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010CD0 RID: 68816 RVA: 0x002CEB21 File Offset: 0x002CCD21
		public SlicerCacheDefinition(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010CD1 RID: 68817 RVA: 0x002CEB2A File Offset: 0x002CCD2A
		public SlicerCacheDefinition(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06010CD2 RID: 68818 RVA: 0x002CEB10 File Offset: 0x002CCD10
		public SlicerCacheDefinition()
		{
		}

		// Token: 0x06010CD3 RID: 68819 RVA: 0x002CEBEB File Offset: 0x002CCDEB
		public void Save(SlicerCachePart openXmlPart)
		{
			base.SaveToPart(openXmlPart);
		}

		// Token: 0x06010CD4 RID: 68820 RVA: 0x002E74BC File Offset: 0x002E56BC
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (53 == namespaceId && "pivotTables" == name)
			{
				return new SlicerCachePivotTables();
			}
			if (53 == namespaceId && "data" == name)
			{
				return new SlicerCacheData();
			}
			if (53 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17004E32 RID: 20018
		// (get) Token: 0x06010CD5 RID: 68821 RVA: 0x002E7512 File Offset: 0x002E5712
		internal override string[] ElementTagNames
		{
			get
			{
				return SlicerCacheDefinition.eleTagNames;
			}
		}

		// Token: 0x17004E33 RID: 20019
		// (get) Token: 0x06010CD6 RID: 68822 RVA: 0x002E7519 File Offset: 0x002E5719
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return SlicerCacheDefinition.eleNamespaceIds;
			}
		}

		// Token: 0x17004E34 RID: 20020
		// (get) Token: 0x06010CD7 RID: 68823 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17004E35 RID: 20021
		// (get) Token: 0x06010CD8 RID: 68824 RVA: 0x002E7520 File Offset: 0x002E5720
		// (set) Token: 0x06010CD9 RID: 68825 RVA: 0x002E7529 File Offset: 0x002E5729
		public SlicerCachePivotTables SlicerCachePivotTables
		{
			get
			{
				return base.GetElement<SlicerCachePivotTables>(0);
			}
			set
			{
				base.SetElement<SlicerCachePivotTables>(0, value);
			}
		}

		// Token: 0x17004E36 RID: 20022
		// (get) Token: 0x06010CDA RID: 68826 RVA: 0x002E7533 File Offset: 0x002E5733
		// (set) Token: 0x06010CDB RID: 68827 RVA: 0x002E753C File Offset: 0x002E573C
		public SlicerCacheData SlicerCacheData
		{
			get
			{
				return base.GetElement<SlicerCacheData>(1);
			}
			set
			{
				base.SetElement<SlicerCacheData>(1, value);
			}
		}

		// Token: 0x17004E37 RID: 20023
		// (get) Token: 0x06010CDC RID: 68828 RVA: 0x002E7546 File Offset: 0x002E5746
		// (set) Token: 0x06010CDD RID: 68829 RVA: 0x002E754F File Offset: 0x002E574F
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

		// Token: 0x06010CDE RID: 68830 RVA: 0x002E7559 File Offset: 0x002E5759
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "sourceName" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010CDF RID: 68831 RVA: 0x002E758F File Offset: 0x002E578F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SlicerCacheDefinition>(deep);
		}

		// Token: 0x06010CE0 RID: 68832 RVA: 0x002E7598 File Offset: 0x002E5798
		// Note: this type is marked as 'beforefieldinit'.
		static SlicerCacheDefinition()
		{
			byte[] array = new byte[2];
			SlicerCacheDefinition.attributeNamespaceIds = array;
			SlicerCacheDefinition.eleTagNames = new string[] { "pivotTables", "data", "extLst" };
			SlicerCacheDefinition.eleNamespaceIds = new byte[] { 53, 53, 53 };
		}

		// Token: 0x0400765A RID: 30298
		private const string tagName = "slicerCacheDefinition";

		// Token: 0x0400765B RID: 30299
		private const byte tagNsId = 53;

		// Token: 0x0400765C RID: 30300
		internal const int ElementTypeIdConst = 12929;

		// Token: 0x0400765D RID: 30301
		private static string[] attributeTagNames = new string[] { "name", "sourceName" };

		// Token: 0x0400765E RID: 30302
		private static byte[] attributeNamespaceIds;

		// Token: 0x0400765F RID: 30303
		private static readonly string[] eleTagNames;

		// Token: 0x04007660 RID: 30304
		private static readonly byte[] eleNamespaceIds;
	}
}
