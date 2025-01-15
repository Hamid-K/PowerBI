using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AnalysisServices.Core;
using Microsoft.AnalysisServices.Hosting;
using Microsoft.AnalysisServices.Tabular.DataRefresh;
using Microsoft.AnalysisServices.Tabular.Extensions;
using Microsoft.AnalysisServices.Tabular.Json;
using Microsoft.AnalysisServices.Tabular.Metadata;
using Microsoft.AnalysisServices.Tabular.Utilities;

namespace Microsoft.AnalysisServices.Tabular.Scripting
{
	// Token: 0x020001AB RID: 427
	internal sealed class RefreshJsonCommand : JsonCommand
	{
		// Token: 0x06001A2D RID: 6701 RVA: 0x000AD398 File Offset: 0x000AB598
		public RefreshJsonCommand(NamedMetadataObject @object, RefreshType refreshType)
			: this()
		{
			this.refreshType = new RefreshType?(refreshType);
			this.objectPaths.Add(ObjectPath.CreateDbQualifiedPath(@object.GetPath(null), @object.Model.Database.Name));
			this.isCommandValid = true;
			base.ObjectType = @object.ObjectType;
		}

		// Token: 0x06001A2E RID: 6702 RVA: 0x000AD3F4 File Offset: 0x000AB5F4
		public RefreshJsonCommand(NamedMetadataObject @object, RefreshType refreshType, RefreshPolicyBehavior behavior)
			: this()
		{
			this.refreshType = new RefreshType?(refreshType);
			this.applyRefreshPolicy = new bool?(behavior != RefreshPolicyBehavior.Ignore && Utils.CanApplyRefreshPolicies(refreshType));
			this.objectPaths.Add(ObjectPath.CreateDbQualifiedPath(@object.GetPath(null), @object.Model.Database.Name));
			this.isCommandValid = true;
			base.ObjectType = @object.ObjectType;
		}

		// Token: 0x06001A2F RID: 6703 RVA: 0x000AD468 File Offset: 0x000AB668
		public RefreshJsonCommand(NamedMetadataObject @object, RefreshType refreshType, DateTime effectiveDate)
			: this()
		{
			this.refreshType = new RefreshType?(refreshType);
			this.applyRefreshPolicy = new bool?(true);
			this.effectiveDate = new DateTime?(effectiveDate);
			this.objectPaths.Add(ObjectPath.CreateDbQualifiedPath(@object.GetPath(null), @object.Model.Database.Name));
			this.isCommandValid = true;
			base.ObjectType = @object.ObjectType;
		}

		// Token: 0x06001A30 RID: 6704 RVA: 0x000AD4DC File Offset: 0x000AB6DC
		public RefreshJsonCommand(IEnumerable<NamedMetadataObject> objects, RefreshType refreshType)
			: this()
		{
			this.refreshType = new RefreshType?(refreshType);
			foreach (NamedMetadataObject namedMetadataObject in objects)
			{
				this.objectPaths.Add(ObjectPath.CreateDbQualifiedPath(namedMetadataObject.GetPath(null), namedMetadataObject.Model.Database.Name));
			}
			this.isCommandValid = true;
			base.ObjectType = JsonCommand.CalculateTargetObjectType(this.objectPaths);
		}

		// Token: 0x06001A31 RID: 6705 RVA: 0x000AD570 File Offset: 0x000AB770
		public RefreshJsonCommand(IEnumerable<NamedMetadataObject> objects, RefreshType refreshType, RefreshPolicyBehavior behavior)
			: this()
		{
			this.refreshType = new RefreshType?(refreshType);
			this.applyRefreshPolicy = new bool?(behavior != RefreshPolicyBehavior.Ignore && Utils.CanApplyRefreshPolicies(refreshType));
			foreach (NamedMetadataObject namedMetadataObject in objects)
			{
				this.objectPaths.Add(ObjectPath.CreateDbQualifiedPath(namedMetadataObject.GetPath(null), namedMetadataObject.Model.Database.Name));
			}
			this.isCommandValid = true;
			base.ObjectType = JsonCommand.CalculateTargetObjectType(this.objectPaths);
		}

		// Token: 0x06001A32 RID: 6706 RVA: 0x000AD61C File Offset: 0x000AB81C
		public RefreshJsonCommand(IEnumerable<NamedMetadataObject> objects, RefreshType refreshType, DateTime effectiveDate)
			: this()
		{
			this.refreshType = new RefreshType?(refreshType);
			this.applyRefreshPolicy = new bool?(true);
			foreach (NamedMetadataObject namedMetadataObject in objects)
			{
				this.objectPaths.Add(ObjectPath.CreateDbQualifiedPath(namedMetadataObject.GetPath(null), namedMetadataObject.Model.Database.Name));
			}
			this.isCommandValid = true;
			base.ObjectType = JsonCommand.CalculateTargetObjectType(this.objectPaths);
		}

		// Token: 0x06001A33 RID: 6707 RVA: 0x000AD6BC File Offset: 0x000AB8BC
		public RefreshJsonCommand(Database database, RefreshType refreshType)
			: this()
		{
			this.refreshType = new RefreshType?(refreshType);
			this.objectPaths.Add(new ObjectPath(ObjectType.Database, database.Name));
			this.isCommandValid = true;
			base.ObjectType = ObjectType.Database;
		}

		// Token: 0x06001A34 RID: 6708 RVA: 0x000AD708 File Offset: 0x000AB908
		public RefreshJsonCommand(Database database, RefreshType refreshType, RefreshPolicyBehavior behavior)
			: this()
		{
			this.refreshType = new RefreshType?(refreshType);
			this.applyRefreshPolicy = new bool?(behavior != RefreshPolicyBehavior.Ignore);
			this.objectPaths.Add(new ObjectPath(ObjectType.Database, database.Name));
			this.isCommandValid = true;
			base.ObjectType = ObjectType.Database;
		}

		// Token: 0x06001A35 RID: 6709 RVA: 0x000AD768 File Offset: 0x000AB968
		public RefreshJsonCommand(Database database, RefreshType refreshType, DateTime effectiveDate)
			: this()
		{
			this.refreshType = new RefreshType?(refreshType);
			this.applyRefreshPolicy = new bool?(true);
			this.effectiveDate = new DateTime?(effectiveDate);
			this.objectPaths.Add(new ObjectPath(ObjectType.Database, database.Name));
			this.isCommandValid = true;
			base.ObjectType = ObjectType.Database;
		}

		// Token: 0x06001A36 RID: 6710 RVA: 0x000AD7CC File Offset: 0x000AB9CC
		internal RefreshJsonCommand()
			: base(JsonCommandType.Refresh, "refresh", "Refresh", true, false, true, false)
		{
			this.objectPaths = new List<ObjectPath>();
		}

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x06001A37 RID: 6711 RVA: 0x000AD7EE File Offset: 0x000AB9EE
		public RefreshType? RefreshType
		{
			get
			{
				return this.refreshType;
			}
		}

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x06001A38 RID: 6712 RVA: 0x000AD7F6 File Offset: 0x000AB9F6
		public bool? ApplyRefreshPolicy
		{
			get
			{
				return this.applyRefreshPolicy;
			}
		}

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x06001A39 RID: 6713 RVA: 0x000AD7FE File Offset: 0x000AB9FE
		public DateTime? EffectiveDate
		{
			get
			{
				return this.effectiveDate;
			}
		}

		// Token: 0x06001A3A RID: 6714 RVA: 0x000AD808 File Offset: 0x000ABA08
		internal override void ApplyChangesLocally(Server server, Database activeDB)
		{
			Utils.Verify(activeDB != null);
			Model model = activeDB.Model;
			if (this.overrides != null)
			{
				this.ValidateOverrides(model);
			}
			foreach (ObjectPath objectPath in this.objectPaths)
			{
				Utils.Verify(objectPath.First<KeyValuePair<ObjectType, string>>().Key == ObjectType.Database);
				string value = objectPath.First<KeyValuePair<ObjectType, string>>().Value;
				if (server.Databases.FindByName(value) != activeDB)
				{
					throw new TomException(TomSR.Exception_JsonCommandRefreshMultipleDbsNotSupported);
				}
				this.RequestRefreshObject(objectPath, model);
			}
			this.modelToSave = model;
		}

		// Token: 0x06001A3B RID: 6715 RVA: 0x000AD8C8 File Offset: 0x000ABAC8
		internal override JsonCommandResult ExecuteCommandWithoutCaptureXML(Server server)
		{
			Utils.Verify(!server.CaptureXml, "Server must not be in capturing mode");
			return new JsonCommandResult
			{
				ModelOperationResult = this.modelToSave.SaveChangesImpl(SaveFlags.Default, (base.MaxParallelism != null) ? base.MaxParallelism.Value : 0)
			};
		}

		// Token: 0x06001A3C RID: 6716 RVA: 0x000AD920 File Offset: 0x000ABB20
		internal override XmlaScript GenerateXmlaScript(Server server)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001A3D RID: 6717 RVA: 0x000AD928 File Offset: 0x000ABB28
		private protected override void ParseProperties(JsonTextReader reader, CompatibilityMode mode)
		{
			while (reader.TokenType != 13)
			{
				reader.VerifyToken(4);
				string text = (string)reader.Value;
				if (!(text == "type"))
				{
					if (!(text == "applyRefreshPolicy"))
					{
						if (!(text == "effectiveDate"))
						{
							if (!(text == "objects"))
							{
								if (!(text == "overrides"))
								{
									throw JsonSerializationUtil.CreateException(TomSR.Exception_UnexpectedJsonProperty(text), reader, null);
								}
								reader.Read();
								this.overrides = new List<OverrideCollection>();
								JsonPropertyHelper.ParseArrayOfObjects(reader, delegate(JsonTextReader innerReader)
								{
									innerReader.VerifyToken(1);
									OverrideCollection overrideCollection = new OverrideCollection();
									overrideCollection.ReadFromJson(innerReader);
									this.overrides.Add(overrideCollection);
								});
							}
							else
							{
								reader.Read();
								JsonPropertyHelper.ParseArrayOfObjects(reader, delegate(JsonTextReader innerReader)
								{
									innerReader.VerifyToken(1);
									ObjectPath objectPath = ObjectPath.Parse(innerReader);
									objectPath.Normalize();
									if (objectPath.Count == 0)
									{
										throw new TomException(TomSR.Exception_JsonCommandObjectNotSpecified(base.CommandName));
									}
									if (objectPath[0].Key != ObjectType.Database)
									{
										throw new TomException(TomSR.Exception_JsonCommandDatabaseNotSpecified(base.CommandName));
									}
									this.objectPaths.Add(objectPath);
									innerReader.VerifyToken(13);
									innerReader.Read();
								});
								base.ObjectType = JsonCommand.CalculateTargetObjectType(this.objectPaths);
							}
						}
						else
						{
							reader.Read();
							reader.VerifyToken(9);
							string text2 = (string)reader.Value;
							this.effectiveDate = new DateTime?(JsonPropertyHelper.ConvertJsonValueToPrimitive<DateTime>(text2));
							reader.Read();
						}
					}
					else
					{
						reader.Read();
						reader.VerifyToken(10);
						this.applyRefreshPolicy = new bool?((bool)reader.Value);
						reader.Read();
					}
				}
				else
				{
					reader.Read();
					reader.VerifyToken(9);
					string text3 = (string)reader.Value;
					this.refreshType = new RefreshType?(JsonPropertyHelper.ConvertStringToEnum<RefreshType>(text3, reader));
					reader.Read();
				}
			}
		}

		// Token: 0x06001A3E RID: 6718 RVA: 0x000ADAB0 File Offset: 0x000ABCB0
		private protected override void ValidateProperties()
		{
			if (this.refreshType == null)
			{
				throw new TomException(TomSR.Exception_JsonCommandTypeNotSpecified(base.CommandName));
			}
			if (this.effectiveDate != null)
			{
				if (this.applyRefreshPolicy == null)
				{
					throw new TomException(TomSR.Exception_JsonCommandRefreshPolicyParameterMissing(base.CommandName));
				}
				if (!Utils.CanApplyRefreshPolicies(this.refreshType.Value))
				{
					throw new TomException(TomSR.Exception_JsonCommandRefreshPolicyNotSupportForRefreshType(base.CommandName, this.refreshType.Value.ToString()));
				}
			}
			if (this.objectPaths.Count == 0)
			{
				throw new TomException(TomSR.Exception_JsonCommandObjectNotSpecified(base.CommandName));
			}
			using (List<ObjectPath>.Enumerator enumerator = this.objectPaths.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsEmpty)
					{
						throw new TomException(TomSR.Exception_JsonCommandObjectNotSpecified(base.CommandName));
					}
				}
			}
		}

		// Token: 0x06001A3F RID: 6719 RVA: 0x000ADBB8 File Offset: 0x000ABDB8
		private protected override void WriteProperties(JsonWriter writer)
		{
			writer.WritePropertyName("type");
			writer.WriteValue(JsonPropertyHelper.ConvertEnumToJsonValue<RefreshType>(this.refreshType.Value));
			if (this.applyRefreshPolicy != null)
			{
				writer.WritePropertyName("applyRefreshPolicy");
				writer.WriteValue(this.applyRefreshPolicy.Value);
			}
			if (this.effectiveDate != null)
			{
				writer.WritePropertyName("effectiveDate");
				writer.WriteValue(this.effectiveDate.Value.ToUniversalTime().ToString("d").ToJsonCase());
			}
			writer.WritePropertyName("objects");
			writer.WriteStartArray();
			foreach (ObjectPath objectPath in this.objectPaths)
			{
				objectPath.Write(writer);
			}
			writer.WriteEndArray();
			List<OverrideCollection> list = this.overrides;
		}

		// Token: 0x06001A40 RID: 6720 RVA: 0x000ADCB4 File Offset: 0x000ABEB4
		private protected override void WritePropertiesSchema(JsonWriter writer, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WritePropertyName("type");
			writer.WriteStartObject();
			writer.WritePropertyName("enum");
			writer.WriteStartArray();
			writer.WriteValue("full");
			writer.WriteValue("clearValues");
			writer.WriteValue("calculate");
			writer.WriteValue("dataOnly");
			writer.WriteValue("automatic");
			writer.WriteValue("add");
			writer.WriteValue("defragment");
			writer.WriteEndArray();
			writer.WriteEndObject();
			writer.WritePropertyName("applyRefreshPolicy");
			JsonSchemaWriter.WriteSchemaForBoolean(writer);
			writer.WritePropertyName("effectiveDate");
			JsonSchemaWriter.WriteSchemaForDateTime(writer);
			writer.WritePropertyName("objects");
			writer.WriteStartObject();
			writer.WritePropertyName("type");
			writer.WriteValue("array");
			writer.WritePropertyName("items");
			writer.WriteStartObject();
			writer.WritePropertyName("anyOf");
			writer.WriteStartArray();
			JsonSchemaWriter.WriteSchemaForObjectPath(writer, ObjectType.Database, true);
			JsonSchemaWriter.WriteSchemaForObjectPath(writer, ObjectType.Table, true);
			JsonSchemaWriter.WriteSchemaForObjectPath(writer, ObjectType.Partition, true);
			writer.WriteEndArray();
			writer.WriteEndObject();
			writer.WriteEndObject();
			writer.WritePropertyName("overrides");
			writer.WriteStartObject();
			writer.WritePropertyName("type");
			writer.WriteValue("array");
			writer.WritePropertyName("items");
			OverrideCollection.WriteSchema(writer, JsonCommand.JsonSerializationOptions.ObjectWithDescendants, mode, dbCompatibilityLevel);
			writer.WriteEndObject();
		}

		// Token: 0x06001A41 RID: 6721 RVA: 0x000ADE1C File Offset: 0x000AC01C
		private protected override string GetAffectedDatabaseName()
		{
			return this.objectPaths[0].SingleOrDefault((KeyValuePair<ObjectType, string> e) => e.Key == ObjectType.Database).Value;
		}

		// Token: 0x06001A42 RID: 6722 RVA: 0x000ADE64 File Offset: 0x000AC064
		private void ValidateOverrides(Model model)
		{
			foreach (OverrideCollection overrideCollection in this.overrides)
			{
				if (overrideCollection.ScopePath != null)
				{
					if (overrideCollection.ScopePath.IsEmpty)
					{
						throw new TomException(TomSR.Exception_OverridesScopeObjectIsEmpty);
					}
					overrideCollection.ScopePath.Normalize();
					ObjectType key = overrideCollection.ScopePath.Last<KeyValuePair<ObjectType, string>>().Key;
					MetadataObject metadataObject = ObjectTreeHelper.LocateObjectByPath(overrideCollection.ScopePath, model);
					if (metadataObject == null)
					{
						throw new TomException(TomSR.Exception_OverridesScopeObjectCannotBeFound(Utils.GetUserFriendlyNameOfObjectType(key)));
					}
					if (!ObjectTreeHelper.SupportsRefresh(metadataObject.ObjectType))
					{
						throw new TomException(TomSR.Exception_OverridesScopeObjectDoesntSupportRefresh(Utils.GetUserFriendlyNameOfObjectType(metadataObject.ObjectType)));
					}
					overrideCollection.Scope = metadataObject;
				}
				foreach (IObjectOverride objectOverride in this.overrides.SelectMany((OverrideCollection b) => b.Columns.Cast<IObjectOverride>().Concat(b.DataSources).Concat(b.Partitions)
					.Concat(b.Expressions)))
				{
					objectOverride.EnsureAllReferencesResolved(model);
				}
			}
		}

		// Token: 0x06001A43 RID: 6723 RVA: 0x000ADFC0 File Offset: 0x000AC1C0
		private void RequestRefreshObject(ObjectPath path, Model model)
		{
			ObjectType key = path.Last<KeyValuePair<ObjectType, string>>().Key;
			MetadataObject metadataObject = ObjectTreeHelper.LocateObjectByPath(path, model);
			if (metadataObject == null)
			{
				throw new TomException(TomSR.Exception_JsonCommandCannotFindObject(base.CommandName, Utils.GetUserFriendlyNameOfObjectType(key), ClientHostingManager.MarkAsRestrictedInformation(path.ToString(), InfoRestrictionType.CCON)));
			}
			if (key != ObjectType.Database && (!ObjectTreeHelper.SupportsScriptOut(metadataObject.ObjectType) || !ObjectTreeHelper.SupportsRefresh(metadataObject.ObjectType)))
			{
				throw new TomException(TomSR.Exception_JsonCommandRefreshNotSupportedForObjectType(Utils.GetUserFriendlyNameOfObjectType(key)));
			}
			if (this.applyRefreshPolicy != null && key != ObjectType.Database && key != ObjectType.Model && key != ObjectType.Table)
			{
				throw new TomException(TomSR.Exception_JsonCommandRefreshPolicyNotSupportedForObjectType(Utils.GetUserFriendlyNameOfObjectType(key)));
			}
			ObjectType objectType;
			if (this.applyRefreshPolicy == null)
			{
				objectType = metadataObject.ObjectType;
				if (objectType <= ObjectType.Table)
				{
					if (objectType != ObjectType.Model)
					{
						if (objectType != ObjectType.Table)
						{
							goto IL_0223;
						}
						((Table)metadataObject).RequestRefresh(this.refreshType.Value, this.overrides);
						return;
					}
				}
				else
				{
					if (objectType == ObjectType.Partition)
					{
						((Partition)metadataObject).RequestRefresh(this.refreshType.Value, this.overrides);
						return;
					}
					if (objectType != ObjectType.Database)
					{
						goto IL_0223;
					}
				}
				((Model)metadataObject).RequestRefresh(this.refreshType.Value, this.overrides);
				return;
				IL_0223:
				throw new TomInternalException("unreachable code path");
			}
			if (this.effectiveDate != null)
			{
				objectType = metadataObject.ObjectType;
				if (objectType != ObjectType.Model)
				{
					if (objectType == ObjectType.Table)
					{
						((Table)metadataObject).RequestRefresh(this.refreshType.Value, this.overrides, this.effectiveDate.Value);
						return;
					}
					if (objectType != ObjectType.Database)
					{
						throw new TomInternalException("unreachable code path");
					}
				}
				((Model)metadataObject).RequestRefresh(this.refreshType.Value, this.overrides, this.effectiveDate.Value);
				return;
			}
			objectType = metadataObject.ObjectType;
			if (objectType != ObjectType.Model)
			{
				if (objectType == ObjectType.Table)
				{
					((Table)metadataObject).RequestRefresh(this.refreshType.Value, this.overrides, this.applyRefreshPolicy.Value ? RefreshPolicyBehavior.Default : RefreshPolicyBehavior.Ignore);
					return;
				}
				if (objectType != ObjectType.Database)
				{
					throw new TomInternalException("unreachable code path");
				}
			}
			((Model)metadataObject).RequestRefresh(this.refreshType.Value, this.overrides, this.applyRefreshPolicy.Value ? RefreshPolicyBehavior.Default : RefreshPolicyBehavior.Ignore);
		}

		// Token: 0x06001A44 RID: 6724 RVA: 0x000AE1FC File Offset: 0x000AC3FC
		[Conditional("DEBUG")]
		private static void ValidateObjectsCollectionForRefresh(IEnumerable<NamedMetadataObject> objects)
		{
			Database database = null;
			foreach (NamedMetadataObject namedMetadataObject in objects)
			{
				if (database == null)
				{
					database = namedMetadataObject.Model.Database;
				}
			}
		}

		// Token: 0x04000506 RID: 1286
		private RefreshType? refreshType;

		// Token: 0x04000507 RID: 1287
		private bool? applyRefreshPolicy;

		// Token: 0x04000508 RID: 1288
		private DateTime? effectiveDate;

		// Token: 0x04000509 RID: 1289
		private List<ObjectPath> objectPaths;

		// Token: 0x0400050A RID: 1290
		private List<OverrideCollection> overrides;

		// Token: 0x0400050B RID: 1291
		private Model modelToSave;
	}
}
