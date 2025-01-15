using System;
using Microsoft.ProgramSynthesis.Transformation.Table.Semantics.Meta;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Constraints
{
	// Token: 0x02001B5F RID: 7007
	public class SamplingConstraint : Constraint<ITable<object>, ITable<object>>, IOptionConstraint<Options>
	{
		// Token: 0x0600E607 RID: 58887 RVA: 0x0030B96C File Offset: 0x00309B6C
		public SamplingConstraint(bool useAllDataForLearning = false, int numSamplesForLearning = 1, int numRowsToConsiderForLearning = 500, int numRowsToSampleForLearning = 200, bool useAllDataForRanking = false, bool useLearningDataForRanking = true, int numSamplesForRanking = 1, int numRowsToConsiderForRanking = 500, int numRowsToSampleForRanking = 200)
		{
			this._useAllDataForLearning = useAllDataForLearning;
			this._numSamplesForLearning = numSamplesForLearning;
			this._numRowsToConsiderForLearning = numRowsToConsiderForLearning;
			this._numRowsToSampleForLearning = numRowsToSampleForLearning;
			this._useAllDataForRanking = useAllDataForRanking;
			this._useLearningDataForRanking = useLearningDataForRanking;
			this._numSamplesForRanking = numSamplesForRanking;
			this._numRowsToConsiderForRanking = numRowsToConsiderForRanking;
			this._numRowsToSampleForRanking = numRowsToSampleForRanking;
		}

		// Token: 0x17002656 RID: 9814
		// (get) Token: 0x0600E608 RID: 58888 RVA: 0x0030B9C4 File Offset: 0x00309BC4
		public bool UseAllDataForLearning
		{
			get
			{
				return this._useAllDataForLearning;
			}
		}

		// Token: 0x17002657 RID: 9815
		// (get) Token: 0x0600E609 RID: 58889 RVA: 0x0030B9CC File Offset: 0x00309BCC
		public int NumSamplesForLearning
		{
			get
			{
				return this._numSamplesForLearning;
			}
		}

		// Token: 0x17002658 RID: 9816
		// (get) Token: 0x0600E60A RID: 58890 RVA: 0x0030B9D4 File Offset: 0x00309BD4
		public int NumRowsToConsiderForLearning
		{
			get
			{
				return this._numRowsToConsiderForLearning;
			}
		}

		// Token: 0x17002659 RID: 9817
		// (get) Token: 0x0600E60B RID: 58891 RVA: 0x0030B9DC File Offset: 0x00309BDC
		public int NumRowsToSampleForLearning
		{
			get
			{
				return this._numRowsToSampleForLearning;
			}
		}

		// Token: 0x1700265A RID: 9818
		// (get) Token: 0x0600E60C RID: 58892 RVA: 0x0030B9E4 File Offset: 0x00309BE4
		public bool UseAllDataForRanking
		{
			get
			{
				return this._useAllDataForRanking;
			}
		}

		// Token: 0x1700265B RID: 9819
		// (get) Token: 0x0600E60D RID: 58893 RVA: 0x0030B9EC File Offset: 0x00309BEC
		public bool UseLearningDataForRanking
		{
			get
			{
				return this._useLearningDataForRanking;
			}
		}

		// Token: 0x1700265C RID: 9820
		// (get) Token: 0x0600E60E RID: 58894 RVA: 0x0030B9F4 File Offset: 0x00309BF4
		public int NumSamplesForRanking
		{
			get
			{
				return this._numSamplesForRanking;
			}
		}

		// Token: 0x1700265D RID: 9821
		// (get) Token: 0x0600E60F RID: 58895 RVA: 0x0030B9FC File Offset: 0x00309BFC
		public int NumRowsToConsiderForRanking
		{
			get
			{
				return this._numRowsToConsiderForRanking;
			}
		}

		// Token: 0x1700265E RID: 9822
		// (get) Token: 0x0600E610 RID: 58896 RVA: 0x0030BA04 File Offset: 0x00309C04
		public int NumRowsToSampleForRanking
		{
			get
			{
				return this._numRowsToSampleForRanking;
			}
		}

		// Token: 0x0600E611 RID: 58897 RVA: 0x0030BA0C File Offset: 0x00309C0C
		public override bool ConflictsWith(Constraint<ITable<object>, ITable<object>> other)
		{
			SamplingConstraint samplingConstraint = other as SamplingConstraint;
			return samplingConstraint != null && (this.UseAllDataForLearning != samplingConstraint.UseAllDataForLearning || this.NumSamplesForLearning != samplingConstraint.NumSamplesForLearning || this.NumRowsToConsiderForLearning != samplingConstraint.NumRowsToConsiderForLearning || this.NumRowsToSampleForLearning != samplingConstraint.NumRowsToSampleForLearning || this.UseAllDataForRanking != samplingConstraint.UseAllDataForRanking || this.UseLearningDataForRanking != samplingConstraint.UseLearningDataForRanking || this.NumSamplesForRanking != samplingConstraint.NumSamplesForRanking || this.NumRowsToConsiderForRanking != samplingConstraint.NumRowsToConsiderForRanking || this.NumRowsToSampleForRanking != samplingConstraint.NumRowsToSampleForRanking);
		}

		// Token: 0x0600E612 RID: 58898 RVA: 0x0030BAAC File Offset: 0x00309CAC
		public override bool Equals(Constraint<ITable<object>, ITable<object>> other)
		{
			SamplingConstraint samplingConstraint = other as SamplingConstraint;
			return samplingConstraint != null && this.UseAllDataForLearning == samplingConstraint.UseAllDataForLearning && this.NumSamplesForLearning == samplingConstraint.NumSamplesForLearning && this.NumRowsToConsiderForLearning == samplingConstraint.NumRowsToConsiderForLearning && this.NumRowsToSampleForLearning == samplingConstraint.NumRowsToSampleForLearning && this.UseAllDataForRanking == samplingConstraint.UseAllDataForRanking && this.UseLearningDataForRanking == samplingConstraint.UseLearningDataForRanking && this.NumSamplesForRanking == samplingConstraint.NumSamplesForRanking && this.NumRowsToConsiderForRanking == samplingConstraint.NumRowsToConsiderForRanking && this.NumRowsToSampleForRanking == samplingConstraint.NumRowsToSampleForRanking;
		}

		// Token: 0x0600E613 RID: 58899 RVA: 0x0030BB44 File Offset: 0x00309D44
		public override int GetHashCode()
		{
			return new string[]
			{
				this.UseAllDataForLearning.ToString(),
				this.NumSamplesForLearning.ToString(),
				this.NumRowsToConsiderForLearning.ToString(),
				this.NumRowsToSampleForLearning.ToString(),
				this.UseAllDataForRanking.ToString(),
				this.UseLearningDataForRanking.ToString(),
				this.NumSamplesForRanking.ToString(),
				this.NumRowsToConsiderForRanking.ToString(),
				this.NumRowsToSampleForRanking.ToString()
			}.OrderDependentHashCode<string>();
		}

		// Token: 0x0600E614 RID: 58900 RVA: 0x0030BBF8 File Offset: 0x00309DF8
		public void SetOptions(Options options)
		{
			options.UseAllDataForLearning = this.UseAllDataForLearning;
			options.NumSamplesForLearning = this.NumSamplesForLearning;
			options.NumRowsToConsiderForLearning = new int?(this.NumRowsToConsiderForLearning);
			options.NumRowsToSampleForLearning = new int?(this.NumRowsToSampleForLearning);
			options.UseAllDataForRanking = this.UseAllDataForRanking;
			options.UseLearningDataForRanking = this.UseLearningDataForRanking;
			options.NumSamplesForRanking = this.NumSamplesForRanking;
			options.NumRowsToConsiderForRanking = new int?(this.NumRowsToConsiderForRanking);
			options.NumRowsToSampleForRanking = new int?(this.NumRowsToSampleForRanking);
		}

		// Token: 0x0600E615 RID: 58901 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override bool Valid(Program<ITable<object>, ITable<object>> program)
		{
			return true;
		}

		// Token: 0x04005759 RID: 22361
		private readonly bool _useAllDataForLearning;

		// Token: 0x0400575A RID: 22362
		private readonly int _numSamplesForLearning;

		// Token: 0x0400575B RID: 22363
		private readonly int _numRowsToConsiderForLearning;

		// Token: 0x0400575C RID: 22364
		private readonly int _numRowsToSampleForLearning;

		// Token: 0x0400575D RID: 22365
		private readonly bool _useAllDataForRanking;

		// Token: 0x0400575E RID: 22366
		private readonly bool _useLearningDataForRanking;

		// Token: 0x0400575F RID: 22367
		private readonly int _numSamplesForRanking;

		// Token: 0x04005760 RID: 22368
		private readonly int _numRowsToConsiderForRanking;

		// Token: 0x04005761 RID: 22369
		private readonly int _numRowsToSampleForRanking;
	}
}
