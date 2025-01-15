using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004B2 RID: 1202
	public abstract class BucketedAggregatesCollection<T> : IEnumerable<T>, IEnumerable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable where T : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06003C10 RID: 15376 RVA: 0x0010390F File Offset: 0x00101B0F
		public BucketedAggregatesCollection()
		{
			this.m_buckets = new List<AggregateBucket<T>>();
		}

		// Token: 0x06003C11 RID: 15377 RVA: 0x00103924 File Offset: 0x00101B24
		public AggregateBucket<T> GetOrCreateBucket(int level)
		{
			AggregateBucket<T> aggregateBucket = null;
			int num = 0;
			while (num < this.m_buckets.Count && aggregateBucket == null)
			{
				AggregateBucket<T> aggregateBucket2 = this.m_buckets[num];
				if (aggregateBucket2.Level == level)
				{
					aggregateBucket = aggregateBucket2;
				}
				else if (aggregateBucket2.Level > level)
				{
					aggregateBucket = this.CreateBucket(level);
					this.m_buckets.Insert(num, aggregateBucket);
				}
				num++;
			}
			if (aggregateBucket == null)
			{
				aggregateBucket = this.CreateBucket(level);
				this.m_buckets.Add(aggregateBucket);
			}
			return aggregateBucket;
		}

		// Token: 0x170019C3 RID: 6595
		// (get) Token: 0x06003C12 RID: 15378 RVA: 0x0010399C File Offset: 0x00101B9C
		// (set) Token: 0x06003C13 RID: 15379 RVA: 0x001039A4 File Offset: 0x00101BA4
		public List<AggregateBucket<T>> Buckets
		{
			get
			{
				return this.m_buckets;
			}
			set
			{
				this.m_buckets = value;
			}
		}

		// Token: 0x06003C14 RID: 15380 RVA: 0x001039B0 File Offset: 0x00101BB0
		public void MergeFrom(BucketedAggregatesCollection<T> otherCol)
		{
			if (otherCol == null)
			{
				return;
			}
			foreach (AggregateBucket<T> aggregateBucket in otherCol.Buckets)
			{
				this.GetOrCreateBucket(aggregateBucket.Level).Aggregates.AddRange(aggregateBucket.Aggregates);
			}
		}

		// Token: 0x170019C4 RID: 6596
		// (get) Token: 0x06003C15 RID: 15381 RVA: 0x00103A1C File Offset: 0x00101C1C
		public bool IsEmpty
		{
			get
			{
				return this.m_buckets.Count == 0;
			}
		}

		// Token: 0x06003C16 RID: 15382 RVA: 0x00103A2C File Offset: 0x00101C2C
		public IEnumerator<T> GetEnumerator()
		{
			foreach (AggregateBucket<T> aggregateBucket in this.m_buckets)
			{
				foreach (T t in aggregateBucket.Aggregates)
				{
					yield return t;
				}
				List<T>.Enumerator enumerator2 = default(List<T>.Enumerator);
			}
			List<AggregateBucket<T>>.Enumerator enumerator = default(List<AggregateBucket<T>>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06003C17 RID: 15383 RVA: 0x00103A3B File Offset: 0x00101C3B
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06003C18 RID: 15384
		protected abstract AggregateBucket<T> CreateBucket(int level);

		// Token: 0x06003C19 RID: 15385
		protected abstract Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetSpecificDeclaration();

		// Token: 0x06003C1A RID: 15386
		public abstract Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType();

		// Token: 0x06003C1B RID: 15387 RVA: 0x00103A44 File Offset: 0x00101C44
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(this.GetSpecificDeclaration());
			while (writer.NextMember())
			{
				if (writer.CurrentMember.MemberName == MemberName.Buckets)
				{
					writer.Write<AggregateBucket<T>>(this.m_buckets);
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x06003C1C RID: 15388 RVA: 0x00103A98 File Offset: 0x00101C98
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(this.GetSpecificDeclaration());
			while (reader.NextMember())
			{
				if (reader.CurrentMember.MemberName == MemberName.Buckets)
				{
					this.m_buckets = reader.ReadGenericListOfRIFObjects<AggregateBucket<T>>();
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x06003C1D RID: 15389 RVA: 0x00103AEA File Offset: 0x00101CEA
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false, "No references to resolve.");
		}

		// Token: 0x04001C50 RID: 7248
		private List<AggregateBucket<T>> m_buckets;
	}
}
