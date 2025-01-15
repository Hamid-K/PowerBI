using System;
using System.Collections.Generic;

namespace Microsoft.Data.Edm.Library
{
	// Token: 0x020000D5 RID: 213
	public sealed class EdmNavigationPropertyInfo
	{
		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000442 RID: 1090 RVA: 0x0000BD5C File Offset: 0x00009F5C
		// (set) Token: 0x06000443 RID: 1091 RVA: 0x0000BD64 File Offset: 0x00009F64
		public string Name { get; set; }

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000444 RID: 1092 RVA: 0x0000BD6D File Offset: 0x00009F6D
		// (set) Token: 0x06000445 RID: 1093 RVA: 0x0000BD75 File Offset: 0x00009F75
		public IEdmEntityType Target { get; set; }

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000446 RID: 1094 RVA: 0x0000BD7E File Offset: 0x00009F7E
		// (set) Token: 0x06000447 RID: 1095 RVA: 0x0000BD86 File Offset: 0x00009F86
		public EdmMultiplicity TargetMultiplicity { get; set; }

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000448 RID: 1096 RVA: 0x0000BD8F File Offset: 0x00009F8F
		// (set) Token: 0x06000449 RID: 1097 RVA: 0x0000BD97 File Offset: 0x00009F97
		public IEnumerable<IEdmStructuralProperty> DependentProperties { get; set; }

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x0600044A RID: 1098 RVA: 0x0000BDA0 File Offset: 0x00009FA0
		// (set) Token: 0x0600044B RID: 1099 RVA: 0x0000BDA8 File Offset: 0x00009FA8
		public bool ContainsTarget { get; set; }

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x0600044C RID: 1100 RVA: 0x0000BDB1 File Offset: 0x00009FB1
		// (set) Token: 0x0600044D RID: 1101 RVA: 0x0000BDB9 File Offset: 0x00009FB9
		public EdmOnDeleteAction OnDelete { get; set; }

		// Token: 0x0600044E RID: 1102 RVA: 0x0000BDC4 File Offset: 0x00009FC4
		public EdmNavigationPropertyInfo Clone()
		{
			return new EdmNavigationPropertyInfo
			{
				Name = this.Name,
				Target = this.Target,
				TargetMultiplicity = this.TargetMultiplicity,
				DependentProperties = this.DependentProperties,
				ContainsTarget = this.ContainsTarget,
				OnDelete = this.OnDelete
			};
		}
	}
}
