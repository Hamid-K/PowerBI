using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000067 RID: 103
	[NullableContext(2)]
	[Nullable(0)]
	internal class ReflectionMember
	{
		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000590 RID: 1424 RVA: 0x00017E42 File Offset: 0x00016042
		// (set) Token: 0x06000591 RID: 1425 RVA: 0x00017E4A File Offset: 0x0001604A
		public Type MemberType { get; set; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000592 RID: 1426 RVA: 0x00017E53 File Offset: 0x00016053
		// (set) Token: 0x06000593 RID: 1427 RVA: 0x00017E5B File Offset: 0x0001605B
		[Nullable(new byte[] { 2, 1, 2 })]
		public Func<object, object> Getter
		{
			[return: Nullable(new byte[] { 2, 1, 2 })]
			get;
			[param: Nullable(new byte[] { 2, 1, 2 })]
			set;
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000594 RID: 1428 RVA: 0x00017E64 File Offset: 0x00016064
		// (set) Token: 0x06000595 RID: 1429 RVA: 0x00017E6C File Offset: 0x0001606C
		[Nullable(new byte[] { 2, 1, 2 })]
		public Action<object, object> Setter
		{
			[return: Nullable(new byte[] { 2, 1, 2 })]
			get;
			[param: Nullable(new byte[] { 2, 1, 2 })]
			set;
		}
	}
}
