using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x0200007C RID: 124
	[NullableContext(1)]
	[Nullable(0)]
	public class ErrorEventArgs : EventArgs
	{
		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600067C RID: 1660 RVA: 0x0001C0BF File Offset: 0x0001A2BF
		[Nullable(2)]
		public object CurrentObject
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600067D RID: 1661 RVA: 0x0001C0C7 File Offset: 0x0001A2C7
		public ErrorContext ErrorContext { get; }

		// Token: 0x0600067E RID: 1662 RVA: 0x0001C0CF File Offset: 0x0001A2CF
		public ErrorEventArgs([Nullable(2)] object currentObject, ErrorContext errorContext)
		{
			this.CurrentObject = currentObject;
			this.ErrorContext = errorContext;
		}
	}
}
