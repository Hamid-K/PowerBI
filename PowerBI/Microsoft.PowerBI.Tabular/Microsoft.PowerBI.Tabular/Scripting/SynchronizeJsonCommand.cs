using System;
using Microsoft.AnalysisServices.Tabular.Extensions;
using Microsoft.AnalysisServices.Tabular.Json;

namespace Microsoft.AnalysisServices.Tabular.Scripting
{
	// Token: 0x020001AE RID: 430
	internal sealed class SynchronizeJsonCommand : JsonCommand
	{
		// Token: 0x06001A60 RID: 6752 RVA: 0x000AF115 File Offset: 0x000AD315
		public SynchronizeJsonCommand(string databaseName, string source, SynchronizeSecurity? synchronizeSecurity, bool? applyCompression)
			: this()
		{
			this.databaseName = databaseName;
			this.source = source;
			this.synchronizeSecurity = synchronizeSecurity;
			this.applyCompression = applyCompression;
			this.isCommandValid = true;
		}

		// Token: 0x06001A61 RID: 6753 RVA: 0x000AF141 File Offset: 0x000AD341
		internal SynchronizeJsonCommand()
			: base(JsonCommandType.Synchronize, "synchronize", "Synchronize", false, true, false, false)
		{
			base.ObjectType = ObjectType.Database;
		}

		// Token: 0x06001A62 RID: 6754 RVA: 0x000AF164 File Offset: 0x000AD364
		internal override void ApplyChangesLocally(Server server, Database activeDB)
		{
		}

		// Token: 0x06001A63 RID: 6755 RVA: 0x000AF168 File Offset: 0x000AD368
		internal override XmlaScript GenerateXmlaScript(Server server)
		{
			Utils.Verify(!server.CaptureXml && server.CaptureLog.Count == 0, "Server must not be in capturing mode, and can't have any cached XMLA commands!");
			string id;
			using (Server server2 = new Server())
			{
				server2.Connect(this.source);
				Database database = server2.Databases.FindByName(this.databaseName);
				if (database == null)
				{
					throw new TomException(TomSR.Exception_JsonCommandDatabaseNotFound(base.CommandName, this.databaseName));
				}
				id = database.ID;
			}
			SynchronizeInfo synchronizeInfo = new SynchronizeInfo(id, this.source);
			if (this.synchronizeSecurity != null)
			{
				synchronizeInfo.SynchronizeSecurity = this.synchronizeSecurity.Value;
			}
			if (this.applyCompression != null)
			{
				synchronizeInfo.ApplyCompression = this.applyCompression.Value;
			}
			XmlaScript xmlaScript = new XmlaScript();
			try
			{
				server.CaptureXml = true;
				server.Synchronize(synchronizeInfo);
				foreach (string text in server.CaptureLog)
				{
					xmlaScript.Commands.Add(new XmlaCommand
					{
						CommandText = text,
						DatabaseName = this.databaseName,
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

		// Token: 0x06001A64 RID: 6756 RVA: 0x000AF2E4 File Offset: 0x000AD4E4
		private protected override void ParseProperties(JsonTextReader reader, CompatibilityMode mode)
		{
			while (reader.TokenType != 13)
			{
				reader.VerifyToken(4);
				string text = (string)reader.Value;
				if (!(text == "database"))
				{
					if (!(text == "source"))
					{
						if (!(text == "synchronizeSecurity"))
						{
							if (!(text == "applyCompression"))
							{
								throw JsonSerializationUtil.CreateException(TomSR.Exception_UnexpectedJsonTag(text), reader, null);
							}
							reader.Read();
							reader.VerifyToken(10);
							this.applyCompression = new bool?((bool)reader.Value);
							reader.Read();
						}
						else
						{
							reader.Read();
							reader.VerifyToken(9);
							string text2 = (string)reader.Value;
							this.synchronizeSecurity = new SynchronizeSecurity?(JsonPropertyHelper.ConvertStringToEnum<SynchronizeSecurity>(text2, reader));
							reader.Read();
						}
					}
					else
					{
						reader.Read();
						reader.VerifyToken(9);
						this.source = (string)reader.Value;
						reader.Read();
					}
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

		// Token: 0x06001A65 RID: 6757 RVA: 0x000AF41C File Offset: 0x000AD61C
		private protected override void ValidateProperties()
		{
			if (string.IsNullOrEmpty(this.databaseName))
			{
				throw new TomException(TomSR.Exception_JsonCommandDatabaseNotSpecified(base.CommandName));
			}
			if (string.IsNullOrEmpty(this.source))
			{
				throw new TomException(TomSR.Exception_JsonCommandParameterIsMissing(base.CommandName, "source"));
			}
		}

		// Token: 0x06001A66 RID: 6758 RVA: 0x000AF46C File Offset: 0x000AD66C
		private protected override void WriteProperties(JsonWriter writer)
		{
			writer.WritePropertyName("database");
			writer.WriteValue(this.databaseName);
			writer.WritePropertyName("source");
			writer.WriteValue(this.source);
			if (this.synchronizeSecurity != null)
			{
				writer.WritePropertyName("synchronizeSecurity");
				writer.WriteValue(JsonPropertyHelper.ConvertEnumToJsonValue<SynchronizeSecurity>(this.synchronizeSecurity.Value));
			}
			if (this.applyCompression != null)
			{
				writer.WritePropertyName("applyCompression");
				writer.WriteValue(this.applyCompression.Value);
			}
		}

		// Token: 0x06001A67 RID: 6759 RVA: 0x000AF500 File Offset: 0x000AD700
		private protected override void WritePropertiesSchema(JsonWriter writer, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WritePropertyName("database");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("source");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("synchronizeSecurity");
			JsonSchemaWriter.WriteSchemaForEnum(writer, typeof(SynchronizeSecurity));
			writer.WritePropertyName("applyCompression");
			JsonSchemaWriter.WriteSchemaForBoolean(writer);
		}

		// Token: 0x06001A68 RID: 6760 RVA: 0x000AF55B File Offset: 0x000AD75B
		private protected override string GetAffectedDatabaseName()
		{
			return this.databaseName;
		}

		// Token: 0x06001A69 RID: 6761 RVA: 0x000AF563 File Offset: 0x000AD763
		private protected override bool CanAffectedDatabaseBeMissing()
		{
			return true;
		}

		// Token: 0x04000519 RID: 1305
		private string databaseName;

		// Token: 0x0400051A RID: 1306
		private string source;

		// Token: 0x0400051B RID: 1307
		private SynchronizeSecurity? synchronizeSecurity;

		// Token: 0x0400051C RID: 1308
		private bool? applyCompression;
	}
}
