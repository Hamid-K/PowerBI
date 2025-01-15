using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder.Internal
{
	// Token: 0x020006FB RID: 1787
	internal sealed class EnumerableValidator<TElementIn, TElementOut, TResult>
	{
		// Token: 0x0600545E RID: 21598 RVA: 0x0012F854 File Offset: 0x0012DA54
		internal EnumerableValidator(IEnumerable<TElementIn> argument, string argumentName)
		{
			this.argumentName = argumentName;
			this.target = argument;
		}

		// Token: 0x17001004 RID: 4100
		// (get) Token: 0x0600545F RID: 21599 RVA: 0x0012F871 File Offset: 0x0012DA71
		// (set) Token: 0x06005460 RID: 21600 RVA: 0x0012F879 File Offset: 0x0012DA79
		public bool AllowEmpty { get; set; }

		// Token: 0x17001005 RID: 4101
		// (get) Token: 0x06005461 RID: 21601 RVA: 0x0012F882 File Offset: 0x0012DA82
		// (set) Token: 0x06005462 RID: 21602 RVA: 0x0012F88A File Offset: 0x0012DA8A
		public int ExpectedElementCount
		{
			get
			{
				return this.expectedElementCount;
			}
			set
			{
				this.expectedElementCount = value;
			}
		}

		// Token: 0x17001006 RID: 4102
		// (get) Token: 0x06005463 RID: 21603 RVA: 0x0012F893 File Offset: 0x0012DA93
		// (set) Token: 0x06005464 RID: 21604 RVA: 0x0012F89B File Offset: 0x0012DA9B
		public Func<TElementIn, int, TElementOut> ConvertElement { get; set; }

		// Token: 0x17001007 RID: 4103
		// (get) Token: 0x06005465 RID: 21605 RVA: 0x0012F8A4 File Offset: 0x0012DAA4
		// (set) Token: 0x06005466 RID: 21606 RVA: 0x0012F8AC File Offset: 0x0012DAAC
		public Func<List<TElementOut>, TResult> CreateResult { get; set; }

		// Token: 0x17001008 RID: 4104
		// (get) Token: 0x06005467 RID: 21607 RVA: 0x0012F8B5 File Offset: 0x0012DAB5
		// (set) Token: 0x06005468 RID: 21608 RVA: 0x0012F8BD File Offset: 0x0012DABD
		public Func<TElementIn, int, string> GetName { get; set; }

		// Token: 0x06005469 RID: 21609 RVA: 0x0012F8C6 File Offset: 0x0012DAC6
		internal TResult Validate()
		{
			return EnumerableValidator<TElementIn, TElementOut, TResult>.Validate(this.target, this.argumentName, this.ExpectedElementCount, this.AllowEmpty, this.ConvertElement, this.CreateResult, this.GetName);
		}

		// Token: 0x0600546A RID: 21610 RVA: 0x0012F8F8 File Offset: 0x0012DAF8
		private static TResult Validate(IEnumerable<TElementIn> argument, string argumentName, int expectedElementCount, bool allowEmpty, Func<TElementIn, int, TElementOut> map, Func<List<TElementOut>, TResult> collect, Func<TElementIn, int, string> deriveName)
		{
			bool flag = default(TElementIn) == null;
			bool flag2 = expectedElementCount != -1;
			Dictionary<string, int> dictionary = null;
			if (deriveName != null)
			{
				dictionary = new Dictionary<string, int>();
			}
			int num = 0;
			List<TElementOut> list = new List<TElementOut>();
			foreach (TElementIn telementIn in argument)
			{
				if (flag2 && num == expectedElementCount)
				{
					throw new ArgumentException(Strings.Cqt_ExpressionList_IncorrectElementCount, argumentName);
				}
				if (flag && telementIn == null)
				{
					throw new ArgumentNullException(StringUtil.FormatIndex(argumentName, num));
				}
				TElementOut telementOut = map(telementIn, num);
				list.Add(telementOut);
				if (deriveName != null)
				{
					string text = deriveName(telementIn, num);
					int num2 = -1;
					if (dictionary.TryGetValue(text, out num2))
					{
						throw new ArgumentException(Strings.Cqt_Util_CheckListDuplicateName(num2, num, text), StringUtil.FormatIndex(argumentName, num));
					}
					dictionary[text] = num;
				}
				num++;
			}
			if (flag2)
			{
				if (num != expectedElementCount)
				{
					throw new ArgumentException(Strings.Cqt_ExpressionList_IncorrectElementCount, argumentName);
				}
			}
			else if (num == 0 && !allowEmpty)
			{
				throw new ArgumentException(Strings.Cqt_Util_CheckListEmptyInvalid, argumentName);
			}
			return collect(list);
		}

		// Token: 0x04001E04 RID: 7684
		private readonly string argumentName;

		// Token: 0x04001E05 RID: 7685
		private readonly IEnumerable<TElementIn> target;

		// Token: 0x04001E06 RID: 7686
		private int expectedElementCount = -1;
	}
}
