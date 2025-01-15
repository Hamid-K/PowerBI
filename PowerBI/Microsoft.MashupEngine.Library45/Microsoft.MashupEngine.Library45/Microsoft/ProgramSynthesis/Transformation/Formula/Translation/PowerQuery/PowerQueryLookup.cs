using System;
using System.Text.RegularExpressions;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerQuery
{
	// Token: 0x020018BA RID: 6330
	internal class PowerQueryLookup : FormulaExpression, IFormulaTyped
	{
		// Token: 0x0600CE4A RID: 52810 RVA: 0x002BFFD7 File Offset: 0x002BE1D7
		public PowerQueryLookup(FormulaExpression subject, string name)
			: this(subject, name, null)
		{
		}

		// Token: 0x0600CE4B RID: 52811 RVA: 0x002BFFE4 File Offset: 0x002BE1E4
		public PowerQueryLookup(FormulaExpression subject, string name, Type type)
		{
			this.Subject = ((subject is IFormulaOperator) ? new PowerQueryParenthesis(subject) : subject);
			this.Name = name;
			this.Type = type;
			FormulaExpression[] array;
			if (!(this.Subject == null))
			{
				(array = new FormulaExpression[1])[0] = this.Subject;
			}
			else
			{
				array = null;
			}
			base.Children = array;
		}

		// Token: 0x170022AE RID: 8878
		// (get) Token: 0x0600CE4C RID: 52812 RVA: 0x002C0042 File Offset: 0x002BE242
		public string Name { get; }

		// Token: 0x170022AF RID: 8879
		// (get) Token: 0x0600CE4D RID: 52813 RVA: 0x002C004A File Offset: 0x002BE24A
		public FormulaExpression Subject { get; }

		// Token: 0x0600CE4E RID: 52814 RVA: 0x002C0052 File Offset: 0x002BE252
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			FormulaExpression subject = this.Subject;
			return new PowerQueryLookup((subject != null) ? subject.Accept<FormulaExpression>(visitor) : null, this.Name, this.Type);
		}

		// Token: 0x0600CE4F RID: 52815 RVA: 0x002C0078 File Offset: 0x002BE278
		public FormulaExpression CloneWith(string name)
		{
			return new PowerQueryLookup(this.Subject, name, this.Type);
		}

		// Token: 0x0600CE50 RID: 52816 RVA: 0x002C008C File Offset: 0x002BE28C
		public static string EscapeLookupIdentifier(string name)
		{
			if (!PowerQueryLookup._generalIdentifierPattern.IsMatch(name))
			{
				return "#" + PowerQueryStringLiteral.EscapeString(name);
			}
			return name;
		}

		// Token: 0x0600CE51 RID: 52817 RVA: 0x002C00AD File Offset: 0x002BE2AD
		protected override string ToCodeString()
		{
			FormulaExpression subject = this.Subject;
			return (((subject != null) ? subject.ToString() : null) ?? string.Empty) + "[" + PowerQueryLookup.EscapeLookupIdentifier(this.Name) + "]";
		}

		// Token: 0x170022B0 RID: 8880
		// (get) Token: 0x0600CE52 RID: 52818 RVA: 0x002C00E4 File Offset: 0x002BE2E4
		public Type Type { get; }

		// Token: 0x04005097 RID: 20631
		internal const string IdentifierPartCharacters = "_\\p{Lu}\\p{Ll}\\p{Lt}\\p{Lm}\\p{Lo}\\p{Nl}\\p{Nd}\\p{Pc}\\p{Mn}\\p{Mc}\\p{Cf}";

		// Token: 0x04005098 RID: 20632
		internal const string IdentifierStartCharacters = "_\\p{Lu}\\p{Ll}\\p{Lt}\\p{Lm}\\p{Lo}\\p{Nl}";

		// Token: 0x04005099 RID: 20633
		private static readonly Regex _generalIdentifierPattern = new Regex("^(([_\\p{Lu}\\p{Ll}\\p{Lt}\\p{Lm}\\p{Lo}\\p{Nl}][_\\p{Lu}\\p{Ll}\\p{Lt}\\p{Lm}\\p{Lo}\\p{Nl}\\p{Nd}\\p{Pc}\\p{Mn}\\p{Mc}\\p{Cf}]*\\.?)+ ?)+$", RegexOptions.ExplicitCapture | RegexOptions.Compiled);
	}
}
