using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.Identity.Json.Utilities
{
	// Token: 0x02000043 RID: 67
	internal class BidirectionalDictionary<TFirst, TSecond>
	{
		// Token: 0x06000432 RID: 1074 RVA: 0x00010480 File Offset: 0x0000E680
		public BidirectionalDictionary()
			: this(EqualityComparer<TFirst>.Default, EqualityComparer<TSecond>.Default)
		{
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x00010492 File Offset: 0x0000E692
		public BidirectionalDictionary(IEqualityComparer<TFirst> firstEqualityComparer, IEqualityComparer<TSecond> secondEqualityComparer)
			: this(firstEqualityComparer, secondEqualityComparer, "Duplicate item already exists for '{0}'.", "Duplicate item already exists for '{0}'.")
		{
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x000104A6 File Offset: 0x0000E6A6
		public BidirectionalDictionary(IEqualityComparer<TFirst> firstEqualityComparer, IEqualityComparer<TSecond> secondEqualityComparer, string duplicateFirstErrorMessage, string duplicateSecondErrorMessage)
		{
			this._firstToSecond = new Dictionary<TFirst, TSecond>(firstEqualityComparer);
			this._secondToFirst = new Dictionary<TSecond, TFirst>(secondEqualityComparer);
			this._duplicateFirstErrorMessage = duplicateFirstErrorMessage;
			this._duplicateSecondErrorMessage = duplicateSecondErrorMessage;
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x000104D8 File Offset: 0x0000E6D8
		public void Set(TFirst first, TSecond second)
		{
			TSecond tsecond;
			if (this._firstToSecond.TryGetValue(first, out tsecond) && !tsecond.Equals(second))
			{
				throw new ArgumentException(this._duplicateFirstErrorMessage.FormatWith(CultureInfo.InvariantCulture, first));
			}
			TFirst tfirst;
			if (this._secondToFirst.TryGetValue(second, out tfirst) && !tfirst.Equals(first))
			{
				throw new ArgumentException(this._duplicateSecondErrorMessage.FormatWith(CultureInfo.InvariantCulture, second));
			}
			this._firstToSecond.Add(first, second);
			this._secondToFirst.Add(second, first);
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x00010581 File Offset: 0x0000E781
		public bool TryGetByFirst(TFirst first, out TSecond second)
		{
			return this._firstToSecond.TryGetValue(first, out second);
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x00010590 File Offset: 0x0000E790
		public bool TryGetBySecond(TSecond second, out TFirst first)
		{
			return this._secondToFirst.TryGetValue(second, out first);
		}

		// Token: 0x04000155 RID: 341
		private readonly IDictionary<TFirst, TSecond> _firstToSecond;

		// Token: 0x04000156 RID: 342
		private readonly IDictionary<TSecond, TFirst> _secondToFirst;

		// Token: 0x04000157 RID: 343
		private readonly string _duplicateFirstErrorMessage;

		// Token: 0x04000158 RID: 344
		private readonly string _duplicateSecondErrorMessage;
	}
}
