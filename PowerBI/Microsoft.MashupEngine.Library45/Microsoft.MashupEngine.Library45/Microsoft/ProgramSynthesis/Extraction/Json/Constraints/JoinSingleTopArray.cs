using System;
using Microsoft.ProgramSynthesis.Extraction.Json.Learning;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Extraction.Json.Constraints
{
	// Token: 0x02000BA5 RID: 2981
	public class JoinSingleTopArray : Constraint<string, ITable<string>>, IOptionConstraint<SynthesisOptions>
	{
		// Token: 0x06004BBF RID: 19391 RVA: 0x000EEB91 File Offset: 0x000ECD91
		private JoinSingleTopArray()
		{
		}

		// Token: 0x17000D85 RID: 3461
		// (get) Token: 0x06004BC0 RID: 19392 RVA: 0x000EED8B File Offset: 0x000ECF8B
		public static JoinSingleTopArray Instance { get; } = new JoinSingleTopArray();

		// Token: 0x06004BC1 RID: 19393 RVA: 0x000EED92 File Offset: 0x000ECF92
		public void SetOptions(SynthesisOptions options)
		{
			options.JoinSingleTopArray = true;
		}

		// Token: 0x06004BC2 RID: 19394 RVA: 0x000EED9B File Offset: 0x000ECF9B
		public override bool ConflictsWith(Constraint<string, ITable<string>> other)
		{
			return other is AutoFlatten || other is JoinAllArrays || other is SplitTopArrays;
		}

		// Token: 0x06004BC3 RID: 19395 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override bool Valid(Program<string, ITable<string>> program)
		{
			return true;
		}

		// Token: 0x06004BC4 RID: 19396 RVA: 0x0000D050 File Offset: 0x0000B250
		private bool Equals(JoinSingleTopArray other)
		{
			return this == other;
		}

		// Token: 0x06004BC5 RID: 19397 RVA: 0x000EEDB8 File Offset: 0x000ECFB8
		public override bool Equals(Constraint<string, ITable<string>> other)
		{
			return this.Equals(other as JoinSingleTopArray);
		}

		// Token: 0x06004BC6 RID: 19398 RVA: 0x000EEDB8 File Offset: 0x000ECFB8
		public override bool Equals(object obj)
		{
			return this.Equals(obj as JoinSingleTopArray);
		}

		// Token: 0x06004BC7 RID: 19399 RVA: 0x000EEDC6 File Offset: 0x000ECFC6
		public override int GetHashCode()
		{
			return 661;
		}
	}
}
