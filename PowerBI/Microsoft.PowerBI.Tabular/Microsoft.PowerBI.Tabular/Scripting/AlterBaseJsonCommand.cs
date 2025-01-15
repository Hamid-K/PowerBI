using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AnalysisServices.Core;
using Microsoft.AnalysisServices.Hosting;
using Microsoft.AnalysisServices.Tabular.Extensions;
using Microsoft.AnalysisServices.Tabular.Json;
using Microsoft.AnalysisServices.Tabular.Metadata;
using Microsoft.AnalysisServices.Tabular.Serialization;
using Microsoft.AnalysisServices.Tabular.Utilities;

namespace Microsoft.AnalysisServices.Tabular.Scripting
{
	// Token: 0x0200019C RID: 412
	internal abstract class AlterBaseJsonCommand : JsonCommand
	{
		// Token: 0x0600196F RID: 6511 RVA: 0x000A8F1C File Offset: 0x000A711C
		private protected AlterBaseJsonCommand(JsonCommandType commandType, string commandTag, string commandNameInSchema, bool allowCreate, CopyFlags copyFlags, UpdateOptions updateOptions, NamedMetadataObject @object)
			: this(commandType, commandTag, commandNameInSchema, allowCreate, copyFlags, updateOptions)
		{
			this.objectPath = ObjectPath.CreateDbQualifiedPath(@object.GetPath(null), @object.Model.Database.Name);
			this.objectDefinition = @object;
			base.ObjectType = @object.ObjectType;
		}

		// Token: 0x06001970 RID: 6512 RVA: 0x000A8F71 File Offset: 0x000A7171
		private protected AlterBaseJsonCommand(JsonCommandType commandType, string commandTag, string commandNameInSchema, bool allowCreate, CopyFlags copyFlags, UpdateOptions updateOptions, Database database)
			: this(commandType, commandTag, commandNameInSchema, allowCreate, copyFlags, updateOptions)
		{
			this.objectPath = new ObjectPath(ObjectType.Database, database.Name);
			this.databaseDefinition = database;
			base.ObjectType = ObjectType.Database;
		}

		// Token: 0x06001971 RID: 6513 RVA: 0x000A8FAC File Offset: 0x000A71AC
		private protected AlterBaseJsonCommand(JsonCommandType commandType, string commandTag, string commandNameInSchema, bool allowCreate, CopyFlags copyFlags, UpdateOptions updateOptions)
			: base(commandType, commandTag, commandNameInSchema, true, true, false, true)
		{
			base.ObjectType = ObjectType.Null;
			this.allowCreate = allowCreate;
			this.copyFlags = copyFlags;
			this.updateOptions = updateOptions;
		}

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x06001972 RID: 6514 RVA: 0x000A8FDA File Offset: 0x000A71DA
		internal SerializeOptions JsonSerializeOptions
		{
			get
			{
				if (this.jsonSerializeOptions == null)
				{
					this.jsonSerializeOptions = this.GetJsonSerializeOptions().Clone();
				}
				return this.jsonSerializeOptions;
			}
		}

		// Token: 0x06001973 RID: 6515 RVA: 0x000A8FFB File Offset: 0x000A71FB
		internal override void ApplyChangesLocally(Server server, Database activeDB)
		{
			base.EnsureNoTMDatabaseHasLocalChanges(server, activeDB);
			if (this.objectDefinition != null)
			{
				this.ApplyTabularObjectChanges(server, activeDB);
				return;
			}
			this.ApplyDatabaseChanges(server, activeDB);
		}

		// Token: 0x06001974 RID: 6516 RVA: 0x000A9020 File Offset: 0x000A7220
		internal override XmlaScript GenerateXmlaScript(Server server)
		{
			Utils.Verify(!server.CaptureXml && server.CaptureLog.Count == 0, "Server must not be in capturing mode, and can't have any cached XMLA commands!");
			if (this.fallbackCreateCommand != null)
			{
				return this.fallbackCreateCommand.GenerateXmlaScript(server);
			}
			if (this.databaseToUpdate != null)
			{
				return this.GenerateXmlaScriptForDbUpdate(server);
			}
			return this.GenerateXmlaScriptForModelSave(server);
		}

		// Token: 0x06001975 RID: 6517 RVA: 0x000A907C File Offset: 0x000A727C
		private protected override void ParseProperties(JsonTextReader reader, CompatibilityMode mode)
		{
			while (reader.TokenType != 13)
			{
				reader.VerifyToken(4);
				string text = (string)reader.Value;
				if (!(text == "object"))
				{
					if (!(text == "database"))
					{
						if (!(text == "dataSource"))
						{
							if (!(text == "table"))
							{
								if (!(text == "partition"))
								{
									if (!(text == "role"))
									{
										throw JsonSerializationUtil.CreateException(TomSR.Exception_UnexpectedJsonProperty(text), reader, null);
									}
									this.ParseObjectDefinition(ObjectType.Role, reader, mode);
								}
								else
								{
									this.ParseObjectDefinition(ObjectType.Partition, reader, mode);
								}
							}
							else
							{
								this.ParseObjectDefinition(ObjectType.Table, reader, mode);
							}
						}
						else
						{
							this.ParseObjectDefinition(ObjectType.DataSource, reader, mode);
						}
					}
					else
					{
						this.ParseDatabaseDefinition(reader, mode);
					}
				}
				else
				{
					reader.Read();
					reader.VerifyToken(1);
					this.objectPath = ObjectPath.Parse(reader);
					reader.VerifyToken(13);
					reader.Read();
				}
			}
		}

		// Token: 0x06001976 RID: 6518 RVA: 0x000A9170 File Offset: 0x000A7370
		private protected override void ValidateProperties()
		{
			if (this.objectPath == null || this.objectPath.IsEmpty)
			{
				throw new TomException(TomSR.Exception_JsonCommandObjectNotSpecified(base.CommandName));
			}
			if (this.objectDefinition == null && this.databaseDefinition == null)
			{
				throw new TomException(TomSR.Exception_JsonCommandObjectDefinitionNotSpecified(base.CommandName));
			}
		}

		// Token: 0x06001977 RID: 6519 RVA: 0x000A91C4 File Offset: 0x000A73C4
		private protected override void WriteProperties(JsonWriter writer)
		{
			writer.WritePropertyName("object");
			this.objectPath.Write(writer);
			if (this.objectDefinition != null)
			{
				writer.WritePropertyName(JsonPropertyName.ObjectPath.GetObjectPathPropertyName(this.objectDefinition.ObjectType));
				writer.WriteRawJson(JsonSerializer.SerializeObjectImpl(this.objectDefinition, this.jsonSerializeOptions ?? this.GetJsonSerializeOptions(), -3, CompatibilityMode.Unknown));
				return;
			}
			writer.WritePropertyName("database");
			writer.WriteRawJson(JsonSerializer.SerializeDatabase(this.databaseDefinition, this.jsonSerializeOptions ?? this.GetJsonSerializeOptions()));
		}

		// Token: 0x06001978 RID: 6520 RVA: 0x000A9258 File Offset: 0x000A7458
		private protected override void WritePropertiesSchema(JsonWriter writer, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue(string.Format("Parameters of {0} command of Analysis Services JSON API", base.CommandNameInSchema));
			writer.WritePropertyName("anyOf");
			writer.WriteStartArray();
			this.WriteSchemaForParametersOfAlterDatabase(writer, mode, dbCompatibilityLevel);
			this.WriteSchemaForParametersOfAlterDataSource(writer, mode, dbCompatibilityLevel);
			this.WriteSchemaForParametersOfAlterTable(writer, mode, dbCompatibilityLevel);
			this.WriteSchemaForParametersOfAlterPartition(writer, mode, dbCompatibilityLevel);
			this.WriteSchemaForParametersOfAlterRole(writer, mode, dbCompatibilityLevel);
			writer.WriteEndArray();
			writer.WriteEndObject();
		}

		// Token: 0x06001979 RID: 6521
		private protected abstract SerializeOptions GetJsonSerializeOptions();

		// Token: 0x0600197A RID: 6522
		private protected abstract DeserializeOptions GetJsonDeserializeOptions();

		// Token: 0x0600197B RID: 6523 RVA: 0x000A92D8 File Offset: 0x000A74D8
		private protected override string GetAffectedDatabaseName()
		{
			return this.objectPath.SingleOrDefault((KeyValuePair<ObjectType, string> e) => e.Key == ObjectType.Database).Value;
		}

		// Token: 0x0600197C RID: 6524 RVA: 0x000A9318 File Offset: 0x000A7518
		private void ParseObjectDefinition(ObjectType objectType, JsonTextReader reader, CompatibilityMode mode)
		{
			this.VerifyObjectDefinitionWasNotRead(reader);
			if (!ObjectTreeHelper.SupportsScriptOut(objectType))
			{
				throw new TomException(TomSR.Exception_JsonCommandAlterNotSupportedForObjectType(Utils.GetUserFriendlyNameOfObjectType(objectType)));
			}
			base.ObjectType = objectType;
			reader.Read();
			reader.VerifyToken(1);
			this.objectDefinition = JsonPropertyHelper.ParseObjectFromJson(objectType, reader, this.GetJsonDeserializeOptions(), mode, int.MaxValue);
			reader.VerifyToken(13);
			reader.Read();
		}

		// Token: 0x0600197D RID: 6525 RVA: 0x000A9384 File Offset: 0x000A7584
		private void ParseDatabaseDefinition(JsonTextReader reader, CompatibilityMode mode)
		{
			this.VerifyObjectDefinitionWasNotRead(reader);
			base.ObjectType = ObjectType.Database;
			reader.Read();
			reader.VerifyToken(1);
			this.databaseDefinition = new Database();
			this.databaseDefinition.StorageEngineUsed = StorageEngineUsed.TabularMetadata;
			JsonSerializer.DeserializeDatabase((Database)this.databaseDefinition, reader, this.GetJsonDeserializeOptions(), mode);
			reader.VerifyToken(13);
			reader.Read();
		}

		// Token: 0x0600197E RID: 6526 RVA: 0x000A93EF File Offset: 0x000A75EF
		private void VerifyObjectDefinitionWasNotRead(JsonTextReader reader)
		{
			if (this.objectDefinition != null || this.databaseDefinition != null)
			{
				throw JsonSerializationUtil.CreateException(TomSR.Exception_JsonCommandMustContainExactlyOneObjectDefinition(base.CommandName), reader, null);
			}
		}

		// Token: 0x0600197F RID: 6527 RVA: 0x000A9414 File Offset: 0x000A7614
		private void WriteSchemaForParametersOfAlterDatabase(JsonWriter writer, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue(string.Format("{0} command for Database object", base.CommandNameInSchema));
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("object");
			JsonSchemaWriter.WriteSchemaForObjectPath(writer, ObjectType.Database, true);
			writer.WritePropertyName("database");
			JsonSchemaWriter.WriteSchemaForDatabase(writer, this.JsonSerializeOptions, mode, dbCompatibilityLevel);
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001980 RID: 6528 RVA: 0x000A94C0 File Offset: 0x000A76C0
		private void WriteSchemaForParametersOfAlterDataSource(JsonWriter writer, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue(string.Format("{0} command for DataSource object", base.CommandNameInSchema));
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("object");
			JsonSchemaWriter.WriteSchemaForObjectPath(writer, ObjectType.DataSource, true);
			writer.WritePropertyName("dataSource");
			JsonSchemaWriter.WriteSchemaForDataSource(writer, this.JsonSerializeOptions, mode, dbCompatibilityLevel);
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001981 RID: 6529 RVA: 0x000A9568 File Offset: 0x000A7768
		private void WriteSchemaForParametersOfAlterTable(JsonWriter writer, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue(string.Format("{0} command for Table object", base.CommandNameInSchema));
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("object");
			JsonSchemaWriter.WriteSchemaForObjectPath(writer, ObjectType.Table, true);
			writer.WritePropertyName("table");
			JsonSchemaWriter.WriteSchemaForTable(writer, this.JsonSerializeOptions, mode, dbCompatibilityLevel);
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001982 RID: 6530 RVA: 0x000A9610 File Offset: 0x000A7810
		private void WriteSchemaForParametersOfAlterPartition(JsonWriter writer, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue(string.Format("{0} command for Partition object", base.CommandNameInSchema));
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("object");
			JsonSchemaWriter.WriteSchemaForObjectPath(writer, ObjectType.Partition, true);
			writer.WritePropertyName("partition");
			JsonSchemaWriter.WriteSchemaForPartition(writer, this.JsonSerializeOptions, mode, dbCompatibilityLevel);
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001983 RID: 6531 RVA: 0x000A96B8 File Offset: 0x000A78B8
		private void WriteSchemaForParametersOfAlterRole(JsonWriter writer, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue(string.Format("{0} command for Role object", base.CommandNameInSchema));
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("object");
			JsonSchemaWriter.WriteSchemaForObjectPath(writer, ObjectType.Role, true);
			writer.WritePropertyName("role");
			JsonSchemaWriter.WriteSchemaForModelRole(writer, this.JsonSerializeOptions, mode, dbCompatibilityLevel);
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001984 RID: 6532 RVA: 0x000A9760 File Offset: 0x000A7960
		private void ApplyDatabaseChanges(Server server, Database activeDB)
		{
			if (this.objectPath.Count != 1 || this.objectPath[0].Key != ObjectType.Database)
			{
				throw new TomException(TomSR.Exception_JsonCommandInvalidObjectSpecified(base.CommandName));
			}
			if (activeDB != null)
			{
				activeDB.Refresh(true);
				this.databaseDefinition.CopyTo(activeDB, true);
				this.databaseToUpdate = activeDB;
				return;
			}
			if (this.allowCreate)
			{
				CreateJsonCommand createJsonCommand = new CreateJsonCommand(this.databaseDefinition)
				{
					CommandName = base.CommandName
				};
				createJsonCommand.ApplyChangesLocally(server, activeDB);
				this.fallbackCreateCommand = createJsonCommand;
				return;
			}
			throw new TomException(TomSR.Exception_JsonCommandDatabaseNotFound(base.CommandName, this.objectPath[0].Value));
		}

		// Token: 0x06001985 RID: 6533 RVA: 0x000A981C File Offset: 0x000A7A1C
		private void ApplyTabularObjectChanges(Server server, Database activeDB)
		{
			Utils.Verify(activeDB != null);
			Model model = activeDB.Model;
			MetadataObject metadataObject = ObjectTreeHelper.LocateObjectByPath(this.objectPath, model);
			if (metadataObject == null)
			{
				if (this.allowCreate)
				{
					ObjectPath objectPath = this.objectPath.Clone();
					objectPath.Pop();
					CreateJsonCommand createJsonCommand = new CreateJsonCommand(this.objectDefinition, objectPath)
					{
						CommandName = base.CommandName
					};
					createJsonCommand.ApplyChangesLocally(server, activeDB);
					this.fallbackCreateCommand = createJsonCommand;
					return;
				}
				throw new TomException(TomSR.Exception_JsonCommandCannotFindObject(base.CommandName, Utils.GetUserFriendlyNameOfObjectType(base.ObjectType), ClientHostingManager.MarkAsRestrictedInformation(this.objectPath.ToString(), InfoRestrictionType.CCON)));
			}
			else
			{
				if (metadataObject.ObjectType != base.ObjectType)
				{
					throw new TomException(TomSR.Exception_JsonCommandObjectReferenceAndDefinitionMismatch(base.CommandName));
				}
				metadataObject.CopyFrom(this.objectDefinition, this.copyFlags);
				if (metadataObject.ContainsUnresolvedCrossLinks())
				{
					throw JsonSerializationUtil.CreateCannotResolvePathsWhileDeserializeObjectException(base.ObjectType, null, null);
				}
				this.modelToSave = model;
				return;
			}
		}

		// Token: 0x06001986 RID: 6534 RVA: 0x000A9908 File Offset: 0x000A7B08
		private XmlaScript GenerateXmlaScriptForDbUpdate(Server server)
		{
			XmlaScript xmlaScript = new XmlaScript();
			try
			{
				server.CaptureXml = true;
				this.databaseToUpdate.Update(this.updateOptions);
				for (int i = 0; i < server.CaptureLog.Count; i++)
				{
					xmlaScript.Commands.Add(new XmlaCommand
					{
						CommandText = server.CaptureLog[i],
						DatabaseName = this.databaseToUpdate.Name,
						IsTabular = (i >= 1)
					});
				}
			}
			finally
			{
				server.CaptureXml = false;
				server.CaptureLog.Clear();
			}
			return xmlaScript;
		}

		// Token: 0x06001987 RID: 6535 RVA: 0x000A99B0 File Offset: 0x000A7BB0
		private XmlaScript GenerateXmlaScriptForModelSave(Server server)
		{
			XmlaScript xmlaScript = new XmlaScript();
			if (this.modelToSave.HasLocalChanges)
			{
				try
				{
					server.CaptureXml = true;
					this.modelToSave.SaveChangesImpl(SaveFlags.Default, 0);
					for (int i = 0; i < server.CaptureLog.Count; i++)
					{
						xmlaScript.Commands.Add(new XmlaCommand
						{
							CommandText = server.CaptureLog[i],
							DatabaseName = this.modelToSave.Database.Name,
							IsTabular = true
						});
					}
				}
				finally
				{
					server.CaptureXml = false;
					server.CaptureLog.Clear();
				}
			}
			return xmlaScript;
		}

		// Token: 0x040004BE RID: 1214
		private ObjectPath objectPath;

		// Token: 0x040004BF RID: 1215
		private NamedMetadataObject objectDefinition;

		// Token: 0x040004C0 RID: 1216
		private Database databaseDefinition;

		// Token: 0x040004C1 RID: 1217
		private bool allowCreate;

		// Token: 0x040004C2 RID: 1218
		private CopyFlags copyFlags;

		// Token: 0x040004C3 RID: 1219
		private UpdateOptions updateOptions;

		// Token: 0x040004C4 RID: 1220
		private SerializeOptions jsonSerializeOptions;

		// Token: 0x040004C5 RID: 1221
		private Database databaseToUpdate;

		// Token: 0x040004C6 RID: 1222
		private Model modelToSave;

		// Token: 0x040004C7 RID: 1223
		private CreateJsonCommand fallbackCreateCommand;
	}
}
