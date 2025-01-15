using System;
using Microsoft.AnalysisServices.Tabular.Extensions;
using Microsoft.AnalysisServices.Tabular.Json;
using Microsoft.AnalysisServices.Tabular.Metadata;
using Microsoft.AnalysisServices.Tabular.Utilities;

namespace Microsoft.AnalysisServices.Tabular.DataRefresh
{
	// Token: 0x02000213 RID: 531
	public class PartitionOverride : IObjectOverride
	{
		// Token: 0x06001DEB RID: 7659 RVA: 0x000C9B17 File Offset: 0x000C7D17
		public PartitionOverride()
		{
			this.replacementProperties = new PartitionOverride.Overrides();
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x06001DEC RID: 7660 RVA: 0x000C9B2A File Offset: 0x000C7D2A
		// (set) Token: 0x06001DED RID: 7661 RVA: 0x000C9B32 File Offset: 0x000C7D32
		public Partition OriginalObject { get; set; }

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x06001DEE RID: 7662 RVA: 0x000C9B3B File Offset: 0x000C7D3B
		// (set) Token: 0x06001DEF RID: 7663 RVA: 0x000C9B43 File Offset: 0x000C7D43
		internal ObjectPath OriginalObjectPath { get; set; }

		// Token: 0x06001DF0 RID: 7664 RVA: 0x000C9B4C File Offset: 0x000C7D4C
		private void EnsureAllReferencesResolved(Model model)
		{
			if (this.OriginalObject == null)
			{
				if (this.OriginalObjectPath == null)
				{
					throw new TomException(TomSR.Exception_OverridesOriginalObjectPathIsNull(Utils.GetUserFriendlyNameOfObjectType(ObjectType.Partition)));
				}
				this.OriginalObjectPath.Normalize();
				Partition partition = ObjectTreeHelper.LocateObjectByPath(this.OriginalObjectPath, model) as Partition;
				if (partition == null)
				{
					throw new TomException(TomSR.Exception_OverridesOriginalObjectCannotBeFound(Utils.GetUserFriendlyNameOfObjectType(ObjectType.Partition)));
				}
				this.OriginalObject = partition;
			}
			QueryPartitionSourceOverride queryPartitionSourceOverride = this.Source as QueryPartitionSourceOverride;
			if (queryPartitionSourceOverride != null && queryPartitionSourceOverride.DataSource == null && queryPartitionSourceOverride.DataSourcePath != null)
			{
				MetadataObject metadataObject = ObjectTreeHelper.LocateObjectByPath(queryPartitionSourceOverride.DataSourcePath, model);
				if (metadataObject != null)
				{
					DataSource dataSource = metadataObject as DataSource;
					if (dataSource != null)
					{
						queryPartitionSourceOverride.DataSource = dataSource;
						return;
					}
				}
				throw new TomException(TomSR.Exception_OverridesDatasourceCannotBeFound);
			}
		}

		// Token: 0x06001DF1 RID: 7665 RVA: 0x000C9C00 File Offset: 0x000C7E00
		internal bool ReadPropertyFromJson(JsonTextReader jsonReader)
		{
			jsonReader.VerifyToken(4);
			if ((string)jsonReader.Value == "originalObject")
			{
				jsonReader.Read();
				jsonReader.VerifyToken(1);
				this.OriginalObjectPath = ObjectPath.Parse(jsonReader);
				jsonReader.VerifyToken(13);
				jsonReader.Read();
				return true;
			}
			bool flag = false;
			this.ReadAdditionalPropertyFromJson(jsonReader, ref flag);
			return flag;
		}

		// Token: 0x06001DF2 RID: 7666 RVA: 0x000C9C64 File Offset: 0x000C7E64
		private void ReadAdditionalPropertyFromJson(JsonTextReader jsonReader, ref bool wasRead)
		{
			jsonReader.VerifyToken(4);
			if ((string)jsonReader.Value == "source")
			{
				jsonReader.Read();
				jsonReader.VerifyToken(1);
				PartitionSourceOverride partitionSourceOverride = ObjectFactory.CreatePartitionSourceOverrideFromJsonReader(jsonReader);
				if (partitionSourceOverride != null)
				{
					this.Source = partitionSourceOverride;
				}
				jsonReader.VerifyToken(13);
				jsonReader.Read();
				wasRead = true;
				return;
			}
			wasRead = false;
		}

		// Token: 0x06001DF3 RID: 7667 RVA: 0x000C9CC4 File Offset: 0x000C7EC4
		internal static void WriteSchema(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue("PartitionOverride object of Tabular Object Model (TOM)");
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("originalObject");
			JsonSchemaWriter.WriteSchemaForObjectPath(writer, ObjectType.Partition, true);
			PartitionOverride.WriteAdditionalSchemaProperties(writer, options, mode, dbCompatibilityLevel);
			writer.WriteEndObject();
			writer.WriteEndObject();
		}

		// Token: 0x06001DF4 RID: 7668 RVA: 0x000C9D3C File Offset: 0x000C7F3C
		private static void WriteAdditionalSchemaProperties(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WritePropertyName("source");
			writer.WriteStartObject();
			if (dbCompatibilityLevel < 1400)
			{
				QueryPartitionSourceOverride.WriteSchema(writer, options, mode, dbCompatibilityLevel);
			}
			else
			{
				writer.WritePropertyName("anyOf");
				writer.WriteStartArray();
				QueryPartitionSourceOverride.WriteSchema(writer, options, mode, dbCompatibilityLevel);
				MPartitionSourceOverride.WriteSchema(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndArray();
			}
			writer.WriteEndObject();
		}

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x06001DF5 RID: 7669 RVA: 0x000C9D9C File Offset: 0x000C7F9C
		ObjectType IObjectOverride.ObjectType
		{
			get
			{
				return ObjectType.Partition;
			}
		}

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x06001DF6 RID: 7670 RVA: 0x000C9D9F File Offset: 0x000C7F9F
		MetadataObject IObjectOverride.OriginalObject
		{
			get
			{
				return this.OriginalObject;
			}
		}

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x06001DF7 RID: 7671 RVA: 0x000C9DA7 File Offset: 0x000C7FA7
		ObjectPath IObjectOverride.OriginalObjectPath
		{
			get
			{
				return this.OriginalObjectPath;
			}
		}

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x06001DF8 RID: 7672 RVA: 0x000C9DAF File Offset: 0x000C7FAF
		ReplacementPropertiesCollection IObjectOverride.ReplacementProperties
		{
			get
			{
				return this.replacementProperties;
			}
		}

		// Token: 0x06001DF9 RID: 7673 RVA: 0x000C9DB7 File Offset: 0x000C7FB7
		void IObjectOverride.EnsureAllReferencesResolved(Model model)
		{
			this.EnsureAllReferencesResolved(model);
		}

		// Token: 0x06001DFA RID: 7674 RVA: 0x000C9DC0 File Offset: 0x000C7FC0
		bool IObjectOverride.ReadPropertyFromJson(JsonTextReader jsonReader)
		{
			return this.ReadPropertyFromJson(jsonReader);
		}

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x06001DFB RID: 7675 RVA: 0x000C9DC9 File Offset: 0x000C7FC9
		// (set) Token: 0x06001DFC RID: 7676 RVA: 0x000C9DD1 File Offset: 0x000C7FD1
		public PartitionSourceOverride Source
		{
			get
			{
				return this.source;
			}
			set
			{
				this.source = value;
				if (value == null)
				{
					this.ResetReplacementProperties();
					return;
				}
				value.Owner = this;
			}
		}

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x06001DFD RID: 7677 RVA: 0x000C9DEB File Offset: 0x000C7FEB
		internal PartitionOverride.Overrides ReplacementProperties
		{
			get
			{
				return this.replacementProperties;
			}
		}

		// Token: 0x06001DFE RID: 7678 RVA: 0x000C9DF3 File Offset: 0x000C7FF3
		internal void ResetReplacementProperties()
		{
			this.replacementProperties = new PartitionOverride.Overrides();
		}

		// Token: 0x040006E1 RID: 1761
		private PartitionOverride.Overrides replacementProperties;

		// Token: 0x040006E4 RID: 1764
		private PartitionSourceOverride source;

		// Token: 0x02000443 RID: 1091
		internal static class OverrideName
		{
			// Token: 0x04001433 RID: 5171
			public const string DataSource = "DataSourceID";

			// Token: 0x04001434 RID: 5172
			public const string ExpressionSource = "ExpressionSourceID";

			// Token: 0x04001435 RID: 5173
			public const string QueryDefinition = "QueryDefinition";

			// Token: 0x04001436 RID: 5174
			public const string Type = "Type";
		}

		// Token: 0x02000444 RID: 1092
		internal sealed class Overrides : ReplacementPropertiesCollection
		{
			// Token: 0x170007EF RID: 2031
			// (get) Token: 0x060028F0 RID: 10480 RVA: 0x000F08BE File Offset: 0x000EEABE
			// (set) Token: 0x060028F1 RID: 10481 RVA: 0x000F08CB File Offset: 0x000EEACB
			public DataSource DataSourceID
			{
				get
				{
					return this.dataSourceID.Value;
				}
				set
				{
					this.dataSourceID.Value = value;
				}
			}

			// Token: 0x170007F0 RID: 2032
			// (get) Token: 0x060028F2 RID: 10482 RVA: 0x000F08D9 File Offset: 0x000EEAD9
			// (set) Token: 0x060028F3 RID: 10483 RVA: 0x000F08E6 File Offset: 0x000EEAE6
			public NamedExpression ExpressionSourceID
			{
				get
				{
					return this.expressionSourceID.Value;
				}
				set
				{
					this.expressionSourceID.Value = value;
				}
			}

			// Token: 0x170007F1 RID: 2033
			// (get) Token: 0x060028F4 RID: 10484 RVA: 0x000F08F4 File Offset: 0x000EEAF4
			// (set) Token: 0x060028F5 RID: 10485 RVA: 0x000F0901 File Offset: 0x000EEB01
			public string QueryDefinition
			{
				get
				{
					return this.queryDefinition.Value;
				}
				set
				{
					this.queryDefinition.Value = value;
				}
			}

			// Token: 0x170007F2 RID: 2034
			// (get) Token: 0x060028F6 RID: 10486 RVA: 0x000F090F File Offset: 0x000EEB0F
			// (set) Token: 0x060028F7 RID: 10487 RVA: 0x000F091C File Offset: 0x000EEB1C
			public PartitionSourceType Type
			{
				get
				{
					return this.type.Value;
				}
				set
				{
					this.type.Value = value;
				}
			}

			// Token: 0x060028F8 RID: 10488 RVA: 0x000F092C File Offset: 0x000EEB2C
			internal override bool IsLinkOverriden(string propertyName, out MetadataObject newValue)
			{
				if (propertyName == "DataSourceID")
				{
					newValue = this.dataSourceID.Value;
					return this.dataSourceID.IsSet;
				}
				if (!(propertyName == "ExpressionSourceID"))
				{
					throw TomInternalException.Create("Invalid property name - {0}", new object[] { propertyName });
				}
				newValue = this.expressionSourceID.Value;
				return this.expressionSourceID.IsSet;
			}

			// Token: 0x060028F9 RID: 10489 RVA: 0x000F099C File Offset: 0x000EEB9C
			internal override bool IsPropertyOverriden(string propertyName, out object newValue)
			{
				if (propertyName == "QueryDefinition")
				{
					newValue = this.queryDefinition.Value;
					return this.queryDefinition.IsSet;
				}
				if (!(propertyName == "Type"))
				{
					throw TomInternalException.Create("Invalid property name - {0}", new object[] { propertyName });
				}
				newValue = this.type.Value;
				return this.type.IsSet;
			}

			// Token: 0x04001437 RID: 5175
			private ReplacementPropertiesCollection.OverridenProperty<DataSource> dataSourceID;

			// Token: 0x04001438 RID: 5176
			private ReplacementPropertiesCollection.OverridenProperty<NamedExpression> expressionSourceID;

			// Token: 0x04001439 RID: 5177
			private ReplacementPropertiesCollection.OverridenProperty<string> queryDefinition;

			// Token: 0x0400143A RID: 5178
			private ReplacementPropertiesCollection.OverridenProperty<PartitionSourceType> type;
		}
	}
}
