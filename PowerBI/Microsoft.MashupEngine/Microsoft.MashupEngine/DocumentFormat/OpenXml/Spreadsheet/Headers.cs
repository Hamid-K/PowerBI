using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B1E RID: 11038
	[ChildElementInfo(typeof(Header))]
	[GeneratedCode("DomGen", "2.0")]
	internal class Headers : OpenXmlPartRootElement
	{
		// Token: 0x17007683 RID: 30339
		// (get) Token: 0x060167E2 RID: 92130 RVA: 0x002E897B File Offset: 0x002E6B7B
		public override string LocalName
		{
			get
			{
				return "headers";
			}
		}

		// Token: 0x17007684 RID: 30340
		// (get) Token: 0x060167E3 RID: 92131 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007685 RID: 30341
		// (get) Token: 0x060167E4 RID: 92132 RVA: 0x0032B253 File Offset: 0x00329453
		internal override int ElementTypeId
		{
			get
			{
				return 11036;
			}
		}

		// Token: 0x060167E5 RID: 92133 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007686 RID: 30342
		// (get) Token: 0x060167E6 RID: 92134 RVA: 0x0032B25A File Offset: 0x0032945A
		internal override string[] AttributeTagNames
		{
			get
			{
				return Headers.attributeTagNames;
			}
		}

		// Token: 0x17007687 RID: 30343
		// (get) Token: 0x060167E7 RID: 92135 RVA: 0x0032B261 File Offset: 0x00329461
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Headers.attributeNamespaceIds;
			}
		}

		// Token: 0x17007688 RID: 30344
		// (get) Token: 0x060167E8 RID: 92136 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x060167E9 RID: 92137 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17007689 RID: 30345
		// (get) Token: 0x060167EA RID: 92138 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x060167EB RID: 92139 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "lastGuid")]
		public StringValue LastGuid
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

		// Token: 0x1700768A RID: 30346
		// (get) Token: 0x060167EC RID: 92140 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x060167ED RID: 92141 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "shared")]
		public BooleanValue Shared
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

		// Token: 0x1700768B RID: 30347
		// (get) Token: 0x060167EE RID: 92142 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x060167EF RID: 92143 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "diskRevisions")]
		public BooleanValue DiskRevisions
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

		// Token: 0x1700768C RID: 30348
		// (get) Token: 0x060167F0 RID: 92144 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x060167F1 RID: 92145 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "history")]
		public BooleanValue History
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

		// Token: 0x1700768D RID: 30349
		// (get) Token: 0x060167F2 RID: 92146 RVA: 0x002D7D70 File Offset: 0x002D5F70
		// (set) Token: 0x060167F3 RID: 92147 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "trackRevisions")]
		public BooleanValue TrackRevisions
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

		// Token: 0x1700768E RID: 30350
		// (get) Token: 0x060167F4 RID: 92148 RVA: 0x002CCC33 File Offset: 0x002CAE33
		// (set) Token: 0x060167F5 RID: 92149 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "exclusive")]
		public BooleanValue Exclusive
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

		// Token: 0x1700768F RID: 30351
		// (get) Token: 0x060167F6 RID: 92150 RVA: 0x0032B268 File Offset: 0x00329468
		// (set) Token: 0x060167F7 RID: 92151 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "revisionId")]
		public UInt32Value RevisionId
		{
			get
			{
				return (UInt32Value)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x17007690 RID: 30352
		// (get) Token: 0x060167F8 RID: 92152 RVA: 0x002ED55B File Offset: 0x002EB75B
		// (set) Token: 0x060167F9 RID: 92153 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "version")]
		public Int32Value Version
		{
			get
			{
				return (Int32Value)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x17007691 RID: 30353
		// (get) Token: 0x060167FA RID: 92154 RVA: 0x002C92EA File Offset: 0x002C74EA
		// (set) Token: 0x060167FB RID: 92155 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "keepChangeHistory")]
		public BooleanValue KeepChangeHistory
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

		// Token: 0x17007692 RID: 30354
		// (get) Token: 0x060167FC RID: 92156 RVA: 0x002C8762 File Offset: 0x002C6962
		// (set) Token: 0x060167FD RID: 92157 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "protected")]
		public BooleanValue Protected
		{
			get
			{
				return (BooleanValue)base.Attributes[10];
			}
			set
			{
				base.Attributes[10] = value;
			}
		}

		// Token: 0x17007693 RID: 30355
		// (get) Token: 0x060167FE RID: 92158 RVA: 0x002E9686 File Offset: 0x002E7886
		// (set) Token: 0x060167FF RID: 92159 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "preserveHistory")]
		public UInt32Value PreserveHistory
		{
			get
			{
				return (UInt32Value)base.Attributes[11];
			}
			set
			{
				base.Attributes[11] = value;
			}
		}

		// Token: 0x06016800 RID: 92160 RVA: 0x002CEBD9 File Offset: 0x002CCDD9
		internal Headers(WorkbookRevisionHeaderPart ownerPart)
			: base(ownerPart)
		{
		}

		// Token: 0x06016801 RID: 92161 RVA: 0x002CEBE2 File Offset: 0x002CCDE2
		public void Load(WorkbookRevisionHeaderPart openXmlPart)
		{
			base.LoadFromPart(openXmlPart);
		}

		// Token: 0x17007694 RID: 30356
		// (get) Token: 0x06016802 RID: 92162 RVA: 0x0032B277 File Offset: 0x00329477
		// (set) Token: 0x06016803 RID: 92163 RVA: 0x002CEC01 File Offset: 0x002CCE01
		public WorkbookRevisionHeaderPart WorkbookRevisionHeaderPart
		{
			get
			{
				return base.OpenXmlPart as WorkbookRevisionHeaderPart;
			}
			internal set
			{
				base.OpenXmlPart = value;
			}
		}

		// Token: 0x06016804 RID: 92164 RVA: 0x002CEB18 File Offset: 0x002CCD18
		public Headers(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016805 RID: 92165 RVA: 0x002CEB21 File Offset: 0x002CCD21
		public Headers(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016806 RID: 92166 RVA: 0x002CEB2A File Offset: 0x002CCD2A
		public Headers(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016807 RID: 92167 RVA: 0x002CEB10 File Offset: 0x002CCD10
		public Headers()
		{
		}

		// Token: 0x06016808 RID: 92168 RVA: 0x002CEBEB File Offset: 0x002CCDEB
		public void Save(WorkbookRevisionHeaderPart openXmlPart)
		{
			base.SaveToPart(openXmlPart);
		}

		// Token: 0x06016809 RID: 92169 RVA: 0x0032B284 File Offset: 0x00329484
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "header" == name)
			{
				return new Header();
			}
			return null;
		}

		// Token: 0x0601680A RID: 92170 RVA: 0x0032B2A0 File Offset: 0x003294A0
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "guid" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "lastGuid" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "shared" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "diskRevisions" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "history" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "trackRevisions" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "exclusive" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "revisionId" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "version" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "keepChangeHistory" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "protected" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "preserveHistory" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601680B RID: 92171 RVA: 0x0032B3BD File Offset: 0x003295BD
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Headers>(deep);
		}

		// Token: 0x0601680C RID: 92172 RVA: 0x0032B3C8 File Offset: 0x003295C8
		// Note: this type is marked as 'beforefieldinit'.
		static Headers()
		{
			byte[] array = new byte[12];
			Headers.attributeNamespaceIds = array;
		}

		// Token: 0x040098F9 RID: 39161
		private const string tagName = "headers";

		// Token: 0x040098FA RID: 39162
		private const byte tagNsId = 22;

		// Token: 0x040098FB RID: 39163
		internal const int ElementTypeIdConst = 11036;

		// Token: 0x040098FC RID: 39164
		private static string[] attributeTagNames = new string[]
		{
			"guid", "lastGuid", "shared", "diskRevisions", "history", "trackRevisions", "exclusive", "revisionId", "version", "keepChangeHistory",
			"protected", "preserveHistory"
		};

		// Token: 0x040098FD RID: 39165
		private static byte[] attributeNamespaceIds;
	}
}
