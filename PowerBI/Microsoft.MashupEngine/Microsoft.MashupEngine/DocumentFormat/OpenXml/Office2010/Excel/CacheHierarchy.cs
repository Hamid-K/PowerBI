using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x020023DE RID: 9182
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(SetLevels), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class CacheHierarchy : OpenXmlCompositeElement
	{
		// Token: 0x17004D57 RID: 19799
		// (get) Token: 0x06010AFA RID: 68346 RVA: 0x002E5F7E File Offset: 0x002E417E
		public override string LocalName
		{
			get
			{
				return "cacheHierarchy";
			}
		}

		// Token: 0x17004D58 RID: 19800
		// (get) Token: 0x06010AFB RID: 68347 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004D59 RID: 19801
		// (get) Token: 0x06010AFC RID: 68348 RVA: 0x002E5F85 File Offset: 0x002E4185
		internal override int ElementTypeId
		{
			get
			{
				return 12908;
			}
		}

		// Token: 0x06010AFD RID: 68349 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004D5A RID: 19802
		// (get) Token: 0x06010AFE RID: 68350 RVA: 0x002E5F8C File Offset: 0x002E418C
		internal override string[] AttributeTagNames
		{
			get
			{
				return CacheHierarchy.attributeTagNames;
			}
		}

		// Token: 0x17004D5B RID: 19803
		// (get) Token: 0x06010AFF RID: 68351 RVA: 0x002E5F93 File Offset: 0x002E4193
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return CacheHierarchy.attributeNamespaceIds;
			}
		}

		// Token: 0x17004D5C RID: 19804
		// (get) Token: 0x06010B00 RID: 68352 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x06010B01 RID: 68353 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "flattenHierarchies")]
		public BooleanValue FlattenHierarchies
		{
			get
			{
				return (BooleanValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17004D5D RID: 19805
		// (get) Token: 0x06010B02 RID: 68354 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x06010B03 RID: 68355 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "measuresSet")]
		public BooleanValue MeasuresSet
		{
			get
			{
				return (BooleanValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17004D5E RID: 19806
		// (get) Token: 0x06010B04 RID: 68356 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x06010B05 RID: 68357 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "hierarchizeDistinct")]
		public BooleanValue HierarchizeDistinct
		{
			get
			{
				return (BooleanValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17004D5F RID: 19807
		// (get) Token: 0x06010B06 RID: 68358 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x06010B07 RID: 68359 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "ignore")]
		public BooleanValue Ignore
		{
			get
			{
				return (BooleanValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x06010B08 RID: 68360 RVA: 0x00293ECF File Offset: 0x002920CF
		public CacheHierarchy()
		{
		}

		// Token: 0x06010B09 RID: 68361 RVA: 0x00293ED7 File Offset: 0x002920D7
		public CacheHierarchy(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010B0A RID: 68362 RVA: 0x00293EE0 File Offset: 0x002920E0
		public CacheHierarchy(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010B0B RID: 68363 RVA: 0x00293EE9 File Offset: 0x002920E9
		public CacheHierarchy(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06010B0C RID: 68364 RVA: 0x002E5F9A File Offset: 0x002E419A
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (53 == namespaceId && "setLevels" == name)
			{
				return new SetLevels();
			}
			return null;
		}

		// Token: 0x17004D60 RID: 19808
		// (get) Token: 0x06010B0D RID: 68365 RVA: 0x002E5FB5 File Offset: 0x002E41B5
		internal override string[] ElementTagNames
		{
			get
			{
				return CacheHierarchy.eleTagNames;
			}
		}

		// Token: 0x17004D61 RID: 19809
		// (get) Token: 0x06010B0E RID: 68366 RVA: 0x002E5FBC File Offset: 0x002E41BC
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return CacheHierarchy.eleNamespaceIds;
			}
		}

		// Token: 0x17004D62 RID: 19810
		// (get) Token: 0x06010B0F RID: 68367 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17004D63 RID: 19811
		// (get) Token: 0x06010B10 RID: 68368 RVA: 0x002E5FC3 File Offset: 0x002E41C3
		// (set) Token: 0x06010B11 RID: 68369 RVA: 0x002E5FCC File Offset: 0x002E41CC
		public SetLevels SetLevels
		{
			get
			{
				return base.GetElement<SetLevels>(0);
			}
			set
			{
				base.SetElement<SetLevels>(0, value);
			}
		}

		// Token: 0x06010B12 RID: 68370 RVA: 0x002E5FD8 File Offset: 0x002E41D8
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "flattenHierarchies" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "measuresSet" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "hierarchizeDistinct" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "ignore" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010B13 RID: 68371 RVA: 0x002E6045 File Offset: 0x002E4245
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CacheHierarchy>(deep);
		}

		// Token: 0x06010B14 RID: 68372 RVA: 0x002E6050 File Offset: 0x002E4250
		// Note: this type is marked as 'beforefieldinit'.
		static CacheHierarchy()
		{
			byte[] array = new byte[4];
			CacheHierarchy.attributeNamespaceIds = array;
			CacheHierarchy.eleTagNames = new string[] { "setLevels" };
			CacheHierarchy.eleNamespaceIds = new byte[] { 53 };
		}

		// Token: 0x040075E9 RID: 30185
		private const string tagName = "cacheHierarchy";

		// Token: 0x040075EA RID: 30186
		private const byte tagNsId = 53;

		// Token: 0x040075EB RID: 30187
		internal const int ElementTypeIdConst = 12908;

		// Token: 0x040075EC RID: 30188
		private static string[] attributeTagNames = new string[] { "flattenHierarchies", "measuresSet", "hierarchizeDistinct", "ignore" };

		// Token: 0x040075ED RID: 30189
		private static byte[] attributeNamespaceIds;

		// Token: 0x040075EE RID: 30190
		private static readonly string[] eleTagNames;

		// Token: 0x040075EF RID: 30191
		private static readonly byte[] eleNamespaceIds;
	}
}
