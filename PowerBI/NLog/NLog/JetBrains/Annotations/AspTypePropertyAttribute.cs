using System;

namespace JetBrains.Annotations
{
	// Token: 0x020001FF RID: 511
	[AttributeUsage(AttributeTargets.Property)]
	internal sealed class AspTypePropertyAttribute : Attribute
	{
		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x0600148B RID: 5259 RVA: 0x00036BAB File Offset: 0x00034DAB
		// (set) Token: 0x0600148C RID: 5260 RVA: 0x00036BB3 File Offset: 0x00034DB3
		public bool CreateConstructorReferences { get; private set; }

		// Token: 0x0600148D RID: 5261 RVA: 0x00036BBC File Offset: 0x00034DBC
		public AspTypePropertyAttribute(bool createConstructorReferences)
		{
			this.CreateConstructorReferences = createConstructorReferences;
		}
	}
}
