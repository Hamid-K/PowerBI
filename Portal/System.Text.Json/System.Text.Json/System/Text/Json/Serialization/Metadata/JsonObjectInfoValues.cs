using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace System.Text.Json.Serialization.Metadata
{
	// Token: 0x020000A9 RID: 169
	[EditorBrowsable(EditorBrowsableState.Never)]
	public sealed class JsonObjectInfoValues<[Nullable(2)] T>
	{
		// Token: 0x17000203 RID: 515
		// (get) Token: 0x060009D8 RID: 2520 RVA: 0x000299A8 File Offset: 0x00027BA8
		// (set) Token: 0x060009D9 RID: 2521 RVA: 0x000299B0 File Offset: 0x00027BB0
		[Nullable(new byte[] { 2, 1 })]
		public Func<T> ObjectCreator
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get;
			[param: Nullable(new byte[] { 2, 1 })]
			set;
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x060009DA RID: 2522 RVA: 0x000299B9 File Offset: 0x00027BB9
		// (set) Token: 0x060009DB RID: 2523 RVA: 0x000299C1 File Offset: 0x00027BC1
		[Nullable(new byte[] { 2, 1, 1, 1 })]
		public Func<object[], T> ObjectWithParameterizedConstructorCreator
		{
			[return: Nullable(new byte[] { 2, 1, 1, 1 })]
			get;
			[param: Nullable(new byte[] { 2, 1, 1, 1 })]
			set;
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x060009DC RID: 2524 RVA: 0x000299CA File Offset: 0x00027BCA
		// (set) Token: 0x060009DD RID: 2525 RVA: 0x000299D2 File Offset: 0x00027BD2
		[Nullable(new byte[] { 2, 1, 1, 1 })]
		public Func<JsonSerializerContext, JsonPropertyInfo[]> PropertyMetadataInitializer
		{
			[return: Nullable(new byte[] { 2, 1, 1, 1 })]
			get;
			[param: Nullable(new byte[] { 2, 1, 1, 1 })]
			set;
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x060009DE RID: 2526 RVA: 0x000299DB File Offset: 0x00027BDB
		// (set) Token: 0x060009DF RID: 2527 RVA: 0x000299E3 File Offset: 0x00027BE3
		[Nullable(new byte[] { 2, 1, 1 })]
		public Func<JsonParameterInfoValues[]> ConstructorParameterMetadataInitializer
		{
			[return: Nullable(new byte[] { 2, 1, 1 })]
			get;
			[param: Nullable(new byte[] { 2, 1, 1 })]
			set;
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x060009E0 RID: 2528 RVA: 0x000299EC File Offset: 0x00027BEC
		// (set) Token: 0x060009E1 RID: 2529 RVA: 0x000299F4 File Offset: 0x00027BF4
		public JsonNumberHandling NumberHandling { get; set; }

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x060009E2 RID: 2530 RVA: 0x000299FD File Offset: 0x00027BFD
		// (set) Token: 0x060009E3 RID: 2531 RVA: 0x00029A05 File Offset: 0x00027C05
		[Nullable(new byte[] { 2, 1, 1 })]
		public Action<Utf8JsonWriter, T> SerializeHandler
		{
			[return: Nullable(new byte[] { 2, 1, 1 })]
			get;
			[param: Nullable(new byte[] { 2, 1, 1 })]
			set;
		}
	}
}
