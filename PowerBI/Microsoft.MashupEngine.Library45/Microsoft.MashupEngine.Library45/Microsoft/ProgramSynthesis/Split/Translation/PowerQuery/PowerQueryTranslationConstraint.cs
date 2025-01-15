using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Split.Text;
using Microsoft.ProgramSynthesis.Split.Text.Semantics;
using Microsoft.ProgramSynthesis.Translation.PowerQuery;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Split.Translation.PowerQuery
{
	// Token: 0x02001411 RID: 5137
	public class PowerQueryTranslationConstraint : TranslationConstraint
	{
		// Token: 0x06009E88 RID: 40584 RVA: 0x0021A008 File Offset: 0x00218208
		public PowerQueryTranslationConstraint(string inputStepName, string inputColumnName, ILocalizedPowerQueryMStrings localizedStrings, IEscapePowerQueryM escape, IEnumerable<string> forbiddenStepNames = null, IEnumerable<string> forbiddenColumnNames = null, bool removeOriginalColumn = true)
		{
			this._forbiddenStepNames = ((forbiddenStepNames != null) ? forbiddenStepNames.ConvertToHashSet<string>() : null);
			this._forbiddenColumnNames = ((forbiddenColumnNames != null) ? forbiddenColumnNames.ConvertToHashSet<string>() : null);
			this.InputStepName = inputStepName;
			this.InputColumnName = inputColumnName;
			this.LocalizedStrings = localizedStrings;
			this.Escape = escape;
			this.RemoveOriginalColumn = removeOriginalColumn;
		}

		// Token: 0x17001AE1 RID: 6881
		// (get) Token: 0x06009E89 RID: 40585 RVA: 0x0021A07A File Offset: 0x0021827A
		// (set) Token: 0x06009E8A RID: 40586 RVA: 0x0021A082 File Offset: 0x00218282
		public string InputColumnName { get; set; } = "input";

		// Token: 0x17001AE2 RID: 6882
		// (get) Token: 0x06009E8B RID: 40587 RVA: 0x0021A08B File Offset: 0x0021828B
		// (set) Token: 0x06009E8C RID: 40588 RVA: 0x0021A0A7 File Offset: 0x002182A7
		public string OutputColumnPrefix
		{
			get
			{
				if (!string.IsNullOrEmpty(this._outputColumnPrefix))
				{
					return this._outputColumnPrefix;
				}
				return this.InputColumnName;
			}
			set
			{
				this._outputColumnPrefix = value;
			}
		}

		// Token: 0x17001AE3 RID: 6883
		// (get) Token: 0x06009E8D RID: 40589 RVA: 0x0021A0B0 File Offset: 0x002182B0
		// (set) Token: 0x06009E8E RID: 40590 RVA: 0x0021A0B8 File Offset: 0x002182B8
		public bool RemoveOriginalColumn { get; set; } = true;

		// Token: 0x17001AE4 RID: 6884
		// (get) Token: 0x06009E8F RID: 40591 RVA: 0x0021A0C1 File Offset: 0x002182C1
		public IEnumerable<string> ForbiddenStepNames
		{
			get
			{
				return this._forbiddenStepNames;
			}
		}

		// Token: 0x17001AE5 RID: 6885
		// (get) Token: 0x06009E90 RID: 40592 RVA: 0x0021A0C9 File Offset: 0x002182C9
		public IEnumerable<string> ForbiddenColumnNames
		{
			get
			{
				return this._forbiddenColumnNames;
			}
		}

		// Token: 0x17001AE6 RID: 6886
		// (get) Token: 0x06009E91 RID: 40593 RVA: 0x0021A0D1 File Offset: 0x002182D1
		public string InputStepName { get; }

		// Token: 0x17001AE7 RID: 6887
		// (get) Token: 0x06009E92 RID: 40594 RVA: 0x0021A0D9 File Offset: 0x002182D9
		public ILocalizedPowerQueryMStrings LocalizedStrings { get; }

		// Token: 0x17001AE8 RID: 6888
		// (get) Token: 0x06009E93 RID: 40595 RVA: 0x0021A0E1 File Offset: 0x002182E1
		public IEscapePowerQueryM Escape { get; }

		// Token: 0x06009E94 RID: 40596 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override bool Valid(Program<StringRegion, SplitCell[]> program)
		{
			return true;
		}

		// Token: 0x06009E95 RID: 40597 RVA: 0x0021A0EC File Offset: 0x002182EC
		public override bool Equals(Constraint<StringRegion, SplitCell[]> other)
		{
			PowerQueryTranslationConstraint powerQueryTranslationConstraint = other as PowerQueryTranslationConstraint;
			return powerQueryTranslationConstraint != null && ((this._forbiddenStepNames == null) ? (powerQueryTranslationConstraint.ForbiddenStepNames == null || !powerQueryTranslationConstraint.ForbiddenStepNames.Any<string>()) : this._forbiddenStepNames.SetEquals(powerQueryTranslationConstraint.ForbiddenStepNames)) && ((this._forbiddenColumnNames == null) ? (powerQueryTranslationConstraint.ForbiddenColumnNames == null || !powerQueryTranslationConstraint.ForbiddenColumnNames.Any<string>()) : this._forbiddenColumnNames.SetEquals(powerQueryTranslationConstraint.ForbiddenColumnNames)) && this.InputColumnName == powerQueryTranslationConstraint.InputColumnName && this.OutputColumnPrefix == powerQueryTranslationConstraint.OutputColumnPrefix && this.RemoveOriginalColumn == powerQueryTranslationConstraint.RemoveOriginalColumn && this.InputStepName == powerQueryTranslationConstraint.InputStepName && this.LocalizedStrings == powerQueryTranslationConstraint.LocalizedStrings && this.Escape == powerQueryTranslationConstraint.Escape;
		}

		// Token: 0x06009E96 RID: 40598 RVA: 0x0021A1DC File Offset: 0x002183DC
		public override int GetHashCode()
		{
			int num = 17 * 17;
			IEnumerable<string> forbiddenStepNames = this.ForbiddenStepNames;
			int num2 = (num + ((forbiddenStepNames != null) ? forbiddenStepNames.OrderIndependentHashCode<string>() : 0)) * 17;
			IEnumerable<string> forbiddenColumnNames = this.ForbiddenColumnNames;
			return ((((((num2 + ((forbiddenColumnNames != null) ? forbiddenColumnNames.OrderIndependentHashCode<string>() : 0)) * 17 + this.InputColumnName.GetHashCode()) * 17 + this.OutputColumnPrefix.GetHashCode()) * 17 + this.RemoveOriginalColumn.GetHashCode()) * 17 + this.InputStepName.GetHashCode()) * 17 + this.LocalizedStrings.GetHashCode()) * 17 + this.Escape.GetHashCode();
		}

		// Token: 0x06009E97 RID: 40599 RVA: 0x0021A274 File Offset: 0x00218474
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"InputStepName=",
				this.InputStepName,
				" InputColumnName=",
				this.InputColumnName,
				" OutputColumnPrefix=",
				this.OutputColumnPrefix,
				string.Format(" RemoveOriginalColumn={0}", this.RemoveOriginalColumn),
				" ForbiddenStepNames=",
				(this.ForbiddenStepNames == null) ? "" : string.Join(", ", this.ForbiddenStepNames),
				" ForbiddenColumnNames=",
				(this.ForbiddenColumnNames == null) ? "" : string.Join(", ", this.ForbiddenColumnNames)
			});
		}

		// Token: 0x04004026 RID: 16422
		private string _outputColumnPrefix;

		// Token: 0x04004028 RID: 16424
		internal readonly HashSet<string> _forbiddenStepNames;

		// Token: 0x04004029 RID: 16425
		internal readonly HashSet<string> _forbiddenColumnNames;
	}
}
