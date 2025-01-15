using System;
using System.Collections.Generic;

namespace System.Data.Entity.Infrastructure.Pluralization
{
	// Token: 0x02000269 RID: 617
	internal class BidirectionalDictionary<TFirst, TSecond>
	{
		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x06001F57 RID: 8023 RVA: 0x00056CBF File Offset: 0x00054EBF
		// (set) Token: 0x06001F58 RID: 8024 RVA: 0x00056CC7 File Offset: 0x00054EC7
		internal Dictionary<TFirst, TSecond> FirstToSecondDictionary { get; set; }

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x06001F59 RID: 8025 RVA: 0x00056CD0 File Offset: 0x00054ED0
		// (set) Token: 0x06001F5A RID: 8026 RVA: 0x00056CD8 File Offset: 0x00054ED8
		internal Dictionary<TSecond, TFirst> SecondToFirstDictionary { get; set; }

		// Token: 0x06001F5B RID: 8027 RVA: 0x00056CE1 File Offset: 0x00054EE1
		internal BidirectionalDictionary()
		{
			this.FirstToSecondDictionary = new Dictionary<TFirst, TSecond>();
			this.SecondToFirstDictionary = new Dictionary<TSecond, TFirst>();
		}

		// Token: 0x06001F5C RID: 8028 RVA: 0x00056D00 File Offset: 0x00054F00
		internal BidirectionalDictionary(Dictionary<TFirst, TSecond> firstToSecondDictionary)
			: this()
		{
			foreach (TFirst tfirst in firstToSecondDictionary.Keys)
			{
				this.AddValue(tfirst, firstToSecondDictionary[tfirst]);
			}
		}

		// Token: 0x06001F5D RID: 8029 RVA: 0x00056D60 File Offset: 0x00054F60
		internal virtual bool ExistsInFirst(TFirst value)
		{
			return this.FirstToSecondDictionary.ContainsKey(value);
		}

		// Token: 0x06001F5E RID: 8030 RVA: 0x00056D73 File Offset: 0x00054F73
		internal virtual bool ExistsInSecond(TSecond value)
		{
			return this.SecondToFirstDictionary.ContainsKey(value);
		}

		// Token: 0x06001F5F RID: 8031 RVA: 0x00056D88 File Offset: 0x00054F88
		internal virtual TSecond GetSecondValue(TFirst value)
		{
			if (this.ExistsInFirst(value))
			{
				return this.FirstToSecondDictionary[value];
			}
			return default(TSecond);
		}

		// Token: 0x06001F60 RID: 8032 RVA: 0x00056DB4 File Offset: 0x00054FB4
		internal virtual TFirst GetFirstValue(TSecond value)
		{
			if (this.ExistsInSecond(value))
			{
				return this.SecondToFirstDictionary[value];
			}
			return default(TFirst);
		}

		// Token: 0x06001F61 RID: 8033 RVA: 0x00056DE0 File Offset: 0x00054FE0
		internal void AddValue(TFirst firstValue, TSecond secondValue)
		{
			this.FirstToSecondDictionary.Add(firstValue, secondValue);
			if (!this.SecondToFirstDictionary.ContainsKey(secondValue))
			{
				this.SecondToFirstDictionary.Add(secondValue, firstValue);
			}
		}
	}
}
