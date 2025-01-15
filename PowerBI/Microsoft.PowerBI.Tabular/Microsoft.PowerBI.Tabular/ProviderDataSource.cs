using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.Json;
using Microsoft.AnalysisServices.Tabular.Json.Linq;
using Microsoft.AnalysisServices.Tabular.Serialization;
using Microsoft.AnalysisServices.Tabular.Utilities;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000A9 RID: 169
	public class ProviderDataSource : DataSource
	{
		// Token: 0x06000A45 RID: 2629 RVA: 0x00055320 File Offset: 0x00053520
		public ProviderDataSource()
		{
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x00055328 File Offset: 0x00053528
		internal ProviderDataSource(IEqualityComparer<string> comparer)
			: base(comparer)
		{
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x00055331 File Offset: 0x00053531
		internal override MetadataObject CreateObjectOfSameType()
		{
			return new ProviderDataSource();
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000A48 RID: 2632 RVA: 0x00055338 File Offset: 0x00053538
		// (set) Token: 0x06000A49 RID: 2633 RVA: 0x00055340 File Offset: 0x00053540
		public new string ConnectionString
		{
			get
			{
				return base.ConnectionString;
			}
			set
			{
				base.ConnectionString = value;
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000A4A RID: 2634 RVA: 0x00055349 File Offset: 0x00053549
		// (set) Token: 0x06000A4B RID: 2635 RVA: 0x00055351 File Offset: 0x00053551
		public new ImpersonationMode ImpersonationMode
		{
			get
			{
				return base.ImpersonationMode;
			}
			set
			{
				base.ImpersonationMode = value;
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000A4C RID: 2636 RVA: 0x0005535A File Offset: 0x0005355A
		// (set) Token: 0x06000A4D RID: 2637 RVA: 0x00055362 File Offset: 0x00053562
		public new string Account
		{
			get
			{
				return base.Account;
			}
			set
			{
				base.Account = value;
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000A4E RID: 2638 RVA: 0x0005536B File Offset: 0x0005356B
		// (set) Token: 0x06000A4F RID: 2639 RVA: 0x00055373 File Offset: 0x00053573
		public new string Password
		{
			get
			{
				return base.Password;
			}
			set
			{
				base.Password = value;
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000A50 RID: 2640 RVA: 0x0005537C File Offset: 0x0005357C
		// (set) Token: 0x06000A51 RID: 2641 RVA: 0x00055384 File Offset: 0x00053584
		public new DatasourceIsolation Isolation
		{
			get
			{
				return base.Isolation;
			}
			set
			{
				base.Isolation = value;
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000A52 RID: 2642 RVA: 0x0005538D File Offset: 0x0005358D
		// (set) Token: 0x06000A53 RID: 2643 RVA: 0x00055395 File Offset: 0x00053595
		public new int Timeout
		{
			get
			{
				return base.Timeout;
			}
			set
			{
				base.Timeout = value;
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000A54 RID: 2644 RVA: 0x0005539E File Offset: 0x0005359E
		// (set) Token: 0x06000A55 RID: 2645 RVA: 0x000553A6 File Offset: 0x000535A6
		public new string Provider
		{
			get
			{
				return base.Provider;
			}
			set
			{
				base.Provider = value;
			}
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x000553B0 File Offset: 0x000535B0
		private protected override void WriteRegularPropertiesToMetadataStream(SerializationActivityContext context, IMetadataWriter writer)
		{
			base.WriteRegularPropertiesToMetadataStream(context, writer);
			if (!string.IsNullOrEmpty(this.body.ConnectionString) && writer.ShouldIncludeProperty("connectionString", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Restricted))
			{
				writer.WriteStringProperty("connectionString", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Restricted, this.body.ConnectionString);
			}
			if (this.body.ImpersonationMode != ImpersonationMode.Default && writer.ShouldIncludeProperty("impersonationMode", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteEnumProperty<ImpersonationMode>("impersonationMode", MetadataPropertyNature.RegularProperty, this.body.ImpersonationMode);
			}
			if (!string.IsNullOrEmpty(this.body.Account) && writer.ShouldIncludeProperty("account", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteStringProperty("account", MetadataPropertyNature.RegularProperty, this.body.Account);
			}
			if (!string.IsNullOrEmpty(this.body.Password) && writer.ShouldIncludeProperty("password", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Restricted))
			{
				writer.WriteStringProperty("password", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Restricted, this.body.Password);
			}
			if (this.body.Isolation != DatasourceIsolation.ReadCommitted && writer.ShouldIncludeProperty("isolation", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteEnumProperty<DatasourceIsolation>("isolation", MetadataPropertyNature.RegularProperty, this.body.Isolation);
			}
			if (this.body.Timeout != 0 && writer.ShouldIncludeProperty("timeout", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteInt32Property("timeout", MetadataPropertyNature.RegularProperty, this.body.Timeout);
			}
			if (!string.IsNullOrEmpty(this.body.Provider) && writer.ShouldIncludeProperty("provider", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteStringProperty("provider", MetadataPropertyNature.RegularProperty, this.body.Provider);
			}
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x0005554C File Offset: 0x0005374C
		private protected override bool TryReadNextMetadataProperty(SerializationActivityContext context, IMetadataReader reader, out UnexpectedPropertyClassification classification)
		{
			if (base.TryReadNextMetadataProperty(context, reader, out classification))
			{
				return true;
			}
			if (classification != UnexpectedPropertyClassification.Unclassified)
			{
				return false;
			}
			string propertyName = reader.PropertyName;
			if (propertyName != null)
			{
				int length = propertyName.Length;
				switch (length)
				{
				case 7:
				{
					char c = propertyName[0];
					if (c != 'a')
					{
						if (c == 't')
						{
							if (propertyName == "timeout")
							{
								this.body.Timeout = reader.ReadInt32Property();
								return true;
							}
						}
					}
					else if (propertyName == "account")
					{
						this.body.Account = reader.ReadStringProperty();
						return true;
					}
					break;
				}
				case 8:
				{
					char c = propertyName[1];
					if (c != 'a')
					{
						if (c == 'r')
						{
							if (propertyName == "provider")
							{
								this.body.Provider = reader.ReadStringProperty();
								return true;
							}
						}
					}
					else if (propertyName == "password")
					{
						this.body.Password = reader.ReadStringProperty();
						return true;
					}
					break;
				}
				case 9:
					if (propertyName == "isolation")
					{
						this.body.Isolation = reader.ReadEnumProperty<DatasourceIsolation>();
						return true;
					}
					break;
				default:
					if (length != 16)
					{
						if (length == 17)
						{
							if (propertyName == "impersonationMode")
							{
								this.body.ImpersonationMode = reader.ReadEnumProperty<ImpersonationMode>();
								return true;
							}
						}
					}
					else if (propertyName == "connectionString")
					{
						this.body.ConnectionString = reader.ReadStringProperty();
						return true;
					}
					break;
				}
			}
			classification = UnexpectedPropertyClassification.UnknownProperty;
			return false;
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x000556E0 File Offset: 0x000538E0
		internal override void SerializeToJsonObject(JsonObject result, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			base.SerializeToJsonObject(result, options, mode, dbCompatibilityLevel);
			if (options.IncludeTranslatablePropertiesOnly)
			{
				return;
			}
			if (!string.IsNullOrEmpty(this.body.ConnectionString))
			{
				result["connectionString", TomPropCategory.Regular, 5, false] = JsonPropertyHelper.ConvertStringToJsonValue(options.IncludeRestrictedInformation ? this.body.ConnectionString : PropertyHelper.GetCuratedValueForConnectionString(this.body.ConnectionString), SplitMultilineOptions.None);
			}
			if (this.body.ImpersonationMode != ImpersonationMode.Default)
			{
				result["impersonationMode", TomPropCategory.Regular, 6, false] = JsonPropertyHelper.ConvertEnumToJsonValue<ImpersonationMode>(this.ImpersonationMode);
			}
			if (!string.IsNullOrEmpty(this.body.Account))
			{
				result["account", TomPropCategory.Regular, 7, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.Account, SplitMultilineOptions.None);
			}
			if (!string.IsNullOrEmpty(this.body.Password))
			{
				result["password", TomPropCategory.Regular, 8, false] = JsonPropertyHelper.ConvertStringToJsonValue(options.IncludeRestrictedInformation ? this.body.Password : PropertyHelper.GetCuratedValueForPassword(this.body.Password), SplitMultilineOptions.None);
			}
			if (this.body.Isolation != DatasourceIsolation.ReadCommitted)
			{
				result["isolation", TomPropCategory.Regular, 10, false] = JsonPropertyHelper.ConvertEnumToJsonValue<DatasourceIsolation>(this.Isolation);
			}
			if (this.body.Timeout != 0)
			{
				result["timeout", TomPropCategory.Regular, 11, false] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<int>(this.Timeout);
			}
			if (!string.IsNullOrEmpty(this.body.Provider))
			{
				result["provider", TomPropCategory.Regular, 12, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.Provider, SplitMultilineOptions.None);
			}
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x00055870 File Offset: 0x00053A70
		internal override bool ReadPropertyFromJson(JProperty jsonProp, DeserializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (base.ReadPropertyFromJson(jsonProp, options, mode, dbCompatibilityLevel))
			{
				return true;
			}
			string name = jsonProp.Name;
			if (name != null)
			{
				int length = name.Length;
				switch (length)
				{
				case 7:
				{
					char c = name[0];
					if (c != 'a')
					{
						if (c == 't')
						{
							if (name == "timeout")
							{
								this.Timeout = JsonPropertyHelper.ConvertJsonValueToPrimitive<int>(jsonProp.Value);
								return true;
							}
						}
					}
					else if (name == "account")
					{
						this.Account = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
						return true;
					}
					break;
				}
				case 8:
				{
					char c = name[1];
					if (c != 'a')
					{
						if (c == 'r')
						{
							if (name == "provider")
							{
								this.Provider = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
								return true;
							}
						}
					}
					else if (name == "password")
					{
						this.Password = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
						return true;
					}
					break;
				}
				case 9:
					if (name == "isolation")
					{
						this.Isolation = JsonPropertyHelper.ConvertJsonValueToEnum<DatasourceIsolation>(jsonProp.Value);
						return true;
					}
					break;
				default:
					if (length != 16)
					{
						if (length == 17)
						{
							if (name == "impersonationMode")
							{
								this.ImpersonationMode = JsonPropertyHelper.ConvertJsonValueToEnum<ImpersonationMode>(jsonProp.Value);
								return true;
							}
						}
					}
					else if (name == "connectionString")
					{
						this.ConnectionString = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
						return true;
					}
					break;
				}
			}
			return false;
		}
	}
}
