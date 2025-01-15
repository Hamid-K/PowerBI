using System;
using System.Runtime.CompilerServices;

namespace Microsoft.Identity.Json.Utilities
{
	// Token: 0x02000067 RID: 103
	[NullableContext(2)]
	[Nullable(0)]
	internal class ReflectionMember
	{
		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000589 RID: 1417 RVA: 0x000178DA File Offset: 0x00015ADA
		// (set) Token: 0x0600058A RID: 1418 RVA: 0x000178E2 File Offset: 0x00015AE2
		public Type MemberType { get; set; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600058B RID: 1419 RVA: 0x000178EB File Offset: 0x00015AEB
		// (set) Token: 0x0600058C RID: 1420 RVA: 0x000178F3 File Offset: 0x00015AF3
		[Nullable(new byte[] { 2, 0, 2 })]
		public Func<object, object> Getter
		{
			[return: Nullable(new byte[] { 2, 0, 2 })]
			get;
			[param: Nullable(new byte[] { 2, 0, 2 })]
			set;
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x0600058D RID: 1421 RVA: 0x000178FC File Offset: 0x00015AFC
		// (set) Token: 0x0600058E RID: 1422 RVA: 0x00017904 File Offset: 0x00015B04
		[Nullable(new byte[] { 2, 0, 2 })]
		public Action<object, object> Setter
		{
			[return: Nullable(new byte[] { 2, 0, 2 })]
			get;
			[param: Nullable(new byte[] { 2, 0, 2 })]
			set;
		}
	}
}
