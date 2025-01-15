using System;

namespace System.Web.Http.ModelBinding
{
	// Token: 0x0200005D RID: 93
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
	public abstract class CustomModelBinderAttribute : Attribute
	{
		// Token: 0x06000283 RID: 643
		public abstract IModelBinder GetBinder();

		// Token: 0x04000090 RID: 144
		internal const AttributeTargets ValidTargets = AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Parameter;
	}
}
