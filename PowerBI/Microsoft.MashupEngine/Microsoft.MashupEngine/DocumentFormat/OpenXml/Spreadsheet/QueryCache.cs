using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B6A RID: 11114
	[ChildElementInfo(typeof(Query))]
	[GeneratedCode("DomGen", "2.0")]
	internal class QueryCache : OpenXmlCompositeElement
	{
		// Token: 0x17007923 RID: 31011
		// (get) Token: 0x06016DF7 RID: 93687 RVA: 0x00330043 File Offset: 0x0032E243
		public override string LocalName
		{
			get
			{
				return "queryCache";
			}
		}

		// Token: 0x17007924 RID: 31012
		// (get) Token: 0x06016DF8 RID: 93688 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007925 RID: 31013
		// (get) Token: 0x06016DF9 RID: 93689 RVA: 0x0033004A File Offset: 0x0032E24A
		internal override int ElementTypeId
		{
			get
			{
				return 11093;
			}
		}

		// Token: 0x06016DFA RID: 93690 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007926 RID: 31014
		// (get) Token: 0x06016DFB RID: 93691 RVA: 0x00330051 File Offset: 0x0032E251
		internal override string[] AttributeTagNames
		{
			get
			{
				return QueryCache.attributeTagNames;
			}
		}

		// Token: 0x17007927 RID: 31015
		// (get) Token: 0x06016DFC RID: 93692 RVA: 0x00330058 File Offset: 0x0032E258
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return QueryCache.attributeNamespaceIds;
			}
		}

		// Token: 0x17007928 RID: 31016
		// (get) Token: 0x06016DFD RID: 93693 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06016DFE RID: 93694 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "count")]
		public UInt32Value Count
		{
			get
			{
				return (UInt32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06016DFF RID: 93695 RVA: 0x00293ECF File Offset: 0x002920CF
		public QueryCache()
		{
		}

		// Token: 0x06016E00 RID: 93696 RVA: 0x00293ED7 File Offset: 0x002920D7
		public QueryCache(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016E01 RID: 93697 RVA: 0x00293EE0 File Offset: 0x002920E0
		public QueryCache(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016E02 RID: 93698 RVA: 0x00293EE9 File Offset: 0x002920E9
		public QueryCache(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016E03 RID: 93699 RVA: 0x0033005F File Offset: 0x0032E25F
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "query" == name)
			{
				return new Query();
			}
			return null;
		}

		// Token: 0x06016E04 RID: 93700 RVA: 0x002E67DA File Offset: 0x002E49DA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06016E05 RID: 93701 RVA: 0x0033007A File Offset: 0x0032E27A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<QueryCache>(deep);
		}

		// Token: 0x06016E06 RID: 93702 RVA: 0x00330084 File Offset: 0x0032E284
		// Note: this type is marked as 'beforefieldinit'.
		static QueryCache()
		{
			byte[] array = new byte[1];
			QueryCache.attributeNamespaceIds = array;
		}

		// Token: 0x04009A46 RID: 39494
		private const string tagName = "queryCache";

		// Token: 0x04009A47 RID: 39495
		private const byte tagNsId = 22;

		// Token: 0x04009A48 RID: 39496
		internal const int ElementTypeIdConst = 11093;

		// Token: 0x04009A49 RID: 39497
		private static string[] attributeTagNames = new string[] { "count" };

		// Token: 0x04009A4A RID: 39498
		private static byte[] attributeNamespaceIds;
	}
}
