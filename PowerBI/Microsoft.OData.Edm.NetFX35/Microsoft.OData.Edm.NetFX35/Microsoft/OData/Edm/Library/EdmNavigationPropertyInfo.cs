using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x02000112 RID: 274
	public sealed class EdmNavigationPropertyInfo
	{
		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000569 RID: 1385 RVA: 0x0000DB6B File Offset: 0x0000BD6B
		// (set) Token: 0x0600056A RID: 1386 RVA: 0x0000DB73 File Offset: 0x0000BD73
		public string Name { get; set; }

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x0600056B RID: 1387 RVA: 0x0000DB7C File Offset: 0x0000BD7C
		// (set) Token: 0x0600056C RID: 1388 RVA: 0x0000DB84 File Offset: 0x0000BD84
		public IEdmEntityType Target { get; set; }

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x0600056D RID: 1389 RVA: 0x0000DB8D File Offset: 0x0000BD8D
		// (set) Token: 0x0600056E RID: 1390 RVA: 0x0000DB95 File Offset: 0x0000BD95
		public EdmMultiplicity TargetMultiplicity { get; set; }

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x0600056F RID: 1391 RVA: 0x0000DB9E File Offset: 0x0000BD9E
		// (set) Token: 0x06000570 RID: 1392 RVA: 0x0000DBA6 File Offset: 0x0000BDA6
		public IEnumerable<IEdmStructuralProperty> DependentProperties { get; set; }

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000571 RID: 1393 RVA: 0x0000DBAF File Offset: 0x0000BDAF
		// (set) Token: 0x06000572 RID: 1394 RVA: 0x0000DBB7 File Offset: 0x0000BDB7
		public IEnumerable<IEdmStructuralProperty> PrincipalProperties { get; set; }

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000573 RID: 1395 RVA: 0x0000DBC0 File Offset: 0x0000BDC0
		// (set) Token: 0x06000574 RID: 1396 RVA: 0x0000DBC8 File Offset: 0x0000BDC8
		public bool ContainsTarget { get; set; }

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000575 RID: 1397 RVA: 0x0000DBD1 File Offset: 0x0000BDD1
		// (set) Token: 0x06000576 RID: 1398 RVA: 0x0000DBD9 File Offset: 0x0000BDD9
		public EdmOnDeleteAction OnDelete { get; set; }

		// Token: 0x06000577 RID: 1399 RVA: 0x0000DBE4 File Offset: 0x0000BDE4
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
