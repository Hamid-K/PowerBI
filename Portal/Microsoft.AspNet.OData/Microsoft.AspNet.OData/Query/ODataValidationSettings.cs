using System;
using System.Collections.ObjectModel;
using Microsoft.AspNet.OData.Common;

namespace Microsoft.AspNet.OData.Query
{
	// Token: 0x020000CD RID: 205
	public class ODataValidationSettings
	{
		// Token: 0x1700026D RID: 621
		// (get) Token: 0x060006C1 RID: 1729 RVA: 0x000176B7 File Offset: 0x000158B7
		// (set) Token: 0x060006C2 RID: 1730 RVA: 0x000176BF File Offset: 0x000158BF
		public AllowedArithmeticOperators AllowedArithmeticOperators
		{
			get
			{
				return this._allowedArithmeticOperators;
			}
			set
			{
				if (value > AllowedArithmeticOperators.All || value < AllowedArithmeticOperators.None)
				{
					throw Error.InvalidEnumArgument("value", (int)value, typeof(AllowedArithmeticOperators));
				}
				this._allowedArithmeticOperators = value;
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x060006C3 RID: 1731 RVA: 0x000176E7 File Offset: 0x000158E7
		// (set) Token: 0x060006C4 RID: 1732 RVA: 0x000176EF File Offset: 0x000158EF
		public AllowedFunctions AllowedFunctions
		{
			get
			{
				return this._allowedFunctions;
			}
			set
			{
				if (value > AllowedFunctions.AllFunctions || value < AllowedFunctions.None)
				{
					throw Error.InvalidEnumArgument("value", (int)value, typeof(AllowedFunctions));
				}
				this._allowedFunctions = value;
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x060006C5 RID: 1733 RVA: 0x0001771A File Offset: 0x0001591A
		// (set) Token: 0x060006C6 RID: 1734 RVA: 0x00017722 File Offset: 0x00015922
		public AllowedLogicalOperators AllowedLogicalOperators
		{
			get
			{
				return this._allowedLogicalOperators;
			}
			set
			{
				if (value > AllowedLogicalOperators.All || value < AllowedLogicalOperators.None)
				{
					throw Error.InvalidEnumArgument("value", (int)value, typeof(AllowedLogicalOperators));
				}
				this._allowedLogicalOperators = value;
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x060006C7 RID: 1735 RVA: 0x0001774D File Offset: 0x0001594D
		public Collection<string> AllowedOrderByProperties
		{
			get
			{
				return this._allowedOrderByProperties;
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x060006C8 RID: 1736 RVA: 0x00017755 File Offset: 0x00015955
		// (set) Token: 0x060006C9 RID: 1737 RVA: 0x0001775D File Offset: 0x0001595D
		public AllowedQueryOptions AllowedQueryOptions
		{
			get
			{
				return this._allowedQueryParameters;
			}
			set
			{
				if (value > AllowedQueryOptions.All || value < AllowedQueryOptions.None)
				{
					throw Error.InvalidEnumArgument("value", (int)value, typeof(AllowedQueryOptions));
				}
				this._allowedQueryParameters = value;
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x060006CA RID: 1738 RVA: 0x00017788 File Offset: 0x00015988
		// (set) Token: 0x060006CB RID: 1739 RVA: 0x00017790 File Offset: 0x00015990
		public int MaxOrderByNodeCount
		{
			get
			{
				return this._maxOrderByNodeCount;
			}
			set
			{
				if (value < 1)
				{
					throw Error.ArgumentMustBeGreaterThanOrEqualTo("value", value, 1);
				}
				this._maxOrderByNodeCount = value;
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x060006CC RID: 1740 RVA: 0x000177B4 File Offset: 0x000159B4
		// (set) Token: 0x060006CD RID: 1741 RVA: 0x000177BC File Offset: 0x000159BC
		public int MaxAnyAllExpressionDepth
		{
			get
			{
				return this._maxAnyAllExpressionDepth;
			}
			set
			{
				if (value < 1)
				{
					throw Error.ArgumentMustBeGreaterThanOrEqualTo("value", value, 1);
				}
				this._maxAnyAllExpressionDepth = value;
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x060006CE RID: 1742 RVA: 0x000177E0 File Offset: 0x000159E0
		// (set) Token: 0x060006CF RID: 1743 RVA: 0x000177E8 File Offset: 0x000159E8
		public int MaxNodeCount
		{
			get
			{
				return this._maxNodeCount;
			}
			set
			{
				if (value < 1)
				{
					throw Error.ArgumentMustBeGreaterThanOrEqualTo("value", value, 1);
				}
				this._maxNodeCount = value;
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x060006D0 RID: 1744 RVA: 0x0001780C File Offset: 0x00015A0C
		// (set) Token: 0x060006D1 RID: 1745 RVA: 0x00017814 File Offset: 0x00015A14
		public int? MaxSkip
		{
			get
			{
				return this._maxSkip;
			}
			set
			{
				if (value != null)
				{
					int? num = value;
					int num2 = 0;
					if ((num.GetValueOrDefault() < num2) & (num != null))
					{
						throw Error.ArgumentMustBeGreaterThanOrEqualTo("value", value, 0);
					}
				}
				this._maxSkip = value;
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x060006D2 RID: 1746 RVA: 0x00017860 File Offset: 0x00015A60
		// (set) Token: 0x060006D3 RID: 1747 RVA: 0x00017868 File Offset: 0x00015A68
		public int? MaxTop
		{
			get
			{
				return this._maxTop;
			}
			set
			{
				if (value != null)
				{
					int? num = value;
					int num2 = 0;
					if ((num.GetValueOrDefault() < num2) & (num != null))
					{
						throw Error.ArgumentMustBeGreaterThanOrEqualTo("value", value, 0);
					}
				}
				this._maxTop = value;
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x060006D4 RID: 1748 RVA: 0x000178B4 File Offset: 0x00015AB4
		// (set) Token: 0x060006D5 RID: 1749 RVA: 0x000178BC File Offset: 0x00015ABC
		public int MaxExpansionDepth
		{
			get
			{
				return this._maxExpansionDepth;
			}
			set
			{
				if (value < 0)
				{
					throw Error.ArgumentMustBeGreaterThanOrEqualTo("value", value, 0);
				}
				this._maxExpansionDepth = value;
			}
		}

		// Token: 0x040001E5 RID: 485
		private const int MinMaxSkip = 0;

		// Token: 0x040001E6 RID: 486
		private const int MinMaxTop = 0;

		// Token: 0x040001E7 RID: 487
		private const int MinMaxExpansionDepth = 0;

		// Token: 0x040001E8 RID: 488
		private const int MinMaxNodeCount = 1;

		// Token: 0x040001E9 RID: 489
		private const int MinMaxAnyAllExpressionDepth = 1;

		// Token: 0x040001EA RID: 490
		private const int MinMaxOrderByNodeCount = 1;

		// Token: 0x040001EB RID: 491
		internal const int DefaultMaxExpansionDepth = 2;

		// Token: 0x040001EC RID: 492
		private AllowedArithmeticOperators _allowedArithmeticOperators = AllowedArithmeticOperators.All;

		// Token: 0x040001ED RID: 493
		private AllowedFunctions _allowedFunctions = AllowedFunctions.AllFunctions;

		// Token: 0x040001EE RID: 494
		private AllowedLogicalOperators _allowedLogicalOperators = AllowedLogicalOperators.All;

		// Token: 0x040001EF RID: 495
		private AllowedQueryOptions _allowedQueryParameters = AllowedQueryOptions.Supported;

		// Token: 0x040001F0 RID: 496
		private Collection<string> _allowedOrderByProperties = new Collection<string>();

		// Token: 0x040001F1 RID: 497
		private int? _maxSkip;

		// Token: 0x040001F2 RID: 498
		private int? _maxTop;

		// Token: 0x040001F3 RID: 499
		private int _maxAnyAllExpressionDepth = 1;

		// Token: 0x040001F4 RID: 500
		private int _maxNodeCount = 100;

		// Token: 0x040001F5 RID: 501
		private int _maxExpansionDepth = 2;

		// Token: 0x040001F6 RID: 502
		private int _maxOrderByNodeCount = 5;
	}
}
