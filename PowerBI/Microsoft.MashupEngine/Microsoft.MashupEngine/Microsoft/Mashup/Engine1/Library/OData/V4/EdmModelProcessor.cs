using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Internal;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Resources;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;
using Microsoft.OData.Core;
using Microsoft.OData.Core.Atom;
using Microsoft.OData.Edm;

namespace Microsoft.Mashup.Engine1.Library.OData.V4
{
	// Token: 0x02000843 RID: 2115
	internal sealed class EdmModelProcessor : EdmModelProcessorBase<EdmModelProcessorOutput>
	{
		// Token: 0x06003D07 RID: 15623 RVA: 0x000C5F93 File Offset: 0x000C4193
		private EdmModelProcessor(IEngineHost engineHost, ServiceDocumentWrapper serviceDocument, Microsoft.OData.Edm.IEdmModel model, ODataUserSettings userSettings, IResource resource)
			: base(engineHost)
		{
			this.serviceDocument = serviceDocument;
			this.model = model;
			this.userSettings = userSettings;
			this.resource = resource;
		}

		// Token: 0x06003D08 RID: 15624 RVA: 0x000C5FBA File Offset: 0x000C41BA
		public static EdmModelProcessorOutput Build(IEngineHost engineHost, ServiceDocumentWrapper serviceDocument, Microsoft.OData.Edm.IEdmModel model, ODataUserSettings userSettings, HttpResource resource)
		{
			return new EdmModelProcessor(engineHost, serviceDocument, model, userSettings, resource.Resource).Build(resource.NewUrl(serviceDocument.ServiceUri.AbsoluteUri));
		}

		// Token: 0x06003D09 RID: 15625 RVA: 0x000C5FE4 File Offset: 0x000C41E4
		private TypeValue CreateRecordTypeValue(Stack<Microsoft.OData.Edm.IEdmType> processingTypes, Microsoft.OData.Edm.IEdmStructuredType type)
		{
			processingTypes.Push(type);
			TypeValue typeValue2;
			try
			{
				List<string> list = new List<string>();
				if (type.TypeKind == Microsoft.OData.Edm.EdmTypeKind.Entity)
				{
					foreach (Microsoft.OData.Edm.IEdmStructuralProperty edmStructuralProperty in ((Microsoft.OData.Edm.IEdmEntityType)type).Key())
					{
						string text = EdmNameEncoder.Decode(edmStructuralProperty.Name);
						list.Add(text);
					}
				}
				List<NamedValue> list2 = new List<NamedValue>();
				Dictionary<string, int> dictionary = new Dictionary<string, int>();
				List<NamedValue> annotations = AnnotationProcessor.GetAnnotations(this.model, type, this.userSettings);
				foreach (Microsoft.OData.Edm.IEdmProperty edmProperty in type.Properties())
				{
					object orCreatePropertyTypeValue = this.GetOrCreatePropertyTypeValue(processingTypes, edmProperty);
					TypeValue typeValue = orCreatePropertyTypeValue as TypeValue;
					Func<int, Value> func = orCreatePropertyTypeValue as Func<int, Value>;
					string text2 = EdmNameEncoder.Decode(edmProperty.Name);
					RecordValue recordValue = ((typeValue != null) ? RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
					{
						typeValue,
						LogicalValue.False
					}) : RecordValue.New(RecordTypeValue.RecordFieldKeys, func));
					if (typeValue != null)
					{
						recordValue = BinaryOperator.AddMeta.Invoke(recordValue, typeValue.MetaValue).AsRecord;
					}
					try
					{
						dictionary.Add(text2, dictionary.Count);
						list2.Add(new NamedValue(text2, recordValue));
					}
					catch (ArgumentException)
					{
						if (!list2[dictionary[text2]].Value.AsRecord.Equals(recordValue))
						{
							throw ODataCommonErrors.DuplicateProperty(this.engineHost, this.resource, type.FullTypeName(), text2);
						}
					}
				}
				IEnumerable<Microsoft.OData.Edm.IEdmFunction> enumerable = this.model.FindBoundOperations(type).OfType<Microsoft.OData.Edm.IEdmFunction>();
				Dictionary<string, Dictionary<Microsoft.OData.Edm.IEdmFunction, Capabilities>> dictionary2 = new Dictionary<string, Dictionary<Microsoft.OData.Edm.IEdmFunction, Capabilities>>(enumerable.Count<Microsoft.OData.Edm.IEdmFunction>());
				foreach (Microsoft.OData.Edm.IEdmFunction edmFunction in enumerable)
				{
					string text3 = EdmNameEncoder.Decode(edmFunction.Name);
					Dictionary<Microsoft.OData.Edm.IEdmFunction, Capabilities> dictionary3;
					if (!dictionary2.TryGetValue(text3, out dictionary3))
					{
						dictionary2[text3] = new Dictionary<Microsoft.OData.Edm.IEdmFunction, Capabilities>();
					}
					dictionary2[text3][edmFunction] = this.ProcessAndAddCapability(edmFunction.Name, edmFunction);
				}
				foreach (KeyValuePair<string, Dictionary<Microsoft.OData.Edm.IEdmFunction, Capabilities>> keyValuePair in dictionary2)
				{
					Func<int, Value> functionDelayedTypeValue = this.GetFunctionDelayedTypeValue(keyValuePair.Value);
					NamedValue namedValue = new NamedValue(keyValuePair.Key, RecordValue.New(RecordTypeValue.RecordFieldKeys, functionDelayedTypeValue));
					list2.Add(namedValue);
				}
				string text4;
				bool flag = this.TryAddMoreColumns(list2, type, this.model, out text4);
				RecordTypeValue recordTypeValue = ((list2.Count > 0) ? RecordTypeValue.New(RecordValue.New(list2.ToArray()), false) : TypeValue.Record);
				if (flag)
				{
					recordTypeValue = TypeServices.ConvertToMoreColumns(recordTypeValue, text4);
				}
				if (list.Count > 0)
				{
					int[] array = new int[list.Count];
					for (int i = 0; i < list.Count; i++)
					{
						int num;
						if (!recordTypeValue.AsRecordType.Fields.Keys.TryGetKeyIndex(list[i], out num))
						{
							throw new InvalidOperationException();
						}
						array[i] = num;
					}
					this.tableKeys.Add(recordTypeValue, new TableKey(array, true));
				}
				if (!this.tableAnnotations.ContainsKey(recordTypeValue) && annotations.Count > 0)
				{
					this.tableAnnotations.Add(recordTypeValue, annotations);
				}
				recordTypeValue = (RecordTypeValue)this.AddDisplayAnnotationsMetadata(RecordValue.New(annotations.ToArray()), recordTypeValue);
				this.output.EdmTypeValueLookup.Add(type, recordTypeValue);
				typeValue2 = recordTypeValue;
			}
			finally
			{
				processingTypes.Pop();
			}
			return typeValue2;
		}

		// Token: 0x06003D0A RID: 15626 RVA: 0x000C640C File Offset: 0x000C460C
		private bool TryAddMoreColumns(List<NamedValue> types, Microsoft.OData.Edm.IEdmStructuredType type, Microsoft.OData.Edm.IEdmModel model, out string newColumnName)
		{
			if (this.userSettings.MoreColumns && (type.IsOpen || this.IsBaseClass(type, model)))
			{
				newColumnName = EdmModelProcessorBase<EdmModelProcessorOutput>.GetOtherColumnsColumnName(types.Select((NamedValue namedValue) => namedValue.Key));
				types.Add(new NamedValue(newColumnName, EdmModelProcessorBase<EdmModelProcessorOutput>.OtherFieldsColumnTypeField));
				return true;
			}
			newColumnName = null;
			return false;
		}

		// Token: 0x06003D0B RID: 15627 RVA: 0x000C6480 File Offset: 0x000C4680
		private bool IsBaseClass(Microsoft.OData.Edm.IEdmStructuredType type, Microsoft.OData.Edm.IEdmModel model)
		{
			if (this.allBaseTypes == null)
			{
				this.allBaseTypes = new HashSet<Microsoft.OData.Edm.IEdmStructuredType>();
				foreach (Microsoft.OData.Edm.IEdmSchemaElement edmSchemaElement in model.SchemaElements)
				{
					Microsoft.OData.Edm.IEdmStructuredType edmStructuredType = edmSchemaElement as Microsoft.OData.Edm.IEdmStructuredType;
					if (edmStructuredType != null && edmStructuredType.BaseType != null)
					{
						this.allBaseTypes.Add(edmStructuredType.BaseType);
					}
				}
			}
			return this.allBaseTypes.Contains(type);
		}

		// Token: 0x06003D0C RID: 15628 RVA: 0x000C6508 File Offset: 0x000C4708
		private object GetOrCreatePropertyTypeValue(Stack<Microsoft.OData.Edm.IEdmType> processingTypes, Microsoft.OData.Edm.IEdmProperty property)
		{
			Microsoft.OData.Edm.IEdmType edmType = property.Type.Definition;
			edmType = ODataTypeServices.StripCollectionType(edmType);
			if (processingTypes.Contains(edmType))
			{
				return new Func<int, Value>(delegate(int i)
				{
					if (i != 0)
					{
						return LogicalValue.False;
					}
					return this.GetPropertyDelayedTypeValue(property);
				});
			}
			TypeValue orCreateTypeValue = this.GetOrCreateTypeValue(processingTypes, edmType);
			return this.GetPropertyTypeValue(property, orCreateTypeValue);
		}

		// Token: 0x06003D0D RID: 15629 RVA: 0x000C6570 File Offset: 0x000C4770
		private TypeValue GetOrCreateTypeValue(Stack<Microsoft.OData.Edm.IEdmType> processingTypes, Microsoft.OData.Edm.IEdmType type)
		{
			TypeValue typeValue;
			if (this.TryGetTypeValue(type, out typeValue))
			{
				return typeValue;
			}
			switch (type.TypeKind)
			{
			case Microsoft.OData.Edm.EdmTypeKind.Primitive:
				return ODataTypeServices.GetTypeValueFromEdm(((Microsoft.OData.Edm.IEdmPrimitiveType)type).PrimitiveKind);
			case Microsoft.OData.Edm.EdmTypeKind.Entity:
			case Microsoft.OData.Edm.EdmTypeKind.Complex:
				return this.CreateRecordTypeValue(processingTypes, (Microsoft.OData.Edm.IEdmStructuredType)type);
			case Microsoft.OData.Edm.EdmTypeKind.Collection:
				return this.GetOrCreateTypeValue(processingTypes, ODataTypeServices.StripCollectionType(type));
			default:
				return TypeValue.Any;
			}
		}

		// Token: 0x06003D0E RID: 15630 RVA: 0x000C65DD File Offset: 0x000C47DD
		private TypeValue GetOrCreateTypeValue(Microsoft.OData.Edm.IEdmType type)
		{
			return this.GetOrCreateTypeValue(new Stack<Microsoft.OData.Edm.IEdmType>(), type);
		}

		// Token: 0x06003D0F RID: 15631 RVA: 0x000C65EC File Offset: 0x000C47EC
		private bool TryGetTypeValue(Microsoft.OData.Edm.IEdmType type, out TypeValue typeValue)
		{
			Microsoft.OData.Edm.IEdmType edmType = ODataTypeServices.StripCollectionType(type);
			if (edmType.TypeKind == Microsoft.OData.Edm.EdmTypeKind.Primitive)
			{
				typeValue = ODataTypeServices.GetTypeValueFromEdm(((Microsoft.OData.Edm.IEdmPrimitiveType)edmType).PrimitiveKind);
				return true;
			}
			return this.output.EdmTypeValueLookup.TryGetValue(edmType, out typeValue);
		}

		// Token: 0x06003D10 RID: 15632 RVA: 0x000C662F File Offset: 0x000C482F
		private Func<int, Value> GetFunctionDelayedTypeValue(Dictionary<Microsoft.OData.Edm.IEdmFunction, Capabilities> functionOverloads)
		{
			return delegate(int i)
			{
				if (i != 0)
				{
					return LogicalValue.False;
				}
				return this.GetFunctionTypeValue(functionOverloads);
			};
		}

		// Token: 0x06003D11 RID: 15633 RVA: 0x000C6650 File Offset: 0x000C4850
		private TypeValue GetFunctionTypeValue(Dictionary<Microsoft.OData.Edm.IEdmFunction, Capabilities> functionOverloads)
		{
			TypeValue typeValue = null;
			foreach (KeyValuePair<Microsoft.OData.Edm.IEdmFunction, Capabilities> keyValuePair in functionOverloads)
			{
				Microsoft.OData.Edm.IEdmType definition = keyValuePair.Key.ReturnType.Definition;
				TypeValue typeValue2 = this.GetFunctionTypeValue(keyValuePair.Key, definition);
				typeValue2 = this.AddDisplayAnnotationsMetadata(keyValuePair.Value.DisplayAnnotations, typeValue2).AsFunctionType;
				if (typeValue == null)
				{
					typeValue = typeValue2;
					if (!this.output.BoundFunctionOverloads.Keys.Contains(keyValuePair.Key.Name))
					{
						this.output.BoundFunctionOverloads.Add(keyValuePair.Key.Name, new Dictionary<Microsoft.OData.Edm.IEdmFunction, FunctionTypeValue>());
					}
				}
				else
				{
					RecordValue metaValue = typeValue.MetaValue;
					HashSet<TypeValue> hashSet = new HashSet<TypeValue>();
					Microsoft.OData.Edm.IEdmOperationParameter edmOperationParameter = keyValuePair.Key.Parameters.FirstOrDefault<Microsoft.OData.Edm.IEdmOperationParameter>();
					if (edmOperationParameter != null)
					{
						TypeValue parameterTypeValue = this.GetParameterTypeValue(edmOperationParameter);
						hashSet.Add(parameterTypeValue.NonNullable);
						hashSet.Add(parameterTypeValue.Nullable);
					}
					typeValue = TypeAlgebra.Union(typeValue, typeValue2, hashSet).AsFunctionType;
					typeValue = BinaryOperator.AddMeta.Invoke(typeValue, metaValue).AsType.AsFunctionType;
					typeValue = BinaryOperator.AddMeta.Invoke(typeValue, typeValue2.MetaValue).AsType.AsFunctionType;
				}
				this.output.BoundFunctionOverloads[keyValuePair.Key.Name][keyValuePair.Key] = typeValue2.AsFunctionType;
			}
			return typeValue;
		}

		// Token: 0x06003D12 RID: 15634 RVA: 0x000C67F8 File Offset: 0x000C49F8
		private TypeValue GetFunctionTypeValue(Microsoft.OData.Edm.IEdmFunction function, Microsoft.OData.Edm.IEdmType edmType)
		{
			TypeValue typeValue;
			this.TryGetTypeValue(edmType, out typeValue);
			if (function.ReturnType.TypeKind() == Microsoft.OData.Edm.EdmTypeKind.Collection)
			{
				if (function.ReturnType.AsCollection().ElementType().IsEntity())
				{
					typeValue = ((typeValue != null) ? base.CreateTableType(typeValue) : TypeValue.Table);
				}
				else
				{
					typeValue = ((typeValue != null) ? base.CreateListType(typeValue) : TypeValue.List);
				}
			}
			typeValue = typeValue ?? TypeValue.Any;
			Microsoft.OData.Edm.IEdmType edmType2 = ODataTypeServices.StripCollectionType(edmType);
			TypeValue typeValue2 = base.SetEdmTypeAttribute(typeValue, edmType2.FullTypeName());
			int num = function.Parameters.Count<Microsoft.OData.Edm.IEdmOperationParameter>();
			List<NamedValue> list = new List<NamedValue>(num - 1);
			for (int i = 1; i < num; i++)
			{
				Microsoft.OData.Edm.IEdmOperationParameter edmOperationParameter = function.Parameters.ElementAt(i);
				list.Add(new NamedValue(edmOperationParameter.Name, this.GetParameterTypeValue(edmOperationParameter)));
			}
			return FunctionTypeValue.New(typeValue2, RecordValue.New(list.ToArray()), list.Count);
		}

		// Token: 0x06003D13 RID: 15635 RVA: 0x000C68E4 File Offset: 0x000C4AE4
		private FunctionTypeValue GetFunctionImportTypeValue(Microsoft.OData.Edm.IEdmFunctionImport functionImport)
		{
			TypeValue functionImportTypeValue = this.GetFunctionImportTypeValue(functionImport, functionImport.Function.ReturnType.Definition);
			List<NamedValue> list = new List<NamedValue>();
			foreach (Microsoft.OData.Edm.IEdmOperationParameter edmOperationParameter in functionImport.Function.Parameters)
			{
				list.Add(new NamedValue(edmOperationParameter.Name, this.GetParameterTypeValue(edmOperationParameter)));
			}
			return FunctionTypeValue.New(functionImportTypeValue, RecordValue.New(list.ToArray()), list.Count);
		}

		// Token: 0x06003D14 RID: 15636 RVA: 0x000C697C File Offset: 0x000C4B7C
		private TypeValue GetFunctionImportTypeValue(Microsoft.OData.Edm.IEdmFunctionImport functionImport, Microsoft.OData.Edm.IEdmType edmType)
		{
			TypeValue typeValue = this.GetOrCreateTypeValue(edmType);
			if (functionImport.Function.ReturnType.TypeKind() == Microsoft.OData.Edm.EdmTypeKind.Collection)
			{
				if (functionImport.EntitySet != null)
				{
					typeValue = base.CreateTableType(typeValue);
				}
				else
				{
					typeValue = base.CreateListType(typeValue);
				}
			}
			if (functionImport.EntitySet == null)
			{
				typeValue = base.SetQueryableAttributeFalse(typeValue);
			}
			else
			{
				Microsoft.OData.Edm.IEdmType edmType2 = ODataTypeServices.StripCollectionType(edmType);
				typeValue = base.SetEdmTypeAttribute(typeValue, edmType2.FullTypeName());
			}
			return typeValue;
		}

		// Token: 0x06003D15 RID: 15637 RVA: 0x000C69E8 File Offset: 0x000C4BE8
		private string GetEdmTypeName(Microsoft.OData.Edm.IEdmTypeReference edmType)
		{
			string text = edmType.FullName();
			if (edmType.IsCollection())
			{
				Microsoft.OData.Edm.IEdmCollectionTypeReference edmCollectionTypeReference = edmType.AsCollection();
				text = string.Format(CultureInfo.InvariantCulture, "Collection({0})", edmCollectionTypeReference.ElementType().FullName());
			}
			return text;
		}

		// Token: 0x06003D16 RID: 15638 RVA: 0x000C6A28 File Offset: 0x000C4C28
		private TypeValue GetParameterTypeValue(Microsoft.OData.Edm.IEdmOperationParameter parameter)
		{
			TypeValue typeValue = this.GetOrCreateTypeValue(parameter.Type.Definition);
			if (parameter.Type.IsCollection())
			{
				typeValue = base.CreateListType(typeValue);
			}
			if (parameter.Type.IsNullable)
			{
				typeValue = typeValue.Nullable;
			}
			RecordValue recordValue = RecordValue.New(Keys.New("EdmParameterType"), new Value[] { TextValue.New(this.GetEdmTypeName(parameter.Type)) });
			typeValue = BinaryOperator.AddMeta.Invoke(typeValue, recordValue).AsType;
			List<NamedValue> parameterDisplayAnnotations = AnnotationProcessor.GetParameterDisplayAnnotations(this.model, parameter, this.userSettings);
			return this.AddDisplayAnnotationsMetadata(parameterDisplayAnnotations, typeValue);
		}

		// Token: 0x06003D17 RID: 15639 RVA: 0x000C6AC8 File Offset: 0x000C4CC8
		private TypeValue GetPropertyTypeValue(Microsoft.OData.Edm.IEdmProperty property, TypeValue typeValue)
		{
			bool flag = property.Type.TypeKind() == Microsoft.OData.Edm.EdmTypeKind.Collection;
			bool flag2 = property.PropertyKind == Microsoft.OData.Edm.EdmPropertyKind.Navigation;
			RecordValue metaValue = typeValue.MetaValue;
			if (!flag && (flag2 || property.Type.IsNullable))
			{
				typeValue = typeValue.Nullable;
			}
			if (flag)
			{
				if (flag2)
				{
					typeValue = base.CreateTableType(typeValue);
				}
				else
				{
					typeValue = base.CreateListType(typeValue);
				}
			}
			else if (flag2)
			{
				typeValue = PreviewServices.ConvertToDelayedValue(typeValue, "Entry");
			}
			typeValue = BinaryOperator.AddMeta.Invoke(typeValue, metaValue).AsType;
			return typeValue;
		}

		// Token: 0x06003D18 RID: 15640 RVA: 0x000C6B50 File Offset: 0x000C4D50
		private TypeValue GetPropertyDelayedTypeValue(Microsoft.OData.Edm.IEdmProperty property)
		{
			TypeValue typeValue;
			if (this.TryGetTypeValue(property.Type.Definition, out typeValue))
			{
				return this.GetPropertyTypeValue(property, typeValue);
			}
			return TypeValue.Any;
		}

		// Token: 0x06003D19 RID: 15641 RVA: 0x000C6B80 File Offset: 0x000C4D80
		protected override void ProcessEntityContainers()
		{
			Dictionary<string, IList<NamedValue>> dictionary = new Dictionary<string, IList<NamedValue>>();
			Dictionary<string, Microsoft.OData.Edm.IEdmEntityContainerElement> dictionary2 = new Dictionary<string, Microsoft.OData.Edm.IEdmEntityContainerElement>();
			Dictionary<string, Microsoft.OData.Edm.IEdmEntityContainerElement> dictionary3 = new Dictionary<string, Microsoft.OData.Edm.IEdmEntityContainerElement>();
			if (this.model != null && this.model.EntityContainer != null)
			{
				foreach (Microsoft.OData.Edm.IEdmEntityContainerElement edmEntityContainerElement in this.model.EntityContainer.Elements)
				{
					if (edmEntityContainerElement.ContainerElementKind == Microsoft.OData.Edm.EdmContainerElementKind.EntitySet)
					{
						Microsoft.OData.Edm.IEdmEntitySet edmEntitySet = (Microsoft.OData.Edm.IEdmEntitySet)edmEntityContainerElement;
						string name = edmEntitySet.EntityType().Name;
						dictionary2.Add(edmEntityContainerElement.Name, edmEntitySet);
						if (!string.IsNullOrEmpty(name) && !dictionary2.ContainsKey(name))
						{
							dictionary3[name] = edmEntitySet;
						}
						Capabilities capabilities = this.ProcessAndAddCapability(edmEntitySet.Name, edmEntitySet);
						dictionary.Add(edmEntitySet.Name, capabilities.DisplayAnnotations);
					}
					else if (edmEntityContainerElement.ContainerElementKind == Microsoft.OData.Edm.EdmContainerElementKind.FunctionImport)
					{
						Microsoft.OData.Edm.IEdmFunctionImport edmFunctionImport = (Microsoft.OData.Edm.IEdmFunctionImport)edmEntityContainerElement;
						FunctionTypeValue functionTypeValue = this.GetFunctionImportTypeValue(edmFunctionImport);
						Capabilities capabilities2 = this.ProcessAndAddCapability(edmFunctionImport.Name, edmFunctionImport);
						string text;
						if (this.userSettings.FunctionOverloads != null && !this.userSettings.FunctionOverloads.Value)
						{
							TypeValue typeValue;
							if (this.output.TypeCatalog.TryGetFunction(edmFunctionImport.Name, out typeValue))
							{
								functionTypeValue = TypeAlgebra.Union(typeValue, functionTypeValue).AsFunctionType;
								functionTypeValue = this.AddDisplayAnnotationsMetadata(capabilities2.DisplayAnnotations, functionTypeValue).AsFunctionType;
							}
							text = functionTypeValue.CreateSignature();
							ODataSchemaItem odataSchemaItem = new ODataSchemaItem(edmFunctionImport.Name, text);
							this.output.TypeCatalog.AddFunction(odataSchemaItem, functionTypeValue);
						}
						else
						{
							functionTypeValue = this.AddDisplayAnnotationsMetadata(capabilities2.DisplayAnnotations, functionTypeValue).AsFunctionType;
							text = functionTypeValue.CreateSignature();
							ODataSchemaItem odataSchemaItem = new ODataSchemaItem(edmFunctionImport.Name, text);
							this.output.TypeCatalog[odataSchemaItem] = functionTypeValue;
						}
						this.output.UnboundFunctionOverloads.Add(Tuple.Create<string, Microsoft.OData.Edm.IEdmFunctionImport, FunctionTypeValue>(text, edmFunctionImport, functionTypeValue));
					}
					else if (edmEntityContainerElement.ContainerElementKind == Microsoft.OData.Edm.EdmContainerElementKind.Singleton)
					{
						Microsoft.OData.Edm.IEdmSingleton edmSingleton = (Microsoft.OData.Edm.IEdmSingleton)edmEntityContainerElement;
						string name2 = edmSingleton.EntityType().Name;
						dictionary2.Add(edmEntityContainerElement.Name, edmSingleton);
						if (!string.IsNullOrEmpty(name2) && !dictionary2.ContainsKey(name2))
						{
							dictionary3[name2] = edmSingleton;
						}
						Capabilities capabilities3 = this.ProcessAndAddCapability(edmSingleton.Name, edmSingleton);
						dictionary.Add(edmSingleton.Name, capabilities3.DisplayAnnotations);
					}
				}
			}
			if (this.serviceDocument != null)
			{
				this.ProcessServiceDocument(dictionary2, dictionary, dictionary3);
			}
		}

		// Token: 0x06003D1A RID: 15642 RVA: 0x000C6E40 File Offset: 0x000C5040
		private void ProcessServiceDocument(Dictionary<string, Microsoft.OData.Edm.IEdmEntityContainerElement> entitySets, Dictionary<string, IList<NamedValue>> entitySetAnnotations, Dictionary<string, Microsoft.OData.Edm.IEdmEntityContainerElement> entitySetsByTypeName)
		{
			ODataServiceDocument document = this.serviceDocument.Document;
			if (document.EntitySets != null)
			{
				this.AddDocumentElements(document.EntitySets.OfType<ODataServiceDocumentElement>(), entitySets, entitySetAnnotations, entitySetsByTypeName, false);
			}
			if (document.Singletons != null)
			{
				this.AddDocumentElements(document.Singletons.OfType<ODataServiceDocumentElement>(), entitySets, entitySetAnnotations, entitySetsByTypeName, true);
			}
		}

		// Token: 0x06003D1B RID: 15643 RVA: 0x000C6E94 File Offset: 0x000C5094
		private void AddDocumentElements(IEnumerable<ODataServiceDocumentElement> elements, Dictionary<string, Microsoft.OData.Edm.IEdmEntityContainerElement> entitySets, Dictionary<string, IList<NamedValue>> entitySetAnnotations, Dictionary<string, Microsoft.OData.Edm.IEdmEntityContainerElement> entitySetsByTypeName, bool isSingleton = false)
		{
			string text;
			if (isSingleton)
			{
				text = "singleton";
			}
			else
			{
				text = "table";
			}
			foreach (ODataServiceDocumentElement odataServiceDocumentElement in elements)
			{
				if (odataServiceDocumentElement.Url != null && odataServiceDocumentElement.Url.Segments.Length != 0)
				{
					AtomResourceCollectionMetadata annotation = odataServiceDocumentElement.GetAnnotation<AtomResourceCollectionMetadata>();
					if (annotation == null || string.IsNullOrEmpty(annotation.Accept) || !string.Equals(annotation.Accept, "application/atomsvc+xml", StringComparison.OrdinalIgnoreCase))
					{
						string text2 = odataServiceDocumentElement.Name ?? Uri.UnescapeDataString(odataServiceDocumentElement.Url.Segments.Last<string>());
						Microsoft.OData.Edm.IEdmEntityContainerElement edmEntityContainerElement;
						if (entitySets.TryGetValue(text2, out edmEntityContainerElement) || entitySetsByTypeName.TryGetValue(text2, out edmEntityContainerElement))
						{
							ODataSchemaItem odataSchemaItem = new ODataSchemaItem(edmEntityContainerElement.Name, text);
							this.AddToCatalog(odataSchemaItem, text2, odataServiceDocumentElement.Url, edmEntityContainerElement, entitySetAnnotations[edmEntityContainerElement.Name]);
						}
					}
				}
			}
		}

		// Token: 0x06003D1C RID: 15644 RVA: 0x000C6FA4 File Offset: 0x000C51A4
		private void AddToCatalog(ODataSchemaItem schemaItem, string resource, Uri uri, Microsoft.OData.Edm.IEdmEntityContainerElement element, IList<NamedValue> displayAnnotations)
		{
			TypeValue typeValue;
			if (!this.output.TypeCatalog.TryGetValue(schemaItem, out typeValue))
			{
				Microsoft.OData.Edm.IEdmEntitySet edmEntitySet = element as Microsoft.OData.Edm.IEdmEntitySet;
				Microsoft.OData.Edm.IEdmSingleton edmSingleton = element as Microsoft.OData.Edm.IEdmSingleton;
				Microsoft.OData.Edm.IEdmType edmType;
				if (edmEntitySet != null)
				{
					edmType = edmEntitySet.Type;
				}
				else
				{
					edmType = edmSingleton.Type;
				}
				if (this.TryGetTypeValue(edmType, out typeValue))
				{
					typeValue = base.CreateTableType(typeValue);
				}
				else
				{
					typeValue = TypeValue.Any;
				}
				if (this.userSettings.IncludeMetadataAnnotations == null)
				{
					typeValue = this.AddDisplayAnnotationsMetadata(displayAnnotations, typeValue);
				}
				this.AddToCatalog(schemaItem, uri, typeValue);
			}
		}

		// Token: 0x06003D1D RID: 15645 RVA: 0x000C7024 File Offset: 0x000C5224
		private void AddToCatalog(ODataSchemaItem schemaItem, Uri uri, TypeValue itemType)
		{
			this.output.TypeCatalog.Add(schemaItem, itemType);
			Uri serviceUri = this.serviceDocument.ServiceUri;
			if (serviceUri != null)
			{
				this.output.CollectionUrls.Add(schemaItem, serviceUri.MakeRelativeUri(uri).OriginalString);
				return;
			}
			this.output.CollectionUrls.Add(schemaItem, uri.OriginalString);
		}

		// Token: 0x06003D1E RID: 15646 RVA: 0x000C7090 File Offset: 0x000C5290
		protected override void ProcessTypes()
		{
			if (this.model != null)
			{
				if (this.model.EntityContainer != null)
				{
					Microsoft.OData.Edm.IEdmEntityContainer entityContainer = this.model.EntityContainer;
					this.output.TypeMetaValue = AnnotationProcessor.ProcessEntityContainer(this.model, entityContainer, this.output.Annotations, this.userSettings);
				}
				foreach (Microsoft.OData.Edm.IEdmSchemaElement edmSchemaElement in this.model.SchemaElements)
				{
					if (edmSchemaElement.SchemaElementKind == Microsoft.OData.Edm.EdmSchemaElementKind.TypeDefinition)
					{
						this.GetOrCreateTypeValue((Microsoft.OData.Edm.IEdmType)edmSchemaElement);
					}
				}
			}
		}

		// Token: 0x06003D1F RID: 15647 RVA: 0x000C1AD4 File Offset: 0x000BFCD4
		private TypeValue AddDisplayAnnotationsMetadata(RecordValue annotations, TypeValue type)
		{
			if (annotations.Keys.Length > 0)
			{
				return BinaryOperator.AddMeta.Invoke(type, annotations).AsType;
			}
			return type;
		}

		// Token: 0x06003D20 RID: 15648 RVA: 0x000C7140 File Offset: 0x000C5340
		private TypeValue AddDisplayAnnotationsMetadata(IList<NamedValue> namedValueAnnotations, TypeValue type)
		{
			if (namedValueAnnotations.Count > 0)
			{
				return BinaryOperator.AddMeta.Invoke(type, RecordValue.New(namedValueAnnotations.ToArray<NamedValue>())).AsType;
			}
			return type;
		}

		// Token: 0x06003D21 RID: 15649 RVA: 0x000C7168 File Offset: 0x000C5368
		private Capabilities ProcessAndAddCapability(string key, IEdmVocabularyAnnotatable annotatable)
		{
			Capabilities capabilities = AnnotationProcessor.ProcessCapabilities(key, this.model, annotatable, this.output.Annotations, this.userSettings);
			this.output.Annotations.TryAddCapability(annotatable, capabilities);
			return capabilities;
		}

		// Token: 0x04001FF6 RID: 8182
		private readonly Microsoft.OData.Edm.IEdmModel model;

		// Token: 0x04001FF7 RID: 8183
		private readonly IResource resource;

		// Token: 0x04001FF8 RID: 8184
		private readonly ServiceDocumentWrapper serviceDocument;

		// Token: 0x04001FF9 RID: 8185
		private readonly ODataUserSettings userSettings;

		// Token: 0x04001FFA RID: 8186
		private HashSet<Microsoft.OData.Edm.IEdmStructuredType> allBaseTypes;
	}
}
