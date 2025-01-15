using System;
using Microsoft.AnalysisServices.Tabular.Extensions;
using Microsoft.AnalysisServices.Tabular.Json;
using Microsoft.AnalysisServices.Tabular.Metadata;
using Microsoft.AnalysisServices.Tabular.Utilities;

namespace Microsoft.AnalysisServices.Tabular.DataRefresh
{
	// Token: 0x02000210 RID: 528
	public class DataColumnOverride : ColumnOverride
	{
		// Token: 0x06001DC2 RID: 7618 RVA: 0x000C96EB File Offset: 0x000C78EB
		public DataColumnOverride()
		{
			this.replacementProperties = new DataColumnOverride.Overrides();
		}

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x06001DC3 RID: 7619 RVA: 0x000C96FE File Offset: 0x000C78FE
		// (set) Token: 0x06001DC4 RID: 7620 RVA: 0x000C9706 File Offset: 0x000C7906
		public DataColumn OriginalObject { get; set; }

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x06001DC5 RID: 7621 RVA: 0x000C970F File Offset: 0x000C790F
		// (set) Token: 0x06001DC6 RID: 7622 RVA: 0x000C9717 File Offset: 0x000C7917
		internal ObjectPath OriginalObjectPath { get; set; }

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x06001DC7 RID: 7623 RVA: 0x000C9720 File Offset: 0x000C7920
		// (set) Token: 0x06001DC8 RID: 7624 RVA: 0x000C972D File Offset: 0x000C792D
		public string SourceColumn
		{
			get
			{
				return this.replacementProperties.SourceColumn;
			}
			set
			{
				this.replacementProperties.SourceColumn = value;
			}
		}

		// Token: 0x06001DC9 RID: 7625 RVA: 0x000C973B File Offset: 0x000C793B
		internal override MetadataObject GetOriginalObject()
		{
			return this.OriginalObject;
		}

		// Token: 0x06001DCA RID: 7626 RVA: 0x000C9743 File Offset: 0x000C7943
		internal override ObjectPath GetOriginalObjectPath()
		{
			return this.OriginalObjectPath;
		}

		// Token: 0x06001DCB RID: 7627 RVA: 0x000C974B File Offset: 0x000C794B
		internal override ReplacementPropertiesCollection GetReplacementProperties()
		{
			return this.replacementProperties;
		}

		// Token: 0x06001DCC RID: 7628 RVA: 0x000C9754 File Offset: 0x000C7954
		internal override void EnsureAllReferencesResolved(Model model)
		{
			if (this.OriginalObject != null)
			{
				return;
			}
			if (this.OriginalObjectPath == null)
			{
				throw new TomException(TomSR.Exception_OverridesOriginalObjectPathIsNull(Utils.GetUserFriendlyNameOfObjectType(ObjectType.Column)));
			}
			this.OriginalObjectPath.Normalize();
			DataColumn dataColumn = ObjectTreeHelper.LocateObjectByPath(this.OriginalObjectPath, model) as DataColumn;
			if (dataColumn != null)
			{
				this.OriginalObject = dataColumn;
				return;
			}
			throw new TomException(TomSR.Exception_OverridesOriginalObjectCannotBeFound(Utils.GetUserFriendlyNameOfObjectType(ObjectType.Column)));
		}

		// Token: 0x06001DCD RID: 7629 RVA: 0x000C97BC File Offset: 0x000C79BC
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
			if (!(text == "sourceColumn"))
			{
				return false;
			}
			jsonReader.Read();
			jsonReader.VerifyToken(9);
			this.SourceColumn = (string)jsonReader.Value;
			jsonReader.Read();
			return true;
		}

		// Token: 0x06001DCE RID: 7630 RVA: 0x000C9850 File Offset: 0x000C7A50
		internal static void WriteSchema(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue("DataColumnOverride object of Tabular Object Model (TOM)");
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("originalObject");
			JsonSchemaWriter.WriteSchemaForObjectPath(writer, ObjectType.Column, true);
			writer.WritePropertyName("sourceColumn");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WriteEndObject();
			writer.WriteEndObject();
		}

		// Token: 0x040006DB RID: 1755
		private DataColumnOverride.Overrides replacementProperties;

		// Token: 0x0200043F RID: 1087
		internal sealed class Overrides : ReplacementPropertiesCollection
		{
			// Token: 0x170007EC RID: 2028
			// (get) Token: 0x060028E4 RID: 10468 RVA: 0x000F0793 File Offset: 0x000EE993
			// (set) Token: 0x060028E5 RID: 10469 RVA: 0x000F07A0 File Offset: 0x000EE9A0
			public string SourceColumn
			{
				get
				{
					return this.sourceColumn.Value;
				}
				set
				{
					this.sourceColumn.Value = value;
				}
			}

			// Token: 0x060028E6 RID: 10470 RVA: 0x000F07AE File Offset: 0x000EE9AE
			internal override bool IsLinkOverriden(string propertyName, out MetadataObject newValue)
			{
				throw TomInternalException.Create("Invalid property name - {0}", new object[] { propertyName });
			}

			// Token: 0x060028E7 RID: 10471 RVA: 0x000F07C4 File Offset: 0x000EE9C4
			internal override bool IsPropertyOverriden(string propertyName, out object newValue)
			{
				if (propertyName == "SourceColumn")
				{
					newValue = this.sourceColumn.Value;
					return this.sourceColumn.IsSet;
				}
				throw TomInternalException.Create("Invalid property name - {0}", new object[] { propertyName });
			}

			// Token: 0x04001422 RID: 5154
			private ReplacementPropertiesCollection.OverridenProperty<string> sourceColumn;
		}
	}
}
