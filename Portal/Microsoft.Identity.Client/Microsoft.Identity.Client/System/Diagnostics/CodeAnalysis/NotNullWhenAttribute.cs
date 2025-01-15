using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000008 RID: 8
	[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
	internal sealed class NotNullWhenAttribute : Attribute
	{
		// Token: 0x06000008 RID: 8 RVA: 0x000020AD File Offset: 0x000002AD
		public NotNullWhenAttribute(bool returnValue)
		{
			this.ReturnValue = returnValue;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000020BC File Offset: 0x000002BC
		public bool ReturnValue { get; }
	}
}
