using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Runtime;
using ParquetSharp;
using ParquetSharp.Schema;

namespace Microsoft.Mashup.Engine1.Library.Parquet.Schema
{
	// Token: 0x02001FE3 RID: 8163
	internal sealed class PrimitiveSchemaElement : SchemaElement
	{
		// Token: 0x060110B2 RID: 69810 RVA: 0x003ACCB0 File Offset: 0x003AAEB0
		public PrimitiveSchemaElement(string name, Repetition repetition, ParquetPrimitiveTypeMap typeMap, ColumnOrder columnOrder, RepeatedTypeKind repeatedTypeKind = RepeatedTypeKind.Default)
			: base(name, repetition, typeMap.LogicalTypeType, new Func<LogicalType>(typeMap.CreateLogicalType), repeatedTypeKind)
		{
			this.typeMap = typeMap;
			this.columnOrder = columnOrder;
			this.itemTypeValue = typeMap.TypeValue.NewFacets(typeMap.Facets);
			this.primitiveElements = new PrimitiveSchemaElement[] { this };
			this.toValue = typeMap.GetToValue(this.itemTypeValue);
			this.fromValue = typeMap.GetFromValue(this.itemTypeValue);
		}

		// Token: 0x17002CF1 RID: 11505
		// (get) Token: 0x060110B3 RID: 69811 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public override NodeType ElementType
		{
			get
			{
				return NodeType.Primitive;
			}
		}

		// Token: 0x17002CF2 RID: 11506
		// (get) Token: 0x060110B4 RID: 69812 RVA: 0x003ACD33 File Offset: 0x003AAF33
		public ColumnOrder ColumnOrder
		{
			get
			{
				return this.columnOrder;
			}
		}

		// Token: 0x17002CF3 RID: 11507
		// (get) Token: 0x060110B5 RID: 69813 RVA: 0x003ACD3B File Offset: 0x003AAF3B
		public PhysicalType PhysicalType
		{
			get
			{
				return this.typeMap.PhysicalType;
			}
		}

		// Token: 0x17002CF4 RID: 11508
		// (get) Token: 0x060110B6 RID: 69814 RVA: 0x003ACD48 File Offset: 0x003AAF48
		public int TypeLength
		{
			get
			{
				return this.typeMap.TypeLength.GetValueOrDefault(-1);
			}
		}

		// Token: 0x17002CF5 RID: 11509
		// (get) Token: 0x060110B7 RID: 69815 RVA: 0x003ACD69 File Offset: 0x003AAF69
		public override IList<PrimitiveSchemaElement> PrimitiveElements
		{
			get
			{
				return this.primitiveElements;
			}
		}

		// Token: 0x17002CF6 RID: 11510
		// (get) Token: 0x060110B8 RID: 69816 RVA: 0x003ACD71 File Offset: 0x003AAF71
		public override TypeValue ItemTypeValue
		{
			get
			{
				return this.itemTypeValue;
			}
		}

		// Token: 0x17002CF7 RID: 11511
		// (get) Token: 0x060110B9 RID: 69817 RVA: 0x003ACD79 File Offset: 0x003AAF79
		public ParquetPrimitiveTypeMap TypeMap
		{
			get
			{
				return this.typeMap;
			}
		}

		// Token: 0x060110BA RID: 69818 RVA: 0x003ACD84 File Offset: 0x003AAF84
		public override Node CreateNode()
		{
			Node node;
			using (LogicalType logicalType = base.CreateLogicalType())
			{
				node = new PrimitiveNode(base.Name, base.Repetition, logicalType, this.PhysicalType, this.TypeLength);
			}
			return node;
		}

		// Token: 0x060110BB RID: 69819 RVA: 0x003ACDD4 File Offset: 0x003AAFD4
		public override Value ToValue(object raw)
		{
			return this.toValue(raw);
		}

		// Token: 0x060110BC RID: 69820 RVA: 0x003ACDE2 File Offset: 0x003AAFE2
		public override object FromValue(IAllocator allocator, Value value)
		{
			return this.fromValue(allocator, value);
		}

		// Token: 0x060110BD RID: 69821 RVA: 0x003ACDF1 File Offset: 0x003AAFF1
		public override bool TrySelectColumns(NestedColumnSelection columnSelection, out SchemaElement schemaElement)
		{
			if (!columnSelection.IsAll)
			{
				schemaElement = null;
				return false;
			}
			schemaElement = new PrimitiveSchemaElement(base.Name, base.Repetition, this.typeMap, this.ColumnOrder, this.repeatedTypeKind);
			return true;
		}

		// Token: 0x0400670D RID: 26381
		private readonly ParquetPrimitiveTypeMap typeMap;

		// Token: 0x0400670E RID: 26382
		private readonly ColumnOrder columnOrder;

		// Token: 0x0400670F RID: 26383
		private readonly TypeValue itemTypeValue;

		// Token: 0x04006710 RID: 26384
		private readonly PrimitiveSchemaElement[] primitiveElements;

		// Token: 0x04006711 RID: 26385
		private readonly Func<object, Value> toValue;

		// Token: 0x04006712 RID: 26386
		private readonly Func<IAllocator, Value, object> fromValue;
	}
}
