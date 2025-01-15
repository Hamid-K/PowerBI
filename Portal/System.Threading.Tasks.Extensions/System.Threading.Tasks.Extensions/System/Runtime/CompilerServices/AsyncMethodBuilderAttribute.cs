using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x0200000C RID: 12
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Delegate, Inherited = false, AllowMultiple = false)]
	public sealed class AsyncMethodBuilderAttribute : Attribute
	{
		// Token: 0x06000036 RID: 54 RVA: 0x000027B7 File Offset: 0x000009B7
		public AsyncMethodBuilderAttribute(Type builderType)
		{
			this.BuilderType = builderType;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000037 RID: 55 RVA: 0x000027C6 File Offset: 0x000009C6
		public Type BuilderType { get; }
	}
}
