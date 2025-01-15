using System;
using System.Runtime.CompilerServices;

namespace System.Text.Json.Serialization
{
	// Token: 0x0200006E RID: 110
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public sealed class JsonSourceGenerationOptionsAttribute : JsonAttribute
	{
		// Token: 0x060007DB RID: 2011 RVA: 0x00024823 File Offset: 0x00022A23
		public JsonSourceGenerationOptionsAttribute()
		{
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x0002482B File Offset: 0x00022A2B
		public JsonSourceGenerationOptionsAttribute(JsonSerializerDefaults defaults)
		{
			if (defaults == JsonSerializerDefaults.Web)
			{
				this.PropertyNameCaseInsensitive = true;
				this.PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase;
				this.NumberHandling = JsonNumberHandling.AllowReadingFromString;
				return;
			}
			if (defaults != JsonSerializerDefaults.General)
			{
				throw new ArgumentOutOfRangeException("defaults");
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060007DD RID: 2013 RVA: 0x0002485B File Offset: 0x00022A5B
		// (set) Token: 0x060007DE RID: 2014 RVA: 0x00024863 File Offset: 0x00022A63
		public bool AllowTrailingCommas { get; set; }

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060007DF RID: 2015 RVA: 0x0002486C File Offset: 0x00022A6C
		// (set) Token: 0x060007E0 RID: 2016 RVA: 0x00024874 File Offset: 0x00022A74
		[Nullable(new byte[] { 2, 1 })]
		public Type[] Converters
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get;
			[param: Nullable(new byte[] { 2, 1 })]
			set;
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060007E1 RID: 2017 RVA: 0x0002487D File Offset: 0x00022A7D
		// (set) Token: 0x060007E2 RID: 2018 RVA: 0x00024885 File Offset: 0x00022A85
		public int DefaultBufferSize { get; set; }

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060007E3 RID: 2019 RVA: 0x0002488E File Offset: 0x00022A8E
		// (set) Token: 0x060007E4 RID: 2020 RVA: 0x00024896 File Offset: 0x00022A96
		public JsonIgnoreCondition DefaultIgnoreCondition { get; set; }

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060007E5 RID: 2021 RVA: 0x0002489F File Offset: 0x00022A9F
		// (set) Token: 0x060007E6 RID: 2022 RVA: 0x000248A7 File Offset: 0x00022AA7
		public JsonKnownNamingPolicy DictionaryKeyPolicy { get; set; }

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060007E7 RID: 2023 RVA: 0x000248B0 File Offset: 0x00022AB0
		// (set) Token: 0x060007E8 RID: 2024 RVA: 0x000248B8 File Offset: 0x00022AB8
		public bool IgnoreReadOnlyFields { get; set; }

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060007E9 RID: 2025 RVA: 0x000248C1 File Offset: 0x00022AC1
		// (set) Token: 0x060007EA RID: 2026 RVA: 0x000248C9 File Offset: 0x00022AC9
		public bool IgnoreReadOnlyProperties { get; set; }

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060007EB RID: 2027 RVA: 0x000248D2 File Offset: 0x00022AD2
		// (set) Token: 0x060007EC RID: 2028 RVA: 0x000248DA File Offset: 0x00022ADA
		public bool IncludeFields { get; set; }

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060007ED RID: 2029 RVA: 0x000248E3 File Offset: 0x00022AE3
		// (set) Token: 0x060007EE RID: 2030 RVA: 0x000248EB File Offset: 0x00022AEB
		public int MaxDepth { get; set; }

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060007EF RID: 2031 RVA: 0x000248F4 File Offset: 0x00022AF4
		// (set) Token: 0x060007F0 RID: 2032 RVA: 0x000248FC File Offset: 0x00022AFC
		public JsonNumberHandling NumberHandling { get; set; }

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060007F1 RID: 2033 RVA: 0x00024905 File Offset: 0x00022B05
		// (set) Token: 0x060007F2 RID: 2034 RVA: 0x0002490D File Offset: 0x00022B0D
		public JsonObjectCreationHandling PreferredObjectCreationHandling { get; set; }

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060007F3 RID: 2035 RVA: 0x00024916 File Offset: 0x00022B16
		// (set) Token: 0x060007F4 RID: 2036 RVA: 0x0002491E File Offset: 0x00022B1E
		public bool PropertyNameCaseInsensitive { get; set; }

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060007F5 RID: 2037 RVA: 0x00024927 File Offset: 0x00022B27
		// (set) Token: 0x060007F6 RID: 2038 RVA: 0x0002492F File Offset: 0x00022B2F
		public JsonKnownNamingPolicy PropertyNamingPolicy { get; set; }

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x060007F7 RID: 2039 RVA: 0x00024938 File Offset: 0x00022B38
		// (set) Token: 0x060007F8 RID: 2040 RVA: 0x00024940 File Offset: 0x00022B40
		public JsonCommentHandling ReadCommentHandling { get; set; }

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060007F9 RID: 2041 RVA: 0x00024949 File Offset: 0x00022B49
		// (set) Token: 0x060007FA RID: 2042 RVA: 0x00024951 File Offset: 0x00022B51
		public JsonUnknownTypeHandling UnknownTypeHandling { get; set; }

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060007FB RID: 2043 RVA: 0x0002495A File Offset: 0x00022B5A
		// (set) Token: 0x060007FC RID: 2044 RVA: 0x00024962 File Offset: 0x00022B62
		public JsonUnmappedMemberHandling UnmappedMemberHandling { get; set; }

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060007FD RID: 2045 RVA: 0x0002496B File Offset: 0x00022B6B
		// (set) Token: 0x060007FE RID: 2046 RVA: 0x00024973 File Offset: 0x00022B73
		public bool WriteIndented { get; set; }

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060007FF RID: 2047 RVA: 0x0002497C File Offset: 0x00022B7C
		// (set) Token: 0x06000800 RID: 2048 RVA: 0x00024984 File Offset: 0x00022B84
		public JsonSourceGenerationMode GenerationMode { get; set; }

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000801 RID: 2049 RVA: 0x0002498D File Offset: 0x00022B8D
		// (set) Token: 0x06000802 RID: 2050 RVA: 0x00024995 File Offset: 0x00022B95
		public bool UseStringEnumConverter { get; set; }
	}
}
