using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000068 RID: 104
	public sealed class EdmNavigationPropertyInfo
	{
		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x0000B817 File Offset: 0x00009A17
		// (set) Token: 0x060003B4 RID: 948 RVA: 0x0000B81F File Offset: 0x00009A1F
		public string Name { get; set; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x0000B828 File Offset: 0x00009A28
		// (set) Token: 0x060003B6 RID: 950 RVA: 0x0000B830 File Offset: 0x00009A30
		public IEdmEntityType Target { get; set; }

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060003B7 RID: 951 RVA: 0x0000B839 File Offset: 0x00009A39
		// (set) Token: 0x060003B8 RID: 952 RVA: 0x0000B841 File Offset: 0x00009A41
		public EdmMultiplicity TargetMultiplicity { get; set; }

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060003B9 RID: 953 RVA: 0x0000B84A File Offset: 0x00009A4A
		// (set) Token: 0x060003BA RID: 954 RVA: 0x0000B852 File Offset: 0x00009A52
		public IEnumerable<IEdmStructuralProperty> DependentProperties { get; set; }

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060003BB RID: 955 RVA: 0x0000B85B File Offset: 0x00009A5B
		// (set) Token: 0x060003BC RID: 956 RVA: 0x0000B863 File Offset: 0x00009A63
		public IEnumerable<IEdmStructuralProperty> PrincipalProperties { get; set; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060003BD RID: 957 RVA: 0x0000B86C File Offset: 0x00009A6C
		// (set) Token: 0x060003BE RID: 958 RVA: 0x0000B874 File Offset: 0x00009A74
		public bool ContainsTarget { get; set; }

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060003BF RID: 959 RVA: 0x0000B87D File Offset: 0x00009A7D
		// (set) Token: 0x060003C0 RID: 960 RVA: 0x0000B885 File Offset: 0x00009A85
		public EdmOnDeleteAction OnDelete { get; set; }

		// Token: 0x060003C1 RID: 961 RVA: 0x0000B890 File Offset: 0x00009A90
		public EdmNavigationPropertyInfo Clone()
		{
			return new EdmNavigationPropertyInfo
			{
				Name = this.Name,
				Target = this.Target,
				TargetMultiplicity = this.TargetMultiplicity,
				DependentProperties = this.DependentProperties,
				PrincipalProperties = this.PrincipalProperties,
				ContainsTarget = this.ContainsTarget,
				OnDelete = this.OnDelete
			};
		}
	}
}
