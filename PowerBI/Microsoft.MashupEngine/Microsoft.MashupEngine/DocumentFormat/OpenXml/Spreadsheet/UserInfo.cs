using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BCB RID: 11211
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	internal class UserInfo : OpenXmlCompositeElement
	{
		// Token: 0x17007CCE RID: 31950
		// (get) Token: 0x060175CF RID: 95695 RVA: 0x00335D96 File Offset: 0x00333F96
		public override string LocalName
		{
			get
			{
				return "userInfo";
			}
		}

		// Token: 0x17007CCF RID: 31951
		// (get) Token: 0x060175D0 RID: 95696 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007CD0 RID: 31952
		// (get) Token: 0x060175D1 RID: 95697 RVA: 0x00335D9D File Offset: 0x00333F9D
		internal override int ElementTypeId
		{
			get
			{
				return 11183;
			}
		}

		// Token: 0x060175D2 RID: 95698 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007CD1 RID: 31953
		// (get) Token: 0x060175D3 RID: 95699 RVA: 0x00335DA4 File Offset: 0x00333FA4
		internal override string[] AttributeTagNames
		{
			get
			{
				return UserInfo.attributeTagNames;
			}
		}

		// Token: 0x17007CD2 RID: 31954
		// (get) Token: 0x060175D4 RID: 95700 RVA: 0x00335DAB File Offset: 0x00333FAB
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return UserInfo.attributeNamespaceIds;
			}
		}

		// Token: 0x17007CD3 RID: 31955
		// (get) Token: 0x060175D5 RID: 95701 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x060175D6 RID: 95702 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "guid")]
		public StringValue Guid
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

		// Token: 0x17007CD4 RID: 31956
		// (get) Token: 0x060175D7 RID: 95703 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x060175D8 RID: 95704 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "name")]
		public StringValue Name
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

		// Token: 0x17007CD5 RID: 31957
		// (get) Token: 0x060175D9 RID: 95705 RVA: 0x002E1683 File Offset: 0x002DF883
		// (set) Token: 0x060175DA RID: 95706 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "id")]
		public Int32Value Id
		{
			get
			{
				return (Int32Value)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17007CD6 RID: 31958
		// (get) Token: 0x060175DB RID: 95707 RVA: 0x00335DB2 File Offset: 0x00333FB2
		// (set) Token: 0x060175DC RID: 95708 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "dateTime")]
		public DateTimeValue DateTime
		{
			get
			{
				return (DateTimeValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x060175DD RID: 95709 RVA: 0x00293ECF File Offset: 0x002920CF
		public UserInfo()
		{
		}

		// Token: 0x060175DE RID: 95710 RVA: 0x00293ED7 File Offset: 0x002920D7
		public UserInfo(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060175DF RID: 95711 RVA: 0x00293EE0 File Offset: 0x002920E0
		public UserInfo(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060175E0 RID: 95712 RVA: 0x00293EE9 File Offset: 0x002920E9
		public UserInfo(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060175E1 RID: 95713 RVA: 0x003328DF File Offset: 0x00330ADF
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17007CD7 RID: 31959
		// (get) Token: 0x060175E2 RID: 95714 RVA: 0x00335DC1 File Offset: 0x00333FC1
		internal override string[] ElementTagNames
		{
			get
			{
				return UserInfo.eleTagNames;
			}
		}

		// Token: 0x17007CD8 RID: 31960
		// (get) Token: 0x060175E3 RID: 95715 RVA: 0x00335DC8 File Offset: 0x00333FC8
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return UserInfo.eleNamespaceIds;
			}
		}

		// Token: 0x17007CD9 RID: 31961
		// (get) Token: 0x060175E4 RID: 95716 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17007CDA RID: 31962
		// (get) Token: 0x060175E5 RID: 95717 RVA: 0x00332908 File Offset: 0x00330B08
		// (set) Token: 0x060175E6 RID: 95718 RVA: 0x00332911 File Offset: 0x00330B11
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(0);
			}
			set
			{
				base.SetElement<ExtensionList>(0, value);
			}
		}

		// Token: 0x060175E7 RID: 95719 RVA: 0x00335DD0 File Offset: 0x00333FD0
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "guid" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "id" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "dateTime" == name)
			{
				return new DateTimeValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060175E8 RID: 95720 RVA: 0x00335E3D File Offset: 0x0033403D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<UserInfo>(deep);
		}

		// Token: 0x060175E9 RID: 95721 RVA: 0x00335E48 File Offset: 0x00334048
		// Note: this type is marked as 'beforefieldinit'.
		static UserInfo()
		{
			byte[] array = new byte[4];
			UserInfo.attributeNamespaceIds = array;
			UserInfo.eleTagNames = new string[] { "extLst" };
			UserInfo.eleNamespaceIds = new byte[] { 22 };
		}

		// Token: 0x04009C14 RID: 39956
		private const string tagName = "userInfo";

		// Token: 0x04009C15 RID: 39957
		private const byte tagNsId = 22;

		// Token: 0x04009C16 RID: 39958
		internal const int ElementTypeIdConst = 11183;

		// Token: 0x04009C17 RID: 39959
		private static string[] attributeTagNames = new string[] { "guid", "name", "id", "dateTime" };

		// Token: 0x04009C18 RID: 39960
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009C19 RID: 39961
		private static readonly string[] eleTagNames;

		// Token: 0x04009C1A RID: 39962
		private static readonly byte[] eleNamespaceIds;
	}
}
