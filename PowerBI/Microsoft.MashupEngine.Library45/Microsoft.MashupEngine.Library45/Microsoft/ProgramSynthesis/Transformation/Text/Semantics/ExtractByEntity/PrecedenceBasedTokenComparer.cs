using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Utils.Interactive;
using Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Entities;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Semantics.ExtractByEntity
{
	// Token: 0x02001D74 RID: 7540
	public class PrecedenceBasedTokenComparer
	{
		// Token: 0x17002A43 RID: 10819
		// (get) Token: 0x0600FD9D RID: 64925 RVA: 0x00362FAB File Offset: 0x003611AB
		private Dictionary<EntityType, HashSet<EntityType>> HasPrecedenceOver
		{
			get
			{
				return this.HasPrecedenceOverLazy.Value;
			}
		}

		// Token: 0x0600FD9E RID: 64926 RVA: 0x00362FB8 File Offset: 0x003611B8
		private static Dictionary<EntityType, HashSet<EntityType>> BuildPrecedenceLists()
		{
			List<EntityType> list = Enum.GetValues(typeof(EntityType)).Cast<EntityType>().ToList<EntityType>();
			Dictionary<EntityType, HashSet<EntityType>> res = list.ToDictionary((EntityType t) => t, (EntityType t) => new HashSet<EntityType>());
			list.Where((EntityType t) => t != EntityType.Number && t > EntityType.Unknown).ForEach(delegate(EntityType t)
			{
				res[t].Add(EntityType.Number);
			});
			res[EntityType.Date].Add(EntityType.Path);
			res[EntityType.Url].Add(EntityType.Path);
			res[EntityType.Date].Add(EntityType.FileName);
			return res;
		}

		// Token: 0x0600FD9F RID: 64927 RVA: 0x003630A8 File Offset: 0x003612A8
		public int Compare(EntityToken x, EntityToken y)
		{
			if (x == y)
			{
				return 0;
			}
			if (x == null)
			{
				return -1;
			}
			if (y == null)
			{
				return 1;
			}
			EntityType entityType = EntityMappings.TypeToEntityType[x.GetType()];
			EntityType entityType2 = EntityMappings.TypeToEntityType[y.GetType()];
			if (this.HasPrecedenceOver[entityType].Contains(entityType2))
			{
				return 1;
			}
			if (this.HasPrecedenceOver[entityType2].Contains(entityType))
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x0600FDA0 RID: 64928 RVA: 0x00363114 File Offset: 0x00361314
		private PrecedenceBasedTokenComparer()
		{
		}

		// Token: 0x17002A44 RID: 10820
		// (get) Token: 0x0600FDA1 RID: 64929 RVA: 0x00363146 File Offset: 0x00361346
		public static PrecedenceBasedTokenComparer Instance { get; } = new PrecedenceBasedTokenComparer();

		// Token: 0x04005EDE RID: 24286
		private Lazy<Dictionary<EntityType, HashSet<EntityType>>> HasPrecedenceOverLazy = new Lazy<Dictionary<EntityType, HashSet<EntityType>>>(() => PrecedenceBasedTokenComparer.BuildPrecedenceLists());
	}
}
