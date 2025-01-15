using System;
using Microsoft.ProgramSynthesis.Translation;
using Newtonsoft.Json.Linq;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation
{
	// Token: 0x02001804 RID: 6148
	public class FormulaTranslation : ITranslation<Program, FormulaExpression>, IEquatable<FormulaTranslation>, IEquatable<ITranslation<Program, FormulaExpression>>
	{
		// Token: 0x0600CA23 RID: 51747 RVA: 0x002B379E File Offset: 0x002B199E
		internal FormulaTranslation(Program program, FormulaExpression expression, TargetLanguage target, TranslationMeta meta)
		{
			this.Program = program;
			this._translatedExpression = expression;
			this.Target = target;
			this.Meta = meta;
		}

		// Token: 0x17002215 RID: 8725
		// (get) Token: 0x0600CA24 RID: 51748 RVA: 0x002B37C3 File Offset: 0x002B19C3
		public TranslationMeta Meta { get; }

		// Token: 0x17002216 RID: 8726
		// (get) Token: 0x0600CA25 RID: 51749 RVA: 0x002B37CB File Offset: 0x002B19CB
		public Program Program { get; }

		// Token: 0x17002217 RID: 8727
		// (get) Token: 0x0600CA26 RID: 51750 RVA: 0x002B37D3 File Offset: 0x002B19D3
		public virtual TargetLanguage Target { get; }

		// Token: 0x17002218 RID: 8728
		// (get) Token: 0x0600CA27 RID: 51751 RVA: 0x002B37DC File Offset: 0x002B19DC
		public FormulaExpression TranslatedExpression
		{
			get
			{
				TranslationMeta meta = this.Meta;
				if (meta == null || !meta.Valid)
				{
					return null;
				}
				return this._translatedExpression;
			}
		}

		// Token: 0x0600CA28 RID: 51752 RVA: 0x002B3804 File Offset: 0x002B1A04
		public override bool Equals(object other)
		{
			FormulaTranslation formulaTranslation = other as FormulaTranslation;
			if (formulaTranslation == null || !this.Equals(formulaTranslation))
			{
				ITranslation<Program, FormulaExpression> translation = other as ITranslation<Program, FormulaExpression>;
				return translation != null && this.Equals(translation);
			}
			return true;
		}

		// Token: 0x0600CA29 RID: 51753 RVA: 0x00218E7F File Offset: 0x0021707F
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		// Token: 0x0600CA2A RID: 51754 RVA: 0x002B3839 File Offset: 0x002B1A39
		public JObject ToJson()
		{
			TargetLanguage target = this.Target;
			FormulaExpression translatedExpression = this.TranslatedExpression;
			return JObject.FromObject(new
			{
				Target = target,
				TranslatedExpression = ((translatedExpression != null) ? translatedExpression.ToString() : null),
				Program = this.Program.ToString(),
				Meta = this.Meta.ToJson()
			});
		}

		// Token: 0x0600CA2B RID: 51755 RVA: 0x002B3874 File Offset: 0x002B1A74
		public override string ToString()
		{
			string text;
			if ((text = this._toString) == null)
			{
				FormulaExpression translatedExpression = this.TranslatedExpression;
				text = (this._toString = ((translatedExpression != null) ? translatedExpression.ToString() : null) ?? string.Empty);
			}
			return text;
		}

		// Token: 0x0600CA2C RID: 51756 RVA: 0x002B38AF File Offset: 0x002B1AAF
		public bool Equals(FormulaTranslation other)
		{
			return other != null && this.ToString() == other.ToString();
		}

		// Token: 0x0600CA2D RID: 51757 RVA: 0x002B38CD File Offset: 0x002B1ACD
		public static bool operator ==(FormulaTranslation left, FormulaTranslation right)
		{
			return (left == null && right == null) || (left != null && left.Equals(right));
		}

		// Token: 0x0600CA2E RID: 51758 RVA: 0x002B38E3 File Offset: 0x002B1AE3
		public static bool operator !=(FormulaTranslation left, FormulaTranslation right)
		{
			return !(left == right);
		}

		// Token: 0x0600CA2F RID: 51759 RVA: 0x002B38EF File Offset: 0x002B1AEF
		public bool Equals(ITranslation<Program, FormulaExpression> other)
		{
			return other != null && this.ToString() == other.ToString();
		}

		// Token: 0x0600CA30 RID: 51760 RVA: 0x00218E8C File Offset: 0x0021708C
		public static bool operator ==(ITranslation<Program, FormulaExpression> left, FormulaTranslation right)
		{
			return (left == null && right == null) || (left != null && left.Equals(right));
		}

		// Token: 0x0600CA31 RID: 51761 RVA: 0x002B3907 File Offset: 0x002B1B07
		public static bool operator ==(FormulaTranslation left, ITranslation<Program, FormulaExpression> right)
		{
			return (left == null && right == null) || (left != null && left.Equals(right));
		}

		// Token: 0x0600CA32 RID: 51762 RVA: 0x002B391D File Offset: 0x002B1B1D
		public static bool operator !=(ITranslation<Program, FormulaExpression> left, FormulaTranslation right)
		{
			return !(left == right);
		}

		// Token: 0x0600CA33 RID: 51763 RVA: 0x002B3929 File Offset: 0x002B1B29
		public static bool operator !=(FormulaTranslation left, ITranslation<Program, FormulaExpression> right)
		{
			return !(left == right);
		}

		// Token: 0x04004F60 RID: 20320
		private string _toString;

		// Token: 0x04004F61 RID: 20321
		private readonly FormulaExpression _translatedExpression;
	}
}
