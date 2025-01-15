using System;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Transformation.Formula.Constraints;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python
{
	// Token: 0x0200183A RID: 6202
	internal class PandasExpressionOptimizer : PythonExpressionOptimizer
	{
		// Token: 0x0600CB47 RID: 52039 RVA: 0x002B6530 File Offset: 0x002B4730
		private PandasExpressionOptimizer(IPandasTranslationOptions options = null)
			: base(null)
		{
			this._options = options ?? new PandasTranslationConstraint();
		}

		// Token: 0x0600CB48 RID: 52040 RVA: 0x002B6549 File Offset: 0x002B4749
		public static FormulaExpression Optimize(PythonDefinition definition, IPandasTranslationOptions options = null)
		{
			return new PandasExpressionOptimizer(options).OptimizeInternal(definition);
		}

		// Token: 0x0600CB49 RID: 52041 RVA: 0x002B6557 File Offset: 0x002B4757
		public static PythonProgram Optimize(PythonProgram program, IPandasTranslationOptions options = null)
		{
			return new PandasExpressionOptimizer(options).OptimizeInternal(program);
		}

		// Token: 0x0600CB4A RID: 52042 RVA: 0x002B6568 File Offset: 0x002B4768
		private PythonDefinition OptimizeInternal(PythonDefinition definition)
		{
			PythonDefinition pythonDefinition = definition;
			if (this._options.PandasOptimizations.HasFlag(PandasOptimizations.UseSeriesFunctions))
			{
				pythonDefinition = PandasExpressionOptimizer.UseSeriesFunctions(pythonDefinition);
			}
			return PythonExpressionOptimizer.AvoidParenthesis(pythonDefinition);
		}

		// Token: 0x0600CB4B RID: 52043 RVA: 0x002B65A4 File Offset: 0x002B47A4
		private PythonProgram OptimizeInternal(PythonProgram program)
		{
			PythonProgram pythonProgram = program;
			if (this._options.PandasOptimizations.HasFlag(PandasOptimizations.UseInlineFunctions))
			{
				pythonProgram = PythonExpressionOptimizer.UseInlineFunctions(pythonProgram);
			}
			return PandasExpressionOptimizer.UsePdIsNull(pythonProgram);
		}

		// Token: 0x0600CB4C RID: 52044 RVA: 0x002B65DF File Offset: 0x002B47DF
		private static PythonProgram UsePdIsNull(PythonProgram program)
		{
			return program.Transform(delegate(FormulaExpression node)
			{
				PythonEqual pythonEqual = node as PythonEqual;
				if (pythonEqual != null)
				{
					PythonVariable pythonVariable = pythonEqual.Left as PythonVariable;
					if (pythonVariable != null && pythonVariable.Name == "None")
					{
						return PandasExpressionOptimizer.<UsePdIsNull>g__IsNull|6_0(pythonEqual.Right);
					}
					PythonVariable pythonVariable2 = pythonEqual.Right as PythonVariable;
					if (pythonVariable2 != null)
					{
						if (pythonVariable2.Name == "None")
						{
							return PandasExpressionOptimizer.<UsePdIsNull>g__IsNull|6_0(pythonEqual.Left);
						}
					}
				}
				return node;
			}) as PythonProgram;
		}

		// Token: 0x0600CB4D RID: 52045 RVA: 0x002B660C File Offset: 0x002B480C
		private static PythonDefinition UseSeriesFunctions(PythonDefinition definition)
		{
			PythonBlock pythonBlock = definition.Body.Transform(delegate(FormulaExpression node)
			{
				PythonOr pythonOr = node as PythonOr;
				if (pythonOr != null)
				{
					FormulaVariable formulaVariable = pythonOr.Left as FormulaVariable;
					if (formulaVariable != null)
					{
						IFormulaTyped formulaTyped = formulaVariable as IFormulaTyped;
						if (formulaTyped != null)
						{
							FormulaStringLiteral formulaStringLiteral = pythonOr.Right as FormulaStringLiteral;
							if (formulaStringLiteral != null)
							{
								if (formulaTyped.Type == typeof(string))
								{
									return PythonExpressionHelper.Dot(pythonOr.Left, PythonExpressionHelper.Func("fillna", new FormulaExpression[] { formulaStringLiteral }));
								}
							}
						}
					}
				}
				else
				{
					PythonIndex pythonIndex = node as PythonIndex;
					if (pythonIndex != null)
					{
						return PythonExpressionHelper.Index<string>(PythonExpressionHelper.Dot(pythonIndex.Subject, "str"), pythonIndex.Index);
					}
					PythonFunc pythonFunc = node as PythonFunc;
					if (pythonFunc != null)
					{
						string name = pythonFunc.Name;
						if (name != null)
						{
							switch (name.Length)
							{
							case 3:
							{
								char c = name[0];
								if (c != 'i')
								{
									if (c != 's')
									{
										goto IL_0483;
									}
									if (!(name == "str"))
									{
										goto IL_0483;
									}
									return PythonExpressionHelper.Dot(PythonExpressionHelper.Parenthesis(pythonFunc.Children[0]), PythonExpressionHelper.Func("astype", new FormulaExpression[] { PythonExpressionHelper.StringLiteral("string") }));
								}
								else
								{
									if (!(name == "int"))
									{
										goto IL_0483;
									}
									return PythonExpressionHelper.Dot(PythonExpressionHelper.Parenthesis(pythonFunc.Children[0]), PythonExpressionHelper.Func("astype", new FormulaExpression[] { PythonExpressionHelper.StringLiteral("int") }));
								}
								break;
							}
							case 4:
							case 6:
								goto IL_0483;
							case 5:
							{
								char c = name[2];
								switch (c)
								{
								case 'l':
									if (!(name == "split"))
									{
										goto IL_0483;
									}
									break;
								case 'm':
								case 'n':
								case 'q':
								case 's':
									goto IL_0483;
								case 'o':
									if (!(name == "float"))
									{
										goto IL_0483;
									}
									return PythonExpressionHelper.Dot(PythonExpressionHelper.Parenthesis(pythonFunc.Children[0]), PythonExpressionHelper.Func("astype", new FormulaExpression[] { PythonExpressionHelper.StringLiteral("float") }));
								case 'p':
									if (!(name == "upper"))
									{
										goto IL_0483;
									}
									break;
								case 'r':
									if (!(name == "strip"))
									{
										goto IL_0483;
									}
									break;
								case 't':
									if (!(name == "title"))
									{
										goto IL_0483;
									}
									break;
								default:
									if (c != 'w')
									{
										goto IL_0483;
									}
									if (!(name == "lower"))
									{
										goto IL_0483;
									}
									break;
								}
								break;
							}
							case 7:
								if (!(name == "replace"))
								{
									goto IL_0483;
								}
								break;
							default:
								goto IL_0483;
							}
							return PythonExpressionHelper.Dot("str", pythonFunc);
						}
					}
					else
					{
						PythonDot pythonDot = node as PythonDot;
						if (pythonDot != null)
						{
							PythonVariable pythonVariable = pythonDot.Accessor as PythonVariable;
							if (pythonVariable != null)
							{
								string name2 = pythonVariable.Name;
								if (name2 == "second" || name2 == "minute" || name2 == "hour" || name2 == "day" || name2 == "month" || name2 == "year")
								{
									return PythonExpressionHelper.Dot(PythonExpressionHelper.Dot(pythonDot.Subject, "dt"), pythonVariable);
								}
							}
						}
						else
						{
							PythonDateQuarter pythonDateQuarter = node as PythonDateQuarter;
							if (pythonDateQuarter != null)
							{
								return PythonExpressionHelper.Dot(PythonExpressionHelper.Dot(pythonDateQuarter.Subject, "dt"), "quarter");
							}
							PythonWeekDay pythonWeekDay = node as PythonWeekDay;
							if (pythonWeekDay != null)
							{
								return PythonExpressionHelper.Plus1(PythonExpressionHelper.Modulo(PythonExpressionHelper.Dot(PythonExpressionHelper.Dot(PythonExpressionHelper.Dot(pythonWeekDay.Subject, "dt"), PythonExpressionHelper.Func("isocalendar")), "day"), 7));
							}
							PythonMonthDays pythonMonthDays = node as PythonMonthDays;
							if (pythonMonthDays != null)
							{
								return PythonExpressionHelper.Dot(PythonExpressionHelper.Dot(pythonMonthDays.Subject, "dt"), "daysinmonth");
							}
							PythonYearDay pythonYearDay = node as PythonYearDay;
							if (pythonYearDay != null)
							{
								return PythonExpressionHelper.Dot(PythonExpressionHelper.Dot(pythonYearDay.Subject, "dt"), "dayofyear");
							}
						}
					}
				}
				IL_0483:
				return node;
			}) as PythonBlock;
			return definition.With(null, null, pythonBlock, null);
		}

		// Token: 0x0600CB4E RID: 52046 RVA: 0x002B6653 File Offset: 0x002B4853
		[CompilerGenerated]
		internal static FormulaExpression <UsePdIsNull>g__IsNull|6_0(FormulaExpression subject)
		{
			return PythonExpressionHelper.Dot("pd", PythonExpressionHelper.Func("isnull", new FormulaExpression[] { subject }));
		}

		// Token: 0x04004FC2 RID: 20418
		private readonly IPandasTranslationOptions _options;
	}
}
