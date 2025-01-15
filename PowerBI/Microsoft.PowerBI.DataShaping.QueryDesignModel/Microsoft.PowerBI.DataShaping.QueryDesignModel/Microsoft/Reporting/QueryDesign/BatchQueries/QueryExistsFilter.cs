using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Microsoft.DataShaping.Common;
using Microsoft.InfoNav;
using Microsoft.Reporting.QueryDesign.Edm.Internal;

namespace Microsoft.Reporting.QueryDesign.BatchQueries
{
	// Token: 0x0200026C RID: 620
	internal sealed class QueryExistsFilter : IEquatable<QueryExistsFilter>
	{
		// Token: 0x06001AC7 RID: 6855 RVA: 0x0004A962 File Offset: 0x00048B62
		internal QueryExistsFilter(IEnumerable<EntitySet> targetEntitySets, EntitySet existsEntitySet, IEnumerable<IConceptualEntity> targetEntities = null, IConceptualEntity existsEntity = null)
		{
			this._targetEntitySets = ((targetEntitySets != null) ? targetEntitySets.ToReadOnlyCollection<EntitySet>() : null);
			this._existsEntitySet = existsEntitySet;
			this._targetEntities = ((targetEntities != null) ? targetEntities.ToReadOnlyCollection<IConceptualEntity>() : null);
			this._existsEntity = existsEntity;
		}

		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x06001AC8 RID: 6856 RVA: 0x0004A99D File Offset: 0x00048B9D
		public ReadOnlyCollection<EntitySet> TargetEntitySets
		{
			get
			{
				return this._targetEntitySets;
			}
		}

		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x06001AC9 RID: 6857 RVA: 0x0004A9A5 File Offset: 0x00048BA5
		public IReadOnlyList<IConceptualEntity> TargetEntities
		{
			get
			{
				return this._targetEntities;
			}
		}

		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x06001ACA RID: 6858 RVA: 0x0004A9AD File Offset: 0x00048BAD
		public EntitySet ExistsEntitySet
		{
			get
			{
				return this._existsEntitySet;
			}
		}

		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x06001ACB RID: 6859 RVA: 0x0004A9B5 File Offset: 0x00048BB5
		public IConceptualEntity ExistsEntity
		{
			get
			{
				return this._existsEntity;
			}
		}

		// Token: 0x06001ACC RID: 6860 RVA: 0x0004A9C0 File Offset: 0x00048BC0
		public bool Equals(QueryExistsFilter other)
		{
			return other != null && (this.TargetEntitySets == null || this.TargetEntitySets.SetEquals(other.TargetEntitySets)) && object.Equals(this.ExistsEntitySet, other.ExistsEntitySet) && (this.TargetEntities == null || this.TargetEntities.SetEquals(other.TargetEntities)) && object.Equals(this.ExistsEntity, other.ExistsEntity);
		}

		// Token: 0x06001ACD RID: 6861 RVA: 0x0004AA2C File Offset: 0x00048C2C
		public override bool Equals(object obj)
		{
			return this.Equals(obj as QueryExistsFilter);
		}

		// Token: 0x06001ACE RID: 6862 RVA: 0x0004AA3A File Offset: 0x00048C3A
		public override int GetHashCode()
		{
			return Microsoft.DataShaping.Common.Hashing.CombineHash(Microsoft.DataShaping.Common.Hashing.GetHashCode<EntitySet>(this.ExistsEntitySet, null), Microsoft.DataShaping.Common.Hashing.GetHashCode<IConceptualEntity>(this.ExistsEntity, null), Microsoft.DataShaping.Common.Hashing.CombineHash<EntitySet>(this.TargetEntitySets, null), Microsoft.DataShaping.Common.Hashing.CombineHashReadonly<IConceptualEntity>(this.TargetEntities, null));
		}

		// Token: 0x06001ACF RID: 6863 RVA: 0x0004AA74 File Offset: 0x00048C74
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("QueryExistsFilter");
			StringBuilder stringBuilder2 = stringBuilder.Append("  Exists: ");
			IConceptualEntity existsEntity = this.ExistsEntity;
			stringBuilder2.Append(((existsEntity != null) ? existsEntity.GetFullName() : null) ?? this.ExistsEntitySet.FullName).AppendLine();
			stringBuilder.Append("  Targets:").AppendLine();
			if (this.TargetEntities != null)
			{
				using (IEnumerator<IConceptualEntity> enumerator = this.TargetEntities.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						IConceptualEntity conceptualEntity = enumerator.Current;
						stringBuilder.Append("    ").Append(conceptualEntity.GetFullName()).AppendLine();
					}
					goto IL_00F8;
				}
			}
			foreach (EntitySet entitySet in this.TargetEntitySets)
			{
				stringBuilder.Append("    ").Append(entitySet.FullName).AppendLine();
			}
			IL_00F8:
			return stringBuilder.ToString();
		}

		// Token: 0x04000ED8 RID: 3800
		private readonly ReadOnlyCollection<EntitySet> _targetEntitySets;

		// Token: 0x04000ED9 RID: 3801
		private readonly EntitySet _existsEntitySet;

		// Token: 0x04000EDA RID: 3802
		private readonly IReadOnlyList<IConceptualEntity> _targetEntities;

		// Token: 0x04000EDB RID: 3803
		private readonly IConceptualEntity _existsEntity;
	}
}
