using System;
using System.Collections.Generic;

namespace Microsoft.AnalysisServices.Tabular.Metadata
{
	// Token: 0x020001E0 RID: 480
	internal sealed class CompatibilityRestrictionSet
	{
		// Token: 0x06001C35 RID: 7221 RVA: 0x000C50C8 File Offset: 0x000C32C8
		public CompatibilityRestrictionSet(int level)
			: this(new int[3])
		{
			for (int i = 0; i < 3; i++)
			{
				this.restrictions[i] = level;
			}
		}

		// Token: 0x06001C36 RID: 7222 RVA: 0x000C50F8 File Offset: 0x000C32F8
		public CompatibilityRestrictionSet(CompatibilityMode mode)
			: this(new int[3])
		{
			int modeRestrictionIndex = CompatibilityRestrictionSet.GetModeRestrictionIndex(mode);
			for (int i = 0; i < 3; i++)
			{
				if (i == modeRestrictionIndex)
				{
					this.restrictions[i] = -1;
				}
				else
				{
					this.restrictions[i] = -2;
				}
			}
		}

		// Token: 0x06001C37 RID: 7223 RVA: 0x000C5140 File Offset: 0x000C3340
		public CompatibilityRestrictionSet(params KeyValuePair<CompatibilityMode, int>[] restrictions)
			: this(-2)
		{
			if (restrictions != null && restrictions.Length != 0)
			{
				for (int i = 0; i < restrictions.Length; i++)
				{
					this.restrictions[CompatibilityRestrictionSet.GetModeRestrictionIndex(restrictions[i].Key)] = restrictions[i].Value;
				}
			}
		}

		// Token: 0x06001C38 RID: 7224 RVA: 0x000C518E File Offset: 0x000C338E
		private CompatibilityRestrictionSet(int[] restrictions)
		{
			this.restrictions = restrictions;
		}

		// Token: 0x17000639 RID: 1593
		public int this[CompatibilityMode mode]
		{
			get
			{
				return this.restrictions[CompatibilityRestrictionSet.GetModeRestrictionIndex(mode)];
			}
		}

		// Token: 0x06001C3A RID: 7226 RVA: 0x000C51AC File Offset: 0x000C33AC
		public bool IsCompatible(CompatibilityMode mode, int level)
		{
			int num = this.restrictions[CompatibilityRestrictionSet.GetModeRestrictionIndex(mode)];
			return num != -2 && num <= level;
		}

		// Token: 0x06001C3B RID: 7227 RVA: 0x000C51D5 File Offset: 0x000C33D5
		public bool IsCompatible(CompatibilityMode mode)
		{
			return this.IsCompatible(mode, int.MaxValue);
		}

		// Token: 0x06001C3C RID: 7228 RVA: 0x000C51E4 File Offset: 0x000C33E4
		public RestrictionsComapreResult Compare(CompatibilityRestrictionSet set)
		{
			RestrictionsComapreResult restrictionsComapreResult = RestrictionsComapreResult.Equal;
			for (int i = 0; i < 3; i++)
			{
				RestrictionsComapreResult restrictionsComapreResult2 = CompatibilityRestrictionSet.CompareLevel(this.restrictions[i], set.restrictions[i]);
				if (restrictionsComapreResult2 != RestrictionsComapreResult.LessRestrictive)
				{
					if (restrictionsComapreResult2 == RestrictionsComapreResult.MoreRestrictive)
					{
						if (restrictionsComapreResult == RestrictionsComapreResult.LessRestrictive)
						{
							return RestrictionsComapreResult.Incomparable;
						}
						if (restrictionsComapreResult == RestrictionsComapreResult.Equal)
						{
							restrictionsComapreResult = RestrictionsComapreResult.MoreRestrictive;
						}
					}
				}
				else
				{
					if (restrictionsComapreResult == RestrictionsComapreResult.MoreRestrictive)
					{
						return RestrictionsComapreResult.Incomparable;
					}
					if (restrictionsComapreResult == RestrictionsComapreResult.Equal)
					{
						restrictionsComapreResult = RestrictionsComapreResult.LessRestrictive;
					}
				}
			}
			return restrictionsComapreResult;
		}

		// Token: 0x06001C3D RID: 7229 RVA: 0x000C523C File Offset: 0x000C343C
		public CompatibilityRestrictionSet Merge(CompatibilityRestrictionSet set)
		{
			int[] array = new int[3];
			bool flag = false;
			for (int i = 0; i < 3; i++)
			{
				if (CompatibilityRestrictionSet.MergeLevelDemand(this.restrictions[i], set.restrictions[i], out array[i]))
				{
					flag = true;
				}
			}
			if (!flag)
			{
				return this;
			}
			return new CompatibilityRestrictionSet(array);
		}

		// Token: 0x06001C3E RID: 7230 RVA: 0x000C5289 File Offset: 0x000C3489
		internal static int GetModeRestrictionIndex(CompatibilityMode mode)
		{
			switch (mode)
			{
			case CompatibilityMode.AnalysisServices:
				return 0;
			case CompatibilityMode.PowerBI:
				return 1;
			case CompatibilityMode.Excel:
				return 2;
			}
			throw new ArgumentOutOfRangeException();
		}

		// Token: 0x06001C3F RID: 7231 RVA: 0x000C52B0 File Offset: 0x000C34B0
		internal static CompatibilityMode GetModeByRestrictionIndex(int index)
		{
			switch (index)
			{
			case 0:
				return CompatibilityMode.AnalysisServices;
			case 1:
				return CompatibilityMode.PowerBI;
			case 2:
				return CompatibilityMode.Excel;
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x06001C40 RID: 7232 RVA: 0x000C52D1 File Offset: 0x000C34D1
		internal static bool IsUnbound(int level)
		{
			return level == -1 || level == 1200;
		}

		// Token: 0x06001C41 RID: 7233 RVA: 0x000C52E1 File Offset: 0x000C34E1
		internal static bool MergeLevelDemand(int currentLevel, int newLevel, out int result)
		{
			if (CompatibilityRestrictionSet.CompareLevel(newLevel, currentLevel) == RestrictionsComapreResult.MoreRestrictive)
			{
				result = newLevel;
				return true;
			}
			result = currentLevel;
			return false;
		}

		// Token: 0x06001C42 RID: 7234 RVA: 0x000C52F8 File Offset: 0x000C34F8
		internal static RestrictionsComapreResult CompareLevel(int level, int otherLevel)
		{
			if (level == -2)
			{
				if (otherLevel != -2)
				{
					return RestrictionsComapreResult.MoreRestrictive;
				}
				return RestrictionsComapreResult.Equal;
			}
			else if (CompatibilityRestrictionSet.IsUnbound(level))
			{
				if (!CompatibilityRestrictionSet.IsUnbound(otherLevel))
				{
					return RestrictionsComapreResult.LessRestrictive;
				}
				return RestrictionsComapreResult.Equal;
			}
			else
			{
				if (otherLevel == -2)
				{
					return RestrictionsComapreResult.LessRestrictive;
				}
				if (CompatibilityRestrictionSet.IsUnbound(otherLevel))
				{
					return RestrictionsComapreResult.MoreRestrictive;
				}
				if (level == otherLevel)
				{
					return RestrictionsComapreResult.Equal;
				}
				if (level <= otherLevel)
				{
					return RestrictionsComapreResult.LessRestrictive;
				}
				return RestrictionsComapreResult.MoreRestrictive;
			}
		}

		// Token: 0x04000665 RID: 1637
		internal const int TotalValidCompatibilityModes = 3;

		// Token: 0x04000666 RID: 1638
		public static readonly CompatibilityRestrictionSet Empty = new CompatibilityRestrictionSet(-1);

		// Token: 0x04000667 RID: 1639
		private readonly int[] restrictions;
	}
}
