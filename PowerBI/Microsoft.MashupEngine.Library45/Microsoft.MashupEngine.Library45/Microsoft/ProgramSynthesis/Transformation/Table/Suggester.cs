using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using Microsoft.ProgramSynthesis.Transformation.Table.Constraints;
using Microsoft.ProgramSynthesis.Transformation.Table.Translation;
using Microsoft.ProgramSynthesis.Transformation.Table.Translation.DataWrangling;
using Microsoft.ProgramSynthesis.Translation;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Microsoft.ProgramSynthesis.Wrangling.Logging;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;
using Microsoft.ProgramSynthesis.Wrangling.Session;

namespace Microsoft.ProgramSynthesis.Transformation.Table
{
	// Token: 0x02001A86 RID: 6790
	public class Suggester
	{
		// Token: 0x0600DF63 RID: 57187 RVA: 0x002F6384 File Offset: 0x002F4584
		public Suggester(ITable<object> inputTable, CultureInfo culture = null, ILogger logger = null)
		{
			this._session = new Session(null, culture, logger)
			{
				InputTable = inputTable
			};
		}

		// Token: 0x17002549 RID: 9545
		// (get) Token: 0x0600DF64 RID: 57188 RVA: 0x002F63A1 File Offset: 0x002F45A1
		// (set) Token: 0x0600DF65 RID: 57189 RVA: 0x002F63AE File Offset: 0x002F45AE
		public ITable<object> InputTable
		{
			get
			{
				return this._session.InputTable;
			}
			set
			{
				this._session.InputTable = value;
			}
		}

		// Token: 0x1700254A RID: 9546
		// (get) Token: 0x0600DF66 RID: 57190 RVA: 0x002F63BC File Offset: 0x002F45BC
		public NotifyingCollection<Constraint<ITable<object>, ITable<object>>> Constraints
		{
			get
			{
				return this._session.Constraints;
			}
		}

		// Token: 0x0600DF67 RID: 57191 RVA: 0x002F63CC File Offset: 0x002F45CC
		public IEnumerable<TransformationTableTranslation> Suggest(TargetLanguage targetLanguage, int? maxNumSuggestions = 1, Guid? guid = null, CancellationToken cancel = default(CancellationToken))
		{
			if (targetLanguage != TargetLanguage.Pandas && targetLanguage != TargetLanguage.PowerQueryM)
			{
				throw new NotImplementedException("Target language not supported: " + targetLanguage.ToString());
			}
			if (!this.Constraints.OfType<AllowedOperators>().Any<AllowedOperators>())
			{
				AllowedOperators allowedOperators = AllowedOperators.DefaultForTarget(targetLanguage);
				if (allowedOperators != null)
				{
					this.Constraints.Add(allowedOperators);
				}
			}
			if (!this.Constraints.OfType<SamplingConstraint>().Any<SamplingConstraint>())
			{
				this.Constraints.Add(new SamplingConstraint(false, 1, 500, 200, false, true, 1, 500, 200));
			}
			Session<Program, ITable<object>, ITable<object>>.IProgramSetWrapper programSetWrapper = this._session.LearnAll(cancel, guid);
			IEnumerable<Program> enumerable = ((programSetWrapper != null) ? programSetWrapper.RealizedPrograms : null);
			IEnumerable<Program> enumerable2;
			if (maxNumSuggestions != null)
			{
				int valueOrDefault = maxNumSuggestions.GetValueOrDefault();
				enumerable2 = enumerable.Take(valueOrDefault);
			}
			else
			{
				enumerable2 = enumerable;
			}
			enumerable = enumerable2;
			if (enumerable != null)
			{
				return enumerable.Select((Program p) => this._session.Translate(p, targetLanguage));
			}
			return Enumerable.Empty<TransformationTableTranslation>();
		}

		// Token: 0x0600DF68 RID: 57192 RVA: 0x002F64DC File Offset: 0x002F46DC
		public IEnumerable<DataWranglingOperationTranslation> Suggest(int? maxNumSuggestions = 1, Guid? guid = null, CancellationToken cancel = default(CancellationToken))
		{
			if (!this.Constraints.OfType<AllowedOperators>().Any<AllowedOperators>())
			{
				AllowedOperators allowedOperators = AllowedOperators.DefaultForTarget(TargetLanguage.Pandas);
				if (allowedOperators != null)
				{
					this.Constraints.Add(allowedOperators);
				}
			}
			if (!this.Constraints.OfType<SamplingConstraint>().Any<SamplingConstraint>())
			{
				this.Constraints.Add(new SamplingConstraint(false, 1, 500, 200, false, true, 1, 500, 200));
			}
			Session<Program, ITable<object>, ITable<object>>.IProgramSetWrapper programSetWrapper = this._session.LearnAll(cancel, guid);
			IEnumerable<Program> enumerable = ((programSetWrapper != null) ? programSetWrapper.RealizedPrograms : null);
			if (enumerable != null)
			{
				IEnumerable<Program> enumerable2;
				if (maxNumSuggestions != null)
				{
					int valueOrDefault = maxNumSuggestions.GetValueOrDefault();
					enumerable2 = enumerable.Take(valueOrDefault);
				}
				else
				{
					enumerable2 = enumerable;
				}
				return enumerable2.Select(new Func<Program, DataWranglingOperationTranslation>(this._session.Translate));
			}
			return Enumerable.Empty<DataWranglingOperationTranslation>();
		}

		// Token: 0x0600DF69 RID: 57193 RVA: 0x002F65A2 File Offset: 0x002F47A2
		public TransformationTableTranslation Translate(DataWranglingOperationTranslation translation, TargetLanguage targetLanguage)
		{
			return this._session.Translate(translation.Program, targetLanguage);
		}

		// Token: 0x0600DF6A RID: 57194 RVA: 0x002F65B6 File Offset: 0x002F47B6
		public TransformationTableTranslation Translate(TransformationTableTranslation translation, TargetLanguage targetLanguage)
		{
			return this._session.Translate(translation.Program, targetLanguage);
		}

		// Token: 0x040054C2 RID: 21698
		internal readonly Session _session;
	}
}
