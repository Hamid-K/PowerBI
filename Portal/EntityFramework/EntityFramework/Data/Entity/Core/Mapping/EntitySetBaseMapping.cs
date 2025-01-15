using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x0200052B RID: 1323
	public abstract class EntitySetBaseMapping : MappingItem
	{
		// Token: 0x0600413F RID: 16703 RVA: 0x000DCCC1 File Offset: 0x000DAEC1
		internal EntitySetBaseMapping(EntityContainerMapping containerMapping)
		{
			this._containerMapping = containerMapping;
		}

		// Token: 0x17000CCF RID: 3279
		// (get) Token: 0x06004140 RID: 16704 RVA: 0x000DCCE0 File Offset: 0x000DAEE0
		public EntityContainerMapping ContainerMapping
		{
			get
			{
				return this._containerMapping;
			}
		}

		// Token: 0x17000CD0 RID: 3280
		// (get) Token: 0x06004141 RID: 16705 RVA: 0x000DCCE8 File Offset: 0x000DAEE8
		internal EntityContainerMapping EntityContainerMapping
		{
			get
			{
				return this.ContainerMapping;
			}
		}

		// Token: 0x17000CD1 RID: 3281
		// (get) Token: 0x06004142 RID: 16706 RVA: 0x000DCCF0 File Offset: 0x000DAEF0
		// (set) Token: 0x06004143 RID: 16707 RVA: 0x000DCCF8 File Offset: 0x000DAEF8
		public string QueryView
		{
			get
			{
				return this._queryView;
			}
			set
			{
				base.ThrowIfReadOnly();
				this._queryView = value;
			}
		}

		// Token: 0x17000CD2 RID: 3282
		// (get) Token: 0x06004144 RID: 16708
		internal abstract EntitySetBase Set { get; }

		// Token: 0x17000CD3 RID: 3283
		// (get) Token: 0x06004145 RID: 16709
		internal abstract IEnumerable<TypeMapping> TypeMappings { get; }

		// Token: 0x17000CD4 RID: 3284
		// (get) Token: 0x06004146 RID: 16710 RVA: 0x000DCD08 File Offset: 0x000DAF08
		internal virtual bool HasNoContent
		{
			get
			{
				if (this.QueryView != null)
				{
					return false;
				}
				foreach (TypeMapping typeMapping in this.TypeMappings)
				{
					using (IEnumerator<MappingFragment> enumerator2 = typeMapping.MappingFragments.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							if (enumerator2.Current.AllProperties.Any<PropertyMapping>())
							{
								return false;
							}
						}
					}
				}
				return true;
			}
		}

		// Token: 0x17000CD5 RID: 3285
		// (get) Token: 0x06004147 RID: 16711 RVA: 0x000DCDA0 File Offset: 0x000DAFA0
		// (set) Token: 0x06004148 RID: 16712 RVA: 0x000DCDA8 File Offset: 0x000DAFA8
		internal int StartLineNumber { get; set; }

		// Token: 0x17000CD6 RID: 3286
		// (get) Token: 0x06004149 RID: 16713 RVA: 0x000DCDB1 File Offset: 0x000DAFB1
		// (set) Token: 0x0600414A RID: 16714 RVA: 0x000DCDB9 File Offset: 0x000DAFB9
		internal int StartLinePosition { get; set; }

		// Token: 0x17000CD7 RID: 3287
		// (get) Token: 0x0600414B RID: 16715 RVA: 0x000DCDC2 File Offset: 0x000DAFC2
		// (set) Token: 0x0600414C RID: 16716 RVA: 0x000DCDCA File Offset: 0x000DAFCA
		internal bool HasModificationFunctionMapping { get; set; }

		// Token: 0x0600414D RID: 16717 RVA: 0x000DCDD3 File Offset: 0x000DAFD3
		internal bool ContainsTypeSpecificQueryView(Pair<EntitySetBase, Pair<EntityTypeBase, bool>> key)
		{
			return this._typeSpecificQueryViews.ContainsKey(key);
		}

		// Token: 0x0600414E RID: 16718 RVA: 0x000DCDE1 File Offset: 0x000DAFE1
		internal void AddTypeSpecificQueryView(Pair<EntitySetBase, Pair<EntityTypeBase, bool>> key, string viewString)
		{
			this._typeSpecificQueryViews.Add(key, viewString);
		}

		// Token: 0x0600414F RID: 16719 RVA: 0x000DCDF0 File Offset: 0x000DAFF0
		internal ReadOnlyCollection<Pair<EntitySetBase, Pair<EntityTypeBase, bool>>> GetTypeSpecificQVKeys()
		{
			return new ReadOnlyCollection<Pair<EntitySetBase, Pair<EntityTypeBase, bool>>>(this._typeSpecificQueryViews.Keys.ToList<Pair<EntitySetBase, Pair<EntityTypeBase, bool>>>());
		}

		// Token: 0x06004150 RID: 16720 RVA: 0x000DCE07 File Offset: 0x000DB007
		internal string GetTypeSpecificQueryView(Pair<EntitySetBase, Pair<EntityTypeBase, bool>> key)
		{
			return this._typeSpecificQueryViews[key];
		}

		// Token: 0x040016A2 RID: 5794
		private readonly EntityContainerMapping _containerMapping;

		// Token: 0x040016A3 RID: 5795
		private string _queryView;

		// Token: 0x040016A4 RID: 5796
		private readonly Dictionary<Pair<EntitySetBase, Pair<EntityTypeBase, bool>>, string> _typeSpecificQueryViews = new Dictionary<Pair<EntitySetBase, Pair<EntityTypeBase, bool>>, string>(Pair<EntitySetBase, Pair<EntityTypeBase, bool>>.PairComparer.Instance);
	}
}
