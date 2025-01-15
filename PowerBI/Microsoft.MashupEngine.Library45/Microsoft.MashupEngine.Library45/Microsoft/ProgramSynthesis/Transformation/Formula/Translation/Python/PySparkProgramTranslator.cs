using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.ProgramSynthesis.Transformation.Formula.Constraints;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Logging;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python
{
	// Token: 0x02001842 RID: 6210
	internal class PySparkProgramTranslator : ProgramTranslatorBase
	{
		// Token: 0x0600CB6A RID: 52074 RVA: 0x002B7348 File Offset: 0x002B5548
		private PySparkProgramTranslator(Program program, IPySparkTranslationOptions options, bool enableMatchUnicode, IEnumerable<Example> examples, IEnumerable<IRow> inputs, ILogger logger)
			: base(program, examples, inputs, logger)
		{
			this._options = options;
			this._enableMatchUnicode = enableMatchUnicode;
		}

		// Token: 0x0600CB6B RID: 52075 RVA: 0x002B7365 File Offset: 0x002B5565
		public static PythonProgram Translate(Program program, IPySparkTranslationOptions options, bool enableMatchUnicode, IEnumerable<Example> examples, IEnumerable<IRow> inputs, ILogger logger = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return new PySparkProgramTranslator(program, options, enableMatchUnicode, examples, inputs, logger).Translate(cancellationToken) as PythonProgram;
		}

		// Token: 0x0600CB6C RID: 52076 RVA: 0x002B7380 File Offset: 0x002B5580
		protected override FormulaExpression Translate(CancellationToken cancellationToken = default(CancellationToken))
		{
			string definitionName = this._options.TransformationFunctionName ?? PythonProgramTranslator.ResolveIdentifierName(this._options.DerivedColumnName, "transform_column");
			bool useSqlDataFrame = this._options.UseSqlDataFrame;
			IPythonTranslationOptions pythonTranslationOptions = new PythonTranslationConstraint
			{
				DefinitionName = definitionName,
				PythonOptimizations = this._options.PythonOptimizations,
				IndentLevel = this._options.IndentLevel,
				IndentSize = this._options.IndentSize,
				LocalizedStrings = this._options.LocalizedStrings,
				MaximumExamplesInComments = this._options.MaximumExamplesInComments
			};
			PythonProgram pythonProgram = PythonProgramTranslator.Translate(base.Program, base.Examples, base.Inputs, pythonTranslationOptions, this._enableMatchUnicode, base.Logger, cancellationToken);
			if (pythonProgram == null || !pythonProgram.Definitions.Any<PythonDefinition>())
			{
				return null;
			}
			PythonDefinition pythonDefinition = pythonProgram.Definitions.SingleOrDefault((PythonDefinition def) => def.Name == definitionName);
			if (pythonDefinition == null)
			{
				return null;
			}
			bool flag = useSqlDataFrame && this._options.PandasOptimizations.HasFlag(PandasOptimizations.UseSeriesFunctions) && PandasProgramTranslator.SupportsSeriesFunctions(base.Program);
			PandasOptimizations pandasOptimizations = this._options.PandasOptimizations;
			if (flag)
			{
				pandasOptimizations |= PandasOptimizations.UseSeriesFunctions;
			}
			else
			{
				pandasOptimizations &= ~PandasOptimizations.UseSeriesFunctions;
			}
			IPandasTranslationOptions options = this._options;
			string text = null;
			string text2 = null;
			PandasOptimizations? pandasOptimizations2 = new PandasOptimizations?(pandasOptimizations);
			IPandasTranslationOptions pandasTranslationOptions = options.With(text, text2, null, null, null, null, null, null, null, pandasOptimizations2, null, null, null);
			pythonDefinition = PandasExpressionOptimizer.Optimize(pythonDefinition, pandasTranslationOptions) as PythonDefinition;
			Example example = base.Examples.First<Example>();
			object obj;
			if (example == null)
			{
				obj = null;
			}
			else
			{
				IEnumerable<string> columnNames = example.Input.ColumnNames;
				if (columnNames == null)
				{
					obj = null;
				}
				else
				{
					obj = columnNames.OrderBy((string col) => col).ToList<string>();
				}
			}
			object obj2 = obj;
			if (obj2 == null)
			{
				throw new Exception("ColumnNames not found");
			}
			IEnumerable<string> enumerable = obj2;
			IEnumerable<FormulaExpression> enumerable2;
			if (flag)
			{
				FormulaExpression formulaExpression = PySparkProgramTranslator.MakePandasUdf(definitionName, enumerable, this.GetReturnType(), "pd_udf_fn");
				FormulaExpression formulaExpression2 = this.InvokePandasUdf(enumerable, "pd_udf_fn");
				enumerable2 = new FormulaExpression[] { formulaExpression, formulaExpression2 };
			}
			else if (enumerable.Count<string>() == 1)
			{
				if (!useSqlDataFrame)
				{
					int num = this._options.DerivedColumnIndex ?? enumerable.Count<string>();
					FormulaExpression formulaExpression2 = PandasExpressionHelper.Insert(PythonExpressionHelper.Variable(this._options.DataFrameName), new FormulaExpression[]
					{
						PythonExpressionHelper.NumberLiteral(num),
						PythonExpressionHelper.StringLiteral(this._options.DerivedColumnName),
						PythonExpressionHelper.Dot(PythonExpressionHelper.Index<object>(PythonExpressionHelper.Variable(this._options.DataFrameName), PythonExpressionHelper.StringLiteral(enumerable.First<string>())), PythonExpressionHelper.Func("apply", new FormulaExpression[] { PythonExpressionHelper.Variable(definitionName, typeof(Func<object, object>)) }))
					});
					enumerable2 = new FormulaExpression[] { formulaExpression2 };
				}
				else
				{
					FormulaExpression formulaExpression3 = PySparkProgramTranslator.MakeUdf(definitionName, enumerable, this.GetReturnType(), "udf_fn");
					FormulaExpression formulaExpression2 = this.InvokeUdf(enumerable, "udf_fn");
					enumerable2 = new FormulaExpression[] { formulaExpression3, formulaExpression2 };
				}
			}
			else if (!useSqlDataFrame)
			{
				FormulaExpression[] array = enumerable.Select((string p) => PythonExpressionHelper.Index<string>(PythonExpressionHelper.Variable("row"), PythonExpressionHelper.StringLiteral(p))).ToArray<FormulaExpression>();
				FormulaExpression formulaExpression4 = PythonExpressionHelper.Func(definitionName, array);
				FormulaExpression formulaExpression5 = PythonExpressionHelper.Dot(PythonExpressionHelper.Index<object>(PythonExpressionHelper.Variable(this._options.DataFrameName), PythonExpressionHelper.Array(enumerable)), PythonExpressionHelper.Func("apply", new FormulaExpression[]
				{
					PythonExpressionHelper.Lambda(PythonExpressionHelper.Variable("row").Yield<FormulaExpression>(), formulaExpression4),
					PythonExpressionHelper.AssignArg(PythonExpressionHelper.Variable("axis"), PythonExpressionHelper.NumberLiteral(1))
				}));
				FormulaExpression formulaExpression6 = PandasExpressionHelper.Merge(PythonExpressionHelper.Variable(this._options.DataFrameName), new FormulaExpression[]
				{
					PandasExpressionHelper.Rename(formulaExpression5, this._options.DerivedColumnName),
					PythonExpressionHelper.AssignArg(PythonExpressionHelper.Variable("left_index"), PythonExpressionHelper.True()),
					PythonExpressionHelper.AssignArg(PythonExpressionHelper.Variable("right_index"), PythonExpressionHelper.True())
				});
				int num2 = this._options.DerivedColumnIndex ?? enumerable.Count<string>();
				FormulaExpression formulaExpression7 = PythonExpressionHelper.Assign(PythonExpressionHelper.Variable("mergedDf"), formulaExpression6);
				FormulaExpression formulaExpression8 = PandasExpressionHelper.Insert(PythonExpressionHelper.Dot(PythonExpressionHelper.Variable(this._options.DataFrameName), "columns"), new FormulaExpression[]
				{
					PythonExpressionHelper.NumberLiteral(num2),
					PythonExpressionHelper.StringLiteral(this._options.DerivedColumnName)
				});
				FormulaExpression formulaExpression9 = PythonExpressionHelper.Assign(PythonExpressionHelper.Variable(this._options.DataFrameName), PandasExpressionHelper.Reindex(PythonExpressionHelper.Variable("mergedDf"), formulaExpression8));
				enumerable2 = new FormulaExpression[] { formulaExpression7, formulaExpression9 };
			}
			else
			{
				FormulaExpression formulaExpression10 = PySparkProgramTranslator.MakeUdf(definitionName, enumerable, this.GetReturnType(), "udf_fn");
				FormulaExpression formulaExpression2 = this.InvokeUdf(enumerable, "udf_fn");
				enumerable2 = new FormulaExpression[] { formulaExpression10, formulaExpression2 };
			}
			List<PythonDefinition> list = pythonProgram.Definitions.Skip(1).ToList<PythonDefinition>();
			list.Insert(0, pythonDefinition);
			IEnumerable<PythonImport> enumerable3 = pythonProgram.Imports;
			if (this._options.ImportPySpark)
			{
				if (!useSqlDataFrame)
				{
					enumerable3 = enumerable3.Concat(new PythonImport[] { PythonExpressionHelper.Import("pyspark.pandas", "ps") });
				}
				else if (!flag)
				{
					enumerable3 = enumerable3.Concat(new PythonImport[]
					{
						PythonExpressionHelper.Import("pyspark.sql", "functions", "F"),
						PythonExpressionHelper.Import("pyspark.sql", "types", "T")
					});
				}
				else
				{
					enumerable3 = enumerable3.Concat(new PythonImport[]
					{
						PythonExpressionHelper.Import("pyspark.sql", "functions", "F"),
						PythonExpressionHelper.Import("pyspark.sql", "types", "T")
					});
				}
			}
			return PySparkExpressionOptimizer.Optimize(PythonExpressionHelper.Program(enumerable3, list, enumerable2, null), this._options);
		}

		// Token: 0x0600CB6D RID: 52077 RVA: 0x002B79FC File Offset: 0x002B5BFC
		private FormulaExpression GetReturnType()
		{
			Example example = base.Examples.FirstOrDefault<Example>();
			object obj = ((example != null) ? example.Output : null);
			FormulaExpression formulaExpression;
			if (!(obj is int))
			{
				if (!(obj is long))
				{
					if (!(obj is short))
					{
						if (!(obj is decimal))
						{
							if (!(obj is float))
							{
								if (!(obj is double))
								{
									if (!(obj is bool))
									{
										if (!(obj is DateTime))
										{
											formulaExpression = PythonExpressionHelper.Dot("T", PythonExpressionHelper.Func("StringType"));
										}
										else
										{
											formulaExpression = PythonExpressionHelper.Dot("T", PythonExpressionHelper.Func("TimestampType"));
										}
									}
									else
									{
										formulaExpression = PythonExpressionHelper.Dot("T", PythonExpressionHelper.Func("BooleanType"));
									}
								}
								else
								{
									formulaExpression = PythonExpressionHelper.Dot("T", PythonExpressionHelper.Func("DoubleType"));
								}
							}
							else
							{
								formulaExpression = PythonExpressionHelper.Dot("T", PythonExpressionHelper.Func("FloatType"));
							}
						}
						else
						{
							formulaExpression = PythonExpressionHelper.Dot("T", PythonExpressionHelper.Func("DecimalType"));
						}
					}
					else
					{
						formulaExpression = PythonExpressionHelper.Dot("T", PythonExpressionHelper.Func("ShortType"));
					}
				}
				else
				{
					formulaExpression = PythonExpressionHelper.Dot("T", PythonExpressionHelper.Func("LongType"));
				}
			}
			else
			{
				formulaExpression = PythonExpressionHelper.Dot("T", PythonExpressionHelper.Func("IntegerType"));
			}
			return formulaExpression;
		}

		// Token: 0x0600CB6E RID: 52078 RVA: 0x002B7B4C File Offset: 0x002B5D4C
		private FormulaExpression InvokePandasUdf(IEnumerable<string> orderedColumnNames, string udfFunName)
		{
			FormulaExpression[] array = orderedColumnNames.Select((string c) => PythonExpressionHelper.Func("col", new FormulaExpression[] { PythonExpressionHelper.StringLiteral(c) })).ToArray<FormulaExpression>();
			FormulaExpression formulaExpression = PythonExpressionHelper.Func(udfFunName, array);
			FormulaExpression formulaExpression2 = PythonExpressionHelper.Dot(PythonExpressionHelper.Variable(this._options.DataFrameName), PythonExpressionHelper.Func("withColumn", new FormulaExpression[]
			{
				PythonExpressionHelper.StringLiteral(this._options.DerivedColumnName),
				formulaExpression
			}));
			return PythonExpressionHelper.Assign(PythonExpressionHelper.Variable(this._options.DataFrameName), formulaExpression2);
		}

		// Token: 0x0600CB6F RID: 52079 RVA: 0x002B7BE0 File Offset: 0x002B5DE0
		private FormulaExpression InvokeUdf(IEnumerable<string> orderedColumnNames, string udfFunName)
		{
			orderedColumnNames = orderedColumnNames.ToReadOnlyList<string>();
			string text = "F";
			FormulaExpression formulaExpression;
			if (orderedColumnNames.Count<string>() != 1)
			{
				string text2 = "struct";
				FormulaExpression[] array = new FormulaExpression[1];
				array[0] = PythonExpressionHelper.Array(orderedColumnNames.Select((string c) => PythonExpressionHelper.Func("col", new FormulaExpression[] { PythonExpressionHelper.StringLiteral(c) })));
				formulaExpression = PythonExpressionHelper.Func(text2, array);
			}
			else
			{
				formulaExpression = PythonExpressionHelper.Func("col", new FormulaExpression[] { PythonExpressionHelper.StringLiteral(orderedColumnNames.First<string>()) });
			}
			FormulaExpression formulaExpression2 = PythonExpressionHelper.Dot(text, formulaExpression);
			FormulaExpression formulaExpression3 = PythonExpressionHelper.Func(udfFunName, new FormulaExpression[] { formulaExpression2 });
			FormulaExpression formulaExpression4 = PythonExpressionHelper.Dot(PythonExpressionHelper.Variable(this._options.DataFrameName), PythonExpressionHelper.Func("withColumn", new FormulaExpression[]
			{
				PythonExpressionHelper.StringLiteral(this._options.DerivedColumnName),
				formulaExpression3
			}));
			return PythonExpressionHelper.Assign(PythonExpressionHelper.Variable(this._options.DataFrameName), formulaExpression4);
		}

		// Token: 0x0600CB70 RID: 52080 RVA: 0x002B7CCC File Offset: 0x002B5ECC
		private static FormulaExpression MakePandasUdf(string definitionName, IEnumerable<string> orderedColumnNames, FormulaExpression returnType, string udfFunName)
		{
			orderedColumnNames = orderedColumnNames.ToReadOnlyList<string>();
			FormulaExpression formulaExpression = PythonExpressionHelper.Variable("v");
			FormulaExpression[] array = new FormulaExpression[] { formulaExpression };
			if (orderedColumnNames.Count<string>() > 1)
			{
				array = orderedColumnNames.Select((string _, int i) => PythonExpressionHelper.Variable(string.Format("v{0}", i))).ToArray<FormulaExpression>();
			}
			FormulaExpression formulaExpression2 = PythonExpressionHelper.Lambda(array, PythonExpressionHelper.Func(definitionName, array));
			FormulaExpression formulaExpression3 = PythonExpressionHelper.Func("pandas_udf", new FormulaExpression[] { formulaExpression2, returnType });
			return PythonExpressionHelper.Assign(PythonExpressionHelper.Variable(udfFunName), formulaExpression3);
		}

		// Token: 0x0600CB71 RID: 52081 RVA: 0x002B7D60 File Offset: 0x002B5F60
		private static FormulaExpression MakeUdf(string definitionName, IEnumerable<string> orderedColumnNames, FormulaExpression returnType, string udfFunName)
		{
			orderedColumnNames = orderedColumnNames.ToReadOnlyList<string>();
			FormulaExpression inputVar = PythonExpressionHelper.Variable("v");
			FormulaExpression[] array = new FormulaExpression[] { inputVar };
			if (orderedColumnNames.Count<string>() > 1)
			{
				array = orderedColumnNames.Select((string _, int i) => PythonExpressionHelper.Index<string>(inputVar, PythonExpressionHelper.NumberLiteral(i))).ToArray<FormulaExpression>();
			}
			FormulaExpression formulaExpression = PythonExpressionHelper.Lambda(inputVar.Yield<FormulaExpression>(), PythonExpressionHelper.Func(definitionName, array));
			FormulaExpression formulaExpression2 = PythonExpressionHelper.Dot("F", PythonExpressionHelper.Func("udf", new FormulaExpression[] { formulaExpression, returnType }));
			return PythonExpressionHelper.Assign(PythonExpressionHelper.Variable(udfFunName), formulaExpression2);
		}

		// Token: 0x04004FD5 RID: 20437
		private readonly bool _enableMatchUnicode;

		// Token: 0x04004FD6 RID: 20438
		private readonly IPySparkTranslationOptions _options;
	}
}
