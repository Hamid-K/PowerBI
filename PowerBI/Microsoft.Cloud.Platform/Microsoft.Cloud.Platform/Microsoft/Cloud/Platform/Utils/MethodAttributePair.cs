using System;
using System.Reflection;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000209 RID: 521
	public class MethodAttributePair<T> : MemberInfoAttributePair<MethodInfo, T> where T : Attribute
	{
		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000DC8 RID: 3528 RVA: 0x00030825 File Offset: 0x0002EA25
		// (set) Token: 0x06000DC9 RID: 3529 RVA: 0x0003082D File Offset: 0x0002EA2D
		public MethodInfo Method
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
