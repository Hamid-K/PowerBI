using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AnalysisServices.Core;
using Microsoft.AnalysisServices.Hosting;
using Microsoft.AnalysisServices.Tabular.Extensions;
using Microsoft.AnalysisServices.Tabular.Json;
using Microsoft.AnalysisServices.Tabular.Metadata;
using Microsoft.AnalysisServices.Tabular.Utilities;

namespace Microsoft.AnalysisServices.Tabular.Scripting
{
	// Token: 0x020001A3 RID: 419
	internal sealed class DeleteJsonCommand : JsonCommand
	{
		// Token: 0x060019CA RID: 6602 RVA: 0x000AB09C File Offset: 0x000A929C
		public DeleteJsonCommand(Database database)
			: this()
		{
			this.objectPath = new ObjectPath(ObjectType.Database, database.Name);
			base.ObjectType = ObjectType.Database;
			this.isCommandValid = true;
		}

		// Token: 0x060019CB RID: 6603 RVA: 0x000AB0CC File Offset: 0x000A92CC
		public DeleteJsonCommand(NamedMetadataObject @object)
			: this()
		{
			this.objectPath = ObjectPath.CreateDbQualifiedPath(@object.GetPath(null), @object.Model.Database.Name);
			base.ObjectType = @object.ObjectType;
			this.isCommandValid = true;
		}

		// Token: 0x060019CC RID: 6604 RVA: 0x000AB109 File Offset: 0x000A9309
		internal DeleteJsonCommand()
			: base(JsonCommandType.Delete, "delete", "Delete", true, true, false, true)
		{
			base.ObjectType = ObjectType.Null;
		}

		// Token: 0x060019CD RID: 6605 RVA: 0x000AB128 File Offset: 0x000A9328
		internal override void ApplyChangesLocally(Server server, Database activeDB)
		{
			base.EnsureNoTMDatabaseHasLocalChanges(server, activeDB);
			if (this.objectPath.Count == 1 && this.objectPath[0].Key == ObjectType.Database)
			{
				this.ApplyDatabaseDelete(server, activeDB);
				return;
			}
			this.ApplyTabularObjectDelete(server, activeDB);
		}

		// Token: 0x060019CE RID: 6606 RVA: 0x000AB178 File Offset: 0x000A9378
		internal override XmlaScript GenerateXmlaScript(Server server)
		{
			Utils.Verify(!server.CaptureXml && server.CaptureLog.Count == 0, "Server must not be in capturing mode, and can't have any cached XMLA commands!");
			XmlaScript xmlaScript = new XmlaScript();
			if (this.databaseToDrop != null)
			{
				try
				{
					server.CaptureXml = true;
					this.databaseToDrop.Drop();
					foreach (string text in server.CaptureLog)
					{
						xmlaScript.Commands.Add(new XmlaCommand
						{
							CommandText = text,
							DatabaseName = this.databaseToDrop.Name,
							IsTabular = false
						});
					}
					return xmlaScript;
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

		// Token: 0x060019CF RID: 6607 RVA: 0x000AB328 File Offset: 0x000A9528
		private protected override void ParseProperties(JsonTextReader reader, CompatibilityMode mode)
		{
			while (reader.TokenType != 13)
			{
				reader.VerifyToken(4);
				string text = (string)reader.Value;
				if (!(text == "object"))
				{
					throw JsonSerializationUtil.CreateException(TomSR.Exception_UnexpectedJsonProperty(text), reader, null);
				}
				reader.Read();
				reader.VerifyToken(1);
				this.objectPath = ObjectPath.Parse(reader);
				this.objectPath.Normalize();
				base.ObjectType = this.objectPath.Last<KeyValuePair<ObjectType, string>>().Key;
				reader.VerifyToken(13);
				reader.Read();
			}
		}

		// Token: 0x060019D0 RID: 6608 RVA: 0x000AB3C1 File Offset: 0x000A95C1
		private protected override void ValidateProperties()
		{
			if (this.objectPath == null || this.objectPath.IsEmpty)
			{
				throw new TomException(TomSR.Exception_JsonCommandObjectNotSpecified(base.CommandName));
			}
		}

		// Token: 0x060019D1 RID: 6609 RVA: 0x000AB3E9 File Offset: 0x000A95E9
		private protected override void WriteProperties(JsonWriter writer)
		{
			writer.WritePropertyName("object");
			this.objectPath.Write(writer);
		}

		// Token: 0x060019D2 RID: 6610 RVA: 0x000AB404 File Offset: 0x000A9604
		private protected override void WritePropertiesSchema(JsonWriter writer, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue(string.Format("Parameters of {0} command of Analysis Services JSON API", base.CommandNameInSchema));
			writer.WritePropertyName("anyOf");
			writer.WriteStartArray();
			this.WriteSchemaForParametersOfDeleteDatabase(writer, mode, dbCompatibilityLevel);
			this.WriteSchemaForParametersOfDeleteDataSource(writer, mode, dbCompatibilityLevel);
			this.WriteSchemaForParametersOfDeleteTable(writer, mode, dbCompatibilityLevel);
			this.WriteSchemaForParametersOfDeletePartition(writer, mode, dbCompatibilityLevel);
			this.WriteSchemaForParametersOfDeleteRole(writer, mode, dbCompatibilityLevel);
			writer.WriteEndArray();
			writer.WriteEndObject();
		}

		// Token: 0x060019D3 RID: 6611 RVA: 0x000AB484 File Offset: 0x000A9684
		private protected override string GetAffectedDatabaseName()
		{
			return this.objectPath.SingleOrDefault((KeyValuePair<ObjectType, string> e) => e.Key == ObjectType.Database).Value;
		}

		// Token: 0x060019D4 RID: 6612 RVA: 0x000AB4C4 File Offset: 0x000A96C4
		private void WriteSchemaForParametersOfDeleteDatabase(JsonWriter writer, CompatibilityMode mode, int dbCompatibilityLevel)
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
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x060019D5 RID: 6613 RVA: 0x000AB554 File Offset: 0x000A9754
		private void WriteSchemaForParametersOfDeleteDataSource(JsonWriter writer, CompatibilityMode mode, int dbCompatibilityLevel)
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
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x060019D6 RID: 6614 RVA: 0x000AB5E0 File Offset: 0x000A97E0
		private void WriteSchemaForParametersOfDeleteTable(JsonWriter writer, CompatibilityMode mode, int dbCompatibilityLevel)
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
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x060019D7 RID: 6615 RVA: 0x000AB66C File Offset: 0x000A986C
		private void WriteSchemaForParametersOfDeletePartition(JsonWriter writer, CompatibilityMode mode, int dbCompatibilityLevel)
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
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x060019D8 RID: 6616 RVA: 0x000AB6F8 File Offset: 0x000A98F8
		private void WriteSchemaForParametersOfDeleteRole(JsonWriter writer, CompatibilityMode mode, int dbCompatibilityLevel)
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
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x060019D9 RID: 6617 RVA: 0x000AB788 File Offset: 0x000A9988
		private void ApplyDatabaseDelete(Server server, Database activeDB)
		{
			string value = this.objectPath[0].Value;
			Database database = server.Databases.FindByName(value);
			if (database == null)
			{
				throw new TomException(TomSR.Exception_JsonCommandDatabaseNotFound(base.CommandName, value));
			}
			this.databaseToDrop = database;
		}

		// Token: 0x060019DA RID: 6618 RVA: 0x000AB7D4 File Offset: 0x000A99D4
		private void ApplyTabularObjectDelete(Server server, Database activeDB)
		{
			Utils.Verify(activeDB != null);
			Model model = activeDB.Model;
			MetadataObject metadataObject = ObjectTreeHelper.LocateObjectByPath(this.objectPath, model);
			if (metadataObject == null)
			{
				throw new TomException(TomSR.Exception_JsonCommandCannotFindObject(base.CommandName, Utils.GetUserFriendlyNameOfObjectType(base.ObjectType), ClientHostingManager.MarkAsRestrictedInformation(this.objectPath.ToString(), InfoRestrictionType.CCON)));
			}
			if (!ObjectTreeHelper.SupportsScriptOut(metadataObject.ObjectType))
			{
				throw new TomException(TomSR.Exception_JsonCommandDeleteNotSupportedForObjectType(Utils.GetUserFriendlyNameOfObjectType(base.ObjectType)));
			}
			if (metadataObject.ParentCollection != null)
			{
				metadataObject.ParentCollection.Remove(metadataObject);
				this.modelToSave = model;
				return;
			}
			throw new TomInternalException("Json Delete doesn't support deleting objects linked to parents through direct reference (like Measure.KPI)");
		}

		// Token: 0x040004D9 RID: 1241
		private ObjectPath objectPath;

		// Token: 0x040004DA RID: 1242
		private Model modelToSave;

		// Token: 0x040004DB RID: 1243
		private Database databaseToDrop;
	}
}
