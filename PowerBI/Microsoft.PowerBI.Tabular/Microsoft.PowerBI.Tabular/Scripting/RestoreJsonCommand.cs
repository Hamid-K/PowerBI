using System;
using Microsoft.AnalysisServices.Tabular.Extensions;
using Microsoft.AnalysisServices.Tabular.Json;

namespace Microsoft.AnalysisServices.Tabular.Scripting
{
	// Token: 0x020001AC RID: 428
	internal class RestoreJsonCommand : JsonCommand
	{
		// Token: 0x06001A47 RID: 6727 RVA: 0x000AE2FC File Offset: 0x000AC4FC
		public RestoreJsonCommand(string filePath, string databaseName, bool? allowOverwrite, string password, string dbStorageLocation, ReadWriteMode? readWriteMode, RestoreSecurity? restoreSecurity, bool? ignoreIncompatibilities, bool? forceRestore)
			: this()
		{
			this.filePath = filePath;
			this.databaseName = databaseName;
			this.allowOverwrite = allowOverwrite;
			this.password = password;
			this.dbStorageLocation = dbStorageLocation;
			this.readWriteMode = readWriteMode;
			this.restoreSecurity = restoreSecurity;
			this.ignoreIncompatibilities = ignoreIncompatibilities;
			this.forceRestore = forceRestore;
			this.isCommandValid = true;
		}

		// Token: 0x06001A48 RID: 6728 RVA: 0x000AE35B File Offset: 0x000AC55B
		internal RestoreJsonCommand()
			: base(JsonCommandType.Restore, "restore", "Restore", false, true, false, false)
		{
			base.ObjectType = ObjectType.Database;
		}

		// Token: 0x06001A49 RID: 6729 RVA: 0x000AE37D File Offset: 0x000AC57D
		internal override void ApplyChangesLocally(Server server, Database activeDB)
		{
		}

		// Token: 0x06001A4A RID: 6730 RVA: 0x000AE380 File Offset: 0x000AC580
		internal override XmlaScript GenerateXmlaScript(Server server)
		{
			Utils.Verify(!server.CaptureXml && server.CaptureLog.Count == 0, "Server must not be in capturing mode, and can't have any cached XMLA commands!");
			RestoreInfo restoreInfo = new RestoreInfo
			{
				DatabaseName = this.databaseName,
				File = this.filePath,
				Password = this.password,
				DbStorageLocation = this.dbStorageLocation
			};
			if (this.allowOverwrite != null)
			{
				restoreInfo.AllowOverwrite = this.allowOverwrite.Value;
			}
			if (this.readWriteMode != null)
			{
				restoreInfo.DatabaseReadWriteMode = this.readWriteMode.Value;
			}
			if (this.restoreSecurity != null)
			{
				restoreInfo.Security = this.restoreSecurity.Value;
			}
			if (this.ignoreIncompatibilities != null)
			{
				restoreInfo.IgnoreIncompatibilities = this.ignoreIncompatibilities.Value;
			}
			if (this.forceRestore != null)
			{
				restoreInfo.ForceRestore = this.forceRestore.Value;
			}
			XmlaScript xmlaScript = new XmlaScript();
			try
			{
				server.CaptureXml = true;
				server.Restore(restoreInfo);
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

		// Token: 0x06001A4B RID: 6731 RVA: 0x000AE520 File Offset: 0x000AC720
		private protected override void ParseProperties(JsonTextReader reader, CompatibilityMode mode)
		{
			while (reader.TokenType != 13)
			{
				reader.VerifyToken(4);
				string text = (string)reader.Value;
				if (text != null)
				{
					int length = text.Length;
					if (length <= 14)
					{
						if (length != 4)
						{
							switch (length)
							{
							case 8:
							{
								char c = text[0];
								if (c != 'd')
								{
									if (c != 'p')
									{
										if (c == 's')
										{
											if (text == "security")
											{
												reader.Read();
												reader.VerifyToken(9);
												string text2 = (string)reader.Value;
												this.restoreSecurity = new RestoreSecurity?(JsonPropertyHelper.ConvertStringToEnum<RestoreSecurity>(text2, reader));
												reader.Read();
												continue;
											}
										}
									}
									else if (text == "password")
									{
										reader.Read();
										reader.VerifyToken(9);
										this.password = (string)reader.Value;
										reader.Read();
										continue;
									}
								}
								else if (text == "database")
								{
									reader.Read();
									reader.VerifyToken(9);
									this.databaseName = (string)reader.Value;
									reader.Read();
									continue;
								}
								break;
							}
							case 12:
								if (text == "forceRestore")
								{
									reader.Read();
									reader.VerifyToken(10);
									this.forceRestore = new bool?((bool)reader.Value);
									reader.Read();
									continue;
								}
								break;
							case 13:
								if (text == "readWriteMode")
								{
									reader.Read();
									reader.VerifyToken(9);
									string text3 = (string)reader.Value;
									this.readWriteMode = new ReadWriteMode?(JsonPropertyHelper.ConvertStringToEnum<ReadWriteMode>(text3, reader));
									reader.Read();
									continue;
								}
								break;
							case 14:
								if (text == "allowOverwrite")
								{
									reader.Read();
									reader.VerifyToken(10);
									this.allowOverwrite = new bool?((bool)reader.Value);
									reader.Read();
									continue;
								}
								break;
							}
						}
						else if (text == "file")
						{
							reader.Read();
							reader.VerifyToken(9);
							this.filePath = (string)reader.Value;
							reader.Read();
							continue;
						}
					}
					else if (length != 17)
					{
						if (length == 23)
						{
							if (text == "ignoreIncompatibilities")
							{
								reader.Read();
								reader.VerifyToken(10);
								this.ignoreIncompatibilities = new bool?((bool)reader.Value);
								reader.Read();
								continue;
							}
						}
					}
					else if (text == "dbStorageLocation")
					{
						reader.Read();
						reader.VerifyToken(9);
						this.dbStorageLocation = (string)reader.Value;
						reader.Read();
						continue;
					}
				}
				throw JsonSerializationUtil.CreateException(TomSR.Exception_UnexpectedJsonTag(text), reader, null);
			}
		}

		// Token: 0x06001A4C RID: 6732 RVA: 0x000AE840 File Offset: 0x000ACA40
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

		// Token: 0x06001A4D RID: 6733 RVA: 0x000AE890 File Offset: 0x000ACA90
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
			if (!string.IsNullOrEmpty(this.dbStorageLocation))
			{
				writer.WritePropertyName("dbStorageLocation");
				writer.WriteValue(this.dbStorageLocation);
			}
			if (this.allowOverwrite != null)
			{
				writer.WritePropertyName("allowOverwrite");
				writer.WriteValue(this.allowOverwrite.Value);
			}
			if (this.ignoreIncompatibilities != null)
			{
				writer.WritePropertyName("ignoreIncompatibilities");
				writer.WriteValue(this.allowOverwrite.Value);
			}
			if (this.forceRestore != null)
			{
				writer.WritePropertyName("forceRestore");
				writer.WriteValue(this.allowOverwrite.Value);
			}
			if (this.readWriteMode != null)
			{
				writer.WritePropertyName("readWriteMode");
				writer.WriteValue(JsonPropertyHelper.ConvertEnumToJsonValue<ReadWriteMode>(this.readWriteMode.Value));
			}
			if (this.restoreSecurity != null)
			{
				writer.WritePropertyName("security");
				writer.WriteValue(JsonPropertyHelper.ConvertEnumToJsonValue<RestoreSecurity>(this.restoreSecurity.Value));
			}
		}

		// Token: 0x06001A4E RID: 6734 RVA: 0x000AE9EC File Offset: 0x000ACBEC
		private protected override void WritePropertiesSchema(JsonWriter writer, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WritePropertyName("database");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("file");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("password");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("dbStorageLocation");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("allowOverwrite");
			JsonSchemaWriter.WriteSchemaForBoolean(writer);
			writer.WritePropertyName("ignoreIncompatibilities");
			JsonSchemaWriter.WriteSchemaForBoolean(writer);
			writer.WritePropertyName("forceRestore");
			JsonSchemaWriter.WriteSchemaForBoolean(writer);
			writer.WritePropertyName("readWriteMode");
			JsonSchemaWriter.WriteSchemaForEnum(writer, typeof(ReadWriteMode));
			if (dbCompatibilityLevel >= 1400)
			{
				writer.WritePropertyName("security");
				JsonSchemaWriter.WriteSchemaForEnum(writer, typeof(RestoreSecurity));
			}
		}

		// Token: 0x06001A4F RID: 6735 RVA: 0x000AEAAE File Offset: 0x000ACCAE
		private protected override string GetAffectedDatabaseName()
		{
			return this.databaseName;
		}

		// Token: 0x06001A50 RID: 6736 RVA: 0x000AEAB6 File Offset: 0x000ACCB6
		private protected override bool CanAffectedDatabaseBeMissing()
		{
			return true;
		}

		// Token: 0x0400050C RID: 1292
		private string filePath;

		// Token: 0x0400050D RID: 1293
		private string databaseName;

		// Token: 0x0400050E RID: 1294
		private string password;

		// Token: 0x0400050F RID: 1295
		private string dbStorageLocation;

		// Token: 0x04000510 RID: 1296
		private ReadWriteMode? readWriteMode;

		// Token: 0x04000511 RID: 1297
		private bool? allowOverwrite;

		// Token: 0x04000512 RID: 1298
		private RestoreSecurity? restoreSecurity;

		// Token: 0x04000513 RID: 1299
		private bool? ignoreIncompatibilities;

		// Token: 0x04000514 RID: 1300
		private bool? forceRestore;
	}
}
