using System;
using System.Text.RegularExpressions;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x0200028A RID: 650
	public sealed class MetricColumn
	{
		// Token: 0x06000F29 RID: 3881 RVA: 0x000533E4 File Offset: 0x000515E4
		public MetricColumn(string loadName, string name, MetricColumn.Objective target = MetricColumn.Objective.Maximize, bool canBeWeighted = true, bool isVector = false, Regex namePattern = null, string groupName = null, string nameFormat = null)
		{
			Contracts.CheckValue<string>(loadName, "loadName");
			Contracts.CheckValue<string>(name, "name");
			this.LoadName = loadName;
			this.Name = name;
			this.MetricTarget = target;
			this.CanBeWeighted = canBeWeighted;
			this.IsVector = isVector;
			this._loadNamePattern = namePattern;
			this._groupName = groupName;
			this._nameFormat = nameFormat;
		}

		// Token: 0x06000F2A RID: 3882 RVA: 0x0005344C File Offset: 0x0005164C
		public string GetNameMatch(string input)
		{
			if (this._loadNamePattern == null)
			{
				if (input.Equals(this.LoadName, StringComparison.OrdinalIgnoreCase) || (this.CanBeWeighted && input == "Weighted" + this.LoadName))
				{
					return this.Name;
				}
				return null;
			}
			else
			{
				if (string.IsNullOrEmpty(this._groupName) || string.IsNullOrEmpty(this._nameFormat))
				{
					return null;
				}
				Match match = this._loadNamePattern.Match(input);
				if (!match.Success)
				{
					return null;
				}
				Group group = match.Groups[this._groupName];
				return string.Format(this._nameFormat, group);
			}
		}

		// Token: 0x04000835 RID: 2101
		public readonly string LoadName;

		// Token: 0x04000836 RID: 2102
		public readonly bool IsVector;

		// Token: 0x04000837 RID: 2103
		public readonly MetricColumn.Objective MetricTarget;

		// Token: 0x04000838 RID: 2104
		public readonly string Name;

		// Token: 0x04000839 RID: 2105
		public readonly bool CanBeWeighted;

		// Token: 0x0400083A RID: 2106
		private readonly Regex _loadNamePattern;

		// Token: 0x0400083B RID: 2107
		private readonly string _groupName;

		// Token: 0x0400083C RID: 2108
		private readonly string _nameFormat;

		// Token: 0x0200028B RID: 651
		public enum Objective
		{
			// Token: 0x0400083E RID: 2110
			Maximize,
			// Token: 0x0400083F RID: 2111
			Minimize,
			// Token: 0x04000840 RID: 2112
			Info
		}
	}
}
