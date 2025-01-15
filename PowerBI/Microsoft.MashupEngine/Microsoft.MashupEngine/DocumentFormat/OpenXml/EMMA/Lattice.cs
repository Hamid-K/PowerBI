using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.EMMA
{
	// Token: 0x02003073 RID: 12403
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Arc))]
	[ChildElementInfo(typeof(Node))]
	internal class Lattice : OpenXmlCompositeElement
	{
		// Token: 0x17009682 RID: 38530
		// (get) Token: 0x0601ADE4 RID: 110052 RVA: 0x0036892F File Offset: 0x00366B2F
		public override string LocalName
		{
			get
			{
				return "lattice";
			}
		}

		// Token: 0x17009683 RID: 38531
		// (get) Token: 0x0601ADE5 RID: 110053 RVA: 0x0036884A File Offset: 0x00366A4A
		internal override byte NamespaceId
		{
			get
			{
				return 44;
			}
		}

		// Token: 0x17009684 RID: 38532
		// (get) Token: 0x0601ADE6 RID: 110054 RVA: 0x00368936 File Offset: 0x00366B36
		internal override int ElementTypeId
		{
			get
			{
				return 12672;
			}
		}

		// Token: 0x0601ADE7 RID: 110055 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17009685 RID: 38533
		// (get) Token: 0x0601ADE8 RID: 110056 RVA: 0x0036893D File Offset: 0x00366B3D
		internal override string[] AttributeTagNames
		{
			get
			{
				return Lattice.attributeTagNames;
			}
		}

		// Token: 0x17009686 RID: 38534
		// (get) Token: 0x0601ADE9 RID: 110057 RVA: 0x00368944 File Offset: 0x00366B44
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Lattice.attributeNamespaceIds;
			}
		}

		// Token: 0x17009687 RID: 38535
		// (get) Token: 0x0601ADEA RID: 110058 RVA: 0x002EC050 File Offset: 0x002EA250
		// (set) Token: 0x0601ADEB RID: 110059 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "initial")]
		public IntegerValue Initial
		{
			get
			{
				return (IntegerValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17009688 RID: 38536
		// (get) Token: 0x0601ADEC RID: 110060 RVA: 0x0036894B File Offset: 0x00366B4B
		// (set) Token: 0x0601ADED RID: 110061 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "final")]
		public ListValue<DecimalValue> Final
		{
			get
			{
				return (ListValue<DecimalValue>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17009689 RID: 38537
		// (get) Token: 0x0601ADEE RID: 110062 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0601ADEF RID: 110063 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(44, "time-ref-uri")]
		public StringValue TimeReference
		{
			get
			{
				return (StringValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x1700968A RID: 38538
		// (get) Token: 0x0601ADF0 RID: 110064 RVA: 0x0036895A File Offset: 0x00366B5A
		// (set) Token: 0x0601ADF1 RID: 110065 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(44, "time-ref-anchor-point")]
		public EnumValue<AnchorPointValues> TimeReferenceAnchorPoint
		{
			get
			{
				return (EnumValue<AnchorPointValues>)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x0601ADF2 RID: 110066 RVA: 0x00293ECF File Offset: 0x002920CF
		public Lattice()
		{
		}

		// Token: 0x0601ADF3 RID: 110067 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Lattice(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601ADF4 RID: 110068 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Lattice(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601ADF5 RID: 110069 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Lattice(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601ADF6 RID: 110070 RVA: 0x00368969 File Offset: 0x00366B69
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (44 == namespaceId && "arc" == name)
			{
				return new Arc();
			}
			if (44 == namespaceId && "node" == name)
			{
				return new Node();
			}
			return null;
		}

		// Token: 0x0601ADF7 RID: 110071 RVA: 0x0036899C File Offset: 0x00366B9C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "initial" == name)
			{
				return new IntegerValue();
			}
			if (namespaceId == 0 && "final" == name)
			{
				return new ListValue<DecimalValue>();
			}
			if (44 == namespaceId && "time-ref-uri" == name)
			{
				return new StringValue();
			}
			if (44 == namespaceId && "time-ref-anchor-point" == name)
			{
				return new EnumValue<AnchorPointValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601ADF8 RID: 110072 RVA: 0x00368A0D File Offset: 0x00366C0D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Lattice>(deep);
		}

		// Token: 0x0400B215 RID: 45589
		private const string tagName = "lattice";

		// Token: 0x0400B216 RID: 45590
		private const byte tagNsId = 44;

		// Token: 0x0400B217 RID: 45591
		internal const int ElementTypeIdConst = 12672;

		// Token: 0x0400B218 RID: 45592
		private static string[] attributeTagNames = new string[] { "initial", "final", "time-ref-uri", "time-ref-anchor-point" };

		// Token: 0x0400B219 RID: 45593
		private static byte[] attributeNamespaceIds = new byte[] { 0, 0, 44, 44 };
	}
}
