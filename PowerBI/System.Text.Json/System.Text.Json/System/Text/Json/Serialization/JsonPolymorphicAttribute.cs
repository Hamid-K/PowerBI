using System;
using System.Runtime.CompilerServices;

namespace System.Text.Json.Serialization
{
	// Token: 0x02000078 RID: 120
	[NullableContext(2)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false, Inherited = false)]
	public sealed class JsonPolymorphicAttribute : JsonAttribute
	{
		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000817 RID: 2071 RVA: 0x00024AA5 File Offset: 0x00022CA5
		// (set) Token: 0x06000818 RID: 2072 RVA: 0x00024AAD File Offset: 0x00022CAD
		public string TypeDiscriminatorPropertyName { get; set; }

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000819 RID: 2073 RVA: 0x00024AB6 File Offset: 0x00022CB6
		// (set) Token: 0x0600081A RID: 2074 RVA: 0x00024ABE File Offset: 0x00022CBE
		public JsonUnknownDerivedTypeHandling UnknownDerivedTypeHandling { get; set; }

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x0600081B RID: 2075 RVA: 0x00024AC7 File Offset: 0x00022CC7
		// (set) Token: 0x0600081C RID: 2076 RVA: 0x00024ACF File Offset: 0x00022CCF
		public bool IgnoreUnrecognizedTypeDiscriminators { get; set; }
	}
}
