using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OData.Core;
using Microsoft.OData.Edm;

namespace Microsoft.Mashup.Engine1.Library.OData.V4.Write
{
	// Token: 0x020008A5 RID: 2213
	internal class ValueODataValueVisitor
	{
		// Token: 0x06003F55 RID: 16213 RVA: 0x000D0834 File Offset: 0x000CEA34
		private object VisitValue(Value value, Microsoft.OData.Edm.IEdmType type)
		{
			switch (value.Kind)
			{
			case ValueKind.List:
				return this.VisitListWithScope(value.AsList, type);
			case ValueKind.Record:
				return this.VisitRecordWithScope(value.AsRecord, type);
			case ValueKind.Table:
			case ValueKind.Function:
			case ValueKind.Type:
			case ValueKind.Action:
				throw new NotSupportedException();
			default:
				return this.VisitPrimitiveValue(value, type);
			}
		}

		// Token: 0x06003F56 RID: 16214 RVA: 0x000D0895 File Offset: 0x000CEA95
		public object ToSource(Value value, Microsoft.OData.Edm.IEdmType type)
		{
			return new ValueODataValueVisitor().VisitValue(value, type);
		}

		// Token: 0x06003F57 RID: 16215 RVA: 0x000D08A4 File Offset: 0x000CEAA4
		private object VisitRecordWithScope(RecordValue record, Microsoft.OData.Edm.IEdmType type)
		{
			object obj;
			using (ValueODataValueVisitor.Scope scope = new ValueODataValueVisitor.Scope(this, record))
			{
				if (!scope.Visited)
				{
					obj = this.VisitRecord(record, type);
				}
				else
				{
					obj = scope.Value;
				}
			}
			return obj;
		}

		// Token: 0x06003F58 RID: 16216 RVA: 0x000D08F0 File Offset: 0x000CEAF0
		private object VisitListWithScope(ListValue list, Microsoft.OData.Edm.IEdmType type)
		{
			object obj;
			using (ValueODataValueVisitor.Scope scope = new ValueODataValueVisitor.Scope(this, list))
			{
				if (!scope.Visited)
				{
					obj = this.VisitList(list, type);
				}
				else
				{
					obj = scope.Value;
				}
			}
			return obj;
		}

		// Token: 0x06003F59 RID: 16217 RVA: 0x000D093C File Offset: 0x000CEB3C
		public void VisitCycle(int depth, Value value)
		{
			throw ValueException.NewExpressionError<Message0>(Strings.ODataCannotWriteData, TextValue.New("Cycle encountered."), null);
		}

		// Token: 0x06003F5A RID: 16218 RVA: 0x000D0954 File Offset: 0x000CEB54
		private object VisitRecord(RecordValue record, Microsoft.OData.Edm.IEdmType type)
		{
			if (type != null && type.TypeKind == Microsoft.OData.Edm.EdmTypeKind.Complex)
			{
				Microsoft.OData.Edm.IEdmComplexType edmComplexType = (Microsoft.OData.Edm.IEdmComplexType)type;
				ODataComplexValue odataComplexValue = new ODataComplexValue();
				List<ODataProperty> list = new List<ODataProperty>(record.Keys.Length);
				foreach (NamedValue namedValue in record.GetFields())
				{
					Microsoft.OData.Edm.IEdmProperty edmProperty = edmComplexType.FindProperty(namedValue.Key);
					object obj = this.VisitValue(namedValue.Value, edmProperty.Type.Definition);
					list.Add(new ODataProperty
					{
						Name = namedValue.Key,
						Value = obj
					});
				}
				odataComplexValue.TypeName = type.FullTypeName();
				odataComplexValue.Properties = list;
				return odataComplexValue;
			}
			throw ValueException.NewExpressionError<Message0>(Strings.ODataCannotWriteData, TextValue.New(type.FullTypeName()), null);
		}

		// Token: 0x06003F5B RID: 16219 RVA: 0x000D0A4C File Offset: 0x000CEC4C
		private object VisitPrimitiveValue(Value value, Microsoft.OData.Edm.IEdmType type)
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

		// Token: 0x06003F5C RID: 16220 RVA: 0x000D0AA4 File Offset: 0x000CECA4
		private object VisitList(ListValue list, Microsoft.OData.Edm.IEdmType type)
		{
			if (type != null && type.TypeKind == Microsoft.OData.Edm.EdmTypeKind.Collection)
			{
				Microsoft.OData.Edm.IEdmType definition = ((Microsoft.OData.Edm.IEdmCollectionType)type).ElementType.Definition;
				ODataCollectionValue odataCollectionValue = new ODataCollectionValue();
				List<object> list2 = new List<object>();
				foreach (IValueReference valueReference in list)
				{
					object obj = this.VisitValue(valueReference.Value, definition);
					list2.Add(obj);
				}
				odataCollectionValue.Items = list2;
				odataCollectionValue.TypeName = type.FullTypeName();
				return odataCollectionValue;
			}
			throw ValueException.NewExpressionError<Message0>(Strings.ODataCannotWriteData, TextValue.New(type.FullTypeName()), null);
		}

		// Token: 0x04002145 RID: 8517
		private ValueODataValueVisitor.Scope currentScope;

		// Token: 0x020008A6 RID: 2214
		private class Scope : IDisposable
		{
			// Token: 0x06003F5E RID: 16222 RVA: 0x000D0B54 File Offset: 0x000CED54
			public Scope(ValueODataValueVisitor visitor, Value value)
			{
				this.visitor = visitor;
				this.value = value;
				this.previousScope = visitor.currentScope;
				this.depth = ((this.previousScope == null) ? 0 : (this.previousScope.depth + 1));
				visitor.currentScope = this;
				for (ValueODataValueVisitor.Scope scope = this.previousScope; scope != null; scope = scope.previousScope)
				{
					if (scope.value == value)
					{
						this.visited = true;
						visitor.VisitCycle(scope.depth, value);
						return;
					}
				}
			}

			// Token: 0x06003F5F RID: 16223 RVA: 0x000D0BD7 File Offset: 0x000CEDD7
			void IDisposable.Dispose()
			{
				this.visitor.currentScope = this.previousScope;
				this.value = null;
			}

			// Token: 0x1700149F RID: 5279
			// (get) Token: 0x06003F60 RID: 16224 RVA: 0x000D0BF1 File Offset: 0x000CEDF1
			public bool Visited
			{
				get
				{
					return this.visited;
				}
			}

			// Token: 0x170014A0 RID: 5280
			// (get) Token: 0x06003F61 RID: 16225 RVA: 0x000D0BF9 File Offset: 0x000CEDF9
			public Value Value
			{
				get
				{
					return this.value;
				}
			}

			// Token: 0x170014A1 RID: 5281
			// (get) Token: 0x06003F62 RID: 16226 RVA: 0x000D0C01 File Offset: 0x000CEE01
			public int Depth
			{
				get
				{
					return this.depth;
				}
			}

			// Token: 0x04002146 RID: 8518
			private ValueODataValueVisitor visitor;

			// Token: 0x04002147 RID: 8519
			private ValueODataValueVisitor.Scope previousScope;

			// Token: 0x04002148 RID: 8520
			private Value value;

			// Token: 0x04002149 RID: 8521
			private int depth;

			// Token: 0x0400214A RID: 8522
			private bool visited;
		}
	}
}
