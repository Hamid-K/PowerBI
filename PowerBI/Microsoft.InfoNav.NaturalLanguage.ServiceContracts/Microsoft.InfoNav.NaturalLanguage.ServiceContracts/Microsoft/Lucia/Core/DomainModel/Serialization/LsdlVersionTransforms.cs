using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.InfoNav;
using Microsoft.Lucia.Json;
using Newtonsoft.Json.Linq;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x02000194 RID: 404
	internal static class LsdlVersionTransforms
	{
		// Token: 0x06000830 RID: 2096 RVA: 0x00010974 File Offset: 0x0000EB74
		internal static bool TryUpgradeJson(JObject schema, Version targetVersion, IDomainModelDiagnosticContext diagnosticContext)
		{
			Version version;
			LsdlVersion lsdlVersion;
			if (!LsdlVersion.TryParse(schema, out version) || !LsdlVersion.SupportedVersions.TryGetValue(version, out lsdlVersion))
			{
				diagnosticContext.Register(DomainModelDiagnosticMessageFactory.LinguisticSchemaVersionNotSupported());
				return false;
			}
			if (lsdlVersion.Value > targetVersion)
			{
				diagnosticContext.Register(DomainModelDiagnosticMessageFactory.LinguisticSchemaUpgradeInternalError());
				return false;
			}
			while (lsdlVersion.Value != targetVersion)
			{
				if (lsdlVersion.Next == null)
				{
					diagnosticContext.Register(DomainModelDiagnosticMessageFactory.LinguisticSchemaUpgradeInternalError());
					return false;
				}
				ILsdlDocumentUpgradeTransform upgradeTransform = lsdlVersion.UpgradeTransform;
				if (upgradeTransform != null)
				{
					upgradeTransform.Upgrade(schema);
				}
				lsdlVersion = LsdlVersion.SupportedVersions[lsdlVersion.Next];
			}
			LsdlVersion.SetVersion(schema, lsdlVersion);
			return true;
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x00010A18 File Offset: 0x0000EC18
		internal static void DowngradeJson(JObject schema, Version targetVersion)
		{
			Version version;
			if (!LsdlVersion.TryParse(schema, out version) || !LsdlVersion.IsSupportedInternal(version))
			{
				throw new ArgumentException(DomainModelDiagnosticMessageFactory.LinguisticSchemaVersionNotSupported().ToString("m"));
			}
			foreach (ILsdlDocumentDowngradeTransform lsdlDocumentDowngradeTransform in LsdlVersionTransforms.GetDowngradeTransforms(version, targetVersion))
			{
				lsdlDocumentDowngradeTransform.Downgrade(schema);
			}
			LsdlVersion.SetVersion(schema, LsdlVersion.SupportedVersions[targetVersion]);
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x00010A9C File Offset: 0x0000EC9C
		internal static bool IsDowngradeTransformNeeded(Version sourceVersion, Version targetVersion)
		{
			return LsdlVersionTransforms.GetDowngradeTransforms(sourceVersion, targetVersion).Any<ILsdlDocumentDowngradeTransform>();
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x00010AAA File Offset: 0x0000ECAA
		private static IEnumerable<ILsdlDocumentDowngradeTransform> GetDowngradeTransforms(Version sourceVersion, Version targetVersion)
		{
			LsdlVersion version;
			if (!LsdlVersion.SupportedVersions.TryGetValue(sourceVersion, out version))
			{
				throw new ArgumentOutOfRangeException("sourceVersion");
			}
			if (!LsdlVersion.IsSupportedInternal(targetVersion))
			{
				throw new ArgumentOutOfRangeException("targetVersion");
			}
			while (version.Value != targetVersion)
			{
				if (version.Previous == null)
				{
					throw Contract.Except(string.Format("Reached version {0} with no downgrade path to {1}", version.Value, targetVersion));
				}
				if (version.DowngradeTransform != null)
				{
					yield return version.DowngradeTransform;
				}
				version = LsdlVersion.SupportedVersions[version.Previous];
			}
			yield break;
		}

		// Token: 0x02000238 RID: 568
		internal sealed class V0_7_0ToV1_0_0 : ILsdlDocumentUpgradeTransform, ILsdlDocumentDowngradeTransform
		{
			// Token: 0x1700034F RID: 847
			// (get) Token: 0x06000C1D RID: 3101 RVA: 0x00018663 File Offset: 0x00016863
			public Version SourceVersion
			{
				get
				{
					return LsdlVersion.V0_7_0;
				}
			}

			// Token: 0x17000350 RID: 848
			// (get) Token: 0x06000C1E RID: 3102 RVA: 0x0001866A File Offset: 0x0001686A
			public Version TargetVersion
			{
				get
				{
					return LsdlVersion.V1_0_0;
				}
			}

			// Token: 0x06000C1F RID: 3103 RVA: 0x00018674 File Offset: 0x00016874
			public void Upgrade(JObject schema)
			{
				JObject jobject = schema["Entities"] as JObject;
				if (jobject != null)
				{
					JObject jobject2 = jobject;
					Action<JObject> action;
					if ((action = LsdlVersionTransforms.V0_7_0ToV1_0_0.<>O.<0>__UpgradeEntity) == null)
					{
						action = (LsdlVersionTransforms.V0_7_0ToV1_0_0.<>O.<0>__UpgradeEntity = new Action<JObject>(LsdlVersionTransforms.V0_7_0ToV1_0_0.UpgradeEntity));
					}
					jobject2.VisitDictionaryElements(action);
				}
				JObject jobject3 = schema["Relationships"] as JObject;
				if (jobject3 != null)
				{
					JObject jobject4 = jobject3;
					Action<JObject> action2;
					if ((action2 = LsdlVersionTransforms.V0_7_0ToV1_0_0.<>O.<1>__UpgradeRelationship) == null)
					{
						action2 = (LsdlVersionTransforms.V0_7_0ToV1_0_0.<>O.<1>__UpgradeRelationship = new Action<JObject>(LsdlVersionTransforms.V0_7_0ToV1_0_0.UpgradeRelationship));
					}
					jobject4.VisitDictionaryElements(action2);
				}
				JArray jarray = schema["GlobalSubstitutions"] as JArray;
				if (jarray != null)
				{
					jarray.VisitArrayElements(delegate(JObject s)
					{
						Action<JObject> action3;
						if ((action3 = LsdlVersionTransforms.V0_7_0ToV1_0_0.<>O.<2>__UpgradeState) == null)
						{
							action3 = (LsdlVersionTransforms.V0_7_0ToV1_0_0.<>O.<2>__UpgradeState = new Action<JObject>(LsdlVersionTransforms.V0_7_0ToV1_0_0.UpgradeState));
						}
						s.VisitDictionaryElements(action3);
					});
				}
			}

			// Token: 0x06000C20 RID: 3104 RVA: 0x00018724 File Offset: 0x00016924
			public void Downgrade(JObject schema)
			{
				JObject jobject = schema["Entities"] as JObject;
				if (jobject != null)
				{
					JObject jobject2 = jobject;
					Action<JObject> action;
					if ((action = LsdlVersionTransforms.V0_7_0ToV1_0_0.<>O.<3>__DowngradeEntity) == null)
					{
						action = (LsdlVersionTransforms.V0_7_0ToV1_0_0.<>O.<3>__DowngradeEntity = new Action<JObject>(LsdlVersionTransforms.V0_7_0ToV1_0_0.DowngradeEntity));
					}
					jobject2.VisitDictionaryElements(action);
				}
				JObject jobject3 = schema["Relationships"] as JObject;
				if (jobject3 != null)
				{
					JObject jobject4 = jobject3;
					Action<JObject> action2;
					if ((action2 = LsdlVersionTransforms.V0_7_0ToV1_0_0.<>O.<4>__DowngradeRelationship) == null)
					{
						action2 = (LsdlVersionTransforms.V0_7_0ToV1_0_0.<>O.<4>__DowngradeRelationship = new Action<JObject>(LsdlVersionTransforms.V0_7_0ToV1_0_0.DowngradeRelationship));
					}
					jobject4.VisitDictionaryElements(action2);
				}
				JArray jarray = schema["GlobalSubstitutions"] as JArray;
				if (jarray != null)
				{
					jarray.VisitArrayElements(delegate(JObject s)
					{
						Action<JObject> action3;
						if ((action3 = LsdlVersionTransforms.V0_7_0ToV1_0_0.<>O.<5>__DowngradeState) == null)
						{
							action3 = (LsdlVersionTransforms.V0_7_0ToV1_0_0.<>O.<5>__DowngradeState = new Action<JObject>(LsdlVersionTransforms.V0_7_0ToV1_0_0.DowngradeState));
						}
						s.VisitDictionaryElements(action3);
					});
				}
			}

			// Token: 0x06000C21 RID: 3105 RVA: 0x000187D4 File Offset: 0x000169D4
			private static void UpgradeEntity(JObject entity)
			{
				LsdlVersionTransforms.V0_7_0ToV1_0_0.UpgradeState(entity);
				JObject jobject = entity["Instances"] as JObject;
				if (jobject != null)
				{
					JObject jobject2 = jobject["Synonyms"] as JObject;
					if (jobject2 != null)
					{
						LsdlVersionTransforms.V0_7_0ToV1_0_0.UpgradeState(jobject2);
					}
				}
				JArray jarray = entity["Words"] as JArray;
				if (jarray != null)
				{
					jarray.VisitArrayElements(delegate(JObject w)
					{
						Action<JObject> action;
						if ((action = LsdlVersionTransforms.V0_7_0ToV1_0_0.<>O.<2>__UpgradeState) == null)
						{
							action = (LsdlVersionTransforms.V0_7_0ToV1_0_0.<>O.<2>__UpgradeState = new Action<JObject>(LsdlVersionTransforms.V0_7_0ToV1_0_0.UpgradeState));
						}
						w.VisitDictionaryElements(action);
					});
					entity.Remove("Words");
					entity.Add("Terms", jarray);
				}
			}

			// Token: 0x06000C22 RID: 3106 RVA: 0x00018868 File Offset: 0x00016A68
			private static void DowngradeEntity(JObject entity)
			{
				LsdlVersionTransforms.V0_7_0ToV1_0_0.DowngradeState(entity);
				JObject jobject = entity["Instances"] as JObject;
				if (jobject != null)
				{
					JObject jobject2 = jobject["Synonyms"] as JObject;
					if (jobject2 != null)
					{
						LsdlVersionTransforms.V0_7_0ToV1_0_0.DowngradeState(jobject2);
					}
				}
				JArray jarray = entity["Terms"] as JArray;
				if (jarray != null)
				{
					jarray.VisitArrayElements(delegate(JObject w)
					{
						Action<JObject> action;
						if ((action = LsdlVersionTransforms.V0_7_0ToV1_0_0.<>O.<5>__DowngradeState) == null)
						{
							action = (LsdlVersionTransforms.V0_7_0ToV1_0_0.<>O.<5>__DowngradeState = new Action<JObject>(LsdlVersionTransforms.V0_7_0ToV1_0_0.DowngradeState));
						}
						w.VisitDictionaryElements(action);
					});
					entity.Remove("Terms");
					entity.Add("Words", jarray);
				}
			}

			// Token: 0x06000C23 RID: 3107 RVA: 0x000188FC File Offset: 0x00016AFC
			private static void UpgradeRelationship(JObject relationship)
			{
				LsdlVersionTransforms.V0_7_0ToV1_0_0.UpgradeState(relationship);
				JArray jarray = relationship["Phrasings"] as JArray;
				if (jarray != null)
				{
					JArray jarray2 = jarray;
					Action<JObject> action;
					if ((action = LsdlVersionTransforms.V0_7_0ToV1_0_0.<>O.<2>__UpgradeState) == null)
					{
						action = (LsdlVersionTransforms.V0_7_0ToV1_0_0.<>O.<2>__UpgradeState = new Action<JObject>(LsdlVersionTransforms.V0_7_0ToV1_0_0.UpgradeState));
					}
					jarray2.VisitArrayElements(action);
				}
			}

			// Token: 0x06000C24 RID: 3108 RVA: 0x00018944 File Offset: 0x00016B44
			private static void DowngradeRelationship(JObject relationship)
			{
				LsdlVersionTransforms.V0_7_0ToV1_0_0.DowngradeState(relationship);
				JArray jarray = relationship["Phrasings"] as JArray;
				if (jarray != null)
				{
					JArray jarray2 = jarray;
					Action<JObject> action;
					if ((action = LsdlVersionTransforms.V0_7_0ToV1_0_0.<>O.<5>__DowngradeState) == null)
					{
						action = (LsdlVersionTransforms.V0_7_0ToV1_0_0.<>O.<5>__DowngradeState = new Action<JObject>(LsdlVersionTransforms.V0_7_0ToV1_0_0.DowngradeState));
					}
					jarray2.VisitArrayElements(action);
				}
			}

			// Token: 0x06000C25 RID: 3109 RVA: 0x0001898C File Offset: 0x00016B8C
			private static void UpgradeState(JObject schemaElement)
			{
				JToken jtoken = schemaElement["State"];
				if (((jtoken != null) ? jtoken.Value<string>() : null) == "UserAuthored")
				{
					schemaElement["State"] = "Authored";
				}
			}

			// Token: 0x06000C26 RID: 3110 RVA: 0x000189C6 File Offset: 0x00016BC6
			private static void DowngradeState(JObject schemaElement)
			{
				JToken jtoken = schemaElement["State"];
				if (((jtoken != null) ? jtoken.Value<string>() : null) == "Authored")
				{
					schemaElement["State"] = "UserAuthored";
				}
			}

			// Token: 0x04000914 RID: 2324
			private const string EntitiesProperty = "Entities";

			// Token: 0x04000915 RID: 2325
			private const string RelationshipsProperty = "Relationships";

			// Token: 0x04000916 RID: 2326
			private const string GlobalSubstitutionsProperty = "GlobalSubstitutions";

			// Token: 0x04000917 RID: 2327
			private const string InstancesProperty = "Instances";

			// Token: 0x04000918 RID: 2328
			private const string SynonymsProperty = "Synonyms";

			// Token: 0x04000919 RID: 2329
			private const string WordsProperty = "Words";

			// Token: 0x0400091A RID: 2330
			private const string TermsProperty = "Terms";

			// Token: 0x0400091B RID: 2331
			private const string PhrasingsProperty = "Phrasings";

			// Token: 0x0400091C RID: 2332
			private const string StateProperty = "State";

			// Token: 0x0400091D RID: 2333
			private const string StateUserAuthored = "UserAuthored";

			// Token: 0x0400091E RID: 2334
			private const string StateAuthored = "Authored";

			// Token: 0x0200025E RID: 606
			[CompilerGenerated]
			private static class <>O
			{
				// Token: 0x040009AC RID: 2476
				public static Action<JObject> <0>__UpgradeEntity;

				// Token: 0x040009AD RID: 2477
				public static Action<JObject> <1>__UpgradeRelationship;

				// Token: 0x040009AE RID: 2478
				public static Action<JObject> <2>__UpgradeState;

				// Token: 0x040009AF RID: 2479
				public static Action<JObject> <3>__DowngradeEntity;

				// Token: 0x040009B0 RID: 2480
				public static Action<JObject> <4>__DowngradeRelationship;

				// Token: 0x040009B1 RID: 2481
				public static Action<JObject> <5>__DowngradeState;
			}
		}

		// Token: 0x02000239 RID: 569
		internal sealed class V1_3_0ToV2_0_0 : ILsdlDocumentUpgradeTransform, ILsdlDocumentDowngradeTransform
		{
			// Token: 0x17000351 RID: 849
			// (get) Token: 0x06000C28 RID: 3112 RVA: 0x00018A08 File Offset: 0x00016C08
			public Version SourceVersion
			{
				get
				{
					return LsdlVersion.V1_3_0;
				}
			}

			// Token: 0x17000352 RID: 850
			// (get) Token: 0x06000C29 RID: 3113 RVA: 0x00018A0F File Offset: 0x00016C0F
			public Version TargetVersion
			{
				get
				{
					return LsdlVersion.V2_0_0;
				}
			}

			// Token: 0x06000C2A RID: 3114 RVA: 0x00018A18 File Offset: 0x00016C18
			public void Upgrade(JObject schema)
			{
				JObject jobject = schema["Entities"] as JObject;
				if (jobject != null)
				{
					JObject jobject2 = jobject;
					Action<JObject> action;
					if ((action = LsdlVersionTransforms.V1_3_0ToV2_0_0.<>O.<0>__UpgradeEntity) == null)
					{
						action = (LsdlVersionTransforms.V1_3_0ToV2_0_0.<>O.<0>__UpgradeEntity = new Action<JObject>(LsdlVersionTransforms.V1_3_0ToV2_0_0.UpgradeEntity));
					}
					jobject2.VisitDictionaryElements(action);
				}
			}

			// Token: 0x06000C2B RID: 3115 RVA: 0x00018A5C File Offset: 0x00016C5C
			public void Downgrade(JObject schema)
			{
				JObject jobject = schema["Entities"] as JObject;
				if (jobject != null)
				{
					JObject jobject2 = jobject;
					Action<JObject> action;
					if ((action = LsdlVersionTransforms.V1_3_0ToV2_0_0.<>O.<1>__DowngradeEntity) == null)
					{
						action = (LsdlVersionTransforms.V1_3_0ToV2_0_0.<>O.<1>__DowngradeEntity = new Action<JObject>(LsdlVersionTransforms.V1_3_0ToV2_0_0.DowngradeEntity));
					}
					jobject2.VisitDictionaryElements(action);
				}
			}

			// Token: 0x06000C2C RID: 3116 RVA: 0x00018AA0 File Offset: 0x00016CA0
			private static void UpgradeEntity(JObject entity)
			{
				JObject jobject = entity["Binding"] as JObject;
				if (jobject != null)
				{
					JObject jobject2 = new JObject();
					jobject2["Binding"] = jobject;
					JObject jobject3 = jobject2;
					entity.Remove("Binding");
					entity.AddFirst(new JProperty("Definition", jobject3));
				}
			}

			// Token: 0x06000C2D RID: 3117 RVA: 0x00018AF0 File Offset: 0x00016CF0
			private static void DowngradeEntity(JObject entity)
			{
				JObject jobject = entity["Definition"] as JObject;
				if (jobject != null)
				{
					JObject jobject2 = jobject["Binding"] as JObject;
					if (jobject2 != null)
					{
						entity.Remove("Definition");
						entity.AddFirst(new JProperty("Binding", jobject2));
					}
				}
			}

			// Token: 0x0400091F RID: 2335
			private const string EntitiesProperty = "Entities";

			// Token: 0x04000920 RID: 2336
			private const string BindingProperty = "Binding";

			// Token: 0x04000921 RID: 2337
			private const string DefinitionProperty = "Definition";

			// Token: 0x04000922 RID: 2338
			private const string DynamicImprovementProperty = "DynamicImprovement";

			// Token: 0x04000923 RID: 2339
			private const string DynamicImprovementHighConfidence = "HighConfidence";

			// Token: 0x02000260 RID: 608
			[CompilerGenerated]
			private static class <>O
			{
				// Token: 0x040009B7 RID: 2487
				public static Action<JObject> <0>__UpgradeEntity;

				// Token: 0x040009B8 RID: 2488
				public static Action<JObject> <1>__DowngradeEntity;
			}
		}

		// Token: 0x0200023A RID: 570
		internal sealed class V2_0_0ToV3_0_0 : ILsdlDocumentUpgradeTransform, ILsdlDocumentDowngradeTransform
		{
			// Token: 0x17000353 RID: 851
			// (get) Token: 0x06000C2F RID: 3119 RVA: 0x00018B4A File Offset: 0x00016D4A
			public Version SourceVersion
			{
				get
				{
					return LsdlVersion.V2_0_0;
				}
			}

			// Token: 0x17000354 RID: 852
			// (get) Token: 0x06000C30 RID: 3120 RVA: 0x00018B51 File Offset: 0x00016D51
			public Version TargetVersion
			{
				get
				{
					return LsdlVersion.V3_0_0;
				}
			}

			// Token: 0x06000C31 RID: 3121 RVA: 0x00018B58 File Offset: 0x00016D58
			public void Upgrade(JObject schema)
			{
				JObject jobject = schema["Entities"] as JObject;
				if (jobject != null)
				{
					JObject jobject2 = jobject;
					Action<JObject> action;
					if ((action = LsdlVersionTransforms.V2_0_0ToV3_0_0.<>O.<0>__UpgradeEntity) == null)
					{
						action = (LsdlVersionTransforms.V2_0_0ToV3_0_0.<>O.<0>__UpgradeEntity = new Action<JObject>(LsdlVersionTransforms.V2_0_0ToV3_0_0.UpgradeEntity));
					}
					jobject2.VisitDictionaryElements(action);
				}
			}

			// Token: 0x06000C32 RID: 3122 RVA: 0x00018B9C File Offset: 0x00016D9C
			public void Downgrade(JObject schema)
			{
				JObject jobject = schema["Entities"] as JObject;
				if (jobject != null)
				{
					JObject jobject2 = jobject;
					Action<JObject> action;
					if ((action = LsdlVersionTransforms.V2_0_0ToV3_0_0.<>O.<1>__DowngradeEntity) == null)
					{
						action = (LsdlVersionTransforms.V2_0_0ToV3_0_0.<>O.<1>__DowngradeEntity = new Action<JObject>(LsdlVersionTransforms.V2_0_0ToV3_0_0.DowngradeEntity));
					}
					jobject2.VisitDictionaryElements(action);
				}
			}

			// Token: 0x06000C33 RID: 3123 RVA: 0x00018BE0 File Offset: 0x00016DE0
			private static void UpgradeEntity(JObject entity)
			{
				JToken jtoken = entity["Hidden"];
				JValue jvalue = jtoken as JValue;
				if (jvalue != null)
				{
					JObject jobject = entity["Definition"]["Binding"] as JObject;
					if (jobject != null)
					{
						jtoken.Parent.Replace(new JProperty("Visibility", LsdlVersionTransforms.V2_0_0ToV3_0_0.GetVisibility(jobject, jvalue)));
					}
				}
			}

			// Token: 0x06000C34 RID: 3124 RVA: 0x00018C40 File Offset: 0x00016E40
			private static void DowngradeEntity(JObject entity)
			{
				JToken jtoken = entity["Visibility"];
				JValue jvalue = jtoken as JValue;
				if (jvalue != null)
				{
					string text = jvalue.Value<string>();
					LsdlVersionTransforms.V2_0_0ToV3_0_0.ReplaceWithHidden(jtoken, text);
					return;
				}
				JObject jobject = jtoken as JObject;
				if (jobject != null)
				{
					JValue jvalue2 = jobject["Value"] as JValue;
					if (jvalue2 != null)
					{
						string text2 = jvalue2.Value<string>();
						LsdlVersionTransforms.V2_0_0ToV3_0_0.ReplaceWithHidden(jtoken, text2);
					}
				}
			}

			// Token: 0x06000C35 RID: 3125 RVA: 0x00018CA4 File Offset: 0x00016EA4
			private static JObject GetVisibility(JObject binding, JValue hiddenValue)
			{
				EntityVisibility entityVisibility = (hiddenValue.Value<bool>() ? (LsdlVersionTransforms.V2_0_0ToV3_0_0.IsTableBinding(binding) ? EntityVisibility.Children : EntityVisibility.Hidden) : EntityVisibility.Visible);
				JObject jobject = new JObject();
				jobject["Value"] = entityVisibility.ToString();
				return jobject;
			}

			// Token: 0x06000C36 RID: 3126 RVA: 0x00018CEC File Offset: 0x00016EEC
			private static bool IsTableBinding(JObject binding)
			{
				return (binding["Table"] != null || binding["ConceptualEntity"] != null) && binding["Column"] == null && binding["ConceptualProperty"] == null && binding["Measure"] == null && binding["Hierarchy"] == null && binding["HierarchyLevel"] == null && binding["VariationSource"] == null && binding["VariationSet"] == null;
			}

			// Token: 0x06000C37 RID: 3127 RVA: 0x00018D74 File Offset: 0x00016F74
			private static void ReplaceWithHidden(JToken visibility, string value)
			{
				if (value == "Hidden" || value == "Children")
				{
					visibility.Parent.Replace(new JProperty("Hidden", true));
					return;
				}
				visibility.Parent.Replace(new JProperty("Hidden", false));
			}

			// Token: 0x04000924 RID: 2340
			private const string EntitiesProperty = "Entities";

			// Token: 0x04000925 RID: 2341
			private const string HiddenProperty = "Hidden";

			// Token: 0x04000926 RID: 2342
			private const string VisibilityProperty = "Visibility";

			// Token: 0x04000927 RID: 2343
			private const string DefinitionProperty = "Definition";

			// Token: 0x04000928 RID: 2344
			private const string BindingProperty = "Binding";

			// Token: 0x04000929 RID: 2345
			private const string ConceptualEntityProperty = "ConceptualEntity";

			// Token: 0x0400092A RID: 2346
			private const string ConceptualPropertyProperty = "ConceptualProperty";

			// Token: 0x0400092B RID: 2347
			private const string TableProperty = "Table";

			// Token: 0x0400092C RID: 2348
			private const string MeasureProperty = "Measure";

			// Token: 0x0400092D RID: 2349
			private const string ColumnProperty = "Column";

			// Token: 0x0400092E RID: 2350
			private const string HierarchyProperty = "Hierarchy";

			// Token: 0x0400092F RID: 2351
			private const string HierarchyLevelProperty = "HierarchyLevel";

			// Token: 0x04000930 RID: 2352
			private const string VariationSource = "VariationSource";

			// Token: 0x04000931 RID: 2353
			private const string VariationSet = "VariationSet";

			// Token: 0x04000932 RID: 2354
			private const string Visible = "Visible";

			// Token: 0x04000933 RID: 2355
			private const string Hidden = "Hidden";

			// Token: 0x04000934 RID: 2356
			private const string Children = "Children";

			// Token: 0x04000935 RID: 2357
			private const string Value = "Value";

			// Token: 0x02000261 RID: 609
			[CompilerGenerated]
			private static class <>O
			{
				// Token: 0x040009B9 RID: 2489
				public static Action<JObject> <0>__UpgradeEntity;

				// Token: 0x040009BA RID: 2490
				public static Action<JObject> <1>__DowngradeEntity;
			}
		}
	}
}
