using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x0200000D RID: 13
	internal class FromManagerUtils
	{
		// Token: 0x06000020 RID: 32 RVA: 0x0000260C File Offset: 0x0000080C
		internal static char GetFirstLetter(string name)
		{
			string text = name ?? string.Empty;
			if (text.Length > 0 && char.IsLetter(text[0]))
			{
				return char.ToLowerInvariant(text[0]);
			}
			return 'a';
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000264C File Offset: 0x0000084C
		internal static EntitySource FirstOrDefault(IEnumerable<EntitySource> sources, string entityName)
		{
			return sources.FirstOrDefault((EntitySource e) => e.Entity == entityName);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002678 File Offset: 0x00000878
		internal static EntitySource CreateEntitySource(IEnumerable<EntitySource> existingEntitySources, string entityName)
		{
			HashSet<string> hashSet = new HashSet<string>(existingEntitySources.Select((EntitySource es) => es.Name));
			char firstLetter = FromManagerUtils.GetFirstLetter(entityName);
			string text = StringUtil.MakeUniqueName(string.Empty + firstLetter.ToString(), hashSet);
			return new EntitySource
			{
				Entity = entityName,
				Name = text
			};
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000026E4 File Offset: 0x000008E4
		internal static string RewriteEntityName(string entityName, IConceptualSchema conceptualSchema)
		{
			IConceptualEntity conceptualEntity;
			if (conceptualSchema.TryGetEntityByEdmName(entityName, out conceptualEntity))
			{
				return conceptualEntity.Name;
			}
			return FromManagerUtils.StripEntityContainer(entityName);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000270C File Offset: 0x0000090C
		internal static string RewritePropertyName(EntitySource entitySource, string propertyName, IConceptualSchema conceptualSchema, out bool isMeasure)
		{
			IConceptualEntity conceptualEntity;
			IConceptualProperty conceptualProperty;
			if (conceptualSchema.TryGetEntity(entitySource.Entity, out conceptualEntity) && conceptualEntity.TryGetPropertyByEdmName(propertyName, out conceptualProperty))
			{
				isMeasure = conceptualProperty is IConceptualMeasure;
				return conceptualProperty.Name;
			}
			isMeasure = false;
			return propertyName;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000274C File Offset: 0x0000094C
		internal static string RewriteHierarchyName(EntitySource entitySource, string hierarchyName, IConceptualSchema conceptualSchema)
		{
			IConceptualEntity conceptualEntity;
			IConceptualHierarchy conceptualHierarchy;
			if (conceptualSchema.TryGetEntity(entitySource.Entity, out conceptualEntity) && conceptualEntity.TryGetHierarchyByEdmName(hierarchyName, out conceptualHierarchy))
			{
				return conceptualHierarchy.Name;
			}
			return hierarchyName;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000277C File Offset: 0x0000097C
		internal static string RewriteHierarchyLevelName(EntitySource entitySource, string hierarchyName, string hierarchyLevelName, IConceptualSchema conceptualSchema)
		{
			IConceptualEntity conceptualEntity;
			IConceptualHierarchy conceptualHierarchy;
			IConceptualHierarchyLevel conceptualHierarchyLevel;
			if (conceptualSchema.TryGetEntity(entitySource.Entity, out conceptualEntity) && conceptualEntity.TryGetHierarchyByEdmName(hierarchyName, out conceptualHierarchy) && conceptualHierarchy.TryGetLevelByEdmName(hierarchyLevelName, out conceptualHierarchyLevel))
			{
				return conceptualHierarchyLevel.Name;
			}
			return hierarchyLevelName;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000027B8 File Offset: 0x000009B8
		private static string StripEntityContainer(string name)
		{
			int num = name.IndexOf('.');
			if (num <= -1)
			{
				return name;
			}
			return name.Substring(num + 1);
		}
	}
}
