using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000019 RID: 25
	[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
	public sealed class AsyncIteratorStateMachineAttribute : StateMachineAttribute
	{
		// Token: 0x06000030 RID: 48 RVA: 0x0000228A File Offset: 0x0000048A
		[NullableContext(1)]
		public AsyncIteratorStateMachineAttribute(Type stateMachineType)
			: base(stateMachineType)
		{
		}
	}
}
