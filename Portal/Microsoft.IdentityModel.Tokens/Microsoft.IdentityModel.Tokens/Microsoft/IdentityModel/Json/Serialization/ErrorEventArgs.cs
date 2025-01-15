using System;
using System.Runtime.CompilerServices;

namespace Microsoft.IdentityModel.Json.Serialization
{
	// Token: 0x0200007D RID: 125
	[NullableContext(1)]
	[Nullable(0)]
	internal class ErrorEventArgs : EventArgs
	{
		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600067D RID: 1661 RVA: 0x0001C0EB File Offset: 0x0001A2EB
		[Nullable(2)]
		public object CurrentObject
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600067E RID: 1662 RVA: 0x0001C0F3 File Offset: 0x0001A2F3
		public ErrorContext ErrorContext { get; }

		// Token: 0x0600067F RID: 1663 RVA: 0x0001C0FB File Offset: 0x0001A2FB
		public ErrorEventArgs([Nullable(2)] object currentObject, ErrorContext errorContext)
		{
			this.CurrentObject = currentObject;
			this.ErrorContext = errorContext;
		}
	}
}
