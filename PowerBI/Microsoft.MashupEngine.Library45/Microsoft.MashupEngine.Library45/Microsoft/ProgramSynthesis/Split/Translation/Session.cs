using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Split.Text;
using Microsoft.ProgramSynthesis.Split.Text.Semantics;
using Microsoft.ProgramSynthesis.Split.Translation.PowerQuery;
using Microsoft.ProgramSynthesis.Split.Translation.Python;
using Microsoft.ProgramSynthesis.Transformation.Formula;
using Microsoft.ProgramSynthesis.Transformation.Formula.Translation;
using Microsoft.ProgramSynthesis.Translation;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Logging;
using Microsoft.ProgramSynthesis.Wrangling.Session;

namespace Microsoft.ProgramSynthesis.Split.Translation
{
	// Token: 0x02001400 RID: 5120
	public class Session : SplitSession
	{
		// Token: 0x06009E27 RID: 40487 RVA: 0x002189BC File Offset: 0x00216BBC
		public Session(IJournalStorage journalStorage = null, CultureInfo culture = null, ILogger logger = null)
			: base(journalStorage, culture, logger)
		{
		}

		// Token: 0x06009E28 RID: 40488 RVA: 0x002189C8 File Offset: 0x00216BC8
		public ITranslation<SplitProgram, FormulaExpression> Translate(TargetLanguage target, SplitProgram program, OptimizeFor? optimizeFor = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			ITranslation<SplitProgram, FormulaExpression> translation;
			if (target != TargetLanguage.Pandas)
			{
				if (target != TargetLanguage.PowerQueryM)
				{
					throw new NotImplementedException(string.Format("Translation not supported for {0}.", target));
				}
				translation = PowerQueryTranslator.Translate(this, program, optimizeFor, cancellationToken);
			}
			else
			{
				translation = PandasTranslator.Translate(this, program, optimizeFor, cancellationToken);
			}
			return translation;
		}

		// Token: 0x06009E29 RID: 40489 RVA: 0x00218A10 File Offset: 0x00216C10
		public PandasTranslation TranslatePandas(SplitProgram program, OptimizeFor? optimizeFor = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return (PandasTranslation)this.Translate(TargetLanguage.Pandas, program, optimizeFor, cancellationToken);
		}

		// Token: 0x06009E2A RID: 40490 RVA: 0x00218A21 File Offset: 0x00216C21
		public PowerQueryTranslation TranslatePowerQuery(SplitProgram program, OptimizeFor? optimizeFor = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return (PowerQueryTranslation)this.Translate(TargetLanguage.PowerQueryM, program, optimizeFor, cancellationToken);
		}

		// Token: 0x02001401 RID: 5121
		internal abstract class Translator<T> where T : class
		{
			// Token: 0x06009E2B RID: 40491 RVA: 0x00218A34 File Offset: 0x00216C34
			public Translator(Session session, SplitProgram program = null)
			{
				this._session = session;
				this._program = program ?? session.Learn(null, default(CancellationToken), null);
			}

			// Token: 0x17001AC8 RID: 6856
			// (get) Token: 0x06009E2C RID: 40492 RVA: 0x00218A72 File Offset: 0x00216C72
			public List<SplitCell[]> ProgramOutputs
			{
				get
				{
					if (this._programOutputs == null)
					{
						this._programOutputs = this._session.Inputs.Select(new Func<StringRegion, SplitCell[]>(this._program.Run)).ToList<SplitCell[]>();
					}
					return this._programOutputs;
				}
			}

			// Token: 0x17001AC9 RID: 6857
			// (get) Token: 0x06009E2D RID: 40493
			public abstract string InputColumnName { get; }

			// Token: 0x17001ACA RID: 6858
			// (get) Token: 0x06009E2E RID: 40494
			public abstract string OutputColumnPrefix { get; }

			// Token: 0x17001ACB RID: 6859
			// (get) Token: 0x06009E2F RID: 40495 RVA: 0x0000FA11 File Offset: 0x0000DC11
			public virtual int FirstOutputColumnIndex
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x06009E30 RID: 40496
			protected abstract ITranslation<SplitProgram, FormulaExpression> WrapTranslated(T translated, IEnumerable<string> outputColumnNames);

			// Token: 0x06009E31 RID: 40497 RVA: 0x00218AB0 File Offset: 0x00216CB0
			public ITranslation<SplitProgram, FormulaExpression> Translate(OptimizeFor? optimizeFor = null, CancellationToken cancellationToken = default(CancellationToken))
			{
				T t;
				IEnumerable<string> enumerable;
				if (!this.TryTranslateSplit(out t, out enumerable))
				{
					t = this.TranslateToTFormula(optimizeFor, cancellationToken, out enumerable);
				}
				return this.WrapTranslated(t, enumerable);
			}

			// Token: 0x06009E32 RID: 40498
			protected abstract bool TryTranslateSplit(out T program, out IEnumerable<string> outputColumnNames);

			// Token: 0x06009E33 RID: 40499 RVA: 0x00218ADC File Offset: 0x00216CDC
			protected T TranslateToTFormula(OptimizeFor? optimizeFor, CancellationToken cancellationToken, out IEnumerable<string> outputColumnNames)
			{
				Session.Translator<T>.<>c__DisplayClass15_0 CS$<>8__locals1 = new Session.Translator<T>.<>c__DisplayClass15_0();
				CS$<>8__locals1.<>4__this = this;
				outputColumnNames = null;
				CS$<>8__locals1.inputColumn = this.InputColumnName ?? "input";
				List<InputRow> list = this._session.Inputs.Select(delegate(StringRegion input)
				{
					Dictionary<string, object> dictionary = new Dictionary<string, object>();
					string inputColumn = CS$<>8__locals1.inputColumn;
					dictionary[inputColumn] = input.Value;
					return new InputRow(dictionary);
				}).ToList<InputRow>();
				int num = this.ProgramOutputs[0].Length;
				int firstOutputColumnIndex = this.FirstOutputColumnIndex;
				List<string> list2 = new List<string>();
				int i = 0;
				while (i < num)
				{
					Session session = new Session(this._session.Logger, this._session.JournalStorage, this._session.Culture);
					List<Example> list3 = new List<Example>();
					int j;
					for (j = 0; j < list.Count; j++)
					{
						IRow row = list[j];
						SplitCell splitCell = this.ProgramOutputs[j][i];
						string text;
						if (splitCell == null)
						{
							text = null;
						}
						else
						{
							StringRegion cellValue = splitCell.CellValue;
							text = ((cellValue != null) ? cellValue.Value : null);
						}
						string text2 = text ?? string.Empty;
						Example example = new Example(row, text2, false);
						if (!list3.Contains(example))
						{
							list3.Add(example);
						}
						if (list3.Count == 3)
						{
							break;
						}
					}
					session.Constraints.Add(list3);
					int num2 = j - 1;
					Microsoft.ProgramSynthesis.Transformation.Formula.Program program;
					bool flag;
					do
					{
						cancellationToken.ThrowIfCancellationRequested();
						program = session.Learn(null, cancellationToken, null);
						if (program == null)
						{
							goto Block_7;
						}
						flag = true;
						for (int num3 = CS$<>8__locals1.<TranslateToTFormula>g__next|1(num2); num3 != num2; num3 = CS$<>8__locals1.<TranslateToTFormula>g__next|1(num3))
						{
							if (num3 == this.ProgramOutputs.Count)
							{
								num3 = 0;
							}
							InputRow inputRow = list[num3];
							if (!this.ProgramOutputs[num3].All(delegate(SplitCell c)
							{
								object obj;
								if (c == null)
								{
									obj = null;
								}
								else
								{
									StringRegion cellValue3 = c.CellValue;
									obj = ((cellValue3 != null) ? cellValue3.Value : null);
								}
								return obj == null;
							}))
							{
								SplitCell splitCell2 = this.ProgramOutputs[num3][i];
								string text3;
								if (splitCell2 == null)
								{
									text3 = null;
								}
								else
								{
									StringRegion cellValue2 = splitCell2.CellValue;
									text3 = ((cellValue2 != null) ? cellValue2.Value : null);
								}
								string text4 = text3 ?? string.Empty;
								if (program.Run(inputRow) as string != text4)
								{
									flag = false;
									num2 = num3;
									Example example2 = new Example(inputRow, text4, false);
									session.Constraints.Add(example2);
									break;
								}
							}
						}
					}
					while (!flag);
					string text5;
					if (!this.TryRegisterTFormulaProgram(session, program, this.OutputColumnPrefix, firstOutputColumnIndex++, i, cancellationToken, out text5))
					{
						return default(T);
					}
					list2.Add(text5);
					i++;
					continue;
					Block_7:
					return default(T);
				}
				outputColumnNames = list2;
				return this.BuildCombinedTFormulaProgram();
			}

			// Token: 0x06009E34 RID: 40500
			protected abstract bool TryRegisterTFormulaProgram(Session tfSession, Microsoft.ProgramSynthesis.Transformation.Formula.Program tfProgram, string outputColumnPrefix, int outputColumnNumber, int newColumnIndex, CancellationToken cancellationToken, out string outputColumnName);

			// Token: 0x06009E35 RID: 40501
			protected abstract T BuildCombinedTFormulaProgram();

			// Token: 0x04004000 RID: 16384
			protected readonly Session _session;

			// Token: 0x04004001 RID: 16385
			protected readonly SplitProgram _program;

			// Token: 0x04004002 RID: 16386
			private List<SplitCell[]> _programOutputs;
		}
	}
}
