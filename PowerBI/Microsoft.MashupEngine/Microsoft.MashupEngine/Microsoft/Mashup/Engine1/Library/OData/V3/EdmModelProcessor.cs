using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Annotations;
using Microsoft.Data.Edm.Csdl;
using Microsoft.Data.Edm.Expressions;
using Microsoft.Data.Edm.Values;
using Microsoft.Data.OData;
using Microsoft.Data.OData.Atom;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Resources;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Library.OData.V3
{
	// Token: 0x020008AF RID: 2223
	internal class EdmModelProcessor : EdmModelProcessorBase<EdmModelProcessorOutput>
	{
		// Token: 0x06003F91 RID: 16273 RVA: 0x000D1C24 File Offset: 0x000CFE24
		private EdmModelProcessor(IEngineHost engineHost, ODataServiceDocument serviceDocument, IEdmModel model, ODataUserSettings userSettings, IResource resource)
			: base(engineHost)
		{
			this.serviceDocument = serviceDocument;
			this.model = model;
			this.userSettings = userSettings;
			this.resource = resource;
		}

		// Token: 0x06003F92 RID: 16274 RVA: 0x000D1C56 File Offset: 0x000CFE56
		public static EdmModelProcessorOutput Build(IEngineHost engineHost, ODataServiceDocument serviceDocument, IEdmModel model, ODataUserSettings userSettings, HttpResource resource)
		{
			return new EdmModelProcessor(engineHost, serviceDocument, model, userSettings, resource.Resource).Build(resource.NewUrl(serviceDocument.ServiceLocation.AbsoluteUri));
		}

		// Token: 0x06003F93 RID: 16275 RVA: 0x000D1C80 File Offset: 0x000CFE80
		private List<NamedValue> GetAnnotations(IEdmStructuredType type)
		{
			bool flag = this.userSettings.IncludeMetadataAnnotations != null;
			List<NamedValue> list = new List<NamedValue>();
			if (flag)
			{
				List<NamedValue> list2 = new List<NamedValue>();
				List<NamedValue> list3 = new List<NamedValue>();
				IEdmVocabularyAnnotatable edmVocabularyAnnotatable = type as IEdmVocabularyAnnotatable;
				if (edmVocabularyAnnotatable != null)
				{
					List<NamedValue> vocabularyAnnotations = this.GetVocabularyAnnotations(edmVocabularyAnnotatable);
					if (vocabularyAnnotations.Count > 0)
					{
						list3.AddRange(vocabularyAnnotations);
					}
				}
				foreach (IEdmProperty edmProperty in type.Properties())
				{
					edmVocabularyAnnotatable = edmProperty;
					if (edmVocabularyAnnotatable != null)
					{
						List<NamedValue> vocabularyAnnotations2 = this.GetVocabularyAnnotations(edmVocabularyAnnotatable);
						if (vocabularyAnnotations2.Count > 0)
						{
							list2.Add(new NamedValue(edmProperty.Name, RecordValue.New(vocabularyAnnotations2.ToArray())));
						}
					}
				}
				if (list2.Count > 0)
				{
					list.Add(new NamedValue("OData.FieldAnnotations", RecordValue.New(list2.ToArray())));
				}
				if (list3.Count > 0)
				{
					list.Add(new NamedValue("OData.Annotations", RecordValue.New(list3.ToArray())));
				}
			}
			return list;
		}

		// Token: 0x06003F94 RID: 16276 RVA: 0x000D1D98 File Offset: 0x000CFF98
		private static Value ToValueFromODataExpression(IEdmExpression expression)
		{
			EdmExpressionKind expressionKind = expression.ExpressionKind;
			switch (expressionKind)
			{
			case EdmExpressionKind.BinaryConstant:
				return BinaryValue.New(((IEdmBinaryConstantExpression)expression).Value);
			case EdmExpressionKind.BooleanConstant:
				return LogicalValue.New(((IEdmBooleanConstantExpression)expression).Value);
			case EdmExpressionKind.DateTimeConstant:
				break;
			case EdmExpressionKind.DateTimeOffsetConstant:
				return DateTimeZoneValue.New(((IEdmDateTimeOffsetConstantExpression)expression).Value);
			case EdmExpressionKind.DecimalConstant:
				return NumberValue.New(((IEdmDecimalConstantExpression)expression).Value);
			case EdmExpressionKind.FloatingConstant:
				return NumberValue.New(((IEdmFloatingConstantExpression)expression).Value);
			case EdmExpressionKind.GuidConstant:
				return TextValue.New(((IEdmGuidConstantExpression)expression).Value.ToString());
			case EdmExpressionKind.IntegerConstant:
				return NumberValue.New(((IEdmIntegerConstantExpression)expression).Value);
			case EdmExpressionKind.StringConstant:
				return TextValue.New(((IEdmStringConstantExpression)expression).Value);
			case EdmExpressionKind.TimeConstant:
				return TimeValue.New(((IEdmTimeConstantExpression)expression).Value);
			default:
				if (expressionKind == EdmExpressionKind.EnumMemberReference)
				{
					new List<string>();
					IEdmEnumMember referencedEnumMember = (expression as IEdmEnumMemberReferenceExpression).ReferencedEnumMember;
					return TextValue.New(string.Format(CultureInfo.InvariantCulture, "{0}/{1}", referencedEnumMember.DeclaringType, referencedEnumMember.Name));
				}
				break;
			}
			return Value.Null;
		}

		// Token: 0x06003F95 RID: 16277 RVA: 0x000D1EC8 File Offset: 0x000D00C8
		private TypeValue CreateRecordTypeValue(Stack<IEdmType> processingTypes, IEdmStructuredType type)
		{
			processingTypes.Push(type);
			TypeValue typeValue2;
			try
			{
				List<string> list = new List<string>();
				if (type.TypeKind == EdmTypeKind.Entity)
				{
					foreach (IEdmStructuralProperty edmStructuralProperty in ((IEdmEntityType)type).Key())
					{
						string text = EdmNameEncoder.Decode(edmStructuralProperty.Name);
						list.Add(text);
					}
				}
				List<NamedValue> list2 = new List<NamedValue>();
				List<NamedValue> list3 = new List<NamedValue>();
				Dictionary<string, int> dictionary = new Dictionary<string, int>();
				List<NamedValue> annotations = this.GetAnnotations(type);
				foreach (IEdmProperty edmProperty in type.Properties())
				{
					TextValue textValue = null;
					object orCreateTypeValue = this.GetOrCreateTypeValue(processingTypes, edmProperty, out textValue);
					if (textValue != null)
					{
						list3.Add(new NamedValue(edmProperty.Name, textValue));
					}
					TypeValue typeValue = orCreateTypeValue as TypeValue;
					Func<int, Value> func = orCreateTypeValue as Func<int, Value>;
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
							throw ODataCommonErrors.DuplicateProperty(this.engineHost, this.resource, null, text2);
						}
					}
				}
				string text3;
				bool flag = this.TryAddMoreColumns(list2, type, this.model, out text3);
				RecordTypeValue recordTypeValue = ((list2.Count > 0) ? RecordTypeValue.New(RecordValue.New(list2.ToArray()), false) : TypeValue.Record);
				if (flag)
				{
					recordTypeValue = TypeServices.ConvertToMoreColumns(recordTypeValue, text3);
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
				if (!this.tableAnnotations.ContainsKey(recordTypeValue))
				{
					if (annotations.Count > 0)
					{
						this.tableAnnotations.Add(recordTypeValue, annotations);
						recordTypeValue = (RecordTypeValue)BinaryOperator.AddMeta.Invoke(recordTypeValue, RecordValue.New(annotations.ToArray())).AsType;
					}
					else if (list3.Count > 0)
					{
						List<NamedValue> list4 = new List<NamedValue>();
						list4.Add(new NamedValue("Documentation.FieldDescription", RecordValue.New(list3.ToArray())));
						this.tableAnnotations.Add(recordTypeValue, list4);
						recordTypeValue = (RecordTypeValue)BinaryOperator.AddMeta.Invoke(recordTypeValue, RecordValue.New(list4.ToArray())).AsType;
					}
				}
				this.typeValues.Add(type, recordTypeValue);
				typeValue2 = recordTypeValue;
			}
			finally
			{
				processingTypes.Pop();
			}
			return typeValue2;
		}

		// Token: 0x06003F96 RID: 16278 RVA: 0x000D2254 File Offset: 0x000D0454
		private bool TryAddMoreColumns(List<NamedValue> types, IEdmStructuredType type, IEdmModel model, out string newColumnName)
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

		// Token: 0x06003F97 RID: 16279 RVA: 0x000D22C8 File Offset: 0x000D04C8
		private bool IsBaseClass(IEdmStructuredType type, IEdmModel model)
		{
			if (this.allBaseTypes == null)
			{
				this.allBaseTypes = new HashSet<IEdmStructuredType>();
				foreach (IEdmSchemaElement edmSchemaElement in model.SchemaElements)
				{
					IEdmStructuredType edmStructuredType = edmSchemaElement as IEdmStructuredType;
					if (edmStructuredType != null && edmStructuredType.BaseType != null)
					{
						this.allBaseTypes.Add(edmStructuredType.BaseType);
					}
				}
			}
			return this.allBaseTypes.Contains(type);
		}

		// Token: 0x06003F98 RID: 16280 RVA: 0x000D2350 File Offset: 0x000D0550
		private object GetOrCreateTypeValue(Stack<IEdmType> processingTypes, IEdmProperty property, out TextValue description)
		{
			IEdmType edmType = property.Type.Definition;
			edmType = ODataTypeServices.StripCollectionType(edmType);
			if (processingTypes.Contains(edmType))
			{
				object obj = new Func<int, Value>(delegate(int i)
				{
					if (i != 0)
					{
						return LogicalValue.False;
					}
					return this.GetTypeValueDelayed(property);
				});
				description = this.GetPropertyDescription(property);
				return obj;
			}
			TypeValue orCreateTypeValue = this.GetOrCreateTypeValue(processingTypes, edmType);
			return this.GetTypeValue(property, orCreateTypeValue, out description);
		}

		// Token: 0x06003F99 RID: 16281 RVA: 0x000D23C4 File Offset: 0x000D05C4
		private TypeValue GetOrCreateTypeValue(Stack<IEdmType> processingTypes, IEdmType type)
		{
			TypeValue typeValue = this.GetTypeValue(type);
			if (typeValue != null)
			{
				return typeValue;
			}
			switch (type.TypeKind)
			{
			case EdmTypeKind.Primitive:
				return ODataTypeServices.GetTypeValueFromEdm(((IEdmPrimitiveType)type).PrimitiveKind);
			case EdmTypeKind.Entity:
			case EdmTypeKind.Complex:
				return this.CreateRecordTypeValue(processingTypes, (IEdmStructuredType)type);
			case EdmTypeKind.Collection:
				return this.GetOrCreateTypeValue(processingTypes, ODataTypeServices.StripCollectionType(type));
			}
			return TypeValue.Any;
		}

		// Token: 0x06003F9A RID: 16282 RVA: 0x000D2435 File Offset: 0x000D0635
		private TypeValue GetOrCreateTypeValue(IEdmType type)
		{
			return this.GetOrCreateTypeValue(new Stack<IEdmType>(), type);
		}

		// Token: 0x06003F9B RID: 16283 RVA: 0x000D2444 File Offset: 0x000D0644
		private RecordValue GetEntityContainerTypeMetaValue(IEdmEntityContainer entityContainer)
		{
			List<NamedValue> vocabularyAnnotations = this.GetVocabularyAnnotations(entityContainer);
			if (this.userSettings.IncludeMetadataAnnotations != null)
			{
				return RecordValue.New(vocabularyAnnotations.ToArray());
			}
			foreach (NamedValue namedValue in vocabularyAnnotations)
			{
				if (namedValue.Key == "Documentation.Description")
				{
					NamedValue namedValue2 = new NamedValue("Documentation.Description", namedValue.Value.AsText);
					return RecordValue.New(new NamedValue[] { namedValue2 });
				}
			}
			return null;
		}

		// Token: 0x06003F9C RID: 16284 RVA: 0x000D24F4 File Offset: 0x000D06F4
		private TextValue GetPropertyDescription(IEdmProperty property)
		{
			bool flag;
			RecordValue dataMarketAnnotations = this.GetDataMarketAnnotations(this.model.DirectValueAnnotations(property), out flag);
			Value value;
			if (dataMarketAnnotations != null && dataMarketAnnotations.TryGetValue("Documentation.Description", out value))
			{
				return value.AsText;
			}
			foreach (NamedValue namedValue in this.GetVocabularyAnnotations(property))
			{
				if (namedValue.Key == "Documentation.Description")
				{
					return namedValue.Value.AsText;
				}
			}
			return null;
		}

		// Token: 0x06003F9D RID: 16285 RVA: 0x000D2598 File Offset: 0x000D0798
		private TypeValue GetTypeValue(IEdmType type)
		{
			TypeValue typeValue;
			this.typeValues.TryGetValue(ODataTypeServices.StripCollectionType(type), out typeValue);
			return typeValue;
		}

		// Token: 0x06003F9E RID: 16286 RVA: 0x000D25BC File Offset: 0x000D07BC
		private FunctionTypeValue GetTypeValue(IEdmFunctionImport function)
		{
			TypeValue typeValue = this.GetTypeValue(function, function.ReturnType.Definition);
			List<NamedValue> list = new List<NamedValue>();
			foreach (IEdmFunctionParameter edmFunctionParameter in function.Parameters)
			{
				list.Add(new NamedValue(edmFunctionParameter.Name, this.GetTypeValue(edmFunctionParameter)));
			}
			return FunctionTypeValue.New(typeValue, RecordValue.New(list.ToArray()), list.Count);
		}

		// Token: 0x06003F9F RID: 16287 RVA: 0x000D264C File Offset: 0x000D084C
		private TypeValue GetTypeValue(IEdmFunctionImport function, IEdmType edmType)
		{
			TypeValue typeValue = this.GetOrCreateTypeValue(edmType);
			if (function.ReturnType.TypeKind() == EdmTypeKind.Collection)
			{
				if (function.EntitySet != null)
				{
					typeValue = base.CreateTableType(typeValue);
				}
				else
				{
					typeValue = base.CreateListType(typeValue);
				}
			}
			if (function.EntitySet == null)
			{
				typeValue = base.SetQueryableAttributeFalse(typeValue);
			}
			else
			{
				IEdmEntityType edmEntityType = (IEdmEntityType)ODataTypeServices.StripCollectionType(edmType);
				typeValue = base.SetEdmTypeAttribute(typeValue, edmEntityType.Namespace + "." + edmEntityType.Name);
			}
			return typeValue;
		}

		// Token: 0x06003FA0 RID: 16288 RVA: 0x000D26C8 File Offset: 0x000D08C8
		private string GetEdmTypeName(IEdmTypeReference edmType)
		{
			string text = edmType.FullName();
			if (edmType.IsCollection())
			{
				IEdmCollectionTypeReference edmCollectionTypeReference = edmType.AsCollection();
				text = string.Format(CultureInfo.InvariantCulture, "Collection({0})", edmCollectionTypeReference.ElementType().FullName());
			}
			return text;
		}

		// Token: 0x06003FA1 RID: 16289 RVA: 0x000D2708 File Offset: 0x000D0908
		private TypeValue GetTypeValue(IEdmFunctionParameter parameter)
		{
			TypeValue typeValue = this.GetOrCreateTypeValue(parameter.Type.Definition);
			bool flag;
			RecordValue dataMarketAnnotations = this.GetDataMarketAnnotations(this.model.DirectValueAnnotations(parameter), out flag);
			if (parameter.Type.IsCollection())
			{
				typeValue = base.CreateListType(typeValue);
			}
			if (flag || parameter.Type.IsNullable)
			{
				typeValue = typeValue.Nullable;
			}
			if (dataMarketAnnotations != null)
			{
				typeValue = BinaryOperator.AddMeta.Invoke(typeValue, dataMarketAnnotations).AsType;
			}
			RecordValue recordValue = RecordValue.New(Keys.New("EdmParameterType"), new Value[] { TextValue.New(this.GetEdmTypeName(parameter.Type)) });
			typeValue = BinaryOperator.AddMeta.Invoke(typeValue, recordValue).AsType;
			return this.ProcessVocabularyAnnotations(parameter, typeValue);
		}

		// Token: 0x06003FA2 RID: 16290 RVA: 0x000D27C4 File Offset: 0x000D09C4
		private TypeValue GetTypeValue(IEdmProperty property, TypeValue typeValue, out TextValue description)
		{
			bool flag;
			RecordValue dataMarketAnnotations = this.GetDataMarketAnnotations(this.model.DirectValueAnnotations(property), out flag);
			bool flag2 = property.Type.TypeKind() == EdmTypeKind.Collection;
			bool flag3 = property.PropertyKind == EdmPropertyKind.Navigation;
			RecordValue metaValue = typeValue.MetaValue;
			if (!flag && flag3)
			{
				EdmMultiplicity edmMultiplicity = ((IEdmNavigationProperty)property).Multiplicity();
				if (edmMultiplicity == EdmMultiplicity.Many || edmMultiplicity == EdmMultiplicity.ZeroOrOne)
				{
					flag = true;
				}
			}
			if (!flag2 && (flag3 || flag || property.Type.IsNullable))
			{
				typeValue = typeValue.Nullable;
			}
			if (flag2)
			{
				if (flag3)
				{
					typeValue = base.CreateTableType(typeValue);
				}
				else
				{
					typeValue = base.CreateListType(typeValue);
				}
			}
			else if (flag3)
			{
				typeValue = PreviewServices.ConvertToDelayedValue(typeValue, "Entry");
			}
			if (dataMarketAnnotations != null)
			{
				typeValue = BinaryOperator.AddMeta.Invoke(typeValue, dataMarketAnnotations).AsType;
			}
			if (this.userSettings.IncludeMetadataAnnotations == null)
			{
				typeValue = this.ProcessVocabularyAnnotations(property, typeValue);
			}
			RecordValue metaValue2 = typeValue.MetaValue;
			Value value;
			if (metaValue2 != null && metaValue2.TryGetValue("Documentation.Description", out value))
			{
				description = value.AsText;
			}
			else
			{
				description = null;
			}
			typeValue = BinaryOperator.AddMeta.Invoke(typeValue, metaValue).AsType;
			return typeValue;
		}

		// Token: 0x06003FA3 RID: 16291 RVA: 0x000D28DC File Offset: 0x000D0ADC
		private RecordValue GetDataMarketAnnotations(IEnumerable<IEdmDirectValueAnnotation> annotations, out bool nullable)
		{
			nullable = false;
			if (annotations != null)
			{
				bool flag = false;
				bool flag2 = false;
				List<NamedValue> list = new List<NamedValue>();
				HashSet<string> hashSet = new HashSet<string>();
				foreach (IEdmDirectValueAnnotation edmDirectValueAnnotation in annotations)
				{
					if (!hashSet.Contains(edmDirectValueAnnotation.Name))
					{
						IEdmStringValue edmStringValue = edmDirectValueAnnotation.Value as IEdmStringValue;
						if (edmStringValue != null)
						{
							if (edmDirectValueAnnotation.Name == "Description")
							{
								list.Add(new NamedValue("Documentation.Description", TextValue.New(edmStringValue.Value)));
							}
							else if (edmDirectValueAnnotation.Name == "Enum")
							{
								string[] entries = edmStringValue.Value.Split(new char[] { '|' });
								list.Add(new NamedValue("Documentation.AllowedValues", ListValue.New(entries.Length, (int i) => TextValue.New(entries[i]))));
							}
							else if (edmDirectValueAnnotation.Name == "SampleValues")
							{
								list.Add(new NamedValue("Documentation.SampleValues", ListValue.New(new Value[] { TextValue.New(edmStringValue.Value) })));
							}
							else
							{
								string text = edmStringValue.Value.ToUpperInvariant();
								if (edmDirectValueAnnotation.Name == "Queryable" && text == "FALSE")
								{
									flag = true;
								}
								else if (edmDirectValueAnnotation.Name == "Returned" && text == "FALSE")
								{
									flag2 = true;
								}
								else if (edmDirectValueAnnotation.Name == "Nullable" && text == "TRUE")
								{
									nullable = true;
								}
							}
						}
					}
				}
				if (flag || flag2)
				{
					list.Add(new NamedValue("Queryable", LogicalValue.New(!flag)));
					list.Add(new NamedValue("Returned", LogicalValue.New(!flag2)));
				}
				if (list.Count > 0)
				{
					return RecordValue.New(list.ToArray());
				}
			}
			return null;
		}

		// Token: 0x06003FA4 RID: 16292 RVA: 0x000D2B1C File Offset: 0x000D0D1C
		private TypeValue GetTypeValueDelayed(IEdmProperty property)
		{
			TypeValue typeValue = this.GetTypeValue(property.Type.Definition);
			if (typeValue == null)
			{
				return TypeValue.Any;
			}
			TextValue textValue;
			return this.GetTypeValue(property, typeValue, out textValue);
		}

		// Token: 0x06003FA5 RID: 16293 RVA: 0x000D2B50 File Offset: 0x000D0D50
		public static List<NamedValue> GetVocabularyAnnotations(IEdmVocabularyAnnotatable annotable, IEdmModel model, ODataUserSettings settings)
		{
			List<NamedValue> list = new List<NamedValue>();
			HashSet<string> hashSet = new HashSet<string>();
			bool flag = settings.IncludeMetadataAnnotations != null;
			foreach (IEdmValueAnnotation edmValueAnnotation in model.FindVocabularyAnnotations(annotable).OfType<IEdmValueAnnotation>())
			{
				string text = edmValueAnnotation.Term.FullName();
				if (!hashSet.Contains(text))
				{
					if (flag)
					{
						Value value = EdmModelProcessor.ToValueFromODataExpression(edmValueAnnotation.Value);
						if (value != Value.Null)
						{
							list.Add(new NamedValue(text, value));
						}
					}
					else if (!(text == "Org.OData.Documentation.V1.Description") && !(text == "Org.OData.Display.V1.Description"))
					{
						if (!(text == "Org.OData.Documentation.V1.SampleStringValues"))
						{
							if (text == "Org.OData.Documentation.V1.StringMembers")
							{
								if (edmValueAnnotation.Value.ExpressionKind == EdmExpressionKind.Collection)
								{
									IEdmCollectionExpression edmCollectionExpression = (IEdmCollectionExpression)edmValueAnnotation.Value;
									List<NamedValue> list2 = list;
									string text2 = "Documentation.AllowedValues";
									Value[] array = (from cs in edmCollectionExpression.Elements.OfType<IEdmStringConstantExpression>()
										select TextValue.New(cs.Value)).ToArray<TextValue>();
									list2.Add(new NamedValue(text2, ListValue.New(array)));
								}
							}
						}
						else if (edmValueAnnotation.Value.ExpressionKind == EdmExpressionKind.Collection)
						{
							IEdmCollectionExpression edmCollectionExpression2 = (IEdmCollectionExpression)edmValueAnnotation.Value;
							List<NamedValue> list3 = list;
							string text3 = "Documentation.SampleValues";
							Value[] array = (from cs in edmCollectionExpression2.Elements.OfType<IEdmStringConstantExpression>()
								select TextValue.New(cs.Value)).ToArray<TextValue>();
							list3.Add(new NamedValue(text3, ListValue.New(array)));
						}
					}
					else if (edmValueAnnotation.Value.ExpressionKind == EdmExpressionKind.StringConstant)
					{
						IEdmStringConstantExpression edmStringConstantExpression = (IEdmStringConstantExpression)edmValueAnnotation.Value;
						list.Add(new NamedValue("Documentation.Description", TextValue.New(edmStringConstantExpression.Value)));
					}
					hashSet.Add(text);
				}
			}
			return list;
		}

		// Token: 0x06003FA6 RID: 16294 RVA: 0x000D2D7C File Offset: 0x000D0F7C
		public List<NamedValue> GetVocabularyAnnotations(IEdmVocabularyAnnotatable annotable)
		{
			return EdmModelProcessor.GetVocabularyAnnotations(annotable, this.model, this.userSettings);
		}

		// Token: 0x06003FA7 RID: 16295 RVA: 0x000D2D90 File Offset: 0x000D0F90
		protected override void ProcessEntityContainers()
		{
			Dictionary<string, IEdmEntitySet> dictionary = new Dictionary<string, IEdmEntitySet>();
			Dictionary<string, IEdmEntitySet> dictionary2 = new Dictionary<string, IEdmEntitySet>();
			HashSet<string> hashSet = new HashSet<string>();
			if (this.model != null)
			{
				Version dataServiceVersion = this.model.GetDataServiceVersion();
				foreach (IEdmEntityContainer edmEntityContainer in this.model.EntityContainers())
				{
					if (this.output.TypeMetaValue == null)
					{
						this.output.TypeMetaValue = this.GetEntityContainerTypeMetaValue(edmEntityContainer);
					}
					foreach (IEdmEntityContainerElement edmEntityContainerElement in edmEntityContainer.Elements)
					{
						if (edmEntityContainerElement.ContainerElementKind == EdmContainerElementKind.EntitySet)
						{
							IEdmEntitySet edmEntitySet = (IEdmEntitySet)edmEntityContainerElement;
							try
							{
								dictionary.Add(edmEntityContainerElement.Name, edmEntitySet);
							}
							catch (ArgumentException)
							{
								if (!dictionary[edmEntityContainerElement.Name].ElementType.ToString().Equals(edmEntitySet.ElementType.ToString()))
								{
									hashSet.Add(edmEntityContainerElement.Name);
									continue;
								}
							}
							if (!string.IsNullOrEmpty(edmEntitySet.ElementType.Name) && !dictionary.ContainsKey(edmEntitySet.ElementType.Name))
							{
								dictionary2[edmEntitySet.ElementType.Name] = edmEntitySet;
							}
						}
						else if (edmEntityContainerElement.ContainerElementKind == EdmContainerElementKind.FunctionImport)
						{
							IEdmFunctionImport edmFunctionImport = (IEdmFunctionImport)edmEntityContainerElement;
							if (edmFunctionImport.ReturnType != null && !this.FunctionRequiresHttpPostMethod(edmFunctionImport, dataServiceVersion) && !edmFunctionImport.IsBindable)
							{
								FunctionTypeValue functionTypeValue = this.GetTypeValue(edmFunctionImport);
								bool flag = this.userSettings.FunctionOverloads == null || !this.userSettings.FunctionOverloads.Value;
								TypeValue typeValue;
								if (flag && this.output.TypeCatalog.TryGetFunction(edmFunctionImport.Name, out typeValue))
								{
									functionTypeValue = TypeAlgebra.Union(typeValue, functionTypeValue).AsFunctionType;
								}
								functionTypeValue = this.ProcessVocabularyAnnotations(edmFunctionImport, functionTypeValue).AsFunctionType;
								ODataSchemaItem odataSchemaItem = new ODataSchemaItem(edmFunctionImport.Name, functionTypeValue.CreateSignature());
								if (flag)
								{
									this.output.TypeCatalog.AddFunction(odataSchemaItem, functionTypeValue);
								}
								else
								{
									this.output.TypeCatalog[odataSchemaItem] = functionTypeValue;
								}
							}
						}
					}
				}
				if (hashSet.Count > 0)
				{
					foreach (string text in hashSet)
					{
						dictionary.Remove(text);
					}
				}
			}
			if (this.serviceDocument != null)
			{
				List<Tuple<ODataResourceCollectionInfo, AlternateFeedName, IEdmEntitySet>> list = new List<Tuple<ODataResourceCollectionInfo, AlternateFeedName, IEdmEntitySet>>();
				foreach (ODataResourceCollectionInfo odataResourceCollectionInfo in this.serviceDocument.Document.Collections)
				{
					if (!(odataResourceCollectionInfo.Url == null) && odataResourceCollectionInfo.Url.Segments.Length != 0)
					{
						IEdmEntitySet edmEntitySet2 = null;
						string text2 = odataResourceCollectionInfo.Name ?? Uri.UnescapeDataString(odataResourceCollectionInfo.Url.Segments.Last<string>());
						AtomResourceCollectionMetadata annotation = odataResourceCollectionInfo.GetAnnotation<AtomResourceCollectionMetadata>();
						ODataSchemaItem odataSchemaItem2 = new ODataSchemaItem(text2, "table");
						if (annotation == null || string.IsNullOrEmpty(annotation.Accept) || !string.Equals(annotation.Accept, "application/atomsvc+xml", StringComparison.OrdinalIgnoreCase))
						{
							AlternateFeedName annotation2 = odataResourceCollectionInfo.GetAnnotation<AlternateFeedName>();
							TypeValue typeValue2;
							if (dictionary.TryGetValue(text2, out edmEntitySet2) || dictionary2.TryGetValue(text2, out edmEntitySet2))
							{
								this.AddToCatalog(text2, odataResourceCollectionInfo.Url, edmEntitySet2);
							}
							else if (hashSet.Contains(text2))
							{
								this.AddToCatalog(odataSchemaItem2);
							}
							else if (!this.output.TypeCatalog.ContainsKey(odataSchemaItem2) && !this.output.TypeCatalog.TryGetFunction(text2, out typeValue2))
							{
								if (annotation2 != null && (dictionary.TryGetValue(annotation2.Name, out edmEntitySet2) || dictionary2.TryGetValue(annotation2.Name, out edmEntitySet2)))
								{
									list.Add(Tuple.Create<ODataResourceCollectionInfo, AlternateFeedName, IEdmEntitySet>(odataResourceCollectionInfo, annotation2, edmEntitySet2));
								}
								else
								{
									this.AddToCatalog(odataSchemaItem2, odataResourceCollectionInfo.Url, TypeValue.Table);
								}
							}
						}
						else
						{
							odataSchemaItem2 = new ODataSchemaItem(text2, "record");
							this.output.TypeCatalog.Add(odataSchemaItem2, NavigationTableServices.ConvertToLink(TypeValue.Record.Nullable, "Service", false));
							this.output.CollectionUrls.Add(odataSchemaItem2, odataResourceCollectionInfo.Url.AbsoluteUri);
						}
					}
				}
				foreach (Tuple<ODataResourceCollectionInfo, AlternateFeedName, IEdmEntitySet> tuple in list)
				{
					this.AddToCatalog(tuple.Item2.Name, tuple.Item2.Uri, tuple.Item3);
					tuple.Item1.Name = tuple.Item2.Name;
					tuple.Item1.Url = tuple.Item2.Uri;
				}
			}
		}

		// Token: 0x06003FA8 RID: 16296 RVA: 0x000D334C File Offset: 0x000D154C
		private void AddToCatalog(string resource, Uri uri, IEdmEntitySet entitySet)
		{
			ODataSchemaItem odataSchemaItem = new ODataSchemaItem(resource, "table");
			TypeValue typeValue;
			if (!this.output.TypeCatalog.TryGetValue(odataSchemaItem, out typeValue))
			{
				typeValue = base.CreateTableType(this.GetOrCreateTypeValue(entitySet.ElementType));
				if (this.userSettings.IncludeMetadataAnnotations == null)
				{
					typeValue = this.ProcessVocabularyAnnotations(entitySet, typeValue);
				}
				this.AddToCatalog(odataSchemaItem, uri, typeValue);
			}
		}

		// Token: 0x06003FA9 RID: 16297 RVA: 0x000D33AC File Offset: 0x000D15AC
		private void AddToCatalog(ODataSchemaItem schemaItem, Uri uri, TypeValue itemType)
		{
			this.output.TypeCatalog.Add(schemaItem, itemType);
			this.output.CollectionUrls.Add(schemaItem, this.serviceDocument.ServiceLocation.MakeRelativeUri(uri).OriginalString);
		}

		// Token: 0x06003FAA RID: 16298 RVA: 0x000D33E7 File Offset: 0x000D15E7
		private void AddToCatalog(ODataSchemaItem schemaItem)
		{
			this.output.TypeCatalog.Add(schemaItem, TypeValue.Any);
			this.output.CollectionUrls.Add(schemaItem, null);
		}

		// Token: 0x06003FAB RID: 16299 RVA: 0x000D3414 File Offset: 0x000D1614
		protected override void ProcessTypes()
		{
			if (this.model != null)
			{
				foreach (IEdmSchemaElement edmSchemaElement in this.model.SchemaElements)
				{
					if (edmSchemaElement.SchemaElementKind == EdmSchemaElementKind.TypeDefinition)
					{
						this.GetOrCreateTypeValue((IEdmType)edmSchemaElement);
					}
				}
			}
		}

		// Token: 0x06003FAC RID: 16300 RVA: 0x000D3480 File Offset: 0x000D1680
		private TypeValue ProcessVocabularyAnnotations(IEdmVocabularyAnnotatable annotable, TypeValue type)
		{
			List<NamedValue> vocabularyAnnotations = this.GetVocabularyAnnotations(annotable);
			if (vocabularyAnnotations.Count > 0)
			{
				return BinaryOperator.AddMeta.Invoke(type, RecordValue.New(vocabularyAnnotations.ToArray())).AsType;
			}
			return type;
		}

		// Token: 0x06003FAD RID: 16301 RVA: 0x000D34BC File Offset: 0x000D16BC
		private bool FunctionRequiresHttpPostMethod(IEdmFunctionImport function, Version modelDataServiceVersion)
		{
			string httpMethod = this.model.GetHttpMethod(function);
			return (modelDataServiceVersion != null && modelDataServiceVersion.Major > 2 && httpMethod == null && !function.IsComposable && function.IsSideEffecting) || httpMethod == "POST";
		}

		// Token: 0x0400215D RID: 8541
		private const string Nullable = "Nullable";

		// Token: 0x0400215E RID: 8542
		private readonly IEdmModel model;

		// Token: 0x0400215F RID: 8543
		private readonly IResource resource;

		// Token: 0x04002160 RID: 8544
		private readonly ODataServiceDocument serviceDocument;

		// Token: 0x04002161 RID: 8545
		private readonly Dictionary<IEdmType, TypeValue> typeValues = new Dictionary<IEdmType, TypeValue>();

		// Token: 0x04002162 RID: 8546
		private readonly ODataUserSettings userSettings;

		// Token: 0x04002163 RID: 8547
		private HashSet<IEdmStructuredType> allBaseTypes;
	}
}
