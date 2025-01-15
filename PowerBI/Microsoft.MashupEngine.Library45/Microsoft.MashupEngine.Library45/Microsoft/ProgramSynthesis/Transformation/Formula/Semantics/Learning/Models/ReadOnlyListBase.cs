using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Models
{
	// Token: 0x020016DC RID: 5852
	public class ReadOnlyListBase<T> : IReadOnlyList<T>, IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x0600C32A RID: 49962 RVA: 0x002A081A File Offset: 0x0029EA1A
		public ReadOnlyListBase(IEnumerable<T> items)
		{
			this.Items = items.ToReadOnlyList<T>();
		}

		// Token: 0x17002137 RID: 8503
		public T this[int index]
		{
			get
			{
				return this.Items[index];
			}
			set
			{
				throw new InvalidOperationException("value");
			}
		}

		// Token: 0x17002138 RID: 8504
		// (get) Token: 0x0600C32D RID: 49965 RVA: 0x002A083C File Offset: 0x0029EA3C
		// (set) Token: 0x0600C32E RID: 49966 RVA: 0x002A0153 File Offset: 0x0029E353
		public int Count
		{
			get
			{
				return this.Items.Count;
			}
			set
			{
				throw new InvalidOperationException("value");
			}
		}

		// Token: 0x17002139 RID: 8505
		// (get) Token: 0x0600C32F RID: 49967 RVA: 0x002A0849 File Offset: 0x0029EA49
		protected IReadOnlyList<T> Items { get; }

		// Token: 0x0600C330 RID: 49968 RVA: 0x002A0851 File Offset: 0x0029EA51
		public IEnumerator<T> GetEnumerator()
		{
			return this.Items.GetEnumerator();
		}

		// Token: 0x0600C331 RID: 49969 RVA: 0x002A085E File Offset: 0x0029EA5E
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}
	}
}
