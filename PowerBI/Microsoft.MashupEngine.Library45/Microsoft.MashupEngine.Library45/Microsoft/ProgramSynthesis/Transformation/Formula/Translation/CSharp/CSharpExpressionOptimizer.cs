using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis.Transformation.Formula.Constraints;
using Microsoft.ProgramSynthesis.Transformation.Formula.Extensions;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.CSharp
{
	// Token: 0x02001976 RID: 6518
	internal class CSharpExpressionOptimizer
	{
		// Token: 0x0600D519 RID: 54553 RVA: 0x002D42BE File Offset: 0x002D24BE
		private CSharpExpressionOptimizer(ICSharpTranslationOptions options = null)
		{
			this._options = options ?? new CSharpTranslationConstraint();
		}

		// Token: 0x0600D51A RID: 54554 RVA: 0x002D42E1 File Offset: 0x002D24E1
		public static FormulaExpression Optimize(CSharpMethod method, ICSharpTranslationOptions options = null)
		{
			return new CSharpExpressionOptimizer(options).OptimizeInternal(method);
		}

		// Token: 0x0600D51B RID: 54555 RVA: 0x002D42F0 File Offset: 0x002D24F0
		private FormulaExpression OptimizeInternal(CSharpMethod method)
		{
			CSharpMethod csharpMethod = method;
			if (this._options.Optimizations.HasFlag(CSharpOptimizations.ConsolidateCommonExpressions))
			{
				csharpMethod = this.ConsolidateCommonExpressions(csharpMethod);
			}
			if (this._options.Optimizations.HasFlag(CSharpOptimizations.UseTernary))
			{
				csharpMethod = CSharpExpressionOptimizer.UseTernary(csharpMethod);
			}
			if (this._options.Optimizations.HasFlag(CSharpOptimizations.RemoveTrailingElse))
			{
				csharpMethod = CSharpExpressionOptimizer.RemoveTrailingElse(csharpMethod);
			}
			csharpMethod = CSharpExpressionOptimizer.ConsolidateCommonVariables(csharpMethod);
			return CSharpExpressionOptimizer.AvoidReturnVariable(csharpMethod);
		}

		// Token: 0x0600D51C RID: 54556 RVA: 0x002D437C File Offset: 0x002D257C
		protected static CSharpBlock ConsolidateCommonVariables(CSharpBlock block)
		{
			if (block == null)
			{
				return null;
			}
			IEnumerable<CSharpAssign> assignNodes = block.Nodes.Where(delegate(FormulaExpression node)
			{
				CSharpAssign csharpAssign = node as CSharpAssign;
				return csharpAssign != null && csharpAssign.Left is CSharpVariable && csharpAssign.Right is CSharpVariable;
			}).Cast<CSharpAssign>();
			IEnumerable<FormulaExpression> enumerable = block.Statements.Where((FormulaExpression n) => !assignNodes.Contains(n));
			Dictionary<FormulaExpression, FormulaExpression> dictionary = assignNodes.ToLookup((CSharpAssign n) => n.Left, (CSharpAssign n) => n.Right).ToDictionary((IGrouping<FormulaExpression, FormulaExpression> g) => g.Key, (IGrouping<FormulaExpression, FormulaExpression> g) => g.First<FormulaExpression>());
			return CSharpExpressionHelper.Block(enumerable.Substitute(dictionary).Where(delegate(FormulaExpression n)
			{
				CSharpVar csharpVar = n as CSharpVar;
				if (csharpVar != null)
				{
					CSharpAssign csharpAssign2 = csharpVar.Subject as CSharpAssign;
					if (csharpAssign2 != null)
					{
						CSharpVariable csharpVariable = csharpAssign2.Left as CSharpVariable;
						if (csharpVariable != null)
						{
							CSharpVariable csharpVariable2 = csharpAssign2.Right as CSharpVariable;
							if (csharpVariable2 != null)
							{
								return csharpVariable != csharpVariable2;
							}
						}
					}
				}
				return true;
			}));
		}

		// Token: 0x0600D51D RID: 54557 RVA: 0x002D44A2 File Offset: 0x002D26A2
		private static CSharpMethod AvoidReturnVariable(CSharpMethod method)
		{
			if (method == null)
			{
				return null;
			}
			return method.Transform<CSharpMethod>(delegate(FormulaExpression node)
			{
				CSharpBlock csharpBlock = node as CSharpBlock;
				if (csharpBlock != null)
				{
					return CSharpExpressionOptimizer.AvoidReturnVariable(csharpBlock);
				}
				return node;
			});
		}

		// Token: 0x0600D51E RID: 54558 RVA: 0x002D44D4 File Offset: 0x002D26D4
		private static CSharpBlock AvoidReturnVariable(CSharpBlock block)
		{
			if (block == null)
			{
				return null;
			}
			List<FormulaExpression> list = block.Statements.ToList<FormulaExpression>();
			CSharpVariable csharpVariable = block.Statements.LastOrDefault<FormulaExpression>() as CSharpVariable;
			if (csharpVariable == null)
			{
				return block;
			}
			list.RemoveAt(list.Count - 1);
			CSharpVar csharpVar = list.LastOrDefault<FormulaExpression>() as CSharpVar;
			if (csharpVar != null)
			{
				CSharpAssign csharpAssign = csharpVar.Subject as CSharpAssign;
				if (csharpAssign != null && !(csharpAssign.Left != csharpVariable))
				{
					list.RemoveAt(list.Count - 1);
					list.Add(csharpAssign.Right);
					return CSharpExpressionHelper.Block(list);
				}
			}
			return block;
		}

		// Token: 0x0600D51F RID: 54559 RVA: 0x002D4570 File Offset: 0x002D2770
		private CSharpMethod ConsolidateCommonExpressions(CSharpMethod method)
		{
			if (method == null)
			{
				return null;
			}
			return method.Transform<CSharpMethod>(delegate(FormulaExpression node)
			{
				CSharpBlock csharpBlock = node as CSharpBlock;
				if (csharpBlock != null)
				{
					return this.ConsolidateCommonExpressions(csharpBlock);
				}
				return node;
			});
		}

		// Token: 0x0600D520 RID: 54560 RVA: 0x002D4590 File Offset: 0x002D2790
		private CSharpBlock ConsolidateCommonExpressions(CSharpBlock block)
		{
			IReadOnlyList<FormulaExpression> statements = block.Statements;
			var list = (from node in statements.ExtractNodes().ToList<FormulaExpression>().Where(delegate(FormulaExpression node)
				{
					bool flag = node is CSharpIf || node is CSharpTernary;
					return !flag;
				})
				group node by node into uniqueGroup
				let source = uniqueGroup.First<FormulaExpression>()
				let replacement = this.<ConsolidateCommonExpressions>g__ToExpression|9_0(source, uniqueGroup.Count<FormulaExpression>())
				where replacement != null
				select new
				{
					Source = source,
					Replacement = replacement
				}).ToList();
			Dictionary<FormulaExpression, FormulaExpression> dictionary = list.ToDictionary(g => g.Source, g => g.Replacement);
			if (!dictionary.Any<KeyValuePair<FormulaExpression, FormulaExpression>>())
			{
				return block;
			}
			IEnumerable<FormulaExpression> enumerable = statements.Substitute(dictionary);
			List<FormulaExpression> list2 = ((enumerable != null) ? enumerable.ToList<FormulaExpression>() : null);
			if (list2 == null)
			{
				return block;
			}
			using (var enumerator = list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					<>f__AnonymousType25<FormulaExpression, FormulaExpression> source = enumerator.Current;
					Func<FormulaExpression, bool> <>9__10;
					FormulaExpression formulaExpression = list2.FirstOrDefault(delegate(FormulaExpression statement)
					{
						IEnumerable<FormulaExpression> nodes = statement.Nodes;
						Func<FormulaExpression, bool> func;
						if ((func = <>9__10) == null)
						{
							func = (<>9__10 = (FormulaExpression node) => node == source.Replacement);
						}
						return nodes.Any(func);
					});
					int num = list2.IndexOf(formulaExpression);
					num = ((num <= 0) ? 0 : (num - 1));
					list2.Insert(num, CSharpExpressionHelper.Var(CSharpExpressionHelper.Assign(source.Replacement, source.Source)));
				}
			}
			return CSharpExpressionHelper.Block(list2);
		}

		// Token: 0x0600D521 RID: 54561 RVA: 0x002D4788 File Offset: 0x002D2988
		private static CSharpMethod ConsolidateCommonVariables(CSharpMethod method)
		{
			if (method == null)
			{
				return null;
			}
			return method.Transform<CSharpMethod>(delegate(FormulaExpression node)
			{
				CSharpBlock csharpBlock = node as CSharpBlock;
				if (csharpBlock != null)
				{
					return CSharpExpressionOptimizer.ConsolidateCommonVariables(csharpBlock);
				}
				return node;
			});
		}

		// Token: 0x0600D522 RID: 54562 RVA: 0x002D47BC File Offset: 0x002D29BC
		private static CSharpMethod RemoveTrailingElse(CSharpMethod method)
		{
			if (method == null)
			{
				return null;
			}
			if (!(method.Body.Children.LastOrDefault<FormulaExpression>() is CSharpIf))
			{
				return method;
			}
			int num = 0;
			for (;;)
			{
				FormulaExpression formulaExpression = method.Body.Children.LastOrDefault<FormulaExpression>();
				CSharpIf lastIf = formulaExpression as CSharpIf;
				if (lastIf == null || lastIf.TrueBlock == null || lastIf.FalseBlock == null || num++ > 20)
				{
					return method;
				}
				method = method.Transform<CSharpMethod>(delegate(FormulaExpression node)
				{
					if (!(node != lastIf))
					{
						return CSharpExpressionHelper.If(lastIf.Condition, lastIf.TrueBlock);
					}
					return node;
				});
				if (method == null)
				{
					break;
				}
				List<FormulaExpression> list = method.Body.Statements.ToList<FormulaExpression>();
				list.AddRange(lastIf.FalseBlock.Statements);
				method = CSharpExpressionHelper.Method(method.Name, method.ReturnType, method.Parameters, list, "public");
			}
			return null;
		}

		// Token: 0x0600D523 RID: 54563 RVA: 0x002D48AE File Offset: 0x002D2AAE
		private static CSharpMethod UseTernary(FormulaExpression method)
		{
			if (method == null)
			{
				return null;
			}
			return method.Transform<CSharpMethod>(delegate(FormulaExpression node, FormulaTransformNodeInfo info)
			{
				FormulaExpression formulaExpression = null;
				CSharpIf csharpIf = node as CSharpIf;
				bool flag = csharpIf != null;
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
					bool flag2 = formulaExpression3 is CSharpIf || formulaExpression3 is CSharpTernary;
					flag = !flag2;
				}
				bool flag3;
				if (flag)
				{
					FormulaExpression condition = csharpIf.Condition;
					if (condition != null && condition.ToString().Length < 30)
					{
						CSharpBlock trueBlock = csharpIf.TrueBlock;
						if (trueBlock != null && trueBlock.Statements.Count == 1)
						{
							CSharpBlock falseBlock = csharpIf.FalseBlock;
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
					FormulaExpression formulaExpression3 = csharpIf.TrueBlock.Statements.Single<FormulaExpression>();
					bool flag2 = formulaExpression3 is CSharpIf || formulaExpression3 is CSharpTernary;
					bool flag5 = flag2;
					if (!flag5)
					{
						FormulaExpression formulaExpression4 = csharpIf.FalseBlock.Statements.Single<FormulaExpression>();
						bool flag6 = formulaExpression4 is CSharpIf || formulaExpression4 is CSharpTernary;
						flag5 = flag6;
					}
					flag4 = !flag5;
				}
				if (flag4)
				{
					formulaExpression = CSharpExpressionHelper.Ternary(csharpIf.Condition, csharpIf.TrueBlock.Statements.Single<FormulaExpression>(), csharpIf.FalseBlock.Statements.Single<FormulaExpression>());
				}
				if (formulaExpression == null || formulaExpression.ToString().Length >= 100)
				{
					return node;
				}
				return formulaExpression;
			});
		}

		// Token: 0x0600D525 RID: 54565 RVA: 0x002D4900 File Offset: 0x002D2B00
		[CompilerGenerated]
		private FormulaExpression <ConsolidateCommonExpressions>g__ToExpression|9_0(FormulaExpression node, int count)
		{
			CSharpCultureInfo csharpCultureInfo = node as CSharpCultureInfo;
			var <>f__AnonymousType;
			if (csharpCultureInfo != null)
			{
				CSharpStringLiteral csharpStringLiteral = csharpCultureInfo.Locale as CSharpStringLiteral;
				if (csharpStringLiteral != null)
				{
					<>f__AnonymousType = new
					{
						Name = csharpStringLiteral.Value.Replace("-", string.Empty) + "Culture",
						Type = typeof(CultureInfo)
					};
					goto IL_004C;
				}
			}
			<>f__AnonymousType = null;
			IL_004C:
			var <>f__AnonymousType2 = <>f__AnonymousType;
			if (<>f__AnonymousType2 != null)
			{
				return CSharpExpressionHelper.Variable(<>f__AnonymousType2.Name, <>f__AnonymousType2.Type);
			}
			if (count <= 1)
			{
				return null;
			}
			CSharpDot csharpDot = node as CSharpDot;
			if (csharpDot != null)
			{
				FormulaFunc formulaFunc = csharpDot.Accessor as FormulaFunc;
				if (formulaFunc != null)
				{
					string name = formulaFunc.Name;
					if (name == "IndexOf")
					{
						<>f__AnonymousType = new
						{
							Name = "indexOf",
							Type = typeof(int)
						};
						goto IL_01F9;
					}
					if (name == "LastIndexOf")
					{
						<>f__AnonymousType = new
						{
							Name = "lastIndexOf",
							Type = typeof(int)
						};
						goto IL_01F9;
					}
					if (name == "AllIndexesOf")
					{
						<>f__AnonymousType = new
						{
							Name = "allIndexesOf",
							Type = typeof(int[])
						};
						goto IL_01F9;
					}
					if (name == "Split")
					{
						<>f__AnonymousType = new
						{
							Name = "split",
							Type = typeof(string[])
						};
						goto IL_01F9;
					}
					if (name == "Regex.Match")
					{
						<>f__AnonymousType = new
						{
							Name = "match",
							Type = typeof(Match)
						};
						goto IL_01F9;
					}
					FormulaFunc formulaFunc2 = csharpDot.Subject as FormulaFunc;
					if (formulaFunc2 != null && formulaFunc2.Name == "Regex.Matches")
					{
						if (name == "ToArray")
						{
							<>f__AnonymousType = new
							{
								Name = "matches",
								Type = typeof(Match[])
							};
							goto IL_01F9;
						}
					}
				}
			}
			else
			{
				CSharpFunc csharpFunc = node as CSharpFunc;
				if (csharpFunc != null)
				{
					if (csharpFunc.Name == "Regex.Match")
					{
						<>f__AnonymousType = new
						{
							Name = "match",
							Type = typeof(Match)
						};
						goto IL_01F9;
					}
				}
			}
			<>f__AnonymousType = null;
			IL_01F9:
			<>f__AnonymousType2 = <>f__AnonymousType;
			if (<>f__AnonymousType2 != null)
			{
				return CSharpExpressionHelper.Variable(string.Format("{0}{1}", <>f__AnonymousType2.Name, this._varNameCount.GetAndIncrement(<>f__AnonymousType2.Name)));
			}
			return null;
		}

		// Token: 0x040051A9 RID: 20905
		private readonly ICSharpTranslationOptions _options;

		// Token: 0x040051AA RID: 20906
		private readonly Dictionary<string, int> _varNameCount = new Dictionary<string, int>();
	}
}
