using System;
using Microsoft.ProgramSynthesis.Split.Text;
using Microsoft.ProgramSynthesis.Transformation.Formula.Translation;
using Microsoft.ProgramSynthesis.Translation;

namespace Microsoft.ProgramSynthesis.Split.Translation
{
	// Token: 0x02001404 RID: 5124
	public abstract class SplitTranslation : ITranslation<SplitProgram, FormulaExpression>, IEquatable<ITranslation<SplitProgram, FormulaExpression>>
	{
		// Token: 0x06009E3C RID: 40508 RVA: 0x00218DE1 File Offset: 0x00216FE1
		internal SplitTranslation(SplitProgram program, FormulaExpression expression, TranslationConstraint translationConstraint, Metadata metadata)
		{
			this.TranslatedExpression = expression;
			this.Program = program;
			this._translationConstraint = translationConstraint;
			this.Metadata = metadata;
		}

		// Token: 0x17001ACC RID: 6860
		// (get) Token: 0x06009E3D RID: 40509 RVA: 0x00218E06 File Offset: 0x00217006
		public SplitProgram Program { get; }

		// Token: 0x17001ACD RID: 6861
		// (get) Token: 0x06009E3E RID: 40510
		public abstract TargetLanguage Target { get; }

		// Token: 0x17001ACE RID: 6862
		// (get) Token: 0x06009E3F RID: 40511 RVA: 0x00218E0E File Offset: 0x0021700E
		public FormulaExpression TranslatedExpression { get; }

		// Token: 0x17001ACF RID: 6863
		// (get) Token: 0x06009E40 RID: 40512 RVA: 0x00218E16 File Offset: 0x00217016
		public Metadata Metadata { get; }

		// Token: 0x06009E41 RID: 40513 RVA: 0x00218E20 File Offset: 0x00217020
		public override string ToString()
		{
			string text;
			if ((text = this._codeString) == null)
			{
				text = (this._codeString = this.ToCodeString());
			}
			return text;
		}

		// Token: 0x06009E42 RID: 40514 RVA: 0x00218E46 File Offset: 0x00217046
		protected virtual string ToCodeString()
		{
			FormulaExpression translatedExpression = this.TranslatedExpression;
			if (translatedExpression == null)
			{
				return null;
			}
			return translatedExpression.ToString();
		}

		// Token: 0x06009E43 RID: 40515 RVA: 0x00218E59 File Offset: 0x00217059
		public override bool Equals(object other)
		{
			return this.Equals(other as ITranslation<SplitProgram, FormulaExpression>);
		}

		// Token: 0x06009E44 RID: 40516 RVA: 0x00218E67 File Offset: 0x00217067
		public bool Equals(ITranslation<SplitProgram, FormulaExpression> other)
		{
			return other != null && this.ToString() == other.ToString();
		}

		// Token: 0x06009E45 RID: 40517 RVA: 0x00218E7F File Offset: 0x0021707F
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		// Token: 0x06009E46 RID: 40518 RVA: 0x00218E8C File Offset: 0x0021708C
		public static bool operator ==(ITranslation<SplitProgram, FormulaExpression> left, SplitTranslation right)
		{
			return (left == null && right == null) || (left != null && left.Equals(right));
		}

		// Token: 0x06009E47 RID: 40519 RVA: 0x00218EA2 File Offset: 0x002170A2
		public static bool operator ==(SplitTranslation left, ITranslation<SplitProgram, FormulaExpression> right)
		{
			return (left == null && right == null) || (left != null && left.Equals(right));
		}

		// Token: 0x06009E48 RID: 40520 RVA: 0x00218EB8 File Offset: 0x002170B8
		public static bool operator !=(ITranslation<SplitProgram, FormulaExpression> left, SplitTranslation right)
		{
			return !(left == right);
		}

		// Token: 0x06009E49 RID: 40521 RVA: 0x00218EC4 File Offset: 0x002170C4
		public static bool operator !=(SplitTranslation left, ITranslation<SplitProgram, FormulaExpression> right)
		{
			return !(left == right);
		}

		// Token: 0x04004007 RID: 16391
		private readonly TranslationConstraint _translationConstraint;

		// Token: 0x04004008 RID: 16392
		private string _codeString;
	}
}
