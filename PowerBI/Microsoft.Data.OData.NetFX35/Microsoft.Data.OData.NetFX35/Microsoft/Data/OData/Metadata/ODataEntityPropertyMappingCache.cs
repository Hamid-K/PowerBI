using System;
using System.Collections.Generic;
using System.Data.Services.Common;
using System.Linq;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.Metadata
{
	// Token: 0x02000205 RID: 517
	internal sealed class ODataEntityPropertyMappingCache
	{
		// Token: 0x06000EFF RID: 3839 RVA: 0x00037134 File Offset: 0x00035334
		internal ODataEntityPropertyMappingCache(ODataEntityPropertyMappingCollection mappings, IEdmModel model, int totalMappingCount)
		{
			this.mappings = mappings;
			this.model = model;
			this.totalMappingCount = totalMappingCount;
			this.mappingsForInheritedProperties = new List<EntityPropertyMappingAttribute>();
			this.mappingsForDeclaredProperties = ((mappings == null) ? new List<EntityPropertyMappingAttribute>() : new List<EntityPropertyMappingAttribute>(mappings));
			this.epmTargetTree = new EpmTargetTree();
			this.epmSourceTree = new EpmSourceTree(this.epmTargetTree);
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000F00 RID: 3840 RVA: 0x00037199 File Offset: 0x00035399
		internal List<EntityPropertyMappingAttribute> MappingsForInheritedProperties
		{
			get
			{
				return this.mappingsForInheritedProperties;
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000F01 RID: 3841 RVA: 0x000371A1 File Offset: 0x000353A1
		internal List<EntityPropertyMappingAttribute> MappingsForDeclaredProperties
		{
			get
			{
				return this.mappingsForDeclaredProperties;
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000F02 RID: 3842 RVA: 0x000371A9 File Offset: 0x000353A9
		internal EpmSourceTree EpmSourceTree
		{
			get
			{
				return this.epmSourceTree;
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000F03 RID: 3843 RVA: 0x000371B1 File Offset: 0x000353B1
		internal EpmTargetTree EpmTargetTree
		{
			get
			{
				return this.epmTargetTree;
			}
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000F04 RID: 3844 RVA: 0x000371B9 File Offset: 0x000353B9
		internal IEnumerable<EntityPropertyMappingAttribute> AllMappings
		{
			get
			{
				return Enumerable.Concat<EntityPropertyMappingAttribute>(this.MappingsForDeclaredProperties, this.MappingsForInheritedProperties);
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000F05 RID: 3845 RVA: 0x000371CC File Offset: 0x000353CC
		internal int TotalMappingCount
		{
			get
			{
				return this.totalMappingCount;
			}
		}

		// Token: 0x06000F06 RID: 3846 RVA: 0x000371D4 File Offset: 0x000353D4
		internal void BuildEpmForType(IEdmEntityType definingEntityType, IEdmEntityType affectedEntityType)
		{
			if (definingEntityType.BaseType != null)
			{
				this.BuildEpmForType(definingEntityType.BaseEntityType(), affectedEntityType);
			}
			ODataEntityPropertyMappingCollection entityPropertyMappings = this.model.GetEntityPropertyMappings(definingEntityType);
			if (entityPropertyMappings == null)
			{
				return;
			}
			foreach (EntityPropertyMappingAttribute entityPropertyMappingAttribute in entityPropertyMappings)
			{
				this.epmSourceTree.Add(new EntityPropertyMappingInfo(entityPropertyMappingAttribute, definingEntityType, affectedEntityType));
				if (definingEntityType == affectedEntityType && !ODataEntityPropertyMappingCache.PropertyExistsOnType(affectedEntityType, entityPropertyMappingAttribute))
				{
					this.MappingsForInheritedProperties.Add(entityPropertyMappingAttribute);
					this.MappingsForDeclaredProperties.Remove(entityPropertyMappingAttribute);
				}
			}
		}

		// Token: 0x06000F07 RID: 3847 RVA: 0x00037274 File Offset: 0x00035474
		internal bool IsDirty(ODataEntityPropertyMappingCollection propertyMappings)
		{
			return (this.mappings != null || propertyMappings != null) && (!object.ReferenceEquals(this.mappings, propertyMappings) || this.mappings.Count != propertyMappings.Count);
		}

		// Token: 0x06000F08 RID: 3848 RVA: 0x000372C4 File Offset: 0x000354C4
		private static bool PropertyExistsOnType(IEdmStructuredType structuredType, EntityPropertyMappingAttribute epmAttribute)
		{
			int num = epmAttribute.SourcePath.IndexOf('/');
			string propertyToLookFor = ((num == -1) ? epmAttribute.SourcePath : epmAttribute.SourcePath.Substring(0, num));
			return Enumerable.Any<IEdmProperty>(structuredType.DeclaredProperties, (IEdmProperty p) => p.Name == propertyToLookFor);
		}

		// Token: 0x040005BB RID: 1467
		private readonly ODataEntityPropertyMappingCollection mappings;

		// Token: 0x040005BC RID: 1468
		private readonly List<EntityPropertyMappingAttribute> mappingsForInheritedProperties;

		// Token: 0x040005BD RID: 1469
		private readonly List<EntityPropertyMappingAttribute> mappingsForDeclaredProperties;

		// Token: 0x040005BE RID: 1470
		private readonly EpmSourceTree epmSourceTree;

		// Token: 0x040005BF RID: 1471
		private readonly EpmTargetTree epmTargetTree;

		// Token: 0x040005C0 RID: 1472
		private readonly IEdmModel model;

		// Token: 0x040005C1 RID: 1473
		private readonly int totalMappingCount;
	}
}
