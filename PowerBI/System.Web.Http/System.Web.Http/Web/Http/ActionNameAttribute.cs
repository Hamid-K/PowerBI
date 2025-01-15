using System;

namespace System.Web.Http
{
	// Token: 0x02000023 RID: 35
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public sealed class ActionNameAttribute : Attribute
	{
		// Token: 0x060000BE RID: 190 RVA: 0x00003E85 File Offset: 0x00002085
		public ActionNameAttribute(string name)
		{
			this.Name = name;
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00003E94 File Offset: 0x00002094
		// (set) Token: 0x060000C0 RID: 192 RVA: 0x00003E9C File Offset: 0x0000209C
		public string Name { get; private set; }
	}
}
