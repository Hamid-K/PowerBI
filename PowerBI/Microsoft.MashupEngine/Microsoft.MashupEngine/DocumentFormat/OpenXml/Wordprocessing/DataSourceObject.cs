using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F76 RID: 12150
	[ChildElementInfo(typeof(MailMergeSource))]
	[ChildElementInfo(typeof(ColumnDelimiter))]
	[ChildElementInfo(typeof(SourceReference))]
	[ChildElementInfo(typeof(RecipientDataReference))]
	[ChildElementInfo(typeof(UdlConnectionString))]
	[ChildElementInfo(typeof(DataSourceTableName))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(FirstRowHeader))]
	[ChildElementInfo(typeof(FieldMapData))]
	internal class DataSourceObject : OpenXmlCompositeElement
	{
		// Token: 0x17009113 RID: 37139
		// (get) Token: 0x0601A247 RID: 107079 RVA: 0x0035DF08 File Offset: 0x0035C108
		public override string LocalName
		{
			get
			{
				return "odso";
			}
		}

		// Token: 0x17009114 RID: 37140
		// (get) Token: 0x0601A248 RID: 107080 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17009115 RID: 37141
		// (get) Token: 0x0601A249 RID: 107081 RVA: 0x0035DF0F File Offset: 0x0035C10F
		internal override int ElementTypeId
		{
			get
			{
				return 11827;
			}
		}

		// Token: 0x0601A24A RID: 107082 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601A24B RID: 107083 RVA: 0x00293ECF File Offset: 0x002920CF
		public DataSourceObject()
		{
		}

		// Token: 0x0601A24C RID: 107084 RVA: 0x00293ED7 File Offset: 0x002920D7
		public DataSourceObject(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A24D RID: 107085 RVA: 0x00293EE0 File Offset: 0x002920E0
		public DataSourceObject(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A24E RID: 107086 RVA: 0x00293EE9 File Offset: 0x002920E9
		public DataSourceObject(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601A24F RID: 107087 RVA: 0x0035DF18 File Offset: 0x0035C118
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "udl" == name)
			{
				return new UdlConnectionString();
			}
			if (23 == namespaceId && "table" == name)
			{
				return new DataSourceTableName();
			}
			if (23 == namespaceId && "src" == name)
			{
				return new SourceReference();
			}
			if (23 == namespaceId && "colDelim" == name)
			{
				return new ColumnDelimiter();
			}
			if (23 == namespaceId && "type" == name)
			{
				return new MailMergeSource();
			}
			if (23 == namespaceId && "fHdr" == name)
			{
				return new FirstRowHeader();
			}
			if (23 == namespaceId && "fieldMapData" == name)
			{
				return new FieldMapData();
			}
			if (23 == namespaceId && "recipientData" == name)
			{
				return new RecipientDataReference();
			}
			return null;
		}

		// Token: 0x17009116 RID: 37142
		// (get) Token: 0x0601A250 RID: 107088 RVA: 0x0035DFE6 File Offset: 0x0035C1E6
		internal override string[] ElementTagNames
		{
			get
			{
				return DataSourceObject.eleTagNames;
			}
		}

		// Token: 0x17009117 RID: 37143
		// (get) Token: 0x0601A251 RID: 107089 RVA: 0x0035DFED File Offset: 0x0035C1ED
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return DataSourceObject.eleNamespaceIds;
			}
		}

		// Token: 0x17009118 RID: 37144
		// (get) Token: 0x0601A252 RID: 107090 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17009119 RID: 37145
		// (get) Token: 0x0601A253 RID: 107091 RVA: 0x0035DFF4 File Offset: 0x0035C1F4
		// (set) Token: 0x0601A254 RID: 107092 RVA: 0x0035DFFD File Offset: 0x0035C1FD
		public UdlConnectionString UdlConnectionString
		{
			get
			{
				return base.GetElement<UdlConnectionString>(0);
			}
			set
			{
				base.SetElement<UdlConnectionString>(0, value);
			}
		}

		// Token: 0x1700911A RID: 37146
		// (get) Token: 0x0601A255 RID: 107093 RVA: 0x0035E007 File Offset: 0x0035C207
		// (set) Token: 0x0601A256 RID: 107094 RVA: 0x0035E010 File Offset: 0x0035C210
		public DataSourceTableName DataSourceTableName
		{
			get
			{
				return base.GetElement<DataSourceTableName>(1);
			}
			set
			{
				base.SetElement<DataSourceTableName>(1, value);
			}
		}

		// Token: 0x1700911B RID: 37147
		// (get) Token: 0x0601A257 RID: 107095 RVA: 0x0035E01A File Offset: 0x0035C21A
		// (set) Token: 0x0601A258 RID: 107096 RVA: 0x0035E023 File Offset: 0x0035C223
		public SourceReference SourceReference
		{
			get
			{
				return base.GetElement<SourceReference>(2);
			}
			set
			{
				base.SetElement<SourceReference>(2, value);
			}
		}

		// Token: 0x1700911C RID: 37148
		// (get) Token: 0x0601A259 RID: 107097 RVA: 0x0035E02D File Offset: 0x0035C22D
		// (set) Token: 0x0601A25A RID: 107098 RVA: 0x0035E036 File Offset: 0x0035C236
		public ColumnDelimiter ColumnDelimiter
		{
			get
			{
				return base.GetElement<ColumnDelimiter>(3);
			}
			set
			{
				base.SetElement<ColumnDelimiter>(3, value);
			}
		}

		// Token: 0x1700911D RID: 37149
		// (get) Token: 0x0601A25B RID: 107099 RVA: 0x0035E040 File Offset: 0x0035C240
		// (set) Token: 0x0601A25C RID: 107100 RVA: 0x0035E049 File Offset: 0x0035C249
		public MailMergeSource MailMergeSource
		{
			get
			{
				return base.GetElement<MailMergeSource>(4);
			}
			set
			{
				base.SetElement<MailMergeSource>(4, value);
			}
		}

		// Token: 0x1700911E RID: 37150
		// (get) Token: 0x0601A25D RID: 107101 RVA: 0x0035E053 File Offset: 0x0035C253
		// (set) Token: 0x0601A25E RID: 107102 RVA: 0x0035E05C File Offset: 0x0035C25C
		public FirstRowHeader FirstRowHeader
		{
			get
			{
				return base.GetElement<FirstRowHeader>(5);
			}
			set
			{
				base.SetElement<FirstRowHeader>(5, value);
			}
		}

		// Token: 0x0601A25F RID: 107103 RVA: 0x0035E066 File Offset: 0x0035C266
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DataSourceObject>(deep);
		}

		// Token: 0x0400AC03 RID: 44035
		private const string tagName = "odso";

		// Token: 0x0400AC04 RID: 44036
		private const byte tagNsId = 23;

		// Token: 0x0400AC05 RID: 44037
		internal const int ElementTypeIdConst = 11827;

		// Token: 0x0400AC06 RID: 44038
		private static readonly string[] eleTagNames = new string[] { "udl", "table", "src", "colDelim", "type", "fHdr", "fieldMapData", "recipientData" };

		// Token: 0x0400AC07 RID: 44039
		private static readonly byte[] eleNamespaceIds = new byte[] { 23, 23, 23, 23, 23, 23, 23, 23 };
	}
}
