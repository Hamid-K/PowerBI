using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Extraction.Json.Learning;
using Microsoft.ProgramSynthesis.Extraction.Json.Learning.PowerQueryM;
using Microsoft.ProgramSynthesis.Translation;
using Microsoft.ProgramSynthesis.Translation.PowerQuery;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Extraction.Json.Constraints
{
	// Token: 0x02000BA9 RID: 2985
	public class TranslatePowerQueryM : Constraint<string, ITable<string>>, IOptionConstraint<SynthesisOptions>
	{
		// Token: 0x06004BE4 RID: 19428 RVA: 0x000EEEDD File Offset: 0x000ED0DD
		public TranslatePowerQueryM(ILocalizedPowerQueryMJsonStrings localizedStrings, IEscapePowerQueryM escapeIdentifiers, IEnumerable<string> forbiddenStepNames, string sourceStepName = null)
		{
			this._localizedStrings = localizedStrings;
			this._escapeIdentifiers = escapeIdentifiers;
			this._forbiddenStepNames = ((forbiddenStepNames != null) ? forbiddenStepNames.ConvertToHashSet<string>() : null);
			this._sourceStepName = sourceStepName;
		}

		// Token: 0x06004BE5 RID: 19429 RVA: 0x000EEF0D File Offset: 0x000ED10D
		public void SetOptions(SynthesisOptions options)
		{
			options.TranslationTargets.Add(TargetLanguage.PowerQueryM);
			options.LocalizedPowerQueryMStrings = this._localizedStrings;
			options.EscapePowerQueryM = this._escapeIdentifiers;
			options.ForbiddenMStepNames = this._forbiddenStepNames;
			options.SourceMStepName = this._sourceStepName;
		}

		// Token: 0x06004BE6 RID: 19430 RVA: 0x000EEF4C File Offset: 0x000ED14C
		public override bool Equals(Constraint<string, ITable<string>> other)
		{
			TranslatePowerQueryM translatePowerQueryM = other as TranslatePowerQueryM;
			return translatePowerQueryM != null && translatePowerQueryM._localizedStrings.Equals(this._localizedStrings) && translatePowerQueryM._escapeIdentifiers.Equals(this._escapeIdentifiers) && ((translatePowerQueryM._forbiddenStepNames == null && this._forbiddenStepNames == null) || (translatePowerQueryM._forbiddenStepNames != null && this._forbiddenStepNames != null && translatePowerQueryM._forbiddenStepNames.SetEquals(this._forbiddenStepNames) && object.Equals(translatePowerQueryM._sourceStepName, this._sourceStepName)));
		}

		// Token: 0x06004BE7 RID: 19431 RVA: 0x000EEFD3 File Offset: 0x000ED1D3
		public override bool ConflictsWith(Constraint<string, ITable<string>> other)
		{
			return other is TranslatePowerQueryM;
		}

		// Token: 0x06004BE8 RID: 19432 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override bool Valid(Program<string, ITable<string>> program)
		{
			return true;
		}

		// Token: 0x06004BE9 RID: 19433 RVA: 0x000EEFDE File Offset: 0x000ED1DE
		public override int GetHashCode()
		{
			int num = this._localizedStrings.GetHashCode() ^ this._escapeIdentifiers.GetHashCode();
			HashSet<string> forbiddenStepNames = this._forbiddenStepNames;
			int num2 = num ^ ((forbiddenStepNames != null) ? forbiddenStepNames.OrderIndependentHashCode<string>() : 0);
			string sourceStepName = this._sourceStepName;
			return num2 ^ ((sourceStepName != null) ? sourceStepName.GetHashCode() : 0);
		}

		// Token: 0x06004BEA RID: 19434 RVA: 0x000EF01D File Offset: 0x000ED21D
		public override bool Equals(object obj)
		{
			return object.Equals(this, obj as Constraint<string, ITable<string>>);
		}

		// Token: 0x0400220A RID: 8714
		private readonly ILocalizedPowerQueryMJsonStrings _localizedStrings;

		// Token: 0x0400220B RID: 8715
		private readonly IEscapePowerQueryM _escapeIdentifiers;

		// Token: 0x0400220C RID: 8716
		private readonly HashSet<string> _forbiddenStepNames;

		// Token: 0x0400220D RID: 8717
		private readonly string _sourceStepName;
	}
}
