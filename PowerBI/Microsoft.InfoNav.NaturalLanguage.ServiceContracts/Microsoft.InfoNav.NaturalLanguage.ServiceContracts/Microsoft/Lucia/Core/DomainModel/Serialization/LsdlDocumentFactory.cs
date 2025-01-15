using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x0200018D RID: 397
	public static class LsdlDocumentFactory
	{
		// Token: 0x060007D8 RID: 2008 RVA: 0x0000F3CC File Offset: 0x0000D5CC
		public static LsdlDocument CreateLsdlDocument(Version version = null, string language = null, LsdlDynamicImprovement dynamicImprovement = LsdlDynamicImprovement.Default, IEnumerable<KeyValuePair<string, Entity>> entities = null, IEnumerable<KeyValuePair<string, Relationship>> relationships = null, IEnumerable<KeyValuePair<string, LsdlReference>> namespaces = null, IEnumerable<GlobalSubstitution> globalSubstitutions = null, IEnumerable<Example> examples = null, IEnumerable<KeyValuePair<string, AgentProperties>> agents = null)
		{
			return new LsdlDocument
			{
				Version = (version ?? LsdlVersion.Latest),
				DynamicImprovement = dynamicImprovement,
				Language = (language ?? LanguageIdentifier.en_US.ToLanguageName()),
				Entities = { entities.EmptyIfNull<KeyValuePair<string, Entity>>() },
				Relationships = { relationships.EmptyIfNull<KeyValuePair<string, Relationship>>() },
				Namespaces = { namespaces.EmptyIfNull<KeyValuePair<string, LsdlReference>>() },
				GlobalSubstitutions = { globalSubstitutions.EmptyIfNull<GlobalSubstitution>() },
				Examples = { examples.EmptyIfNull<Example>() },
				Agents = { agents.EmptyIfNull<KeyValuePair<string, AgentProperties>>() }
			};
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x0000F478 File Offset: 0x0000D678
		public static Entity CreateEntity(EntityDefinition definition, State state = State.Generated, EnumProperty<EntityVisibility> visibility = default(EnumProperty<EntityVisibility>), double weight = 1.0, string templateSchema = null, EntitySemanticType? semanticType = null, EntityNameType nameType = EntityNameType.None, Instances instances = null, IEnumerable<Term> terms = null, IEnumerable<string> units = null)
		{
			Entity entity = new Entity
			{
				Definition = definition,
				State = state,
				Visibility = visibility,
				Weight = weight,
				TemplateSchema = templateSchema,
				SemanticType = semanticType,
				NameType = nameType,
				Instances = instances
			};
			if (terms != null)
			{
				entity.Terms.AddRange(terms);
			}
			if (units != null)
			{
				entity.Units.AddRange(units);
			}
			return entity;
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x0000F4EA File Offset: 0x0000D6EA
		public static EntityDefinition CreateEntityDefinition(string textDefinition)
		{
			return new EntityDefinition(textDefinition);
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x0000F4F2 File Offset: 0x0000D6F2
		public static EntityDefinition CreateEntityDefinition(Binding binding)
		{
			return new EntityDefinition(binding);
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x0000F4FC File Offset: 0x0000D6FC
		public static Binding CreateBinding(string conceptualEntity, string conceptualProperty = null, string conceptualHierarchy = null, string conceptualHierarchyLevel = null, string conceptualVariationSource = null, string conceptualVariationSet = null)
		{
			if (conceptualProperty != null)
			{
				return new ConceptualPropertyBinding
				{
					ConceptualEntity = conceptualEntity,
					VariationSource = conceptualVariationSource,
					VariationSet = conceptualVariationSet,
					ConceptualProperty = conceptualProperty
				};
			}
			if (conceptualHierarchyLevel != null)
			{
				return new HierarchyLevelBinding
				{
					ConceptualEntity = conceptualEntity,
					VariationSource = conceptualVariationSource,
					VariationSet = conceptualVariationSet,
					Hierarchy = conceptualHierarchy,
					HierarchyLevel = conceptualHierarchyLevel
				};
			}
			if (conceptualHierarchy != null)
			{
				return new HierarchyBinding
				{
					ConceptualEntity = conceptualEntity,
					VariationSource = conceptualVariationSource,
					VariationSet = conceptualVariationSet,
					Hierarchy = conceptualHierarchy
				};
			}
			return new ConceptualEntityBinding
			{
				ConceptualEntity = conceptualEntity
			};
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x0000F591 File Offset: 0x0000D791
		public static ConceptualEntityBinding CreateEntityBinding(string conceptualEntity)
		{
			return new ConceptualEntityBinding
			{
				ConceptualEntity = conceptualEntity
			};
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x0000F59F File Offset: 0x0000D79F
		public static ConceptualPropertyBinding CreatePropertyBinding(string conceptualEntity, string conceptualProperty)
		{
			return new ConceptualPropertyBinding
			{
				ConceptualEntity = conceptualEntity,
				ConceptualProperty = conceptualProperty
			};
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x0000F5B4 File Offset: 0x0000D7B4
		public static Instances CreateInstances(State state = State.Generated, ConceptualPropertyBinding synonymBinding = null, ConceptualPropertyBinding valueBinding = null, ConceptualPropertyBinding instanceWeights = null, EntityInstancePluralNormalization pluralNormalization = EntityInstancePluralNormalization.Default, EntityInstanceIndex instanceIndex = EntityInstanceIndex.Default)
		{
			Instances instances = new Instances
			{
				PluralNormalization = pluralNormalization,
				Index = instanceIndex
			};
			if (synonymBinding != null && valueBinding != null)
			{
				instances.Synonyms = new InstanceSynonyms
				{
					State = state,
					SynonymBinding = synonymBinding,
					ValueBinding = valueBinding
				};
			}
			if (instanceWeights != null)
			{
				instances.Weights = new InstanceWeights
				{
					Binding = instanceWeights
				};
			}
			return instances;
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x0000F614 File Offset: 0x0000D814
		public static Term CreateTerm(string value, State state = State.Generated, double weight = 1.0, TermPropertiesType? type = null, DateTime? lastModified = null, SourceType sourceType = SourceType.Default)
		{
			return new Term(value, new TermProperties
			{
				State = state,
				Source = ((sourceType != SourceType.Default) ? new Source
				{
					Type = sourceType
				} : default(Source)),
				Type = type,
				Weight = weight,
				LastModified = lastModified
			});
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x0000F670 File Offset: 0x0000D870
		public static Term CreateTerm(string value, State state, TermPropertiesType? type, double weight, Source source, DateTime? lastModified)
		{
			return new Term(value, new TermProperties
			{
				State = state,
				Type = type,
				Source = source,
				Weight = weight,
				LastModified = lastModified
			});
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x0000F6A4 File Offset: 0x0000D8A4
		public static IEnumerable<Term> CreateTerms(this IEnumerable<string> stringValues, State state = State.Generated, double weight = 1.0, TermPropertiesType? type = null, DateTime? lastModified = null)
		{
			return from s in stringValues.EmptyIfNull<string>()
				select LsdlDocumentFactory.CreateTerm(s, state, weight, type, lastModified, SourceType.Default);
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x0000F6EC File Offset: 0x0000D8EC
		public static EnumProperty<TEnum> CreateEnumProperty<TEnum>(TEnum value, PropertyState state = PropertyState.Generated) where TEnum : struct, Enum
		{
			return new EnumProperty<TEnum>
			{
				Value = value,
				State = state
			};
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x0000F714 File Offset: 0x0000D914
		public static KeyValuePair<string, Role> CreateDefaultRole(string entityName, IEnumerable<Term> nouns = null, string entityNamespace = null)
		{
			if (string.IsNullOrEmpty(entityName))
			{
				return default(KeyValuePair<string, Role>);
			}
			Role role = new Role
			{
				Target = new EntityReference
				{
					Entity = entityName,
					Namespace = entityNamespace
				},
				Nouns = { nouns }
			};
			return Util.ToKeyValuePair<string, Role>(entityName, role);
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x0000F768 File Offset: 0x0000D968
		public static KeyValuePair<string, Role> CreateRole(string name, string entityName, string quantity = null, string amount = null, IEnumerable<Term> nouns = null, string entityNamespace = null)
		{
			if (string.IsNullOrEmpty(entityName))
			{
				return default(KeyValuePair<string, Role>);
			}
			Role role = new Role
			{
				Target = new EntityReference
				{
					Entity = entityName,
					Namespace = entityNamespace
				},
				Nouns = { nouns },
				Quantity = LsdlDocumentFactory.CreateRoleReference(quantity),
				Amount = LsdlDocumentFactory.CreateRoleReference(amount)
			};
			return Util.ToKeyValuePair<string, Role>(name, role);
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x0000F7D4 File Offset: 0x0000D9D4
		public static Phrasing CreateAttributePhrasing(RoleReference subjectRole, RoleReference objectRole, IEnumerable<PrepPhrase> prepPhrases = null, double weight = 1.0, State state = State.Generated)
		{
			AttributePhrasingProperties attributePhrasingProperties = new AttributePhrasingProperties
			{
				Subject = subjectRole,
				Object = objectRole
			};
			if (prepPhrases != null)
			{
				attributePhrasingProperties.PrepositionalPhrases.AddRange(prepPhrases);
			}
			return new Phrasing
			{
				Attribute = attributePhrasingProperties,
				State = state,
				Weight = weight
			};
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x0000F820 File Offset: 0x0000DA20
		public static Phrasing CreateAdjectivePhrasing(RoleReference subjectRole, IEnumerable<Term> adjectives, IEnumerable<Term> antonyms, RoleReference measurementRole = null, IEnumerable<PrepPhrase> prepPhrases = null, IEnumerable<AdverbPhrase> adverbPhrases = null, double weight = 1.0, State state = State.Generated)
		{
			AdjectivePhrasingProperties adjectivePhrasingProperties = new AdjectivePhrasingProperties
			{
				Subject = subjectRole,
				Measurement = measurementRole
			};
			if (adjectives != null)
			{
				adjectivePhrasingProperties.Adjectives.AddRange(adjectives);
			}
			if (antonyms != null)
			{
				adjectivePhrasingProperties.Antonyms.AddRange(antonyms);
			}
			if (prepPhrases != null)
			{
				adjectivePhrasingProperties.PrepositionalPhrases.AddRange(prepPhrases);
			}
			if (adverbPhrases != null)
			{
				adjectivePhrasingProperties.AdverbPhrases.AddRange(adverbPhrases);
			}
			return new Phrasing
			{
				Adjective = adjectivePhrasingProperties,
				State = state,
				Weight = weight
			};
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x0000F8A0 File Offset: 0x0000DAA0
		public static Phrasing CreateDynamicAdjectivePhrasing(RoleReference subjectRole, RoleReference adjectiveRole, IEnumerable<PrepPhrase> prepPhrases = null, double weight = 1.0, State state = State.Generated)
		{
			DynamicAdjectivePhrasingProperties dynamicAdjectivePhrasingProperties = new DynamicAdjectivePhrasingProperties
			{
				Subject = subjectRole,
				Adjective = adjectiveRole
			};
			if (prepPhrases != null)
			{
				dynamicAdjectivePhrasingProperties.PrepositionalPhrases.AddRange(prepPhrases);
			}
			return new Phrasing
			{
				DynamicAdjective = dynamicAdjectivePhrasingProperties,
				State = state,
				Weight = weight
			};
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x0000F8EC File Offset: 0x0000DAEC
		public static Phrasing CreateDynamicNounPhrasing(RoleReference subjectRole, RoleReference nounRole, IEnumerable<PrepPhrase> prepPhrases = null, double weight = 1.0, State state = State.Generated)
		{
			DynamicNounPhrasingProperties dynamicNounPhrasingProperties = new DynamicNounPhrasingProperties
			{
				Subject = subjectRole,
				Noun = nounRole
			};
			if (prepPhrases != null)
			{
				dynamicNounPhrasingProperties.PrepositionalPhrases.AddRange(prepPhrases);
			}
			return new Phrasing
			{
				DynamicNoun = dynamicNounPhrasingProperties,
				State = state,
				Weight = weight
			};
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x0000F937 File Offset: 0x0000DB37
		public static Phrasing CreateNamePhrasing(RoleReference subjectRole, RoleReference nameRole, double weight = 1.0, State state = State.Generated)
		{
			return new Phrasing
			{
				Name = new NamePhrasingProperties
				{
					Subject = subjectRole,
					Name = nameRole
				},
				State = state,
				Weight = weight
			};
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x0000F968 File Offset: 0x0000DB68
		public static Phrasing CreateNounPhrasing(RoleReference subjectRole, IEnumerable<Term> nouns, IEnumerable<PrepPhrase> prepPhrases = null, double weight = 1.0, State state = State.Generated)
		{
			Phrasing phrasing = new Phrasing
			{
				Noun = new NounPhrasingProperties
				{
					Subject = subjectRole,
					Nouns = { nouns }
				},
				State = state,
				Weight = weight
			};
			if (prepPhrases != null)
			{
				phrasing.Noun.PrepositionalPhrases.AddRange(prepPhrases);
			}
			return phrasing;
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x0000F9C0 File Offset: 0x0000DBC0
		public static Phrasing CreatePrepositionPhrasing(RoleReference subjectRole, IEnumerable<Term> prepositions, RoleReference prepObjectRole, IEnumerable<PrepPhrase> prepPhrases = null, double weight = 1.0, State state = State.Generated)
		{
			PrepositionPhrasingProperties prepositionPhrasingProperties = new PrepositionPhrasingProperties
			{
				Subject = subjectRole,
				Object = prepObjectRole,
				Prepositions = { prepositions }
			};
			if (prepPhrases != null)
			{
				prepositionPhrasingProperties.PrepositionalPhrases.AddRange(prepPhrases);
			}
			return new Phrasing
			{
				Preposition = prepositionPhrasingProperties,
				State = state,
				Weight = weight
			};
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x0000FA18 File Offset: 0x0000DC18
		public static PrepPhrase CreatePrepPhrase(IEnumerable<Term> prepositions, RoleReference objectRole)
		{
			return new PrepPhrase
			{
				Object = objectRole,
				Prepositions = { prepositions }
			};
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x0000FA34 File Offset: 0x0000DC34
		public static Phrasing CreateVerbPhrasing(RoleReference subjectRole, IEnumerable<Term> verbs, RoleReference objectRole, IEnumerable<PrepPhrase> prepPhrases = null, double weight = 1.0, RoleReference indirectObject = null, IEnumerable<AdverbPhrase> adverbPhrases = null, State state = State.Generated)
		{
			VerbPhrasingProperties verbPhrasingProperties = new VerbPhrasingProperties
			{
				Subject = subjectRole,
				Object = objectRole,
				IndirectObject = indirectObject,
				Verbs = { verbs }
			};
			if (prepPhrases != null)
			{
				verbPhrasingProperties.PrepositionalPhrases.AddRange(prepPhrases);
			}
			if (adverbPhrases != null)
			{
				verbPhrasingProperties.AdverbPhrases.AddRange(adverbPhrases);
			}
			return new Phrasing
			{
				Verb = verbPhrasingProperties,
				State = state,
				Weight = weight
			};
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x0000FAA8 File Offset: 0x0000DCA8
		public static AdverbPhrase CreateAdverbPhrase(IEnumerable<Term> adverbs, IEnumerable<Term> antonyms = null, RoleReference measurement = null)
		{
			AdverbPhrase adverbPhrase = new AdverbPhrase
			{
				Adverbs = { adverbs },
				Measurement = measurement
			};
			if (antonyms != null)
			{
				adverbPhrase.Antonyms.AddRange(antonyms);
			}
			return adverbPhrase;
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x0000FADE File Offset: 0x0000DCDE
		public static RoleReference CreateRoleReference(string name)
		{
			if (!string.IsNullOrEmpty(name))
			{
				return new RoleReference
				{
					Role = name
				};
			}
			return null;
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x0000FAF8 File Offset: 0x0000DCF8
		public static Relationship CreateBinaryRelationship(KeyValuePair<string, Role> sourceRole, KeyValuePair<string, Role> targetRole, IEnumerable<Phrasing> phrasings = null, RoleReference where = null, RoleReference when = null, Condition condition = null, State source = State.Generated)
		{
			return LsdlDocumentFactory.CreateRelationship(new KeyValuePair<string, Role>[] { sourceRole, targetRole }, phrasings, null, where, when, null, null, (condition != null) ? condition.ArrayWrap<Condition>() : null, 1.0, source);
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x0000FB40 File Offset: 0x0000DD40
		public static Relationship CreateUnaryRelationship(KeyValuePair<string, Role> role, IEnumerable<Phrasing> phrasings, Condition condition = null, State state = State.Generated)
		{
			return LsdlDocumentFactory.CreateRelationship(new KeyValuePair<string, Role>[] { role }, phrasings, null, null, null, null, null, (condition != null) ? condition.ArrayWrap<Condition>() : null, 1.0, state);
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x0000FB7C File Offset: 0x0000DD7C
		public static Relationship CreateRelationship(IEnumerable<KeyValuePair<string, Role>> roles, IEnumerable<Phrasing> phrasings = null, string conceptualEntity = null, RoleReference where = null, RoleReference when = null, RoleReference duration = null, RoleReference occurrences = null, IEnumerable<Condition> conditions = null, double weight = 1.0, State state = State.Generated)
		{
			Relationship relationship = new Relationship
			{
				State = state,
				Binding = new ConceptualEntityBinding
				{
					ConceptualEntity = conceptualEntity
				},
				SemanticSlots = LsdlDocumentFactory.CreateSemanticSlots(where, when, duration, occurrences),
				Weight = weight
			};
			if (roles != null)
			{
				foreach (KeyValuePair<string, Role> keyValuePair in roles)
				{
					string text;
					Role role;
					keyValuePair.Deconstruct(out text, out role);
					string text2 = text;
					Role role2 = role;
					relationship.Roles.Add(text2, role2);
				}
			}
			if (phrasings != null)
			{
				foreach (Phrasing phrasing in phrasings)
				{
					relationship.Phrasings.Add(phrasing);
				}
			}
			if (conditions != null)
			{
				relationship.Conditions.AddRange(conditions);
			}
			return relationship;
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x0000FC70 File Offset: 0x0000DE70
		public static SemanticSlots CreateSemanticSlots(RoleReference where = null, RoleReference when = null, RoleReference duration = null, RoleReference occurrences = null)
		{
			if (where != null || when != null || duration != null || occurrences != null)
			{
				return new SemanticSlots
				{
					Where = where,
					When = when,
					Duration = duration,
					Occurrences = occurrences
				};
			}
			return null;
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x0000FCA1 File Offset: 0x0000DEA1
		public static Condition CreateBooleanCondition(RoleReference target, bool conditionValue)
		{
			return LsdlDocumentFactory.CreateCondition(target, ConditionOperator.Equals, new Value
			{
				Boolean = new ValueList<bool?>
				{
					new bool?(conditionValue)
				}
			}, Aggregation.None);
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x0000FCC7 File Offset: 0x0000DEC7
		public static Condition CreateCondition(RoleReference target, ConditionOperator @operator, Value value, Aggregation aggregation = Aggregation.None)
		{
			return new Condition
			{
				Target = target,
				Operator = @operator,
				Value = value,
				Aggregation = aggregation
			};
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x0000FCEA File Offset: 0x0000DEEA
		public static Value CreateValue(string text)
		{
			return new Value
			{
				Text = new ValueList<string> { text }
			};
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x0000FD03 File Offset: 0x0000DF03
		public static Value CreateValue(double value)
		{
			return new Value
			{
				Number = new ValueList<Union<long, double>> { value }
			};
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x0000FD21 File Offset: 0x0000DF21
		public static Value CreateValue(long value)
		{
			return new Value
			{
				Number = new ValueList<Union<long, double>> { value }
			};
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x0000FD3F File Offset: 0x0000DF3F
		public static Value CreateValue(bool value)
		{
			return new Value
			{
				Boolean = new ValueList<bool?>
				{
					new bool?(value)
				}
			};
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x0000FD60 File Offset: 0x0000DF60
		public static Relationship CreateNamePhrasingRelationship(KeyValuePair<string, Role> subjectRole, KeyValuePair<string, Role> nameRole, double weight)
		{
			Phrasing[] array = new Phrasing[] { LsdlDocumentFactory.CreateNamePhrasing(LsdlDocumentFactory.CreateRoleReference(subjectRole.Key), LsdlDocumentFactory.CreateRoleReference(nameRole.Key), weight, State.Generated) };
			return LsdlDocumentFactory.CreateBinaryRelationship(subjectRole, nameRole, array, null, null, null, State.Generated);
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x0000FDA4 File Offset: 0x0000DFA4
		public static Relationship CreatePrepositionPhrasingRelationship(KeyValuePair<string, Role> subjectRole, IEnumerable<Term> prepositions, KeyValuePair<string, Role> prepObjectRole, double weight, RoleReference whereRole = null)
		{
			Phrasing[] array = new Phrasing[] { LsdlDocumentFactory.CreatePrepositionPhrasing(LsdlDocumentFactory.CreateRoleReference(subjectRole.Key), prepositions, LsdlDocumentFactory.CreateRoleReference(prepObjectRole.Key), null, weight, State.Generated) };
			return LsdlDocumentFactory.CreateBinaryRelationship(subjectRole, prepObjectRole, array, whereRole, null, null, State.Generated);
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x0000FDE9 File Offset: 0x0000DFE9
		public static LsdlReference CreateLsdlReference()
		{
			return new LsdlReference();
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x0000FDF0 File Offset: 0x0000DFF0
		public static Example CreateExample(string value, string templateSchema = null)
		{
			return new Example(value, new ExampleProperties
			{
				TemplateSchema = templateSchema
			});
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x0000FE04 File Offset: 0x0000E004
		public static AgentProperties CreateAgentProperties(DateTime lastModifiedTime)
		{
			return new AgentProperties
			{
				LastModified = lastModifiedTime
			};
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x0000FE12 File Offset: 0x0000E012
		public static GlobalSubstitution CreateGlobalSubstitution(string text, string substitute, string templateSchema = null, State state = State.Generated)
		{
			return new GlobalSubstitution(text, new GlobalSubstitutionProperties
			{
				Substitute = substitute,
				TemplateSchema = templateSchema,
				State = state
			});
		}
	}
}
