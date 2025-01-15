using System;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData.Builder
{
	// Token: 0x0200011D RID: 285
	internal class NullableEnumTypeConfiguration : IEdmTypeConfiguration
	{
		// Token: 0x060009C8 RID: 2504 RVA: 0x00028854 File Offset: 0x00026A54
		internal NullableEnumTypeConfiguration(EnumTypeConfiguration enumTypeConfiguration)
		{
			this.ClrType = typeof(Nullable<>).MakeGenericType(new Type[] { enumTypeConfiguration.ClrType });
			this.FullName = enumTypeConfiguration.FullName;
			this.Namespace = enumTypeConfiguration.Namespace;
			this.Name = enumTypeConfiguration.Name;
			this.Kind = enumTypeConfiguration.Kind;
			this.ModelBuilder = enumTypeConfiguration.ModelBuilder;
			this.EnumTypeConfiguration = enumTypeConfiguration;
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x060009C9 RID: 2505 RVA: 0x000288CE File Offset: 0x00026ACE
		// (set) Token: 0x060009CA RID: 2506 RVA: 0x000288D6 File Offset: 0x00026AD6
		public Type ClrType { get; private set; }

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x060009CB RID: 2507 RVA: 0x000288DF File Offset: 0x00026ADF
		// (set) Token: 0x060009CC RID: 2508 RVA: 0x000288E7 File Offset: 0x00026AE7
		public string FullName { get; private set; }

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x060009CD RID: 2509 RVA: 0x000288F0 File Offset: 0x00026AF0
		// (set) Token: 0x060009CE RID: 2510 RVA: 0x000288F8 File Offset: 0x00026AF8
		public string Namespace { get; private set; }

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x060009CF RID: 2511 RVA: 0x00028901 File Offset: 0x00026B01
		// (set) Token: 0x060009D0 RID: 2512 RVA: 0x00028909 File Offset: 0x00026B09
		public string Name { get; private set; }

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x060009D1 RID: 2513 RVA: 0x00028912 File Offset: 0x00026B12
		// (set) Token: 0x060009D2 RID: 2514 RVA: 0x0002891A File Offset: 0x00026B1A
		public EdmTypeKind Kind { get; private set; }

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x060009D3 RID: 2515 RVA: 0x00028923 File Offset: 0x00026B23
		// (set) Token: 0x060009D4 RID: 2516 RVA: 0x0002892B File Offset: 0x00026B2B
		public ODataModelBuilder ModelBuilder { get; private set; }

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x060009D5 RID: 2517 RVA: 0x00028934 File Offset: 0x00026B34
		// (set) Token: 0x060009D6 RID: 2518 RVA: 0x0002893C File Offset: 0x00026B3C
		internal EnumTypeConfiguration EnumTypeConfiguration { get; private set; }
	}
}
