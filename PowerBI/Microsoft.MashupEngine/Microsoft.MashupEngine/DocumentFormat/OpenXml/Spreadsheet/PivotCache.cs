using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C34 RID: 11316
	[GeneratedCode("DomGen", "2.0")]
	internal class PivotCache : OpenXmlLeafElement
	{
		// Token: 0x17008109 RID: 33033
		// (get) Token: 0x06017EEC RID: 98028 RVA: 0x0033CB9F File Offset: 0x0033AD9F
		public override string LocalName
		{
			get
			{
				return "pivotCache";
			}
		}

		// Token: 0x1700810A RID: 33034
		// (get) Token: 0x06017EED RID: 98029 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x1700810B RID: 33035
		// (get) Token: 0x06017EEE RID: 98030 RVA: 0x0033CBA6 File Offset: 0x0033ADA6
		internal override int ElementTypeId
		{
			get
			{
				return 11298;
			}
		}

		// Token: 0x06017EEF RID: 98031 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700810C RID: 33036
		// (get) Token: 0x06017EF0 RID: 98032 RVA: 0x0033CBAD File Offset: 0x0033ADAD
		internal override string[] AttributeTagNames
		{
			get
			{
				return PivotCache.attributeTagNames;
			}
		}

		// Token: 0x1700810D RID: 33037
		// (get) Token: 0x06017EF1 RID: 98033 RVA: 0x0033CBB4 File Offset: 0x0033ADB4
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PivotCache.attributeNamespaceIds;
			}
		}

		// Token: 0x1700810E RID: 33038
		// (get) Token: 0x06017EF2 RID: 98034 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06017EF3 RID: 98035 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "cacheId")]
		public UInt32Value CacheId
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

		// Token: 0x1700810F RID: 33039
		// (get) Token: 0x06017EF4 RID: 98036 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06017EF5 RID: 98037 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(19, "id")]
		public StringValue Id
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

		// Token: 0x06017EF7 RID: 98039 RVA: 0x0033CBBB File Offset: 0x0033ADBB
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "cacheId" == name)
			{
				return new UInt32Value();
			}
			if (19 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017EF8 RID: 98040 RVA: 0x0033CBF3 File Offset: 0x0033ADF3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PivotCache>(deep);
		}

		// Token: 0x04009E36 RID: 40502
		private const string tagName = "pivotCache";

		// Token: 0x04009E37 RID: 40503
		private const byte tagNsId = 22;

		// Token: 0x04009E38 RID: 40504
		internal const int ElementTypeIdConst = 11298;

		// Token: 0x04009E39 RID: 40505
		private static string[] attributeTagNames = new string[] { "cacheId", "id" };

		// Token: 0x04009E3A RID: 40506
		private static byte[] attributeNamespaceIds = new byte[] { 0, 19 };
	}
}
