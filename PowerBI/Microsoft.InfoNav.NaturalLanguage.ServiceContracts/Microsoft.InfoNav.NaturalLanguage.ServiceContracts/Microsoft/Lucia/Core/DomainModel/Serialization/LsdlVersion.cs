using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.InfoNav;
using Newtonsoft.Json.Linq;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x02000191 RID: 401
	public sealed class LsdlVersion
	{
		// Token: 0x0600081B RID: 2075 RVA: 0x00010508 File Offset: 0x0000E708
		private LsdlVersion(Version value, string jsonSchemaName, [Nullable] Version previous, [Nullable] Version next, [Nullable] ILsdlDocumentUpgradeTransform upgradeTransform, [Nullable] ILsdlDocumentDowngradeTransform downgradeTransform)
		{
			this._versionString = value.ToString();
			this.Value = value;
			this.JsonSchemaName = jsonSchemaName;
			this.Previous = previous;
			this.Next = next;
			this.UpgradeTransform = upgradeTransform;
			this.DowngradeTransform = downgradeTransform;
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x0600081C RID: 2076 RVA: 0x00010554 File Offset: 0x0000E754
		internal Version Value { get; }

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x0600081D RID: 2077 RVA: 0x0001055C File Offset: 0x0000E75C
		internal string JsonSchemaName { get; }

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x0600081E RID: 2078 RVA: 0x00010564 File Offset: 0x0000E764
		[Nullable]
		internal Version Previous { get; }

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x0600081F RID: 2079 RVA: 0x0001056C File Offset: 0x0000E76C
		[Nullable]
		internal Version Next { get; }

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000820 RID: 2080 RVA: 0x00010574 File Offset: 0x0000E774
		[Nullable]
		internal ILsdlDocumentUpgradeTransform UpgradeTransform { get; }

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000821 RID: 2081 RVA: 0x0001057C File Offset: 0x0000E77C
		[Nullable]
		internal ILsdlDocumentDowngradeTransform DowngradeTransform { get; }

		// Token: 0x06000822 RID: 2082 RVA: 0x00010584 File Offset: 0x0000E784
		public override string ToString()
		{
			return this._versionString;
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x0001058C File Offset: 0x0000E78C
		public static bool IsSupported(Version version, IEnumerable<FeatureSwitch> featureSwitches)
		{
			return (!(version >= LsdlVersion.V3_4_0) || featureSwitches.Contains(FeatureSwitch.Adverbs)) && LsdlVersion.IsSupportedInternal(version);
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x000105B2 File Offset: 0x0000E7B2
		internal static bool IsSupportedInternal(Version version)
		{
			return LsdlVersion.SupportedVersions.ContainsKey(version);
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x000105C0 File Offset: 0x0000E7C0
		public static bool IsSupported(JObject schema, IEnumerable<FeatureSwitch> featureSwitches)
		{
			Version version;
			return LsdlVersion.TryParse(schema, out version) && LsdlVersion.IsSupported(version, featureSwitches);
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x000105E0 File Offset: 0x0000E7E0
		public static Version GetMinimumRequiredVersion(LsdlDocument lsdlDoc)
		{
			LsdlVersion.MinRequiredVisitor minRequiredVisitor = new LsdlVersion.MinRequiredVisitor();
			minRequiredVisitor.Visit(lsdlDoc);
			return minRequiredVisitor.RequiredVersion;
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x000105F3 File Offset: 0x0000E7F3
		public static bool IsDowngradeTransformNeeded(Version targetVersion)
		{
			return LsdlVersionTransforms.IsDowngradeTransformNeeded(LsdlVersion.Latest, targetVersion);
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x00010600 File Offset: 0x0000E800
		internal static bool TryParse(JObject schema, out Version version)
		{
			JValue jvalue = schema["Version"] as JValue;
			if (jvalue != null && jvalue.Type == JTokenType.String && Version.TryParse((string)jvalue, out version))
			{
				return true;
			}
			version = null;
			return false;
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x0001063E File Offset: 0x0000E83E
		internal static void SetVersion(JObject schema, LsdlVersion version)
		{
			schema["Version"] = version.ToString();
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x00010658 File Offset: 0x0000E858
		private static IReadOnlyDictionary<Version, LsdlVersion> CreateSupportedVersionsMap([global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Value", "JsonSchemaName" })] IReadOnlyList<global::System.ValueTuple<Version, string>> versionSequence, IReadOnlyList<ILsdlDocumentUpgradeTransform> versionTransforms)
		{
			Dictionary<Version, LsdlVersion> dictionary = new Dictionary<Version, LsdlVersion>();
			IEnumerator<global::System.ValueTuple<Version, string>> enumerator = versionSequence.GetEnumerator();
			if (!enumerator.MoveNext())
			{
				return dictionary;
			}
			global::System.ValueTuple<Version, string> valueTuple = enumerator.Current;
			Version version = null;
			Version version2 = null;
			IEnumerator<ILsdlDocumentUpgradeTransform> enumerator2 = versionTransforms.GetEnumerator();
			ILsdlDocumentUpgradeTransform lsdlDocumentUpgradeTransform = (enumerator2.MoveNext() ? enumerator2.Current : null);
			ILsdlDocumentUpgradeTransform lsdlDocumentUpgradeTransform2 = null;
			ILsdlDocumentDowngradeTransform lsdlDocumentDowngradeTransform = null;
			while (enumerator.MoveNext())
			{
				global::System.ValueTuple<Version, string> valueTuple2 = enumerator.Current;
				version2 = valueTuple2.Item1;
				if (lsdlDocumentUpgradeTransform != null && lsdlDocumentUpgradeTransform.SourceVersion == valueTuple.Item1)
				{
					lsdlDocumentUpgradeTransform2 = lsdlDocumentUpgradeTransform;
				}
				dictionary.Add(valueTuple.Item1, new LsdlVersion(valueTuple.Item1, valueTuple.Item2, version, version2, lsdlDocumentUpgradeTransform2, lsdlDocumentDowngradeTransform));
				version = valueTuple.Item1;
				valueTuple = enumerator.Current;
				version2 = null;
				if (lsdlDocumentUpgradeTransform2 != null)
				{
					lsdlDocumentDowngradeTransform = lsdlDocumentUpgradeTransform2 as ILsdlDocumentDowngradeTransform;
					lsdlDocumentUpgradeTransform2 = null;
					lsdlDocumentUpgradeTransform = (enumerator2.MoveNext() ? enumerator2.Current : null);
				}
				else
				{
					lsdlDocumentDowngradeTransform = null;
				}
			}
			dictionary.Add(valueTuple.Item1, new LsdlVersion(valueTuple.Item1, valueTuple.Item2, version, version2, lsdlDocumentUpgradeTransform2, lsdlDocumentDowngradeTransform));
			return dictionary;
		}

		// Token: 0x04000701 RID: 1793
		public static readonly Version V0_7_0 = new Version(0, 7, 0);

		// Token: 0x04000702 RID: 1794
		public static readonly Version V1_0_0 = new Version(1, 0, 0);

		// Token: 0x04000703 RID: 1795
		public static readonly Version V1_1_0 = new Version(1, 1, 0);

		// Token: 0x04000704 RID: 1796
		public static readonly Version V1_2_0 = new Version(1, 2, 0);

		// Token: 0x04000705 RID: 1797
		public static readonly Version V1_3_0 = new Version(1, 3, 0);

		// Token: 0x04000706 RID: 1798
		public static readonly Version V2_0_0 = new Version(2, 0, 0);

		// Token: 0x04000707 RID: 1799
		public static readonly Version V3_0_0 = new Version(3, 0, 0);

		// Token: 0x04000708 RID: 1800
		public static readonly Version V3_1_0 = new Version(3, 1, 0);

		// Token: 0x04000709 RID: 1801
		public static readonly Version V3_2_0 = new Version(3, 2, 0);

		// Token: 0x0400070A RID: 1802
		public static readonly Version V3_3_0 = new Version(3, 3, 0);

		// Token: 0x0400070B RID: 1803
		public static readonly Version V3_4_0 = new Version(3, 4, 0);

		// Token: 0x0400070C RID: 1804
		public static readonly Version V3_5_0 = new Version(3, 5, 0);

		// Token: 0x0400070D RID: 1805
		public static readonly Version BasePublic = LsdlVersion.V1_0_0;

		// Token: 0x0400070E RID: 1806
		public static readonly Version LatestPublic = LsdlVersion.V3_4_0;

		// Token: 0x0400070F RID: 1807
		internal static readonly Version Base = LsdlVersion.V0_7_0;

		// Token: 0x04000710 RID: 1808
		internal static readonly Version Latest = LsdlVersion.V3_5_0;

		// Token: 0x04000711 RID: 1809
		internal static IReadOnlyDictionary<Version, LsdlVersion> SupportedVersions = LsdlVersion.CreateSupportedVersionsMap(new global::System.ValueTuple<Version, string>[]
		{
			new global::System.ValueTuple<Version, string>(LsdlVersion.V0_7_0, "lsdlschema-0.7.json"),
			new global::System.ValueTuple<Version, string>(LsdlVersion.V1_0_0, "lsdlschema-1.0.json"),
			new global::System.ValueTuple<Version, string>(LsdlVersion.V1_1_0, "lsdlschema-1.2.json"),
			new global::System.ValueTuple<Version, string>(LsdlVersion.V1_2_0, "lsdlschema-1.2.json"),
			new global::System.ValueTuple<Version, string>(LsdlVersion.V1_3_0, "lsdlschema-1.3.json"),
			new global::System.ValueTuple<Version, string>(LsdlVersion.V2_0_0, "lsdlschema-2.0.json"),
			new global::System.ValueTuple<Version, string>(LsdlVersion.V3_0_0, "lsdlschema-3.0.json"),
			new global::System.ValueTuple<Version, string>(LsdlVersion.V3_1_0, "lsdlschema-3.1.json"),
			new global::System.ValueTuple<Version, string>(LsdlVersion.V3_2_0, "lsdlschema-3.2.json"),
			new global::System.ValueTuple<Version, string>(LsdlVersion.V3_3_0, "lsdlschema-3.3.json"),
			new global::System.ValueTuple<Version, string>(LsdlVersion.V3_4_0, "lsdlschema-3.4.json"),
			new global::System.ValueTuple<Version, string>(LsdlVersion.V3_5_0, "lsdlschema-latest.json")
		}, new ILsdlDocumentUpgradeTransform[]
		{
			new LsdlVersionTransforms.V0_7_0ToV1_0_0(),
			new LsdlVersionTransforms.V1_3_0ToV2_0_0(),
			new LsdlVersionTransforms.V2_0_0ToV3_0_0()
		});

		// Token: 0x04000712 RID: 1810
		private readonly string _versionString;

		// Token: 0x02000237 RID: 567
		private sealed class MinRequiredVisitor : DefaultLsdlDocumentVisitor
		{
			// Token: 0x1700034E RID: 846
			// (get) Token: 0x06000C12 RID: 3090 RVA: 0x0001823F File Offset: 0x0001643F
			// (set) Token: 0x06000C13 RID: 3091 RVA: 0x00018247 File Offset: 0x00016447
			internal Version RequiredVersion { get; private set; } = LsdlVersion.Base;

			// Token: 0x06000C14 RID: 3092 RVA: 0x00018250 File Offset: 0x00016450
			public override void Visit(LsdlDocument lsdlDocument)
			{
				base.Visit(lsdlDocument);
				if (lsdlDocument.Agents.Count > 0)
				{
					this.RequireVersion(LsdlVersion.V3_2_0);
					return;
				}
				if (lsdlDocument.Examples.Count > 0)
				{
					this.RequireVersion(LsdlVersion.V2_0_0);
					return;
				}
				if (lsdlDocument.MinResultConfidence != LsdlMinResultConfidence.Default)
				{
					this.RequireVersion(LsdlVersion.V1_0_0);
				}
			}

			// Token: 0x06000C15 RID: 3093 RVA: 0x000182AC File Offset: 0x000164AC
			public override void Visit(Entity entity)
			{
				base.Visit(entity);
				EntityDefinition definition = entity.Definition;
				bool flag = ((definition != null) ? definition.Binding : null) is ConceptualEntityBinding;
				EnumProperty<EntityVisibility> visibility = entity.Visibility;
				if (entity.ImplicitGroupings.Count > 0)
				{
					this.RequireVersion(LsdlVersion.V3_3_0);
					return;
				}
				if (entity.NameType != EntityNameType.None)
				{
					this.RequireVersion(LsdlVersion.V3_1_0);
					return;
				}
				if (visibility.State == PropertyState.Default && (!flag || visibility.Value != EntityVisibility.Hidden || entity.State == State.Generated) && (flag || visibility.Value != EntityVisibility.Children))
				{
					EntityDefinition definition2 = entity.Definition;
					if (string.IsNullOrEmpty((definition2 != null) ? definition2.Text : null))
					{
						Instances instances = entity.Instances;
						if (instances == null || instances.Index != EntityInstanceIndex.All)
						{
							if (visibility.Value != EntityVisibility.Visible)
							{
								this.RequireVersion(LsdlVersion.V1_2_0);
								return;
							}
							if (entity.State != State.Suggested && entity.TemplateSchema == null)
							{
								EntitySemanticType? semanticType = entity.SemanticType;
								EntitySemanticType entitySemanticType = EntitySemanticType.Duration;
								if (!((semanticType.GetValueOrDefault() == entitySemanticType) & (semanticType != null)) && entity.Units.Count <= 0)
								{
									Instances instances2 = entity.Instances;
									if (instances2 != null)
									{
										if (instances2.Index == EntityInstanceIndex.Default && instances2.PluralNormalization == EntityInstancePluralNormalization.Default)
										{
											InstanceSynonyms synonyms = instances2.Synonyms;
											if (synonyms == null || synonyms.State != State.Suggested)
											{
												return;
											}
										}
										this.RequireVersion(LsdlVersion.V1_0_0);
									}
									return;
								}
							}
							this.RequireVersion(LsdlVersion.V1_0_0);
							return;
						}
					}
				}
				this.RequireVersion(LsdlVersion.V3_0_0);
			}

			// Token: 0x06000C16 RID: 3094 RVA: 0x00018418 File Offset: 0x00016618
			public override void Visit(Term term)
			{
				base.Visit(term);
				if (!term.Properties.Source.IsDefault())
				{
					this.RequireVersion(LsdlVersion.V2_0_0);
					return;
				}
				if (term.Properties.LastModified != null)
				{
					this.RequireVersion(LsdlVersion.V1_2_0);
					return;
				}
				if (term.Properties.State == State.Suggested || term.Properties.TemplateSchema != null)
				{
					this.RequireVersion(LsdlVersion.V1_0_0);
				}
			}

			// Token: 0x06000C17 RID: 3095 RVA: 0x00018494 File Offset: 0x00016694
			public override void Visit(Relationship relationship)
			{
				base.Visit(relationship);
				if (relationship.LastModified != null)
				{
					this.RequireVersion(LsdlVersion.V3_5_0);
					return;
				}
				if (relationship.Phrasings.Count == 0)
				{
					this.RequireVersion(LsdlVersion.V1_2_0);
					return;
				}
				if (relationship.State != State.Suggested && relationship.TemplateSchema == null)
				{
					SemanticSlots semanticSlots = relationship.SemanticSlots;
					if (((semanticSlots != null) ? semanticSlots.Duration : null) == null)
					{
						return;
					}
				}
				this.RequireVersion(LsdlVersion.V1_0_0);
			}

			// Token: 0x06000C18 RID: 3096 RVA: 0x00018510 File Offset: 0x00016710
			public override void Visit(Phrasing phrasing)
			{
				base.Visit(phrasing);
				if (phrasing.LastModified != null || phrasing.ID != null)
				{
					this.RequireVersion(LsdlVersion.V3_5_0);
					return;
				}
				VerbPhrasingProperties verb = phrasing.Verb;
				if (verb == null || verb.AdverbPhrases.IsNullOrEmpty<AdverbPhrase>())
				{
					AdjectivePhrasingProperties adjective = phrasing.Adjective;
					if (adjective == null || adjective.AdverbPhrases.IsNullOrEmpty<AdverbPhrase>())
					{
						if (phrasing.State == State.Suggested || phrasing.TemplateSchema != null)
						{
							this.RequireVersion(LsdlVersion.V1_0_0);
						}
						return;
					}
				}
				this.RequireVersion(LsdlVersion.V3_4_0);
			}

			// Token: 0x06000C19 RID: 3097 RVA: 0x000185A8 File Offset: 0x000167A8
			public override void Visit(GlobalSubstitution globalSubstitution)
			{
				base.Visit(globalSubstitution);
				if (globalSubstitution.Properties.State == State.Suggested || globalSubstitution.Properties.TemplateSchema != null)
				{
					this.RequireVersion(LsdlVersion.V1_0_0);
				}
			}

			// Token: 0x06000C1A RID: 3098 RVA: 0x000185D8 File Offset: 0x000167D8
			public override void Visit(Condition condition)
			{
				base.Visit(condition);
				if (condition.Aggregation != Aggregation.None)
				{
					this.RequireVersion(LsdlVersion.V2_0_0);
					return;
				}
				if (condition.Operator == ConditionOperator.NotEquals || condition.Operator == ConditionOperator.Contains || condition.Operator == ConditionOperator.NotContains || condition.Operator == ConditionOperator.StartsWith || condition.Operator == ConditionOperator.NotStartsWith)
				{
					this.RequireVersion(LsdlVersion.V1_2_0);
				}
			}

			// Token: 0x06000C1B RID: 3099 RVA: 0x00018639 File Offset: 0x00016839
			private void RequireVersion(Version version)
			{
				if (version > this.RequiredVersion)
				{
					this.RequiredVersion = version;
				}
			}
		}
	}
}
