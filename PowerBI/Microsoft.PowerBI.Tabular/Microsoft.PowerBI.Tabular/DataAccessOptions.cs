using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.Json.Linq;
using Microsoft.AnalysisServices.Tabular.Metadata;
using Microsoft.AnalysisServices.Tabular.Utilities;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000F4 RID: 244
	[CompatibilityRequirement("1400")]
	public sealed class DataAccessOptions : CustomJsonProperty<Model>
	{
		// Token: 0x06001018 RID: 4120 RVA: 0x000780A9 File Offset: 0x000762A9
		public DataAccessOptions()
		{
		}

		// Token: 0x06001019 RID: 4121 RVA: 0x000780B1 File Offset: 0x000762B1
		internal DataAccessOptions(JToken json)
		{
			this.json = new CustomJsonPropertyHelper(json);
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x0600101A RID: 4122 RVA: 0x000780C5 File Offset: 0x000762C5
		// (set) Token: 0x0600101B RID: 4123 RVA: 0x000780D7 File Offset: 0x000762D7
		public bool FastCombine
		{
			get
			{
				return this.json.GetValue<bool>("fastCombine");
			}
			set
			{
				this.SetValue("fastCombine", value);
			}
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x0600101C RID: 4124 RVA: 0x000780E5 File Offset: 0x000762E5
		// (set) Token: 0x0600101D RID: 4125 RVA: 0x000780F7 File Offset: 0x000762F7
		public bool LegacyRedirects
		{
			get
			{
				return this.json.GetValue<bool>("legacyRedirects");
			}
			set
			{
				this.SetValue("legacyRedirects", value);
			}
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x0600101E RID: 4126 RVA: 0x00078105 File Offset: 0x00076305
		// (set) Token: 0x0600101F RID: 4127 RVA: 0x00078117 File Offset: 0x00076317
		public bool ReturnErrorValuesAsNull
		{
			get
			{
				return this.json.GetValue<bool>("returnErrorValuesAsNull");
			}
			set
			{
				this.SetValue("returnErrorValuesAsNull", value);
			}
		}

		// Token: 0x06001020 RID: 4128 RVA: 0x00078125 File Offset: 0x00076325
		public override string ToJson()
		{
			return this.json.ToString();
		}

		// Token: 0x06001021 RID: 4129 RVA: 0x00078138 File Offset: 0x00076338
		internal override JToken GetJson()
		{
			return this.json.ToJson();
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06001022 RID: 4130 RVA: 0x00078145 File Offset: 0x00076345
		private protected override string PropertyName
		{
			get
			{
				return "DataAccessOptions";
			}
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06001023 RID: 4131 RVA: 0x0007814C File Offset: 0x0007634C
		private protected override CompatibilityRestrictionSet Restrictions
		{
			get
			{
				return CompatibilityRestrictions.Model_DataAccessOptions;
			}
		}

		// Token: 0x06001024 RID: 4132 RVA: 0x00078154 File Offset: 0x00076354
		private protected override object GetValueImpl(string key)
		{
			if (key == "fastCombine" || key == "legacyRedirects" || key == "returnErrorValuesAsNull")
			{
				return this.json.GetValue<bool>(key);
			}
			return this.json.GetValue<object>(key);
		}

		// Token: 0x06001025 RID: 4133 RVA: 0x000781A8 File Offset: 0x000763A8
		private protected override void SetValueImpl(string key, object value)
		{
			if (key == "fastCombine" || key == "legacyRedirects" || key == "returnErrorValuesAsNull")
			{
				this.json.SetValue<bool>(key, Convert.ToBoolean(value));
				return;
			}
			this.json.SetValue<object>(key, value);
		}

		// Token: 0x06001026 RID: 4134 RVA: 0x000781FC File Offset: 0x000763FC
		private protected override void ParseJsonImpl(string json)
		{
			this.json = new CustomJsonPropertyHelper(json);
		}

		// Token: 0x06001027 RID: 4135 RVA: 0x0007820A File Offset: 0x0007640A
		private protected override void ParseJsonImpl(JToken json)
		{
			this.json = new CustomJsonPropertyHelper(json);
		}

		// Token: 0x06001028 RID: 4136 RVA: 0x00078218 File Offset: 0x00076418
		private protected override void MarkAsDirty()
		{
			this.owner.dataAccessOptions.MarkAsDirty();
		}

		// Token: 0x06001029 RID: 4137 RVA: 0x0007822C File Offset: 0x0007642C
		private void SetValue(string key, bool value)
		{
			if (this.json.GetValue<bool>(key) != value)
			{
				string text;
				KeyValuePair<CompatibilityMode, Stack<string>>[] array;
				base.OnChanging(out text, out array);
				this.json.SetValue<bool>(key, value);
				base.OnChanged(text, array);
			}
		}

		// Token: 0x0400021D RID: 541
		private CustomJsonPropertyHelper json;

		// Token: 0x020002F7 RID: 759
		internal static class JsonPropertyName
		{
			// Token: 0x04000AE3 RID: 2787
			public const string FastCombine = "fastCombine";

			// Token: 0x04000AE4 RID: 2788
			public const string LegacyRedirects = "legacyRedirects";

			// Token: 0x04000AE5 RID: 2789
			public const string ReturnErrorValuesAsNull = "returnErrorValuesAsNull";
		}
	}
}
