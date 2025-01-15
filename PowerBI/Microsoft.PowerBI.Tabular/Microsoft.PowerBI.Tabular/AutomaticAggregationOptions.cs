using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.AnalysisServices.Tabular.Json;
using Microsoft.AnalysisServices.Tabular.Json.Linq;
using Microsoft.AnalysisServices.Tabular.Metadata;
using Microsoft.AnalysisServices.Tabular.Utilities;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000E9 RID: 233
	[CompatibilityRequirement("1564")]
	public sealed class AutomaticAggregationOptions : CustomJsonProperty<Model>
	{
		// Token: 0x06000F71 RID: 3953 RVA: 0x000769BD File Offset: 0x00074BBD
		public AutomaticAggregationOptions()
		{
		}

		// Token: 0x06000F72 RID: 3954 RVA: 0x000769C5 File Offset: 0x00074BC5
		public AutomaticAggregationOptions(double queryCoverage)
		{
			this.QueryCoverage = queryCoverage;
		}

		// Token: 0x06000F73 RID: 3955 RVA: 0x000769D4 File Offset: 0x00074BD4
		internal AutomaticAggregationOptions(JToken json)
		{
			this.json = new CustomJsonPropertyHelper(json);
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06000F74 RID: 3956 RVA: 0x000769E8 File Offset: 0x00074BE8
		// (set) Token: 0x06000F75 RID: 3957 RVA: 0x000769FA File Offset: 0x00074BFA
		public double QueryCoverage
		{
			get
			{
				return this.json.GetValue<double>("queryCoverage");
			}
			set
			{
				if (value < 0.0 || value > 1.0)
				{
					throw new ArgumentOutOfRangeException("queryCoverage");
				}
				this.SetValue<double>("queryCoverage", value);
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06000F76 RID: 3958 RVA: 0x00076A2B File Offset: 0x00074C2B
		// (set) Token: 0x06000F77 RID: 3959 RVA: 0x00076A3D File Offset: 0x00074C3D
		public long DetailTableMinRows
		{
			get
			{
				return this.json.GetValue<long>("detailTableMinRows");
			}
			set
			{
				if (value < 0L)
				{
					throw new ArgumentOutOfRangeException("detailTableMinRows");
				}
				this.SetValue<long>("detailTableMinRows", value);
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06000F78 RID: 3960 RVA: 0x00076A5B File Offset: 0x00074C5B
		// (set) Token: 0x06000F79 RID: 3961 RVA: 0x00076A6D File Offset: 0x00074C6D
		public long AggregationTableMaxRows
		{
			get
			{
				return this.json.GetValue<long>("aggregationTableMaxRows");
			}
			set
			{
				if (value < 0L)
				{
					throw new ArgumentOutOfRangeException("aggregationTableMaxRows");
				}
				this.SetValue<long>("aggregationTableMaxRows", value);
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06000F7A RID: 3962 RVA: 0x00076A8B File Offset: 0x00074C8B
		// (set) Token: 0x06000F7B RID: 3963 RVA: 0x00076A9D File Offset: 0x00074C9D
		public long AggregationTableSizeLimit
		{
			get
			{
				return this.json.GetValue<long>("aggregationTableSizeLimit");
			}
			set
			{
				if (value < 0L || value > 100L)
				{
					throw new ArgumentOutOfRangeException("aggregationTableSizeLimit");
				}
				this.SetValue<long>("aggregationTableSizeLimit", value);
			}
		}

		// Token: 0x06000F7C RID: 3964 RVA: 0x00076AC1 File Offset: 0x00074CC1
		public override string ToJson()
		{
			return this.json.ToString();
		}

		// Token: 0x06000F7D RID: 3965 RVA: 0x00076AD4 File Offset: 0x00074CD4
		internal override JToken GetJson()
		{
			return this.json.ToJson();
		}

		// Token: 0x06000F7E RID: 3966 RVA: 0x00076AE4 File Offset: 0x00074CE4
		internal static void WriteAutomaticAggregationsOptionsSchema(JsonWriter writer)
		{
			writer.WritePropertyName("queryCoverage");
			writer.WriteStartObject();
			writer.WritePropertyName("type");
			writer.WriteValue("float");
			writer.WriteEndObject();
			writer.WritePropertyName("detailTableMinRows");
			writer.WriteStartObject();
			writer.WritePropertyName("type");
			writer.WriteValue("integer");
			writer.WriteEndObject();
			writer.WritePropertyName("aggregationTableMaxRows");
			writer.WriteStartObject();
			writer.WritePropertyName("type");
			writer.WriteValue("integer");
			writer.WriteEndObject();
			writer.WritePropertyName("aggregationTableSizeLimit");
			writer.WriteStartObject();
			writer.WritePropertyName("type");
			writer.WriteValue("integer");
			writer.WriteEndObject();
		}

		// Token: 0x06000F7F RID: 3967 RVA: 0x00076BA8 File Offset: 0x00074DA8
		internal static string GetDaxRecomendationQuery(AutomaticAggregationOptions options)
		{
			if (options == null)
			{
				return "evaluate aggregationrecommendations({})";
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("evaluate aggregationrecommendations({\r\n");
			stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "\t(\"{0}\",{1:0.0#})", "queryCoverage", options.QueryCoverage);
			if (options.DetailTableMinRows > 0L)
			{
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, ",\r\n\t(\"{0}\",{1})", "detailTableMinRows", options.DetailTableMinRows);
			}
			if (options.AggregationTableMaxRows > 0L)
			{
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, ",\r\n\t(\"{0}\",{1})", "aggregationTableMaxRows", options.AggregationTableMaxRows);
			}
			if (options.AggregationTableSizeLimit > 0L)
			{
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, ",\r\n\t(\"{0}\",{1})", "aggregationTableSizeLimit", options.AggregationTableSizeLimit);
			}
			stringBuilder.Append("\r\n})");
			return stringBuilder.ToString();
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06000F80 RID: 3968 RVA: 0x00076C84 File Offset: 0x00074E84
		private protected override string PropertyName
		{
			get
			{
				return "AutomaticAggregationOptions";
			}
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06000F81 RID: 3969 RVA: 0x00076C8B File Offset: 0x00074E8B
		private protected override CompatibilityRestrictionSet Restrictions
		{
			get
			{
				return CompatibilityRestrictions.Model_AutomaticAggregationOptions;
			}
		}

		// Token: 0x06000F82 RID: 3970 RVA: 0x00076C92 File Offset: 0x00074E92
		private protected override object GetValueImpl(string key)
		{
			return this.json.GetValue<object>(key);
		}

		// Token: 0x06000F83 RID: 3971 RVA: 0x00076CA0 File Offset: 0x00074EA0
		private protected override void SetValueImpl(string key, object value)
		{
			this.json.SetValue<object>(key, value);
		}

		// Token: 0x06000F84 RID: 3972 RVA: 0x00076CAF File Offset: 0x00074EAF
		private protected override void ParseJsonImpl(string json)
		{
			this.json = new CustomJsonPropertyHelper(json);
		}

		// Token: 0x06000F85 RID: 3973 RVA: 0x00076CBD File Offset: 0x00074EBD
		private protected override void ParseJsonImpl(JToken json)
		{
			this.json = new CustomJsonPropertyHelper(json);
		}

		// Token: 0x06000F86 RID: 3974 RVA: 0x00076CCB File Offset: 0x00074ECB
		private protected override void MarkAsDirty()
		{
			this.owner.automaticAggregationOptions.MarkAsDirty();
		}

		// Token: 0x06000F87 RID: 3975 RVA: 0x00076CE0 File Offset: 0x00074EE0
		private void SetValue<TValue>(string key, TValue value)
		{
			TValue value2 = this.json.GetValue<TValue>(key);
			if (!value2.Equals(value))
			{
				string text;
				KeyValuePair<CompatibilityMode, Stack<string>>[] array;
				base.OnChanging(out text, out array);
				this.json.SetValue<TValue>(key, value);
				base.OnChanged(text, array);
			}
		}

		// Token: 0x040001DA RID: 474
		private CustomJsonPropertyHelper json;

		// Token: 0x020002F3 RID: 755
		internal static class JsonPropertyName
		{
			// Token: 0x04000AD8 RID: 2776
			public const string QueryCoverage = "queryCoverage";

			// Token: 0x04000AD9 RID: 2777
			public const string DetailTableMinRows = "detailTableMinRows";

			// Token: 0x04000ADA RID: 2778
			public const string AggregationTableMaxRows = "aggregationTableMaxRows";

			// Token: 0x04000ADB RID: 2779
			public const string AggregationTableSizeLimit = "aggregationTableSizeLimit";
		}
	}
}
