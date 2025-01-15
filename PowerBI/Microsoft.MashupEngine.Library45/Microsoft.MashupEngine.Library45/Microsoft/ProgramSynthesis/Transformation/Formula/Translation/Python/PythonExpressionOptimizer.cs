using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Transformation.Formula.Constraints;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python
{
	// Token: 0x02001886 RID: 6278
	internal class PythonExpressionOptimizer
	{
		// Token: 0x0600CD2B RID: 52523 RVA: 0x002BACE8 File Offset: 0x002B8EE8
		protected PythonExpressionOptimizer(IPythonTranslationOptions options = null)
		{
			this._options = options ?? new PythonTranslationConstraint();
		}

		// Token: 0x0600CD2C RID: 52524 RVA: 0x002BAD00 File Offset: 0x002B8F00
		public static FormulaExpression Optimize(PythonDefinition definition, IPythonTranslationOptions options = null)
		{
			return new PythonExpressionOptimizer(options).OptimizeInternal(definition);
		}

		// Token: 0x0600CD2D RID: 52525 RVA: 0x002BAD10 File Offset: 0x002B8F10
		private PythonDefinition OptimizeInternal(PythonDefinition definition)
		{
			PythonDefinition pythonDefinition = definition;
			if (this._options.PythonOptimizations.HasFlag(PythonOptimizations.RemoveUnusedParameters))
			{
				pythonDefinition = PythonExpressionOptimizer.RemoveUnusedParameters(pythonDefinition);
			}
			if (this._options.PythonOptimizations.HasFlag(PythonOptimizations.ConsolidateCommonExpressions))
			{
				pythonDefinition = PythonExpressionOptimizer.ConsolidateCommonExpressions(pythonDefinition);
			}
			if (this._options.PythonOptimizations.HasFlag(PythonOptimizations.UseTernary))
			{
				pythonDefinition = PythonExpressionOptimizer.UseTernary(pythonDefinition);
			}
			if (this._options.PythonOptimizations.HasFlag(PythonOptimizations.RemoveTrailingElse))
			{
				pythonDefinition = PythonExpressionOptimizer.RemoveTrailingElse(pythonDefinition);
			}
			if (this._options.PythonOptimizations.HasFlag(PythonOptimizations.ConsolidateCommonVariables))
			{
				pythonDefinition = PythonExpressionOptimizer.ConsolidateCommonVariables(pythonDefinition);
			}
			return PythonExpressionOptimizer.AvoidReturnVariable(pythonDefinition);
		}

		// Token: 0x0600CD2E RID: 52526 RVA: 0x002BADDC File Offset: 0x002B8FDC
		protected static PythonProgram UseInlineFunctions(PythonProgram program)
		{
			if (program == null)
			{
				return null;
			}
			Dictionary<string, PythonDefinition> defLookup = program.Definitions.Where((PythonDefinition def) => def.Body.Statements.Count == 1).ToDictionary((PythonDefinition def) => def.Name, (PythonDefinition def) => def);
			if (!defLookup.Any<KeyValuePair<string, PythonDefinition>>())
			{
				return program;
			}
			PythonProgram pythonProgram = program.Transform(delegate(FormulaExpression node)
			{
				PythonFunc pythonFunc = node as PythonFunc;
				FormulaExpression formulaExpression;
				if (pythonFunc == null)
				{
					PythonVariable pythonVariable = node as PythonVariable;
					if (pythonVariable == null)
					{
						formulaExpression = node;
					}
					else
					{
						formulaExpression = base.<UseInlineFunctions>g__TransformVariable|4(pythonVariable);
					}
				}
				else
				{
					formulaExpression = base.<UseInlineFunctions>g__TransformFunc|3(pythonFunc);
				}
				return formulaExpression;
			}) as PythonProgram;
			if (pythonProgram == null)
			{
				return program;
			}
			IEnumerable<PythonDefinition> enumerable = pythonProgram.Definitions.Where((PythonDefinition def) => !defLookup.ContainsKey(def.Name));
			string text = string.Join(Environment.NewLine, from def in defLookup.Values
				select def.Comment.Comment into comment
				where !string.IsNullOrEmpty(comment)
				select comment);
			return pythonProgram.With(null, enumerable, null, PythonExpressionHelper.Comment(text, PythonCommentType.Comment));
		}

		// Token: 0x0600CD2F RID: 52527 RVA: 0x002BAF25 File Offset: 0x002B9125
		protected static PythonDefinition AvoidParenthesis(PythonDefinition definition)
		{
			return definition.Transform<PythonDefinition>(delegate(FormulaExpression node)
			{
				FormulaParenthesis formulaParenthesis = node as FormulaParenthesis;
				if (formulaParenthesis != null && !(formulaParenthesis.Body is FormulaBinaryOperator))
				{
					return formulaParenthesis.Body;
				}
				return node;
			});
		}

		// Token: 0x0600CD30 RID: 52528 RVA: 0x002BAF4C File Offset: 0x002B914C
		private static PythonDefinition AvoidReturnVariable(PythonDefinition definition)
		{
			if (definition == null)
			{
				return null;
			}
			PythonBlock body = definition.Body;
			List<FormulaExpression> list = body.Statements.ToList<FormulaExpression>();
			PythonVariable pythonVariable = body.Statements.LastOrDefault<FormulaExpression>() as PythonVariable;
			if (pythonVariable == null)
			{
				return definition;
			}
			list.RemoveAt(list.Count - 1);
			PythonAssign pythonAssign = list.LastOrDefault<FormulaExpression>() as PythonAssign;
			if (pythonAssign == null || pythonAssign.Left != pythonVariable)
			{
				return definition;
			}
			list.RemoveAt(list.Count - 1);
			list.Add(pythonAssign.Right);
			return definition.With(null, null, body, null);
		}

		// Token: 0x0600CD31 RID: 52529 RVA: 0x002BAFE4 File Offset: 0x002B91E4
		private static PythonDefinition ConsolidateCommonExpressions(PythonDefinition definition)
		{
			if (definition == null)
			{
				return null;
			}
			Dictionary<string, int> varNameCount = new Dictionary<string, int>();
			Dictionary<FormulaExpression, FormulaExpression> dictionary = (from source in definition.NodeDetails
				group source by source.Node into uniqueGroup
				let first = uniqueGroup.First<FormulaExpressionDetail>()
				let exp = base.<ConsolidateCommonExpressions>g__ToExpression|0(first.Node, uniqueGroup.Count<FormulaExpressionDetail>())
				where exp != null
				orderby first.Order
				select new
				{
					Node = first.Node,
					Expression = exp
				}).ToDictionary(g => g.Node, g => g.Expression);
			if (!dictionary.Any<KeyValuePair<FormulaExpression, FormulaExpression>>())
			{
				return definition;
			}
			PythonDefinition pythonDefinition = definition.Substitute(dictionary) as PythonDefinition;
			if (pythonDefinition == null)
			{
				return definition;
			}
			List<FormulaExpression> list = pythonDefinition.Body.Statements.ToList<FormulaExpression>();
			int num = 0;
			foreach (FormulaExpression formulaExpression in dictionary.Keys)
			{
				list.Insert(num++, PythonExpressionHelper.Assign(dictionary[formulaExpression], formulaExpression));
			}
			return pythonDefinition.With(null, null, new PythonBlock(list), null);
		}

		// Token: 0x0600CD32 RID: 52530 RVA: 0x002BB1C0 File Offset: 0x002B93C0
		private static PythonDefinition ConsolidateCommonVariables(PythonDefinition definition)
		{
			if (definition == null)
			{
				return null;
			}
			PythonBlock pythonBlock = definition.Body;
			IEnumerable<PythonAssign> assignNodes = pythonBlock.Nodes.Where(delegate(FormulaExpression node)
			{
				PythonAssign pythonAssign = node as PythonAssign;
				return pythonAssign != null && pythonAssign.Left is PythonVariable && pythonAssign.Right is PythonVariable;
			}).Cast<PythonAssign>();
			pythonBlock = PythonExpressionHelper.Block(pythonBlock.Statements.Where((FormulaExpression n) => !assignNodes.Contains(n)));
			Dictionary<FormulaExpression, FormulaExpression> dictionary = assignNodes.ToLookup((PythonAssign n) => n.Left, (PythonAssign n) => n.Right).ToDictionary((IGrouping<FormulaExpression, FormulaExpression> g) => g.Key, (IGrouping<FormulaExpression, FormulaExpression> g) => g.First<FormulaExpression>());
			pythonBlock = pythonBlock.Substitute<PythonBlock>(dictionary);
			if (!(pythonBlock == null))
			{
				return definition.With(null, null, pythonBlock, null);
			}
			return null;
		}

		// Token: 0x0600CD33 RID: 52531 RVA: 0x002BB2E4 File Offset: 0x002B94E4
		private static PythonDefinition RemoveTrailingElse(PythonDefinition definition)
		{
			if (definition == null)
			{
				return null;
			}
			if (!(definition.Body.Children.LastOrDefault<FormulaExpression>() is PythonIf))
			{
				return definition;
			}
			int num = 0;
			for (;;)
			{
				FormulaExpression formulaExpression = definition.Body.Children.LastOrDefault<FormulaExpression>();
				PythonIf lastIf = formulaExpression as PythonIf;
				if (lastIf == null || lastIf.TrueBlock == null || lastIf.FalseBlock == null || num++ > 20)
				{
					return definition;
				}
				definition = definition.Transform<PythonDefinition>(delegate(FormulaExpression node)
				{
					if (!(node != lastIf))
					{
						return PythonExpressionHelper.If(lastIf.Condition, lastIf.TrueBlock);
					}
					return node;
				});
				if (definition == null)
				{
					break;
				}
				List<FormulaExpression> list = definition.Body.Statements.ToList<FormulaExpression>();
				list.AddRange(lastIf.FalseBlock.Statements);
				definition = definition.With(null, null, new PythonBlock(list), null);
			}
			return null;
		}

		// Token: 0x0600CD34 RID: 52532 RVA: 0x002BB3C4 File Offset: 0x002B95C4
		private static PythonDefinition RemoveUnusedParameters(PythonDefinition definition)
		{
			if (definition == null)
			{
				return null;
			}
			IEnumerable<FormulaExpression> inputSelectors = definition.Body.Nodes.Where(delegate(FormulaExpression node)
			{
				PythonVariable pythonVariable = node as PythonVariable;
				return pythonVariable != null && definition.Parameters.Contains(pythonVariable);
			});
			IEnumerable<PythonVariable> enumerable = definition.Parameters.Where((PythonVariable p) => inputSelectors.Contains(p));
			return definition.With(null, enumerable.ToArray<PythonVariable>(), null, null);
		}

		// Token: 0x0600CD35 RID: 52533 RVA: 0x002BB446 File Offset: 0x002B9646
		private static PythonDefinition UseTernary(PythonDefinition definition)
		{
			if (definition == null)
			{
				return null;
			}
			return definition.Transform<PythonDefinition>(delegate(FormulaExpression node, FormulaTransformNodeInfo info)
			{
				FormulaExpression formulaExpression = null;
				PythonIf pythonIf = node as PythonIf;
				bool flag = pythonIf != null;
				if (flag)
				{
					FormulaTransformNodeInfo parent = info.Parent;
					FormulaExpression formulaExpression2;
					if (parent == null)
					{
						formulaExpression2 = null;
					}
					else
					{
						FormulaTransformNodeInfo parent2 = parent.Parent;
						formulaExpression2 = ((parent2 != null) ? parent2.Node : null);
					}
					FormulaExpression formulaExpression3 = formulaExpression2;
					bool flag2 = formulaExpression3 is PythonIf || formulaExpression3 is PythonTernary;
					flag = !flag2;
				}
				bool flag3;
				if (flag)
				{
					FormulaExpression condition = pythonIf.Condition;
					if (condition != null && condition.ToString().Length < 30)
					{
						PythonBlock trueBlock = pythonIf.TrueBlock;
						if (trueBlock != null && trueBlock.Statements.Count == 1)
						{
							PythonBlock falseBlock = pythonIf.FalseBlock;
							flag3 = falseBlock != null && falseBlock.Statements.Count == 1;
							goto IL_00AA;
						}
					}
				}
				flag3 = false;
				IL_00AA:
				bool flag4 = flag3;
				if (flag4)
				{
					FormulaExpression formulaExpression3 = pythonIf.TrueBlock.Statements.Single<FormulaExpression>();
					bool flag2 = formulaExpression3 is PythonIf || formulaExpression3 is PythonTernary;
					bool flag5 = flag2;
					if (!flag5)
					{
						FormulaExpression formulaExpression4 = pythonIf.FalseBlock.Statements.Single<FormulaExpression>();
						bool flag6 = formulaExpression4 is PythonIf || formulaExpression4 is PythonTernary;
						flag5 = flag6;
					}
					flag4 = !flag5;
				}
				if (flag4)
				{
					formulaExpression = PythonExpressionHelper.Ternary(pythonIf.Condition, pythonIf.TrueBlock.Statements.Single<FormulaExpression>(), pythonIf.FalseBlock.Statements.Single<FormulaExpression>());
				}
				if (formulaExpression == null || formulaExpression.ToString().Length >= 100)
				{
					return node;
				}
				return formulaExpression;
			});
		}

		// Token: 0x0400502D RID: 20525
		private readonly IPythonTranslationOptions _options;
	}
}
