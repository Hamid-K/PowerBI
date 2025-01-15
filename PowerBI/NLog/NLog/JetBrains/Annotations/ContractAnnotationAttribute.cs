using System;

namespace JetBrains.Annotations
{
	// Token: 0x020001C7 RID: 455
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
	internal sealed class ContractAnnotationAttribute : Attribute
	{
		// Token: 0x0600140C RID: 5132 RVA: 0x000366C9 File Offset: 0x000348C9
		public ContractAnnotationAttribute([NotNull] string contract)
			: this(contract, false)
		{
		}

		// Token: 0x0600140D RID: 5133 RVA: 0x000366D3 File Offset: 0x000348D3
		public ContractAnnotationAttribute([NotNull] string contract, bool forceFullStates)
		{
			this.Contract = contract;
			this.ForceFullStates = forceFullStates;
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x0600140E RID: 5134 RVA: 0x000366E9 File Offset: 0x000348E9
		// (set) Token: 0x0600140F RID: 5135 RVA: 0x000366F1 File Offset: 0x000348F1
		[NotNull]
		public string Contract { get; private set; }

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06001410 RID: 5136 RVA: 0x000366FA File Offset: 0x000348FA
		// (set) Token: 0x06001411 RID: 5137 RVA: 0x00036702 File Offset: 0x00034902
		public bool ForceFullStates { get; private set; }
	}
}
