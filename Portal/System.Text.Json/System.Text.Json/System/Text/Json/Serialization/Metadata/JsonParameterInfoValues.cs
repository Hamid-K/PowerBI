using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace System.Text.Json.Serialization.Metadata
{
	// Token: 0x020000AA RID: 170
	[NullableContext(1)]
	[Nullable(0)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public sealed class JsonParameterInfoValues
	{
		// Token: 0x17000209 RID: 521
		// (get) Token: 0x060009E5 RID: 2533 RVA: 0x00029A16 File Offset: 0x00027C16
		// (set) Token: 0x060009E6 RID: 2534 RVA: 0x00029A1E File Offset: 0x00027C1E
		public string Name { get; set; }

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x060009E7 RID: 2535 RVA: 0x00029A27 File Offset: 0x00027C27
		// (set) Token: 0x060009E8 RID: 2536 RVA: 0x00029A2F File Offset: 0x00027C2F
		public Type ParameterType { get; set; }

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x060009E9 RID: 2537 RVA: 0x00029A38 File Offset: 0x00027C38
		// (set) Token: 0x060009EA RID: 2538 RVA: 0x00029A40 File Offset: 0x00027C40
		public int Position { get; set; }

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x060009EB RID: 2539 RVA: 0x00029A49 File Offset: 0x00027C49
		// (set) Token: 0x060009EC RID: 2540 RVA: 0x00029A51 File Offset: 0x00027C51
		public bool HasDefaultValue { get; set; }

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x060009ED RID: 2541 RVA: 0x00029A5A File Offset: 0x00027C5A
		// (set) Token: 0x060009EE RID: 2542 RVA: 0x00029A62 File Offset: 0x00027C62
		[Nullable(2)]
		public object DefaultValue
		{
			[NullableContext(2)]
			get;
			[NullableContext(2)]
			set;
		}
	}
}
