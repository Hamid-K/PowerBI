using System;
using Microsoft.ProgramSynthesis.Transformation.Formula.Translation;
using Microsoft.ProgramSynthesis.Transformation.Table.Constraints;
using Microsoft.ProgramSynthesis.Translation;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Translation
{
	// Token: 0x02001B32 RID: 6962
	public abstract class TransformationTableTranslation : ITranslation<Program, FormulaExpression>, IEquatable<ITranslation<Program, FormulaExpression>>
	{
		// Token: 0x0600E4B4 RID: 58548 RVA: 0x00307C0C File Offset: 0x00305E0C
		internal TransformationTableTranslation(Program program, FormulaExpression expression, TranslationConstraint translationConstraint, Metadata metadata)
		{
			this.TranslatedExpression = expression;
			this.Program = program;
			this.TranslationConstraint = translationConstraint;
			this.Metadata = metadata;
		}

		// Token: 0x17002617 RID: 9751
		// (get) Token: 0x0600E4B5 RID: 58549 RVA: 0x00307C31 File Offset: 0x00305E31
		public Program Program { get; }

		// Token: 0x17002618 RID: 9752
		// (get) Token: 0x0600E4B6 RID: 58550 RVA: 0x00307C39 File Offset: 0x00305E39
		public Metadata Metadata { get; }

		// Token: 0x17002619 RID: 9753
		// (get) Token: 0x0600E4B7 RID: 58551
		public abstract TargetLanguage Target { get; }

		// Token: 0x1700261A RID: 9754
		// (get) Token: 0x0600E4B8 RID: 58552 RVA: 0x00307C41 File Offset: 0x00305E41
		public FormulaExpression TranslatedExpression { get; }

		// Token: 0x1700261B RID: 9755
		// (get) Token: 0x0600E4B9 RID: 58553 RVA: 0x00307C49 File Offset: 0x00305E49
		public TranslationConstraint TranslationConstraint { get; }

		// Token: 0x0600E4BA RID: 58554 RVA: 0x00307C54 File Offset: 0x00305E54
		public override string ToString()
		{
			string text;
			if ((text = this._codeString) == null)
			{
				text = (this._codeString = this.ToCodeString());
			}
			return text;
		}

		// Token: 0x0600E4BB RID: 58555 RVA: 0x00307C7A File Offset: 0x00305E7A
		protected virtual string ToCodeString()
		{
			FormulaExpression translatedExpression = this.TranslatedExpression;
			if (translatedExpression == null)
			{
				return null;
			}
			return translatedExpression.ToString();
		}

		// Token: 0x0600E4BC RID: 58556 RVA: 0x00307C8D File Offset: 0x00305E8D
		public override bool Equals(object other)
		{
			return this.Equals(other as ITranslation<Program, FormulaExpression>);
		}

		// Token: 0x0600E4BD RID: 58557 RVA: 0x00307C9B File Offset: 0x00305E9B
		public bool Equals(ITranslation<Program, FormulaExpression> other)
		{
			return other != null && this.ToString() == other.ToString();
		}

		// Token: 0x0600E4BE RID: 58558 RVA: 0x00218E7F File Offset: 0x0021707F
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		// Token: 0x0600E4BF RID: 58559 RVA: 0x00218E8C File Offset: 0x0021708C
		public static bool operator ==(ITranslation<Program, FormulaExpression> left, TransformationTableTranslation right)
		{
			return (left == null && right == null) || (left != null && left.Equals(right));
		}

		// Token: 0x0600E4C0 RID: 58560 RVA: 0x00307CB3 File Offset: 0x00305EB3
		public static bool operator ==(TransformationTableTranslation left, ITranslation<Program, FormulaExpression> right)
		{
			return (left == null && right == null) || (left != null && left.Equals(right));
		}

		// Token: 0x0600E4C1 RID: 58561 RVA: 0x00307CC9 File Offset: 0x00305EC9
		public static bool operator !=(ITranslation<Program, FormulaExpression> left, TransformationTableTranslation right)
		{
			return !(left == right);
		}

		// Token: 0x0600E4C2 RID: 58562 RVA: 0x00307CD5 File Offset: 0x00305ED5
		public static bool operator !=(TransformationTableTranslation left, ITranslation<Program, FormulaExpression> right)
		{
			return !(left == right);
		}

		// Token: 0x040056C5 RID: 22213
		private string _codeString;
	}
}
