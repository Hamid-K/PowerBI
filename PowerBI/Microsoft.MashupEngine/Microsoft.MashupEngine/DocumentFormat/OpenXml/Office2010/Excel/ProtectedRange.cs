using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office.Excel;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x02002432 RID: 9266
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ReferenceSequence))]
	[GeneratedCode("DomGen", "2.0")]
	internal class ProtectedRange : OpenXmlCompositeElement
	{
		// Token: 0x17004FF4 RID: 20468
		// (get) Token: 0x060110CB RID: 69835 RVA: 0x002EA114 File Offset: 0x002E8314
		public override string LocalName
		{
			get
			{
				return "protectedRange";
			}
		}

		// Token: 0x17004FF5 RID: 20469
		// (get) Token: 0x060110CC RID: 69836 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004FF6 RID: 20470
		// (get) Token: 0x060110CD RID: 69837 RVA: 0x002EA11B File Offset: 0x002E831B
		internal override int ElementTypeId
		{
			get
			{
				return 12990;
			}
		}

		// Token: 0x060110CE RID: 69838 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004FF7 RID: 20471
		// (get) Token: 0x060110CF RID: 69839 RVA: 0x002EA122 File Offset: 0x002E8322
		internal override string[] AttributeTagNames
		{
			get
			{
				return ProtectedRange.attributeTagNames;
			}
		}

		// Token: 0x17004FF8 RID: 20472
		// (get) Token: 0x060110D0 RID: 69840 RVA: 0x002EA129 File Offset: 0x002E8329
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ProtectedRange.attributeNamespaceIds;
			}
		}

		// Token: 0x17004FF9 RID: 20473
		// (get) Token: 0x060110D1 RID: 69841 RVA: 0x002EA130 File Offset: 0x002E8330
		// (set) Token: 0x060110D2 RID: 69842 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "password")]
		public HexBinaryValue Password
		{
			get
			{
				return (HexBinaryValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17004FFA RID: 20474
		// (get) Token: 0x060110D3 RID: 69843 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x060110D4 RID: 69844 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "algorithmName")]
		public StringValue AlgorithmName
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

		// Token: 0x17004FFB RID: 20475
		// (get) Token: 0x060110D5 RID: 69845 RVA: 0x002EA13F File Offset: 0x002E833F
		// (set) Token: 0x060110D6 RID: 69846 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "hashValue")]
		public Base64BinaryValue HashValue
		{
			get
			{
				return (Base64BinaryValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17004FFC RID: 20476
		// (get) Token: 0x060110D7 RID: 69847 RVA: 0x002EA14E File Offset: 0x002E834E
		// (set) Token: 0x060110D8 RID: 69848 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "saltValue")]
		public Base64BinaryValue SaltValue
		{
			get
			{
				return (Base64BinaryValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17004FFD RID: 20477
		// (get) Token: 0x060110D9 RID: 69849 RVA: 0x002E6C42 File Offset: 0x002E4E42
		// (set) Token: 0x060110DA RID: 69850 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "spinCount")]
		public UInt32Value SpinCount
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

		// Token: 0x17004FFE RID: 20478
		// (get) Token: 0x060110DB RID: 69851 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x060110DC RID: 69852 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "name")]
		public StringValue Name
		{
			get
			{
				return (StringValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x17004FFF RID: 20479
		// (get) Token: 0x060110DD RID: 69853 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x060110DE RID: 69854 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "securityDescriptor")]
		public StringValue SecurityDescriptor
		{
			get
			{
				return (StringValue)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x060110DF RID: 69855 RVA: 0x00293ECF File Offset: 0x002920CF
		public ProtectedRange()
		{
		}

		// Token: 0x060110E0 RID: 69856 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ProtectedRange(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060110E1 RID: 69857 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ProtectedRange(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060110E2 RID: 69858 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ProtectedRange(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060110E3 RID: 69859 RVA: 0x002E9F5B File Offset: 0x002E815B
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (32 == namespaceId && "sqref" == name)
			{
				return new ReferenceSequence();
			}
			return null;
		}

		// Token: 0x17005000 RID: 20480
		// (get) Token: 0x060110E4 RID: 69860 RVA: 0x002EA15D File Offset: 0x002E835D
		internal override string[] ElementTagNames
		{
			get
			{
				return ProtectedRange.eleTagNames;
			}
		}

		// Token: 0x17005001 RID: 20481
		// (get) Token: 0x060110E5 RID: 69861 RVA: 0x002EA164 File Offset: 0x002E8364
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ProtectedRange.eleNamespaceIds;
			}
		}

		// Token: 0x17005002 RID: 20482
		// (get) Token: 0x060110E6 RID: 69862 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005003 RID: 20483
		// (get) Token: 0x060110E7 RID: 69863 RVA: 0x002E9F84 File Offset: 0x002E8184
		// (set) Token: 0x060110E8 RID: 69864 RVA: 0x002E9F8D File Offset: 0x002E818D
		public ReferenceSequence ReferenceSequence
		{
			get
			{
				return base.GetElement<ReferenceSequence>(0);
			}
			set
			{
				base.SetElement<ReferenceSequence>(0, value);
			}
		}

		// Token: 0x060110E9 RID: 69865 RVA: 0x002EA16C File Offset: 0x002E836C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "password" == name)
			{
				return new HexBinaryValue();
			}
			if (namespaceId == 0 && "algorithmName" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "hashValue" == name)
			{
				return new Base64BinaryValue();
			}
			if (namespaceId == 0 && "saltValue" == name)
			{
				return new Base64BinaryValue();
			}
			if (namespaceId == 0 && "spinCount" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "securityDescriptor" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060110EA RID: 69866 RVA: 0x002EA21B File Offset: 0x002E841B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ProtectedRange>(deep);
		}

		// Token: 0x060110EB RID: 69867 RVA: 0x002EA224 File Offset: 0x002E8424
		// Note: this type is marked as 'beforefieldinit'.
		static ProtectedRange()
		{
			byte[] array = new byte[7];
			ProtectedRange.attributeNamespaceIds = array;
			ProtectedRange.eleTagNames = new string[] { "sqref" };
			ProtectedRange.eleNamespaceIds = new byte[] { 32 };
		}

		// Token: 0x0400776F RID: 30575
		private const string tagName = "protectedRange";

		// Token: 0x04007770 RID: 30576
		private const byte tagNsId = 53;

		// Token: 0x04007771 RID: 30577
		internal const int ElementTypeIdConst = 12990;

		// Token: 0x04007772 RID: 30578
		private static string[] attributeTagNames = new string[] { "password", "algorithmName", "hashValue", "saltValue", "spinCount", "name", "securityDescriptor" };

		// Token: 0x04007773 RID: 30579
		private static byte[] attributeNamespaceIds;

		// Token: 0x04007774 RID: 30580
		private static readonly string[] eleTagNames;

		// Token: 0x04007775 RID: 30581
		private static readonly byte[] eleNamespaceIds;
	}
}
