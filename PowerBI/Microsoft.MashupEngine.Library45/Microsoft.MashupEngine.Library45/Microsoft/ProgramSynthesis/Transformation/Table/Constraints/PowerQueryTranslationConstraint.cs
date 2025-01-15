using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Transformation.Table.Translation.PowerQuery;
using Microsoft.ProgramSynthesis.Translation.PowerQuery;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Constraints
{
	// Token: 0x02001B5E RID: 7006
	public class PowerQueryTranslationConstraint : TranslationConstraint
	{
		// Token: 0x0600E5FC RID: 58876 RVA: 0x0030B6F0 File Offset: 0x003098F0
		public PowerQueryTranslationConstraint(string inputStepName, Microsoft.ProgramSynthesis.Transformation.Table.Translation.PowerQuery.ILocalizedPowerQueryMStrings localizedStrings, IEscapePowerQueryM escape, IReadOnlyList<string> forbiddenStepNames = null, IReadOnlyList<string> forbiddenColumnNames = null, bool combineMultiStepPrograms = true, bool removeOriginalColumn_Split = true)
		{
			this._forbiddenStepNames = ((forbiddenStepNames != null) ? forbiddenStepNames.ConvertToHashSet<string>() : null);
			this._forbiddenColumnNames = ((forbiddenColumnNames != null) ? forbiddenColumnNames.ConvertToHashSet<string>() : null);
			this.InputStepName = inputStepName;
			this.LocalizedStrings = localizedStrings;
			this.Escape = escape;
			this.CombineMultiStepPrograms = combineMultiStepPrograms;
			this.RemoveOriginalColumn_Split = removeOriginalColumn_Split;
		}

		// Token: 0x1700264F RID: 9807
		// (get) Token: 0x0600E5FD RID: 58877 RVA: 0x0030B757 File Offset: 0x00309957
		public string InputStepName { get; }

		// Token: 0x17002650 RID: 9808
		// (get) Token: 0x0600E5FE RID: 58878 RVA: 0x0030B75F File Offset: 0x0030995F
		public Microsoft.ProgramSynthesis.Transformation.Table.Translation.PowerQuery.ILocalizedPowerQueryMStrings LocalizedStrings { get; }

		// Token: 0x17002651 RID: 9809
		// (get) Token: 0x0600E5FF RID: 58879 RVA: 0x0030B767 File Offset: 0x00309967
		public IEscapePowerQueryM Escape { get; }

		// Token: 0x17002652 RID: 9810
		// (get) Token: 0x0600E600 RID: 58880 RVA: 0x0030B76F File Offset: 0x0030996F
		public IEnumerable<string> ForbiddenStepNames
		{
			get
			{
				return this._forbiddenStepNames;
			}
		}

		// Token: 0x17002653 RID: 9811
		// (get) Token: 0x0600E601 RID: 58881 RVA: 0x0030B777 File Offset: 0x00309977
		public IEnumerable<string> ForbiddenColumnNames
		{
			get
			{
				return this._forbiddenColumnNames;
			}
		}

		// Token: 0x17002654 RID: 9812
		// (get) Token: 0x0600E602 RID: 58882 RVA: 0x0030B77F File Offset: 0x0030997F
		public bool CombineMultiStepPrograms { get; }

		// Token: 0x17002655 RID: 9813
		// (get) Token: 0x0600E603 RID: 58883 RVA: 0x0030B787 File Offset: 0x00309987
		public bool RemoveOriginalColumn_Split { get; } = true;

		// Token: 0x0600E604 RID: 58884 RVA: 0x0030B790 File Offset: 0x00309990
		public override bool Equals(Constraint<ITable<object>, ITable<object>> other)
		{
			PowerQueryTranslationConstraint powerQueryTranslationConstraint = other as PowerQueryTranslationConstraint;
			return powerQueryTranslationConstraint != null && ((this._forbiddenStepNames == null) ? (powerQueryTranslationConstraint.ForbiddenStepNames == null || !powerQueryTranslationConstraint.ForbiddenStepNames.Any<string>()) : this._forbiddenStepNames.SetEquals(powerQueryTranslationConstraint.ForbiddenStepNames)) && ((this._forbiddenColumnNames == null) ? (powerQueryTranslationConstraint.ForbiddenColumnNames == null || !powerQueryTranslationConstraint.ForbiddenColumnNames.Any<string>()) : this._forbiddenColumnNames.SetEquals(powerQueryTranslationConstraint.ForbiddenColumnNames)) && this.InputStepName == powerQueryTranslationConstraint.InputStepName && this.LocalizedStrings == powerQueryTranslationConstraint.LocalizedStrings && this.Escape == powerQueryTranslationConstraint.Escape && this.CombineMultiStepPrograms == powerQueryTranslationConstraint.CombineMultiStepPrograms;
		}

		// Token: 0x0600E605 RID: 58885 RVA: 0x0030B858 File Offset: 0x00309A58
		public override int GetHashCode()
		{
			int num = 17 * 17;
			IEnumerable<string> forbiddenStepNames = this.ForbiddenStepNames;
			int num2 = (num + ((forbiddenStepNames != null) ? forbiddenStepNames.OrderIndependentHashCode<string>() : 0)) * 17;
			IEnumerable<string> forbiddenColumnNames = this.ForbiddenColumnNames;
			return ((((num2 + ((forbiddenColumnNames != null) ? forbiddenColumnNames.OrderIndependentHashCode<string>() : 0)) * 17 + this.InputStepName.GetHashCode()) * 17 + this.LocalizedStrings.GetHashCode()) * 17 + this.Escape.GetHashCode()) * 17 + this.CombineMultiStepPrograms.GetHashCode();
		}

		// Token: 0x0600E606 RID: 58886 RVA: 0x0030B8D4 File Offset: 0x00309AD4
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"InputStepName=",
				this.InputStepName,
				" ForbiddenStepNames=",
				(this.ForbiddenStepNames == null) ? "" : string.Join(", ", this.ForbiddenStepNames),
				" ForbiddenColumnNames=",
				(this.ForbiddenColumnNames == null) ? "" : string.Join(", ", this.ForbiddenColumnNames),
				string.Format(" CombineMultiStepPrograms={0}", this.CombineMultiStepPrograms)
			});
		}

		// Token: 0x04005755 RID: 22357
		internal readonly HashSet<string> _forbiddenStepNames;

		// Token: 0x04005756 RID: 22358
		internal readonly HashSet<string> _forbiddenColumnNames;
	}
}
