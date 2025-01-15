using System;
using System.Reflection;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200020B RID: 523
	public class MemberInfoAttributePair<TMemberInfo, TAttribute> where TMemberInfo : MemberInfo where TAttribute : Attribute
	{
		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000DCC RID: 3532 RVA: 0x00030846 File Offset: 0x0002EA46
		// (set) Token: 0x06000DCD RID: 3533 RVA: 0x0003084E File Offset: 0x0002EA4E
		public TMemberInfo Member { get; set; }

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000DCE RID: 3534 RVA: 0x00030857 File Offset: 0x0002EA57
		// (set) Token: 0x06000DCF RID: 3535 RVA: 0x0003085F File Offset: 0x0002EA5F
		public TAttribute Attribute { get; set; }
	}
}
