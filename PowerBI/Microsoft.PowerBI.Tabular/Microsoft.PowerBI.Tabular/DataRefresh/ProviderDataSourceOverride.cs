using System;
using Microsoft.AnalysisServices.Tabular.Extensions;
using Microsoft.AnalysisServices.Tabular.Json;
using Microsoft.AnalysisServices.Tabular.Json.Linq;
using Microsoft.AnalysisServices.Tabular.Metadata;
using Microsoft.AnalysisServices.Tabular.Utilities;

namespace Microsoft.AnalysisServices.Tabular.DataRefresh
{
	// Token: 0x02000214 RID: 532
	public class ProviderDataSourceOverride : DataSourceOverride
	{
		// Token: 0x06001DFF RID: 7679 RVA: 0x000C9E00 File Offset: 0x000C8000
		public ProviderDataSourceOverride()
		{
			this.replacementProperties = new ProviderDataSourceOverride.Overrides();
		}

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x06001E00 RID: 7680 RVA: 0x000C9E13 File Offset: 0x000C8013
		// (set) Token: 0x06001E01 RID: 7681 RVA: 0x000C9E1B File Offset: 0x000C801B
		public ProviderDataSource OriginalObject { get; set; }

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x06001E02 RID: 7682 RVA: 0x000C9E24 File Offset: 0x000C8024
		// (set) Token: 0x06001E03 RID: 7683 RVA: 0x000C9E2C File Offset: 0x000C802C
		internal ObjectPath OriginalObjectPath { get; set; }

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x06001E04 RID: 7684 RVA: 0x000C9E35 File Offset: 0x000C8035
		// (set) Token: 0x06001E05 RID: 7685 RVA: 0x000C9E42 File Offset: 0x000C8042
		public string ConnectionString
		{
			get
			{
				return this.replacementProperties.ConnectionString;
			}
			set
			{
				this.replacementProperties.ConnectionString = value;
			}
		}

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x06001E06 RID: 7686 RVA: 0x000C9E50 File Offset: 0x000C8050
		// (set) Token: 0x06001E07 RID: 7687 RVA: 0x000C9E5D File Offset: 0x000C805D
		public ImpersonationMode ImpersonationMode
		{
			get
			{
				return this.replacementProperties.ImpersonationMode;
			}
			set
			{
				this.replacementProperties.ImpersonationMode = value;
			}
		}

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x06001E08 RID: 7688 RVA: 0x000C9E6B File Offset: 0x000C806B
		// (set) Token: 0x06001E09 RID: 7689 RVA: 0x000C9E78 File Offset: 0x000C8078
		public string Account
		{
			get
			{
				return this.replacementProperties.Account;
			}
			set
			{
				this.replacementProperties.Account = value;
			}
		}

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x06001E0A RID: 7690 RVA: 0x000C9E86 File Offset: 0x000C8086
		// (set) Token: 0x06001E0B RID: 7691 RVA: 0x000C9E93 File Offset: 0x000C8093
		public string Password
		{
			get
			{
				return this.replacementProperties.Password;
			}
			set
			{
				this.replacementProperties.Password = value;
			}
		}

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x06001E0C RID: 7692 RVA: 0x000C9EA1 File Offset: 0x000C80A1
		// (set) Token: 0x06001E0D RID: 7693 RVA: 0x000C9EAE File Offset: 0x000C80AE
		public DatasourceIsolation Isolation
		{
			get
			{
				return this.replacementProperties.Isolation;
			}
			set
			{
				this.replacementProperties.Isolation = value;
			}
		}

		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x06001E0E RID: 7694 RVA: 0x000C9EBC File Offset: 0x000C80BC
		// (set) Token: 0x06001E0F RID: 7695 RVA: 0x000C9EC9 File Offset: 0x000C80C9
		public int Timeout
		{
			get
			{
				return this.replacementProperties.Timeout;
			}
			set
			{
				this.replacementProperties.Timeout = value;
			}
		}

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x06001E10 RID: 7696 RVA: 0x000C9ED7 File Offset: 0x000C80D7
		// (set) Token: 0x06001E11 RID: 7697 RVA: 0x000C9EE4 File Offset: 0x000C80E4
		public string Provider
		{
			get
			{
				return this.replacementProperties.Provider;
			}
			set
			{
				this.replacementProperties.Provider = value;
			}
		}

		// Token: 0x06001E12 RID: 7698 RVA: 0x000C9EF2 File Offset: 0x000C80F2
		internal override MetadataObject GetOriginalObject()
		{
			return this.OriginalObject;
		}

		// Token: 0x06001E13 RID: 7699 RVA: 0x000C9EFA File Offset: 0x000C80FA
		internal override ObjectPath GetOriginalObjectPath()
		{
			return this.OriginalObjectPath;
		}

		// Token: 0x06001E14 RID: 7700 RVA: 0x000C9F02 File Offset: 0x000C8102
		internal override ReplacementPropertiesCollection GetReplacementProperties()
		{
			return this.replacementProperties;
		}

		// Token: 0x06001E15 RID: 7701 RVA: 0x000C9F0C File Offset: 0x000C810C
		internal override void EnsureAllReferencesResolved(Model model)
		{
			if (this.OriginalObject != null)
			{
				return;
			}
			if (this.OriginalObjectPath == null)
			{
				throw new TomException(TomSR.Exception_OverridesOriginalObjectPathIsNull(Utils.GetUserFriendlyNameOfObjectType(ObjectType.DataSource)));
			}
			this.OriginalObjectPath.Normalize();
			ProviderDataSource providerDataSource = ObjectTreeHelper.LocateObjectByPath(this.OriginalObjectPath, model) as ProviderDataSource;
			if (providerDataSource != null)
			{
				this.OriginalObject = providerDataSource;
				return;
			}
			throw new TomException(TomSR.Exception_OverridesOriginalObjectCannotBeFound(Utils.GetUserFriendlyNameOfObjectType(ObjectType.DataSource)));
		}

		// Token: 0x06001E16 RID: 7702 RVA: 0x000C9F74 File Offset: 0x000C8174
		internal override bool ReadPropertyFromJson(JsonTextReader jsonReader)
		{
			jsonReader.VerifyToken(4);
			string text = (string)jsonReader.Value;
			if (text != null)
			{
				switch (text.Length)
				{
				case 7:
				{
					char c = text[0];
					if (c != 'a')
					{
						if (c == 't')
						{
							if (text == "timeout")
							{
								jsonReader.Read();
								jsonReader.VerifyToken(7);
								this.Timeout = (int)jsonReader.Value;
								jsonReader.Read();
								return true;
							}
						}
					}
					else if (text == "account")
					{
						jsonReader.Read();
						jsonReader.VerifyToken(9);
						this.Account = (string)jsonReader.Value;
						jsonReader.Read();
						return true;
					}
					break;
				}
				case 8:
				{
					char c = text[1];
					if (c != 'a')
					{
						if (c == 'r')
						{
							if (text == "provider")
							{
								jsonReader.Read();
								jsonReader.VerifyToken(9);
								this.Provider = (string)jsonReader.Value;
								jsonReader.Read();
								return true;
							}
						}
					}
					else if (text == "password")
					{
						jsonReader.Read();
						jsonReader.VerifyToken(9);
						this.Password = (string)jsonReader.Value;
						jsonReader.Read();
						return true;
					}
					break;
				}
				case 9:
					if (text == "isolation")
					{
						jsonReader.Read();
						jsonReader.VerifyToken(9);
						this.Isolation = JsonPropertyHelper.ConvertStringToEnum<DatasourceIsolation>((string)jsonReader.Value, jsonReader);
						jsonReader.Read();
						return true;
					}
					break;
				case 14:
					if (text == "originalObject")
					{
						jsonReader.Read();
						jsonReader.VerifyToken(1);
						this.OriginalObjectPath = ObjectPath.Parse(jsonReader);
						jsonReader.VerifyToken(13);
						jsonReader.Read();
						return true;
					}
					break;
				case 16:
					if (text == "connectionString")
					{
						jsonReader.Read();
						jsonReader.VerifyToken(9);
						this.ConnectionString = (string)jsonReader.Value;
						jsonReader.Read();
						return true;
					}
					break;
				case 17:
					if (text == "impersonationMode")
					{
						jsonReader.Read();
						jsonReader.VerifyToken(9);
						this.ImpersonationMode = JsonPropertyHelper.ConvertStringToEnum<ImpersonationMode>((string)jsonReader.Value, jsonReader);
						jsonReader.Read();
						return true;
					}
					break;
				}
			}
			bool flag = false;
			this.ReadAdditionalPropertyFromJson(jsonReader, ref flag);
			return flag;
		}

		// Token: 0x06001E17 RID: 7703 RVA: 0x000CA214 File Offset: 0x000C8414
		private void ReadAdditionalPropertyFromJson(JsonTextReader jsonReader, ref bool wasRead)
		{
			if (JProperty.Load(jsonReader).Name == "type")
			{
				wasRead = true;
			}
		}

		// Token: 0x06001E18 RID: 7704 RVA: 0x000CA230 File Offset: 0x000C8430
		internal static void WriteSchema(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue("ProviderDataSourceOverride object of Tabular Object Model (TOM)");
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("originalObject");
			JsonSchemaWriter.WriteSchemaForObjectPath(writer, ObjectType.DataSource, true);
			writer.WritePropertyName("connectionString");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("impersonationMode");
			writer.WriteStartObject();
			writer.WritePropertyName("enum");
			writer.WriteStartArray();
			writer.WriteValue("default");
			writer.WriteValue("impersonateAccount");
			writer.WriteValue("impersonateAnonymous");
			writer.WriteValue("impersonateCurrentUser");
			writer.WriteValue("impersonateServiceAccount");
			writer.WriteValue("impersonateUnattendedAccount");
			writer.WriteEndArray();
			writer.WriteEndObject();
			writer.WritePropertyName("account");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("password");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("isolation");
			writer.WriteStartObject();
			writer.WritePropertyName("enum");
			writer.WriteStartArray();
			writer.WriteValue("readCommitted");
			writer.WriteValue("snapshot");
			writer.WriteEndArray();
			writer.WriteEndObject();
			writer.WritePropertyName("timeout");
			JsonSchemaWriter.WriteSchemaForInteger(writer);
			writer.WritePropertyName("provider");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WriteEndObject();
			writer.WriteEndObject();
		}

		// Token: 0x040006E5 RID: 1765
		private ProviderDataSourceOverride.Overrides replacementProperties;

		// Token: 0x02000445 RID: 1093
		internal sealed class Overrides : ReplacementPropertiesCollection
		{
			// Token: 0x170007F3 RID: 2035
			// (get) Token: 0x060028FB RID: 10491 RVA: 0x000F0A18 File Offset: 0x000EEC18
			// (set) Token: 0x060028FC RID: 10492 RVA: 0x000F0A25 File Offset: 0x000EEC25
			public string ConnectionString
			{
				get
				{
					return this.connectionString.Value;
				}
				set
				{
					this.connectionString.Value = value;
				}
			}

			// Token: 0x170007F4 RID: 2036
			// (get) Token: 0x060028FD RID: 10493 RVA: 0x000F0A33 File Offset: 0x000EEC33
			// (set) Token: 0x060028FE RID: 10494 RVA: 0x000F0A40 File Offset: 0x000EEC40
			public ImpersonationMode ImpersonationMode
			{
				get
				{
					return this.impersonationMode.Value;
				}
				set
				{
					this.impersonationMode.Value = value;
				}
			}

			// Token: 0x170007F5 RID: 2037
			// (get) Token: 0x060028FF RID: 10495 RVA: 0x000F0A4E File Offset: 0x000EEC4E
			// (set) Token: 0x06002900 RID: 10496 RVA: 0x000F0A5B File Offset: 0x000EEC5B
			public string Account
			{
				get
				{
					return this.account.Value;
				}
				set
				{
					this.account.Value = value;
				}
			}

			// Token: 0x170007F6 RID: 2038
			// (get) Token: 0x06002901 RID: 10497 RVA: 0x000F0A69 File Offset: 0x000EEC69
			// (set) Token: 0x06002902 RID: 10498 RVA: 0x000F0A76 File Offset: 0x000EEC76
			public string Password
			{
				get
				{
					return this.password.Value;
				}
				set
				{
					this.password.Value = value;
				}
			}

			// Token: 0x170007F7 RID: 2039
			// (get) Token: 0x06002903 RID: 10499 RVA: 0x000F0A84 File Offset: 0x000EEC84
			// (set) Token: 0x06002904 RID: 10500 RVA: 0x000F0A91 File Offset: 0x000EEC91
			public DatasourceIsolation Isolation
			{
				get
				{
					return this.isolation.Value;
				}
				set
				{
					this.isolation.Value = value;
				}
			}

			// Token: 0x170007F8 RID: 2040
			// (get) Token: 0x06002905 RID: 10501 RVA: 0x000F0A9F File Offset: 0x000EEC9F
			// (set) Token: 0x06002906 RID: 10502 RVA: 0x000F0AAC File Offset: 0x000EECAC
			public int Timeout
			{
				get
				{
					return this.timeout.Value;
				}
				set
				{
					this.timeout.Value = value;
				}
			}

			// Token: 0x170007F9 RID: 2041
			// (get) Token: 0x06002907 RID: 10503 RVA: 0x000F0ABA File Offset: 0x000EECBA
			// (set) Token: 0x06002908 RID: 10504 RVA: 0x000F0AC7 File Offset: 0x000EECC7
			public string Provider
			{
				get
				{
					return this.provider.Value;
				}
				set
				{
					this.provider.Value = value;
				}
			}

			// Token: 0x06002909 RID: 10505 RVA: 0x000F0AD5 File Offset: 0x000EECD5
			internal override bool IsLinkOverriden(string propertyName, out MetadataObject newValue)
			{
				throw TomInternalException.Create("Invalid property name - {0}", new object[] { propertyName });
			}

			// Token: 0x0600290A RID: 10506 RVA: 0x000F0AEC File Offset: 0x000EECEC
			internal override bool IsPropertyOverriden(string propertyName, out object newValue)
			{
				if (propertyName != null)
				{
					switch (propertyName.Length)
					{
					case 7:
					{
						char c = propertyName[0];
						if (c != 'A')
						{
							if (c != 'O')
							{
								if (c != 'T')
								{
									goto IL_0266;
								}
								if (!(propertyName == "Timeout"))
								{
									goto IL_0266;
								}
								newValue = this.timeout.Value;
								return this.timeout.IsSet;
							}
							else if (!(propertyName == "Options"))
							{
								goto IL_0266;
							}
						}
						else
						{
							if (!(propertyName == "Account"))
							{
								goto IL_0266;
							}
							newValue = this.account.Value;
							return this.account.IsSet;
						}
						break;
					}
					case 8:
					{
						char c = propertyName[1];
						if (c != 'a')
						{
							if (c != 'r')
							{
								goto IL_0266;
							}
							if (!(propertyName == "Provider"))
							{
								goto IL_0266;
							}
							newValue = this.provider.Value;
							return this.provider.IsSet;
						}
						else
						{
							if (!(propertyName == "Password"))
							{
								goto IL_0266;
							}
							newValue = this.password.Value;
							return this.password.IsSet;
						}
						break;
					}
					case 9:
						if (!(propertyName == "Isolation"))
						{
							goto IL_0266;
						}
						newValue = this.isolation.Value;
						return this.isolation.IsSet;
					case 10:
						if (!(propertyName == "Credential"))
						{
							goto IL_0266;
						}
						break;
					case 11:
					case 12:
					case 13:
					case 15:
						goto IL_0266;
					case 14:
						if (!(propertyName == "MaxConnections"))
						{
							goto IL_0266;
						}
						break;
					case 16:
						if (!(propertyName == "ConnectionString"))
						{
							goto IL_0266;
						}
						newValue = this.connectionString.Value;
						return this.connectionString.IsSet;
					case 17:
					{
						char c = propertyName[3];
						if (c != 'e')
						{
							if (c != 'n')
							{
								if (c != 't')
								{
									goto IL_0266;
								}
								if (!(propertyName == "ContextExpression"))
								{
									goto IL_0266;
								}
							}
							else if (!(propertyName == "ConnectionDetails"))
							{
								goto IL_0266;
							}
						}
						else
						{
							if (!(propertyName == "ImpersonationMode"))
							{
								goto IL_0266;
							}
							newValue = this.impersonationMode.Value;
							return this.impersonationMode.IsSet;
						}
						break;
					}
					default:
						goto IL_0266;
					}
					newValue = null;
					return false;
				}
				IL_0266:
				throw TomInternalException.Create("Invalid property name - {0}", new object[] { propertyName });
			}

			// Token: 0x0400143B RID: 5179
			private ReplacementPropertiesCollection.OverridenProperty<string> connectionString;

			// Token: 0x0400143C RID: 5180
			private ReplacementPropertiesCollection.OverridenProperty<ImpersonationMode> impersonationMode;

			// Token: 0x0400143D RID: 5181
			private ReplacementPropertiesCollection.OverridenProperty<string> account;

			// Token: 0x0400143E RID: 5182
			private ReplacementPropertiesCollection.OverridenProperty<string> password;

			// Token: 0x0400143F RID: 5183
			private ReplacementPropertiesCollection.OverridenProperty<DatasourceIsolation> isolation;

			// Token: 0x04001440 RID: 5184
			private ReplacementPropertiesCollection.OverridenProperty<int> timeout;

			// Token: 0x04001441 RID: 5185
			private ReplacementPropertiesCollection.OverridenProperty<string> provider;
		}
	}
}
