using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BC1 RID: 11201
	[ChildElementInfo(typeof(CellFormula))]
	[ChildElementInfo(typeof(CellValue))]
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(InlineString))]
	internal abstract class CellType : OpenXmlCompositeElement
	{
		// Token: 0x17007C7C RID: 31868
		// (get) Token: 0x06017514 RID: 95508 RVA: 0x0033555B File Offset: 0x0033375B
		internal override string[] AttributeTagNames
		{
			get
			{
				return CellType.attributeTagNames;
			}
		}

		// Token: 0x17007C7D RID: 31869
		// (get) Token: 0x06017515 RID: 95509 RVA: 0x00335562 File Offset: 0x00333762
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return CellType.attributeNamespaceIds;
			}
		}

		// Token: 0x17007C7E RID: 31870
		// (get) Token: 0x06017516 RID: 95510 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06017517 RID: 95511 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "r")]
		public StringValue CellReference
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

		// Token: 0x17007C7F RID: 31871
		// (get) Token: 0x06017518 RID: 95512 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x06017519 RID: 95513 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "s")]
		public UInt32Value StyleIndex
		{
			get
			{
				return (UInt32Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17007C80 RID: 31872
		// (get) Token: 0x0601751A RID: 95514 RVA: 0x00335569 File Offset: 0x00333769
		// (set) Token: 0x0601751B RID: 95515 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "t")]
		public EnumValue<CellValues> DataType
		{
			get
			{
				return (EnumValue<CellValues>)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17007C81 RID: 31873
		// (get) Token: 0x0601751C RID: 95516 RVA: 0x002E5B0D File Offset: 0x002E3D0D
		// (set) Token: 0x0601751D RID: 95517 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "cm")]
		public UInt32Value CellMetaIndex
		{
			get
			{
				return (UInt32Value)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17007C82 RID: 31874
		// (get) Token: 0x0601751E RID: 95518 RVA: 0x002E6C42 File Offset: 0x002E4E42
		// (set) Token: 0x0601751F RID: 95519 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "vm")]
		public UInt32Value ValueMetaIndex
		{
			get
			{
				return (UInt32Value)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17007C83 RID: 31875
		// (get) Token: 0x06017520 RID: 95520 RVA: 0x002D7D70 File Offset: 0x002D5F70
		// (set) Token: 0x06017521 RID: 95521 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "ph")]
		public BooleanValue ShowPhonetic
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

		// Token: 0x06017522 RID: 95522 RVA: 0x00335578 File Offset: 0x00333778
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "f" == name)
			{
				return new CellFormula();
			}
			if (22 == namespaceId && "v" == name)
			{
				return new CellValue();
			}
			if (22 == namespaceId && "is" == name)
			{
				return new InlineString();
			}
			if (22 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17007C84 RID: 31876
		// (get) Token: 0x06017523 RID: 95523 RVA: 0x003355E6 File Offset: 0x003337E6
		internal override string[] ElementTagNames
		{
			get
			{
				return CellType.eleTagNames;
			}
		}

		// Token: 0x17007C85 RID: 31877
		// (get) Token: 0x06017524 RID: 95524 RVA: 0x003355ED File Offset: 0x003337ED
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return CellType.eleNamespaceIds;
			}
		}

		// Token: 0x17007C86 RID: 31878
		// (get) Token: 0x06017525 RID: 95525 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17007C87 RID: 31879
		// (get) Token: 0x06017526 RID: 95526 RVA: 0x003355F4 File Offset: 0x003337F4
		// (set) Token: 0x06017527 RID: 95527 RVA: 0x003355FD File Offset: 0x003337FD
		public CellFormula CellFormula
		{
			get
			{
				return base.GetElement<CellFormula>(0);
			}
			set
			{
				base.SetElement<CellFormula>(0, value);
			}
		}

		// Token: 0x17007C88 RID: 31880
		// (get) Token: 0x06017528 RID: 95528 RVA: 0x00335607 File Offset: 0x00333807
		// (set) Token: 0x06017529 RID: 95529 RVA: 0x00335610 File Offset: 0x00333810
		public CellValue CellValue
		{
			get
			{
				return base.GetElement<CellValue>(1);
			}
			set
			{
				base.SetElement<CellValue>(1, value);
			}
		}

		// Token: 0x17007C89 RID: 31881
		// (get) Token: 0x0601752A RID: 95530 RVA: 0x0033561A File Offset: 0x0033381A
		// (set) Token: 0x0601752B RID: 95531 RVA: 0x00335623 File Offset: 0x00333823
		public InlineString InlineString
		{
			get
			{
				return base.GetElement<InlineString>(2);
			}
			set
			{
				base.SetElement<InlineString>(2, value);
			}
		}

		// Token: 0x17007C8A RID: 31882
		// (get) Token: 0x0601752C RID: 95532 RVA: 0x00332E05 File Offset: 0x00331005
		// (set) Token: 0x0601752D RID: 95533 RVA: 0x00332E0E File Offset: 0x0033100E
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(3);
			}
			set
			{
				base.SetElement<ExtensionList>(3, value);
			}
		}

		// Token: 0x0601752E RID: 95534 RVA: 0x00335630 File Offset: 0x00333830
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "r" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "s" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "t" == name)
			{
				return new EnumValue<CellValues>();
			}
			if (namespaceId == 0 && "cm" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "vm" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "ph" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601752F RID: 95535 RVA: 0x00293ECF File Offset: 0x002920CF
		protected CellType()
		{
		}

		// Token: 0x06017530 RID: 95536 RVA: 0x00293ED7 File Offset: 0x002920D7
		protected CellType(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017531 RID: 95537 RVA: 0x00293EE0 File Offset: 0x002920E0
		protected CellType(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017532 RID: 95538 RVA: 0x00293EE9 File Offset: 0x002920E9
		protected CellType(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06017533 RID: 95539 RVA: 0x003356CC File Offset: 0x003338CC
		// Note: this type is marked as 'beforefieldinit'.
		static CellType()
		{
			byte[] array = new byte[6];
			CellType.attributeNamespaceIds = array;
			CellType.eleTagNames = new string[] { "f", "v", "is", "extLst" };
			CellType.eleNamespaceIds = new byte[] { 22, 22, 22, 22 };
		}

		// Token: 0x04009BEE RID: 39918
		private static string[] attributeTagNames = new string[] { "r", "s", "t", "cm", "vm", "ph" };

		// Token: 0x04009BEF RID: 39919
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009BF0 RID: 39920
		private static readonly string[] eleTagNames;

		// Token: 0x04009BF1 RID: 39921
		private static readonly byte[] eleNamespaceIds;
	}
}
