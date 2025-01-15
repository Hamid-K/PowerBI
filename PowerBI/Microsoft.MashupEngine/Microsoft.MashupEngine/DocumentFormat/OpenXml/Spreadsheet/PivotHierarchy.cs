using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B7C RID: 11132
	[ChildElementInfo(typeof(MemberProperties))]
	[ChildElementInfo(typeof(Members))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(PivotHierarchyExtensionList))]
	internal class PivotHierarchy : OpenXmlCompositeElement
	{
		// Token: 0x17007A18 RID: 31256
		// (get) Token: 0x06016FFE RID: 94206 RVA: 0x002E6972 File Offset: 0x002E4B72
		public override string LocalName
		{
			get
			{
				return "pivotHierarchy";
			}
		}

		// Token: 0x17007A19 RID: 31257
		// (get) Token: 0x06016FFF RID: 94207 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007A1A RID: 31258
		// (get) Token: 0x06017000 RID: 94208 RVA: 0x0033184E File Offset: 0x0032FA4E
		internal override int ElementTypeId
		{
			get
			{
				return 11112;
			}
		}

		// Token: 0x06017001 RID: 94209 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007A1B RID: 31259
		// (get) Token: 0x06017002 RID: 94210 RVA: 0x00331855 File Offset: 0x0032FA55
		internal override string[] AttributeTagNames
		{
			get
			{
				return PivotHierarchy.attributeTagNames;
			}
		}

		// Token: 0x17007A1C RID: 31260
		// (get) Token: 0x06017003 RID: 94211 RVA: 0x0033185C File Offset: 0x0032FA5C
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PivotHierarchy.attributeNamespaceIds;
			}
		}

		// Token: 0x17007A1D RID: 31261
		// (get) Token: 0x06017004 RID: 94212 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x06017005 RID: 94213 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "outline")]
		public BooleanValue Outline
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

		// Token: 0x17007A1E RID: 31262
		// (get) Token: 0x06017006 RID: 94214 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x06017007 RID: 94215 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "multipleItemSelectionAllowed")]
		public BooleanValue MultipleItemSelectionAllowed
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

		// Token: 0x17007A1F RID: 31263
		// (get) Token: 0x06017008 RID: 94216 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x06017009 RID: 94217 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "subtotalTop")]
		public BooleanValue SubtotalTop
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

		// Token: 0x17007A20 RID: 31264
		// (get) Token: 0x0601700A RID: 94218 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x0601700B RID: 94219 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "showInFieldList")]
		public BooleanValue ShowInFieldList
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

		// Token: 0x17007A21 RID: 31265
		// (get) Token: 0x0601700C RID: 94220 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x0601700D RID: 94221 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "dragToRow")]
		public BooleanValue DragToRow
		{
			get
			{
				return (BooleanValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17007A22 RID: 31266
		// (get) Token: 0x0601700E RID: 94222 RVA: 0x002D7D70 File Offset: 0x002D5F70
		// (set) Token: 0x0601700F RID: 94223 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "dragToCol")]
		public BooleanValue DragToColumn
		{
			get
			{
				return (BooleanValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x17007A23 RID: 31267
		// (get) Token: 0x06017010 RID: 94224 RVA: 0x002CCC33 File Offset: 0x002CAE33
		// (set) Token: 0x06017011 RID: 94225 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "dragToPage")]
		public BooleanValue DragToPage
		{
			get
			{
				return (BooleanValue)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x17007A24 RID: 31268
		// (get) Token: 0x06017012 RID: 94226 RVA: 0x002CA736 File Offset: 0x002C8936
		// (set) Token: 0x06017013 RID: 94227 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "dragToData")]
		public BooleanValue DragToData
		{
			get
			{
				return (BooleanValue)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x17007A25 RID: 31269
		// (get) Token: 0x06017014 RID: 94228 RVA: 0x002CB706 File Offset: 0x002C9906
		// (set) Token: 0x06017015 RID: 94229 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "dragOff")]
		public BooleanValue DragOff
		{
			get
			{
				return (BooleanValue)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x17007A26 RID: 31270
		// (get) Token: 0x06017016 RID: 94230 RVA: 0x002C92EA File Offset: 0x002C74EA
		// (set) Token: 0x06017017 RID: 94231 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "includeNewItemsInFilter")]
		public BooleanValue IncludeNewItemsInFilter
		{
			get
			{
				return (BooleanValue)base.Attributes[9];
			}
			set
			{
				base.Attributes[9] = value;
			}
		}

		// Token: 0x17007A27 RID: 31271
		// (get) Token: 0x06017018 RID: 94232 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x06017019 RID: 94233 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "caption")]
		public StringValue Caption
		{
			get
			{
				return (StringValue)base.Attributes[10];
			}
			set
			{
				base.Attributes[10] = value;
			}
		}

		// Token: 0x0601701A RID: 94234 RVA: 0x00293ECF File Offset: 0x002920CF
		public PivotHierarchy()
		{
		}

		// Token: 0x0601701B RID: 94235 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PivotHierarchy(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601701C RID: 94236 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PivotHierarchy(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601701D RID: 94237 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PivotHierarchy(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601701E RID: 94238 RVA: 0x00331864 File Offset: 0x0032FA64
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "mps" == name)
			{
				return new MemberProperties();
			}
			if (22 == namespaceId && "members" == name)
			{
				return new Members();
			}
			if (22 == namespaceId && "extLst" == name)
			{
				return new PivotHierarchyExtensionList();
			}
			return null;
		}

		// Token: 0x17007A28 RID: 31272
		// (get) Token: 0x0601701F RID: 94239 RVA: 0x003318BA File Offset: 0x0032FABA
		internal override string[] ElementTagNames
		{
			get
			{
				return PivotHierarchy.eleTagNames;
			}
		}

		// Token: 0x17007A29 RID: 31273
		// (get) Token: 0x06017020 RID: 94240 RVA: 0x003318C1 File Offset: 0x0032FAC1
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return PivotHierarchy.eleNamespaceIds;
			}
		}

		// Token: 0x17007A2A RID: 31274
		// (get) Token: 0x06017021 RID: 94241 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17007A2B RID: 31275
		// (get) Token: 0x06017022 RID: 94242 RVA: 0x003318C8 File Offset: 0x0032FAC8
		// (set) Token: 0x06017023 RID: 94243 RVA: 0x003318D1 File Offset: 0x0032FAD1
		public MemberProperties MemberProperties
		{
			get
			{
				return base.GetElement<MemberProperties>(0);
			}
			set
			{
				base.SetElement<MemberProperties>(0, value);
			}
		}

		// Token: 0x06017024 RID: 94244 RVA: 0x003318DC File Offset: 0x0032FADC
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "outline" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "multipleItemSelectionAllowed" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "subtotalTop" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "showInFieldList" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "dragToRow" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "dragToCol" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "dragToPage" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "dragToData" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "dragOff" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "includeNewItemsInFilter" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "caption" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017025 RID: 94245 RVA: 0x003319E3 File Offset: 0x0032FBE3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PivotHierarchy>(deep);
		}

		// Token: 0x06017026 RID: 94246 RVA: 0x003319EC File Offset: 0x0032FBEC
		// Note: this type is marked as 'beforefieldinit'.
		static PivotHierarchy()
		{
			byte[] array = new byte[11];
			PivotHierarchy.attributeNamespaceIds = array;
			PivotHierarchy.eleTagNames = new string[] { "mps", "members", "extLst" };
			PivotHierarchy.eleNamespaceIds = new byte[] { 22, 22, 22 };
		}

		// Token: 0x04009AB2 RID: 39602
		private const string tagName = "pivotHierarchy";

		// Token: 0x04009AB3 RID: 39603
		private const byte tagNsId = 22;

		// Token: 0x04009AB4 RID: 39604
		internal const int ElementTypeIdConst = 11112;

		// Token: 0x04009AB5 RID: 39605
		private static string[] attributeTagNames = new string[]
		{
			"outline", "multipleItemSelectionAllowed", "subtotalTop", "showInFieldList", "dragToRow", "dragToCol", "dragToPage", "dragToData", "dragOff", "includeNewItemsInFilter",
			"caption"
		};

		// Token: 0x04009AB6 RID: 39606
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009AB7 RID: 39607
		private static readonly string[] eleTagNames;

		// Token: 0x04009AB8 RID: 39608
		private static readonly byte[] eleNamespaceIds;
	}
}
