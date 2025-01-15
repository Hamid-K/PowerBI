using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x02002412 RID: 9234
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(TupleSetRows), FileFormatVersions.Office2010)]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(TupleSetHeaders), FileFormatVersions.Office2010)]
	internal class TupleSet : OpenXmlCompositeElement
	{
		// Token: 0x17004EF6 RID: 20214
		// (get) Token: 0x06010E82 RID: 69250 RVA: 0x002E884B File Offset: 0x002E6A4B
		public override string LocalName
		{
			get
			{
				return "tupleSet";
			}
		}

		// Token: 0x17004EF7 RID: 20215
		// (get) Token: 0x06010E83 RID: 69251 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004EF8 RID: 20216
		// (get) Token: 0x06010E84 RID: 69252 RVA: 0x002E8852 File Offset: 0x002E6A52
		internal override int ElementTypeId
		{
			get
			{
				return 12952;
			}
		}

		// Token: 0x06010E85 RID: 69253 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004EF9 RID: 20217
		// (get) Token: 0x06010E86 RID: 69254 RVA: 0x002E8859 File Offset: 0x002E6A59
		internal override string[] AttributeTagNames
		{
			get
			{
				return TupleSet.attributeTagNames;
			}
		}

		// Token: 0x17004EFA RID: 20218
		// (get) Token: 0x06010E87 RID: 69255 RVA: 0x002E8860 File Offset: 0x002E6A60
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TupleSet.attributeNamespaceIds;
			}
		}

		// Token: 0x17004EFB RID: 20219
		// (get) Token: 0x06010E88 RID: 69256 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06010E89 RID: 69257 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "rowCount")]
		public UInt32Value RowCount
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

		// Token: 0x17004EFC RID: 20220
		// (get) Token: 0x06010E8A RID: 69258 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x06010E8B RID: 69259 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "columnCount")]
		public UInt32Value ColumnCount
		{
			get
			{
				return (UInt32Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x06010E8C RID: 69260 RVA: 0x00293ECF File Offset: 0x002920CF
		public TupleSet()
		{
		}

		// Token: 0x06010E8D RID: 69261 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TupleSet(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010E8E RID: 69262 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TupleSet(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010E8F RID: 69263 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TupleSet(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06010E90 RID: 69264 RVA: 0x002E8867 File Offset: 0x002E6A67
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (53 == namespaceId && "headers" == name)
			{
				return new TupleSetHeaders();
			}
			if (53 == namespaceId && "rows" == name)
			{
				return new TupleSetRows();
			}
			return null;
		}

		// Token: 0x17004EFD RID: 20221
		// (get) Token: 0x06010E91 RID: 69265 RVA: 0x002E889A File Offset: 0x002E6A9A
		internal override string[] ElementTagNames
		{
			get
			{
				return TupleSet.eleTagNames;
			}
		}

		// Token: 0x17004EFE RID: 20222
		// (get) Token: 0x06010E92 RID: 69266 RVA: 0x002E88A1 File Offset: 0x002E6AA1
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return TupleSet.eleNamespaceIds;
			}
		}

		// Token: 0x17004EFF RID: 20223
		// (get) Token: 0x06010E93 RID: 69267 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17004F00 RID: 20224
		// (get) Token: 0x06010E94 RID: 69268 RVA: 0x002E88A8 File Offset: 0x002E6AA8
		// (set) Token: 0x06010E95 RID: 69269 RVA: 0x002E88B1 File Offset: 0x002E6AB1
		public TupleSetHeaders TupleSetHeaders
		{
			get
			{
				return base.GetElement<TupleSetHeaders>(0);
			}
			set
			{
				base.SetElement<TupleSetHeaders>(0, value);
			}
		}

		// Token: 0x17004F01 RID: 20225
		// (get) Token: 0x06010E96 RID: 69270 RVA: 0x002E88BB File Offset: 0x002E6ABB
		// (set) Token: 0x06010E97 RID: 69271 RVA: 0x002E88C4 File Offset: 0x002E6AC4
		public TupleSetRows TupleSetRows
		{
			get
			{
				return base.GetElement<TupleSetRows>(1);
			}
			set
			{
				base.SetElement<TupleSetRows>(1, value);
			}
		}

		// Token: 0x06010E98 RID: 69272 RVA: 0x002E88CE File Offset: 0x002E6ACE
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "rowCount" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "columnCount" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010E99 RID: 69273 RVA: 0x002E8904 File Offset: 0x002E6B04
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TupleSet>(deep);
		}

		// Token: 0x06010E9A RID: 69274 RVA: 0x002E8910 File Offset: 0x002E6B10
		// Note: this type is marked as 'beforefieldinit'.
		static TupleSet()
		{
			byte[] array = new byte[2];
			TupleSet.attributeNamespaceIds = array;
			TupleSet.eleTagNames = new string[] { "headers", "rows" };
			TupleSet.eleNamespaceIds = new byte[] { 53, 53 };
		}

		// Token: 0x040076D3 RID: 30419
		private const string tagName = "tupleSet";

		// Token: 0x040076D4 RID: 30420
		private const byte tagNsId = 53;

		// Token: 0x040076D5 RID: 30421
		internal const int ElementTypeIdConst = 12952;

		// Token: 0x040076D6 RID: 30422
		private static string[] attributeTagNames = new string[] { "rowCount", "columnCount" };

		// Token: 0x040076D7 RID: 30423
		private static byte[] attributeNamespaceIds;

		// Token: 0x040076D8 RID: 30424
		private static readonly string[] eleTagNames;

		// Token: 0x040076D9 RID: 30425
		private static readonly byte[] eleNamespaceIds;
	}
}
