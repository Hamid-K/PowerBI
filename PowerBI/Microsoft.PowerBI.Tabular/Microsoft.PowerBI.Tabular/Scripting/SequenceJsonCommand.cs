using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AnalysisServices.Tabular.Extensions;
using Microsoft.AnalysisServices.Tabular.Json;

namespace Microsoft.AnalysisServices.Tabular.Scripting
{
	// Token: 0x020001AD RID: 429
	internal sealed class SequenceJsonCommand : JsonCommand
	{
		// Token: 0x06001A51 RID: 6737 RVA: 0x000AEAB9 File Offset: 0x000ACCB9
		public SequenceJsonCommand()
			: base(JsonCommandType.Sequence, "sequence", "Sequence", true, false, false, false)
		{
			this.childCommands = new List<JsonCommand>();
			this.isCommandValid = true;
			base.ObjectType = ObjectType.Null;
		}

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x06001A52 RID: 6738 RVA: 0x000AEAE9 File Offset: 0x000ACCE9
		public IReadOnlyList<JsonCommand> ChildCommands
		{
			get
			{
				return this.childCommands;
			}
		}

		// Token: 0x06001A53 RID: 6739 RVA: 0x000AEAF1 File Offset: 0x000ACCF1
		public void AddChildCommand(JsonCommand command)
		{
			this.AddChildCommandImpl(command);
		}

		// Token: 0x06001A54 RID: 6740 RVA: 0x000AEAFC File Offset: 0x000ACCFC
		public bool ExecuteChunk(Server server, out JsonCommandResult cmdResult)
		{
			Utils.Verify(!server.CaptureXml && server.CaptureLog.Count == 0, "Server must not be in capturing mode, and can't have any cached XMLA commands!");
			if (this.childCommands.Count == 0)
			{
				cmdResult = new JsonCommandResult
				{
					ModelOperationResult = new ModelOperationResult(),
					XmlaResults = new XmlaResultCollection()
				};
				return false;
			}
			RefreshType? refreshType = null;
			bool flag = false;
			while (this.currentCommandIdx < this.childCommands.Count && this.childCommands[this.currentCommandIdx].CommandType == JsonCommandType.Refresh)
			{
				RefreshJsonCommand refreshJsonCommand = (RefreshJsonCommand)this.childCommands[this.currentCommandIdx];
				RefreshType? refreshType2 = refreshJsonCommand.RefreshType;
				if (refreshType2 == null)
				{
					throw new TomException(TomSR.Exception_JsonCommandTypeNotSpecified(base.CommandName));
				}
				if (refreshType != null && !SequenceJsonCommand.CanOneRefreshFollowAnother(refreshType.Value, refreshType2.Value))
				{
					Utils.Verify(flag);
					break;
				}
				this.EnsureActiveDB(server, refreshJsonCommand);
				refreshType = refreshType2;
				refreshJsonCommand.ApplyChangesLocally(server, this.activeDB);
				flag = true;
				this.currentCommandIdx++;
			}
			if (flag)
			{
				ModelOperationResult modelOperationResult = this.activeDB.Model.SaveChangesImpl(SaveFlags.Default, (base.MaxParallelism != null) ? base.MaxParallelism.Value : 0);
				cmdResult = new JsonCommandResult
				{
					ModelOperationResult = modelOperationResult
				};
				return this.currentCommandIdx < this.childCommands.Count;
			}
			Utils.Verify(this.currentCommandIdx < this.childCommands.Count);
			this.EnsureActiveDB(server, this.childCommands[this.currentCommandIdx]);
			cmdResult = this.childCommands[this.currentCommandIdx].ExecuteImpl(server, this.activeDB);
			this.currentCommandIdx++;
			return this.currentCommandIdx < this.childCommands.Count;
		}

		// Token: 0x06001A55 RID: 6741 RVA: 0x000AECEA File Offset: 0x000ACEEA
		internal override void ApplyChangesLocally(Server server, Database activeDB)
		{
			throw new TomInternalException("Sequence command cannot be Executed. The caller must enumerate nested commands and Execute them one by one.");
		}

		// Token: 0x06001A56 RID: 6742 RVA: 0x000AECF6 File Offset: 0x000ACEF6
		internal override XmlaScript GenerateXmlaScript(Server server)
		{
			throw new TomInternalException("Sequence command cannot be Executed. The caller must enumerate nested commands and Execute them one by one.");
		}

		// Token: 0x06001A57 RID: 6743 RVA: 0x000AED04 File Offset: 0x000ACF04
		private protected override void ParseProperties(JsonTextReader reader, CompatibilityMode mode)
		{
			while (reader.TokenType != 13)
			{
				reader.VerifyToken(4);
				string text = (string)reader.Value;
				if (!(text == "maxParallelism"))
				{
					if (!(text == "operations"))
					{
						throw JsonSerializationUtil.CreateException(TomSR.Exception_UnexpectedJsonTag(text), reader, null);
					}
					reader.Read();
					reader.VerifyToken(2);
					reader.Read();
					while (reader.TokenType != 14)
					{
						reader.VerifyToken(1);
						reader.Read();
						reader.VerifyToken(4);
						JsonCommand jsonCommand;
						if (!JsonCommandFactory.TryCreateCommandByTag((string)reader.Value, out jsonCommand))
						{
							throw JsonSerializationUtil.CreateException(TomSR.Exception_UnrecognizedJsonCommand((string)reader.Value), reader, null);
						}
						if (jsonCommand.CommandType == JsonCommandType.Sequence)
						{
							throw new TomException(TomSR.Exception_JsonCommandSequenceNestedNotSupported);
						}
						reader.Read();
						reader.VerifyToken(1);
						jsonCommand.Parse(reader, mode);
						this.AddChildCommandImpl(jsonCommand);
						reader.VerifyToken(13);
						reader.Read();
					}
					reader.VerifyToken(14);
					reader.Read();
				}
				else
				{
					reader.Read();
					reader.VerifyToken(7);
					base.MaxParallelism = new int?((int)((long)reader.Value));
					reader.Read();
				}
			}
		}

		// Token: 0x06001A58 RID: 6744 RVA: 0x000AEE4D File Offset: 0x000AD04D
		private protected override void ValidateProperties()
		{
		}

		// Token: 0x06001A59 RID: 6745 RVA: 0x000AEE50 File Offset: 0x000AD050
		private protected override void WriteProperties(JsonWriter writer)
		{
			if (base.MaxParallelism != null)
			{
				writer.WritePropertyName("maxParallelism");
				writer.WriteValue(base.MaxParallelism.Value);
			}
			writer.WritePropertyName("operations");
			writer.WriteStartArray();
			foreach (JsonCommand jsonCommand in this.childCommands)
			{
				jsonCommand.Write(writer);
			}
			writer.WriteEndArray();
		}

		// Token: 0x06001A5A RID: 6746 RVA: 0x000AEEE8 File Offset: 0x000AD0E8
		private protected override void WritePropertiesSchema(JsonWriter writer, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WritePropertyName("maxParallelism");
			JsonSchemaWriter.WriteSchemaForInteger(writer);
			writer.WritePropertyName("operations");
			writer.WriteStartObject();
			writer.WritePropertyName("type");
			writer.WriteValue("array");
			writer.WritePropertyName("items");
			writer.WriteStartObject();
			writer.WritePropertyName("anyOf");
			writer.WriteStartArray();
			foreach (JsonCommand jsonCommand in from c in JsonCommandFactory.GetJsonSchemaSupportedCommands(mode, dbCompatibilityLevel)
				where c.CommandType != JsonCommandType.Sequence
				select c)
			{
				jsonCommand.WriteSchema(writer, true, mode, dbCompatibilityLevel);
			}
			writer.WriteEndArray();
			writer.WriteEndObject();
			writer.WriteEndObject();
		}

		// Token: 0x06001A5B RID: 6747 RVA: 0x000AEFC8 File Offset: 0x000AD1C8
		private protected override string GetAffectedDatabaseName()
		{
			JsonCommand jsonCommand = this.childCommands.FirstOrDefault<JsonCommand>();
			if (jsonCommand != null)
			{
				return jsonCommand.DatabaseName;
			}
			return null;
		}

		// Token: 0x06001A5C RID: 6748 RVA: 0x000AEFEC File Offset: 0x000AD1EC
		private static bool CanOneRefreshFollowAnother(RefreshType first, RefreshType second)
		{
			return SequenceJsonCommand.refreshTypeOrders.ContainsKey(first) && SequenceJsonCommand.refreshTypeOrders.ContainsKey(second) && SequenceJsonCommand.refreshTypeOrders[first] <= SequenceJsonCommand.refreshTypeOrders[second];
		}

		// Token: 0x06001A5D RID: 6749 RVA: 0x000AF028 File Offset: 0x000AD228
		private void AddChildCommandImpl(JsonCommand command)
		{
			this.childCommands.Add(command);
			if (base.MaxParallelism != null)
			{
				command.MaxParallelism = new int?(base.MaxParallelism.Value);
			}
			command.ParentSequence = this;
			base.IsIntrusiveCommand |= command.IsIntrusiveCommand;
		}

		// Token: 0x06001A5E RID: 6750 RVA: 0x000AF084 File Offset: 0x000AD284
		private void EnsureActiveDB(Server server, JsonCommand command)
		{
			if (this.activeDB == null)
			{
				command.SyncBeforeExecutionImpl(server, out this.activeDB);
				return;
			}
			Database affectedDatabase = command.GetAffectedDatabase(server);
			if (affectedDatabase != null && this.activeDB != affectedDatabase)
			{
				throw new InvalidOperationException(TomSR.Exception_JsonCommandSequenceMultipleDbsNotSupported);
			}
		}

		// Token: 0x04000515 RID: 1301
		private static readonly IDictionary<RefreshType, int> refreshTypeOrders = new Dictionary<RefreshType, int>
		{
			{
				RefreshType.ClearValues,
				10
			},
			{
				RefreshType.DataOnly,
				20
			},
			{
				RefreshType.Calculate,
				30
			},
			{
				RefreshType.Full,
				30
			},
			{
				RefreshType.Automatic,
				30
			},
			{
				RefreshType.Add,
				30
			}
		};

		// Token: 0x04000516 RID: 1302
		private List<JsonCommand> childCommands;

		// Token: 0x04000517 RID: 1303
		private Database activeDB;

		// Token: 0x04000518 RID: 1304
		private int currentCommandIdx;
	}
}
