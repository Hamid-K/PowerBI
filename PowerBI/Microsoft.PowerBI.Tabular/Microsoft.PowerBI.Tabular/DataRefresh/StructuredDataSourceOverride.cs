using System;
using Microsoft.AnalysisServices.Tabular.Extensions;
using Microsoft.AnalysisServices.Tabular.Json;
using Microsoft.AnalysisServices.Tabular.Json.Linq;
using Microsoft.AnalysisServices.Tabular.Metadata;
using Microsoft.AnalysisServices.Tabular.Utilities;

namespace Microsoft.AnalysisServices.Tabular.DataRefresh
{
	// Token: 0x02000215 RID: 533
	[CompatibilityRequirement("1400")]
	public class StructuredDataSourceOverride : DataSourceOverride
	{
		// Token: 0x06001E19 RID: 7705 RVA: 0x000CA3A8 File Offset: 0x000C85A8
		public StructuredDataSourceOverride()
		{
			this.replacementProperties = new StructuredDataSourceOverride.Overrides();
		}

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x06001E1A RID: 7706 RVA: 0x000CA3BB File Offset: 0x000C85BB
		// (set) Token: 0x06001E1B RID: 7707 RVA: 0x000CA3C3 File Offset: 0x000C85C3
		public StructuredDataSource OriginalObject { get; set; }

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x06001E1C RID: 7708 RVA: 0x000CA3CC File Offset: 0x000C85CC
		// (set) Token: 0x06001E1D RID: 7709 RVA: 0x000CA3D4 File Offset: 0x000C85D4
		internal ObjectPath OriginalObjectPath { get; set; }

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x06001E1E RID: 7710 RVA: 0x000CA3DD File Offset: 0x000C85DD
		// (set) Token: 0x06001E1F RID: 7711 RVA: 0x000CA3EA File Offset: 0x000C85EA
		public ConnectionDetails ConnectionDetails
		{
			get
			{
				return this.replacementProperties.ConnectionDetails;
			}
			set
			{
				this.replacementProperties.ConnectionDetails = value;
			}
		}

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x06001E20 RID: 7712 RVA: 0x000CA3F8 File Offset: 0x000C85F8
		// (set) Token: 0x06001E21 RID: 7713 RVA: 0x000CA405 File Offset: 0x000C8605
		public DataSourceOptions Options
		{
			get
			{
				return this.replacementProperties.Options;
			}
			set
			{
				this.replacementProperties.Options = value;
			}
		}

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x06001E22 RID: 7714 RVA: 0x000CA413 File Offset: 0x000C8613
		// (set) Token: 0x06001E23 RID: 7715 RVA: 0x000CA420 File Offset: 0x000C8620
		public Credential Credential
		{
			get
			{
				return this.replacementProperties.Credential;
			}
			set
			{
				this.replacementProperties.Credential = value;
			}
		}

		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x06001E24 RID: 7716 RVA: 0x000CA42E File Offset: 0x000C862E
		// (set) Token: 0x06001E25 RID: 7717 RVA: 0x000CA43B File Offset: 0x000C863B
		public string ContextExpression
		{
			get
			{
				return this.replacementProperties.ContextExpression;
			}
			set
			{
				this.replacementProperties.ContextExpression = value;
			}
		}

		// Token: 0x06001E26 RID: 7718 RVA: 0x000CA449 File Offset: 0x000C8649
		internal override MetadataObject GetOriginalObject()
		{
			return this.OriginalObject;
		}

		// Token: 0x06001E27 RID: 7719 RVA: 0x000CA451 File Offset: 0x000C8651
		internal override ObjectPath GetOriginalObjectPath()
		{
			return this.OriginalObjectPath;
		}

		// Token: 0x06001E28 RID: 7720 RVA: 0x000CA459 File Offset: 0x000C8659
		internal override ReplacementPropertiesCollection GetReplacementProperties()
		{
			return this.replacementProperties;
		}

		// Token: 0x06001E29 RID: 7721 RVA: 0x000CA464 File Offset: 0x000C8664
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
			StructuredDataSource structuredDataSource = ObjectTreeHelper.LocateObjectByPath(this.OriginalObjectPath, model) as StructuredDataSource;
			if (structuredDataSource != null)
			{
				this.OriginalObject = structuredDataSource;
				return;
			}
			throw new TomException(TomSR.Exception_OverridesOriginalObjectCannotBeFound(Utils.GetUserFriendlyNameOfObjectType(ObjectType.DataSource)));
		}

		// Token: 0x06001E2A RID: 7722 RVA: 0x000CA4CC File Offset: 0x000C86CC
		internal override bool ReadPropertyFromJson(JsonTextReader jsonReader)
		{
			jsonReader.VerifyToken(4);
			string text = (string)jsonReader.Value;
			if (text == "originalObject")
			{
				jsonReader.Read();
				jsonReader.VerifyToken(1);
				this.OriginalObjectPath = ObjectPath.Parse(jsonReader);
				jsonReader.VerifyToken(13);
				jsonReader.Read();
				return true;
			}
			if (text == "connectionDetails")
			{
				jsonReader.Read();
				jsonReader.VerifyToken(1);
				this.ConnectionDetails = new ConnectionDetails(JToken.Load(jsonReader));
				jsonReader.Read();
				return true;
			}
			if (text == "options")
			{
				jsonReader.Read();
				jsonReader.VerifyToken(1);
				this.Options = new DataSourceOptions(JToken.Load(jsonReader));
				jsonReader.Read();
				return true;
			}
			if (text == "credential")
			{
				jsonReader.Read();
				jsonReader.VerifyToken(1);
				this.Credential = new Credential(JToken.Load(jsonReader));
				jsonReader.Read();
				return true;
			}
			if (!(text == "contextExpression"))
			{
				bool flag = false;
				this.ReadAdditionalPropertyFromJson(jsonReader, ref flag);
				return flag;
			}
			jsonReader.Read();
			jsonReader.VerifyToken(9);
			this.ContextExpression = (string)jsonReader.Value;
			jsonReader.Read();
			return true;
		}

		// Token: 0x06001E2B RID: 7723 RVA: 0x000CA610 File Offset: 0x000C8810
		private void ReadAdditionalPropertyFromJson(JsonTextReader jsonReader, ref bool wasRead)
		{
			if (JProperty.Load(jsonReader).Name == "type")
			{
				wasRead = true;
			}
		}

		// Token: 0x06001E2C RID: 7724 RVA: 0x000CA62C File Offset: 0x000C882C
		internal static void WriteSchema(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue("StructuredDataSourceOverride object of Tabular Object Model (TOM)");
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("originalObject");
			JsonSchemaWriter.WriteSchemaForObjectPath(writer, ObjectType.DataSource, true);
			if (CompatibilityRestrictions.StructuredDataSource_ConnectionDetails.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("connectionDetails");
				JsonSchemaWriter.WriteSchemaForGenericObject(writer);
			}
			if (CompatibilityRestrictions.StructuredDataSource_Options.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("options");
				JsonSchemaWriter.WriteSchemaForGenericObject(writer);
			}
			if (CompatibilityRestrictions.StructuredDataSource_Credential.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("credential");
				JsonSchemaWriter.WriteSchemaForGenericObject(writer);
			}
			if (CompatibilityRestrictions.StructuredDataSource_ContextExpression.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("contextExpression");
				if (options.SplitMultilineStrings)
				{
					JsonSchemaWriter.WriteSchemaForMultilineString(writer);
				}
				else
				{
					JsonSchemaWriter.WriteSchemaForString(writer);
				}
			}
			writer.WriteEndObject();
			writer.WriteEndObject();
		}

		// Token: 0x040006E8 RID: 1768
		private StructuredDataSourceOverride.Overrides replacementProperties;

		// Token: 0x02000446 RID: 1094
		internal sealed class Overrides : ReplacementPropertiesCollection
		{
			// Token: 0x170007FA RID: 2042
			// (get) Token: 0x0600290C RID: 10508 RVA: 0x000F0D7B File Offset: 0x000EEF7B
			// (set) Token: 0x0600290D RID: 10509 RVA: 0x000F0D88 File Offset: 0x000EEF88
			public ConnectionDetails ConnectionDetails
			{
				get
				{
					return this.connectionDetails.Value;
				}
				set
				{
					this.connectionDetails.Value = value;
				}
			}

			// Token: 0x170007FB RID: 2043
			// (get) Token: 0x0600290E RID: 10510 RVA: 0x000F0D96 File Offset: 0x000EEF96
			// (set) Token: 0x0600290F RID: 10511 RVA: 0x000F0DA3 File Offset: 0x000EEFA3
			public DataSourceOptions Options
			{
				get
				{
					return this.options.Value;
				}
				set
				{
					this.options.Value = value;
				}
			}

			// Token: 0x170007FC RID: 2044
			// (get) Token: 0x06002910 RID: 10512 RVA: 0x000F0DB1 File Offset: 0x000EEFB1
			// (set) Token: 0x06002911 RID: 10513 RVA: 0x000F0DBE File Offset: 0x000EEFBE
			public Credential Credential
			{
				get
				{
					return this.credential.Value;
				}
				set
				{
					this.credential.Value = value;
				}
			}

			// Token: 0x170007FD RID: 2045
			// (get) Token: 0x06002912 RID: 10514 RVA: 0x000F0DCC File Offset: 0x000EEFCC
			// (set) Token: 0x06002913 RID: 10515 RVA: 0x000F0DD9 File Offset: 0x000EEFD9
			public string ContextExpression
			{
				get
				{
					return this.contextExpression.Value;
				}
				set
				{
					this.contextExpression.Value = value;
				}
			}

			// Token: 0x06002914 RID: 10516 RVA: 0x000F0DE7 File Offset: 0x000EEFE7
			internal override bool IsLinkOverriden(string propertyName, out MetadataObject newValue)
			{
				throw TomInternalException.Create("Invalid property name - {0}", new object[] { propertyName });
			}

			// Token: 0x06002915 RID: 10517 RVA: 0x000F0E00 File Offset: 0x000EF000
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
									goto IL_0251;
								}
								if (!(propertyName == "Timeout"))
								{
									goto IL_0251;
								}
							}
							else
							{
								if (!(propertyName == "Options"))
								{
									goto IL_0251;
								}
								newValue = (this.options.IsSet ? ((ICustomProperty<StructuredDataSource, string>)this.options.Value).Convert() : string.Empty);
								return this.options.IsSet;
							}
						}
						else if (!(propertyName == "Account"))
						{
							goto IL_0251;
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
								goto IL_0251;
							}
							if (!(propertyName == "Provider"))
							{
								goto IL_0251;
							}
						}
						else if (!(propertyName == "Password"))
						{
							goto IL_0251;
						}
						break;
					}
					case 9:
						if (!(propertyName == "Isolation"))
						{
							goto IL_0251;
						}
						break;
					case 10:
						if (!(propertyName == "Credential"))
						{
							goto IL_0251;
						}
						newValue = (this.credential.IsSet ? ((ICustomProperty<StructuredDataSource, string>)this.credential.Value).Convert() : string.Empty);
						return this.credential.IsSet;
					case 11:
					case 12:
					case 13:
					case 15:
						goto IL_0251;
					case 14:
						if (!(propertyName == "MaxConnections"))
						{
							goto IL_0251;
						}
						break;
					case 16:
						if (!(propertyName == "ConnectionString"))
						{
							goto IL_0251;
						}
						break;
					case 17:
					{
						char c = propertyName[3];
						if (c != 'e')
						{
							if (c != 'n')
							{
								if (c != 't')
								{
									goto IL_0251;
								}
								if (!(propertyName == "ContextExpression"))
								{
									goto IL_0251;
								}
								newValue = this.contextExpression.Value;
								return this.contextExpression.IsSet;
							}
							else
							{
								if (!(propertyName == "ConnectionDetails"))
								{
									goto IL_0251;
								}
								newValue = (this.connectionDetails.IsSet ? ((ICustomProperty<StructuredDataSource, string>)this.connectionDetails.Value).Convert() : string.Empty);
								return this.connectionDetails.IsSet;
							}
						}
						else if (!(propertyName == "ImpersonationMode"))
						{
							goto IL_0251;
						}
						break;
					}
					default:
						goto IL_0251;
					}
					newValue = null;
					return false;
				}
				IL_0251:
				throw TomInternalException.Create("Invalid property name - {0}", new object[] { propertyName });
			}

			// Token: 0x04001442 RID: 5186
			private ReplacementPropertiesCollection.OverridenProperty<ConnectionDetails> connectionDetails;

			// Token: 0x04001443 RID: 5187
			private ReplacementPropertiesCollection.OverridenProperty<DataSourceOptions> options;

			// Token: 0x04001444 RID: 5188
			private ReplacementPropertiesCollection.OverridenProperty<Credential> credential;

			// Token: 0x04001445 RID: 5189
			private ReplacementPropertiesCollection.OverridenProperty<string> contextExpression;
		}
	}
}
