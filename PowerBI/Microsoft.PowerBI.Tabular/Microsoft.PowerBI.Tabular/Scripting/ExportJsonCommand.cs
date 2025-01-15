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
	// Token: 0x020001A5 RID: 421
	internal sealed class ExportJsonCommand : JsonCommand
	{
		// Token: 0x060019E5 RID: 6629 RVA: 0x000ABB07 File Offset: 0x000A9D07
		public ExportJsonCommand(Database database)
			: this()
		{
			this.objectPaths.Add(new ObjectPath(ObjectType.Database, database.Name));
			base.ObjectType = ObjectType.Database;
		}

		// Token: 0x060019E6 RID: 6630 RVA: 0x000ABB35 File Offset: 0x000A9D35
		internal ExportJsonCommand()
			: base(JsonCommandType.Export, "export", "Export", false, false, false, false)
		{
			this.objectPaths = new List<ObjectPath>();
		}

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x060019E7 RID: 6631 RVA: 0x000ABB58 File Offset: 0x000A9D58
		internal ExportLayout ExportLayout
		{
			get
			{
				return this.exportLayout;
			}
		}

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x060019E8 RID: 6632 RVA: 0x000ABB60 File Offset: 0x000A9D60
		internal ExportType ExportType
		{
			get
			{
				return this.exportType;
			}
		}

		// Token: 0x060019E9 RID: 6633 RVA: 0x000ABB68 File Offset: 0x000A9D68
		internal override void ApplyChangesLocally(Server server, Database activeDB)
		{
		}

		// Token: 0x060019EA RID: 6634 RVA: 0x000ABB6C File Offset: 0x000A9D6C
		internal override XmlaScript GenerateXmlaScript(Server server)
		{
			Utils.Verify(!server.CaptureXml && server.CaptureLog.Count == 0, "Server must not be in capturing mode, and can't have any cached XMLA commands!");
			Database affectedDatabase = base.GetAffectedDatabase(server);
			XmlaScript xmlaScript = new XmlaScript();
			try
			{
				server.CaptureXml = true;
				foreach (ObjectPath objectPath in this.objectPaths)
				{
					string value = objectPath.FirstOrDefault((KeyValuePair<ObjectType, string> kvp) => kvp.Key == ObjectType.Database).Value;
					if (string.IsNullOrEmpty(value) || string.Compare(value, affectedDatabase.Name, StringComparison.InvariantCulture) != 0)
					{
						throw new TomException(TomSR.Exception_JsonCommandCannotFindObject(base.CommandNameInSchema, Utils.GetUserFriendlyNameOfObjectType(ObjectType.Database), ClientHostingManager.MarkAsRestrictedInformation(objectPath.ToString(), InfoRestrictionType.CCON)));
					}
					ObjectType key = objectPath.Last<KeyValuePair<ObjectType, string>>().Key;
					MetadataObject metadataObject = ObjectTreeHelper.LocateObjectByPath(objectPath, affectedDatabase.Model);
					if (metadataObject == null)
					{
						throw new TomException(TomSR.Exception_JsonCommandCannotFindObject(base.CommandNameInSchema, Utils.GetUserFriendlyNameOfObjectType(key), ClientHostingManager.MarkAsRestrictedInformation(objectPath.ToString(), InfoRestrictionType.CCON)));
					}
					if (key != ObjectType.Database && !ObjectTreeHelper.SupportsExport(metadataObject.ObjectType))
					{
						throw new TomException(TomSR.Exception_JsonCommandExportNotSupportedForObjectType(Utils.GetUserFriendlyNameOfObjectType(key)));
					}
					server.Export(affectedDatabase, this.exportLayout, this.exportType);
				}
				foreach (string text in server.CaptureLog)
				{
					xmlaScript.Commands.Add(new XmlaCommand
					{
						CommandText = text,
						DatabaseName = affectedDatabase.Name,
						IsTabular = false
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

		// Token: 0x060019EB RID: 6635 RVA: 0x000ABD9C File Offset: 0x000A9F9C
		private protected override void ParseProperties(JsonTextReader reader, CompatibilityMode mode)
		{
			while (reader.TokenType != 13)
			{
				reader.VerifyToken(4);
				string text = (string)reader.Value;
				if (!(text == "layout"))
				{
					if (!(text == "type"))
					{
						if (!(text == "objects"))
						{
							throw JsonSerializationUtil.CreateException(TomSR.Exception_UnexpectedJsonProperty(text), reader, null);
						}
						reader.Read();
						JsonPropertyHelper.ParseArrayOfObjects(reader, delegate(JsonTextReader innerReader)
						{
							innerReader.VerifyToken(1);
							ObjectPath objectPath = ObjectPath.Parse(innerReader);
							objectPath.Normalize();
							if (objectPath.Count == 0)
							{
								throw new TomException(TomSR.Exception_JsonCommandObjectNotSpecified(base.CommandNameInSchema));
							}
							if (objectPath[0].Key != ObjectType.Database)
							{
								throw new TomException(TomSR.Exception_JsonCommandDatabaseNotSpecified(base.CommandNameInSchema));
							}
							this.objectPaths.Add(objectPath);
							innerReader.VerifyToken(13);
							innerReader.Read();
						});
						base.ObjectType = JsonCommand.CalculateTargetObjectType(this.objectPaths);
					}
					else
					{
						reader.Read();
						reader.VerifyToken(9);
						this.exportType = JsonPropertyHelper.ConvertStringToEnum<ExportType>((string)reader.Value, reader);
						reader.Read();
					}
				}
				else
				{
					reader.Read();
					reader.VerifyToken(9);
					this.exportLayout = JsonPropertyHelper.ConvertStringToEnum<ExportLayout>((string)reader.Value, reader);
					reader.Read();
				}
			}
		}

		// Token: 0x060019EC RID: 6636 RVA: 0x000ABE94 File Offset: 0x000AA094
		private protected override void ValidateProperties()
		{
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

		// Token: 0x060019ED RID: 6637 RVA: 0x000ABF18 File Offset: 0x000AA118
		private protected override void WriteProperties(JsonWriter writer)
		{
			writer.WritePropertyName("layout");
			writer.WriteValue(JsonPropertyHelper.ConvertEnumToJsonValue<ExportLayout>(this.exportLayout));
			writer.WritePropertyName("type");
			writer.WriteValue(JsonPropertyHelper.ConvertEnumToJsonValue<ExportType>(this.exportType));
			writer.WritePropertyName("objects");
			writer.WriteStartArray();
			foreach (ObjectPath objectPath in this.objectPaths)
			{
				objectPath.Write(writer);
			}
			writer.WriteEndArray();
		}

		// Token: 0x060019EE RID: 6638 RVA: 0x000ABFB8 File Offset: 0x000AA1B8
		private protected override void WritePropertiesSchema(JsonWriter writer, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WritePropertyName("layout");
			writer.WriteStartObject();
			writer.WritePropertyName("enum");
			writer.WriteStartArray();
			writer.WriteValue("delta");
			writer.WriteEndArray();
			writer.WriteEndObject();
			writer.WritePropertyName("type");
			writer.WriteStartObject();
			writer.WritePropertyName("enum");
			writer.WriteStartArray();
			writer.WriteValue("full");
			writer.WriteValue("incremental");
			writer.WriteEndArray();
			writer.WriteEndObject();
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
			writer.WriteEndArray();
			writer.WriteEndObject();
			writer.WriteEndObject();
		}

		// Token: 0x060019EF RID: 6639 RVA: 0x000AC0B4 File Offset: 0x000AA2B4
		private protected override string GetAffectedDatabaseName()
		{
			return this.objectPaths[0].SingleOrDefault((KeyValuePair<ObjectType, string> e) => e.Key == ObjectType.Database).Value;
		}

		// Token: 0x040004DF RID: 1247
		private ExportLayout exportLayout;

		// Token: 0x040004E0 RID: 1248
		private ExportType exportType;

		// Token: 0x040004E1 RID: 1249
		private List<ObjectPath> objectPaths;
	}
}
