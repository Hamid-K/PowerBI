using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.Modeling.ModelGeneration
{
	// Token: 0x020000ED RID: 237
	public sealed class SyncModelItemIDsAlgorithm
	{
		// Token: 0x06000C1F RID: 3103 RVA: 0x00027C28 File Offset: 0x00025E28
		public static void SyncModelItemIDs(SemanticModel master, SemanticModel regen)
		{
			new SyncModelItemIDsAlgorithm(master, regen).SyncModelItemIDs();
		}

		// Token: 0x06000C20 RID: 3104 RVA: 0x00027C38 File Offset: 0x00025E38
		private SyncModelItemIDsAlgorithm(SemanticModel master, SemanticModel regen)
		{
			if (master.DataSourceView == null || regen.DataSourceView == null)
			{
				throw new InvalidOperationException();
			}
			this.m_master = master;
			this.m_regen = regen;
			IEqualityComparer<string> equalityComparer;
			if (regen.DataSourceView.CompareInfo == null)
			{
				IEqualityComparer<string> ordinal = StringComparer.Ordinal;
				equalityComparer = ordinal;
			}
			else
			{
				equalityComparer = regen.DataSourceView.CompareInfo.CreateComparer();
			}
			this.m_stringComparer = equalityComparer;
			this.m_columnBindingComparer = new SyncModelItemIDsAlgorithm.ColumnBindingComparer(this.m_stringComparer);
			this.m_relationBindingComparer = new SyncModelItemIDsAlgorithm.RelationBindingComparer(this.m_stringComparer);
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x00027CC0 File Offset: 0x00025EC0
		private void SyncModelItemIDs()
		{
			this.m_regen.ChangeID(this.m_master.ID);
			Dictionary<string, ModelEntity> dictionary = new Dictionary<string, ModelEntity>(this.m_stringComparer);
			Dictionary<ColumnBinding, ModelEntity> dictionary2 = new Dictionary<ColumnBinding, ModelEntity>(this.m_columnBindingComparer);
			List<SyncModelItemIDsAlgorithm.Master2RegenEntityPair> list = new List<SyncModelItemIDsAlgorithm.Master2RegenEntityPair>();
			foreach (ModelEntity modelEntity in this.m_master.GetAllEntities())
			{
				if (modelEntity.Binding is TableBinding)
				{
					dictionary.Add(modelEntity.Binding.Name, modelEntity);
				}
				else if (modelEntity.Binding is ColumnBinding)
				{
					dictionary2.Add((ColumnBinding)modelEntity.Binding, modelEntity);
				}
			}
			foreach (ModelEntity modelEntity2 in this.m_regen.GetAllEntities())
			{
				ModelEntity modelEntity3;
				if (modelEntity2.Binding is TableBinding)
				{
					if (dictionary.TryGetValue(modelEntity2.Binding.Name, out modelEntity3))
					{
						list.Add(new SyncModelItemIDsAlgorithm.Master2RegenEntityPair(modelEntity3, modelEntity2));
					}
				}
				else if (modelEntity2.Binding is ColumnBinding && dictionary2.TryGetValue((ColumnBinding)modelEntity2.Binding, out modelEntity3))
				{
					list.Add(new SyncModelItemIDsAlgorithm.Master2RegenEntityPair(modelEntity3, modelEntity2));
				}
			}
			foreach (SyncModelItemIDsAlgorithm.Master2RegenEntityPair master2RegenEntityPair in list)
			{
				this.SyncEntityDsvBoundContentIDs(master2RegenEntityPair.Master, master2RegenEntityPair.Regen);
			}
			foreach (SyncModelItemIDsAlgorithm.Master2RegenEntityPair master2RegenEntityPair2 in list)
			{
				this.SyncEntityExprAttributeIDs(master2RegenEntityPair2.Master, master2RegenEntityPair2.Regen);
			}
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x00027EC4 File Offset: 0x000260C4
		private void SyncEntityDsvBoundContentIDs(ModelEntity master, ModelEntity regen)
		{
			regen.ChangeID(master.ID);
			Dictionary<string, ModelAttribute> dictionary = new Dictionary<string, ModelAttribute>(this.m_stringComparer);
			Dictionary<RelationBinding, ModelRole> dictionary2 = new Dictionary<RelationBinding, ModelRole>(this.m_relationBindingComparer);
			Dictionary<string, ModelRole> dictionary3 = new Dictionary<string, ModelRole>(this.m_stringComparer);
			foreach (ModelField modelField in master.GetAllFields())
			{
				if (modelField is ModelAttribute)
				{
					ColumnBinding binding = ((ModelAttribute)modelField).Binding;
					if (binding != null)
					{
						dictionary.Add(binding.Name, (ModelAttribute)modelField);
					}
				}
				else if (modelField is ModelRole)
				{
					ModelRole modelRole = (ModelRole)modelField;
					if (modelRole.Binding != null)
					{
						dictionary2.Add(modelRole.Binding, modelRole);
					}
					else if (modelRole.RelatedEntity != null && modelRole.RelatedEntity.Binding != null)
					{
						dictionary3.Add(modelRole.RelatedEntity.Binding.Name, modelRole);
					}
				}
			}
			foreach (ModelField modelField2 in regen.GetAllFields())
			{
				if (modelField2 is ModelAttribute)
				{
					ColumnBinding binding2 = ((ModelAttribute)modelField2).Binding;
					ModelAttribute modelAttribute;
					if (binding2 != null && dictionary.TryGetValue(binding2.Name, out modelAttribute))
					{
						modelField2.ChangeID(modelAttribute.ID);
					}
				}
				else if (modelField2 is ModelRole)
				{
					ModelRole modelRole2 = (ModelRole)modelField2;
					ModelRole modelRole3;
					if (modelRole2.Binding != null)
					{
						if (dictionary2.TryGetValue(modelRole2.Binding, out modelRole3))
						{
							modelRole2.ChangeID(modelRole3.ID);
						}
					}
					else if (modelRole2.RelatedEntity != null && modelRole2.RelatedEntity.Binding != null && dictionary3.TryGetValue(modelRole2.RelatedEntity.Binding.Name, out modelRole3))
					{
						modelRole2.ChangeID(modelRole3.ID);
					}
				}
			}
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x000280C8 File Offset: 0x000262C8
		private void SyncEntityExprAttributeIDs(ModelEntity master, ModelEntity regen)
		{
			Dictionary<string, ModelAttribute> dictionary = this.SerializeExpressions(master.GetAllFields());
			foreach (KeyValuePair<string, ModelAttribute> keyValuePair in this.SerializeExpressions(regen.GetAllFields()))
			{
				ModelAttribute modelAttribute;
				if (dictionary.TryGetValue(keyValuePair.Key, out modelAttribute))
				{
					keyValuePair.Value.ChangeID(modelAttribute.ID);
				}
			}
		}

		// Token: 0x06000C24 RID: 3108 RVA: 0x0002814C File Offset: 0x0002634C
		private Dictionary<string, ModelAttribute> SerializeExpressions(IEnumerable<ModelField> attrs)
		{
			Dictionary<string, ModelAttribute> dictionary = new Dictionary<string, ModelAttribute>(StringComparer.Ordinal);
			foreach (ModelField modelField in attrs)
			{
				ModelAttribute modelAttribute = modelField as ModelAttribute;
				if (modelAttribute != null && modelAttribute.Binding == null && modelAttribute.Expression != null)
				{
					dictionary.Add(modelAttribute.Expression.ToString(), modelAttribute);
				}
			}
			return dictionary;
		}

		// Token: 0x04000506 RID: 1286
		private readonly SemanticModel m_master;

		// Token: 0x04000507 RID: 1287
		private readonly SemanticModel m_regen;

		// Token: 0x04000508 RID: 1288
		private readonly IEqualityComparer<string> m_stringComparer;

		// Token: 0x04000509 RID: 1289
		private readonly SyncModelItemIDsAlgorithm.ColumnBindingComparer m_columnBindingComparer;

		// Token: 0x0400050A RID: 1290
		private readonly SyncModelItemIDsAlgorithm.RelationBindingComparer m_relationBindingComparer;

		// Token: 0x020001CB RID: 459
		private struct Master2RegenEntityPair
		{
			// Token: 0x06001168 RID: 4456 RVA: 0x0003674A File Offset: 0x0003494A
			internal Master2RegenEntityPair(ModelEntity master, ModelEntity regen)
			{
				this.Master = master;
				this.Regen = regen;
			}

			// Token: 0x040007E2 RID: 2018
			internal readonly ModelEntity Master;

			// Token: 0x040007E3 RID: 2019
			internal readonly ModelEntity Regen;
		}

		// Token: 0x020001CC RID: 460
		private sealed class ColumnBindingComparer : IEqualityComparer<ColumnBinding>
		{
			// Token: 0x06001169 RID: 4457 RVA: 0x0003675A File Offset: 0x0003495A
			internal ColumnBindingComparer(IEqualityComparer<string> stringComparer)
			{
				this.m_stringComparer = stringComparer;
			}

			// Token: 0x0600116A RID: 4458 RVA: 0x00036769 File Offset: 0x00034969
			bool IEqualityComparer<ColumnBinding>.Equals(ColumnBinding x, ColumnBinding y)
			{
				return this.m_stringComparer.Equals(x.Name, y.Name) && this.m_stringComparer.Equals(x.TableName, y.TableName);
			}

			// Token: 0x0600116B RID: 4459 RVA: 0x0003679D File Offset: 0x0003499D
			int IEqualityComparer<ColumnBinding>.GetHashCode(ColumnBinding obj)
			{
				return this.m_stringComparer.GetHashCode(obj.Name);
			}

			// Token: 0x040007E4 RID: 2020
			private readonly IEqualityComparer<string> m_stringComparer;
		}

		// Token: 0x020001CD RID: 461
		private sealed class RelationBindingComparer : IEqualityComparer<RelationBinding>
		{
			// Token: 0x0600116C RID: 4460 RVA: 0x000367B0 File Offset: 0x000349B0
			internal RelationBindingComparer(IEqualityComparer<string> stringComparer)
			{
				this.m_stringComparer = stringComparer;
			}

			// Token: 0x0600116D RID: 4461 RVA: 0x000367BF File Offset: 0x000349BF
			bool IEqualityComparer<RelationBinding>.Equals(RelationBinding x, RelationBinding y)
			{
				return this.m_stringComparer.Equals(x.Name, y.Name) && x.RelationEnd == y.RelationEnd;
			}

			// Token: 0x0600116E RID: 4462 RVA: 0x000367EA File Offset: 0x000349EA
			int IEqualityComparer<RelationBinding>.GetHashCode(RelationBinding obj)
			{
				return this.m_stringComparer.GetHashCode(obj.Name);
			}

			// Token: 0x040007E5 RID: 2021
			private readonly IEqualityComparer<string> m_stringComparer;
		}
	}
}
