using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.AspNet.OData.Query;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData.Builder
{
	// Token: 0x02000122 RID: 290
	internal class EdmTypeMap
	{
		// Token: 0x060009F8 RID: 2552 RVA: 0x00028C67 File Offset: 0x00026E67
		public EdmTypeMap(Dictionary<Type, IEdmType> edmTypes, Dictionary<PropertyInfo, IEdmProperty> edmProperties, Dictionary<IEdmProperty, QueryableRestrictions> edmPropertiesRestrictions, Dictionary<IEdmProperty, ModelBoundQuerySettings> edmPropertiesQuerySettings, Dictionary<IEdmStructuredType, ModelBoundQuerySettings> edmStructuredTypeQuerySettings, Dictionary<Enum, IEdmEnumMember> enumMembers, Dictionary<IEdmStructuredType, PropertyInfo> openTypes)
		{
			this.EdmTypes = edmTypes;
			this.EdmProperties = edmProperties;
			this.EdmPropertiesRestrictions = edmPropertiesRestrictions;
			this.EdmPropertiesQuerySettings = edmPropertiesQuerySettings;
			this.EdmStructuredTypeQuerySettings = edmStructuredTypeQuerySettings;
			this.EnumMembers = enumMembers;
			this.OpenTypes = openTypes;
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x060009F9 RID: 2553 RVA: 0x00028CA4 File Offset: 0x00026EA4
		// (set) Token: 0x060009FA RID: 2554 RVA: 0x00028CAC File Offset: 0x00026EAC
		public Dictionary<Type, IEdmType> EdmTypes { get; private set; }

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x060009FB RID: 2555 RVA: 0x00028CB5 File Offset: 0x00026EB5
		// (set) Token: 0x060009FC RID: 2556 RVA: 0x00028CBD File Offset: 0x00026EBD
		public Dictionary<PropertyInfo, IEdmProperty> EdmProperties { get; private set; }

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x060009FD RID: 2557 RVA: 0x00028CC6 File Offset: 0x00026EC6
		// (set) Token: 0x060009FE RID: 2558 RVA: 0x00028CCE File Offset: 0x00026ECE
		public Dictionary<IEdmProperty, QueryableRestrictions> EdmPropertiesRestrictions { get; private set; }

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x060009FF RID: 2559 RVA: 0x00028CD7 File Offset: 0x00026ED7
		// (set) Token: 0x06000A00 RID: 2560 RVA: 0x00028CDF File Offset: 0x00026EDF
		public Dictionary<IEdmProperty, ModelBoundQuerySettings> EdmPropertiesQuerySettings { get; private set; }

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000A01 RID: 2561 RVA: 0x00028CE8 File Offset: 0x00026EE8
		// (set) Token: 0x06000A02 RID: 2562 RVA: 0x00028CF0 File Offset: 0x00026EF0
		public Dictionary<IEdmStructuredType, ModelBoundQuerySettings> EdmStructuredTypeQuerySettings { get; private set; }

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000A03 RID: 2563 RVA: 0x00028CF9 File Offset: 0x00026EF9
		// (set) Token: 0x06000A04 RID: 2564 RVA: 0x00028D01 File Offset: 0x00026F01
		public Dictionary<Enum, IEdmEnumMember> EnumMembers { get; private set; }

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000A05 RID: 2565 RVA: 0x00028D0A File Offset: 0x00026F0A
		// (set) Token: 0x06000A06 RID: 2566 RVA: 0x00028D12 File Offset: 0x00026F12
		public Dictionary<IEdmStructuredType, PropertyInfo> OpenTypes { get; private set; }
	}
}
