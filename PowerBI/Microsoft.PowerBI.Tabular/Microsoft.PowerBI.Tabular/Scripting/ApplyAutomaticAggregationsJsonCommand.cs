using System;
using Microsoft.AnalysisServices.Core;
using Microsoft.AnalysisServices.Tabular.AdaptiveCaching;
using Microsoft.AnalysisServices.Tabular.Extensions;
using Microsoft.AnalysisServices.Tabular.Json;
using Microsoft.AnalysisServices.Tabular.Json.Linq;

namespace Microsoft.AnalysisServices.Tabular.Scripting
{
	// Token: 0x0200019E RID: 414
	internal sealed class ApplyAutomaticAggregationsJsonCommand : JsonCommand
	{
		// Token: 0x0600198D RID: 6541 RVA: 0x000A9ACC File Offset: 0x000A7CCC
		public ApplyAutomaticAggregationsJsonCommand(Database database)
			: this()
		{
			this.databaseName = database.Name;
			this.isCommandValid = true;
		}

		// Token: 0x0600198E RID: 6542 RVA: 0x000A9AE7 File Offset: 0x000A7CE7
		public ApplyAutomaticAggregationsJsonCommand(Database database, AutomaticAggregationOptions options)
			: this()
		{
			this.databaseName = database.Name;
			this.options = options;
			this.isCommandValid = true;
		}

		// Token: 0x0600198F RID: 6543 RVA: 0x000A9B09 File Offset: 0x000A7D09
		internal ApplyAutomaticAggregationsJsonCommand(string databaseName)
			: this()
		{
			this.databaseName = databaseName;
			this.isCommandValid = true;
		}

		// Token: 0x06001990 RID: 6544 RVA: 0x000A9B1F File Offset: 0x000A7D1F
		internal ApplyAutomaticAggregationsJsonCommand(string databaseName, AutomaticAggregationOptions options)
			: this()
		{
			this.databaseName = databaseName;
			this.options = options;
			this.isCommandValid = true;
		}

		// Token: 0x06001991 RID: 6545 RVA: 0x000A9B3C File Offset: 0x000A7D3C
		internal ApplyAutomaticAggregationsJsonCommand()
			: base(JsonCommandType.ApplyAutomaticAggregations, "applyAutomaticAggregations", "ApplyAutomaticAggregations", true, false, true, false)
		{
			base.ObjectType = ObjectType.Database;
		}

		// Token: 0x06001992 RID: 6546 RVA: 0x000A9B5F File Offset: 0x000A7D5F
		internal override void ApplyChangesLocally(Server server, Database activeDB)
		{
			Utils.Verify(activeDB != null);
			base.EnsureNoTMDatabaseHasLocalChanges(server, activeDB);
			AdaptiveCachingHelper.ApplyAutomaticAggregations(activeDB.Model, this.options);
			this.modelToSave = activeDB.Model;
		}

		// Token: 0x06001993 RID: 6547 RVA: 0x000A9B90 File Offset: 0x000A7D90
		internal override JsonCommandResult ExecuteCommandWithoutCaptureXML(Server server)
		{
			Utils.Verify(!server.CaptureXml, "Server must not be in capturing mode");
			if (!this.modelToSave.HasLocalChanges)
			{
				return new JsonCommandResult
				{
					ModelOperationResult = new ModelOperationResult()
				};
			}
			return new JsonCommandResult
			{
				ModelOperationResult = this.modelToSave.SaveChangesImpl(SaveFlags.Default, (base.MaxParallelism != null) ? base.MaxParallelism.Value : 0)
			};
		}

		// Token: 0x06001994 RID: 6548 RVA: 0x000A9C06 File Offset: 0x000A7E06
		internal override XmlaScript GenerateXmlaScript(Server server)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001995 RID: 6549 RVA: 0x000A9C10 File Offset: 0x000A7E10
		private protected override void ParseProperties(JsonTextReader reader, CompatibilityMode mode)
		{
			while (reader.TokenType != 13)
			{
				reader.VerifyToken(4);
				string text = (string)reader.Value;
				if (!(text == "database"))
				{
					if (!(text == "options"))
					{
						throw JsonSerializationUtil.CreateException(TomSR.Exception_UnexpectedJsonProperty(text), reader, null);
					}
					reader.Read();
					this.options = new AutomaticAggregationOptions(JObject.Load(reader));
					reader.Read();
				}
				else
				{
					reader.Read();
					reader.VerifyToken(9);
					this.databaseName = (string)reader.Value;
					reader.Read();
				}
			}
		}

		// Token: 0x06001996 RID: 6550 RVA: 0x000A9CB6 File Offset: 0x000A7EB6
		private protected override void ValidateProperties()
		{
			if (string.IsNullOrEmpty(this.databaseName))
			{
				throw new TomException(TomSR.Exception_JsonCommandDatabaseNotSpecified(base.CommandName));
			}
		}

		// Token: 0x06001997 RID: 6551 RVA: 0x000A9CD6 File Offset: 0x000A7ED6
		private protected override void WriteProperties(JsonWriter writer)
		{
			writer.WritePropertyName("database");
			writer.WriteValue(this.databaseName);
			if (this.options != null)
			{
				writer.WritePropertyName("options");
				writer.WriteRawJson(this.options.ToJson());
			}
		}

		// Token: 0x06001998 RID: 6552 RVA: 0x000A9D14 File Offset: 0x000A7F14
		private protected override void WritePropertiesSchema(JsonWriter writer, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WritePropertyName("database");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("options");
			writer.WriteStartObject();
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			AutomaticAggregationOptions.WriteAutomaticAggregationsOptionsSchema(writer);
			writer.WriteEndObject();
			writer.WriteEndObject();
		}

		// Token: 0x06001999 RID: 6553 RVA: 0x000A9D7C File Offset: 0x000A7F7C
		private protected override string GetAffectedDatabaseName()
		{
			return this.databaseName;
		}

		// Token: 0x040004C8 RID: 1224
		private string databaseName;

		// Token: 0x040004C9 RID: 1225
		private AutomaticAggregationOptions options;

		// Token: 0x040004CA RID: 1226
		private Model modelToSave;
	}
}
