using System;

namespace System.Web.Http.Description
{
	// Token: 0x020000E3 RID: 227
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
	public sealed class ResponseTypeAttribute : Attribute
	{
		// Token: 0x060005CE RID: 1486 RVA: 0x0000E929 File Offset: 0x0000CB29
		public ResponseTypeAttribute(Type responseType)
		{
			this.ResponseType = responseType;
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x060005CF RID: 1487 RVA: 0x0000E938 File Offset: 0x0000CB38
		// (set) Token: 0x060005D0 RID: 1488 RVA: 0x0000E940 File Offset: 0x0000CB40
		public Type ResponseType { get; private set; }
	}
}
