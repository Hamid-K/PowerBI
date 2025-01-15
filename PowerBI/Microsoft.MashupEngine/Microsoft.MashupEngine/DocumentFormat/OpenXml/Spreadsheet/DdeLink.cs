using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C27 RID: 11303
	[ChildElementInfo(typeof(DdeItems))]
	[GeneratedCode("DomGen", "2.0")]
	internal class DdeLink : OpenXmlCompositeElement
	{
		// Token: 0x1700809E RID: 32926
		// (get) Token: 0x06017E00 RID: 97792 RVA: 0x0033C123 File Offset: 0x0033A323
		public override string LocalName
		{
			get
			{
				return "ddeLink";
			}
		}

		// Token: 0x1700809F RID: 32927
		// (get) Token: 0x06017E01 RID: 97793 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170080A0 RID: 32928
		// (get) Token: 0x06017E02 RID: 97794 RVA: 0x0033C12A File Offset: 0x0033A32A
		internal override int ElementTypeId
		{
			get
			{
				return 11284;
			}
		}

		// Token: 0x06017E03 RID: 97795 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170080A1 RID: 32929
		// (get) Token: 0x06017E04 RID: 97796 RVA: 0x0033C131 File Offset: 0x0033A331
		internal override string[] AttributeTagNames
		{
			get
			{
				return DdeLink.attributeTagNames;
			}
		}

		// Token: 0x170080A2 RID: 32930
		// (get) Token: 0x06017E05 RID: 97797 RVA: 0x0033C138 File Offset: 0x0033A338
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return DdeLink.attributeNamespaceIds;
			}
		}

		// Token: 0x170080A3 RID: 32931
		// (get) Token: 0x06017E06 RID: 97798 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06017E07 RID: 97799 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "ddeService")]
		public StringValue DdeService
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

		// Token: 0x170080A4 RID: 32932
		// (get) Token: 0x06017E08 RID: 97800 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06017E09 RID: 97801 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "ddeTopic")]
		public StringValue DdeTopic
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

		// Token: 0x06017E0A RID: 97802 RVA: 0x00293ECF File Offset: 0x002920CF
		public DdeLink()
		{
		}

		// Token: 0x06017E0B RID: 97803 RVA: 0x00293ED7 File Offset: 0x002920D7
		public DdeLink(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017E0C RID: 97804 RVA: 0x00293EE0 File Offset: 0x002920E0
		public DdeLink(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017E0D RID: 97805 RVA: 0x00293EE9 File Offset: 0x002920E9
		public DdeLink(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06017E0E RID: 97806 RVA: 0x0033C13F File Offset: 0x0033A33F
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "ddeItems" == name)
			{
				return new DdeItems();
			}
			return null;
		}

		// Token: 0x170080A5 RID: 32933
		// (get) Token: 0x06017E0F RID: 97807 RVA: 0x0033C15A File Offset: 0x0033A35A
		internal override string[] ElementTagNames
		{
			get
			{
				return DdeLink.eleTagNames;
			}
		}

		// Token: 0x170080A6 RID: 32934
		// (get) Token: 0x06017E10 RID: 97808 RVA: 0x0033C161 File Offset: 0x0033A361
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return DdeLink.eleNamespaceIds;
			}
		}

		// Token: 0x170080A7 RID: 32935
		// (get) Token: 0x06017E11 RID: 97809 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170080A8 RID: 32936
		// (get) Token: 0x06017E12 RID: 97810 RVA: 0x0033C168 File Offset: 0x0033A368
		// (set) Token: 0x06017E13 RID: 97811 RVA: 0x0033C171 File Offset: 0x0033A371
		public DdeItems DdeItems
		{
			get
			{
				return base.GetElement<DdeItems>(0);
			}
			set
			{
				base.SetElement<DdeItems>(0, value);
			}
		}

		// Token: 0x06017E14 RID: 97812 RVA: 0x0033C17B File Offset: 0x0033A37B
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "ddeService" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "ddeTopic" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017E15 RID: 97813 RVA: 0x0033C1B1 File Offset: 0x0033A3B1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DdeLink>(deep);
		}

		// Token: 0x06017E16 RID: 97814 RVA: 0x0033C1BC File Offset: 0x0033A3BC
		// Note: this type is marked as 'beforefieldinit'.
		static DdeLink()
		{
			byte[] array = new byte[2];
			DdeLink.attributeNamespaceIds = array;
			DdeLink.eleTagNames = new string[] { "ddeItems" };
			DdeLink.eleNamespaceIds = new byte[] { 22 };
		}

		// Token: 0x04009DF4 RID: 40436
		private const string tagName = "ddeLink";

		// Token: 0x04009DF5 RID: 40437
		private const byte tagNsId = 22;

		// Token: 0x04009DF6 RID: 40438
		internal const int ElementTypeIdConst = 11284;

		// Token: 0x04009DF7 RID: 40439
		private static string[] attributeTagNames = new string[] { "ddeService", "ddeTopic" };

		// Token: 0x04009DF8 RID: 40440
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009DF9 RID: 40441
		private static readonly string[] eleTagNames;

		// Token: 0x04009DFA RID: 40442
		private static readonly byte[] eleNamespaceIds;
	}
}
