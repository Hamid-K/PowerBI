using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace System.Text.Json.Serialization.Metadata
{
	// Token: 0x020000A7 RID: 167
	[NullableContext(2)]
	[Nullable(0)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public sealed class JsonCollectionInfoValues<TCollection>
	{
		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000987 RID: 2439 RVA: 0x00028FEE File Offset: 0x000271EE
		// (set) Token: 0x06000988 RID: 2440 RVA: 0x00028FF6 File Offset: 0x000271F6
		[Nullable(new byte[] { 2, 1 })]
		public Func<TCollection> ObjectCreator
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get;
			[param: Nullable(new byte[] { 2, 1 })]
			set;
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000989 RID: 2441 RVA: 0x00028FFF File Offset: 0x000271FF
		// (set) Token: 0x0600098A RID: 2442 RVA: 0x00029007 File Offset: 0x00027207
		public JsonTypeInfo KeyInfo { get; set; }

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x0600098B RID: 2443 RVA: 0x00029010 File Offset: 0x00027210
		// (set) Token: 0x0600098C RID: 2444 RVA: 0x00029018 File Offset: 0x00027218
		[Nullable(1)]
		public JsonTypeInfo ElementInfo
		{
			[NullableContext(1)]
			get;
			[NullableContext(1)]
			set;
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x0600098D RID: 2445 RVA: 0x00029021 File Offset: 0x00027221
		// (set) Token: 0x0600098E RID: 2446 RVA: 0x00029029 File Offset: 0x00027229
		public JsonNumberHandling NumberHandling { get; set; }

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x0600098F RID: 2447 RVA: 0x00029032 File Offset: 0x00027232
		// (set) Token: 0x06000990 RID: 2448 RVA: 0x0002903A File Offset: 0x0002723A
		[Nullable(new byte[] { 2, 1, 1 })]
		public Action<Utf8JsonWriter, TCollection> SerializeHandler
		{
			[return: Nullable(new byte[] { 2, 1, 1 })]
			get;
			[param: Nullable(new byte[] { 2, 1, 1 })]
			set;
		}
	}
}
