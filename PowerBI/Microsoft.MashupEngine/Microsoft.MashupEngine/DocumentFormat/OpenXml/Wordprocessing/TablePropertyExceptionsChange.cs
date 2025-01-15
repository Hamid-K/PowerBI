using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F62 RID: 12130
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(PreviousTablePropertyExceptions))]
	internal class TablePropertyExceptionsChange : OpenXmlCompositeElement
	{
		// Token: 0x17009090 RID: 37008
		// (get) Token: 0x0601A134 RID: 106804 RVA: 0x0035D2C7 File Offset: 0x0035B4C7
		public override string LocalName
		{
			get
			{
				return "tblPrExChange";
			}
		}

		// Token: 0x17009091 RID: 37009
		// (get) Token: 0x0601A135 RID: 106805 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17009092 RID: 37010
		// (get) Token: 0x0601A136 RID: 106806 RVA: 0x0035D2CE File Offset: 0x0035B4CE
		internal override int ElementTypeId
		{
			get
			{
				return 11788;
			}
		}

		// Token: 0x0601A137 RID: 106807 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17009093 RID: 37011
		// (get) Token: 0x0601A138 RID: 106808 RVA: 0x0035D2D5 File Offset: 0x0035B4D5
		internal override string[] AttributeTagNames
		{
			get
			{
				return TablePropertyExceptionsChange.attributeTagNames;
			}
		}

		// Token: 0x17009094 RID: 37012
		// (get) Token: 0x0601A139 RID: 106809 RVA: 0x0035D2DC File Offset: 0x0035B4DC
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TablePropertyExceptionsChange.attributeNamespaceIds;
			}
		}

		// Token: 0x17009095 RID: 37013
		// (get) Token: 0x0601A13A RID: 106810 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601A13B RID: 106811 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "author")]
		public StringValue Author
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

		// Token: 0x17009096 RID: 37014
		// (get) Token: 0x0601A13C RID: 106812 RVA: 0x002EC18F File Offset: 0x002EA38F
		// (set) Token: 0x0601A13D RID: 106813 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "date")]
		public DateTimeValue Date
		{
			get
			{
				return (DateTimeValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17009097 RID: 37015
		// (get) Token: 0x0601A13E RID: 106814 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0601A13F RID: 106815 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(23, "id")]
		public StringValue Id
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

		// Token: 0x0601A140 RID: 106816 RVA: 0x00293ECF File Offset: 0x002920CF
		public TablePropertyExceptionsChange()
		{
		}

		// Token: 0x0601A141 RID: 106817 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TablePropertyExceptionsChange(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A142 RID: 106818 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TablePropertyExceptionsChange(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A143 RID: 106819 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TablePropertyExceptionsChange(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601A144 RID: 106820 RVA: 0x0035D2E3 File Offset: 0x0035B4E3
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "tblPrEx" == name)
			{
				return new PreviousTablePropertyExceptions();
			}
			return null;
		}

		// Token: 0x17009098 RID: 37016
		// (get) Token: 0x0601A145 RID: 106821 RVA: 0x0035D2FE File Offset: 0x0035B4FE
		internal override string[] ElementTagNames
		{
			get
			{
				return TablePropertyExceptionsChange.eleTagNames;
			}
		}

		// Token: 0x17009099 RID: 37017
		// (get) Token: 0x0601A146 RID: 106822 RVA: 0x0035D305 File Offset: 0x0035B505
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return TablePropertyExceptionsChange.eleNamespaceIds;
			}
		}

		// Token: 0x1700909A RID: 37018
		// (get) Token: 0x0601A147 RID: 106823 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700909B RID: 37019
		// (get) Token: 0x0601A148 RID: 106824 RVA: 0x0035D30C File Offset: 0x0035B50C
		// (set) Token: 0x0601A149 RID: 106825 RVA: 0x0035D315 File Offset: 0x0035B515
		public PreviousTablePropertyExceptions PreviousTablePropertyExceptions
		{
			get
			{
				return base.GetElement<PreviousTablePropertyExceptions>(0);
			}
			set
			{
				base.SetElement<PreviousTablePropertyExceptions>(0, value);
			}
		}

		// Token: 0x0601A14A RID: 106826 RVA: 0x0035D320 File Offset: 0x0035B520
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "author" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "date" == name)
			{
				return new DateTimeValue();
			}
			if (23 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601A14B RID: 106827 RVA: 0x0035D37D File Offset: 0x0035B57D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TablePropertyExceptionsChange>(deep);
		}

		// Token: 0x0400ABAD RID: 43949
		private const string tagName = "tblPrExChange";

		// Token: 0x0400ABAE RID: 43950
		private const byte tagNsId = 23;

		// Token: 0x0400ABAF RID: 43951
		internal const int ElementTypeIdConst = 11788;

		// Token: 0x0400ABB0 RID: 43952
		private static string[] attributeTagNames = new string[] { "author", "date", "id" };

		// Token: 0x0400ABB1 RID: 43953
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23, 23 };

		// Token: 0x0400ABB2 RID: 43954
		private static readonly string[] eleTagNames = new string[] { "tblPrEx" };

		// Token: 0x0400ABB3 RID: 43955
		private static readonly byte[] eleNamespaceIds = new byte[] { 23 };
	}
}
