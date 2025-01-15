using System;
using Microsoft.AnalysisServices.Tabular.Json.Linq;
using Microsoft.AnalysisServices.Tabular.Metadata;
using Microsoft.AnalysisServices.Tabular.Utilities;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000F5 RID: 245
	[CompatibilityRequirement("1400")]
	public sealed class DataSourceOptions : CustomJsonProperty<StructuredDataSource>
	{
		// Token: 0x0600102A RID: 4138 RVA: 0x00078267 File Offset: 0x00076467
		public DataSourceOptions()
		{
		}

		// Token: 0x0600102B RID: 4139 RVA: 0x0007826F File Offset: 0x0007646F
		public DataSourceOptions(string json)
			: base(json)
		{
			this.json = new CustomJsonPropertyHelper(json);
		}

		// Token: 0x0600102C RID: 4140 RVA: 0x00078284 File Offset: 0x00076484
		internal DataSourceOptions(JToken json)
		{
			this.json = new CustomJsonPropertyHelper(json);
		}

		// Token: 0x0600102D RID: 4141 RVA: 0x00078298 File Offset: 0x00076498
		public override string ToJson()
		{
			return this.json.ToString();
		}

		// Token: 0x0600102E RID: 4142 RVA: 0x000782AB File Offset: 0x000764AB
		internal override JToken GetJson()
		{
			return this.json.ToJson();
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x0600102F RID: 4143 RVA: 0x000782B8 File Offset: 0x000764B8
		private protected override string PropertyName
		{
			get
			{
				return "Options";
			}
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06001030 RID: 4144 RVA: 0x000782BF File Offset: 0x000764BF
		private protected override CompatibilityRestrictionSet Restrictions
		{
			get
			{
				return CompatibilityRestrictions.StructuredDataSource_Options;
			}
		}

		// Token: 0x06001031 RID: 4145 RVA: 0x000782C6 File Offset: 0x000764C6
		private protected override object GetValueImpl(string key)
		{
			return this.json.GetValue<object>(key);
		}

		// Token: 0x06001032 RID: 4146 RVA: 0x000782D4 File Offset: 0x000764D4
		private protected override void SetValueImpl(string key, object value)
		{
			this.json.SetValue<object>(key, value);
		}

		// Token: 0x06001033 RID: 4147 RVA: 0x000782E3 File Offset: 0x000764E3
		private protected override void ParseJsonImpl(string json)
		{
			this.json = new CustomJsonPropertyHelper(json);
		}

		// Token: 0x06001034 RID: 4148 RVA: 0x000782F1 File Offset: 0x000764F1
		private protected override void ParseJsonImpl(JToken json)
		{
			this.json = new CustomJsonPropertyHelper(json);
		}

		// Token: 0x06001035 RID: 4149 RVA: 0x000782FF File Offset: 0x000764FF
		private protected override void MarkAsDirty()
		{
			this.owner.options.MarkAsDirty();
		}

		// Token: 0x0400021E RID: 542
		private CustomJsonPropertyHelper json;
	}
}
