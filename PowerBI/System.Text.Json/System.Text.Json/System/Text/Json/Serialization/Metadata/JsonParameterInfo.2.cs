using System;

namespace System.Text.Json.Serialization.Metadata
{
	// Token: 0x020000AC RID: 172
	internal sealed class JsonParameterInfo<T> : JsonParameterInfo
	{
		// Token: 0x17000219 RID: 537
		// (get) Token: 0x060009FD RID: 2557 RVA: 0x00029B51 File Offset: 0x00027D51
		public new JsonConverter<T> EffectiveConverter
		{
			get
			{
				return this.MatchingProperty.EffectiveConverter;
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x060009FE RID: 2558 RVA: 0x00029B5E File Offset: 0x00027D5E
		public new JsonPropertyInfo<T> MatchingProperty { get; }

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x060009FF RID: 2559 RVA: 0x00029B66 File Offset: 0x00027D66
		public new T DefaultValue { get; }

		// Token: 0x06000A00 RID: 2560 RVA: 0x00029B70 File Offset: 0x00027D70
		public JsonParameterInfo(JsonParameterInfoValues parameterInfoValues, JsonPropertyInfo<T> matchingPropertyInfo)
			: base(parameterInfoValues, matchingPropertyInfo)
		{
			this.MatchingProperty = matchingPropertyInfo;
			this.DefaultValue = ((parameterInfoValues.HasDefaultValue && parameterInfoValues.DefaultValue != null) ? ((T)((object)parameterInfoValues.DefaultValue)) : default(T));
			base.DefaultValue = this.DefaultValue;
		}
	}
}
