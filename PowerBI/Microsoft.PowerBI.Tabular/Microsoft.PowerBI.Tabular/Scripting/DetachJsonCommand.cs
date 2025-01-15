using System;
using Microsoft.AnalysisServices.Core;
using Microsoft.AnalysisServices.Tabular.Extensions;
using Microsoft.AnalysisServices.Tabular.Json;

namespace Microsoft.AnalysisServices.Tabular.Scripting
{
	// Token: 0x020001A4 RID: 420
	internal sealed class DetachJsonCommand : JsonCommand
	{
		// Token: 0x060019DB RID: 6619 RVA: 0x000AB879 File Offset: 0x000A9A79
		public DetachJsonCommand(string databaseName, string password)
			: this()
		{
			this.databaseName = databaseName;
			this.password = password;
			this.isCommandValid = true;
		}

		// Token: 0x060019DC RID: 6620 RVA: 0x000AB896 File Offset: 0x000A9A96
		public DetachJsonCommand(Database database, string password)
			: this()
		{
			this.databaseName = database.Name;
			this.password = password;
			this.isCommandValid = true;
		}

		// Token: 0x060019DD RID: 6621 RVA: 0x000AB8B8 File Offset: 0x000A9AB8
		internal DetachJsonCommand()
			: base(JsonCommandType.Detach, "detach", "Detach", false, true, false, false)
		{
			base.ObjectType = ObjectType.Database;
		}

		// Token: 0x060019DE RID: 6622 RVA: 0x000AB8DB File Offset: 0x000A9ADB
		internal override void ApplyChangesLocally(Server server, Database activeDB)
		{
			Utils.Verify(activeDB != null);
			this.databaseToDetach = activeDB;
		}

		// Token: 0x060019DF RID: 6623 RVA: 0x000AB8F0 File Offset: 0x000A9AF0
		internal override XmlaScript GenerateXmlaScript(Server server)
		{
			Utils.Verify(!server.CaptureXml && server.CaptureLog.Count == 0, "Server must not be in capturing mode, and can't have any cached XMLA commands!");
			XmlaScript xmlaScript = new XmlaScript();
			try
			{
				server.CaptureXml = true;
				this.databaseToDetach.Detach(this.password);
				foreach (string text in server.CaptureLog)
				{
					xmlaScript.Commands.Add(new XmlaCommand
					{
						CommandText = text,
						DatabaseName = this.databaseToDetach.Name,
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

		// Token: 0x060019E0 RID: 6624 RVA: 0x000AB9D0 File Offset: 0x000A9BD0
		private protected override void ParseProperties(JsonTextReader reader, CompatibilityMode mode)
		{
			while (reader.TokenType != 13)
			{
				reader.VerifyToken(4);
				string text = (string)reader.Value;
				if (!(text == "database"))
				{
					if (!(text == "password"))
					{
						throw JsonSerializationUtil.CreateException(TomSR.Exception_UnexpectedJsonTag(text), reader, null);
					}
					reader.Read();
					reader.VerifyToken(9);
					this.password = (string)reader.Value;
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

		// Token: 0x060019E1 RID: 6625 RVA: 0x000ABA7E File Offset: 0x000A9C7E
		private protected override void ValidateProperties()
		{
			if (string.IsNullOrEmpty(this.databaseName))
			{
				throw new TomException(TomSR.Exception_JsonCommandDatabaseNotSpecified(base.CommandName));
			}
		}

		// Token: 0x060019E2 RID: 6626 RVA: 0x000ABA9E File Offset: 0x000A9C9E
		private protected override void WriteProperties(JsonWriter writer)
		{
			writer.WritePropertyName("database");
			writer.WriteValue(this.databaseName);
			if (!string.IsNullOrEmpty(this.password))
			{
				writer.WritePropertyName("password");
				writer.WriteValue(this.password);
			}
		}

		// Token: 0x060019E3 RID: 6627 RVA: 0x000ABADB File Offset: 0x000A9CDB
		private protected override void WritePropertiesSchema(JsonWriter writer, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WritePropertyName("database");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("password");
			JsonSchemaWriter.WriteSchemaForString(writer);
		}

		// Token: 0x060019E4 RID: 6628 RVA: 0x000ABAFF File Offset: 0x000A9CFF
		private protected override string GetAffectedDatabaseName()
		{
			return this.databaseName;
		}

		// Token: 0x040004DC RID: 1244
		private string databaseName;

		// Token: 0x040004DD RID: 1245
		private string password;

		// Token: 0x040004DE RID: 1246
		private Database databaseToDetach;
	}
}
