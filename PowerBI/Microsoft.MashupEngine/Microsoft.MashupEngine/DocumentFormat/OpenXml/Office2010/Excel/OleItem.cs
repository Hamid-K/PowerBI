using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x020023E7 RID: 9191
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(DdeValues), FileFormatVersions.Office2010)]
	internal class OleItem : OpenXmlCompositeElement
	{
		// Token: 0x17004DB0 RID: 19888
		// (get) Token: 0x06010BB7 RID: 68535 RVA: 0x002E6833 File Offset: 0x002E4A33
		public override string LocalName
		{
			get
			{
				return "oleItem";
			}
		}

		// Token: 0x17004DB1 RID: 19889
		// (get) Token: 0x06010BB8 RID: 68536 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004DB2 RID: 19890
		// (get) Token: 0x06010BB9 RID: 68537 RVA: 0x002E683A File Offset: 0x002E4A3A
		internal override int ElementTypeId
		{
			get
			{
				return 12917;
			}
		}

		// Token: 0x06010BBA RID: 68538 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004DB3 RID: 19891
		// (get) Token: 0x06010BBB RID: 68539 RVA: 0x002E6841 File Offset: 0x002E4A41
		internal override string[] AttributeTagNames
		{
			get
			{
				return OleItem.attributeTagNames;
			}
		}

		// Token: 0x17004DB4 RID: 19892
		// (get) Token: 0x06010BBC RID: 68540 RVA: 0x002E6848 File Offset: 0x002E4A48
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return OleItem.attributeNamespaceIds;
			}
		}

		// Token: 0x17004DB5 RID: 19893
		// (get) Token: 0x06010BBD RID: 68541 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06010BBE RID: 68542 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17004DB6 RID: 19894
		// (get) Token: 0x06010BBF RID: 68543 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x06010BC0 RID: 68544 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "icon")]
		public BooleanValue Icon
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

		// Token: 0x17004DB7 RID: 19895
		// (get) Token: 0x06010BC1 RID: 68545 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x06010BC2 RID: 68546 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x17004DB8 RID: 19896
		// (get) Token: 0x06010BC3 RID: 68547 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x06010BC4 RID: 68548 RVA: 0x002BD4AE File Offset: 0x002BB6AE
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

		// Token: 0x06010BC5 RID: 68549 RVA: 0x00293ECF File Offset: 0x002920CF
		public OleItem()
		{
		}

		// Token: 0x06010BC6 RID: 68550 RVA: 0x00293ED7 File Offset: 0x002920D7
		public OleItem(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010BC7 RID: 68551 RVA: 0x00293EE0 File Offset: 0x002920E0
		public OleItem(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010BC8 RID: 68552 RVA: 0x00293EE9 File Offset: 0x002920E9
		public OleItem(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06010BC9 RID: 68553 RVA: 0x002E684F File Offset: 0x002E4A4F
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (53 == namespaceId && "values" == name)
			{
				return new DdeValues();
			}
			return null;
		}

		// Token: 0x17004DB9 RID: 19897
		// (get) Token: 0x06010BCA RID: 68554 RVA: 0x002E686A File Offset: 0x002E4A6A
		internal override string[] ElementTagNames
		{
			get
			{
				return OleItem.eleTagNames;
			}
		}

		// Token: 0x17004DBA RID: 19898
		// (get) Token: 0x06010BCB RID: 68555 RVA: 0x002E6871 File Offset: 0x002E4A71
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return OleItem.eleNamespaceIds;
			}
		}

		// Token: 0x17004DBB RID: 19899
		// (get) Token: 0x06010BCC RID: 68556 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17004DBC RID: 19900
		// (get) Token: 0x06010BCD RID: 68557 RVA: 0x002E6878 File Offset: 0x002E4A78
		// (set) Token: 0x06010BCE RID: 68558 RVA: 0x002E6881 File Offset: 0x002E4A81
		public DdeValues DdeValues
		{
			get
			{
				return base.GetElement<DdeValues>(0);
			}
			set
			{
				base.SetElement<DdeValues>(0, value);
			}
		}

		// Token: 0x06010BCF RID: 68559 RVA: 0x002E688C File Offset: 0x002E4A8C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "icon" == name)
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

		// Token: 0x06010BD0 RID: 68560 RVA: 0x002E68F9 File Offset: 0x002E4AF9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<OleItem>(deep);
		}

		// Token: 0x06010BD1 RID: 68561 RVA: 0x002E6904 File Offset: 0x002E4B04
		// Note: this type is marked as 'beforefieldinit'.
		static OleItem()
		{
			byte[] array = new byte[4];
			OleItem.attributeNamespaceIds = array;
			OleItem.eleTagNames = new string[] { "values" };
			OleItem.eleNamespaceIds = new byte[] { 53 };
		}

		// Token: 0x0400761C RID: 30236
		private const string tagName = "oleItem";

		// Token: 0x0400761D RID: 30237
		private const byte tagNsId = 53;

		// Token: 0x0400761E RID: 30238
		internal const int ElementTypeIdConst = 12917;

		// Token: 0x0400761F RID: 30239
		private static string[] attributeTagNames = new string[] { "name", "icon", "advise", "preferPic" };

		// Token: 0x04007620 RID: 30240
		private static byte[] attributeNamespaceIds;

		// Token: 0x04007621 RID: 30241
		private static readonly string[] eleTagNames;

		// Token: 0x04007622 RID: 30242
		private static readonly byte[] eleNamespaceIds;
	}
}
