using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x020023DD RID: 9181
	[ChildElementInfo(typeof(TupleSet), FileFormatVersions.Office2010)]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class CalculatedMember : OpenXmlCompositeElement
	{
		// Token: 0x17004D49 RID: 19785
		// (get) Token: 0x06010ADD RID: 68317 RVA: 0x002E5E23 File Offset: 0x002E4023
		public override string LocalName
		{
			get
			{
				return "calculatedMember";
			}
		}

		// Token: 0x17004D4A RID: 19786
		// (get) Token: 0x06010ADE RID: 68318 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004D4B RID: 19787
		// (get) Token: 0x06010ADF RID: 68319 RVA: 0x002E5E2A File Offset: 0x002E402A
		internal override int ElementTypeId
		{
			get
			{
				return 12907;
			}
		}

		// Token: 0x06010AE0 RID: 68320 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004D4C RID: 19788
		// (get) Token: 0x06010AE1 RID: 68321 RVA: 0x002E5E31 File Offset: 0x002E4031
		internal override string[] AttributeTagNames
		{
			get
			{
				return CalculatedMember.attributeTagNames;
			}
		}

		// Token: 0x17004D4D RID: 19789
		// (get) Token: 0x06010AE2 RID: 68322 RVA: 0x002E5E38 File Offset: 0x002E4038
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return CalculatedMember.attributeNamespaceIds;
			}
		}

		// Token: 0x17004D4E RID: 19790
		// (get) Token: 0x06010AE3 RID: 68323 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06010AE4 RID: 68324 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "displayFolder")]
		public StringValue DisplayFolder
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

		// Token: 0x17004D4F RID: 19791
		// (get) Token: 0x06010AE5 RID: 68325 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x06010AE6 RID: 68326 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "flattenHierarchies")]
		public BooleanValue FlattenHierarchies
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

		// Token: 0x17004D50 RID: 19792
		// (get) Token: 0x06010AE7 RID: 68327 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x06010AE8 RID: 68328 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "dynamicSet")]
		public BooleanValue DynamicSet
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

		// Token: 0x17004D51 RID: 19793
		// (get) Token: 0x06010AE9 RID: 68329 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x06010AEA RID: 68330 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "hierarchizeDistinct")]
		public BooleanValue HierarchizeDistinct
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

		// Token: 0x17004D52 RID: 19794
		// (get) Token: 0x06010AEB RID: 68331 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x06010AEC RID: 68332 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "mdxLong")]
		public StringValue MdxLong
		{
			get
			{
				return (StringValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x06010AED RID: 68333 RVA: 0x00293ECF File Offset: 0x002920CF
		public CalculatedMember()
		{
		}

		// Token: 0x06010AEE RID: 68334 RVA: 0x00293ED7 File Offset: 0x002920D7
		public CalculatedMember(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010AEF RID: 68335 RVA: 0x00293EE0 File Offset: 0x002920E0
		public CalculatedMember(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010AF0 RID: 68336 RVA: 0x00293EE9 File Offset: 0x002920E9
		public CalculatedMember(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06010AF1 RID: 68337 RVA: 0x002E5E3F File Offset: 0x002E403F
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (53 == namespaceId && "tupleSet" == name)
			{
				return new TupleSet();
			}
			return null;
		}

		// Token: 0x17004D53 RID: 19795
		// (get) Token: 0x06010AF2 RID: 68338 RVA: 0x002E5E5A File Offset: 0x002E405A
		internal override string[] ElementTagNames
		{
			get
			{
				return CalculatedMember.eleTagNames;
			}
		}

		// Token: 0x17004D54 RID: 19796
		// (get) Token: 0x06010AF3 RID: 68339 RVA: 0x002E5E61 File Offset: 0x002E4061
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return CalculatedMember.eleNamespaceIds;
			}
		}

		// Token: 0x17004D55 RID: 19797
		// (get) Token: 0x06010AF4 RID: 68340 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17004D56 RID: 19798
		// (get) Token: 0x06010AF5 RID: 68341 RVA: 0x002E5E68 File Offset: 0x002E4068
		// (set) Token: 0x06010AF6 RID: 68342 RVA: 0x002E5E71 File Offset: 0x002E4071
		public TupleSet TupleSet
		{
			get
			{
				return base.GetElement<TupleSet>(0);
			}
			set
			{
				base.SetElement<TupleSet>(0, value);
			}
		}

		// Token: 0x06010AF7 RID: 68343 RVA: 0x002E5E7C File Offset: 0x002E407C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "displayFolder" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "flattenHierarchies" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "dynamicSet" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "hierarchizeDistinct" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "mdxLong" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010AF8 RID: 68344 RVA: 0x002E5EFF File Offset: 0x002E40FF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CalculatedMember>(deep);
		}

		// Token: 0x06010AF9 RID: 68345 RVA: 0x002E5F08 File Offset: 0x002E4108
		// Note: this type is marked as 'beforefieldinit'.
		static CalculatedMember()
		{
			byte[] array = new byte[5];
			CalculatedMember.attributeNamespaceIds = array;
			CalculatedMember.eleTagNames = new string[] { "tupleSet" };
			CalculatedMember.eleNamespaceIds = new byte[] { 53 };
		}

		// Token: 0x040075E2 RID: 30178
		private const string tagName = "calculatedMember";

		// Token: 0x040075E3 RID: 30179
		private const byte tagNsId = 53;

		// Token: 0x040075E4 RID: 30180
		internal const int ElementTypeIdConst = 12907;

		// Token: 0x040075E5 RID: 30181
		private static string[] attributeTagNames = new string[] { "displayFolder", "flattenHierarchies", "dynamicSet", "hierarchizeDistinct", "mdxLong" };

		// Token: 0x040075E6 RID: 30182
		private static byte[] attributeNamespaceIds;

		// Token: 0x040075E7 RID: 30183
		private static readonly string[] eleTagNames;

		// Token: 0x040075E8 RID: 30184
		private static readonly byte[] eleNamespaceIds;
	}
}
