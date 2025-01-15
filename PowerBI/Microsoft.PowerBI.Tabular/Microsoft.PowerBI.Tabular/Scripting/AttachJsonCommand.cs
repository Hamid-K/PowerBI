using System;
using Microsoft.AnalysisServices.Tabular.Extensions;
using Microsoft.AnalysisServices.Tabular.Json;

namespace Microsoft.AnalysisServices.Tabular.Scripting
{
	// Token: 0x0200019F RID: 415
	internal sealed class AttachJsonCommand : JsonCommand
	{
		// Token: 0x0600199A RID: 6554 RVA: 0x000A9D84 File Offset: 0x000A7F84
		public AttachJsonCommand(string folderPath, ReadWriteMode? readWriteMode, string password)
			: this()
		{
			this.folderPath = folderPath;
			this.password = password;
			this.readWriteMode = readWriteMode;
			this.isCommandValid = true;
		}

		// Token: 0x0600199B RID: 6555 RVA: 0x000A9DA8 File Offset: 0x000A7FA8
		internal AttachJsonCommand()
			: base(JsonCommandType.Attach, "attach", "Attach", false, true, false, false)
		{
			base.ObjectType = ObjectType.Database;
		}

		// Token: 0x0600199C RID: 6556 RVA: 0x000A9DCB File Offset: 0x000A7FCB
		internal override void ApplyChangesLocally(Server server, Database activeDB)
		{
		}

		// Token: 0x0600199D RID: 6557 RVA: 0x000A9DD0 File Offset: 0x000A7FD0
		internal override XmlaScript GenerateXmlaScript(Server server)
		{
			Utils.Verify(!server.CaptureXml && server.CaptureLog.Count == 0, "Server must not be in capturing mode, and can't have any cached XMLA commands!");
			XmlaScript xmlaScript = new XmlaScript();
			try
			{
				server.CaptureXml = true;
				server.Attach(this.folderPath, (this.readWriteMode != null) ? this.readWriteMode.Value : ReadWriteMode.ReadWrite, this.password);
				foreach (string text in server.CaptureLog)
				{
					xmlaScript.Commands.Add(new XmlaCommand
					{
						CommandText = text,
						DatabaseName = null,
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

		// Token: 0x0600199E RID: 6558 RVA: 0x000A9EC0 File Offset: 0x000A80C0
		private protected override void ParseProperties(JsonTextReader reader, CompatibilityMode mode)
		{
			while (reader.TokenType != 13)
			{
				reader.VerifyToken(4);
				string text = (string)reader.Value;
				if (!(text == "folder"))
				{
					if (!(text == "password"))
					{
						if (!(text == "readWriteMode"))
						{
							throw JsonSerializationUtil.CreateException(TomSR.Exception_UnexpectedJsonTag(text), reader, null);
						}
						reader.Read();
						reader.VerifyToken(9);
						string text2 = (string)reader.Value;
						this.readWriteMode = new ReadWriteMode?(JsonPropertyHelper.ConvertStringToEnum<ReadWriteMode>(text2, reader));
						reader.Read();
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
					this.folderPath = (string)reader.Value;
					reader.Read();
				}
			}
		}

		// Token: 0x0600199F RID: 6559 RVA: 0x000A9FB4 File Offset: 0x000A81B4
		private protected override void ValidateProperties()
		{
			if (string.IsNullOrEmpty(this.folderPath))
			{
				throw new TomException(TomSR.Exception_JsonCommandParameterIsMissing(base.CommandName, "folder"));
			}
		}

		// Token: 0x060019A0 RID: 6560 RVA: 0x000A9FDC File Offset: 0x000A81DC
		private protected override void WriteProperties(JsonWriter writer)
		{
			writer.WritePropertyName("folder");
			writer.WriteValue(this.folderPath);
			if (!string.IsNullOrEmpty(this.password))
			{
				writer.WritePropertyName("password");
				writer.WriteValue(this.password);
			}
			if (this.readWriteMode != null)
			{
				writer.WritePropertyName("readWriteMode");
				writer.WriteValue(JsonPropertyHelper.ConvertEnumToJsonValue<ReadWriteMode>(this.readWriteMode.Value));
			}
		}

		// Token: 0x060019A1 RID: 6561 RVA: 0x000AA052 File Offset: 0x000A8252
		private protected override void WritePropertiesSchema(JsonWriter writer, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WritePropertyName("folder");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("password");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("readWriteMode");
			JsonSchemaWriter.WriteSchemaForEnum(writer, typeof(ReadWriteMode));
		}

		// Token: 0x060019A2 RID: 6562 RVA: 0x000AA091 File Offset: 0x000A8291
		private protected override string GetAffectedDatabaseName()
		{
			return null;
		}

		// Token: 0x060019A3 RID: 6563 RVA: 0x000AA094 File Offset: 0x000A8294
		private protected override bool CanAffectedDatabaseBeMissing()
		{
			return true;
		}

		// Token: 0x040004CB RID: 1227
		private string folderPath;

		// Token: 0x040004CC RID: 1228
		private string password;

		// Token: 0x040004CD RID: 1229
		private ReadWriteMode? readWriteMode;
	}
}
