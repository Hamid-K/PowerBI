using System;

namespace System.Text.Json.Serialization.Metadata
{
	// Token: 0x020000AB RID: 171
	internal abstract class JsonParameterInfo
	{
		// Token: 0x1700020E RID: 526
		// (get) Token: 0x060009F0 RID: 2544 RVA: 0x00029A73 File Offset: 0x00027C73
		public JsonConverter EffectiveConverter
		{
			get
			{
				return this.MatchingProperty.EffectiveConverter;
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x060009F1 RID: 2545 RVA: 0x00029A80 File Offset: 0x00027C80
		// (set) Token: 0x060009F2 RID: 2546 RVA: 0x00029A88 File Offset: 0x00027C88
		public object DefaultValue { get; private protected set; }

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x060009F3 RID: 2547 RVA: 0x00029A91 File Offset: 0x00027C91
		public bool IgnoreNullTokensOnRead { get; }

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x060009F4 RID: 2548 RVA: 0x00029A99 File Offset: 0x00027C99
		public JsonSerializerOptions Options { get; }

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x060009F5 RID: 2549 RVA: 0x00029AA1 File Offset: 0x00027CA1
		public byte[] NameAsUtf8Bytes { get; }

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x060009F6 RID: 2550 RVA: 0x00029AA9 File Offset: 0x00027CA9
		public JsonNumberHandling? NumberHandling { get; }

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x060009F7 RID: 2551 RVA: 0x00029AB1 File Offset: 0x00027CB1
		public int Position { get; }

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x060009F8 RID: 2552 RVA: 0x00029AB9 File Offset: 0x00027CB9
		public JsonTypeInfo JsonTypeInfo
		{
			get
			{
				return this.MatchingProperty.JsonTypeInfo;
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x060009F9 RID: 2553 RVA: 0x00029AC6 File Offset: 0x00027CC6
		public Type ParameterType { get; }

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x060009FA RID: 2554 RVA: 0x00029ACE File Offset: 0x00027CCE
		public bool ShouldDeserialize { get; }

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x060009FB RID: 2555 RVA: 0x00029AD6 File Offset: 0x00027CD6
		public JsonPropertyInfo MatchingProperty { get; }

		// Token: 0x060009FC RID: 2556 RVA: 0x00029AE0 File Offset: 0x00027CE0
		public JsonParameterInfo(JsonParameterInfoValues parameterInfoValues, JsonPropertyInfo matchingProperty)
		{
			this.MatchingProperty = matchingProperty;
			this.ShouldDeserialize = !matchingProperty.IsIgnored;
			this.Options = matchingProperty.Options;
			this.Position = parameterInfoValues.Position;
			this.ParameterType = matchingProperty.PropertyType;
			this.NameAsUtf8Bytes = matchingProperty.NameAsUtf8Bytes;
			this.IgnoreNullTokensOnRead = matchingProperty.IgnoreNullTokensOnRead;
			this.NumberHandling = matchingProperty.EffectiveNumberHandling;
		}
	}
}
