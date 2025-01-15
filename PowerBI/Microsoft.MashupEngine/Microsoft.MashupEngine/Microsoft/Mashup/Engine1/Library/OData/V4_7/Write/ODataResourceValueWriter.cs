using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OData;
using Microsoft.OData.Edm;

namespace Microsoft.Mashup.Engine1.Library.OData.V4_7.Write
{
	// Token: 0x02000782 RID: 1922
	internal class ODataResourceValueWriter
	{
		// Token: 0x06003880 RID: 14464 RVA: 0x000B61C9 File Offset: 0x000B43C9
		public ODataResourceValueWriter(ODataWriter writer)
		{
			this.writer = writer;
		}

		// Token: 0x06003881 RID: 14465 RVA: 0x000B61D8 File Offset: 0x000B43D8
		public static object MarshalSimpleValue(Value value, Microsoft.OData.Edm.IEdmType type)
		{
			ValueKind kind = value.Kind;
			if (kind == ValueKind.Null)
			{
				return null;
			}
			if (kind == ValueKind.List)
			{
				return ODataResourceValueWriter.MarshalSimpleList(value.AsList, type);
			}
			if (kind - ValueKind.Record <= 4)
			{
				throw new NotSupportedException();
			}
			return ODataResourceValueWriter.MarshalPrimitiveValue(value, type);
		}

		// Token: 0x06003882 RID: 14466 RVA: 0x000B6217 File Offset: 0x000B4417
		public void WriteRecord(RecordValue record, Microsoft.OData.Edm.IEdmType type)
		{
			this.Write<RecordValue>(record, type, new Action<RecordValue, Microsoft.OData.Edm.IEdmType>(this.WriteRecordCore));
		}

		// Token: 0x06003883 RID: 14467 RVA: 0x000B622D File Offset: 0x000B442D
		public void WriteList(ListValue list, Microsoft.OData.Edm.IEdmType type)
		{
			this.Write<ListValue>(list, type, new Action<ListValue, Microsoft.OData.Edm.IEdmType>(this.WriteListCore));
		}

		// Token: 0x06003884 RID: 14468 RVA: 0x000B6243 File Offset: 0x000B4443
		public void WriteTable(TableValue table, Microsoft.OData.Edm.IEdmType type)
		{
			this.Write<TableValue>(table, type, new Action<TableValue, Microsoft.OData.Edm.IEdmType>(this.WriteTableCore));
		}

		// Token: 0x06003885 RID: 14469 RVA: 0x000B625C File Offset: 0x000B445C
		private void Write<T>(T value, Microsoft.OData.Edm.IEdmType type, Action<T, Microsoft.OData.Edm.IEdmType> writeAction) where T : Value
		{
			using (ODataResourceValueWriter.Scope scope = new ODataResourceValueWriter.Scope(this, value))
			{
				if (scope.Visited)
				{
					throw ValueException.NewExpressionError<Message0>(Strings.ODataCannotWriteData, null, null);
				}
				writeAction(value, type);
			}
		}

		// Token: 0x06003886 RID: 14470 RVA: 0x000B62B0 File Offset: 0x000B44B0
		private void WriteRecordCore(RecordValue record, Microsoft.OData.Edm.IEdmType type)
		{
			Microsoft.OData.Edm.IEdmStructuredType structuredType = type as Microsoft.OData.Edm.IEdmStructuredType;
			if (structuredType == null)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.ODataCannotWriteData, TextValue.New(type.FullTypeName()), null);
			}
			List<ODataProperty> list = new List<ODataProperty>();
			List<string> nestedResourceProperties = new List<string>();
			foreach (NamedValue namedValue in record.GetFields())
			{
				Microsoft.OData.Edm.IEdmProperty edmProperty = structuredType.FindProperty(EdmNameEncoder.Encode(namedValue.Key));
				if (edmProperty == null)
				{
					throw ValueException.NewExpressionError<Message0>(Strings.ODataCannotWriteData, TextValue.New(namedValue.Key), null);
				}
				if (this.IsSimple(edmProperty.Type.Definition))
				{
					object obj = ODataResourceValueWriter.MarshalSimpleValue(namedValue.Value, edmProperty.Type.Definition);
					list.Add(new ODataProperty
					{
						Name = namedValue.Key,
						Value = obj
					});
				}
				else
				{
					nestedResourceProperties.Add(namedValue.Key);
				}
			}
			ODataResource odataResource = new ODataResource();
			Value value;
			if (record.MetaValue.TryGetValue("@odata.type", out value) && !value.IsNull)
			{
				odataResource.TypeName = value.AsString;
			}
			else
			{
				odataResource.TypeName = type.FullTypeName();
			}
			odataResource.Properties = list;
			this.writer.Write(odataResource, delegate
			{
				using (List<string>.Enumerator enumerator2 = nestedResourceProperties.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						string key = enumerator2.Current;
						Microsoft.OData.Edm.IEdmProperty property = structuredType.FindProperty(EdmNameEncoder.Encode(key));
						ODataNestedResourceInfo odataNestedResourceInfo = new ODataNestedResourceInfo();
						odataNestedResourceInfo.Name = property.Name;
						odataNestedResourceInfo.IsCollection = new bool?(record[key].IsList || record[key].IsTable);
						this.writer.Write(odataNestedResourceInfo, delegate
						{
							this.Write(record[key], property.Type.Definition);
						});
					}
				}
			});
		}

		// Token: 0x06003887 RID: 14471 RVA: 0x000B6458 File Offset: 0x000B4658
		private void WriteTableCore(TableValue table, Microsoft.OData.Edm.IEdmType type)
		{
			Microsoft.OData.Edm.IEdmCollectionType collectionType = type as Microsoft.OData.Edm.IEdmCollectionType;
			if (collectionType == null || !collectionType.ElementType.IsStructured())
			{
				throw ValueException.NewExpressionError<Message0>(Strings.ODataCannotWriteData, TextValue.New(type.FullTypeName()), null);
			}
			ODataResourceSet odataResourceSet = new ODataResourceSet();
			odataResourceSet.TypeName = type.FullTypeName();
			this.writer.Write(odataResourceSet, delegate
			{
				foreach (IValueReference valueReference in table)
				{
					this.WriteRecord(valueReference.Value.AsRecord, collectionType.ElementType.AsStructured().StructuredDefinition());
				}
			});
		}

		// Token: 0x06003888 RID: 14472 RVA: 0x000B64E4 File Offset: 0x000B46E4
		private void WriteListCore(ListValue list, Microsoft.OData.Edm.IEdmType type)
		{
			Microsoft.OData.Edm.IEdmCollectionType collectionType = type as Microsoft.OData.Edm.IEdmCollectionType;
			if (collectionType == null || collectionType.ElementType.IsStructured())
			{
				throw ValueException.NewExpressionError<Message0>(Strings.ODataCannotWriteData, TextValue.New(type.FullTypeName()), null);
			}
			ODataResourceSet odataResourceSet = new ODataResourceSet();
			odataResourceSet.TypeName = type.FullTypeName();
			this.writer.Write(odataResourceSet, delegate
			{
				foreach (IValueReference valueReference in list)
				{
					this.Write(valueReference.Value, collectionType.ElementType.Definition);
				}
			});
		}

		// Token: 0x06003889 RID: 14473 RVA: 0x000B6570 File Offset: 0x000B4770
		private void WritePrimitive(Value value, Microsoft.OData.Edm.IEdmType type)
		{
			Microsoft.OData.Edm.IEdmPrimitiveType edmPrimitiveType = type as Microsoft.OData.Edm.IEdmPrimitiveType;
			if (edmPrimitiveType == null)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.ODataCannotWriteData, TextValue.New(type.FullTypeName()), null);
			}
			this.writer.Write(new ODataPrimitiveValue(ODataTypeServices.MarshalToClr(value, edmPrimitiveType)));
		}

		// Token: 0x0600388A RID: 14474 RVA: 0x000B65B6 File Offset: 0x000B47B6
		private void WriteNull()
		{
			this.writer.Write(null);
		}

		// Token: 0x0600388B RID: 14475 RVA: 0x000B65C8 File Offset: 0x000B47C8
		private void Write(Value value, Microsoft.OData.Edm.IEdmType type)
		{
			ValueKind kind = value.Kind;
			if (kind == ValueKind.Null)
			{
				this.WriteNull();
				return;
			}
			switch (kind)
			{
			case ValueKind.List:
				this.WriteList(value.AsList, type);
				return;
			case ValueKind.Record:
				this.WriteRecord(value.AsRecord, type);
				return;
			case ValueKind.Table:
				this.WriteTable(value.AsTable, type);
				return;
			case ValueKind.Function:
			case ValueKind.Type:
			case ValueKind.Action:
				throw new NotSupportedException();
			default:
				this.WritePrimitive(value, type);
				return;
			}
		}

		// Token: 0x0600388C RID: 14476 RVA: 0x000B6644 File Offset: 0x000B4844
		private bool IsSimple(Microsoft.OData.Edm.IEdmType type)
		{
			Microsoft.OData.Edm.EdmTypeKind typeKind = type.AsElementType().TypeKind;
			return typeKind == Microsoft.OData.Edm.EdmTypeKind.Primitive || typeKind == Microsoft.OData.Edm.EdmTypeKind.Enum;
		}

		// Token: 0x0600388D RID: 14477 RVA: 0x000B6668 File Offset: 0x000B4868
		private static object MarshalSimpleList(ListValue list, Microsoft.OData.Edm.IEdmType type)
		{
			if (type != null && type.TypeKind == Microsoft.OData.Edm.EdmTypeKind.Collection)
			{
				Microsoft.OData.Edm.IEdmType definition = ((Microsoft.OData.Edm.IEdmCollectionType)type).ElementType.Definition;
				ODataCollectionValue odataCollectionValue = new ODataCollectionValue();
				List<object> list2 = new List<object>();
				foreach (IValueReference valueReference in list)
				{
					object obj = ODataResourceValueWriter.MarshalSimpleValue(valueReference.Value, definition);
					list2.Add(obj);
				}
				odataCollectionValue.Items = list2;
				odataCollectionValue.TypeName = type.FullTypeName();
				return odataCollectionValue;
			}
			throw ValueException.NewExpressionError<Message0>(Strings.ODataCannotWriteData, TextValue.New(type.FullTypeName()), null);
		}

		// Token: 0x0600388E RID: 14478 RVA: 0x000B6714 File Offset: 0x000B4914
		private static object MarshalPrimitiveValue(Value value, Microsoft.OData.Edm.IEdmType type)
		{
			if (type != null && type.TypeKind == Microsoft.OData.Edm.EdmTypeKind.Primitive)
			{
				Microsoft.OData.Edm.IEdmPrimitiveType edmPrimitiveType = type as Microsoft.OData.Edm.IEdmPrimitiveType;
				return ODataTypeServices.MarshalToClr(value, edmPrimitiveType);
			}
			if (type != null && type.TypeKind == Microsoft.OData.Edm.EdmTypeKind.Enum)
			{
				return new ODataEnumValue(value.AsString);
			}
			throw ValueException.NewExpressionError<Message0>(Strings.ODataCannotWriteData, TextValue.New(type.FullTypeName()), null);
		}

		// Token: 0x04001D33 RID: 7475
		private readonly ODataWriter writer;

		// Token: 0x04001D34 RID: 7476
		private ODataResourceValueWriter.Scope currentScope;

		// Token: 0x02000783 RID: 1923
		private class Scope : IDisposable
		{
			// Token: 0x0600388F RID: 14479 RVA: 0x000B676C File Offset: 0x000B496C
			public Scope(ODataResourceValueWriter writer, Value value)
			{
				this.writer = writer;
				this.value = value;
				this.previousScope = writer.currentScope;
				writer.currentScope = this;
				for (ODataResourceValueWriter.Scope scope = this.previousScope; scope != null; scope = scope.previousScope)
				{
					if (scope.value == value)
					{
						this.visited = true;
						return;
					}
				}
			}

			// Token: 0x06003890 RID: 14480 RVA: 0x000B67C4 File Offset: 0x000B49C4
			void IDisposable.Dispose()
			{
				this.writer.currentScope = this.previousScope;
				this.value = null;
			}

			// Token: 0x17001337 RID: 4919
			// (get) Token: 0x06003891 RID: 14481 RVA: 0x000B67DE File Offset: 0x000B49DE
			public bool Visited
			{
				get
				{
					return this.visited;
				}
			}

			// Token: 0x04001D35 RID: 7477
			private readonly ODataResourceValueWriter writer;

			// Token: 0x04001D36 RID: 7478
			private readonly ODataResourceValueWriter.Scope previousScope;

			// Token: 0x04001D37 RID: 7479
			private Value value;

			// Token: 0x04001D38 RID: 7480
			private bool visited;
		}
	}
}
