using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace System.Text.Json.Serialization.Metadata
{
	// Token: 0x020000AF RID: 175
	[NullableContext(1)]
	[Nullable(0)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public sealed class JsonPropertyInfoValues<[Nullable(2)] T>
	{
		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000A83 RID: 2691 RVA: 0x0002B0DA File Offset: 0x000292DA
		// (set) Token: 0x06000A84 RID: 2692 RVA: 0x0002B0E2 File Offset: 0x000292E2
		public bool IsProperty { get; set; }

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000A85 RID: 2693 RVA: 0x0002B0EB File Offset: 0x000292EB
		// (set) Token: 0x06000A86 RID: 2694 RVA: 0x0002B0F3 File Offset: 0x000292F3
		public bool IsPublic { get; set; }

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000A87 RID: 2695 RVA: 0x0002B0FC File Offset: 0x000292FC
		// (set) Token: 0x06000A88 RID: 2696 RVA: 0x0002B104 File Offset: 0x00029304
		public bool IsVirtual { get; set; }

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000A89 RID: 2697 RVA: 0x0002B10D File Offset: 0x0002930D
		// (set) Token: 0x06000A8A RID: 2698 RVA: 0x0002B115 File Offset: 0x00029315
		public Type DeclaringType { get; set; }

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000A8B RID: 2699 RVA: 0x0002B11E File Offset: 0x0002931E
		// (set) Token: 0x06000A8C RID: 2700 RVA: 0x0002B126 File Offset: 0x00029326
		public JsonTypeInfo PropertyTypeInfo { get; set; }

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000A8D RID: 2701 RVA: 0x0002B12F File Offset: 0x0002932F
		// (set) Token: 0x06000A8E RID: 2702 RVA: 0x0002B137 File Offset: 0x00029337
		[Nullable(new byte[] { 2, 1 })]
		public JsonConverter<T> Converter
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get;
			[param: Nullable(new byte[] { 2, 1 })]
			set;
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000A8F RID: 2703 RVA: 0x0002B140 File Offset: 0x00029340
		// (set) Token: 0x06000A90 RID: 2704 RVA: 0x0002B148 File Offset: 0x00029348
		[Nullable(new byte[] { 2, 1, 2 })]
		public Func<object, T> Getter
		{
			[return: Nullable(new byte[] { 2, 1, 2 })]
			get;
			[param: Nullable(new byte[] { 2, 1, 2 })]
			set;
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000A91 RID: 2705 RVA: 0x0002B151 File Offset: 0x00029351
		// (set) Token: 0x06000A92 RID: 2706 RVA: 0x0002B159 File Offset: 0x00029359
		[Nullable(new byte[] { 2, 1, 2 })]
		public Action<object, T> Setter
		{
			[return: Nullable(new byte[] { 2, 1, 2 })]
			get;
			[param: Nullable(new byte[] { 2, 1, 2 })]
			set;
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000A93 RID: 2707 RVA: 0x0002B162 File Offset: 0x00029362
		// (set) Token: 0x06000A94 RID: 2708 RVA: 0x0002B16A File Offset: 0x0002936A
		public JsonIgnoreCondition? IgnoreCondition { get; set; }

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000A95 RID: 2709 RVA: 0x0002B173 File Offset: 0x00029373
		// (set) Token: 0x06000A96 RID: 2710 RVA: 0x0002B17B File Offset: 0x0002937B
		public bool HasJsonInclude { get; set; }

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000A97 RID: 2711 RVA: 0x0002B184 File Offset: 0x00029384
		// (set) Token: 0x06000A98 RID: 2712 RVA: 0x0002B18C File Offset: 0x0002938C
		public bool IsExtensionData { get; set; }

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000A99 RID: 2713 RVA: 0x0002B195 File Offset: 0x00029395
		// (set) Token: 0x06000A9A RID: 2714 RVA: 0x0002B19D File Offset: 0x0002939D
		public JsonNumberHandling? NumberHandling { get; set; }

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000A9B RID: 2715 RVA: 0x0002B1A6 File Offset: 0x000293A6
		// (set) Token: 0x06000A9C RID: 2716 RVA: 0x0002B1AE File Offset: 0x000293AE
		public string PropertyName { get; set; }

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000A9D RID: 2717 RVA: 0x0002B1B7 File Offset: 0x000293B7
		// (set) Token: 0x06000A9E RID: 2718 RVA: 0x0002B1BF File Offset: 0x000293BF
		[Nullable(2)]
		public string JsonPropertyName
		{
			[NullableContext(2)]
			get;
			[NullableContext(2)]
			set;
		}
	}
}
