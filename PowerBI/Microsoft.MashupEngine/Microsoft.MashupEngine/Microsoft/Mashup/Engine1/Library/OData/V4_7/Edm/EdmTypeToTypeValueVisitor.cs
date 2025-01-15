using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.Mashup.Engine1.Library.OData.V4_7.Edm
{
	// Token: 0x0200080F RID: 2063
	internal sealed class EdmTypeToTypeValueVisitor
	{
		// Token: 0x06003BAD RID: 15277 RVA: 0x000C1BDC File Offset: 0x000BFDDC
		public EdmTypeToTypeValueVisitor(IResource resource, Microsoft.OData.Edm.IEdmModel model, ODataUserSettings userSettings, EdmModelProcessorOutput output)
		{
			this.resource = resource;
			this.model = model;
			this.userSettings = userSettings;
			this.output = output;
			this.processingStructuredTypes = new Stack<Microsoft.OData.Edm.IEdmType>();
		}

		// Token: 0x170013DD RID: 5085
		// (get) Token: 0x06003BAE RID: 15278 RVA: 0x000C1C0C File Offset: 0x000BFE0C
		public EdmModelProcessorOutput Output
		{
			get
			{
				return this.output;
			}
		}

		// Token: 0x06003BAF RID: 15279 RVA: 0x000C1C14 File Offset: 0x000BFE14
		public FunctionTypeValue GetFunctionImportType(Microsoft.OData.Edm.IEdmFunctionImport functionImport)
		{
			return this.VisitFunctionType(functionImport.Function).AsFunctionType;
		}

		// Token: 0x06003BB0 RID: 15280 RVA: 0x000C1C28 File Offset: 0x000BFE28
		public TypeValue VisitType(Microsoft.OData.Edm.IEdmType type)
		{
			switch (type.TypeKind)
			{
			case Microsoft.OData.Edm.EdmTypeKind.Primitive:
				return EdmTypeToTypeValueVisitor.VisitPrimitiveType((Microsoft.OData.Edm.IEdmPrimitiveType)type);
			case Microsoft.OData.Edm.EdmTypeKind.Entity:
			case Microsoft.OData.Edm.EdmTypeKind.Complex:
				return this.VisitStructuredType((Microsoft.OData.Edm.IEdmStructuredType)type);
			case Microsoft.OData.Edm.EdmTypeKind.Collection:
				return this.VisitCollectionType((Microsoft.OData.Edm.IEdmCollectionType)type);
			default:
				return TypeValue.Any;
			}
		}

		// Token: 0x06003BB1 RID: 15281 RVA: 0x000C1C84 File Offset: 0x000BFE84
		public TypeValue VisitTypeReference(Microsoft.OData.Edm.IEdmTypeReference typeReference)
		{
			TypeValue typeValue = this.VisitType(typeReference.Definition);
			TypeValue typeValue2 = ((typeReference.IsNullable && !typeReference.IsCollection()) ? typeValue.Nullable : typeValue.NonNullable);
			return BinaryOperator.AddMeta.Invoke(typeValue2, typeValue.MetaValue).AsType;
		}

		// Token: 0x06003BB2 RID: 15282 RVA: 0x000C1CDC File Offset: 0x000BFEDC
		private TypeValue VisitStructuredType(Microsoft.OData.Edm.IEdmStructuredType type)
		{
			TypeValue typeValue;
			if (this.output.EdmTypeValueLookup.TryGetValue(type, out typeValue))
			{
				return typeValue;
			}
			this.processingStructuredTypes.Push(type);
			TypeValue typeValue2;
			try
			{
				typeValue = this.VisitUnprocessedStructuredType(type);
				this.output.EdmTypeValueLookup.Add(type, typeValue);
				typeValue2 = typeValue;
			}
			finally
			{
				this.processingStructuredTypes.Pop();
			}
			return typeValue2;
		}

		// Token: 0x06003BB3 RID: 15283 RVA: 0x000C1D48 File Offset: 0x000BFF48
		private TypeValue VisitUnprocessedStructuredType(Microsoft.OData.Edm.IEdmStructuredType type)
		{
			IEnumerable<NamedValue> enumerable = EdmTypeToTypeValueVisitor.DistinctKeys(type.Properties().Select(new Func<Microsoft.OData.Edm.IEdmProperty, NamedValue>(this.VisitProperty)), (string propertyName) => ODataCommonErrors.DuplicateProperty(null, this.resource, type.FullTypeName(), propertyName));
			IEnumerable<NamedValue> enumerable2 = (from func in this.model.FindBoundOperations(type).OfType<Microsoft.OData.Edm.IEdmFunction>()
				group func by EdmNameEncoder.Decode(func.Name)).Select(new Func<IGrouping<string, Microsoft.OData.Edm.IEdmFunction>, NamedValue>(this.VisitOverloadedBoundFunction));
			List<NamedValue> list = new List<NamedValue>();
			list.AddRange(enumerable);
			list.AddRange(enumerable2);
			string text = null;
			if (this.userSettings.MoreColumns && (type.IsOpen || this.IsBaseClass(type)))
			{
				text = EdmModelProcessorBase<EdmModelProcessorOutput>.GetOtherColumnsColumnName(list.Select((NamedValue kvp) => kvp.Key));
				list.Add(new NamedValue(text, EdmTypeToTypeValueVisitor.MoreColumnsColumnTypeField));
			}
			RecordTypeValue recordTypeValue = ((list.Count > 0) ? RecordTypeValue.New(RecordValue.New(list.ToArray()), false) : TypeValue.Record);
			if (text != null)
			{
				recordTypeValue = TypeServices.ConvertToMoreColumns(recordTypeValue, text);
			}
			List<NamedValue> annotations = AnnotationProcessor.GetAnnotations(this.model, type, this.userSettings);
			return EdmTypeToTypeValueVisitor.AddAnnotations(recordTypeValue, annotations).AsRecordType;
		}

		// Token: 0x06003BB4 RID: 15284 RVA: 0x000C1EC0 File Offset: 0x000C00C0
		private NamedValue VisitProperty(Microsoft.OData.Edm.IEdmProperty property)
		{
			IValueReference valueReference;
			if (this.processingStructuredTypes.Contains(property.Type.Definition.AsElementType()))
			{
				valueReference = new DelayedValue(delegate
				{
					if (this.processingStructuredTypes.Contains(property.Type.Definition.AsElementType()))
					{
						throw new EdmTypeToTypeValueVisitor.ODataUnresolvedCyclicTypeException();
					}
					return this.VisitPropertyType(property);
				});
			}
			else
			{
				valueReference = this.VisitPropertyType(property);
			}
			return new NamedValue(EdmNameEncoder.Decode(property.Name), RecordValue.New(RecordTypeValue.RecordFieldKeys, new IValueReference[]
			{
				valueReference,
				LogicalValue.False
			}));
		}

		// Token: 0x06003BB5 RID: 15285 RVA: 0x000C1F58 File Offset: 0x000C0158
		private TypeValue VisitPropertyType(Microsoft.OData.Edm.IEdmProperty property)
		{
			TypeValue typeValue = this.VisitTypeReference(property.Type);
			if (property.PropertyKind == Microsoft.OData.Edm.EdmPropertyKind.Navigation && !property.Type.IsCollection())
			{
				typeValue = PreviewServices.ConvertToDelayedValue(typeValue, "Entry");
			}
			return typeValue;
		}

		// Token: 0x06003BB6 RID: 15286 RVA: 0x000C1F95 File Offset: 0x000C0195
		private NamedValue VisitOverloadedBoundFunction(IGrouping<string, Microsoft.OData.Edm.IEdmFunction> functionOverloads)
		{
			return new NamedValue(functionOverloads.Key, RecordValue.New(RecordTypeValue.RecordFieldKeys, new IValueReference[]
			{
				this.VisitOverloadedBoundFunctionType(functionOverloads),
				LogicalValue.False
			}));
		}

		// Token: 0x06003BB7 RID: 15287 RVA: 0x000C1FC4 File Offset: 0x000C01C4
		private IValueReference VisitOverloadedBoundFunctionType(IGrouping<string, Microsoft.OData.Edm.IEdmFunction> functionOverloads)
		{
			Microsoft.OData.Edm.IEdmType currentlyProcessingType = this.processingStructuredTypes.Peek();
			Func<FunctionTypeValue, FunctionTypeValue, FunctionTypeValue> <>9__1;
			return new DelayedValue(delegate
			{
				IEnumerable<FunctionTypeValue> enumerable = functionOverloads.Select(new Func<Microsoft.OData.Edm.IEdmFunction, FunctionTypeValue>(this.GetBoundFunctionType));
				Func<FunctionTypeValue, FunctionTypeValue, FunctionTypeValue> func;
				if ((func = <>9__1) == null)
				{
					func = (<>9__1 = delegate(FunctionTypeValue left, FunctionTypeValue right)
					{
						FunctionTypeValue functionTypeValue = TypeAlgebra.Union(left, right, new HashSet<TypeValue> { this.VisitType(currentlyProcessingType) }).AsFunctionType;
						functionTypeValue = BinaryOperator.AddMeta.Invoke(functionTypeValue, left.MetaValue).AsType.AsFunctionType;
						return BinaryOperator.AddMeta.Invoke(functionTypeValue, right.MetaValue).AsType.AsFunctionType;
					});
				}
				return enumerable.Aggregate(func);
			});
		}

		// Token: 0x06003BB8 RID: 15288 RVA: 0x000C1FFC File Offset: 0x000C01FC
		private FunctionTypeValue GetBoundFunctionType(Microsoft.OData.Edm.IEdmFunction function)
		{
			Dictionary<Microsoft.OData.Edm.IEdmFunction, FunctionTypeValue> dictionary;
			if (!this.output.BoundFunctionOverloads.TryGetValue(function.Name, out dictionary))
			{
				dictionary = new Dictionary<Microsoft.OData.Edm.IEdmFunction, FunctionTypeValue>();
				this.output.BoundFunctionOverloads.Add(function.Name, dictionary);
			}
			FunctionTypeValue functionTypeValue;
			if (!dictionary.TryGetValue(function, out functionTypeValue))
			{
				functionTypeValue = EdmTypeToTypeValueVisitor.AsBoundFunctionType(this.VisitFunctionType(function).AsFunctionType);
				functionTypeValue = EdmTypeToTypeValueVisitor.AddAnnotations(functionTypeValue, this.ProcessAndAddAnnotations(function.Name, function).DisplayAnnotations).AsFunctionType;
				dictionary[function] = functionTypeValue;
			}
			return functionTypeValue;
		}

		// Token: 0x06003BB9 RID: 15289 RVA: 0x000C2084 File Offset: 0x000C0284
		private TypeValue VisitFunctionType(Microsoft.OData.Edm.IEdmFunction function)
		{
			TypeValue typeValue = this.VisitTypeReference(function.ReturnType);
			RecordValue recordValue = RecordValue.New(EdmTypeToTypeValueVisitor.EdmTypeKeys, new Value[] { TextValue.New(function.ReturnType.Definition.AsElementType().FullTypeName()) });
			typeValue = BinaryOperator.AddMeta.Invoke(typeValue, recordValue).AsType;
			NamedValue[] array = function.Parameters.Select(new Func<Microsoft.OData.Edm.IEdmOperationParameter, NamedValue>(this.VisitParameter)).ToArray<NamedValue>();
			return FunctionTypeValue.New(typeValue, RecordValue.New(array), array.Length);
		}

		// Token: 0x06003BBA RID: 15290 RVA: 0x000C210C File Offset: 0x000C030C
		private NamedValue VisitParameter(Microsoft.OData.Edm.IEdmOperationParameter parameter)
		{
			TypeValue typeValue = EdmTypeToTypeValueVisitor.ToListTypeIfTableType(this.VisitTypeReference(parameter.Type));
			RecordValue recordValue = RecordValue.New(Keys.New("EdmParameterType"), new Value[] { TextValue.New(EdmTypeToTypeValueVisitor.GetEdmTypeName(parameter.Type)) });
			typeValue = BinaryOperator.AddMeta.Invoke(typeValue, recordValue).AsType;
			typeValue = EdmTypeToTypeValueVisitor.AddAnnotations(typeValue, AnnotationProcessor.GetParameterDisplayAnnotations(this.model, parameter, this.userSettings));
			return new NamedValue(parameter.Name, typeValue);
		}

		// Token: 0x06003BBB RID: 15291 RVA: 0x000C218C File Offset: 0x000C038C
		private TypeValue VisitCollectionType(Microsoft.OData.Edm.IEdmCollectionType type)
		{
			TypeValue typeValue = this.VisitTypeReference(type.ElementType);
			TypeValue typeValue2;
			if (type.ElementType.IsStructured())
			{
				IList<TableKey> list = null;
				if (type.ElementType.IsEntity())
				{
					list = this.GetEntityTypeKey(type.ElementType.AsEntity().EntityDefinition());
				}
				typeValue2 = TableTypeValue.New(typeValue.AsRecordType, list);
			}
			else
			{
				typeValue2 = ListTypeValue.New(typeValue);
			}
			return typeValue2.NewMeta(typeValue.MetaValue).AsType;
		}

		// Token: 0x06003BBC RID: 15292 RVA: 0x000C2200 File Offset: 0x000C0400
		private IList<TableKey> GetEntityTypeKey(Microsoft.OData.Edm.IEdmEntityType entityType)
		{
			IList<TableKey> list;
			if (!this.output.EntityTypeKeys.TryGetValue(entityType, out list))
			{
				RecordTypeValue recordTypeValue = this.VisitType(entityType).AsRecordType;
				int[] array = (from column in entityType.Key()
					select EdmNameEncoder.Decode(column.Name)).Select(delegate(string column)
				{
					int num;
					if (!recordTypeValue.Fields.Keys.TryGetKeyIndex(column, out num))
					{
						throw new InvalidOperationException();
					}
					return num;
				}).ToArray<int>();
				List<TableKey> list2;
				if (array.Length != 0)
				{
					(list2 = new List<TableKey>()).Add(new TableKey(array, true));
				}
				else
				{
					list2 = new List<TableKey>(0);
				}
				list = list2;
				this.output.EntityTypeKeys.Add(entityType, list);
			}
			return list;
		}

		// Token: 0x06003BBD RID: 15293 RVA: 0x000C22B0 File Offset: 0x000C04B0
		private bool IsBaseClass(Microsoft.OData.Edm.IEdmStructuredType type)
		{
			if (this.output.BaseTypes == null)
			{
				this.output.BaseTypes = new HashSet<Microsoft.OData.Edm.IEdmStructuredType>();
				foreach (Microsoft.OData.Edm.IEdmSchemaElement edmSchemaElement in this.model.SchemaElements)
				{
					Microsoft.OData.Edm.IEdmStructuredType edmStructuredType = edmSchemaElement as Microsoft.OData.Edm.IEdmStructuredType;
					if (edmStructuredType != null && edmStructuredType.BaseType != null)
					{
						this.output.BaseTypes.Add(edmStructuredType.BaseType);
					}
				}
			}
			return this.output.BaseTypes.Contains(type);
		}

		// Token: 0x06003BBE RID: 15294 RVA: 0x000C2350 File Offset: 0x000C0550
		private Capabilities ProcessAndAddAnnotations(string key, Microsoft.OData.Edm.Vocabularies.IEdmVocabularyAnnotatable annotatable)
		{
			Capabilities capabilities = AnnotationProcessor.ProcessCapabilities(key, this.model, annotatable, this.output.Annotations, this.userSettings);
			this.output.Annotations.TryAddCapability(annotatable, capabilities);
			return capabilities;
		}

		// Token: 0x06003BBF RID: 15295 RVA: 0x000C238F File Offset: 0x000C058F
		private static TypeValue VisitPrimitiveType(Microsoft.OData.Edm.IEdmPrimitiveType type)
		{
			return ODataTypeServices.GetTypeValueFromEdm(type.PrimitiveKind);
		}

		// Token: 0x06003BC0 RID: 15296 RVA: 0x000C239C File Offset: 0x000C059C
		private static FunctionTypeValue AsBoundFunctionType(FunctionTypeValue unboundType)
		{
			RecordBuilder recordBuilder = new RecordBuilder(unboundType.Parameters.Keys.Length - 1);
			for (int i = 1; i < unboundType.Parameters.Keys.Length; i++)
			{
				recordBuilder.Add(unboundType.Parameters.Keys[i], unboundType.Parameters[i], TypeValue.Any);
			}
			return FunctionTypeValue.New(unboundType.ReturnType, recordBuilder.ToRecord(), unboundType.Min - 1).NewMeta(unboundType.MetaValue).AsType.AsFunctionType;
		}

		// Token: 0x06003BC1 RID: 15297 RVA: 0x000C2435 File Offset: 0x000C0635
		private static IEnumerable<NamedValue> DistinctKeys(IEnumerable<NamedValue> enumerable, Func<string, Exception> createDuplicateKeyException)
		{
			Dictionary<string, Value> dictionary = new Dictionary<string, Value>();
			foreach (NamedValue namedValue in enumerable)
			{
				Value value;
				if (dictionary.TryGetValue(namedValue.Key, out value))
				{
					bool flag;
					try
					{
						flag = namedValue.Value.Equals(value);
					}
					catch (EdmTypeToTypeValueVisitor.ODataUnresolvedCyclicTypeException)
					{
						flag = false;
					}
					if (!flag)
					{
						throw createDuplicateKeyException(namedValue.Key);
					}
				}
				else
				{
					dictionary.Add(namedValue.Key, namedValue.Value);
					yield return namedValue;
				}
			}
			IEnumerator<NamedValue> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06003BC2 RID: 15298 RVA: 0x000C244C File Offset: 0x000C064C
		private static TypeValue AddAnnotations(TypeValue type, IEnumerable<NamedValue> annotations)
		{
			if (annotations.Any<NamedValue>())
			{
				return BinaryOperator.AddMeta.Invoke(type, RecordValue.New(annotations.ToArray<NamedValue>())).AsType;
			}
			return type;
		}

		// Token: 0x06003BC3 RID: 15299 RVA: 0x000C2473 File Offset: 0x000C0673
		private static string GetEdmTypeName(Microsoft.OData.Edm.IEdmTypeReference typeReference)
		{
			if (typeReference.IsCollection())
			{
				return string.Format(CultureInfo.InvariantCulture, "Collection({0})", typeReference.AsCollection().ElementType().FullName());
			}
			return typeReference.FullName();
		}

		// Token: 0x06003BC4 RID: 15300 RVA: 0x000C24A4 File Offset: 0x000C06A4
		private static TypeValue ToListTypeIfTableType(TypeValue typeValue)
		{
			if (typeValue.IsTableType)
			{
				TypeValue typeValue2 = ListTypeValue.New(typeValue.AsTableType.ItemType);
				typeValue2 = (typeValue.IsNullable ? typeValue2.Nullable : typeValue2.NonNullable);
				return typeValue2.NewMeta(typeValue.MetaValue).AsType;
			}
			return typeValue;
		}

		// Token: 0x04001F16 RID: 7958
		public const string EdmType = "EdmType";

		// Token: 0x04001F17 RID: 7959
		public const string ParameterEdmTypeMetadata = "EdmParameterType";

		// Token: 0x04001F18 RID: 7960
		private static readonly Keys EdmTypeKeys = Keys.New("EdmType");

		// Token: 0x04001F19 RID: 7961
		private static readonly RecordValue MoreColumnsColumnTypeField = RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
		{
			RecordTypeValue.Any,
			LogicalValue.False
		});

		// Token: 0x04001F1A RID: 7962
		private readonly Microsoft.OData.Edm.IEdmModel model;

		// Token: 0x04001F1B RID: 7963
		private readonly IResource resource;

		// Token: 0x04001F1C RID: 7964
		private readonly ODataUserSettings userSettings;

		// Token: 0x04001F1D RID: 7965
		private readonly EdmModelProcessorOutput output;

		// Token: 0x04001F1E RID: 7966
		private readonly Stack<Microsoft.OData.Edm.IEdmType> processingStructuredTypes;

		// Token: 0x02000810 RID: 2064
		[Serializable]
		private class ODataUnresolvedCyclicTypeException : Exception
		{
			// Token: 0x06003BC6 RID: 15302 RVA: 0x00005F33 File Offset: 0x00004133
			public ODataUnresolvedCyclicTypeException()
			{
			}

			// Token: 0x06003BC7 RID: 15303 RVA: 0x00005F45 File Offset: 0x00004145
			protected ODataUnresolvedCyclicTypeException(SerializationInfo info, StreamingContext context)
				: base(info, context)
			{
			}
		}
	}
}
