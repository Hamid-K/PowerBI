using System;
using Microsoft.AnalysisServices.Tabular.Extensions;
using Microsoft.AnalysisServices.Tabular.Json;
using Microsoft.AnalysisServices.Tabular.Metadata;
using Microsoft.AnalysisServices.Tabular.Utilities;

namespace Microsoft.AnalysisServices.Tabular.DataRefresh
{
	// Token: 0x02000212 RID: 530
	[CompatibilityRequirement("1400")]
	public class NamedExpressionOverride : IObjectOverride
	{
		// Token: 0x06001DDB RID: 7643 RVA: 0x000C9905 File Offset: 0x000C7B05
		public NamedExpressionOverride()
		{
			this.replacementProperties = new NamedExpressionOverride.Overrides();
		}

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x06001DDC RID: 7644 RVA: 0x000C9918 File Offset: 0x000C7B18
		// (set) Token: 0x06001DDD RID: 7645 RVA: 0x000C9920 File Offset: 0x000C7B20
		public NamedExpression OriginalObject { get; set; }

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x06001DDE RID: 7646 RVA: 0x000C9929 File Offset: 0x000C7B29
		// (set) Token: 0x06001DDF RID: 7647 RVA: 0x000C9931 File Offset: 0x000C7B31
		internal ObjectPath OriginalObjectPath { get; set; }

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x06001DE0 RID: 7648 RVA: 0x000C993A File Offset: 0x000C7B3A
		// (set) Token: 0x06001DE1 RID: 7649 RVA: 0x000C9947 File Offset: 0x000C7B47
		public string Expression
		{
			get
			{
				return this.replacementProperties.Expression;
			}
			set
			{
				this.replacementProperties.Expression = value;
			}
		}

		// Token: 0x06001DE2 RID: 7650 RVA: 0x000C9958 File Offset: 0x000C7B58
		private void EnsureAllReferencesResolved(Model model)
		{
			if (this.OriginalObject != null)
			{
				return;
			}
			if (this.OriginalObjectPath == null)
			{
				throw new TomException(TomSR.Exception_OverridesOriginalObjectPathIsNull(Utils.GetUserFriendlyNameOfObjectType(ObjectType.Expression)));
			}
			this.OriginalObjectPath.Normalize();
			NamedExpression namedExpression = ObjectTreeHelper.LocateObjectByPath(this.OriginalObjectPath, model) as NamedExpression;
			if (namedExpression != null)
			{
				this.OriginalObject = namedExpression;
				return;
			}
			throw new TomException(TomSR.Exception_OverridesOriginalObjectCannotBeFound(Utils.GetUserFriendlyNameOfObjectType(ObjectType.Expression)));
		}

		// Token: 0x06001DE3 RID: 7651 RVA: 0x000C99C4 File Offset: 0x000C7BC4
		internal bool ReadPropertyFromJson(JsonTextReader jsonReader)
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
			if (!(text == "expression"))
			{
				return false;
			}
			jsonReader.Read();
			jsonReader.VerifyToken(9);
			this.Expression = (string)jsonReader.Value;
			jsonReader.Read();
			return true;
		}

		// Token: 0x06001DE4 RID: 7652 RVA: 0x000C9A58 File Offset: 0x000C7C58
		internal static void WriteSchema(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue("NamedExpressionOverride object of Tabular Object Model (TOM)");
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("originalObject");
			JsonSchemaWriter.WriteSchemaForObjectPath(writer, ObjectType.Expression, true);
			writer.WritePropertyName("expression");
			if (options.SplitMultilineStrings)
			{
				JsonSchemaWriter.WriteSchemaForMultilineString(writer);
			}
			else
			{
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			writer.WriteEndObject();
			writer.WriteEndObject();
		}

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x06001DE5 RID: 7653 RVA: 0x000C9AE9 File Offset: 0x000C7CE9
		ObjectType IObjectOverride.ObjectType
		{
			get
			{
				return ObjectType.Expression;
			}
		}

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x06001DE6 RID: 7654 RVA: 0x000C9AED File Offset: 0x000C7CED
		MetadataObject IObjectOverride.OriginalObject
		{
			get
			{
				return this.OriginalObject;
			}
		}

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x06001DE7 RID: 7655 RVA: 0x000C9AF5 File Offset: 0x000C7CF5
		ObjectPath IObjectOverride.OriginalObjectPath
		{
			get
			{
				return this.OriginalObjectPath;
			}
		}

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x06001DE8 RID: 7656 RVA: 0x000C9AFD File Offset: 0x000C7CFD
		ReplacementPropertiesCollection IObjectOverride.ReplacementProperties
		{
			get
			{
				return this.replacementProperties;
			}
		}

		// Token: 0x06001DE9 RID: 7657 RVA: 0x000C9B05 File Offset: 0x000C7D05
		void IObjectOverride.EnsureAllReferencesResolved(Model model)
		{
			this.EnsureAllReferencesResolved(model);
		}

		// Token: 0x06001DEA RID: 7658 RVA: 0x000C9B0E File Offset: 0x000C7D0E
		bool IObjectOverride.ReadPropertyFromJson(JsonTextReader jsonReader)
		{
			return this.ReadPropertyFromJson(jsonReader);
		}

		// Token: 0x040006DE RID: 1758
		private NamedExpressionOverride.Overrides replacementProperties;

		// Token: 0x02000441 RID: 1089
		internal static class OverrideName
		{
			// Token: 0x0400142F RID: 5167
			public const string ExpressionSource = "ExpressionSourceID";

			// Token: 0x04001430 RID: 5168
			public const string Expression = "Expression";
		}

		// Token: 0x02000442 RID: 1090
		internal sealed class Overrides : ReplacementPropertiesCollection
		{
			// Token: 0x170007ED RID: 2029
			// (get) Token: 0x060028E9 RID: 10473 RVA: 0x000F0808 File Offset: 0x000EEA08
			// (set) Token: 0x060028EA RID: 10474 RVA: 0x000F0815 File Offset: 0x000EEA15
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

			// Token: 0x170007EE RID: 2030
			// (get) Token: 0x060028EB RID: 10475 RVA: 0x000F0823 File Offset: 0x000EEA23
			// (set) Token: 0x060028EC RID: 10476 RVA: 0x000F0830 File Offset: 0x000EEA30
			public string Expression
			{
				get
				{
					return this.expression.Value;
				}
				set
				{
					this.expression.Value = value;
				}
			}

			// Token: 0x060028ED RID: 10477 RVA: 0x000F083E File Offset: 0x000EEA3E
			internal override bool IsLinkOverriden(string propertyName, out MetadataObject newValue)
			{
				if (propertyName == "ExpressionSourceID")
				{
					newValue = this.expressionSourceID.Value;
					return this.expressionSourceID.IsSet;
				}
				throw TomInternalException.Create("Invalid property name - {0}", new object[] { propertyName });
			}

			// Token: 0x060028EE RID: 10478 RVA: 0x000F087A File Offset: 0x000EEA7A
			internal override bool IsPropertyOverriden(string propertyName, out object newValue)
			{
				if (propertyName == "Expression")
				{
					newValue = this.expression.Value;
					return this.expression.IsSet;
				}
				throw TomInternalException.Create("Invalid property name - {0}", new object[] { propertyName });
			}

			// Token: 0x04001431 RID: 5169
			private ReplacementPropertiesCollection.OverridenProperty<NamedExpression> expressionSourceID;

			// Token: 0x04001432 RID: 5170
			private ReplacementPropertiesCollection.OverridenProperty<string> expression;
		}
	}
}
