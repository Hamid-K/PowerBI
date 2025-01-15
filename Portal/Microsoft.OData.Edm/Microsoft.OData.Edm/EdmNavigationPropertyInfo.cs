using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000046 RID: 70
	public sealed class EdmNavigationPropertyInfo
	{
		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00004839 File Offset: 0x00002A39
		// (set) Token: 0x06000162 RID: 354 RVA: 0x00004841 File Offset: 0x00002A41
		public string Name { get; set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000163 RID: 355 RVA: 0x0000484A File Offset: 0x00002A4A
		// (set) Token: 0x06000164 RID: 356 RVA: 0x00004852 File Offset: 0x00002A52
		public IEdmEntityType Target { get; set; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000165 RID: 357 RVA: 0x0000485B File Offset: 0x00002A5B
		// (set) Token: 0x06000166 RID: 358 RVA: 0x00004863 File Offset: 0x00002A63
		public EdmMultiplicity TargetMultiplicity { get; set; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000167 RID: 359 RVA: 0x0000486C File Offset: 0x00002A6C
		// (set) Token: 0x06000168 RID: 360 RVA: 0x00004874 File Offset: 0x00002A74
		public IEnumerable<IEdmStructuralProperty> DependentProperties { get; set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000169 RID: 361 RVA: 0x0000487D File Offset: 0x00002A7D
		// (set) Token: 0x0600016A RID: 362 RVA: 0x00004885 File Offset: 0x00002A85
		public IEnumerable<IEdmStructuralProperty> PrincipalProperties { get; set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600016B RID: 363 RVA: 0x0000488E File Offset: 0x00002A8E
		// (set) Token: 0x0600016C RID: 364 RVA: 0x00004896 File Offset: 0x00002A96
		public bool ContainsTarget { get; set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600016D RID: 365 RVA: 0x0000489F File Offset: 0x00002A9F
		// (set) Token: 0x0600016E RID: 366 RVA: 0x000048A7 File Offset: 0x00002AA7
		public EdmOnDeleteAction OnDelete { get; set; }

		// Token: 0x0600016F RID: 367 RVA: 0x000048B0 File Offset: 0x00002AB0
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
