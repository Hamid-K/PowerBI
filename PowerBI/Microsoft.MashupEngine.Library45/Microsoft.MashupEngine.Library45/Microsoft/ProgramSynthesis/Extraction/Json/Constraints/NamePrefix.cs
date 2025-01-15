using System;
using Microsoft.ProgramSynthesis.Extraction.Json.Learning;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Extraction.Json.Constraints
{
	// Token: 0x02000BA6 RID: 2982
	public class NamePrefix : Constraint<string, ITable<string>>, IOptionConstraint<SynthesisOptions>
	{
		// Token: 0x06004BC9 RID: 19401 RVA: 0x000EEDD9 File Offset: 0x000ECFD9
		public NamePrefix(string prefix)
		{
			if (prefix == null)
			{
				throw new ArgumentNullException("prefix");
			}
			this.Prefix = prefix;
		}

		// Token: 0x17000D86 RID: 3462
		// (get) Token: 0x06004BCA RID: 19402 RVA: 0x000EEDF7 File Offset: 0x000ECFF7
		private string Prefix { get; }

		// Token: 0x06004BCB RID: 19403 RVA: 0x000EEDFF File Offset: 0x000ECFFF
		public void SetOptions(SynthesisOptions options)
		{
			options.NamePrefix = this.Prefix;
		}

		// Token: 0x06004BCC RID: 19404 RVA: 0x000EEE0D File Offset: 0x000ED00D
		public override bool ConflictsWith(Constraint<string, ITable<string>> other)
		{
			return other is NamePrefix;
		}

		// Token: 0x06004BCD RID: 19405 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override bool Valid(Program<string, ITable<string>> program)
		{
			return true;
		}

		// Token: 0x06004BCE RID: 19406 RVA: 0x000EEE18 File Offset: 0x000ED018
		private bool Equals(NamePrefix other)
		{
			return other != null && (this == other || this.Prefix == other.Prefix);
		}

		// Token: 0x06004BCF RID: 19407 RVA: 0x000EEE36 File Offset: 0x000ED036
		public override bool Equals(Constraint<string, ITable<string>> other)
		{
			return this.Equals(other as NamePrefix);
		}

		// Token: 0x06004BD0 RID: 19408 RVA: 0x000EEE36 File Offset: 0x000ED036
		public override bool Equals(object obj)
		{
			return this.Equals(obj as NamePrefix);
		}

		// Token: 0x06004BD1 RID: 19409 RVA: 0x000EEE44 File Offset: 0x000ED044
		public override int GetHashCode()
		{
			int num = 3559;
			string prefix = this.Prefix;
			return num ^ ((prefix != null) ? prefix.GetHashCode() : 0);
		}
	}
}
