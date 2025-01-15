using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C22 RID: 11298
	[ChildElementInfo(typeof(Values))]
	[GeneratedCode("DomGen", "2.0")]
	internal class DdeItem : OpenXmlCompositeElement
	{
		// Token: 0x17008071 RID: 32881
		// (get) Token: 0x06017D9B RID: 97691 RVA: 0x0033BD34 File Offset: 0x00339F34
		public override string LocalName
		{
			get
			{
				return "ddeItem";
			}
		}

		// Token: 0x17008072 RID: 32882
		// (get) Token: 0x06017D9C RID: 97692 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008073 RID: 32883
		// (get) Token: 0x06017D9D RID: 97693 RVA: 0x0033BD3B File Offset: 0x00339F3B
		internal override int ElementTypeId
		{
			get
			{
				return 11279;
			}
		}

		// Token: 0x06017D9E RID: 97694 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008074 RID: 32884
		// (get) Token: 0x06017D9F RID: 97695 RVA: 0x0033BD42 File Offset: 0x00339F42
		internal override string[] AttributeTagNames
		{
			get
			{
				return DdeItem.attributeTagNames;
			}
		}

		// Token: 0x17008075 RID: 32885
		// (get) Token: 0x06017DA0 RID: 97696 RVA: 0x0033BD49 File Offset: 0x00339F49
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return DdeItem.attributeNamespaceIds;
			}
		}

		// Token: 0x17008076 RID: 32886
		// (get) Token: 0x06017DA1 RID: 97697 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06017DA2 RID: 97698 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17008077 RID: 32887
		// (get) Token: 0x06017DA3 RID: 97699 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x06017DA4 RID: 97700 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "ole")]
		public BooleanValue UseOle
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

		// Token: 0x17008078 RID: 32888
		// (get) Token: 0x06017DA5 RID: 97701 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x06017DA6 RID: 97702 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "advise")]
		public BooleanValue Advise
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

		// Token: 0x17008079 RID: 32889
		// (get) Token: 0x06017DA7 RID: 97703 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x06017DA8 RID: 97704 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "preferPic")]
		public BooleanValue PreferPicture
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

		// Token: 0x06017DA9 RID: 97705 RVA: 0x00293ECF File Offset: 0x002920CF
		public DdeItem()
		{
		}

		// Token: 0x06017DAA RID: 97706 RVA: 0x00293ED7 File Offset: 0x002920D7
		public DdeItem(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017DAB RID: 97707 RVA: 0x00293EE0 File Offset: 0x002920E0
		public DdeItem(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017DAC RID: 97708 RVA: 0x00293EE9 File Offset: 0x002920E9
		public DdeItem(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06017DAD RID: 97709 RVA: 0x0033BD50 File Offset: 0x00339F50
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "values" == name)
			{
				return new Values();
			}
			return null;
		}

		// Token: 0x1700807A RID: 32890
		// (get) Token: 0x06017DAE RID: 97710 RVA: 0x0033BD6B File Offset: 0x00339F6B
		internal override string[] ElementTagNames
		{
			get
			{
				return DdeItem.eleTagNames;
			}
		}

		// Token: 0x1700807B RID: 32891
		// (get) Token: 0x06017DAF RID: 97711 RVA: 0x0033BD72 File Offset: 0x00339F72
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return DdeItem.eleNamespaceIds;
			}
		}

		// Token: 0x1700807C RID: 32892
		// (get) Token: 0x06017DB0 RID: 97712 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700807D RID: 32893
		// (get) Token: 0x06017DB1 RID: 97713 RVA: 0x0033BD79 File Offset: 0x00339F79
		// (set) Token: 0x06017DB2 RID: 97714 RVA: 0x0033BD82 File Offset: 0x00339F82
		public Values Values
		{
			get
			{
				return base.GetElement<Values>(0);
			}
			set
			{
				base.SetElement<Values>(0, value);
			}
		}

		// Token: 0x06017DB3 RID: 97715 RVA: 0x0033BD8C File Offset: 0x00339F8C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "ole" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "advise" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "preferPic" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017DB4 RID: 97716 RVA: 0x0033BDF9 File Offset: 0x00339FF9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DdeItem>(deep);
		}

		// Token: 0x06017DB5 RID: 97717 RVA: 0x0033BE04 File Offset: 0x0033A004
		// Note: this type is marked as 'beforefieldinit'.
		static DdeItem()
		{
			byte[] array = new byte[4];
			DdeItem.attributeNamespaceIds = array;
			DdeItem.eleTagNames = new string[] { "values" };
			DdeItem.eleNamespaceIds = new byte[] { 22 };
		}

		// Token: 0x04009DD7 RID: 40407
		private const string tagName = "ddeItem";

		// Token: 0x04009DD8 RID: 40408
		private const byte tagNsId = 22;

		// Token: 0x04009DD9 RID: 40409
		internal const int ElementTypeIdConst = 11279;

		// Token: 0x04009DDA RID: 40410
		private static string[] attributeTagNames = new string[] { "name", "ole", "advise", "preferPic" };

		// Token: 0x04009DDB RID: 40411
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009DDC RID: 40412
		private static readonly string[] eleTagNames;

		// Token: 0x04009DDD RID: 40413
		private static readonly byte[] eleNamespaceIds;
	}
}
