using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Table.Constraints;
using Microsoft.ProgramSynthesis.Transformation.Table.Translation;
using Microsoft.ProgramSynthesis.Transformation.Table.Translation.DataWrangling;
using Microsoft.ProgramSynthesis.Transformation.Table.Translation.PowerQuery;
using Microsoft.ProgramSynthesis.Transformation.Table.Translation.Python;
using Microsoft.ProgramSynthesis.Translation;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;
using Microsoft.ProgramSynthesis.Wrangling.Logging;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;
using Microsoft.ProgramSynthesis.Wrangling.Session;
using Newtonsoft.Json;

namespace Microsoft.ProgramSynthesis.Transformation.Table
{
	// Token: 0x02001A83 RID: 6787
	internal class Session : NonInteractiveSession<Program, ITable<object>, ITable<object>>
	{
		// Token: 0x0600DF52 RID: 57170 RVA: 0x002F6159 File Offset: 0x002F4359
		public Session(IJournalStorage journalStorage = null, CultureInfo culture = null, ILogger logger = null)
			: base(Learner.Instance, Loader.Instance, journalStorage, culture, "Transformation.Table", logger, false)
		{
		}

		// Token: 0x17002544 RID: 9540
		// (get) Token: 0x0600DF53 RID: 57171 RVA: 0x002F6174 File Offset: 0x002F4374
		// (set) Token: 0x0600DF54 RID: 57172 RVA: 0x002F6181 File Offset: 0x002F4381
		public ITable<object> InputTable
		{
			get
			{
				return base.Inputs.FirstOrDefault<ITable<object>>();
			}
			set
			{
				base.Inputs.Clear();
				base.Inputs.Add(value);
			}
		}

		// Token: 0x17002545 RID: 9541
		// (get) Token: 0x0600DF55 RID: 57173 RVA: 0x002F619A File Offset: 0x002F439A
		internal new static JsonSerializerSettings JsonSerializerSettings
		{
			get
			{
				return Session.LazyJsonSerializerSettings.Value;
			}
		}

		// Token: 0x17002546 RID: 9542
		// (get) Token: 0x0600DF56 RID: 57174 RVA: 0x002F61A6 File Offset: 0x002F43A6
		protected override JsonSerializerSettings JsonSerializerSettingsInstance
		{
			get
			{
				return Session.JsonSerializerSettings;
			}
		}

		// Token: 0x0600DF57 RID: 57175 RVA: 0x002F61B0 File Offset: 0x002F43B0
		protected override Session<Program, ITable<object>, ITable<object>>.IProgramSetWrapper LearnImpl(LearnProgramRequest<Program, ITable<object>, ITable<object>> request, RankingMode rankingMode, int? k, int? randomK, CancellationToken cancel)
		{
			if (k != null)
			{
				throw new ArgumentException("k must be null. Take(k) to get TopK.");
			}
			if (randomK != null)
			{
				throw new ArgumentException("randomK must be null. No support for random sampling of suggestions.");
			}
			ProgramSet programSet = this.LearnerFor(rankingMode).LearnAll(request.Constraints, request.Inputs, cancel);
			IEnumerable<Program> enumerable = ((Learner)this.LearnerFor(rankingMode)).GetRankedProgramsWithScores(programSet.RealizedPrograms, request.Inputs, cancel).Select2((ProgramNode node, double score) => ((Loader)base.ProgramLoader).Create(node, score));
			if (enumerable.Count<Program>() == 0)
			{
				return null;
			}
			return new Session<Program, ITable<object>, ITable<object>>.PrunedProgramSetWrapper(enumerable.ToList<Program>(), null);
		}

		// Token: 0x0600DF58 RID: 57176 RVA: 0x002F6248 File Offset: 0x002F4448
		public TransformationTableTranslation Translate(Program program, TargetLanguage targetLanguage)
		{
			TransformationTableTranslation transformationTableTranslation;
			if (targetLanguage != TargetLanguage.Pandas)
			{
				if (targetLanguage != TargetLanguage.PowerQueryM)
				{
					throw new NotImplementedException(string.Format("Translation is unsupported for the target language {0}.", targetLanguage));
				}
				transformationTableTranslation = PowerQueryTranslator.Translate(program, this, null, default(CancellationToken));
			}
			else
			{
				transformationTableTranslation = PandasTranslator.Translate(program, this.InputTable, base.Constraints.OfType<PandasTranslationConstraint>().OnlyOrDefault<PandasTranslationConstraint>() ?? new PandasTranslationConstraint());
			}
			return transformationTableTranslation;
		}

		// Token: 0x0600DF59 RID: 57177 RVA: 0x002F62B8 File Offset: 0x002F44B8
		public DataWranglingOperationTranslation Translate(Program program)
		{
			return DataWranglingOperationTranslator.Translate(program, this.InputTable);
		}

		// Token: 0x040054C0 RID: 21696
		private static readonly Lazy<JsonSerializerSettings> LazyJsonSerializerSettings = new Lazy<JsonSerializerSettings>(() => new SessionJsonSerializerSettings().Initialize());
	}
}
