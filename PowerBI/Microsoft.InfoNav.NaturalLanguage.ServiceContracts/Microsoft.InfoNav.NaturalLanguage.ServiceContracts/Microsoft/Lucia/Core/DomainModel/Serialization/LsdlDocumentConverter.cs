using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x0200018C RID: 396
	public static class LsdlDocumentConverter
	{
		// Token: 0x060007B0 RID: 1968 RVA: 0x0000E7D0 File Offset: 0x0000C9D0
		public static LsdlDocument ToLsdlDocument(this ModelLinguisticSchema legacySchema)
		{
			LsdlDocument lsdlDocument = new LsdlDocument
			{
				Version = LsdlVersion.Latest,
				Language = LsdlDocumentConverter.ConvertLanguage(legacySchema.Language),
				DynamicImprovement = LsdlDocumentConverter.ConvertDynamicImprovement(legacySchema.DynamicImprovement)
			};
			LsdlDocumentConverter.ConvertNamespaces(legacySchema.SchemaReferences, lsdlDocument.Namespaces);
			LsdlDocumentConverter.ConvertEntities(legacySchema.Entities, lsdlDocument.Entities);
			return lsdlDocument;
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x0000E834 File Offset: 0x0000CA34
		public static ModelLinguisticSchema ToLegacySchema(this LsdlDocument schema, bool skipRoleReordering = false)
		{
			return new ModelLinguisticSchema
			{
				Language = LsdlDocumentConverter.ConvertLanguage(schema.Language),
				DynamicImprovement = LsdlDocumentConverter.ConvertDynamicImprovement(schema.DynamicImprovement),
				SchemaReferences = LsdlDocumentConverter.ConvertNamespaces(schema.Namespaces).ToList<ModelSchemaReference>(),
				Entities = LsdlDocumentConverter.ConvertEntities(schema.Entities).ToList<ModelLinguisticEntity>()
			};
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x0000E894 File Offset: 0x0000CA94
		public static Value ConvertValue(DataValue value)
		{
			Value value2;
			if (LsdlDocumentConverter.TryConvertValue(value, out value2))
			{
				return value2;
			}
			throw LsdlDocumentConverter.ConversionError.UnexpectedValue("value", value);
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x0000E8B8 File Offset: 0x0000CAB8
		public static bool TryConvertValue(DataValue value, out Value result)
		{
			if (value == null || value.Type == DataType.Null)
			{
				result = null;
				return true;
			}
			DataType type = value.Type;
			if (type <= DataType.Boolean)
			{
				if (type == DataType.Text)
				{
					result = new Value
					{
						Text = new ValueList<string> { ((DataValue<string>)value).Value }
					};
					return true;
				}
				if (type == DataType.Boolean)
				{
					result = new Value
					{
						Boolean = new ValueList<bool?>
						{
							new bool?(((DataValue<bool>)value).Value)
						}
					};
					return true;
				}
			}
			else
			{
				if (type == DataType.Number)
				{
					result = new Value
					{
						Number = new ValueList<Union<long, double>> { Convert.ToDouble(((DataValue<decimal>)value).Value) }
					};
					return true;
				}
				if (type == DataType.Integer)
				{
					result = new Value
					{
						Number = new ValueList<Union<long, double>> { ((DataValue<long>)value).Value }
					};
					return true;
				}
				if (type == DataType.Year)
				{
					DateItemValue dateItemValue = (DateItemValue)value;
					result = new Value
					{
						Number = new ValueList<Union<long, double>> { (long)dateItemValue.Value.Year.Value }
					};
					return true;
				}
			}
			result = null;
			return false;
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x0000E9F5 File Offset: 0x0000CBF5
		public static State ConvertState(LinguisticItemSource source)
		{
			switch (source)
			{
			case LinguisticItemSource.User:
				return State.Authored;
			case LinguisticItemSource.Generated:
				return State.Generated;
			case LinguisticItemSource.Deleted:
				return State.Deleted;
			case LinguisticItemSource.Suggested:
				return State.Suggested;
			default:
				throw LsdlDocumentConverter.ConversionError.UnexpectedValue("source", source);
			}
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x0000EA27 File Offset: 0x0000CC27
		public static SourceType ConvertSourceType(LinguisticItemSourceType sourceType)
		{
			switch (sourceType)
			{
			case LinguisticItemSourceType.Default:
				return SourceType.Default;
			case LinguisticItemSourceType.User:
				return SourceType.User;
			case LinguisticItemSourceType.Internal:
				return SourceType.Internal;
			case LinguisticItemSourceType.External:
				return SourceType.External;
			default:
				throw LsdlDocumentConverter.ConversionError.UnexpectedValue("sourceType", sourceType);
			}
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x0000EA59 File Offset: 0x0000CC59
		private static string ConvertLanguage(LanguageIdentifier language)
		{
			return language.ToLanguageName();
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x0000EA64 File Offset: 0x0000CC64
		private static LanguageIdentifier ConvertLanguage(string language)
		{
			LanguageIdentifier languageIdentifier;
			if (!LanguageIdentifierUtil.TryAsLanguageIdentifier(language, out languageIdentifier))
			{
				throw LsdlDocumentConverter.ConversionError.UnexpectedValue("language", language);
			}
			return languageIdentifier;
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x0000EA88 File Offset: 0x0000CC88
		private static LsdlDynamicImprovement ConvertDynamicImprovement(ModelDynamicImprovement dynamicImprovement)
		{
			switch (dynamicImprovement)
			{
			case ModelDynamicImprovement.Full:
				return LsdlDynamicImprovement.Full;
			case ModelDynamicImprovement.HighConfidence:
				return LsdlDynamicImprovement.HighConfidence;
			case ModelDynamicImprovement.None:
				return LsdlDynamicImprovement.None;
			case ModelDynamicImprovement.Default:
				return LsdlDynamicImprovement.Default;
			default:
				throw LsdlDocumentConverter.ConversionError.UnexpectedValue("dynamicImprovement", dynamicImprovement);
			}
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x0000EABA File Offset: 0x0000CCBA
		public static ModelDynamicImprovement ConvertDynamicImprovement(LsdlDynamicImprovement dynamicImprovement)
		{
			switch (dynamicImprovement)
			{
			case LsdlDynamicImprovement.Default:
				return ModelDynamicImprovement.Default;
			case LsdlDynamicImprovement.Full:
				return ModelDynamicImprovement.Full;
			case LsdlDynamicImprovement.HighConfidence:
				return ModelDynamicImprovement.HighConfidence;
			case LsdlDynamicImprovement.None:
				return ModelDynamicImprovement.None;
			default:
				throw LsdlDocumentConverter.ConversionError.UnexpectedValue("dynamicImprovement", dynamicImprovement);
			}
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x0000EAEC File Offset: 0x0000CCEC
		private static LsdlMinResultConfidence ConvertMinResultConfidence(ResultConfidenceLevel? minResultConfidence)
		{
			if (minResultConfidence == null)
			{
				return LsdlMinResultConfidence.Default;
			}
			if (minResultConfidence != null)
			{
				ResultConfidenceLevel valueOrDefault = minResultConfidence.GetValueOrDefault();
				if (valueOrDefault <= ResultConfidenceLevel.Medium)
				{
					if (valueOrDefault == ResultConfidenceLevel.Low)
					{
						return LsdlMinResultConfidence.Low;
					}
					if (valueOrDefault == ResultConfidenceLevel.Medium)
					{
						return LsdlMinResultConfidence.Medium;
					}
				}
				else
				{
					if (valueOrDefault == ResultConfidenceLevel.High)
					{
						return LsdlMinResultConfidence.High;
					}
					if (valueOrDefault == ResultConfidenceLevel.VeryHigh)
					{
						return LsdlMinResultConfidence.VeryHigh;
					}
				}
			}
			throw LsdlDocumentConverter.ConversionError.UnexpectedValue("minResultConfidence", minResultConfidence);
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x0000EB48 File Offset: 0x0000CD48
		public static ResultConfidenceLevel? ConvertMinResultConfidence(LsdlMinResultConfidence minResultConfidence)
		{
			switch (minResultConfidence)
			{
			case LsdlMinResultConfidence.Default:
				return null;
			case LsdlMinResultConfidence.VeryHigh:
				return new ResultConfidenceLevel?(ResultConfidenceLevel.VeryHigh);
			case LsdlMinResultConfidence.High:
				return new ResultConfidenceLevel?(ResultConfidenceLevel.High);
			case LsdlMinResultConfidence.Medium:
				return new ResultConfidenceLevel?(ResultConfidenceLevel.Medium);
			case LsdlMinResultConfidence.Low:
				return new ResultConfidenceLevel?(ResultConfidenceLevel.Low);
			default:
				throw LsdlDocumentConverter.ConversionError.UnexpectedValue("minResultConfidence", minResultConfidence);
			}
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x0000EBAC File Offset: 0x0000CDAC
		private static void ConvertNamespaces(List<ModelSchemaReference> schemaReferences, Dictionary<string, LsdlReference> target)
		{
			if (schemaReferences == null)
			{
				return;
			}
			foreach (ModelSchemaReference modelSchemaReference in schemaReferences)
			{
				target.Add(modelSchemaReference.Namespace, new LsdlReference());
			}
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x0000EC08 File Offset: 0x0000CE08
		private static IEnumerable<ModelSchemaReference> ConvertNamespaces(Dictionary<string, LsdlReference> namespaces)
		{
			foreach (KeyValuePair<string, LsdlReference> keyValuePair in namespaces)
			{
				yield return new ModelSchemaReference
				{
					Namespace = keyValuePair.Key
				};
			}
			Dictionary<string, LsdlReference>.Enumerator enumerator = default(Dictionary<string, LsdlReference>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x0000EC18 File Offset: 0x0000CE18
		private static LinguisticItemSource ConvertState(State state)
		{
			switch (state)
			{
			case State.Authored:
				return LinguisticItemSource.User;
			case State.Generated:
				return LinguisticItemSource.Generated;
			case State.Suggested:
				return LinguisticItemSource.Suggested;
			case State.Deleted:
				return LinguisticItemSource.Deleted;
			default:
				throw LsdlDocumentConverter.ConversionError.UnexpectedValue("state", state);
			}
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x0000EC4C File Offset: 0x0000CE4C
		private static EnumProperty<EntityVisibility> ConvertEntityVisibility(ModelLinguisticVisibility visibility)
		{
			return new EnumProperty<EntityVisibility>
			{
				State = LsdlDocumentConverter.ConvertPropertyState(visibility.State),
				Value = LsdlDocumentConverter.ConvertVisibility(visibility.Value)
			};
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x0000EC88 File Offset: 0x0000CE88
		private static ModelLinguisticVisibility ConvertEntityVisibility(EnumProperty<EntityVisibility> visibility)
		{
			return new ModelLinguisticVisibility
			{
				State = LsdlDocumentConverter.ConvertPropertyState(visibility.State),
				Value = LsdlDocumentConverter.ConvertVisibility(visibility.Value)
			};
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x0000ECC4 File Offset: 0x0000CEC4
		private static PropertyState ConvertPropertyState(PropertyState state)
		{
			switch (state)
			{
			case PropertyState.Default:
				return PropertyState.Default;
			case PropertyState.Authored:
				return PropertyState.Authored;
			case PropertyState.Generated:
				return PropertyState.Generated;
			case PropertyState.Suggested:
				return PropertyState.Suggested;
			default:
				throw LsdlDocumentConverter.ConversionError.UnexpectedValue("state", state);
			}
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x0000ECF6 File Offset: 0x0000CEF6
		private static PropertyState ConvertPropertyState(PropertyState source)
		{
			switch (source)
			{
			case PropertyState.Default:
				return PropertyState.Default;
			case PropertyState.Authored:
				return PropertyState.Authored;
			case PropertyState.Generated:
				return PropertyState.Generated;
			case PropertyState.Suggested:
				return PropertyState.Suggested;
			default:
				throw LsdlDocumentConverter.ConversionError.UnexpectedValue("source", source);
			}
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x0000ED28 File Offset: 0x0000CF28
		private static EntityVisibility ConvertVisibility(EntityVisibility visibility)
		{
			switch (visibility)
			{
			case EntityVisibility.Visible:
				return EntityVisibility.Visible;
			case EntityVisibility.Hidden:
				return EntityVisibility.Hidden;
			case EntityVisibility.Children:
				return EntityVisibility.Children;
			default:
				throw LsdlDocumentConverter.ConversionError.UnexpectedValue("visibility", visibility);
			}
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x0000ED54 File Offset: 0x0000CF54
		private static EntityVisibility ConvertVisibility(EntityVisibility visibility)
		{
			switch (visibility)
			{
			case EntityVisibility.Visible:
				return EntityVisibility.Visible;
			case EntityVisibility.Hidden:
				return EntityVisibility.Hidden;
			case EntityVisibility.Children:
				return EntityVisibility.Children;
			default:
				throw LsdlDocumentConverter.ConversionError.UnexpectedValue("visibility", visibility);
			}
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x0000ED80 File Offset: 0x0000CF80
		private static Source ConvertSource(LinguisticItemSourceType type, string agent)
		{
			return new Source
			{
				Type = LsdlDocumentConverter.ConvertSourceType(type),
				Agent = agent
			};
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x0000EDAC File Offset: 0x0000CFAC
		private static LinguisticItemSourceType ConvertSourceType(Source source)
		{
			switch (source.Type)
			{
			case SourceType.Default:
				return LinguisticItemSourceType.Default;
			case SourceType.User:
				return LinguisticItemSourceType.User;
			case SourceType.Internal:
				return LinguisticItemSourceType.Internal;
			case SourceType.External:
				return LinguisticItemSourceType.External;
			default:
				throw LsdlDocumentConverter.ConversionError.UnexpectedValue("Type", source.Type);
			}
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x0000EDF8 File Offset: 0x0000CFF8
		private static void ConvertEntities(List<ModelLinguisticEntity> entities, Dictionary<string, Entity> target)
		{
			if (entities == null)
			{
				return;
			}
			foreach (ModelLinguisticEntity modelLinguisticEntity in entities)
			{
				Entity entity = new Entity
				{
					Definition = LsdlDocumentConverter.ConvertEntityDefinition(modelLinguisticEntity),
					State = LsdlDocumentConverter.ConvertState(modelLinguisticEntity.Source),
					Visibility = LsdlDocumentConverter.ConvertEntityVisibility(modelLinguisticEntity.Visibility),
					Weight = modelLinguisticEntity.Weight
				};
				LsdlDocumentConverter.ConvertTerms(modelLinguisticEntity.Words, entity.Terms);
				target.Add(modelLinguisticEntity.Name, entity);
			}
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x0000EEA4 File Offset: 0x0000D0A4
		private static IEnumerable<ModelLinguisticEntity> ConvertEntities(Dictionary<string, Entity> entities)
		{
			foreach (KeyValuePair<string, Entity> keyValuePair in entities)
			{
				string text;
				Entity entity;
				keyValuePair.Deconstruct(out text, out entity);
				string text2 = text;
				Entity entity2 = entity;
				ModelLinguisticEntity modelLinguisticEntity = new ModelLinguisticEntity
				{
					Name = text2,
					Source = LsdlDocumentConverter.ConvertState(entity2.State),
					Visibility = LsdlDocumentConverter.ConvertEntityVisibility(entity2.Visibility),
					Weight = entity2.Weight,
					Words = LsdlDocumentConverter.ConvertTerms(entity2.Terms).ToList<Word>()
				};
				LsdlDocumentConverter.ConvertEntityDefinition(entity2.Definition, modelLinguisticEntity);
				yield return modelLinguisticEntity;
			}
			Dictionary<string, Entity>.Enumerator enumerator = default(Dictionary<string, Entity>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x0000EEB4 File Offset: 0x0000D0B4
		private static EntityDefinition ConvertEntityDefinition(ModelLinguisticEntity entity)
		{
			return new EntityDefinition(LsdlDocumentConverter.ConvertBinding(entity));
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x0000EEC1 File Offset: 0x0000D0C1
		private static Binding ConvertBinding(ModelLinguisticEntity entity)
		{
			return LsdlDocumentFactory.CreateBinding(entity.ConceptualEntity, entity.ConceptualProperty, entity.ConceptualHierarchy, entity.ConceptualHierarchyLevel, entity.ConceptualVariationSource, entity.ConceptualVariationSet);
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x0000EEEC File Offset: 0x0000D0EC
		private static void ConvertEntityDefinition(EntityDefinition entityDefinition, ModelLinguisticEntity targetEntity)
		{
			if (entityDefinition.Binding != null)
			{
				LsdlDocumentConverter.ConvertBinding(entityDefinition.Binding, targetEntity);
				return;
			}
			throw LsdlDocumentConverter.ConversionError.UnexpectedValue("entityDefinition", "Binding = null && Text = null");
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x0000EF14 File Offset: 0x0000D114
		private static void ConvertBinding(Binding binding, ModelLinguisticEntity targetEntity)
		{
			ConceptualEntityBinding conceptualEntityBinding = binding as ConceptualEntityBinding;
			if (conceptualEntityBinding != null)
			{
				targetEntity.ConceptualEntity = conceptualEntityBinding.ConceptualEntity;
				return;
			}
			ConceptualPropertyBinding conceptualPropertyBinding = binding as ConceptualPropertyBinding;
			if (conceptualPropertyBinding != null)
			{
				targetEntity.ConceptualEntity = conceptualPropertyBinding.ConceptualEntity;
				targetEntity.ConceptualVariationSource = conceptualPropertyBinding.VariationSource;
				targetEntity.ConceptualVariationSet = conceptualPropertyBinding.VariationSet;
				targetEntity.ConceptualProperty = conceptualPropertyBinding.ConceptualProperty;
				return;
			}
			HierarchyBinding hierarchyBinding = binding as HierarchyBinding;
			if (hierarchyBinding != null)
			{
				targetEntity.ConceptualEntity = hierarchyBinding.ConceptualEntity;
				targetEntity.ConceptualVariationSource = hierarchyBinding.VariationSource;
				targetEntity.ConceptualVariationSet = hierarchyBinding.VariationSet;
				targetEntity.ConceptualHierarchy = hierarchyBinding.Hierarchy;
				return;
			}
			HierarchyLevelBinding hierarchyLevelBinding = binding as HierarchyLevelBinding;
			if (hierarchyLevelBinding == null)
			{
				return;
			}
			targetEntity.ConceptualEntity = hierarchyLevelBinding.ConceptualEntity;
			targetEntity.ConceptualVariationSource = hierarchyLevelBinding.VariationSource;
			targetEntity.ConceptualVariationSet = hierarchyLevelBinding.VariationSet;
			targetEntity.ConceptualHierarchy = hierarchyLevelBinding.Hierarchy;
			targetEntity.ConceptualHierarchyLevel = hierarchyLevelBinding.HierarchyLevel;
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x0000EFF8 File Offset: 0x0000D1F8
		public static SemanticType ConvertSemanticType(EntitySemanticType? semanticType)
		{
			if (semanticType == null)
			{
				return SemanticType.Undefined;
			}
			switch (semanticType.GetValueOrDefault())
			{
			case EntitySemanticType.Person:
				return SemanticType.Person;
			case EntitySemanticType.Animate:
				return SemanticType.Animate;
			case EntitySemanticType.Inanimate:
				return SemanticType.Inanimate;
			case EntitySemanticType.Location:
				return SemanticType.Location;
			case EntitySemanticType.Time:
				return SemanticType.Time;
			case EntitySemanticType.Duration:
				return SemanticType.Duration;
			default:
				throw LsdlDocumentConverter.ConversionError.UnexpectedValue("semanticType", semanticType);
			}
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x0000F054 File Offset: 0x0000D254
		public static LinguisticNameType ConvertNameType(EntityNameType nameType)
		{
			switch (nameType)
			{
			case EntityNameType.None:
				return LinguisticNameType.None;
			case EntityNameType.Name:
				return LinguisticNameType.Name;
			case EntityNameType.Identifier:
				return LinguisticNameType.ID;
			default:
				throw LsdlDocumentConverter.ConversionError.UnexpectedValue("nameType", nameType);
			}
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x0000F080 File Offset: 0x0000D280
		public static InstanceIndex ConvertInstanceIndex(EntityInstanceIndex instanceIndex)
		{
			switch (instanceIndex)
			{
			case EntityInstanceIndex.Default:
				return InstanceIndex.Default;
			case EntityInstanceIndex.All:
				return InstanceIndex.All;
			case EntityInstanceIndex.None:
				return InstanceIndex.None;
			default:
				throw LsdlDocumentConverter.ConversionError.UnexpectedValue("instanceIndex", instanceIndex);
			}
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x0000F0AC File Offset: 0x0000D2AC
		public static InstancePluralNormalization ConvertInstancePluralNormalization(EntityInstancePluralNormalization pluralNormalization)
		{
			switch (pluralNormalization)
			{
			case EntityInstancePluralNormalization.Default:
				return InstancePluralNormalization.Default;
			case EntityInstancePluralNormalization.Normalized:
				return InstancePluralNormalization.Normalized;
			case EntityInstancePluralNormalization.None:
				return InstancePluralNormalization.None;
			default:
				throw LsdlDocumentConverter.ConversionError.UnexpectedValue("pluralNormalization", pluralNormalization);
			}
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x0000F0D8 File Offset: 0x0000D2D8
		private static void ConvertTerms(List<Word> words, List<Term> target)
		{
			if (words == null)
			{
				return;
			}
			foreach (Word word in words)
			{
				TermProperties termProperties = new TermProperties
				{
					Type = LsdlDocumentConverter.ConvertTermType(word.Type),
					State = LsdlDocumentConverter.ConvertState(word.Source),
					Source = LsdlDocumentConverter.ConvertSource(word.SourceType, word.SourceAgent),
					Weight = word.Weight,
					TemplateSchema = word.TemplateSchema,
					LastModified = word.LastModified
				};
				target.Add(new Term(word.Value, termProperties));
			}
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x0000F19C File Offset: 0x0000D39C
		private static IEnumerable<Word> ConvertTerms(List<Term> terms)
		{
			foreach (Term term in terms)
			{
				yield return new Word
				{
					Value = term.Value,
					Type = LsdlDocumentConverter.ConvertTermType(term.Properties.Type),
					Source = LsdlDocumentConverter.ConvertState(term.Properties.State),
					SourceType = LsdlDocumentConverter.ConvertSourceType(term.Properties.Source),
					SourceAgent = term.Properties.Source.Agent,
					Weight = term.Properties.Weight,
					TemplateSchema = term.Properties.TemplateSchema,
					LastModified = term.Properties.LastModified
				};
			}
			List<Term>.Enumerator enumerator = default(List<Term>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x0000F1AC File Offset: 0x0000D3AC
		private static TermPropertiesType? ConvertTermType(WordType type)
		{
			switch (type)
			{
			case WordType.None:
				return null;
			case WordType.Noun:
				return new TermPropertiesType?(TermPropertiesType.Noun);
			case WordType.Verb:
				return new TermPropertiesType?(TermPropertiesType.Verb);
			case WordType.Adjective:
				return new TermPropertiesType?(TermPropertiesType.Adjective);
			case WordType.Preposition:
				return new TermPropertiesType?(TermPropertiesType.Preposition);
			default:
				throw LsdlDocumentConverter.ConversionError.UnexpectedValue("type", type);
			}
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x0000F20C File Offset: 0x0000D40C
		private static WordType ConvertTermType(TermPropertiesType? type)
		{
			if (type != null)
			{
				switch (type.GetValueOrDefault())
				{
				case TermPropertiesType.Noun:
					return WordType.Noun;
				case TermPropertiesType.Verb:
					return WordType.Verb;
				case TermPropertiesType.Adjective:
					return WordType.Adjective;
				case TermPropertiesType.Preposition:
					return WordType.Preposition;
				}
			}
			return WordType.None;
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x0000F24C File Offset: 0x0000D44C
		public static LinguisticRelationshipConditionOperator ConvertOperator(ConditionOperator op)
		{
			switch (op)
			{
			case ConditionOperator.Equals:
				return LinguisticRelationshipConditionOperator.Equals;
			case ConditionOperator.NotEquals:
				return LinguisticRelationshipConditionOperator.NotEquals;
			case ConditionOperator.GreaterThan:
				return LinguisticRelationshipConditionOperator.GreaterThan;
			case ConditionOperator.LessThan:
				return LinguisticRelationshipConditionOperator.LessThan;
			case ConditionOperator.GreaterThanOrEquals:
				return LinguisticRelationshipConditionOperator.GreaterThanOrEquals;
			case ConditionOperator.LessThanOrEquals:
				return LinguisticRelationshipConditionOperator.LessThanOrEquals;
			case ConditionOperator.Contains:
				return LinguisticRelationshipConditionOperator.Contains;
			case ConditionOperator.NotContains:
				return LinguisticRelationshipConditionOperator.NotContains;
			case ConditionOperator.StartsWith:
				return LinguisticRelationshipConditionOperator.StartsWith;
			case ConditionOperator.NotStartsWith:
				return LinguisticRelationshipConditionOperator.NotStartsWith;
			default:
				throw LsdlDocumentConverter.ConversionError.UnexpectedValue("op", op);
			}
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x0000F2B0 File Offset: 0x0000D4B0
		public static LinguisticRelationshipConditionAggregation ConvertAggregation(Aggregation op)
		{
			switch (op)
			{
			case Aggregation.None:
				return LinguisticRelationshipConditionAggregation.None;
			case Aggregation.Sum:
				return LinguisticRelationshipConditionAggregation.Sum;
			case Aggregation.Average:
				return LinguisticRelationshipConditionAggregation.Average;
			case Aggregation.Count:
				return LinguisticRelationshipConditionAggregation.Count;
			case Aggregation.Min:
				return LinguisticRelationshipConditionAggregation.Min;
			case Aggregation.Max:
				return LinguisticRelationshipConditionAggregation.Max;
			case Aggregation.Median:
				return LinguisticRelationshipConditionAggregation.Median;
			case Aggregation.Variance:
				return LinguisticRelationshipConditionAggregation.Variance;
			case Aggregation.StandardDeviation:
				return LinguisticRelationshipConditionAggregation.StandardDeviation;
			default:
				throw LsdlDocumentConverter.ConversionError.UnexpectedValue("op", op);
			}
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x0000F30C File Offset: 0x0000D50C
		public static DataValue ConvertValue(Value value)
		{
			if (value == null)
			{
				return NullValue.Null;
			}
			if (value.Text != null)
			{
				string text = value.Text[0];
				if (text == null)
				{
					return NullValue.Null;
				}
				return new StringValue(text);
			}
			else if (value.Number != null)
			{
				Union<long, double> union = value.Number[0];
				if (union == null)
				{
					return NullValue.Null;
				}
				long num;
				if (union.TryAs(out num))
				{
					return new IntegerValue(num);
				}
				return new NumberValue<decimal>(Convert.ToDecimal((T2)union));
			}
			else
			{
				if (value.Boolean == null)
				{
					throw LsdlDocumentConverter.ConversionError.UnexpectedValue("value", value);
				}
				bool? flag = value.Boolean[0];
				if (flag == null)
				{
					return NullValue.Null;
				}
				if (!flag.Value)
				{
					return BooleanValue.False;
				}
				return BooleanValue.True;
			}
		}

		// Token: 0x02000224 RID: 548
		private static class ConversionError
		{
			// Token: 0x06000BB8 RID: 3000 RVA: 0x00017430 File Offset: 0x00015630
			internal static Exception UnexpectedValue(string property, object value)
			{
				return LsdlDocumentConverter.ConversionError.CreateException("Unexpected value '{0}' for {1}", new object[] { value, property });
			}

			// Token: 0x06000BB9 RID: 3001 RVA: 0x0001744A File Offset: 0x0001564A
			private static Exception CreateException(string message, params object[] args)
			{
				return new ArgumentException(StringUtil.FormatInvariant(message, args));
			}
		}
	}
}
