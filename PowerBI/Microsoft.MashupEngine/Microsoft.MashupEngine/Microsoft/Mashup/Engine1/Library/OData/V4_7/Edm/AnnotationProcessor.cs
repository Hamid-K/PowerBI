using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.Mashup.Engine1.Library.OData.V4_7.Edm
{
	// Token: 0x02000802 RID: 2050
	internal static class AnnotationProcessor
	{
		// Token: 0x06003B1B RID: 15131 RVA: 0x000BFE9C File Offset: 0x000BE09C
		public static RecordValue ProcessEntityContainer(Microsoft.OData.Edm.IEdmModel model, Microsoft.OData.Edm.IEdmEntityContainer container, Annotations output, ODataUserSettings userSettings)
		{
			List<NamedValue> displayAnnotations = new List<NamedValue> { AnnotationProcessor.GetDisplayNameAnnotation(container) };
			AnnotationProcessor.ProcessAnnotations(model, container, userSettings, delegate(string fullName, IEdmVocabularyAnnotation edmVocabularyAnnotation)
			{
				if (fullName != null)
				{
					int length = fullName.Length;
					switch (length)
					{
					case 39:
						if (fullName == "Org.OData.Aggregation.V1.ApplySupported")
						{
							output.ApplySupported = true;
							output.PropertyRestrictions = AnnotationProcessor.BuildAggregatePropertyRestrictions(edmVocabularyAnnotation.Value);
							output.AggregateTransformations = AnnotationProcessor.BuildAggregateTransformations(edmVocabularyAnnotation.Value);
							return;
						}
						break;
					case 40:
						if (fullName == "Org.OData.Capabilities.V1.BatchSupported")
						{
							output.SupportsBatch = AnnotationProcessor.ProcessBooleanExpressionValue(edmVocabularyAnnotation.Value);
							return;
						}
						break;
					case 41:
						if (fullName == "Org.OData.Capabilities.V1.FilterFunctions")
						{
							output.Filters = AnnotationProcessor.ProcessCollectionStringExpressionValue(edmVocabularyAnnotation.Value);
							return;
						}
						break;
					case 42:
					{
						char c = fullName[26];
						if (c != 'C')
						{
							if (c == 'S')
							{
								if (fullName == "Org.OData.Capabilities.V1.SupportedFormats")
								{
									output.SupportedFormats = AnnotationProcessor.ProcessCollectionStringExpressionValue(edmVocabularyAnnotation.Value);
									return;
								}
							}
						}
						else if (fullName == "Org.OData.Capabilities.V1.ConformanceLevel")
						{
							output.ConformanceLevel = ConformanceLevel.Advanced;
							return;
						}
						break;
					}
					case 43:
						break;
					case 44:
						if (fullName == "Org.OData.Capabilities.V1.CrossJoinSupported")
						{
							output.SupportsCrossJoin = AnnotationProcessor.ProcessBooleanExpressionValue(edmVocabularyAnnotation.Value);
							return;
						}
						break;
					default:
						if (length == 55)
						{
							if (fullName == "Org.OData.Capabilities.V1.BatchContinueOnErrorSupported")
							{
								output.SupportsBatchContinueOnError = AnnotationProcessor.ProcessBooleanExpressionValue(edmVocabularyAnnotation.Value);
								return;
							}
						}
						break;
					}
				}
				NamedValue namedValue;
				if (AnnotationProcessor.TryProcessDisplayAnnotations(edmVocabularyAnnotation, userSettings, out namedValue))
				{
					displayAnnotations.Add(namedValue);
				}
			});
			return RecordValue.New(displayAnnotations.ToArray());
		}

		// Token: 0x06003B1C RID: 15132 RVA: 0x000BFF00 File Offset: 0x000BE100
		public static Capabilities ProcessCapabilities(string displayName, Microsoft.OData.Edm.IEdmModel model, Microsoft.OData.Edm.Vocabularies.IEdmVocabularyAnnotatable annotatable, Annotations annotations, ODataUserSettings userSettings)
		{
			Capabilities capabilities = annotations.CreateDefaultCapabilities();
			capabilities.DisplayAnnotations.Add(AnnotationProcessor.GetDisplayNameAnnotation(displayName));
			HashSet<string> hashSet = new HashSet<string>();
			foreach (IEdmVocabularyAnnotation edmVocabularyAnnotation in model.FindVocabularyAnnotations(annotatable).OfType<IEdmVocabularyAnnotation>())
			{
				string text = edmVocabularyAnnotation.Term.FullName();
				if (!hashSet.Contains(text))
				{
					if (text == null)
					{
						goto IL_034C;
					}
					int length = text.Length;
					switch (length)
					{
					case 30:
						if (!(text == "Org.OData.Core.V1.ResourcePath"))
						{
							goto IL_034C;
						}
						capabilities.ResourcePath = AnnotationProcessor.ProcessUriExpressionValue(edmVocabularyAnnotation.Value);
						break;
					case 31:
					case 32:
					case 33:
					case 34:
					case 35:
					case 36:
					case 37:
						goto IL_034C;
					case 38:
						if (!(text == "Org.OData.Capabilities.V1.TopSupported"))
						{
							goto IL_034C;
						}
						capabilities.SupportsTop = AnnotationProcessor.ProcessBooleanExpressionValue(edmVocabularyAnnotation.Value);
						break;
					case 39:
					{
						char c = text[10];
						if (c != 'A')
						{
							if (c != 'C')
							{
								goto IL_034C;
							}
							if (!(text == "Org.OData.Capabilities.V1.SkipSupported"))
							{
								goto IL_034C;
							}
							capabilities.SupportsSkip = AnnotationProcessor.ProcessBooleanExpressionValue(edmVocabularyAnnotation.Value);
						}
						else
						{
							if (!(text == "Org.OData.Aggregation.V1.ApplySupported"))
							{
								goto IL_034C;
							}
							capabilities.ApplySupported = true;
							capabilities.PropertyRestrictions = AnnotationProcessor.BuildAggregatePropertyRestrictions(edmVocabularyAnnotation.Value);
							capabilities.AggregateTransformations = AnnotationProcessor.BuildAggregateTransformations(edmVocabularyAnnotation.Value);
						}
						break;
					}
					case 40:
						if (!(text == "Org.OData.Capabilities.V1.IndexableByKey"))
						{
							goto IL_034C;
						}
						capabilities.IsIndexableByKey = AnnotationProcessor.ProcessBooleanExpressionValue(edmVocabularyAnnotation.Value);
						break;
					case 41:
						if (!(text == "Org.OData.Capabilities.V1.FilterFunctions"))
						{
							goto IL_034C;
						}
						capabilities.FilterFunctions = AnnotationProcessor.ProcessCollectionStringExpressionValue(edmVocabularyAnnotation.Value);
						break;
					case 42:
						if (!(text == "Org.OData.Capabilities.V1.SortRestrictions"))
						{
							goto IL_034C;
						}
						capabilities.BuildSortRestrictions(edmVocabularyAnnotation.Value);
						break;
					case 43:
						if (!(text == "Org.OData.Capabilities.V1.CountRestrictions"))
						{
							goto IL_034C;
						}
						capabilities.BuildCountRestrictions(edmVocabularyAnnotation.Value);
						break;
					case 44:
					{
						char c = text[26];
						switch (c)
						{
						case 'D':
							if (!(text == "Org.OData.Capabilities.V1.DeleteRestrictions"))
							{
								goto IL_034C;
							}
							capabilities.BuildDeleteRestrictions(edmVocabularyAnnotation.Value);
							break;
						case 'E':
							if (!(text == "Org.OData.Capabilities.V1.ExpandRestrictions"))
							{
								goto IL_034C;
							}
							capabilities.BuildExpandRestrictions(edmVocabularyAnnotation.Value);
							break;
						case 'F':
							if (!(text == "Org.OData.Capabilities.V1.FilterRestrictions"))
							{
								goto IL_034C;
							}
							capabilities.BuildFilterRestrictions(edmVocabularyAnnotation.Value);
							break;
						case 'G':
						case 'H':
							goto IL_034C;
						case 'I':
							if (!(text == "Org.OData.Capabilities.V1.InsertRestrictions"))
							{
								goto IL_034C;
							}
							capabilities.BuildInsertRestrictions(edmVocabularyAnnotation.Value);
							break;
						default:
							if (c != 'U')
							{
								goto IL_034C;
							}
							if (!(text == "Org.OData.Capabilities.V1.UpdateRestrictions"))
							{
								goto IL_034C;
							}
							capabilities.BuildUpdateRestrictions(edmVocabularyAnnotation.Value);
							break;
						}
						break;
					}
					default:
						if (length != 48)
						{
							goto IL_034C;
						}
						if (!(text == "Org.OData.Capabilities.V1.NavigationRestrictions"))
						{
							goto IL_034C;
						}
						break;
					}
					IL_0365:
					hashSet.Add(text);
					continue;
					IL_034C:
					NamedValue namedValue;
					if (AnnotationProcessor.TryProcessDisplayAnnotations(edmVocabularyAnnotation, userSettings, out namedValue))
					{
						capabilities.DisplayAnnotations.Add(namedValue);
						goto IL_0365;
					}
					goto IL_0365;
				}
			}
			return capabilities;
		}

		// Token: 0x06003B1D RID: 15133 RVA: 0x000C02B0 File Offset: 0x000BE4B0
		public static List<NamedValue> GetAnnotations(Microsoft.OData.Edm.IEdmModel model, Microsoft.OData.Edm.IEdmStructuredType type, ODataUserSettings userSettings)
		{
			bool flag = userSettings.IncludeMetadataAnnotations != null;
			List<NamedValue> list = new List<NamedValue>();
			if (flag)
			{
				List<NamedValue> list2 = new List<NamedValue>();
				List<NamedValue> list3 = new List<NamedValue>();
				List<NamedValue> list4 = AnnotationProcessor.GetStructuredTypeAnnotations(model, type, userSettings);
				if (list4.Count > 0)
				{
					list3.AddRange(list4);
				}
				foreach (Microsoft.OData.Edm.IEdmProperty edmProperty in type.Properties())
				{
					list4 = AnnotationProcessor.GetPropertyAnnotations(model, edmProperty, userSettings);
					if (list4.Count > 0)
					{
						list2.Add(new NamedValue(edmProperty.Name, RecordValue.New(list4.ToArray())));
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
			else
			{
				List<NamedValue> list5 = new List<NamedValue>();
				foreach (Microsoft.OData.Edm.IEdmProperty edmProperty2 in type.Properties())
				{
					list5.AddRange(AnnotationProcessor.GetPropertyDisplayAnnotations(model, edmProperty2, userSettings));
				}
				if (list5.Count > 0)
				{
					list.Add(new NamedValue("Documentation.FieldDescription", RecordValue.New(list5.ToArray())));
				}
			}
			return list;
		}

		// Token: 0x06003B1E RID: 15134 RVA: 0x000C042C File Offset: 0x000BE62C
		public static List<NamedValue> GetPropertyDisplayAnnotations(Microsoft.OData.Edm.IEdmModel model, Microsoft.OData.Edm.IEdmProperty property, ODataUserSettings userSettings)
		{
			Value description = null;
			Value longDescription = null;
			AnnotationProcessor.ProcessAnnotations(model, property, userSettings, delegate(string fullName, IEdmVocabularyAnnotation edmVocabularyAnnotation)
			{
				if (description != null)
				{
					return;
				}
				NamedValue namedValue;
				if (AnnotationProcessor.TryProcessDisplayAnnotations(edmVocabularyAnnotation, userSettings, out namedValue))
				{
					if (namedValue.Key == "Documentation.Description" && description == null)
					{
						description = namedValue.Value;
						return;
					}
					if (namedValue.Key == "Documentation.LongDescription" && longDescription == null)
					{
						longDescription = namedValue.Value;
					}
				}
			});
			List<NamedValue> list = new List<NamedValue>();
			if (description != null || longDescription != null)
			{
				list.Add(new NamedValue(property.Name, description ?? longDescription));
			}
			return list;
		}

		// Token: 0x06003B1F RID: 15135 RVA: 0x000C04A8 File Offset: 0x000BE6A8
		public static List<NamedValue> GetStructuredTypeAnnotations(Microsoft.OData.Edm.IEdmModel model, Microsoft.OData.Edm.IEdmType edmType, ODataUserSettings userSettings)
		{
			List<NamedValue> typeAnnotations = new List<NamedValue>();
			Microsoft.OData.Edm.Vocabularies.IEdmVocabularyAnnotatable edmVocabularyAnnotatable = edmType as Microsoft.OData.Edm.Vocabularies.IEdmVocabularyAnnotatable;
			if (edmVocabularyAnnotatable != null)
			{
				AnnotationProcessor.ProcessAnnotations(model, edmVocabularyAnnotatable, userSettings, delegate(string fullName, IEdmVocabularyAnnotation edmVocabularyAnnotation)
				{
					NamedValue namedValue;
					if (AnnotationProcessor.TryProcessDisplayAnnotations(edmVocabularyAnnotation, userSettings, out namedValue))
					{
						typeAnnotations.Add(namedValue);
					}
				});
			}
			return typeAnnotations;
		}

		// Token: 0x06003B20 RID: 15136 RVA: 0x000C04F8 File Offset: 0x000BE6F8
		public static List<NamedValue> GetPropertyAnnotations(Microsoft.OData.Edm.IEdmModel model, Microsoft.OData.Edm.IEdmProperty property, ODataUserSettings userSettings)
		{
			List<NamedValue> propertyAnnotations = new List<NamedValue>();
			AnnotationProcessor.ProcessAnnotations(model, property, userSettings, delegate(string fullName, IEdmVocabularyAnnotation edmVocabularyAnnotation)
			{
				NamedValue namedValue;
				if (AnnotationProcessor.TryProcessDisplayAnnotations(edmVocabularyAnnotation, userSettings, out namedValue))
				{
					propertyAnnotations.Add(namedValue);
				}
			});
			return propertyAnnotations;
		}

		// Token: 0x06003B21 RID: 15137 RVA: 0x000C053C File Offset: 0x000BE73C
		public static List<NamedValue> GetParameterDisplayAnnotations(Microsoft.OData.Edm.IEdmModel model, Microsoft.OData.Edm.IEdmOperationParameter parameter, ODataUserSettings userSettings)
		{
			List<NamedValue> displayAnnotations = new List<NamedValue> { AnnotationProcessor.GetDisplayNameAnnotation(parameter) };
			AnnotationProcessor.ProcessAnnotations(model, parameter, userSettings, delegate(string fullName, IEdmVocabularyAnnotation edmVocabularyAnnotation)
			{
				NamedValue namedValue;
				if (AnnotationProcessor.TryProcessDisplayAnnotations(edmVocabularyAnnotation, userSettings, out namedValue))
				{
					displayAnnotations.Add(namedValue);
				}
			});
			return displayAnnotations;
		}

		// Token: 0x06003B22 RID: 15138 RVA: 0x000C058C File Offset: 0x000BE78C
		private static void ProcessAnnotations(Microsoft.OData.Edm.IEdmModel model, Microsoft.OData.Edm.Vocabularies.IEdmVocabularyAnnotatable vocabularyAnnotatable, ODataUserSettings userSettings, Action<string, IEdmVocabularyAnnotation> processAction)
		{
			HashSet<string> hashSet = new HashSet<string>();
			foreach (IEdmVocabularyAnnotation edmVocabularyAnnotation in model.FindVocabularyAnnotations(vocabularyAnnotatable).OfType<IEdmVocabularyAnnotation>())
			{
				string text = edmVocabularyAnnotation.Term.FullName();
				if (!hashSet.Contains(text))
				{
					processAction(text, edmVocabularyAnnotation);
					hashSet.Add(text);
				}
			}
		}

		// Token: 0x06003B23 RID: 15139 RVA: 0x000C0604 File Offset: 0x000BE804
		private static NamedValue GetDisplayNameAnnotation(Microsoft.OData.Edm.IEdmNamedElement namedElement)
		{
			return AnnotationProcessor.GetDisplayNameAnnotation(namedElement.Name);
		}

		// Token: 0x06003B24 RID: 15140 RVA: 0x000C0611 File Offset: 0x000BE811
		private static NamedValue GetDisplayNameAnnotation(string elementName)
		{
			return new NamedValue("Documentation.Caption", TextValue.New(elementName));
		}

		// Token: 0x06003B25 RID: 15141 RVA: 0x000C0624 File Offset: 0x000BE824
		private static bool TryProcessDisplayAnnotations(IEdmVocabularyAnnotation vocabularyAnnotation, ODataUserSettings userSettings, out NamedValue displayAnnotation)
		{
			if (userSettings.IncludeMetadataAnnotations != null)
			{
				string text = vocabularyAnnotation.Term.FullName();
				if (AnnotationFilter.Matches(userSettings.IncludeMetadataAnnotations, text))
				{
					Value value = AnnotationProcessor.ToValueFromODataExpression(vocabularyAnnotation.Value);
					if (value != Value.Null)
					{
						displayAnnotation = new NamedValue(text, value);
						return true;
					}
				}
				displayAnnotation = default(NamedValue);
				return false;
			}
			if (vocabularyAnnotation.Term.Name.Equals("Description") && vocabularyAnnotation.Value.ExpressionKind == EdmExpressionKind.StringConstant)
			{
				string value2 = ((IEdmStringConstantExpression)vocabularyAnnotation.Value).Value;
				if (!string.IsNullOrEmpty(value2))
				{
					displayAnnotation = new NamedValue("Documentation.Description", TextValue.New(value2));
					return true;
				}
			}
			if (vocabularyAnnotation.Term.Name.Equals("LongDescription") && vocabularyAnnotation.Value.ExpressionKind == EdmExpressionKind.StringConstant)
			{
				string value3 = ((IEdmStringConstantExpression)vocabularyAnnotation.Value).Value;
				if (!string.IsNullOrEmpty(value3))
				{
					displayAnnotation = new NamedValue("Documentation.LongDescription", TextValue.New(value3));
					return true;
				}
			}
			displayAnnotation = default(NamedValue);
			return false;
		}

		// Token: 0x06003B26 RID: 15142 RVA: 0x000C0734 File Offset: 0x000BE934
		private static Value ToValueFromODataExpression(IEdmExpression expression)
		{
			if (expression == null)
			{
				return Value.Null;
			}
			EdmExpressionKind expressionKind = expression.ExpressionKind;
			switch (expressionKind)
			{
			case EdmExpressionKind.BinaryConstant:
				return BinaryValue.New(((IEdmBinaryConstantExpression)expression).Value);
			case EdmExpressionKind.BooleanConstant:
				return LogicalValue.New(((IEdmBooleanConstantExpression)expression).Value);
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
			case EdmExpressionKind.DurationConstant:
				return DurationValue.New(((IEdmDurationConstantExpression)expression).Value);
			default:
				switch (expressionKind)
				{
				case EdmExpressionKind.DateConstant:
				{
					Date value = ((IEdmDateConstantExpression)expression).Value;
					return DateValue.New(value.Year, value.Month, value.Day);
				}
				case EdmExpressionKind.TimeOfDayConstant:
					return TimeValue.New(((IEdmTimeOfDayConstantExpression)expression).Value.Ticks);
				case EdmExpressionKind.EnumMember:
				{
					List<string> list = new List<string>();
					foreach (Microsoft.OData.Edm.IEdmEnumMember edmEnumMember in (expression as IEdmEnumMemberExpression).EnumMembers)
					{
						string text = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", edmEnumMember.DeclaringType.FullName(), edmEnumMember.Name);
						list.Add(text);
					}
					if (list.Count > 1)
					{
						return ListValue.New(list.ToArray());
					}
					return TextValue.New(list[0]);
				}
				default:
					return Value.Null;
				}
				break;
			}
		}

		// Token: 0x06003B27 RID: 15143 RVA: 0x000C0920 File Offset: 0x000BEB20
		private static void BuildExpandRestrictions(this Capabilities capability, IEdmExpression expressionValue)
		{
			if (expressionValue.ExpressionKind == EdmExpressionKind.Record)
			{
				IEdmRecordExpression edmRecordExpression = expressionValue as IEdmRecordExpression;
				capability.SupportsExpand = AnnotationProcessor.GetBooleanProperty(edmRecordExpression, "Expandable") ?? capability.SupportsExpand;
				capability.NonExpandableProperties = AnnotationProcessor.GetPropertyCollection(edmRecordExpression, "NonExpandableProperties");
			}
		}

		// Token: 0x06003B28 RID: 15144 RVA: 0x000C097C File Offset: 0x000BEB7C
		private static void BuildFilterRestrictions(this Capabilities capability, IEdmExpression expressionValue)
		{
			if (expressionValue.ExpressionKind == EdmExpressionKind.Record)
			{
				IEdmRecordExpression edmRecordExpression = expressionValue as IEdmRecordExpression;
				capability.SupportsFilter = AnnotationProcessor.GetBooleanProperty(edmRecordExpression, "Filterable") ?? capability.SupportsFilter;
				capability.NonFilterableProperties = AnnotationProcessor.GetPropertyCollection(edmRecordExpression, "NonFilterableProperties");
				capability.RequiredPropertiesInFilter = AnnotationProcessor.GetPropertyCollection(edmRecordExpression, "RequiredProperties");
				capability.RequiresFilter = AnnotationProcessor.GetBooleanProperty(edmRecordExpression, "RequiresFilter") ?? capability.RequiresFilter;
			}
		}

		// Token: 0x06003B29 RID: 15145 RVA: 0x000C0A10 File Offset: 0x000BEC10
		private static void BuildCountRestrictions(this Capabilities capability, IEdmExpression expressionValue)
		{
			if (expressionValue.ExpressionKind == EdmExpressionKind.Record)
			{
				IEdmRecordExpression edmRecordExpression = expressionValue as IEdmRecordExpression;
				capability.SupportsCount = AnnotationProcessor.GetBooleanProperty(edmRecordExpression, "Countable") ?? capability.SupportsCount;
				capability.NonCountableProperties = AnnotationProcessor.GetPropertyCollection(edmRecordExpression, "NonCountableProperties");
				capability.NonCountableNavigationProperties = AnnotationProcessor.GetPropertyCollection(edmRecordExpression, "NonCountableNavigationProperties");
			}
		}

		// Token: 0x06003B2A RID: 15146 RVA: 0x000C0A7C File Offset: 0x000BEC7C
		private static Dictionary<string, NavigationType> GetRestrictedNavigationProperties(IEdmRecordExpression recordExpressionValue)
		{
			IEdmPropertyConstructor edmPropertyConstructor = recordExpressionValue.FindProperty("RestrictedProperties");
			Dictionary<string, NavigationType> dictionary = new Dictionary<string, NavigationType>();
			if (edmPropertyConstructor != null && edmPropertyConstructor.Value.ExpressionKind == EdmExpressionKind.Collection)
			{
				foreach (IEdmExpression edmExpression in (edmPropertyConstructor.Value as IEdmCollectionExpression).Elements)
				{
					if (edmExpression.ExpressionKind == EdmExpressionKind.Record)
					{
						IEdmRecordExpression edmRecordExpression = edmExpression as IEdmRecordExpression;
						IEdmPropertyConstructor edmPropertyConstructor2 = edmRecordExpression.FindProperty("NavigationProperty");
						string text = null;
						if (edmPropertyConstructor2 != null)
						{
							text = AnnotationProcessor.ProcessNavigationPathExpression(edmPropertyConstructor2.Value);
						}
						if (text != null)
						{
							NavigationType enumPropertyOrDefault = AnnotationProcessor.GetEnumPropertyOrDefault<NavigationType>(edmRecordExpression, "Navigability", NavigationType.None);
							dictionary.Add(text, enumPropertyOrDefault);
						}
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06003B2B RID: 15147 RVA: 0x000C0B4C File Offset: 0x000BED4C
		private static void BuildInsertRestrictions(this Capabilities capability, IEdmExpression expressionValue)
		{
			if (expressionValue.ExpressionKind == EdmExpressionKind.Record)
			{
				bool? booleanProperty = AnnotationProcessor.GetBooleanProperty(expressionValue as IEdmRecordExpression, "Insertable");
				if (booleanProperty != null)
				{
					capability.IsInsertable = booleanProperty.Value;
				}
			}
		}

		// Token: 0x06003B2C RID: 15148 RVA: 0x000C0B8C File Offset: 0x000BED8C
		private static void BuildUpdateRestrictions(this Capabilities capability, IEdmExpression expressionValue)
		{
			if (expressionValue.ExpressionKind == EdmExpressionKind.Record)
			{
				bool? booleanProperty = AnnotationProcessor.GetBooleanProperty(expressionValue as IEdmRecordExpression, "Updatable");
				if (booleanProperty != null)
				{
					capability.IsUpdatable = booleanProperty.Value;
				}
			}
		}

		// Token: 0x06003B2D RID: 15149 RVA: 0x000C0BCC File Offset: 0x000BEDCC
		private static void BuildDeleteRestrictions(this Capabilities capability, IEdmExpression expressionValue)
		{
			if (expressionValue.ExpressionKind == EdmExpressionKind.Record)
			{
				bool? booleanProperty = AnnotationProcessor.GetBooleanProperty(expressionValue as IEdmRecordExpression, "Deletable");
				if (booleanProperty != null)
				{
					capability.IsDeletable = booleanProperty.Value;
				}
			}
		}

		// Token: 0x06003B2E RID: 15150 RVA: 0x000C0C0C File Offset: 0x000BEE0C
		private static void BuildSortRestrictions(this Capabilities capability, IEdmExpression expressionValue)
		{
			if (expressionValue.ExpressionKind == EdmExpressionKind.Record)
			{
				IEdmRecordExpression edmRecordExpression = expressionValue as IEdmRecordExpression;
				capability.SupportsSort = AnnotationProcessor.GetBooleanProperty(edmRecordExpression, "Sortable") ?? capability.SupportsSort;
				capability.AscendingOnlyProperties = AnnotationProcessor.GetPropertyCollection(edmRecordExpression, "AscendingOnlyProperties");
				capability.DescendingOnlyProperties = AnnotationProcessor.GetPropertyCollection(edmRecordExpression, "DescendingOnlyProperties");
				capability.NonSortableProperties = AnnotationProcessor.GetPropertyCollection(edmRecordExpression, "NonSortableProperties");
			}
		}

		// Token: 0x06003B2F RID: 15151 RVA: 0x000C0C88 File Offset: 0x000BEE88
		private static bool BuildAggregatePropertyRestrictions(IEdmExpression expressionValue)
		{
			return expressionValue.ExpressionKind == EdmExpressionKind.Record && AnnotationProcessor.GetBooleanProperty(expressionValue as IEdmRecordExpression, "PropertyRestrictions").GetValueOrDefault();
		}

		// Token: 0x06003B30 RID: 15152 RVA: 0x000C0CB9 File Offset: 0x000BEEB9
		private static HashSet<string> BuildAggregateTransformations(IEdmExpression expressionValue)
		{
			if (expressionValue.ExpressionKind == EdmExpressionKind.Record)
			{
				return AnnotationProcessor.GetPropertyCollection(expressionValue as IEdmRecordExpression, "Transformations");
			}
			return new HashSet<string>();
		}

		// Token: 0x06003B31 RID: 15153 RVA: 0x000C0CDC File Offset: 0x000BEEDC
		private static HashSet<string> GetPropertyCollection(IEdmRecordExpression recordExpression, string propertyName)
		{
			IEdmPropertyConstructor edmPropertyConstructor = recordExpression.FindProperty(propertyName);
			if (edmPropertyConstructor != null)
			{
				return AnnotationProcessor.ProcessCollectionPathExpressions(edmPropertyConstructor.Value);
			}
			return new HashSet<string>();
		}

		// Token: 0x06003B32 RID: 15154 RVA: 0x000C0D08 File Offset: 0x000BEF08
		private static bool? GetBooleanProperty(IEdmRecordExpression recordExpression, string propertyName)
		{
			IEdmPropertyConstructor edmPropertyConstructor = recordExpression.FindProperty(propertyName);
			if (edmPropertyConstructor != null)
			{
				return new bool?(AnnotationProcessor.ProcessBooleanExpressionValue(edmPropertyConstructor.Value));
			}
			return null;
		}

		// Token: 0x06003B33 RID: 15155 RVA: 0x000C0D3C File Offset: 0x000BEF3C
		private static T GetEnumPropertyOrDefault<T>(IEdmRecordExpression recordExpression, string propertyName, T defaultValue)
		{
			IEdmPropertyConstructor edmPropertyConstructor = recordExpression.FindProperty(propertyName);
			if (edmPropertyConstructor != null)
			{
				return AnnotationProcessor.ProcessEnumExpressionValue<T>(edmPropertyConstructor.Value, defaultValue);
			}
			return defaultValue;
		}

		// Token: 0x06003B34 RID: 15156 RVA: 0x000C0D64 File Offset: 0x000BEF64
		private static T ProcessEnumExpressionValue<T>(IEdmExpression expressionValue, T defaultEnum)
		{
			if (expressionValue.ExpressionKind == EdmExpressionKind.EnumMember)
			{
				IEdmEnumMemberExpression edmEnumMemberExpression = expressionValue as IEdmEnumMemberExpression;
				if (edmEnumMemberExpression.EnumMembers != null)
				{
					return (T)((object)Enum.Parse(typeof(T), edmEnumMemberExpression.EnumMembers.First<Microsoft.OData.Edm.IEdmEnumMember>().Name));
				}
			}
			return defaultEnum;
		}

		// Token: 0x06003B35 RID: 15157 RVA: 0x000C0DB0 File Offset: 0x000BEFB0
		private static HashSet<string> ProcessCollectionStringExpressionValue(IEdmExpression expressionValue)
		{
			HashSet<string> hashSet = new HashSet<string>();
			if (expressionValue.ExpressionKind == EdmExpressionKind.Collection)
			{
				foreach (string text in from s in (expressionValue as IEdmCollectionExpression).Elements.OfType<IEdmStringConstantExpression>()
					select s.Value)
				{
					hashSet.Add(text);
				}
			}
			return hashSet;
		}

		// Token: 0x06003B36 RID: 15158 RVA: 0x000C0E40 File Offset: 0x000BF040
		private static HashSet<string> ProcessCollectionPathExpressions(IEdmExpression expressionValue)
		{
			HashSet<string> hashSet = new HashSet<string>();
			if (expressionValue.ExpressionKind == EdmExpressionKind.Collection)
			{
				foreach (IEnumerable<string> enumerable in from s in (expressionValue as IEdmCollectionExpression).Elements.OfType<IEdmPathExpression>()
					select s.PathSegments)
				{
					hashSet.Add(enumerable.First<string>());
				}
			}
			return hashSet;
		}

		// Token: 0x06003B37 RID: 15159 RVA: 0x000C0ED4 File Offset: 0x000BF0D4
		private static string ProcessNavigationPathExpression(IEdmExpression expressionValue)
		{
			if (expressionValue.ExpressionKind == EdmExpressionKind.NavigationPropertyPath)
			{
				return (expressionValue as IEdmPathExpression).PathSegments.FirstOrDefault<string>();
			}
			return null;
		}

		// Token: 0x06003B38 RID: 15160 RVA: 0x000C0EF2 File Offset: 0x000BF0F2
		private static bool ProcessBooleanExpressionValue(IEdmExpression expressionValue)
		{
			return expressionValue.ExpressionKind == EdmExpressionKind.BooleanConstant && (expressionValue as IEdmBooleanConstantExpression).Value;
		}

		// Token: 0x06003B39 RID: 15161 RVA: 0x000C0F0C File Offset: 0x000BF10C
		private static Uri ProcessUriExpressionValue(IEdmExpression expressionValue)
		{
			if (expressionValue.ExpressionKind == EdmExpressionKind.StringConstant)
			{
				IEdmStringConstantExpression edmStringConstantExpression = expressionValue as IEdmStringConstantExpression;
				try
				{
					return new Uri(edmStringConstantExpression.Value, UriKind.RelativeOrAbsolute);
				}
				catch (UriFormatException ex)
				{
					throw new InvalidOperationException(ex.Message, ex);
				}
			}
			return null;
		}

		// Token: 0x04001EB9 RID: 7865
		private const string ResourcePathAnnotationTerm = "Org.OData.Core.V1.ResourcePath";

		// Token: 0x04001EBA RID: 7866
		private const string CapabilityNamespace = "Org.OData.Capabilities.V1";

		// Token: 0x04001EBB RID: 7867
		private const string ConformanceLevelAnnotationTermName = "Org.OData.Capabilities.V1.ConformanceLevel";

		// Token: 0x04001EBC RID: 7868
		private const string CountRestrictionsAnnotationTerm = "Org.OData.Capabilities.V1.CountRestrictions";

		// Token: 0x04001EBD RID: 7869
		private const string NavigationRestrictionsAnnotationTerm = "Org.OData.Capabilities.V1.NavigationRestrictions";

		// Token: 0x04001EBE RID: 7870
		private const string SortRestrictionsAnnotationTerm = "Org.OData.Capabilities.V1.SortRestrictions";

		// Token: 0x04001EBF RID: 7871
		private const string InsertRestrictionsAnnotationTerm = "Org.OData.Capabilities.V1.InsertRestrictions";

		// Token: 0x04001EC0 RID: 7872
		private const string UpdateRestrictionsAnnotationTerm = "Org.OData.Capabilities.V1.UpdateRestrictions";

		// Token: 0x04001EC1 RID: 7873
		private const string DeleteRestrictionsAnnotationTerm = "Org.OData.Capabilities.V1.DeleteRestrictions";

		// Token: 0x04001EC2 RID: 7874
		private const string IndexableKeyAnnotationTerm = "Org.OData.Capabilities.V1.IndexableByKey";

		// Token: 0x04001EC3 RID: 7875
		private const string TopSupportedAnnotationTerm = "Org.OData.Capabilities.V1.TopSupported";

		// Token: 0x04001EC4 RID: 7876
		private const string SkipSupportedAnnotationTerm = "Org.OData.Capabilities.V1.SkipSupported";

		// Token: 0x04001EC5 RID: 7877
		private const string BatchSupportedAnnotationTerm = "Org.OData.Capabilities.V1.BatchSupported";

		// Token: 0x04001EC6 RID: 7878
		private const string FilterFunctionsAnnotationTerm = "Org.OData.Capabilities.V1.FilterFunctions";

		// Token: 0x04001EC7 RID: 7879
		private const string FilterRestrictionsAnnotationTerm = "Org.OData.Capabilities.V1.FilterRestrictions";

		// Token: 0x04001EC8 RID: 7880
		private const string SupportedFormatsAnnotationTerm = "Org.OData.Capabilities.V1.SupportedFormats";

		// Token: 0x04001EC9 RID: 7881
		private const string ExpandRestrictionsAnnotationTerm = "Org.OData.Capabilities.V1.ExpandRestrictions";

		// Token: 0x04001ECA RID: 7882
		private const string SupportsCrossJoinAnnotationTerm = "Org.OData.Capabilities.V1.CrossJoinSupported";

		// Token: 0x04001ECB RID: 7883
		private const string SupportsBatchContinueOnErrorAnnotationTerm = "Org.OData.Capabilities.V1.BatchContinueOnErrorSupported";

		// Token: 0x04001ECC RID: 7884
		private const string ApplySupportedAnnotationTerm = "Org.OData.Aggregation.V1.ApplySupported";

		// Token: 0x04001ECD RID: 7885
		private const string DescriptionAnnotationTerm = "Description";

		// Token: 0x04001ECE RID: 7886
		private const string LongDescriptionAnnotationTerm = "LongDescription";
	}
}
