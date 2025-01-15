using System;
using Microsoft.ProgramSynthesis.Extraction.Json.Learning;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Extraction.Json.Constraints
{
	// Token: 0x02000BA0 RID: 2976
	public class HandleInvalidJson : Constraint<string, ITable<string>>, IOptionConstraint<SynthesisOptions>
	{
		// Token: 0x06004B9F RID: 19359 RVA: 0x000EEC35 File Offset: 0x000ECE35
		public HandleInvalidJson(bool handleInvalidJson = true)
		{
			this._handleInvalidJson = handleInvalidJson;
		}

		// Token: 0x06004BA0 RID: 19360 RVA: 0x000EEC44 File Offset: 0x000ECE44
		public void SetOptions(SynthesisOptions options)
		{
			options.HandleInvalidJson = this._handleInvalidJson;
		}

		// Token: 0x06004BA1 RID: 19361 RVA: 0x00024CEC File Offset: 0x00022EEC
		public override bool Equals(Constraint<string, ITable<string>> other)
		{
			return this.Equals(other);
		}

		// Token: 0x06004BA2 RID: 19362 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public override bool ConflictsWith(Constraint<string, ITable<string>> other)
		{
			return false;
		}

		// Token: 0x06004BA3 RID: 19363 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override bool Valid(Program<string, ITable<string>> program)
		{
			return true;
		}

		// Token: 0x06004BA4 RID: 19364 RVA: 0x000EEC52 File Offset: 0x000ECE52
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this._handleInvalidJson == ((HandleInvalidJson)obj)._handleInvalidJson));
		}

		// Token: 0x06004BA5 RID: 19365 RVA: 0x000EEC88 File Offset: 0x000ECE88
		public override int GetHashCode()
		{
			return 397 ^ this._handleInvalidJson.GetHashCode();
		}

		// Token: 0x04002202 RID: 8706
		private readonly bool _handleInvalidJson;
	}
}
