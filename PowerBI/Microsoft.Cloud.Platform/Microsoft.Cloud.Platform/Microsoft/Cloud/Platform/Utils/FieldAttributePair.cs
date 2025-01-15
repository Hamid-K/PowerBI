using System;
using System.Reflection;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000208 RID: 520
	public class FieldAttributePair<T> : MemberInfoAttributePair<FieldInfo, T> where T : Attribute
	{
		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000DC5 RID: 3525 RVA: 0x0003080C File Offset: 0x0002EA0C
		// (set) Token: 0x06000DC6 RID: 3526 RVA: 0x00030814 File Offset: 0x0002EA14
		public FieldInfo Field
		{
			get
			{
				return base.Member;
			}
			set
			{
				base.Member = value;
			}
		}
	}
}
