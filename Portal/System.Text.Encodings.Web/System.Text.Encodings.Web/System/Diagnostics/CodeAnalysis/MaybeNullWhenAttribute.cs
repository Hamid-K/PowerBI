using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000011 RID: 17
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	internal sealed class MaybeNullWhenAttribute : Attribute
	{
		// Token: 0x0600002C RID: 44 RVA: 0x00002674 File Offset: 0x00000874
		public MaybeNullWhenAttribute(bool returnValue)
		{
			this.ReturnValue = returnValue;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002683 File Offset: 0x00000883
		public bool ReturnValue { get; }
	}
}
