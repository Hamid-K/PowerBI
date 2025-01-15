using System;
using Microsoft.ProgramSynthesis.Learning.Logging;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.Constraints
{
	// Token: 0x0200022B RID: 555
	public class EnableSynthesisLogging<TInput, TOutput> : Constraint<TInput, TOutput>, IOptionConstraint<DSLOptions>
	{
		// Token: 0x06000BED RID: 3053 RVA: 0x00024368 File Offset: 0x00022568
		public EnableSynthesisLogging(string filename = null, LogInfo? logInfo = null)
		{
			this._filename = filename;
			this._logInfo = logInfo ?? LogInfo.AllAbbreviated;
		}

		// Token: 0x06000BEE RID: 3054 RVA: 0x0002439E File Offset: 0x0002259E
		public void SetOptions(DSLOptions options)
		{
			options.SynthesisLogFilenamePrefix = this._filename.Some<string>();
			options.LogInfo = this._logInfo;
		}

		// Token: 0x06000BEF RID: 3055 RVA: 0x000243C0 File Offset: 0x000225C0
		public override bool ConflictsWith(Constraint<TInput, TOutput> other)
		{
			EnableSynthesisLogging<TInput, TOutput> enableSynthesisLogging = other as EnableSynthesisLogging<TInput, TOutput>;
			return enableSynthesisLogging != null && (!object.Equals(this._filename, enableSynthesisLogging._filename) || !object.Equals(this._logInfo, enableSynthesisLogging._logInfo));
		}

		// Token: 0x06000BF0 RID: 3056 RVA: 0x0002440C File Offset: 0x0002260C
		public override bool Equals(Constraint<TInput, TOutput> other)
		{
			EnableSynthesisLogging<TInput, TOutput> enableSynthesisLogging = other as EnableSynthesisLogging<TInput, TOutput>;
			return enableSynthesisLogging != null && object.Equals(this._filename, enableSynthesisLogging._filename) && object.Equals(this._logInfo, enableSynthesisLogging._logInfo);
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x00024454 File Offset: 0x00022654
		public override int GetHashCode()
		{
			string filename = this._filename;
			return (((filename != null) ? filename.GetHashCode() : 0) * 439) ^ this._logInfo.GetHashCode();
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override bool Valid(Program<TInput, TOutput> program)
		{
			return true;
		}

		// Token: 0x04000612 RID: 1554
		private readonly string _filename;

		// Token: 0x04000613 RID: 1555
		private readonly LogInfo _logInfo;
	}
}
