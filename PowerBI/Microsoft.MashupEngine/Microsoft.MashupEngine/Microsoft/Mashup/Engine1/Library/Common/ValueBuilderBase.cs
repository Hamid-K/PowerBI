using System;
using System.Collections.Generic;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x0200115F RID: 4447
	internal abstract class ValueBuilderBase
	{
		// Token: 0x06007478 RID: 29816 RVA: 0x0018FE2E File Offset: 0x0018E02E
		protected ValueBuilderBase(EnvironmentBase environment, QueryPlanBase queryPlan, int skip, int? take, bool hasCount)
		{
			this.Environment = environment;
			this.HasCount = hasCount;
			this.QueryPlan = queryPlan;
			this.Skip = skip;
			this.Take = take;
		}

		// Token: 0x1700205C RID: 8284
		// (get) Token: 0x06007479 RID: 29817 RVA: 0x0018FE5B File Offset: 0x0018E05B
		// (set) Token: 0x0600747A RID: 29818 RVA: 0x0018FE63 File Offset: 0x0018E063
		private protected EnvironmentBase Environment { protected get; private set; }

		// Token: 0x1700205D RID: 8285
		// (get) Token: 0x0600747B RID: 29819 RVA: 0x0018FE6C File Offset: 0x0018E06C
		// (set) Token: 0x0600747C RID: 29820 RVA: 0x0018FE74 File Offset: 0x0018E074
		private protected bool HasCount { protected get; private set; }

		// Token: 0x1700205E RID: 8286
		// (get) Token: 0x0600747D RID: 29821 RVA: 0x0018FE7D File Offset: 0x0018E07D
		// (set) Token: 0x0600747E RID: 29822 RVA: 0x0018FE85 File Offset: 0x0018E085
		private protected QueryPlanBase QueryPlan { protected get; private set; }

		// Token: 0x1700205F RID: 8287
		// (get) Token: 0x0600747F RID: 29823 RVA: 0x0018FE8E File Offset: 0x0018E08E
		// (set) Token: 0x06007480 RID: 29824 RVA: 0x0018FE96 File Offset: 0x0018E096
		internal int Skip { get; private set; }

		// Token: 0x17002060 RID: 8288
		// (get) Token: 0x06007481 RID: 29825 RVA: 0x0018FE9F File Offset: 0x0018E09F
		// (set) Token: 0x06007482 RID: 29826 RVA: 0x0018FEA7 File Offset: 0x0018E0A7
		internal int? Take { get; private set; }

		// Token: 0x17002061 RID: 8289
		// (get) Token: 0x06007483 RID: 29827 RVA: 0x0018FEB0 File Offset: 0x0018E0B0
		public TypeValue Type
		{
			get
			{
				if (this.HasCount)
				{
					return TypeValue.Number;
				}
				return this.QueryPlan.Type;
			}
		}

		// Token: 0x06007484 RID: 29828 RVA: 0x0018FECC File Offset: 0x0018E0CC
		private int ComputeNewSkip(int skipCountArg, out int? newTake)
		{
			skipCountArg = ((skipCountArg <= 0) ? 0 : skipCountArg);
			newTake = ((this.Take != null) ? new int?(Math.Max(0, this.Take.Value - skipCountArg)) : null);
			return this.Skip + skipCountArg;
		}

		// Token: 0x06007485 RID: 29829 RVA: 0x0018FF28 File Offset: 0x0018E128
		private int? ComputeNewTake(int takeCountArg)
		{
			takeCountArg = ((takeCountArg <= 0) ? 0 : takeCountArg);
			return new int?((this.Take != null) ? Math.Min(this.Take.Value, takeCountArg) : takeCountArg);
		}

		// Token: 0x06007486 RID: 29830
		public abstract long CreateCountOverEnumerator();

		// Token: 0x06007487 RID: 29831
		protected abstract ValueBuilderBase CreatePagingValueBuilder(Query originalQuery, int skipCount, int? takeCount);

		// Token: 0x06007488 RID: 29832
		public abstract IEnumerator<IValueReference> GetEnumerator();

		// Token: 0x06007489 RID: 29833 RVA: 0x000E6755 File Offset: 0x000E4955
		public virtual bool TryGetReader(out IPageReader reader)
		{
			reader = null;
			return false;
		}

		// Token: 0x0600748A RID: 29834
		public abstract Value GetSingleValue();

		// Token: 0x0600748B RID: 29835 RVA: 0x0018FF6C File Offset: 0x0018E16C
		public List<IValueReference> GetValues()
		{
			List<IValueReference> list2;
			using (IEnumerator<IValueReference> enumerator = this.GetEnumerator())
			{
				List<IValueReference> list = new List<IValueReference>();
				while (enumerator.MoveNext())
				{
					IValueReference valueReference = enumerator.Current;
					list.Add(valueReference);
				}
				list2 = list;
			}
			return list2;
		}

		// Token: 0x0600748C RID: 29836 RVA: 0x0018FFBC File Offset: 0x0018E1BC
		public bool TryApplySkip(Query originalQuery, Value count, out ValueBuilderBase newBuilder)
		{
			int num;
			if (!count.IsNumber || !count.AsNumber.TryGetInt32(out num))
			{
				newBuilder = null;
				return false;
			}
			int? num3;
			int num2 = this.ComputeNewSkip(num, out num3);
			ValueBuilderBase valueBuilderBase;
			if (num2 <= 0)
			{
				int? take = this.Take;
				int? num4 = num3;
				if ((take.GetValueOrDefault() == num4.GetValueOrDefault()) & (take != null == (num4 != null)))
				{
					valueBuilderBase = this;
					goto IL_0064;
				}
			}
			valueBuilderBase = this.CreatePagingValueBuilder(originalQuery, num2, num3);
			IL_0064:
			newBuilder = valueBuilderBase;
			return true;
		}

		// Token: 0x0600748D RID: 29837 RVA: 0x00190030 File Offset: 0x0018E230
		public bool TryApplyTake(Query originalQuery, Value count, out ValueBuilderBase newBuilder)
		{
			int num;
			if (!count.IsNumber || !count.AsNumber.TryGetInt32(out num))
			{
				newBuilder = null;
				return false;
			}
			int? num2 = this.ComputeNewTake(num);
			int? take = this.Take;
			int? num3 = num2;
			newBuilder = (((take.GetValueOrDefault() == num3.GetValueOrDefault()) & (take != null == (num3 != null))) ? this : this.CreatePagingValueBuilder(originalQuery, this.Skip, num2));
			return true;
		}
	}
}
