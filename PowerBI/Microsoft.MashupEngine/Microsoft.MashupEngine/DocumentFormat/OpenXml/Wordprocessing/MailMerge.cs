using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FE1 RID: 12257
	[ChildElementInfo(typeof(DoNotSuppressBlankLines))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(DataSourceObject))]
	[ChildElementInfo(typeof(MainDocumentType))]
	[ChildElementInfo(typeof(LinkToQuery))]
	[ChildElementInfo(typeof(DataType))]
	[ChildElementInfo(typeof(ConnectString))]
	[ChildElementInfo(typeof(Query))]
	[ChildElementInfo(typeof(DataSourceReference))]
	[ChildElementInfo(typeof(HeaderSource))]
	[ChildElementInfo(typeof(Destination))]
	[ChildElementInfo(typeof(AddressFieldName))]
	[ChildElementInfo(typeof(MailSubject))]
	[ChildElementInfo(typeof(MailAsAttachment))]
	[ChildElementInfo(typeof(ViewMergedData))]
	[ChildElementInfo(typeof(ActiveRecord))]
	[ChildElementInfo(typeof(CheckErrors))]
	internal class MailMerge : OpenXmlCompositeElement
	{
		// Token: 0x170094CE RID: 38094
		// (get) Token: 0x0601AA37 RID: 109111 RVA: 0x003653F8 File Offset: 0x003635F8
		public override string LocalName
		{
			get
			{
				return "mailMerge";
			}
		}

		// Token: 0x170094CF RID: 38095
		// (get) Token: 0x0601AA38 RID: 109112 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170094D0 RID: 38096
		// (get) Token: 0x0601AA39 RID: 109113 RVA: 0x003653FF File Offset: 0x003635FF
		internal override int ElementTypeId
		{
			get
			{
				return 11987;
			}
		}

		// Token: 0x0601AA3A RID: 109114 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601AA3B RID: 109115 RVA: 0x00293ECF File Offset: 0x002920CF
		public MailMerge()
		{
		}

		// Token: 0x0601AA3C RID: 109116 RVA: 0x00293ED7 File Offset: 0x002920D7
		public MailMerge(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601AA3D RID: 109117 RVA: 0x00293EE0 File Offset: 0x002920E0
		public MailMerge(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601AA3E RID: 109118 RVA: 0x00293EE9 File Offset: 0x002920E9
		public MailMerge(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601AA3F RID: 109119 RVA: 0x00365408 File Offset: 0x00363608
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "mainDocumentType" == name)
			{
				return new MainDocumentType();
			}
			if (23 == namespaceId && "linkToQuery" == name)
			{
				return new LinkToQuery();
			}
			if (23 == namespaceId && "dataType" == name)
			{
				return new DataType();
			}
			if (23 == namespaceId && "connectString" == name)
			{
				return new ConnectString();
			}
			if (23 == namespaceId && "query" == name)
			{
				return new Query();
			}
			if (23 == namespaceId && "dataSource" == name)
			{
				return new DataSourceReference();
			}
			if (23 == namespaceId && "headerSource" == name)
			{
				return new HeaderSource();
			}
			if (23 == namespaceId && "doNotSuppressBlankLines" == name)
			{
				return new DoNotSuppressBlankLines();
			}
			if (23 == namespaceId && "destination" == name)
			{
				return new Destination();
			}
			if (23 == namespaceId && "addressFieldName" == name)
			{
				return new AddressFieldName();
			}
			if (23 == namespaceId && "mailSubject" == name)
			{
				return new MailSubject();
			}
			if (23 == namespaceId && "mailAsAttachment" == name)
			{
				return new MailAsAttachment();
			}
			if (23 == namespaceId && "viewMergedData" == name)
			{
				return new ViewMergedData();
			}
			if (23 == namespaceId && "activeRecord" == name)
			{
				return new ActiveRecord();
			}
			if (23 == namespaceId && "checkErrors" == name)
			{
				return new CheckErrors();
			}
			if (23 == namespaceId && "odso" == name)
			{
				return new DataSourceObject();
			}
			return null;
		}

		// Token: 0x170094D1 RID: 38097
		// (get) Token: 0x0601AA40 RID: 109120 RVA: 0x00365596 File Offset: 0x00363796
		internal override string[] ElementTagNames
		{
			get
			{
				return MailMerge.eleTagNames;
			}
		}

		// Token: 0x170094D2 RID: 38098
		// (get) Token: 0x0601AA41 RID: 109121 RVA: 0x0036559D File Offset: 0x0036379D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return MailMerge.eleNamespaceIds;
			}
		}

		// Token: 0x170094D3 RID: 38099
		// (get) Token: 0x0601AA42 RID: 109122 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170094D4 RID: 38100
		// (get) Token: 0x0601AA43 RID: 109123 RVA: 0x003655A4 File Offset: 0x003637A4
		// (set) Token: 0x0601AA44 RID: 109124 RVA: 0x003655AD File Offset: 0x003637AD
		public MainDocumentType MainDocumentType
		{
			get
			{
				return base.GetElement<MainDocumentType>(0);
			}
			set
			{
				base.SetElement<MainDocumentType>(0, value);
			}
		}

		// Token: 0x170094D5 RID: 38101
		// (get) Token: 0x0601AA45 RID: 109125 RVA: 0x003655B7 File Offset: 0x003637B7
		// (set) Token: 0x0601AA46 RID: 109126 RVA: 0x003655C0 File Offset: 0x003637C0
		public LinkToQuery LinkToQuery
		{
			get
			{
				return base.GetElement<LinkToQuery>(1);
			}
			set
			{
				base.SetElement<LinkToQuery>(1, value);
			}
		}

		// Token: 0x170094D6 RID: 38102
		// (get) Token: 0x0601AA47 RID: 109127 RVA: 0x003655CA File Offset: 0x003637CA
		// (set) Token: 0x0601AA48 RID: 109128 RVA: 0x003655D3 File Offset: 0x003637D3
		public DataType DataType
		{
			get
			{
				return base.GetElement<DataType>(2);
			}
			set
			{
				base.SetElement<DataType>(2, value);
			}
		}

		// Token: 0x170094D7 RID: 38103
		// (get) Token: 0x0601AA49 RID: 109129 RVA: 0x003655DD File Offset: 0x003637DD
		// (set) Token: 0x0601AA4A RID: 109130 RVA: 0x003655E6 File Offset: 0x003637E6
		public ConnectString ConnectString
		{
			get
			{
				return base.GetElement<ConnectString>(3);
			}
			set
			{
				base.SetElement<ConnectString>(3, value);
			}
		}

		// Token: 0x170094D8 RID: 38104
		// (get) Token: 0x0601AA4B RID: 109131 RVA: 0x003655F0 File Offset: 0x003637F0
		// (set) Token: 0x0601AA4C RID: 109132 RVA: 0x003655F9 File Offset: 0x003637F9
		public Query Query
		{
			get
			{
				return base.GetElement<Query>(4);
			}
			set
			{
				base.SetElement<Query>(4, value);
			}
		}

		// Token: 0x170094D9 RID: 38105
		// (get) Token: 0x0601AA4D RID: 109133 RVA: 0x00365603 File Offset: 0x00363803
		// (set) Token: 0x0601AA4E RID: 109134 RVA: 0x0036560C File Offset: 0x0036380C
		public DataSourceReference DataSourceReference
		{
			get
			{
				return base.GetElement<DataSourceReference>(5);
			}
			set
			{
				base.SetElement<DataSourceReference>(5, value);
			}
		}

		// Token: 0x170094DA RID: 38106
		// (get) Token: 0x0601AA4F RID: 109135 RVA: 0x00365616 File Offset: 0x00363816
		// (set) Token: 0x0601AA50 RID: 109136 RVA: 0x0036561F File Offset: 0x0036381F
		public HeaderSource HeaderSource
		{
			get
			{
				return base.GetElement<HeaderSource>(6);
			}
			set
			{
				base.SetElement<HeaderSource>(6, value);
			}
		}

		// Token: 0x170094DB RID: 38107
		// (get) Token: 0x0601AA51 RID: 109137 RVA: 0x00365629 File Offset: 0x00363829
		// (set) Token: 0x0601AA52 RID: 109138 RVA: 0x00365632 File Offset: 0x00363832
		public DoNotSuppressBlankLines DoNotSuppressBlankLines
		{
			get
			{
				return base.GetElement<DoNotSuppressBlankLines>(7);
			}
			set
			{
				base.SetElement<DoNotSuppressBlankLines>(7, value);
			}
		}

		// Token: 0x170094DC RID: 38108
		// (get) Token: 0x0601AA53 RID: 109139 RVA: 0x0036563C File Offset: 0x0036383C
		// (set) Token: 0x0601AA54 RID: 109140 RVA: 0x00365645 File Offset: 0x00363845
		public Destination Destination
		{
			get
			{
				return base.GetElement<Destination>(8);
			}
			set
			{
				base.SetElement<Destination>(8, value);
			}
		}

		// Token: 0x170094DD RID: 38109
		// (get) Token: 0x0601AA55 RID: 109141 RVA: 0x0036564F File Offset: 0x0036384F
		// (set) Token: 0x0601AA56 RID: 109142 RVA: 0x00365659 File Offset: 0x00363859
		public AddressFieldName AddressFieldName
		{
			get
			{
				return base.GetElement<AddressFieldName>(9);
			}
			set
			{
				base.SetElement<AddressFieldName>(9, value);
			}
		}

		// Token: 0x170094DE RID: 38110
		// (get) Token: 0x0601AA57 RID: 109143 RVA: 0x00365664 File Offset: 0x00363864
		// (set) Token: 0x0601AA58 RID: 109144 RVA: 0x0036566E File Offset: 0x0036386E
		public MailSubject MailSubject
		{
			get
			{
				return base.GetElement<MailSubject>(10);
			}
			set
			{
				base.SetElement<MailSubject>(10, value);
			}
		}

		// Token: 0x170094DF RID: 38111
		// (get) Token: 0x0601AA59 RID: 109145 RVA: 0x00365679 File Offset: 0x00363879
		// (set) Token: 0x0601AA5A RID: 109146 RVA: 0x00365683 File Offset: 0x00363883
		public MailAsAttachment MailAsAttachment
		{
			get
			{
				return base.GetElement<MailAsAttachment>(11);
			}
			set
			{
				base.SetElement<MailAsAttachment>(11, value);
			}
		}

		// Token: 0x170094E0 RID: 38112
		// (get) Token: 0x0601AA5B RID: 109147 RVA: 0x0036568E File Offset: 0x0036388E
		// (set) Token: 0x0601AA5C RID: 109148 RVA: 0x00365698 File Offset: 0x00363898
		public ViewMergedData ViewMergedData
		{
			get
			{
				return base.GetElement<ViewMergedData>(12);
			}
			set
			{
				base.SetElement<ViewMergedData>(12, value);
			}
		}

		// Token: 0x170094E1 RID: 38113
		// (get) Token: 0x0601AA5D RID: 109149 RVA: 0x003656A3 File Offset: 0x003638A3
		// (set) Token: 0x0601AA5E RID: 109150 RVA: 0x003656AD File Offset: 0x003638AD
		public ActiveRecord ActiveRecord
		{
			get
			{
				return base.GetElement<ActiveRecord>(13);
			}
			set
			{
				base.SetElement<ActiveRecord>(13, value);
			}
		}

		// Token: 0x170094E2 RID: 38114
		// (get) Token: 0x0601AA5F RID: 109151 RVA: 0x003656B8 File Offset: 0x003638B8
		// (set) Token: 0x0601AA60 RID: 109152 RVA: 0x003656C2 File Offset: 0x003638C2
		public CheckErrors CheckErrors
		{
			get
			{
				return base.GetElement<CheckErrors>(14);
			}
			set
			{
				base.SetElement<CheckErrors>(14, value);
			}
		}

		// Token: 0x170094E3 RID: 38115
		// (get) Token: 0x0601AA61 RID: 109153 RVA: 0x003656CD File Offset: 0x003638CD
		// (set) Token: 0x0601AA62 RID: 109154 RVA: 0x003656D7 File Offset: 0x003638D7
		public DataSourceObject DataSourceObject
		{
			get
			{
				return base.GetElement<DataSourceObject>(15);
			}
			set
			{
				base.SetElement<DataSourceObject>(15, value);
			}
		}

		// Token: 0x0601AA63 RID: 109155 RVA: 0x003656E2 File Offset: 0x003638E2
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MailMerge>(deep);
		}

		// Token: 0x0400ADD4 RID: 44500
		private const string tagName = "mailMerge";

		// Token: 0x0400ADD5 RID: 44501
		private const byte tagNsId = 23;

		// Token: 0x0400ADD6 RID: 44502
		internal const int ElementTypeIdConst = 11987;

		// Token: 0x0400ADD7 RID: 44503
		private static readonly string[] eleTagNames = new string[]
		{
			"mainDocumentType", "linkToQuery", "dataType", "connectString", "query", "dataSource", "headerSource", "doNotSuppressBlankLines", "destination", "addressFieldName",
			"mailSubject", "mailAsAttachment", "viewMergedData", "activeRecord", "checkErrors", "odso"
		};

		// Token: 0x0400ADD8 RID: 44504
		private static readonly byte[] eleNamespaceIds = new byte[]
		{
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 23, 23
		};
	}
}
