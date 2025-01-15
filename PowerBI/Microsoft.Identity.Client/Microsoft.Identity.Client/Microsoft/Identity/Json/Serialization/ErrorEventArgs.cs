using System;
using System.Runtime.CompilerServices;

namespace Microsoft.Identity.Json.Serialization
{
	// Token: 0x0200007C RID: 124
	internal class ErrorEventArgs : EventArgs
	{
		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000673 RID: 1651 RVA: 0x0001BB17 File Offset: 0x00019D17
		[Nullable(2)]
		public object CurrentObject
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000674 RID: 1652 RVA: 0x0001BB1F File Offset: 0x00019D1F
		public ErrorContext ErrorContext { get; }

		// Token: 0x06000675 RID: 1653 RVA: 0x0001BB27 File Offset: 0x00019D27
		public ErrorEventArgs([Nullable(2)] object currentObject, ErrorContext errorContext)
		{
			this.CurrentObject = currentObject;
			this.ErrorContext = errorContext;
		}
	}
}
