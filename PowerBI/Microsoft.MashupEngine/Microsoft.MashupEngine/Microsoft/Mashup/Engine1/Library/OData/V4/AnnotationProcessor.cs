using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Annotations;
using Microsoft.OData.Edm.Expressions;
using Microsoft.OData.Edm.Library;

namespace Microsoft.Mashup.Engine1.Library.OData.V4
{
	// Token: 0x02000838 RID: 2104
	internal static class AnnotationProcessor
	{
		// Token: 0x06003C86 RID: 15494 RVA: 0x000C461C File Offset: 0x000C281C
		public static RecordValue ProcessEntityContainer(Microsoft.OData.Edm.IEdmModel model, Microsoft.OData.Edm.IEdmEntityContainer container, Annotations output, ODataUserSettings userSettings)
		{
			List<NamedValue> displayAnnotations = new List<NamedValue> { AnnotationProcessor.GetDisplayNameAnnotation(container) };
			AnnotationProcessor.ProcessAnnotations(model, container, userSettings, delegate(string fullName, IEdmValueAnnotation edmValueAnnotation)
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
							output.PropertyRestrictions = AnnotationProcessor.BuildAggregatePropertyRestrictions(edmValueAnnotation.Value);
							output.AggregateTransformations = AnnotationProcessor.BuildAggregateTransformations(edmValueAnnotation.Value);
							return;
						}
						break;
					case 40:
						if (fullName == "Org.OData.Capabilities.V1.BatchSupported")
						{
							output.SupportsBatch = AnnotationProcessor.ProcessBooleanExpressionValue(edmValueAnnotation.Value);
							return;
						}
						break;
					case 41:
						if (fullName == "Org.OData.Capabilities.V1.FilterFunctions")
						{
							output.Filters = AnnotationProcessor.ProcessCollectionStringExpressionValue(edmValueAnnotation.Value);
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
									output.SupportedFormats = AnnotationProcessor.ProcessCollectionStringExpressionValue(edmValueAnnotation.Value);
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
							output.SupportsCrossJoin = AnnotationProcessor.ProcessBooleanExpressionValue(edmValueAnnotation.Value);
							return;
						}
						break;
					default:
						if (length == 55)
						{
							if (fullName == "Org.OData.Capabilities.V1.BatchContinueOnErrorSupported")
							{
								output.SupportsBatchContinueOnError = AnnotationProcessor.ProcessBooleanExpressionValue(edmValueAnnotation.Value);
								return;
							}
						}
						break;
					}
				}
				NamedValue namedValue;
				if (AnnotationProcessor.TryProcessDisplayAnnotations(edmValueAnnotation, userSettings, out namedValue))
				{
					displayAnnotations.Add(namedValue);
				}
			});
			return RecordValue.New(displayAnnotations.ToArray());
		}

		// Token: 0x06003C87 RID: 15495 RVA: 0x000C4680 File Offset: 0x000C2880
		public static Capabilities ProcessCapabilities(string displayName, Microsoft.OData.Edm.IEdmModel model, IEdmVocabularyAnnotatable annotatable, Annotations annotations, ODataUserSettings userSettings)
		{
			Capabilities capabilities = annotations.CreateDefaultCapabilities();
			capabilities.DisplayAnnotations.Add(AnnotationProcessor.GetDisplayNameAnnotation(displayName));
			HashSet<string> hashSet = new HashSet<string>();
			foreach (IEdmValueAnnotation edmValueAnnotation in model.FindVocabularyAnnotations(annotatable).OfType<IEdmValueAnnotation>())
			{
				string text = edmValueAnnotation.Term.FullName();
				if (!hashSet.Contains(text))
				{
					if (text == null)
					{
						goto IL_0318;
					}
					switch (text.Length)
					{
					case 38:
						if (!(text == "Org.OData.Capabilities.V1.TopSupported"))
						{
							goto IL_0318;
						}
						capabilities.SupportsTop = AnnotationProcessor.ProcessBooleanExpressionValue(edmValueAnnotation.Value);
						break;
					case 39:
					{
						char c = text[10];
						if (c != 'A')
						{
							if (c != 'C')
							{
								goto IL_0318;
							}
							if (!(text == "Org.OData.Capabilities.V1.SkipSupported"))
							{
								goto IL_0318;
							}
							capabilities.SupportsSkip = AnnotationProcessor.ProcessBooleanExpressionValue(edmValueAnnotation.Value);
						}
						else
						{
							if (!(text == "Org.OData.Aggregation.V1.ApplySupported"))
							{
								goto IL_0318;
							}
							capabilities.ApplySupported = true;
							capabilities.PropertyRestrictions = AnnotationProcessor.BuildAggregatePropertyRestrictions(edmValueAnnotation.Value);
							capabilities.AggregateTransformations = AnnotationProcessor.BuildAggregateTransformations(edmValueAnnotation.Value);
						}
						break;
					}
					case 40:
						if (!(text == "Org.OData.Capabilities.V1.IndexableByKey"))
						{
							goto IL_0318;
						}
						capabilities.IsIndexableByKey = AnnotationProcessor.ProcessBooleanExpressionValue(edmValueAnnotation.Value);
						break;
					case 41:
						if (!(text == "Org.OData.Capabilities.V1.FilterFunctions"))
						{
							goto IL_0318;
						}
						capabilities.FilterFunctions = AnnotationProcessor.ProcessCollectionStringExpressionValue(edmValueAnnotation.Value);
						break;
					case 42:
						if (!(text == "Org.OData.Capabilities.V1.SortRestrictions"))
						{
							goto IL_0318;
						}
						capabilities.BuildSortRestrictions(edmValueAnnotation.Value);
						break;
					case 43:
						if (!(text == "Org.OData.Capabilities.V1.CountRestrictions"))
						{
							goto IL_0318;
						}
						capabilities.BuildCountRestrictions(edmValueAnnotation.Value);
						break;
					case 44:
					{
						char c = text[26];
						switch (c)
						{
						case 'D':
							if (!(text == "Org.OData.Capabilities.V1.DeleteRestrictions"))
							{
								goto IL_0318;
							}
							capabilities.BuildDeleteRestrictions(edmValueAnnotation.Value);
							break;
						case 'E':
							if (!(text == "Org.OData.Capabilities.V1.ExpandRestrictions"))
							{
								goto IL_0318;
							}
							capabilities.BuildExpandRestrictions(edmValueAnnotation.Value);
							break;
						case 'F':
							if (!(text == "Org.OData.Capabilities.V1.FilterRestrictions"))
							{
								goto IL_0318;
							}
							capabilities.BuildFilterRestrictions(edmValueAnnotation.Value);
							break;
						case 'G':
						case 'H':
							goto IL_0318;
						case 'I':
							if (!(text == "Org.OData.Capabilities.V1.InsertRestrictions"))
							{
								goto IL_0318;
							}
							capabilities.BuildInsertRestrictions(edmValueAnnotation.Value);
							break;
						default:
							if (c != 'U')
							{
								goto IL_0318;
							}
							if (!(text == "Org.OData.Capabilities.V1.UpdateRestrictions"))
							{
								goto IL_0318;
							}
							capabilities.BuildUpdateRestrictions(edmValueAnnotation.Value);
							break;
						}
						break;
					}
					case 45:
					case 46:
					case 47:
						goto IL_0318;
					case 48:
						if (!(text == "Org.OData.Capabilities.V1.NavigationRestrictions"))
						{
							goto IL_0318;
						}
						capabilities.BuildNavigationRestrictions(edmValueAnnotation.Value);
						break;
					default:
						goto IL_0318;
					}
					IL_0331:
					hashSet.Add(text);
					continue;
					IL_0318:
					NamedValue namedValue;
					if (AnnotationProcessor.TryProcessDisplayAnnotations(edmValueAnnotation, userSettings, out namedValue))
					{
						capabilities.DisplayAnnotations.Add(namedValue);
						goto IL_0331;
					}
					goto IL_0331;
				}
			}
			return capabilities;
		}

		// Token: 0x06003C88 RID: 15496 RVA: 0x000C49FC File Offset: 0x000C2BFC
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

		// Token: 0x06003C89 RID: 15497 RVA: 0x000C4B78 File Offset: 0x000C2D78
		public static List<NamedValue> GetPropertyDisplayAnnotations(Microsoft.OData.Edm.IEdmModel model, Microsoft.OData.Edm.IEdmProperty property, ODataUserSettings userSettings)
		{
			Value description = null;
			Value longDescription = null;
			AnnotationProcessor.ProcessAnnotations(model, property, userSettings, delegate(string fullName, IEdmValueAnnotation edmValueAnnotation)
			{
				if (description != null)
				{
					return;
				}
				NamedValue namedValue;
				if (AnnotationProcessor.TryProcessDisplayAnnotations(edmValueAnnotation, userSettings, out namedValue))
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

		// Token: 0x06003C8A RID: 15498 RVA: 0x000C4BF4 File Offset: 0x000C2DF4
		public static List<NamedValue> GetStructuredTypeAnnotations(Microsoft.OData.Edm.IEdmModel model, Microsoft.OData.Edm.IEdmType edmType, ODataUserSettings userSettings)
		{
			List<NamedValue> typeAnnotations = new List<NamedValue>();
			IEdmVocabularyAnnotatable edmVocabularyAnnotatable = edmType as IEdmVocabularyAnnotatable;
			if (edmVocabularyAnnotatable != null)
			{
				AnnotationProcessor.ProcessAnnotations(model, edmVocabularyAnnotatable, userSettings, delegate(string fullName, IEdmValueAnnotation edmValueAnnotation)
				{
					NamedValue namedValue;
					if (AnnotationProcessor.TryProcessDisplayAnnotations(edmValueAnnotation, userSettings, out namedValue))
					{
						typeAnnotations.Add(namedValue);
					}
				});
			}
			return typeAnnotations;
		}

		// Token: 0x06003C8B RID: 15499 RVA: 0x000C4C44 File Offset: 0x000C2E44
		public static List<NamedValue> GetPropertyAnnotations(Microsoft.OData.Edm.IEdmModel model, Microsoft.OData.Edm.IEdmProperty property, ODataUserSettings userSettings)
		{
			List<NamedValue> propertyAnnotations = new List<NamedValue>();
			AnnotationProcessor.ProcessAnnotations(model, property, userSettings, delegate(string fullName, IEdmValueAnnotation edmValueAnnotation)
			{
				NamedValue namedValue;
				if (AnnotationProcessor.TryProcessDisplayAnnotations(edmValueAnnotation, userSettings, out namedValue))
				{
					propertyAnnotations.Add(namedValue);
				}
			});
			return propertyAnnotations;
		}

		// Token: 0x06003C8C RID: 15500 RVA: 0x000C4C88 File Offset: 0x000C2E88
		public static List<NamedValue> GetParameterDisplayAnnotations(Microsoft.OData.Edm.IEdmModel model, Microsoft.OData.Edm.IEdmOperationParameter parameter, ODataUserSettings userSettings)
		{
			List<NamedValue> displayAnnotations = new List<NamedValue> { AnnotationProcessor.GetDisplayNameAnnotation(parameter) };
			AnnotationProcessor.ProcessAnnotations(model, parameter, userSettings, delegate(string fullName, IEdmValueAnnotation edmValueAnnotation)
			{
				NamedValue namedValue;
				if (AnnotationProcessor.TryProcessDisplayAnnotations(edmValueAnnotation, userSettings, out namedValue))
				{
					displayAnnotations.Add(namedValue);
				}
			});
			return displayAnnotations;
		}

		// Token: 0x06003C8D RID: 15501 RVA: 0x000C4CD8 File Offset: 0x000C2ED8
		private static void ProcessAnnotations(Microsoft.OData.Edm.IEdmModel model, IEdmVocabularyAnnotatable vocabularyAnnotatable, ODataUserSettings userSettings, Action<string, IEdmValueAnnotation> processAction)
		{
			HashSet<string> hashSet = new HashSet<string>();
			foreach (IEdmValueAnnotation edmValueAnnotation in model.FindVocabularyAnnotations(vocabularyAnnotatable).OfType<IEdmValueAnnotation>())
			{
				string text = edmValueAnnotation.Term.FullName();
				if (!hashSet.Contains(text))
				{
					processAction(text, edmValueAnnotation);
					hashSet.Add(text);
				}
			}
		}

		// Token: 0x06003C8E RID: 15502 RVA: 0x000C4D50 File Offset: 0x000C2F50
		private static NamedValue GetDisplayNameAnnotation(Microsoft.OData.Edm.IEdmNamedElement namedElement)
		{
			return AnnotationProcessor.GetDisplayNameAnnotation(namedElement.Name);
		}

		// Token: 0x06003C8F RID: 15503 RVA: 0x000C0611 File Offset: 0x000BE811
		private static NamedValue GetDisplayNameAnnotation(string elementName)
		{
			return new NamedValue("Documentation.Caption", TextValue.New(elementName));
		}

		// Token: 0x06003C90 RID: 15504 RVA: 0x000C4D60 File Offset: 0x000C2F60
		private static bool TryProcessDisplayAnnotations(IEdmValueAnnotation valueAnnotation, ODataUserSettings userSettings, out NamedValue displayAnnotation)
		{
			if (userSettings.IncludeMetadataAnnotations != null)
			{
				string text = valueAnnotation.Term.FullName();
				if (AnnotationFilter.Matches(userSettings.IncludeMetadataAnnotations, text))
				{
					Value value = AnnotationProcessor.ToValueFromODataExpression(valueAnnotation.Value);
					if (value != Value.Null)
					{
						displayAnnotation = new NamedValue(text, value);
						return true;
					}
				}
				displayAnnotation = default(NamedValue);
				return false;
			}
			if (valueAnnotation.Term.Name.Equals("Description") && valueAnnotation.Value.ExpressionKind == Microsoft.OData.Edm.Expressions.EdmExpressionKind.StringConstant)
			{
				string value2 = ((IEdmStringConstantExpression)valueAnnotation.Value).Value;
				if (!string.IsNullOrEmpty(value2))
				{
					displayAnnotation = new NamedValue("Documentation.Description", TextValue.New(value2));
					return true;
				}
			}
			if (valueAnnotation.Term.Name.Equals("LongDescription") && valueAnnotation.Value.ExpressionKind == Microsoft.OData.Edm.Expressions.EdmExpressionKind.StringConstant)
			{
				string value3 = ((IEdmStringConstantExpression)valueAnnotation.Value).Value;
				if (!string.IsNullOrEmpty(value3))
				{
					displayAnnotation = new NamedValue("Documentation.LongDescription", TextValue.New(value3));
					return true;
				}
			}
			displayAnnotation = default(NamedValue);
			return false;
		}

		// Token: 0x06003C91 RID: 15505 RVA: 0x000C4E70 File Offset: 0x000C3070
		private static Value ToValueFromODataExpression(Microsoft.OData.Edm.Expressions.IEdmExpression expression)
		{
			if (expression == null)
			{
				return Value.Null;
			}
			Microsoft.OData.Edm.Expressions.EdmExpressionKind expressionKind = expression.ExpressionKind;
			switch (expressionKind)
			{
			case Microsoft.OData.Edm.Expressions.EdmExpressionKind.BinaryConstant:
				return BinaryValue.New(((IEdmBinaryConstantExpression)expression).Value);
			case Microsoft.OData.Edm.Expressions.EdmExpressionKind.BooleanConstant:
				return LogicalValue.New(((IEdmBooleanConstantExpression)expression).Value);
			case Microsoft.OData.Edm.Expressions.EdmExpressionKind.DateTimeOffsetConstant:
				return DateTimeZoneValue.New(((IEdmDateTimeOffsetConstantExpression)expression).Value);
			case Microsoft.OData.Edm.Expressions.EdmExpressionKind.DecimalConstant:
				return NumberValue.New(((IEdmDecimalConstantExpression)expression).Value);
			case Microsoft.OData.Edm.Expressions.EdmExpressionKind.FloatingConstant:
				return NumberValue.New(((IEdmFloatingConstantExpression)expression).Value);
			case Microsoft.OData.Edm.Expressions.EdmExpressionKind.GuidConstant:
				return TextValue.New(((IEdmGuidConstantExpression)expression).Value.ToString());
			case Microsoft.OData.Edm.Expressions.EdmExpressionKind.IntegerConstant:
				return NumberValue.New(((IEdmIntegerConstantExpression)expression).Value);
			case Microsoft.OData.Edm.Expressions.EdmExpressionKind.StringConstant:
				return TextValue.New(((IEdmStringConstantExpression)expression).Value);
			case Microsoft.OData.Edm.Expressions.EdmExpressionKind.DurationConstant:
				return DurationValue.New(((IEdmDurationConstantExpression)expression).Value);
			default:
				switch (expressionKind)
				{
				case Microsoft.OData.Edm.Expressions.EdmExpressionKind.DateConstant:
				{
					Microsoft.OData.Edm.Library.Date value = ((IEdmDateConstantExpression)expression).Value;
					return DateValue.New(value.Year, value.Month, value.Day);
				}
				case Microsoft.OData.Edm.Expressions.EdmExpressionKind.TimeOfDayConstant:
					return TimeValue.New(((IEdmTimeOfDayConstantExpression)expression).Value.Ticks);
				case Microsoft.OData.Edm.Expressions.EdmExpressionKind.EnumMember:
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

		// Token: 0x06003C92 RID: 15506 RVA: 0x000C505C File Offset: 0x000C325C
		private static void BuildExpandRestrictions(this Capabilities capability, Microsoft.OData.Edm.Expressions.IEdmExpression expressionValue)
		{
			if (expressionValue.ExpressionKind == Microsoft.OData.Edm.Expressions.EdmExpressionKind.Record)
			{
				IEdmRecordExpression edmRecordExpression = expressionValue as IEdmRecordExpression;
				capability.SupportsExpand = AnnotationProcessor.GetBooleanProperty(edmRecordExpression, "Expandable") ?? capability.SupportsExpand;
				capability.NonExpandableProperties = AnnotationProcessor.GetPropertyCollection(edmRecordExpression, "NonExpandableProperties");
			}
		}

		// Token: 0x06003C93 RID: 15507 RVA: 0x000C50B8 File Offset: 0x000C32B8
		private static void BuildFilterRestrictions(this Capabilities capability, Microsoft.OData.Edm.Expressions.IEdmExpression expressionValue)
		{
			if (expressionValue.ExpressionKind == Microsoft.OData.Edm.Expressions.EdmExpressionKind.Record)
			{
				IEdmRecordExpression edmRecordExpression = expressionValue as IEdmRecordExpression;
				capability.SupportsFilter = AnnotationProcessor.GetBooleanProperty(edmRecordExpression, "Filterable") ?? capability.SupportsFilter;
				capability.NonFilterableProperties = AnnotationProcessor.GetPropertyCollection(edmRecordExpression, "NonFilterableProperties");
				capability.RequiredPropertiesInFilter = AnnotationProcessor.GetPropertyCollection(edmRecordExpression, "RequiredProperties");
				capability.RequiresFilter = AnnotationProcessor.GetBooleanProperty(edmRecordExpression, "RequiresFilter") ?? capability.RequiresFilter;
			}
		}

		// Token: 0x06003C94 RID: 15508 RVA: 0x000C514C File Offset: 0x000C334C
		private static void BuildCountRestrictions(this Capabilities capability, Microsoft.OData.Edm.Expressions.IEdmExpression expressionValue)
		{
			if (expressionValue.ExpressionKind == Microsoft.OData.Edm.Expressions.EdmExpressionKind.Record)
			{
				IEdmRecordExpression edmRecordExpression = expressionValue as IEdmRecordExpression;
				capability.SupportsCount = AnnotationProcessor.GetBooleanProperty(edmRecordExpression, "Countable") ?? capability.SupportsCount;
				capability.NonCountableProperties = AnnotationProcessor.GetPropertyCollection(edmRecordExpression, "NonCountableProperties");
				capability.NonCountableNavigationProperties = AnnotationProcessor.GetPropertyCollection(edmRecordExpression, "NonCountableNavigationProperties");
			}
		}

		// Token: 0x06003C95 RID: 15509 RVA: 0x000C51B8 File Offset: 0x000C33B8
		private static Dictionary<string, NavigationType> GetRestrictedNavigationProperties(IEdmRecordExpression recordExpressionValue)
		{
			IEdmPropertyConstructor edmPropertyConstructor = recordExpressionValue.FindProperty("RestrictedProperties");
			Dictionary<string, NavigationType> dictionary = new Dictionary<string, NavigationType>();
			if (edmPropertyConstructor != null && edmPropertyConstructor.Value.ExpressionKind == Microsoft.OData.Edm.Expressions.EdmExpressionKind.Collection)
			{
				foreach (Microsoft.OData.Edm.Expressions.IEdmExpression edmExpression in (edmPropertyConstructor.Value as IEdmCollectionExpression).Elements)
				{
					if (edmExpression.ExpressionKind == Microsoft.OData.Edm.Expressions.EdmExpressionKind.Record)
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

		// Token: 0x06003C96 RID: 15510 RVA: 0x000C5288 File Offset: 0x000C3488
		private static void BuildNavigationRestrictions(this Capabilities capability, Microsoft.OData.Edm.Expressions.IEdmExpression expressionValue)
		{
			if (expressionValue.ExpressionKind == Microsoft.OData.Edm.Expressions.EdmExpressionKind.Record)
			{
				IEdmRecordExpression edmRecordExpression = expressionValue as IEdmRecordExpression;
				capability.Navigability = AnnotationProcessor.GetEnumPropertyOrDefault<NavigationType>(edmRecordExpression, "Navigability", NavigationType.None);
				capability.RestrictedNavigationProperties = AnnotationProcessor.GetRestrictedNavigationProperties(edmRecordExpression);
			}
		}

		// Token: 0x06003C97 RID: 15511 RVA: 0x000C52C4 File Offset: 0x000C34C4
		private static void BuildInsertRestrictions(this Capabilities capability, Microsoft.OData.Edm.Expressions.IEdmExpression expressionValue)
		{
			if (expressionValue.ExpressionKind == Microsoft.OData.Edm.Expressions.EdmExpressionKind.Record)
			{
				bool? booleanProperty = AnnotationProcessor.GetBooleanProperty(expressionValue as IEdmRecordExpression, "Insertable");
				if (booleanProperty != null)
				{
					capability.IsInsertable = booleanProperty.Value;
				}
			}
		}

		// Token: 0x06003C98 RID: 15512 RVA: 0x000C5304 File Offset: 0x000C3504
		private static void BuildUpdateRestrictions(this Capabilities capability, Microsoft.OData.Edm.Expressions.IEdmExpression expressionValue)
		{
			if (expressionValue.ExpressionKind == Microsoft.OData.Edm.Expressions.EdmExpressionKind.Record)
			{
				bool? booleanProperty = AnnotationProcessor.GetBooleanProperty(expressionValue as IEdmRecordExpression, "Updatable");
				if (booleanProperty != null)
				{
					capability.IsUpdatable = booleanProperty.Value;
				}
			}
		}

		// Token: 0x06003C99 RID: 15513 RVA: 0x000C5344 File Offset: 0x000C3544
		private static void BuildDeleteRestrictions(this Capabilities capability, Microsoft.OData.Edm.Expressions.IEdmExpression expressionValue)
		{
			if (expressionValue.ExpressionKind == Microsoft.OData.Edm.Expressions.EdmExpressionKind.Record)
			{
				bool? booleanProperty = AnnotationProcessor.GetBooleanProperty(expressionValue as IEdmRecordExpression, "Deletable");
				if (booleanProperty != null)
				{
					capability.IsDeletable = booleanProperty.Value;
				}
			}
		}

		// Token: 0x06003C9A RID: 15514 RVA: 0x000C5384 File Offset: 0x000C3584
		private static void BuildSortRestrictions(this Capabilities capability, Microsoft.OData.Edm.Expressions.IEdmExpression expressionValue)
		{
			if (expressionValue.ExpressionKind == Microsoft.OData.Edm.Expressions.EdmExpressionKind.Record)
			{
				IEdmRecordExpression edmRecordExpression = expressionValue as IEdmRecordExpression;
				capability.SupportsSort = AnnotationProcessor.GetBooleanProperty(edmRecordExpression, "Sortable") ?? capability.SupportsSort;
				capability.AscendingOnlyProperties = AnnotationProcessor.GetPropertyCollection(edmRecordExpression, "AscendingOnlyProperties");
				capability.DescendingOnlyProperties = AnnotationProcessor.GetPropertyCollection(edmRecordExpression, "DescendingOnlyProperties");
				capability.NonSortableProperties = AnnotationProcessor.GetPropertyCollection(edmRecordExpression, "NonSortableProperties");
			}
		}

		// Token: 0x06003C9B RID: 15515 RVA: 0x000C5400 File Offset: 0x000C3600
		private static bool BuildAggregatePropertyRestrictions(Microsoft.OData.Edm.Expressions.IEdmExpression expressionValue)
		{
			return expressionValue.ExpressionKind == Microsoft.OData.Edm.Expressions.EdmExpressionKind.Record && AnnotationProcessor.GetBooleanProperty(expressionValue as IEdmRecordExpression, "PropertyRestrictions").GetValueOrDefault();
		}

		// Token: 0x06003C9C RID: 15516 RVA: 0x000C5431 File Offset: 0x000C3631
		private static HashSet<string> BuildAggregateTransformations(Microsoft.OData.Edm.Expressions.IEdmExpression expressionValue)
		{
			if (expressionValue.ExpressionKind == Microsoft.OData.Edm.Expressions.EdmExpressionKind.Record)
			{
				return AnnotationProcessor.GetPropertyCollection(expressionValue as IEdmRecordExpression, "Transformations");
			}
			return new HashSet<string>();
		}

		// Token: 0x06003C9D RID: 15517 RVA: 0x000C5454 File Offset: 0x000C3654
		private static HashSet<string> GetPropertyCollection(IEdmRecordExpression recordExpression, string propertyName)
		{
			IEdmPropertyConstructor edmPropertyConstructor = recordExpression.FindProperty(propertyName);
			if (edmPropertyConstructor != null)
			{
				return AnnotationProcessor.ProcessCollectionPathExpressions(edmPropertyConstructor.Value);
			}
			return new HashSet<string>();
		}

		// Token: 0x06003C9E RID: 15518 RVA: 0x000C5480 File Offset: 0x000C3680
		private static bool? GetBooleanProperty(IEdmRecordExpression recordExpression, string propertyName)
		{
			IEdmPropertyConstructor edmPropertyConstructor = recordExpression.FindProperty(propertyName);
			if (edmPropertyConstructor != null)
			{
				return new bool?(AnnotationProcessor.ProcessBooleanExpressionValue(edmPropertyConstructor.Value));
			}
			return null;
		}

		// Token: 0x06003C9F RID: 15519 RVA: 0x000C54B4 File Offset: 0x000C36B4
		private static T GetEnumPropertyOrDefault<T>(IEdmRecordExpression recordExpression, string propertyName, T defaultValue)
		{
			IEdmPropertyConstructor edmPropertyConstructor = recordExpression.FindProperty(propertyName);
			if (edmPropertyConstructor != null)
			{
				return AnnotationProcessor.ProcessEnumExpressionValue<T>(edmPropertyConstructor.Value, defaultValue);
			}
			return defaultValue;
		}

		// Token: 0x06003CA0 RID: 15520 RVA: 0x000C54DC File Offset: 0x000C36DC
		private static T ProcessEnumExpressionValue<T>(Microsoft.OData.Edm.Expressions.IEdmExpression expressionValue, T defaultEnum)
		{
			if (expressionValue.ExpressionKind == Microsoft.OData.Edm.Expressions.EdmExpressionKind.EnumMember)
			{
				IEdmEnumMemberExpression edmEnumMemberExpression = expressionValue as IEdmEnumMemberExpression;
				if (edmEnumMemberExpression.EnumMembers != null)
				{
					return (T)((object)Enum.Parse(typeof(T), edmEnumMemberExpression.EnumMembers.First<Microsoft.OData.Edm.IEdmEnumMember>().Name));
				}
			}
			return defaultEnum;
		}

		// Token: 0x06003CA1 RID: 15521 RVA: 0x000C5528 File Offset: 0x000C3728
		private static HashSet<string> ProcessCollectionStringExpressionValue(Microsoft.OData.Edm.Expressions.IEdmExpression expressionValue)
		{
			HashSet<string> hashSet = new HashSet<string>();
			if (expressionValue.ExpressionKind == Microsoft.OData.Edm.Expressions.EdmExpressionKind.Collection)
			{
				foreach (string text in from s in (expressionValue as IEdmCollectionExpression).Elements.OfType<IEdmStringConstantExpression>()
					select s.Value)
				{
					hashSet.Add(text);
				}
			}
			return hashSet;
		}

		// Token: 0x06003CA2 RID: 15522 RVA: 0x000C55B8 File Offset: 0x000C37B8
		private static HashSet<string> ProcessCollectionPathExpressions(Microsoft.OData.Edm.Expressions.IEdmExpression expressionValue)
		{
			HashSet<string> hashSet = new HashSet<string>();
			if (expressionValue.ExpressionKind == Microsoft.OData.Edm.Expressions.EdmExpressionKind.Collection)
			{
				foreach (IEnumerable<string> enumerable in from s in (expressionValue as IEdmCollectionExpression).Elements.OfType<Microsoft.OData.Edm.Expressions.IEdmPathExpression>()
					select s.Path)
				{
					hashSet.Add(enumerable.First<string>());
				}
			}
			return hashSet;
		}

		// Token: 0x06003CA3 RID: 15523 RVA: 0x000C564C File Offset: 0x000C384C
		private static string ProcessNavigationPathExpression(Microsoft.OData.Edm.Expressions.IEdmExpression expressionValue)
		{
			if (expressionValue.ExpressionKind == Microsoft.OData.Edm.Expressions.EdmExpressionKind.NavigationPropertyPath)
			{
				return (expressionValue as Microsoft.OData.Edm.Expressions.IEdmPathExpression).Path.FirstOrDefault<string>();
			}
			return null;
		}

		// Token: 0x06003CA4 RID: 15524 RVA: 0x000C566A File Offset: 0x000C386A
		private static bool ProcessBooleanExpressionValue(Microsoft.OData.Edm.Expressions.IEdmExpression expressionValue)
		{
			return expressionValue.ExpressionKind == Microsoft.OData.Edm.Expressions.EdmExpressionKind.BooleanConstant && (expressionValue as IEdmBooleanConstantExpression).Value;
		}

		// Token: 0x04001FA4 RID: 8100
		private const string CapabilityNamespace = "Org.OData.Capabilities.V1";

		// Token: 0x04001FA5 RID: 8101
		private const string ConformanceLevelAnnotationTermName = "Org.OData.Capabilities.V1.ConformanceLevel";

		// Token: 0x04001FA6 RID: 8102
		private const string CountRestrictionsAnnotationTerm = "Org.OData.Capabilities.V1.CountRestrictions";

		// Token: 0x04001FA7 RID: 8103
		private const string NavigationRestrictionsAnnotationTerm = "Org.OData.Capabilities.V1.NavigationRestrictions";

		// Token: 0x04001FA8 RID: 8104
		private const string SortRestrictionsAnnotationTerm = "Org.OData.Capabilities.V1.SortRestrictions";

		// Token: 0x04001FA9 RID: 8105
		private const string InsertRestrictionsAnnotationTerm = "Org.OData.Capabilities.V1.InsertRestrictions";

		// Token: 0x04001FAA RID: 8106
		private const string UpdateRestrictionsAnnotationTerm = "Org.OData.Capabilities.V1.UpdateRestrictions";

		// Token: 0x04001FAB RID: 8107
		private const string DeleteRestrictionsAnnotationTerm = "Org.OData.Capabilities.V1.DeleteRestrictions";

		// Token: 0x04001FAC RID: 8108
		private const string IndexableKeyAnnotationTerm = "Org.OData.Capabilities.V1.IndexableByKey";

		// Token: 0x04001FAD RID: 8109
		private const string TopSupportedAnnotationTerm = "Org.OData.Capabilities.V1.TopSupported";

		// Token: 0x04001FAE RID: 8110
		private const string SkipSupportedAnnotationTerm = "Org.OData.Capabilities.V1.SkipSupported";

		// Token: 0x04001FAF RID: 8111
		private const string BatchSupportedAnnotationTerm = "Org.OData.Capabilities.V1.BatchSupported";

		// Token: 0x04001FB0 RID: 8112
		private const string FilterFunctionsAnnotationTerm = "Org.OData.Capabilities.V1.FilterFunctions";

		// Token: 0x04001FB1 RID: 8113
		private const string FilterRestrictionsAnnotationTerm = "Org.OData.Capabilities.V1.FilterRestrictions";

		// Token: 0x04001FB2 RID: 8114
		private const string SupportedFormatsAnnotationTerm = "Org.OData.Capabilities.V1.SupportedFormats";

		// Token: 0x04001FB3 RID: 8115
		private const string ExpandRestrictionsAnnotationTerm = "Org.OData.Capabilities.V1.ExpandRestrictions";

		// Token: 0x04001FB4 RID: 8116
		private const string SupportsCrossJoinAnnotationTerm = "Org.OData.Capabilities.V1.CrossJoinSupported";

		// Token: 0x04001FB5 RID: 8117
		private const string SupportsBatchContinueOnErrorAnnotationTerm = "Org.OData.Capabilities.V1.BatchContinueOnErrorSupported";

		// Token: 0x04001FB6 RID: 8118
		private const string ApplySupportedAnnotationTerm = "Org.OData.Aggregation.V1.ApplySupported";

		// Token: 0x04001FB7 RID: 8119
		private const string DescriptionAnnotationTerm = "Description";

		// Token: 0x04001FB8 RID: 8120
		private const string LongDescriptionAnnotationTerm = "LongDescription";
	}
}
