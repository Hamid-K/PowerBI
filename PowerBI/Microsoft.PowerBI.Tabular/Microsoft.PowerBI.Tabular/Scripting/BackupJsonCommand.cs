using System;
using Microsoft.AnalysisServices.Core;
using Microsoft.AnalysisServices.Tabular.Extensions;
using Microsoft.AnalysisServices.Tabular.Json;

namespace Microsoft.AnalysisServices.Tabular.Scripting
{
	// Token: 0x020001A0 RID: 416
	internal sealed class BackupJsonCommand : JsonCommand
	{
		// Token: 0x060019A4 RID: 6564 RVA: 0x000AA097 File Offset: 0x000A8297
		public BackupJsonCommand(Database database, string filePath, string password, bool? allowOverwrite, bool? applyCompression)
			: this()
		{
			this.databaseName = database.Name;
			this.filePath = filePath;
			this.password = password;
			this.allowOverwrite = allowOverwrite;
			this.applyCompression = applyCompression;
			this.isCommandValid = true;
		}

		// Token: 0x060019A5 RID: 6565 RVA: 0x000AA0D0 File Offset: 0x000A82D0
		internal BackupJsonCommand()
			: base(JsonCommandType.Backup, "backup", "Backup", true, true, false, false)
		{
			base.ObjectType = ObjectType.Database;
		}

		// Token: 0x060019A6 RID: 6566 RVA: 0x000AA0F2 File Offset: 0x000A82F2
		internal override void ApplyChangesLocally(Server server, Database activeDB)
		{
		}

		// Token: 0x060019A7 RID: 6567 RVA: 0x000AA0F4 File Offset: 0x000A82F4
		internal override XmlaScript GenerateXmlaScript(Server server)
		{
			Utils.Verify(!server.CaptureXml && server.CaptureLog.Count == 0, "Server must not be in capturing mode, and can't have any cached XMLA commands!");
			Database affectedDatabase = base.GetAffectedDatabase(server);
			BackupInfo backupInfo = new BackupInfo
			{
				File = this.filePath,
				Password = this.password
			};
			if (this.allowOverwrite != null)
			{
				backupInfo.AllowOverwrite = this.allowOverwrite.Value;
			}
			if (this.applyCompression != null)
			{
				backupInfo.ApplyCompression = this.applyCompression.Value;
			}
			XmlaScript xmlaScript = new XmlaScript();
			try
			{
				server.CaptureXml = true;
				affectedDatabase.Backup(backupInfo);
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

		// Token: 0x060019A8 RID: 6568 RVA: 0x000AA22C File Offset: 0x000A842C
		private protected override void ParseProperties(JsonTextReader reader, CompatibilityMode mode)
		{
			while (reader.TokenType != 13)
			{
				reader.VerifyToken(4);
				string text = (string)reader.Value;
				if (!(text == "database"))
				{
					if (!(text == "file"))
					{
						if (!(text == "password"))
						{
							if (!(text == "applyCompression"))
							{
								if (!(text == "allowOverwrite"))
								{
									throw JsonSerializationUtil.CreateException(TomSR.Exception_UnexpectedJsonTag(text), reader, null);
								}
								reader.Read();
								reader.VerifyToken(10);
								this.allowOverwrite = new bool?((bool)reader.Value);
								reader.Read();
							}
							else
							{
								reader.Read();
								reader.VerifyToken(10);
								this.applyCompression = new bool?((bool)reader.Value);
								reader.Read();
							}
						}
						else
						{
							reader.Read();
							reader.VerifyToken(9);
							this.password = (string)reader.Value;
							reader.Read();
						}
					}
					else
					{
						reader.Read();
						reader.VerifyToken(9);
						this.filePath = (string)reader.Value;
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

		// Token: 0x060019A9 RID: 6569 RVA: 0x000AA398 File Offset: 0x000A8598
		private protected override void ValidateProperties()
		{
			if (string.IsNullOrEmpty(this.databaseName))
			{
				throw new TomException(TomSR.Exception_JsonCommandDatabaseNotSpecified(base.CommandName));
			}
			if (string.IsNullOrEmpty(this.filePath))
			{
				throw new TomException(TomSR.Exception_JsonCommandParameterIsMissing(base.CommandName, "file"));
			}
		}

		// Token: 0x060019AA RID: 6570 RVA: 0x000AA3E8 File Offset: 0x000A85E8
		private protected override void WriteProperties(JsonWriter writer)
		{
			writer.WritePropertyName("database");
			writer.WriteValue(this.databaseName);
			writer.WritePropertyName("file");
			writer.WriteValue(this.filePath);
			if (!string.IsNullOrEmpty(this.password))
			{
				writer.WritePropertyName("password");
				writer.WriteValue(this.password);
			}
			if (this.allowOverwrite != null)
			{
				writer.WritePropertyName("allowOverwrite");
				writer.WriteValue(this.allowOverwrite.Value);
			}
			if (this.applyCompression != null)
			{
				writer.WritePropertyName("applyCompression");
				writer.WriteValue(this.applyCompression.Value);
			}
		}

		// Token: 0x060019AB RID: 6571 RVA: 0x000AA49C File Offset: 0x000A869C
		private protected override void WritePropertiesSchema(JsonWriter writer, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WritePropertyName("database");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("file");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("password");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("allowOverwrite");
			JsonSchemaWriter.WriteSchemaForBoolean(writer);
			writer.WritePropertyName("applyCompression");
			JsonSchemaWriter.WriteSchemaForBoolean(writer);
		}

		// Token: 0x060019AC RID: 6572 RVA: 0x000AA4FE File Offset: 0x000A86FE
		private protected override string GetAffectedDatabaseName()
		{
			return this.databaseName;
		}

		// Token: 0x040004CE RID: 1230
		private string databaseName;

		// Token: 0x040004CF RID: 1231
		private string filePath;

		// Token: 0x040004D0 RID: 1232
		private string password;

		// Token: 0x040004D1 RID: 1233
		private bool? applyCompression;

		// Token: 0x040004D2 RID: 1234
		private bool? allowOverwrite;
	}
}
