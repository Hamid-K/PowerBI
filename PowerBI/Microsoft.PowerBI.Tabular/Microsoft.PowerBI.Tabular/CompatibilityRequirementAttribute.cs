using System;
using System.Linq;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000EC RID: 236
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
	internal sealed class CompatibilityRequirementAttribute : Attribute
	{
		// Token: 0x06000F9B RID: 3995 RVA: 0x000771A4 File Offset: 0x000753A4
		public CompatibilityRequirementAttribute(string level)
		{
			this.box = (this.pbi = (this.excel = CompatibilityRequirementAttribute.NormalizeLevel(level)));
		}

		// Token: 0x06000F9C RID: 3996 RVA: 0x000771D8 File Offset: 0x000753D8
		public CompatibilityRequirementAttribute()
		{
			this.box = (this.pbi = (this.excel = "Unsupported"));
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06000F9D RID: 3997 RVA: 0x00077208 File Offset: 0x00075408
		// (set) Token: 0x06000F9E RID: 3998 RVA: 0x00077210 File Offset: 0x00075410
		public string Box
		{
			get
			{
				return this.box;
			}
			set
			{
				this.box = CompatibilityRequirementAttribute.NormalizeLevel(value);
			}
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06000F9F RID: 3999 RVA: 0x0007721E File Offset: 0x0007541E
		// (set) Token: 0x06000FA0 RID: 4000 RVA: 0x00077226 File Offset: 0x00075426
		public string Pbi
		{
			get
			{
				return this.pbi;
			}
			set
			{
				this.pbi = CompatibilityRequirementAttribute.NormalizeLevel(value);
			}
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06000FA1 RID: 4001 RVA: 0x00077234 File Offset: 0x00075434
		// (set) Token: 0x06000FA2 RID: 4002 RVA: 0x0007723C File Offset: 0x0007543C
		public string Excel
		{
			get
			{
				return this.excel;
			}
			set
			{
				this.excel = CompatibilityRequirementAttribute.NormalizeLevel(value);
			}
		}

		// Token: 0x06000FA3 RID: 4003 RVA: 0x0007724C File Offset: 0x0007544C
		private static string NormalizeLevel(string level)
		{
			if (string.IsNullOrEmpty(level) || !CompatibilityRequirementAttribute.IsValidCompatibilityLevel(level))
			{
				throw TomInternalException.Create("Invalid compatibility level - '{0}' is not a valid level!", new object[] { level });
			}
			int num;
			if (int.TryParse(level, out num))
			{
				if (num == 1000000)
				{
					level = "Preview";
				}
				else if (num == 2147483647)
				{
					level = "Internal";
				}
			}
			return level;
		}

		// Token: 0x06000FA4 RID: 4004 RVA: 0x000772AC File Offset: 0x000754AC
		private static bool IsValidCompatibilityLevel(string level)
		{
			int num;
			return string.Compare(level, "Preview", StringComparison.InvariantCultureIgnoreCase) == 0 || string.Compare(level, "Internal", StringComparison.InvariantCultureIgnoreCase) == 0 || (int.TryParse(level, out num) && (num == 1000000 || num == int.MaxValue || Constants.CompatibilityLevel.SupportedCompatibilityLevels.Contains(num)));
		}

		// Token: 0x040001DD RID: 477
		private const string INTERNAL_PREVIEW_LEVEL = "Internal";

		// Token: 0x040001DE RID: 478
		private const string PUBLIC_PREVIEW_LEVEL = "Preview";

		// Token: 0x040001DF RID: 479
		private const string UNSUPPORTED_LEVEL = "Unsupported";

		// Token: 0x040001E0 RID: 480
		private string box;

		// Token: 0x040001E1 RID: 481
		private string pbi;

		// Token: 0x040001E2 RID: 482
		private string excel;
	}
}
