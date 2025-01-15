using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AnalysisServices.Core;
using Microsoft.AnalysisServices.Extensions;
using Microsoft.AnalysisServices.Hosting;
using Microsoft.AnalysisServices.Tabular.Extensions;
using Microsoft.AnalysisServices.Tabular.Json;
using Microsoft.AnalysisServices.Tabular.Json.Linq;
using Microsoft.AnalysisServices.Tabular.Tmdl;
using Microsoft.AnalysisServices.Tabular.Utilities;

namespace Microsoft.AnalysisServices.Tabular.Serialization
{
	// Token: 0x0200016C RID: 364
	internal static class TmdlSerializationHelper
	{
		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x0600173B RID: 5947 RVA: 0x000A1010 File Offset: 0x0009F210
		public static IMetadataFilter DefaultFilter
		{
			get
			{
				return TmdlSerializationHelper.defaultFilter;
			}
		}

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x0600173C RID: 5948 RVA: 0x000A1017 File Offset: 0x0009F217
		public static IMetadataFilter IgnoreChildrenFilter
		{
			get
			{
				if (TmdlSerializationHelper.ignoreChildrenFilter == null)
				{
					TmdlSerializationHelper.ignoreChildrenFilter = new IgnoreChildrenMetadataFilter(TmdlSerializationHelper.defaultFilter);
				}
				return TmdlSerializationHelper.ignoreChildrenFilter;
			}
		}

		// Token: 0x0600173D RID: 5949 RVA: 0x000A1034 File Offset: 0x0009F234
		public static void ProcessSerializationOptions(MetadataSerializationOptions options, out TmdlSerializationConfiguration config, out IDisposable contentFormatScope, out ISerializationStrategy strategy, out MetadataCompatibilityOptions compatRequest)
		{
			if (options != null)
			{
				config = new TmdlSerializationConfiguration(options);
				if (options.Formatting != null)
				{
					contentFormatScope = MetadataFormattingOptions.StartFormattingScope(options.Formatting);
				}
				else
				{
					contentFormatScope = null;
				}
				strategy = options.Strategy ?? SerializationStrategy.Default;
				compatRequest = options.Compatibility;
				if (compatRequest != null && compatRequest.CompatibilityMode == CompatibilityMode.Unknown && compatRequest.CompatibilityLevel == -1)
				{
					compatRequest = null;
					return;
				}
			}
			else
			{
				config = new TmdlSerializationConfiguration(TmdlSerializationHelper.DefaultFilter);
				contentFormatScope = null;
				strategy = SerializationStrategy.Default;
				compatRequest = null;
			}
		}

		// Token: 0x0600173E RID: 5950 RVA: 0x000A10C1 File Offset: 0x0009F2C1
		public static void ProcessDeserializationOptions(MetadataDeserializationOptions options, ref bool raiseErrorOnUnresolvedLinks, out MetadataCompatibilityOptions compatRequest)
		{
			if (options != null)
			{
				raiseErrorOnUnresolvedLinks = options.RaiseErrorOnUnresolvedLinks;
				compatRequest = options.Compatibility;
				if (compatRequest != null && compatRequest.CompatibilityMode == CompatibilityMode.Unknown && compatRequest.CompatibilityLevel == -1)
				{
					compatRequest = null;
					return;
				}
			}
			else
			{
				compatRequest = null;
			}
		}

		// Token: 0x0600173F RID: 5951 RVA: 0x000A10F4 File Offset: 0x0009F2F4
		public static void ProcessSchemaSerializationOptions(MetadataSchemaSerializationOptions options, out IDisposable contentFormatScope, out MetadataCompatibilityOptions compatRequest)
		{
			if (options != null)
			{
				if (options.Formatting != null)
				{
					contentFormatScope = MetadataFormattingOptions.StartFormattingScope(options.Formatting);
				}
				else
				{
					contentFormatScope = null;
				}
				compatRequest = options.Compatibility;
				if (compatRequest != null && compatRequest.CompatibilityMode == CompatibilityMode.Unknown && compatRequest.CompatibilityLevel == -1)
				{
					compatRequest = null;
					return;
				}
			}
			else
			{
				contentFormatScope = null;
				compatRequest = null;
			}
		}

		// Token: 0x06001740 RID: 5952 RVA: 0x000A1148 File Offset: 0x0009F348
		public static void GetMetadataObjectCompatibilityRestrictions(MetadataObject @object, out CompatibilityMode compatibilityMode, out int dbCompatibilityLevel)
		{
			if (@object.Model != null && @object.Model.Database != null)
			{
				dbCompatibilityLevel = @object.Model.Database.CompatibilityLevel;
				compatibilityMode = @object.Model.Database.CompatibilityMode;
				if (compatibilityMode == CompatibilityMode.Unknown)
				{
					int num = @object.GetCompatibilityRequirementLevel(CompatibilityMode.AnalysisServices);
					if (num != -2 && num <= dbCompatibilityLevel)
					{
						compatibilityMode = CompatibilityMode.AnalysisServices;
						return;
					}
					num = @object.GetCompatibilityRequirementLevel(CompatibilityMode.PowerBI);
					if (num == -2 || num > dbCompatibilityLevel)
					{
						throw new InvalidOperationException(TomSR.Exception_InvalidCompatModeOnTmdlSerialization);
					}
					compatibilityMode = CompatibilityMode.PowerBI;
					return;
				}
			}
			else
			{
				dbCompatibilityLevel = @object.GetCompatibilityRequirementLevel(CompatibilityMode.AnalysisServices);
				int num2 = dbCompatibilityLevel;
				if (num2 != -2)
				{
					if (num2 == -1)
					{
						dbCompatibilityLevel = 1200;
						compatibilityMode = CompatibilityMode.AnalysisServices;
						return;
					}
					if (num2 == 2147483647)
					{
						compatibilityMode = CompatibilityMode.AnalysisServices;
						return;
					}
					Utils.Verify(dbCompatibilityLevel >= 1200 && dbCompatibilityLevel <= 1000000);
					compatibilityMode = CompatibilityMode.AnalysisServices;
				}
				else
				{
					dbCompatibilityLevel = @object.GetCompatibilityRequirementLevel(CompatibilityMode.PowerBI);
					if (dbCompatibilityLevel == -2)
					{
						throw new InvalidOperationException(TomSR.Exception_InvalidCompatModeOnTmdlSerialization);
					}
					if (dbCompatibilityLevel == -1)
					{
						dbCompatibilityLevel = 1200;
					}
					compatibilityMode = CompatibilityMode.PowerBI;
					return;
				}
			}
		}

		// Token: 0x06001741 RID: 5953 RVA: 0x000A1244 File Offset: 0x0009F444
		public static void GetDatabaseCompatibilityRestrictions(Database db, Model model, out CompatibilityMode compatibilityMode, out int dbCompatibilityLevel)
		{
			dbCompatibilityLevel = db.CompatibilityLevel;
			compatibilityMode = db.CompatibilityMode;
			if (compatibilityMode == CompatibilityMode.Unknown)
			{
				compatibilityMode = CompatibilityMode.PowerBI;
				if (model != null)
				{
					int num;
					string text;
					model.GetCompatibilityRequirement(compatibilityMode, out num, out text);
					if (num == -2 || num > dbCompatibilityLevel)
					{
						compatibilityMode = CompatibilityMode.PowerBI;
					}
				}
			}
		}

		// Token: 0x06001742 RID: 5954 RVA: 0x000A1284 File Offset: 0x0009F484
		public static SerializationActivityContext CreateContextBasedOnCompatibilityRestrictionsRequest(MetadataCompatibilityOptions compatRequest, MetadataObject rootObject, CompatibilityMode requiredMode, int requiredLevel)
		{
			if (compatRequest == null)
			{
				return new SerializationActivityContext(MetadataSerializationMode.Tmdl, requiredMode, requiredLevel, false, false);
			}
			CompatibilityMode compatibilityMode = compatRequest.CompatibilityMode;
			int num = compatRequest.CompatibilityLevel;
			if (compatibilityMode == requiredMode)
			{
				if (num == -1)
				{
					num = int.MaxValue;
				}
				else if (num < requiredLevel)
				{
					throw new InvalidOperationException(TomSR.Exception_InvalidCompatRequestForTmdlSerialization(compatibilityMode.ToString(), num.ToString()));
				}
			}
			else if (compatibilityMode == CompatibilityMode.Unknown)
			{
				if (num == -1)
				{
					num = int.MaxValue;
				}
				else if (num < requiredLevel)
				{
					throw new InvalidOperationException(TomSR.Exception_InvalidCompatRequestForTmdlSerialization2(num.ToString()));
				}
				compatibilityMode = requiredMode;
			}
			else
			{
				int compatibilityRequirementLevel = rootObject.GetCompatibilityRequirementLevel(compatibilityMode);
				if (compatibilityRequirementLevel == -2 || (compatibilityRequirementLevel > num && num != -1))
				{
					throw new InvalidOperationException(TomSR.Exception_InvalidCompatRequestForTmdlSerialization(compatibilityMode.ToString(), num.ToString()));
				}
				if (num == -1)
				{
					num = int.MaxValue;
				}
			}
			return new SerializationActivityContext(MetadataSerializationMode.Tmdl, compatibilityMode, num, false, false);
		}

		// Token: 0x06001743 RID: 5955 RVA: 0x000A1358 File Offset: 0x0009F558
		public static SerializationActivityContext CreateContextBasedOnDeserializationOptions(bool raiseErrorOnUnresolvedLinks, MetadataCompatibilityOptions compatRequest)
		{
			if (compatRequest == null)
			{
				return new SerializationActivityContext(MetadataSerializationMode.Tmdl, CompatibilityMode.PowerBI, 1000000, false, raiseErrorOnUnresolvedLinks);
			}
			CompatibilityMode compatibilityMode = compatRequest.CompatibilityMode;
			if (compatibilityMode == CompatibilityMode.Unknown)
			{
				compatibilityMode = CompatibilityMode.PowerBI;
			}
			int num = compatRequest.CompatibilityLevel;
			if (num == -1)
			{
				num = 1000000;
			}
			return new SerializationActivityContext(MetadataSerializationMode.Tmdl, compatibilityMode, num, false, raiseErrorOnUnresolvedLinks);
		}

		// Token: 0x06001744 RID: 5956 RVA: 0x000A13A0 File Offset: 0x0009F5A0
		public static TmdlObject SerializeMetadataObjectToTmdlObject(TmdlSerializationConfiguration config, MetadataObject @object, SerializationActivityContext context)
		{
			if (context == null)
			{
				CompatibilityMode compatibilityMode;
				int num;
				TmdlSerializationHelper.GetMetadataObjectCompatibilityRestrictions(@object, out compatibilityMode, out num);
				context = new SerializationActivityContext(MetadataSerializationMode.Tmdl, compatibilityMode, num, false, false);
			}
			TmdlObjectWriter tmdlObjectWriter = new TmdlObjectWriter(config, @object.ObjectType);
			@object.SaveMetadata(context, tmdlObjectWriter);
			return tmdlObjectWriter.ExtractObject();
		}

		// Token: 0x06001745 RID: 5957 RVA: 0x000A13E4 File Offset: 0x0009F5E4
		public static TmdlObject SerializeDatabaseToTmdlObject<TDatabase>(TmdlSerializationConfiguration config, TDatabase db, SerializationActivityContext context) where TDatabase : Database, ISerializableTabularDatabase, new()
		{
			if (context == null)
			{
				CompatibilityMode compatibilityMode;
				int num;
				TmdlSerializationHelper.GetDatabaseCompatibilityRestrictions(db, db.Model, out compatibilityMode, out num);
				context = new SerializationActivityContext(MetadataSerializationMode.Tmdl, compatibilityMode, num, false, false);
			}
			TmdlObjectWriter tmdlObjectWriter = new TmdlObjectWriter(config, ObjectType.Database);
			db.SaveMetadata(context, tmdlObjectWriter);
			return tmdlObjectWriter.ExtractObject();
		}

		// Token: 0x06001746 RID: 5958 RVA: 0x000A1439 File Offset: 0x0009F639
		public static TMetadataObject DeserializeMetadataObjectFromTmdlObject<TMetadataObject>(TmdlObject tmdlObject, SerializationActivityContext context) where TMetadataObject : MetadataObject
		{
			return (TMetadataObject)((object)TmdlSerializationHelper.DeserializeMetadataObjectFromTmdlObject(typeof(TMetadataObject), tmdlObject, context));
		}

		// Token: 0x06001747 RID: 5959 RVA: 0x000A1454 File Offset: 0x0009F654
		public static MetadataObject DeserializeMetadataObjectFromTmdlObject(Type type, TmdlObject tmdlObject, SerializationActivityContext context)
		{
			if (context == null)
			{
				context = new SerializationActivityContext(MetadataSerializationMode.Tmdl, CompatibilityMode.PowerBI, 1000000, false, false);
			}
			TmdlObjectReader tmdlObjectReader = new TmdlObjectReader(tmdlObject);
			MetadataObject metadataObject = MetadataObject.CreateFromMetadataStream<MetadataObject>(context, tmdlObject.ObjectType, tmdlObjectReader);
			if (!type.IsAssignableFrom(metadataObject.GetType()))
			{
				throw new TmdlSerializationException(TomSR.Exception_MismatchTypeOnTmdlSerialization(type.FullName, metadataObject.GetType().FullName), default(TmdlSourceLocation));
			}
			context.TryReconstructMasterReferenceCrossLinkForRegistreredObjects(!context.RaiseErrorOnUnresolvedLinks);
			List<string> list = new List<string>();
			if (!metadataObject.TryResolveAllCrossLinksInTreeByObjectPath(list) && context.RaiseErrorOnUnresolvedLinks)
			{
				throw new TmdlSerializationException(TomSR.Exception_CannotDeserializeObjectResolvePathsFailedWithList(Utils.GetUserFriendlyNameOfObjectType(tmdlObject.ObjectType), ClientHostingManager.MarkAsRestrictedInformation(string.Join(", ", list), InfoRestrictionType.CCON)), (tmdlObject.ObjectType != ObjectType.Model) ? tmdlObject.SourceLocation : default(TmdlSourceLocation));
			}
			return metadataObject;
		}

		// Token: 0x06001748 RID: 5960 RVA: 0x000A1528 File Offset: 0x0009F728
		public static TDatabase DeserializeDatabaseFromTmdlProject<TDatabase>(TmdlProject project, SerializationActivityContext context) where TDatabase : Database, ISerializableTabularDatabase, new()
		{
			TmdlObject tmdlObject;
			TmdlObject tmdlObject2;
			project.Analyze(out tmdlObject, out tmdlObject2);
			if (context == null)
			{
				context = new SerializationActivityContext(MetadataSerializationMode.Tmdl, CompatibilityMode.PowerBI, 1000000, false, true);
			}
			TDatabase tdatabase = new TDatabase();
			if (tmdlObject == null)
			{
				Utils.Verify(tdatabase.CheckBody());
				Model model = TmdlSerializationHelper.DeserializeMetadataObjectFromTmdlObject<Model>(tmdlObject2, context);
				TmdlProperty tmdlProperty;
				if (tmdlObject2.TryGetDeprecatedProperty("id", out tmdlProperty))
				{
					if (tmdlProperty.Value != null && tmdlProperty.Value.Type == TmdlValueType.String)
					{
						TmdlStringValue tmdlStringValue = tmdlProperty.Value as TmdlStringValue;
						if (tmdlStringValue != null)
						{
							tdatabase.ID = tmdlStringValue.Lines.FirstOrDefault((string line) => !string.IsNullOrEmpty(line));
							goto IL_00E3;
						}
					}
					throw new TmdlSerializationException(TomSR.Exception_TmdlPropertyMismatchValueType("id"), tmdlProperty.SourceLocation.IsValid ? tmdlProperty.SourceLocation : tmdlObject2.SourceLocation);
				}
				IL_00E3:
				TmdlProperty tmdlProperty2;
				if (tmdlObject2.TryGetDeprecatedProperty("language", out tmdlProperty2))
				{
					if (tmdlProperty2.Value != null && tmdlProperty2.Value.Type == TmdlValueType.Scalar)
					{
						TmdlScalarValue<int> tmdlScalarValue = tmdlProperty2.Value as TmdlScalarValue<int>;
						if (tmdlScalarValue != null)
						{
							tdatabase.Language = tmdlScalarValue.GetValue().Value;
							goto IL_0168;
						}
					}
					throw new TmdlSerializationException(TomSR.Exception_TmdlPropertyMismatchValueType("language"), tmdlProperty2.SourceLocation.IsValid ? tmdlProperty2.SourceLocation : tmdlObject2.SourceLocation);
				}
				IL_0168:
				TmdlProperty tmdlProperty3;
				if (tmdlObject2.TryGetDeprecatedProperty("compatibilityLevel", out tmdlProperty3))
				{
					if (tmdlProperty3.Value != null && tmdlProperty3.Value.Type == TmdlValueType.Scalar)
					{
						TmdlScalarValue<int> tmdlScalarValue2 = tmdlProperty3.Value as TmdlScalarValue<int>;
						if (tmdlScalarValue2 != null)
						{
							int value = tmdlScalarValue2.GetValue().Value;
							int compatibilityRequirementLevel = model.GetCompatibilityRequirementLevel(CompatibilityMode.AnalysisServices);
							if (compatibilityRequirementLevel != -2 && compatibilityRequirementLevel <= value)
							{
								tdatabase.CompatibilityLevel = value;
								goto IL_0278;
							}
							tdatabase.CompatibilityLevel = value;
							tdatabase.CompatibilityMode = CompatibilityMode.PowerBI;
							goto IL_0278;
						}
					}
					throw new TmdlSerializationException(TomSR.Exception_TmdlPropertyMismatchValueType("compatibilityLevel"), tmdlProperty3.SourceLocation.IsValid ? tmdlProperty3.SourceLocation : tmdlObject2.SourceLocation);
				}
				int num = model.GetCompatibilityRequirementLevel(CompatibilityMode.AnalysisServices);
				if (num == -2)
				{
					num = model.GetCompatibilityRequirementLevel(CompatibilityMode.PowerBI);
					if (num > 1600)
					{
						tdatabase.CompatibilityLevel = num;
					}
					tdatabase.CompatibilityMode = CompatibilityMode.PowerBI;
				}
				else if (num > 1600)
				{
					tdatabase.CompatibilityLevel = num;
				}
				IL_0278:
				tdatabase.Model = model;
			}
			else
			{
				tmdlObject.Children.Add(tmdlObject2);
				TmdlObjectReader tmdlObjectReader = new TmdlObjectReader(tmdlObject);
				tdatabase.LoadMetadata(context, tmdlObjectReader);
				if (tdatabase.Model != null)
				{
					context.TryReconstructMasterReferenceCrossLinkForRegistreredObjects(!context.RaiseErrorOnUnresolvedLinks);
					List<string> list = new List<string>();
					if (!tdatabase.Model.TryResolveAllCrossLinksInTreeByObjectPath(list) && context.RaiseErrorOnUnresolvedLinks)
					{
						throw new TmdlSerializationException(TomSR.Exception_CannotDeserializeObjectResolvePathsFailedWithList(Utils.GetUserFriendlyNameOfObjectType(tmdlObject.ObjectType), ClientHostingManager.MarkAsRestrictedInformation(string.Join(", ", list), InfoRestrictionType.CCON)), default(TmdlSourceLocation));
					}
				}
			}
			return tdatabase;
		}

		// Token: 0x06001749 RID: 5961 RVA: 0x000A1854 File Offset: 0x0009FA54
		public static bool TryParseJsonObject(string json, out JToken token)
		{
			bool flag;
			try
			{
				token = JObject.Parse(json);
				flag = true;
			}
			catch (JsonReaderException)
			{
				token = null;
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600174A RID: 5962 RVA: 0x000A1888 File Offset: 0x0009FA88
		internal static IList<KeyValuePair<ObjectType, IList<TmdlObject>>> MergeAndGroupChildObject(IEnumerable<TmdlObject> objects, TmdlSourceLocation rootLocation)
		{
			List<KeyValuePair<ObjectType, IList<TmdlObject>>> list = new List<KeyValuePair<ObjectType, IList<TmdlObject>>>();
			using (IEnumerator<TmdlObject> enumerator = objects.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					TmdlObject @object = enumerator.Current;
					IList<TmdlObject> list2 = (from kvp in list
						where kvp.Key == @object.ObjectType
						select kvp.Value).FirstOrDefault<IList<TmdlObject>>();
					if (list2 == null)
					{
						list2 = new List<TmdlObject>();
						list.Add(new KeyValuePair<ObjectType, IList<TmdlObject>>(@object.ObjectType, list2));
					}
					if (ObjectTreeHelper.IsNamedObject(@object.ObjectType) || ObjectTreeHelper.IsKeyedObject(@object.ObjectType))
					{
						int num = list2.IndexOf(delegate(TmdlObject o)
						{
							if (@object.Name.IsEmpty)
							{
								return o.Name.IsEmpty;
							}
							return !o.Name.IsEmpty && string.Compare(@object.Name.Name, o.Name.Name, StringComparison.InvariantCulture) == 0;
						});
						if (num == -1)
						{
							list2.Add(@object);
						}
						else
						{
							list2[num].AddContentOf(@object);
						}
					}
					else if (@object.ObjectType == ObjectType.ChangedProperty)
					{
						string childProperty;
						if (@object.DefaultProperty != null)
						{
							if (@object.DefaultProperty.Value != null && @object.DefaultProperty.Value.Type == TmdlValueType.String)
							{
								TmdlStringValue tmdlStringValue = @object.DefaultProperty.Value as TmdlStringValue;
								if (tmdlStringValue != null)
								{
									childProperty = tmdlStringValue.RawValue;
									goto IL_01B9;
								}
							}
							throw new TmdlSerializationException(TomSR.Exception_TmdlPropertyMismatchType("property", TmdlValueType.String.ToString("G")), @object.DefaultProperty.SourceLocation.IsValid ? @object.DefaultProperty.SourceLocation : rootLocation);
						}
						childProperty = null;
						IL_01B9:
						int num2 = list2.IndexOf(delegate(TmdlObject o)
						{
							string childProperty2 = childProperty;
							TmdlProperty defaultProperty = o.DefaultProperty;
							return string.Compare(childProperty2, (defaultProperty != null) ? defaultProperty.Value.RawValue : null, StringComparison.InvariantCulture) == 0;
						});
						if (num2 == -1)
						{
							list2.Add(@object);
						}
						else
						{
							list2[num2].AddContentOf(@object);
						}
					}
					else if (@object.ObjectType == ObjectType.ExcludedArtifact)
					{
						TmdlProperty propertyByName = @object.GetPropertyByName("artifactType", StringComparison.Ordinal);
						int childArtifactType;
						if (propertyByName != null)
						{
							if (propertyByName.Value != null && propertyByName.Value.Type == TmdlValueType.Scalar)
							{
								TmdlScalarValue<int> tmdlScalarValue = propertyByName.Value as TmdlScalarValue<int>;
								if (tmdlScalarValue != null && tmdlScalarValue.GetValue() != null)
								{
									childArtifactType = tmdlScalarValue.GetValue().Value;
									goto IL_02BD;
								}
							}
							throw new TmdlSerializationException(TomSR.Exception_TmdlPropertyMismatchType("artifactType", "Integer"), propertyByName.SourceLocation.IsValid ? propertyByName.SourceLocation : rootLocation);
						}
						childArtifactType = -1;
						IL_02BD:
						int num3 = list2.IndexOf(delegate(TmdlObject o)
						{
							TmdlProperty propertyByName2 = o.GetPropertyByName("artifactType", StringComparison.Ordinal);
							int childArtifactType2 = childArtifactType;
							int? value = ((TmdlScalarValue<int>)propertyByName2.Value).GetValue();
							return (childArtifactType2 == value.GetValueOrDefault()) & (value != null);
						});
						if (num3 == -1)
						{
							list2.Add(@object);
						}
						else
						{
							list2[num3].AddContentOf(@object);
						}
					}
					else if (list2.Count > 0)
					{
						list2[0].AddContentOf(@object);
					}
					else
					{
						list2.Add(@object);
					}
				}
			}
			return list;
		}

		// Token: 0x04000444 RID: 1092
		private static readonly IMetadataFilter defaultFilter = new TmdlSerializationHelper.DefaultMetadataFilter();

		// Token: 0x04000445 RID: 1093
		private static IMetadataFilter ignoreChildrenFilter;

		// Token: 0x02000366 RID: 870
		public static class TmdlPropertyName
		{
			// Token: 0x02000467 RID: 1127
			public static class Partition
			{
				// Token: 0x040014A5 RID: 5285
				public const string SourceType = "sourceType";
			}

			// Token: 0x02000468 RID: 1128
			public static class RoleMember
			{
				// Token: 0x040014A6 RID: 5286
				public const string WindowsMember = "adMember";

				// Token: 0x040014A7 RID: 5287
				public const string ExternalMember = "external";

				// Token: 0x040014A8 RID: 5288
				public const string ExternalUser = "user";

				// Token: 0x040014A9 RID: 5289
				public const string ExternalGroup = "group";
			}

			// Token: 0x02000469 RID: 1129
			public static class CustomJsonProperty
			{
				// Token: 0x040014AA RID: 5290
				public const string AdditionalProperties = "additionalProperties";
			}
		}

		// Token: 0x02000367 RID: 871
		public static class TmdlPropertyDefaultValue
		{
			// Token: 0x0200046A RID: 1130
			public static class RoleMember
			{
				// Token: 0x040014AB RID: 5291
				public const string IdentityProvider = "AzureAD";
			}
		}

		// Token: 0x02000368 RID: 872
		public static class Defaults
		{
			// Token: 0x04000EF7 RID: 3831
			public const string Extension = ".tmdl";

			// Token: 0x04000EF8 RID: 3832
			internal const string BackCompatExtension = ".tmd";

			// Token: 0x04000EF9 RID: 3833
			public const CompatibilityMode DefaultParserCompatibilityMode = CompatibilityMode.PowerBI;

			// Token: 0x04000EFA RID: 3834
			public const int DefaultParserCompatibilityLevel = 1000000;
		}

		// Token: 0x02000369 RID: 873
		private sealed class DefaultMetadataFilter : IMetadataFilter
		{
			// Token: 0x060026BC RID: 9916 RVA: 0x000EBAD4 File Offset: 0x000E9CD4
			public bool IgnoreProperty(ObjectType owner, string propertyName, MetadataPropertyNature propertyNature)
			{
				if ((propertyNature & MetadataPropertyNature.Inferred) != MetadataPropertyNature.Inferred)
				{
					if (owner != ObjectType.Table)
					{
						if (owner == ObjectType.PerspectiveTable)
						{
							if (string.Compare(propertyName, "sets", StringComparison.Ordinal) == 0)
							{
								return true;
							}
						}
					}
					else if (string.Compare(propertyName, "sets", StringComparison.Ordinal) == 0)
					{
						return true;
					}
					return false;
				}
				if ((propertyNature & MetadataPropertyNature.Timestamp) == MetadataPropertyNature.Timestamp)
				{
					return true;
				}
				MetadataPropertyNature metadataPropertyNature = propertyNature & MetadataPropertyNature.PropertyCategoryMask;
				if (metadataPropertyNature != MetadataPropertyNature.RegularProperty)
				{
					return metadataPropertyNature != MetadataPropertyNature.ChildCollection;
				}
				if (owner != ObjectType.Column)
				{
					if (owner == ObjectType.Measure)
					{
						if (string.Compare(propertyName, "dataType", StringComparison.Ordinal) == 0)
						{
							return false;
						}
					}
				}
				else if (string.Compare(propertyName, "isDataTypeInferred", StringComparison.Ordinal) == 0 || string.Compare(propertyName, "isNameInferred", StringComparison.Ordinal) == 0)
				{
					return false;
				}
				return true;
			}

			// Token: 0x060026BD RID: 9917 RVA: 0x000EBB78 File Offset: 0x000E9D78
			public bool IgnoreChild(ObjectType owner, string propertyName, MetadataPropertyNature propertyNature, MetadataObject @object)
			{
				return ObjectTreeHelper.IsInferredObject(@object);
			}
		}
	}
}
