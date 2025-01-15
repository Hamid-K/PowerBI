using System;
using System.Collections;
using System.Data.Entity.Core.Metadata.Edm;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Core.Objects.DataClasses
{
	// Token: 0x0200047D RID: 1149
	public interface IRelatedEnd
	{
		// Token: 0x17000AC7 RID: 2759
		// (get) Token: 0x06003834 RID: 14388
		// (set) Token: 0x06003835 RID: 14389
		bool IsLoaded { get; set; }

		// Token: 0x17000AC8 RID: 2760
		// (get) Token: 0x06003836 RID: 14390
		string RelationshipName { get; }

		// Token: 0x17000AC9 RID: 2761
		// (get) Token: 0x06003837 RID: 14391
		string SourceRoleName { get; }

		// Token: 0x17000ACA RID: 2762
		// (get) Token: 0x06003838 RID: 14392
		string TargetRoleName { get; }

		// Token: 0x17000ACB RID: 2763
		// (get) Token: 0x06003839 RID: 14393
		RelationshipSet RelationshipSet { get; }

		// Token: 0x0600383A RID: 14394
		void Load();

		// Token: 0x0600383B RID: 14395
		Task LoadAsync(CancellationToken cancellationToken);

		// Token: 0x0600383C RID: 14396
		void Load(MergeOption mergeOption);

		// Token: 0x0600383D RID: 14397
		Task LoadAsync(MergeOption mergeOption, CancellationToken cancellationToken);

		// Token: 0x0600383E RID: 14398
		void Add(IEntityWithRelationships entity);

		// Token: 0x0600383F RID: 14399
		void Add(object entity);

		// Token: 0x06003840 RID: 14400
		bool Remove(IEntityWithRelationships entity);

		// Token: 0x06003841 RID: 14401
		bool Remove(object entity);

		// Token: 0x06003842 RID: 14402
		void Attach(IEntityWithRelationships entity);

		// Token: 0x06003843 RID: 14403
		void Attach(object entity);

		// Token: 0x06003844 RID: 14404
		IEnumerable CreateSourceQuery();

		// Token: 0x06003845 RID: 14405
		IEnumerator GetEnumerator();
	}
}
