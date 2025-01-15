using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AnalysisServices.Core;
using Microsoft.AnalysisServices.Tabular.Extensions;
using Microsoft.AnalysisServices.Tabular.Json;
using Microsoft.AnalysisServices.Tabular.Metadata;
using Microsoft.AnalysisServices.Tabular.Serialization;
using Microsoft.AnalysisServices.Tabular.Utilities;

namespace Microsoft.AnalysisServices.Tabular.Scripting
{
	// Token: 0x020001A1 RID: 417
	internal sealed class CreateJsonCommand : JsonCommand
	{
		// Token: 0x060019AD RID: 6573 RVA: 0x000AA508 File Offset: 0x000A8708
		public CreateJsonCommand(NamedMetadataObject @object)
			: this()
		{
			this.parentPath = ObjectPath.CreateDbQualifiedPath(@object.Parent.GetPath(null), @object.Model.Database.Name);
			this.objectDefinition = @object;
			base.ObjectType = @object.ObjectType;
			this.isCommandValid = true;
		}

		// Token: 0x060019AE RID: 6574 RVA: 0x000AA55C File Offset: 0x000A875C
		public CreateJsonCommand(Database database)
			: this()
		{
			base.ObjectType = ObjectType.Database;
			this.databaseDefinition = database;
			this.isCommandValid = true;
		}

		// Token: 0x060019AF RID: 6575 RVA: 0x000AA57D File Offset: 0x000A877D
		internal CreateJsonCommand(NamedMetadataObject @object, ObjectPath parentPath)
			: this()
		{
			this.parentPath = parentPath;
			this.objectDefinition = @object;
			base.ObjectType = @object.ObjectType;
			this.isCommandValid = true;
		}

		// Token: 0x060019B0 RID: 6576 RVA: 0x000AA5A6 File Offset: 0x000A87A6
		internal CreateJsonCommand()
			: base(JsonCommandType.Create, "create", "Create", true, true, false, true)
		{
			base.ObjectType = ObjectType.Null;
		}

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x060019B1 RID: 6577 RVA: 0x000AA5C4 File Offset: 0x000A87C4
		internal SerializeOptions JsonSerializeOptions
		{
			get
			{
				if (this.jsonSerializeOptions == null)
				{
					this.jsonSerializeOptions = JsonCommand.JsonSerializationOptions.ObjectWithDescendants.Clone();
				}
				return this.jsonSerializeOptions;
			}
		}

		// Token: 0x060019B2 RID: 6578 RVA: 0x000AA5E4 File Offset: 0x000A87E4
		internal override void ApplyChangesLocally(Server server, Database activeDB)
		{
			base.EnsureNoTMDatabaseHasLocalChanges(server, activeDB);
			if (this.objectDefinition != null)
			{
				Utils.Verify(activeDB != null);
				this.ApplyTabularObjectCreate(server, activeDB);
				return;
			}
			this.ApplyDatabaseCreate(server);
		}

		// Token: 0x060019B3 RID: 6579 RVA: 0x000AA610 File Offset: 0x000A8810
		internal override XmlaScript GenerateXmlaScript(Server server)
		{
			Utils.Verify(!server.CaptureXml && server.CaptureLog.Count == 0, "Server must not be in capturing mode, and can't have any cached XMLA commands!");
			XmlaScript xmlaScript = new XmlaScript();
			if (this.databaseToUpdate != null)
			{
				try
				{
					server.CaptureXml = true;
					this.databaseToUpdate.Update();
					foreach (string text in server.CaptureLog)
					{
						xmlaScript.Commands.Add(new XmlaCommand
						{
							CommandText = text,
							DatabaseName = this.databaseToUpdate.Name,
							IsTabular = false
						});
					}
				}
				catch (Exception ex)
				{
					if (!Utils.IsSafeException(ex))
					{
						throw;
					}
					throw new TomException(TomSR.Exception_JsonScriptFailedToExecute(ex.Message), ex);
				}
				finally
				{
					server.CaptureXml = false;
					server.CaptureLog.Clear();
				}
			}
			if (this.modelToSave != null)
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

		// Token: 0x060019B4 RID: 6580 RVA: 0x000AA7BC File Offset: 0x000A89BC
		private protected override void ParseProperties(JsonTextReader reader, CompatibilityMode mode)
		{
			while (reader.TokenType != 13)
			{
				reader.VerifyToken(4);
				string text = (string)reader.Value;
				if (!(text == "parentObject"))
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
					this.parentPath = ObjectPath.Parse(reader);
					reader.VerifyToken(13);
					reader.Read();
				}
			}
		}

		// Token: 0x060019B5 RID: 6581 RVA: 0x000AA8B0 File Offset: 0x000A8AB0
		private protected override void ValidateProperties()
		{
			if (this.objectDefinition == null && this.databaseDefinition == null)
			{
				throw new TomException(TomSR.Exception_JsonCommandObjectDefinitionNotSpecified(base.CommandName));
			}
			if (this.objectDefinition != null && this.databaseDefinition != null)
			{
				throw new TomException(TomSR.Exception_JsonCommandMustContainExactlyOneObjectDefinition(base.CommandName));
			}
			if (this.objectDefinition != null)
			{
				if (this.parentPath == null || this.parentPath.IsEmpty)
				{
					throw new TomException(TomSR.Exception_JsonCommandParentObjectNotSpecified(base.CommandName));
				}
			}
			else if (this.parentPath != null)
			{
				throw new TomException(TomSR.Exception_JsonCommandParentObjectNotNeededForDatabase(base.CommandName));
			}
		}

		// Token: 0x060019B6 RID: 6582 RVA: 0x000AA948 File Offset: 0x000A8B48
		private protected override void WriteProperties(JsonWriter writer)
		{
			if (this.parentPath != null)
			{
				writer.WritePropertyName("parentObject");
				this.parentPath.Write(writer);
			}
			if (this.objectDefinition != null)
			{
				writer.WritePropertyName(JsonPropertyName.ObjectPath.GetObjectPathPropertyName(this.objectDefinition.ObjectType));
				writer.WriteRawJson(JsonSerializer.SerializeObjectImpl(this.objectDefinition, this.jsonSerializeOptions ?? JsonCommand.JsonSerializationOptions.ObjectWithDescendants, -3, CompatibilityMode.Unknown));
				return;
			}
			writer.WritePropertyName("database");
			writer.WriteRawJson(JsonSerializer.SerializeDatabase(this.databaseDefinition, this.jsonSerializeOptions ?? JsonCommand.JsonSerializationOptions.ObjectWithDescendants));
		}

		// Token: 0x060019B7 RID: 6583 RVA: 0x000AA9E4 File Offset: 0x000A8BE4
		private protected override void WritePropertiesSchema(JsonWriter writer, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue(string.Format("Parameters of {0} command of Analysis Services JSON API", base.CommandNameInSchema));
			writer.WritePropertyName("anyOf");
			writer.WriteStartArray();
			this.WriteSchemaForParametersOfCreateDatabase(writer, mode, dbCompatibilityLevel);
			this.WriteSchemaForParametersOfCreateDataSource(writer, mode, dbCompatibilityLevel);
			this.WriteSchemaForParametersOfCreateTable(writer, mode, dbCompatibilityLevel);
			this.WriteSchemaForParametersOfCreatePartition(writer, mode, dbCompatibilityLevel);
			this.WriteSchemaForParametersOfCreateRole(writer, mode, dbCompatibilityLevel);
			writer.WriteEndArray();
			writer.WriteEndObject();
		}

		// Token: 0x060019B8 RID: 6584 RVA: 0x000AAA64 File Offset: 0x000A8C64
		private protected override string GetAffectedDatabaseName()
		{
			if (this.parentPath != null)
			{
				return this.parentPath.SingleOrDefault((KeyValuePair<ObjectType, string> e) => e.Key == ObjectType.Database).Value;
			}
			return this.databaseDefinition.Name;
		}

		// Token: 0x060019B9 RID: 6585 RVA: 0x000AAAB7 File Offset: 0x000A8CB7
		private protected override bool CanAffectedDatabaseBeMissing()
		{
			return base.ObjectType == ObjectType.Database;
		}

		// Token: 0x060019BA RID: 6586 RVA: 0x000AAAC8 File Offset: 0x000A8CC8
		private void ParseDatabaseDefinition(JsonTextReader reader, CompatibilityMode mode)
		{
			this.VerifyObjectDefinitionWasNotRead(reader);
			reader.Read();
			reader.VerifyToken(1);
			base.ObjectType = ObjectType.Database;
			this.databaseDefinition = new Database();
			this.databaseDefinition.StorageEngineUsed = StorageEngineUsed.TabularMetadata;
			JsonSerializer.DeserializeDatabase((Database)this.databaseDefinition, reader, JsonCommand.JsonDeserializationOptions.Default, mode);
			reader.VerifyToken(13);
			reader.Read();
		}

		// Token: 0x060019BB RID: 6587 RVA: 0x000AAB34 File Offset: 0x000A8D34
		private void ParseObjectDefinition(ObjectType objectType, JsonTextReader reader, CompatibilityMode mode)
		{
			this.VerifyObjectDefinitionWasNotRead(reader);
			if (!ObjectTreeHelper.SupportsScriptOut(objectType))
			{
				throw new TomException(TomSR.Exception_JsonCommandCreateNotSupportedForObjectType(Utils.GetUserFriendlyNameOfObjectType(objectType)));
			}
			reader.Read();
			reader.VerifyToken(1);
			base.ObjectType = objectType;
			this.objectDefinition = JsonPropertyHelper.ParseObjectFromJson(objectType, reader, JsonCommand.JsonDeserializationOptions.Default, mode, int.MaxValue);
			reader.VerifyToken(13);
			reader.Read();
		}

		// Token: 0x060019BC RID: 6588 RVA: 0x000AAB9D File Offset: 0x000A8D9D
		private void VerifyObjectDefinitionWasNotRead(JsonTextReader reader)
		{
			if (this.objectDefinition != null || this.databaseDefinition != null)
			{
				throw JsonSerializationUtil.CreateException(TomSR.Exception_JsonCommandMustContainExactlyOneObjectDefinition(base.CommandName), reader, null);
			}
		}

		// Token: 0x060019BD RID: 6589 RVA: 0x000AABC4 File Offset: 0x000A8DC4
		private void WriteSchemaForParametersOfCreateDatabase(JsonWriter writer, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue(string.Format("{0} command for Database object", base.CommandNameInSchema));
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("database");
			JsonSchemaWriter.WriteSchemaForDatabase(writer, JsonCommand.JsonSerializationOptions.ObjectWithDescendants, mode, dbCompatibilityLevel);
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x060019BE RID: 6590 RVA: 0x000AAC58 File Offset: 0x000A8E58
		private void WriteSchemaForParametersOfCreateDataSource(JsonWriter writer, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue(string.Format("{0} command for DataSource object", base.CommandNameInSchema));
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("parentObject");
			JsonSchemaWriter.WriteSchemaForObjectPath(writer, ObjectType.Database, true);
			writer.WritePropertyName("dataSource");
			JsonSchemaWriter.WriteSchemaForDataSource(writer, JsonCommand.JsonSerializationOptions.ObjectWithDescendants, mode, dbCompatibilityLevel);
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x060019BF RID: 6591 RVA: 0x000AAD00 File Offset: 0x000A8F00
		private void WriteSchemaForParametersOfCreateTable(JsonWriter writer, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue(string.Format("{0} command for Table object", base.CommandNameInSchema));
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("parentObject");
			JsonSchemaWriter.WriteSchemaForObjectPath(writer, ObjectType.Database, true);
			writer.WritePropertyName("table");
			JsonSchemaWriter.WriteSchemaForTable(writer, JsonCommand.JsonSerializationOptions.ObjectWithDescendants, mode, dbCompatibilityLevel);
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x060019C0 RID: 6592 RVA: 0x000AADA8 File Offset: 0x000A8FA8
		private void WriteSchemaForParametersOfCreatePartition(JsonWriter writer, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue(string.Format("{0} command for Partition object", base.CommandNameInSchema));
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("parentObject");
			JsonSchemaWriter.WriteSchemaForObjectPath(writer, ObjectType.Table, true);
			writer.WritePropertyName("partition");
			JsonSchemaWriter.WriteSchemaForPartition(writer, JsonCommand.JsonSerializationOptions.ObjectWithDescendants, mode, dbCompatibilityLevel);
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x060019C1 RID: 6593 RVA: 0x000AAE4C File Offset: 0x000A904C
		private void WriteSchemaForParametersOfCreateRole(JsonWriter writer, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue(string.Format("{0} command for Role object", base.CommandNameInSchema));
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("parentObject");
			JsonSchemaWriter.WriteSchemaForObjectPath(writer, ObjectType.Database, true);
			writer.WritePropertyName("role");
			JsonSchemaWriter.WriteSchemaForModelRole(writer, JsonCommand.JsonSerializationOptions.ObjectWithDescendants, mode, dbCompatibilityLevel);
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x060019C2 RID: 6594 RVA: 0x000AAEF4 File Offset: 0x000A90F4
		private void ApplyDatabaseCreate(Server server)
		{
			Database database = (Database)this.databaseDefinition;
			if (string.IsNullOrEmpty(database.Name))
			{
				throw new TomException(TomSR.Exception_JsonCommandInvalidDatabaseName(base.CommandName));
			}
			if (!database.HasID)
			{
				database.ID = server.Databases.GetNewID(database.Name);
			}
			server.Databases.Add(database);
			this.databaseToUpdate = database;
			if (database.Model != null)
			{
				this.modelToSave = database.Model;
			}
		}

		// Token: 0x060019C3 RID: 6595 RVA: 0x000AAF74 File Offset: 0x000A9174
		private void ApplyTabularObjectCreate(Server server, Database activeDB)
		{
			Model model = activeDB.Model;
			MetadataObject metadataObject = ObjectTreeHelper.LocateObjectByPath(this.parentPath, model);
			if (metadataObject == null)
			{
				throw new TomException(TomSR.Exception_JsonCommandCannotFindParentObject(base.CommandName));
			}
			NamedMetadataObject namedMetadataObject = this.objectDefinition;
			ObjectType childType = namedMetadataObject.ObjectType;
			IMetadataObjectCollection metadataObjectCollection = metadataObject.GetChildrenCollections(true).SingleOrDefault((IMetadataObjectCollection coll) => coll.ItemType == childType);
			if (metadataObjectCollection == null)
			{
				throw new TomException(TomSR.Exception_JsonCommandChildCollectionNotFound(base.CommandName, Utils.GetUserFriendlyNameOfObjectType(childType)));
			}
			metadataObjectCollection.Add(namedMetadataObject);
			List<string> list = new List<string>();
			if (!namedMetadataObject.TryResolveAllCrossLinksInTreeByObjectPath(list))
			{
				throw JsonSerializationUtil.CreateCannotResolvePathsWhileDeserializeObjectException(childType, list, null);
			}
			this.modelToSave = model;
		}

		// Token: 0x040004D3 RID: 1235
		private ObjectPath parentPath;

		// Token: 0x040004D4 RID: 1236
		private NamedMetadataObject objectDefinition;

		// Token: 0x040004D5 RID: 1237
		private Database databaseDefinition;

		// Token: 0x040004D6 RID: 1238
		private SerializeOptions jsonSerializeOptions;

		// Token: 0x040004D7 RID: 1239
		private Database databaseToUpdate;

		// Token: 0x040004D8 RID: 1240
		private Model modelToSave;
	}
}
