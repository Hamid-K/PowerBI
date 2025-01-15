using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.AnalysisServices.Extensions;
using Microsoft.AnalysisServices.Tabular.DDL;
using Microsoft.AnalysisServices.Tabular.Extensions;
using Microsoft.AnalysisServices.Tabular.Json;
using Microsoft.AnalysisServices.Tabular.Json.Linq;
using Microsoft.AnalysisServices.Tabular.Metadata;
using Microsoft.AnalysisServices.Tabular.Utilities;

namespace Microsoft.AnalysisServices.Tabular.Scripting
{
	// Token: 0x020001A6 RID: 422
	internal abstract class JsonCommand
	{
		// Token: 0x060019F1 RID: 6641 RVA: 0x000AC178 File Offset: 0x000AA378
		private protected JsonCommand(JsonCommandType commandType, string commandTag, string commandNameInSchema, bool requiresTransaction = true, bool isIntrusiveCommand = true, bool requiresLiveExecution = false, bool hasCustomSchemaProperties = false)
		{
			this.CommandType = commandType;
			this.CommandTag = commandTag;
			this.CommandNameInSchema = commandNameInSchema;
			this.RequiresTransaction = requiresTransaction;
			this.IsIntrusiveCommand = isIntrusiveCommand;
			this.requiresLiveExecution = requiresLiveExecution;
			this.hasCustomSchemaProperties = hasCustomSchemaProperties;
		}

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x060019F2 RID: 6642 RVA: 0x000AC1B5 File Offset: 0x000AA3B5
		public JsonCommandType CommandType { get; }

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x060019F3 RID: 6643 RVA: 0x000AC1BD File Offset: 0x000AA3BD
		// (set) Token: 0x060019F4 RID: 6644 RVA: 0x000AC1C5 File Offset: 0x000AA3C5
		public ObjectType ObjectType { get; private protected set; }

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x060019F5 RID: 6645 RVA: 0x000AC1CE File Offset: 0x000AA3CE
		public string CommandNameInSchema { get; }

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x060019F6 RID: 6646 RVA: 0x000AC1D6 File Offset: 0x000AA3D6
		// (set) Token: 0x060019F7 RID: 6647 RVA: 0x000AC1F2 File Offset: 0x000AA3F2
		public string CommandName
		{
			get
			{
				if (string.IsNullOrEmpty(this.commandName))
				{
					return this.CommandNameInSchema;
				}
				return this.commandName;
			}
			internal set
			{
				this.commandName = value;
			}
		}

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x060019F8 RID: 6648 RVA: 0x000AC1FB File Offset: 0x000AA3FB
		// (set) Token: 0x060019F9 RID: 6649 RVA: 0x000AC203 File Offset: 0x000AA403
		public string CommandTextForTracing { get; internal set; }

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x060019FA RID: 6650 RVA: 0x000AC20C File Offset: 0x000AA40C
		// (set) Token: 0x060019FB RID: 6651 RVA: 0x000AC214 File Offset: 0x000AA414
		public int? MaxParallelism { get; internal set; }

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x060019FC RID: 6652 RVA: 0x000AC21D File Offset: 0x000AA41D
		public string DatabaseName
		{
			get
			{
				return this.GetAffectedDatabaseName();
			}
		}

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x060019FD RID: 6653 RVA: 0x000AC225 File Offset: 0x000AA425
		public bool RequiresTransaction { get; }

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x060019FE RID: 6654 RVA: 0x000AC22D File Offset: 0x000AA42D
		// (set) Token: 0x060019FF RID: 6655 RVA: 0x000AC235 File Offset: 0x000AA435
		public bool IsIntrusiveCommand { get; private protected set; }

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x06001A00 RID: 6656 RVA: 0x000AC23E File Offset: 0x000AA43E
		// (set) Token: 0x06001A01 RID: 6657 RVA: 0x000AC246 File Offset: 0x000AA446
		internal SequenceJsonCommand ParentSequence { get; set; }

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x06001A02 RID: 6658 RVA: 0x000AC24F File Offset: 0x000AA44F
		private protected string CommandTag { get; }

		// Token: 0x06001A03 RID: 6659 RVA: 0x000AC258 File Offset: 0x000AA458
		public static JsonCommand CreateFromJson(string json, CompatibilityMode mode, bool curateCommandText = true)
		{
			JsonCommand jsonCommand;
			using (TextReader textReader = new StringReader(json))
			{
				using (JsonTextReader jsonTextReader = new CommentIgnoringJsonTextReader(textReader))
				{
					jsonTextReader.Read();
					jsonCommand = JsonCommandFactory.CreateCommand(jsonTextReader, mode);
				}
			}
			jsonCommand.CommandTextForTracing = (curateCommandText ? JsonCommand.CurateCommandText(json) : json);
			return jsonCommand;
		}

		// Token: 0x06001A04 RID: 6660 RVA: 0x000AC2C8 File Offset: 0x000AA4C8
		public string ScriptOut()
		{
			StringBuilder stringBuilder = new StringBuilder();
			using (StringWriter stringWriter = new StringWriter(stringBuilder))
			{
				using (JsonTextWriter jsonTextWriter = new JsonTextWriter(stringWriter))
				{
					jsonTextWriter.Formatting = 1;
					this.Write(jsonTextWriter);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001A05 RID: 6661 RVA: 0x000AC330 File Offset: 0x000AA530
		public void SyncBeforeExecution(Server server)
		{
			if (server == null)
			{
				throw new ArgumentNullException("server");
			}
			Database database;
			this.SyncBeforeExecutionImpl(server, out database);
		}

		// Token: 0x06001A06 RID: 6662 RVA: 0x000AC354 File Offset: 0x000AA554
		public JsonCommandResult Execute(Server server)
		{
			if (server == null)
			{
				throw new ArgumentNullException("server");
			}
			Database database;
			this.SyncBeforeExecutionImpl(server, out database);
			return this.ExecuteImpl(server, database);
		}

		// Token: 0x06001A07 RID: 6663 RVA: 0x000AC380 File Offset: 0x000AA580
		internal void Parse(JsonTextReader reader, CompatibilityMode mode)
		{
			reader.VerifyToken(1);
			reader.Read();
			this.ParseProperties(reader, mode);
			reader.VerifyToken(13);
			if (!reader.Read())
			{
				throw JsonSerializationUtil.CreateException(TomSR.Exception_InvalidJsonScript, null);
			}
			this.ValidateProperties();
			this.isCommandValid = true;
		}

		// Token: 0x06001A08 RID: 6664 RVA: 0x000AC3CC File Offset: 0x000AA5CC
		internal void Write(JsonWriter writer)
		{
			writer.WriteStartObject();
			writer.WritePropertyName(this.CommandTag);
			writer.WriteStartObject();
			this.WriteProperties(writer);
			writer.WriteEndObject();
			writer.WriteEndObject();
		}

		// Token: 0x06001A09 RID: 6665 RVA: 0x000AC3FC File Offset: 0x000AA5FC
		internal void WriteSchema(JsonWriter writer, bool includeCommandTag, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (includeCommandTag)
			{
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("object");
				writer.WritePropertyName("description");
				writer.WriteValue(string.Format("{0} command of Analysis Services JSON API", this.CommandNameInSchema));
				writer.WritePropertyName("properties");
				writer.WriteStartObject();
				writer.WritePropertyName(this.CommandTag);
			}
			if (this.hasCustomSchemaProperties)
			{
				this.WritePropertiesSchema(writer, mode, dbCompatibilityLevel);
			}
			else
			{
				writer.WriteStartObject();
				writer.WritePropertyName("description");
				writer.WriteValue(string.Format("Parameters of {0} command of Analysis Services JSON API", this.CommandNameInSchema));
				writer.WritePropertyName("properties");
				writer.WriteStartObject();
				this.WritePropertiesSchema(writer, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
				writer.WritePropertyName("additionalProperties");
				writer.WriteValue(false);
				writer.WriteEndObject();
			}
			if (includeCommandTag)
			{
				writer.WriteEndObject();
				writer.WritePropertyName("additionalProperties");
				writer.WriteValue(false);
				writer.WriteEndObject();
			}
		}

		// Token: 0x06001A0A RID: 6666 RVA: 0x000AC4FC File Offset: 0x000AA6FC
		internal Database GetAffectedDatabase(Server server)
		{
			bool flag = this.CanAffectedDatabaseBeMissing();
			string affectedDatabaseName = this.GetAffectedDatabaseName();
			if (string.IsNullOrEmpty(affectedDatabaseName))
			{
				if (flag)
				{
					return null;
				}
				throw new TomException(TomSR.Exception_JsonCommandDatabaseNotSpecified(this.CommandName));
			}
			else
			{
				Database database = server.Databases.FindByName(affectedDatabaseName);
				if (database == null && !flag)
				{
					throw new TomException(TomSR.Exception_JsonCommandDatabaseNotFound(this.CommandName, affectedDatabaseName));
				}
				return database;
			}
		}

		// Token: 0x06001A0B RID: 6667 RVA: 0x000AC55C File Offset: 0x000AA75C
		internal void SyncBeforeExecutionImpl(Server server, out Database activeDB)
		{
			Utils.Verify(this.isCommandValid);
			activeDB = this.GetAffectedDatabase(server);
			if (activeDB != null)
			{
				if (!activeDB.DisableDiscoveryOptimization)
				{
					try
					{
						activeDB.DisableDiscoveryOptimization = true;
						activeDB.Refresh(true);
						return;
					}
					finally
					{
						activeDB.DisableDiscoveryOptimization = false;
					}
				}
				activeDB.Refresh(true);
			}
		}

		// Token: 0x06001A0C RID: 6668 RVA: 0x000AC5C0 File Offset: 0x000AA7C0
		internal JsonCommandResult ExecuteImpl(Server server, Database activeDB)
		{
			Utils.Verify(this.isCommandValid);
			this.ApplyChangesLocally(server, activeDB);
			if (this.requiresLiveExecution)
			{
				return this.ExecuteCommandWithoutCaptureXML(server);
			}
			XmlaScript xmlaScript = this.GenerateXmlaScript(server);
			return this.ExecuteXmlaScript(server, xmlaScript);
		}

		// Token: 0x06001A0D RID: 6669
		internal abstract void ApplyChangesLocally(Server server, Database activeDB);

		// Token: 0x06001A0E RID: 6670 RVA: 0x000AC600 File Offset: 0x000AA800
		internal virtual JsonCommandResult ExecuteCommandWithoutCaptureXML(Server server)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001A0F RID: 6671
		internal abstract XmlaScript GenerateXmlaScript(Server server);

		// Token: 0x06001A10 RID: 6672 RVA: 0x000AC608 File Offset: 0x000AA808
		private protected static ObjectType CalculateTargetObjectType(IReadOnlyList<ObjectPath> objectPaths)
		{
			ObjectType objectType = ObjectType.Null;
			foreach (ObjectPath objectPath in objectPaths)
			{
				ObjectType key = objectPath.Last<KeyValuePair<ObjectType, string>>().Key;
				if (objectType == ObjectType.Null)
				{
					objectType = key;
				}
				else if (objectType != key)
				{
					return ObjectType.Null;
				}
			}
			return objectType;
		}

		// Token: 0x06001A11 RID: 6673
		private protected abstract void ParseProperties(JsonTextReader reader, CompatibilityMode mode);

		// Token: 0x06001A12 RID: 6674
		private protected abstract void ValidateProperties();

		// Token: 0x06001A13 RID: 6675
		private protected abstract void WriteProperties(JsonWriter writer);

		// Token: 0x06001A14 RID: 6676
		private protected abstract void WritePropertiesSchema(JsonWriter writer, CompatibilityMode mode, int dbCompatibilityLevel);

		// Token: 0x06001A15 RID: 6677
		private protected abstract string GetAffectedDatabaseName();

		// Token: 0x06001A16 RID: 6678 RVA: 0x000AC66C File Offset: 0x000AA86C
		private protected virtual bool CanAffectedDatabaseBeMissing()
		{
			return false;
		}

		// Token: 0x06001A17 RID: 6679 RVA: 0x000AC66F File Offset: 0x000AA86F
		private protected void EnsureNoTMDatabaseHasLocalChanges(Server server, Database dbWithAllowedLocalChanges)
		{
			if (server.AnyTMDatabaseHasLocalChangesImpl(dbWithAllowedLocalChanges))
			{
				throw new InvalidOperationException(TomSR.Exception_CannotExceuteJsonWithLocalchanges);
			}
		}

		// Token: 0x06001A18 RID: 6680 RVA: 0x000AC688 File Offset: 0x000AA888
		private static string CurateCommandText(string json)
		{
			string text;
			try
			{
				JObject jobject = JObject.Parse(json);
				List<JProperty> list = new List<JProperty>();
				Queue<JContainer> queue = new Queue<JContainer>();
				queue.Enqueue(jobject);
				while (queue.Count > 0)
				{
					foreach (JToken jtoken in queue.Dequeue().Children())
					{
						JProperty jproperty = jtoken as JProperty;
						if (jproperty != null)
						{
							if (JsonCommand.sensitiveProperties.ContainsKey(jproperty.Name))
							{
								list.Add(jproperty);
							}
							else
							{
								queue.Enqueue(jproperty);
							}
						}
						else
						{
							JContainer jcontainer = jtoken as JContainer;
							if (jcontainer != null)
							{
								queue.Enqueue(jcontainer);
							}
						}
					}
				}
				foreach (JProperty jproperty2 in list)
				{
					jproperty2.Value = JsonCommand.sensitiveProperties[jproperty2.Name](jproperty2.Value);
				}
				StringBuilder stringBuilder = new StringBuilder();
				using (TextWriter textWriter = new StringWriter(stringBuilder))
				{
					using (JsonTextWriter jsonTextWriter = new JsonTextWriter(textWriter))
					{
						jsonTextWriter.Formatting = 1;
						jobject.WriteTo(jsonTextWriter, Array.Empty<JsonConverter>());
					}
				}
				text = stringBuilder.ToString();
			}
			catch (JsonException)
			{
				text = string.Empty;
			}
			return text;
		}

		// Token: 0x06001A19 RID: 6681 RVA: 0x000AC86C File Offset: 0x000AAA6C
		private JsonCommandResult ExecuteXmlaScript(Server server, XmlaScript xmlaScript)
		{
			ModelOperationResult modelOperationResult = null;
			XmlaResultCollection xmlaResultCollection = null;
			if (xmlaScript.Commands.Count > 0)
			{
				if (!xmlaScript.Commands[0].IsTabular)
				{
					xmlaResultCollection = server.Execute(xmlaScript.Commands[0].CommandText);
					if (xmlaResultCollection.ContainsErrors)
					{
						throw new OperationException(TomSR.Exception_JsonScriptFailedToExecute(xmlaResultCollection.GetAggregatedMessage()), xmlaResultCollection, xmlaScript.Commands[0].CommandText);
					}
					if (server.CurrentTransaction != null && server.CurrentTransaction.ModifiedDatabase != null && server.CurrentTransaction.ModifiedDatabase.Parent != null)
					{
						server.CurrentTransaction.ModifiedDatabase.Refresh();
					}
				}
				bool flag = false;
				Model model = null;
				for (int i = 0; i < xmlaScript.Commands.Count; i++)
				{
					Utils.Verify(i == 0 || xmlaScript.Commands[i].IsTabular, "Only first command can be non-tabular");
					if (xmlaScript.Commands[i].IsTabular)
					{
						Database byName = server.Databases.GetByName(xmlaScript.Commands[i].DatabaseName);
						Utils.Verify(model == null || model == byName.Model, "It must not happen that multiple tabular commands are executed against different models");
						model = byName.Model;
						flag = true;
					}
				}
				if (flag)
				{
					Utils.Verify(model != null);
					string text = DdlUtil.FormatBatchWithCommands(from c in xmlaScript.Commands
						where c.IsTabular
						select c.CommandText, true);
					modelOperationResult = model.ExecuteXmla(text);
				}
			}
			return new JsonCommandResult
			{
				ModelOperationResult = modelOperationResult,
				XmlaResults = xmlaResultCollection
			};
		}

		// Token: 0x040004E2 RID: 1250
		private static readonly Dictionary<string, Func<JToken, JToken>> sensitiveProperties = new Dictionary<string, Func<JToken, JToken>>
		{
			{
				"connectionString",
				new Func<JToken, JToken>(PropertyHelper.GetCuratedValueForConnectionString)
			},
			{
				"password",
				new Func<JToken, JToken>(PropertyHelper.GetCuratedValueForPassword)
			},
			{
				"credential",
				new Func<JToken, JToken>(PropertyHelper.GetCuratedValueForCredential)
			}
		};

		// Token: 0x040004E3 RID: 1251
		private protected bool isCommandValid;

		// Token: 0x040004E4 RID: 1252
		private string commandName;

		// Token: 0x040004E5 RID: 1253
		private bool requiresLiveExecution;

		// Token: 0x040004E6 RID: 1254
		private bool hasCustomSchemaProperties;

		// Token: 0x020003F2 RID: 1010
		internal static class JsonSerializationOptions
		{
			// Token: 0x0400132D RID: 4909
			public static readonly SerializeOptions ObjectWithDescendants = new SerializeOptions
			{
				IgnoreTimestamps = true,
				IgnoreInferredObjects = true,
				IgnoreInferredProperties = true,
				SplitMultilineStrings = true
			};

			// Token: 0x0400132E RID: 4910
			public static readonly SerializeOptions TopLevelObjectOnly = new SerializeOptions
			{
				IgnoreTimestamps = true,
				IgnoreInferredObjects = true,
				IgnoreInferredProperties = true,
				IgnoreChildren = true
			};
		}

		// Token: 0x020003F3 RID: 1011
		internal static class JsonDeserializationOptions
		{
			// Token: 0x0400132F RID: 4911
			public static readonly DeserializeOptions Default = new DeserializeOptions
			{
				PartitionsMergedWithTable = false,
				ErrorOnUnresolvedLinks = true
			};
		}
	}
}
