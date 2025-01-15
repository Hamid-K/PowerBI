using System;
using System.Globalization;

namespace Microsoft.Fabric.Common
{
	// Token: 0x02000409 RID: 1033
	internal class ComparisonFunc : BinaryFunc
	{
		// Token: 0x06002402 RID: 9218 RVA: 0x0006E588 File Offset: 0x0006C788
		protected ComparisonFunc(ComparisonFunc.Option option)
		{
			this.m_option = option;
		}

		// Token: 0x06002403 RID: 9219 RVA: 0x0006E597 File Offset: 0x0006C797
		public bool Compare(int cmp)
		{
			return ((this.m_option & ComparisonFunc.Option.LessThan) != (ComparisonFunc.Option)0 && cmp < 0) || ((this.m_option & ComparisonFunc.Option.GreaterThan) != (ComparisonFunc.Option)0 && cmp > 0) || ((this.m_option & ComparisonFunc.Option.Equal) != (ComparisonFunc.Option)0 && cmp == 0);
		}

		// Token: 0x06002404 RID: 9220 RVA: 0x0006E5CC File Offset: 0x0006C7CC
		protected override object InvokeBinary(object arg1, object arg2)
		{
			int num = ComparisonFunc.Compare(arg1, arg2);
			return this.Compare(num);
		}

		// Token: 0x06002405 RID: 9221 RVA: 0x0006E5F0 File Offset: 0x0006C7F0
		private static int Compare(object arg1, object arg2)
		{
			if (arg1 == null)
			{
				if (arg2 != null)
				{
					return -1;
				}
				return 0;
			}
			else
			{
				if (arg2 == null)
				{
					return 1;
				}
				try
				{
					long num = Convert.ToInt64(arg1, CultureInfo.InvariantCulture);
					long num2 = Convert.ToInt64(arg2, CultureInfo.InvariantCulture);
					return num.CompareTo(num2);
				}
				catch (FormatException)
				{
				}
				catch (InvalidCastException)
				{
				}
				IComparable comparable = arg1 as IComparable;
				if (comparable != null)
				{
					try
					{
						return comparable.CompareTo(arg2);
					}
					catch (ArgumentException)
					{
					}
				}
				return string.Compare(arg1.ToString(), arg2.ToString(), StringComparison.Ordinal);
			}
		}

		// Token: 0x06002406 RID: 9222 RVA: 0x0006E688 File Offset: 0x0006C888
		public override PropertyFunc Bind(FuncArguments args)
		{
			if (args.Count == 2)
			{
				object obj;
				object obj2;
				if (args.GetLiteralArg<object>(0, out obj))
				{
					string text = obj as string;
					if (text != null)
					{
						args.RemoveAt(0);
						return new UnaryComparisonFunc(this, text);
					}
				}
				else if (args.GetLiteralArg<object>(1, out obj2))
				{
					string text2 = obj2 as string;
					if (text2 != null)
					{
						args.RemoveAt(1);
						return new UnaryComparisonFunc(this.Inverse(), text2);
					}
				}
			}
			return this;
		}

		// Token: 0x06002407 RID: 9223 RVA: 0x0006E6EC File Offset: 0x0006C8EC
		private ComparisonFunc Inverse()
		{
			if (this == ComparisonFunc.LT)
			{
				return ComparisonFunc.GT;
			}
			if (this == ComparisonFunc.GT)
			{
				return ComparisonFunc.LT;
			}
			if (this == ComparisonFunc.LE)
			{
				return ComparisonFunc.GE;
			}
			if (this == ComparisonFunc.GE)
			{
				return ComparisonFunc.LE;
			}
			return this;
		}

		// Token: 0x06002408 RID: 9224 RVA: 0x0006E728 File Offset: 0x0006C928
		public override string ToString()
		{
			if (this == ComparisonFunc.EQ)
			{
				return "eq";
			}
			if (this == ComparisonFunc.NE)
			{
				return "ne";
			}
			if (this == ComparisonFunc.GT)
			{
				return "gt";
			}
			if (this == ComparisonFunc.GE)
			{
				return "ge";
			}
			if (this == ComparisonFunc.LT)
			{
				return "lt";
			}
			return "le";
		}

		// Token: 0x04001640 RID: 5696
		private ComparisonFunc.Option m_option;

		// Token: 0x04001641 RID: 5697
		public static readonly ComparisonFunc EQ = new ComparisonFunc(ComparisonFunc.Option.Equal);

		// Token: 0x04001642 RID: 5698
		public static readonly ComparisonFunc NE = new ComparisonFunc(ComparisonFunc.Option.LessThan | ComparisonFunc.Option.GreaterThan);

		// Token: 0x04001643 RID: 5699
		public static readonly ComparisonFunc GT = new ComparisonFunc(ComparisonFunc.Option.GreaterThan);

		// Token: 0x04001644 RID: 5700
		public static readonly ComparisonFunc LT = new ComparisonFunc(ComparisonFunc.Option.LessThan);

		// Token: 0x04001645 RID: 5701
		public static readonly ComparisonFunc GE = new ComparisonFunc(ComparisonFunc.Option.Equal | ComparisonFunc.Option.GreaterThan);

		// Token: 0x04001646 RID: 5702
		public static readonly ComparisonFunc LE = new ComparisonFunc(ComparisonFunc.Option.LessThan | ComparisonFunc.Option.Equal);

		// Token: 0x0200040A RID: 1034
		[Flags]
		protected enum Option
		{
			// Token: 0x04001648 RID: 5704
			LessThan = 1,
			// Token: 0x04001649 RID: 5705
			Equal = 2,
			// Token: 0x0400164A RID: 5706
			GreaterThan = 4
		}
	}
}
