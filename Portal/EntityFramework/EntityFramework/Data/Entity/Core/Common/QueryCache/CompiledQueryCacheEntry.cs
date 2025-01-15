using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core.Objects.Internal;

namespace System.Data.Entity.Core.Common.QueryCache
{
	// Token: 0x02000627 RID: 1575
	internal sealed class CompiledQueryCacheEntry : QueryCacheEntry
	{
		// Token: 0x06004C0F RID: 19471 RVA: 0x0010BA70 File Offset: 0x00109C70
		internal CompiledQueryCacheEntry(QueryCacheKey queryCacheKey, MergeOption? mergeOption)
			: base(queryCacheKey, null)
		{
			this.PropagatedMergeOption = mergeOption;
			this._plans = new ConcurrentDictionary<string, ObjectQueryExecutionPlan>();
		}

		// Token: 0x06004C10 RID: 19472 RVA: 0x0010BA8C File Offset: 0x00109C8C
		internal ObjectQueryExecutionPlan GetExecutionPlan(MergeOption mergeOption, bool useCSharpNullComparisonBehavior)
		{
			string text = CompiledQueryCacheEntry.GenerateLocalCacheKey(mergeOption, useCSharpNullComparisonBehavior);
			ObjectQueryExecutionPlan objectQueryExecutionPlan;
			this._plans.TryGetValue(text, out objectQueryExecutionPlan);
			return objectQueryExecutionPlan;
		}

		// Token: 0x06004C11 RID: 19473 RVA: 0x0010BAB4 File Offset: 0x00109CB4
		internal ObjectQueryExecutionPlan SetExecutionPlan(ObjectQueryExecutionPlan newPlan, bool useCSharpNullComparisonBehavior)
		{
			string text = CompiledQueryCacheEntry.GenerateLocalCacheKey(newPlan.MergeOption, useCSharpNullComparisonBehavior);
			return this._plans.GetOrAdd(text, newPlan);
		}

		// Token: 0x06004C12 RID: 19474 RVA: 0x0010BADC File Offset: 0x00109CDC
		internal bool TryGetResultType(out TypeUsage resultType)
		{
			using (IEnumerator<ObjectQueryExecutionPlan> enumerator = this._plans.Values.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					ObjectQueryExecutionPlan objectQueryExecutionPlan = enumerator.Current;
					resultType = objectQueryExecutionPlan.ResultType;
					return true;
				}
			}
			resultType = null;
			return false;
		}

		// Token: 0x06004C13 RID: 19475 RVA: 0x0010BB3C File Offset: 0x00109D3C
		internal override object GetTarget()
		{
			return this;
		}

		// Token: 0x06004C14 RID: 19476 RVA: 0x0010BB40 File Offset: 0x00109D40
		private static string GenerateLocalCacheKey(MergeOption mergeOption, bool useCSharpNullComparisonBehavior)
		{
			if (mergeOption <= MergeOption.NoTracking)
			{
				return string.Join("", new object[]
				{
					Enum.GetName(typeof(MergeOption), mergeOption),
					useCSharpNullComparisonBehavior
				});
			}
			throw new ArgumentOutOfRangeException("newPlan.MergeOption");
		}

		// Token: 0x04001A8A RID: 6794
		public readonly MergeOption? PropagatedMergeOption;

		// Token: 0x04001A8B RID: 6795
		private readonly ConcurrentDictionary<string, ObjectQueryExecutionPlan> _plans;
	}
}
