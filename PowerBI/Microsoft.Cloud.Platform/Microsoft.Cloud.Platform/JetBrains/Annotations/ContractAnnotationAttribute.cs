using System;

namespace JetBrains.Annotations
{
	// Token: 0x0200055E RID: 1374
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
	public sealed class ContractAnnotationAttribute : Attribute
	{
		// Token: 0x06002A24 RID: 10788 RVA: 0x000981CB File Offset: 0x000963CB
		public ContractAnnotationAttribute([NotNull] string fdt)
			: this(fdt, false)
		{
		}

		// Token: 0x06002A25 RID: 10789 RVA: 0x000981D5 File Offset: 0x000963D5
		public ContractAnnotationAttribute([NotNull] string fdt, bool forceFullStates)
		{
			this.FDT = fdt;
			this.ForceFullStates = forceFullStates;
		}

		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x06002A26 RID: 10790 RVA: 0x000981EB File Offset: 0x000963EB
		// (set) Token: 0x06002A27 RID: 10791 RVA: 0x000981F3 File Offset: 0x000963F3
		public string FDT { get; private set; }

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x06002A28 RID: 10792 RVA: 0x000981FC File Offset: 0x000963FC
		// (set) Token: 0x06002A29 RID: 10793 RVA: 0x00098204 File Offset: 0x00096404
		public bool ForceFullStates { get; private set; }
	}
}
