using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002CDB RID: 11483
	[GeneratedCode("DomGen", "2.0")]
	internal class Filter : OpenXmlLeafElement
	{
		// Token: 0x170085E1 RID: 34273
		// (get) Token: 0x06018A74 RID: 100980 RVA: 0x002E6B13 File Offset: 0x002E4D13
		public override string LocalName
		{
			get
			{
				return "filter";
			}
		}

		// Token: 0x170085E2 RID: 34274
		// (get) Token: 0x06018A75 RID: 100981 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170085E3 RID: 34275
		// (get) Token: 0x06018A76 RID: 100982 RVA: 0x0034398F File Offset: 0x00341B8F
		internal override int ElementTypeId
		{
			get
			{
				return 11465;
			}
		}

		// Token: 0x06018A77 RID: 100983 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170085E4 RID: 34276
		// (get) Token: 0x06018A78 RID: 100984 RVA: 0x00343996 File Offset: 0x00341B96
		internal override string[] AttributeTagNames
		{
			get
			{
				return Filter.attributeTagNames;
			}
		}

		// Token: 0x170085E5 RID: 34277
		// (get) Token: 0x06018A79 RID: 100985 RVA: 0x0034399D File Offset: 0x00341B9D
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Filter.attributeNamespaceIds;
			}
		}

		// Token: 0x170085E6 RID: 34278
		// (get) Token: 0x06018A7A RID: 100986 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06018A7B RID: 100987 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public StringValue Val
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

		// Token: 0x06018A7D RID: 100989 RVA: 0x002E6B2F File Offset: 0x002E4D2F
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06018A7E RID: 100990 RVA: 0x003439A4 File Offset: 0x00341BA4
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Filter>(deep);
		}

		// Token: 0x06018A7F RID: 100991 RVA: 0x003439B0 File Offset: 0x00341BB0
		// Note: this type is marked as 'beforefieldinit'.
		static Filter()
		{
			byte[] array = new byte[1];
			Filter.attributeNamespaceIds = array;
		}

		// Token: 0x0400A128 RID: 41256
		private const string tagName = "filter";

		// Token: 0x0400A129 RID: 41257
		private const byte tagNsId = 22;

		// Token: 0x0400A12A RID: 41258
		internal const int ElementTypeIdConst = 11465;

		// Token: 0x0400A12B RID: 41259
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400A12C RID: 41260
		private static byte[] attributeNamespaceIds;
	}
}
