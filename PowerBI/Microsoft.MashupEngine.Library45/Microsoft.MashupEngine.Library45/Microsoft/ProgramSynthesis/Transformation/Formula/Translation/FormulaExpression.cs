using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;
using Microsoft.ProgramSynthesis.Transformation.Formula.Translation.CSharp;
using Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerQuery;
using Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation
{
	// Token: 0x020017E5 RID: 6117
	public abstract class FormulaExpression : IEquatable<FormulaExpression>, IComparable<FormulaExpression>
	{
		// Token: 0x170021EB RID: 8683
		// (get) Token: 0x0600C977 RID: 51575 RVA: 0x002B2461 File Offset: 0x002B0661
		// (set) Token: 0x0600C978 RID: 51576 RVA: 0x002B2469 File Offset: 0x002B0669
		public IReadOnlyList<FormulaExpression> Children { get; set; }

		// Token: 0x170021EC RID: 8684
		// (get) Token: 0x0600C979 RID: 51577 RVA: 0x002B2474 File Offset: 0x002B0674
		public IReadOnlyList<FormulaExpressionDetail> NodeDetails
		{
			get
			{
				IReadOnlyList<FormulaExpressionDetail> readOnlyList;
				if ((readOnlyList = this._nodeDetails) == null)
				{
					readOnlyList = (this._nodeDetails = this.LoadNodeDetails());
				}
				return readOnlyList;
			}
		}

		// Token: 0x170021ED RID: 8685
		// (get) Token: 0x0600C97A RID: 51578 RVA: 0x002B249C File Offset: 0x002B069C
		public IReadOnlyList<FormulaExpression> Nodes
		{
			get
			{
				IReadOnlyList<FormulaExpression> readOnlyList;
				if ((readOnlyList = this._nodes) == null)
				{
					readOnlyList = (this._nodes = this.LoadNodes());
				}
				return readOnlyList;
			}
		}

		// Token: 0x0600C97B RID: 51579 RVA: 0x002B24C2 File Offset: 0x002B06C2
		public virtual T Accept<T>(IFormulaVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x0600C97C RID: 51580 RVA: 0x002B24CB File Offset: 0x002B06CB
		public virtual void Accept(IFormulaVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x0600C97D RID: 51581
		public abstract FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor);

		// Token: 0x0600C97E RID: 51582 RVA: 0x002B24D4 File Offset: 0x002B06D4
		public int CompareTo(FormulaExpression other)
		{
			if (!(other == null) && !(base.GetType() != other.GetType()))
			{
				return string.CompareOrdinal(this.ToString(), other.ToString());
			}
			return -1;
		}

		// Token: 0x0600C97F RID: 51583 RVA: 0x002B2505 File Offset: 0x002B0705
		public bool Equals(FormulaExpression other)
		{
			return other != null && base.GetType() == other.GetType() && this.ToString() == other.ToString();
		}

		// Token: 0x0600C980 RID: 51584 RVA: 0x002B2536 File Offset: 0x002B0736
		public override bool Equals(object other)
		{
			return this.Equals(other as FormulaExpression);
		}

		// Token: 0x0600C981 RID: 51585 RVA: 0x002B2544 File Offset: 0x002B0744
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode() + base.GetType().GetHashCode();
		}

		// Token: 0x0600C982 RID: 51586 RVA: 0x002B255D File Offset: 0x002B075D
		public static bool operator ==(FormulaExpression left, FormulaExpression right)
		{
			return (left == null && right == null) || (left != null && left.Equals(right));
		}

		// Token: 0x0600C983 RID: 51587 RVA: 0x002B2573 File Offset: 0x002B0773
		public static bool operator !=(FormulaExpression left, FormulaExpression right)
		{
			return !(left == right);
		}

		// Token: 0x0600C984 RID: 51588 RVA: 0x002B2580 File Offset: 0x002B0780
		public string ToAnonymizedString()
		{
			Dictionary<FormulaExpression, FormulaExpression> variableLookup = new Dictionary<FormulaExpression, FormulaExpression>();
			Dictionary<string, string> definitionNameLookup = new Dictionary<string, string>();
			FormulaExpression formulaExpression = this.Transform(delegate(FormulaExpression node)
			{
				FormulaExpression formulaExpression2;
				if (!(node is PythonComment))
				{
					PythonDefinition pythonDefinition = node as PythonDefinition;
					if (pythonDefinition == null)
					{
						CSharpMethod csharpMethod = node as CSharpMethod;
						if (csharpMethod == null)
						{
							PowerQueryLookup powerQueryLookup = node as PowerQueryLookup;
							if (powerQueryLookup == null)
							{
								FormulaVariable formulaVariable = node as FormulaVariable;
								if (formulaVariable == null)
								{
									FormulaStringLiteral formulaStringLiteral = node as FormulaStringLiteral;
									if (formulaStringLiteral == null)
									{
										FormulaRegexLiteral formulaRegexLiteral = node as FormulaRegexLiteral;
										if (formulaRegexLiteral == null)
										{
											FormulaNumberLiteral formulaNumberLiteral = node as FormulaNumberLiteral;
											if (formulaNumberLiteral == null)
											{
												FormulaDateTimeLiteral formulaDateTimeLiteral = node as FormulaDateTimeLiteral;
												if (formulaDateTimeLiteral == null)
												{
													formulaExpression2 = node;
												}
												else
												{
													formulaExpression2 = new FormulaRaw(formulaDateTimeLiteral.ToCodeString().ToAnonymizedString());
												}
											}
											else
											{
												formulaExpression2 = new FormulaRaw(formulaNumberLiteral.ToCodeString().ToAnonymizedString());
											}
										}
										else
										{
											formulaExpression2 = new FormulaRaw(formulaRegexLiteral.ToCodeString().ToAnonymizedString());
										}
									}
									else
									{
										formulaExpression2 = new FormulaRaw(formulaStringLiteral.ToCodeString().ToAnonymizedString());
									}
								}
								else
								{
									formulaExpression2 = base.<ToAnonymizedString>g__ResolveVariable|0(formulaVariable);
								}
							}
							else
							{
								formulaExpression2 = powerQueryLookup.CloneWith(powerQueryLookup.Name.ToAnonymizedString());
							}
						}
						else
						{
							formulaExpression2 = base.<ToAnonymizedString>g__ResolveCSharpMethod|3(csharpMethod);
						}
					}
					else
					{
						formulaExpression2 = base.<ToAnonymizedString>g__ResolvePythonDefinition|1(pythonDefinition);
					}
				}
				else
				{
					formulaExpression2 = PythonComment.Empty;
				}
				return formulaExpression2;
			});
			if (formulaExpression == null)
			{
				return null;
			}
			return formulaExpression.Transform(delegate(FormulaExpression node)
			{
				PythonFunc pythonFunc = node as PythonFunc;
				FormulaExpression formulaExpression3;
				if (pythonFunc == null)
				{
					CSharpFunc csharpFunc = node as CSharpFunc;
					if (csharpFunc == null)
					{
						formulaExpression3 = node;
					}
					else
					{
						formulaExpression3 = base.<ToAnonymizedString>g__ResolveCSharpFunc|4(csharpFunc);
					}
				}
				else
				{
					formulaExpression3 = base.<ToAnonymizedString>g__ResolvePythonFunc|2(pythonFunc);
				}
				return formulaExpression3;
			}).ToString();
		}

		// Token: 0x0600C985 RID: 51589 RVA: 0x002B25D8 File Offset: 0x002B07D8
		public override string ToString()
		{
			string text;
			if ((text = this._codeString) == null)
			{
				text = (this._codeString = this.ToCodeString());
			}
			return text;
		}

		// Token: 0x0600C986 RID: 51590
		protected abstract string ToCodeString();

		// Token: 0x0600C987 RID: 51591 RVA: 0x002B25FE File Offset: 0x002B07FE
		private IReadOnlyList<FormulaExpressionDetail> LoadNodeDetails()
		{
			return FormulaExtractVisitor.Extract(this).ToReadOnlyList<FormulaExpressionDetail>();
		}

		// Token: 0x0600C988 RID: 51592 RVA: 0x002B260B File Offset: 0x002B080B
		private IReadOnlyList<FormulaExpression> LoadNodes()
		{
			return this.NodeDetails.Select((FormulaExpressionDetail result) => result.Node).ToReadOnlyList<FormulaExpression>();
		}

		// Token: 0x0600C989 RID: 51593 RVA: 0x002B263C File Offset: 0x002B083C
		public FormulaExpression Substitute(Func<FormulaExpression, bool> extractSelector, Func<IEnumerable<FormulaExpression>, Dictionary<FormulaExpression, FormulaExpression>> substituteSelector)
		{
			return FormulaSubstitutionVisitor.Substitute(this, extractSelector, substituteSelector);
		}

		// Token: 0x0600C98A RID: 51594 RVA: 0x002B2646 File Offset: 0x002B0846
		internal TResult Substitute<TResult>(IReadOnlyDictionary<FormulaExpression, FormulaExpression> substitutions) where TResult : FormulaExpression
		{
			return this.Substitute(substitutions) as TResult;
		}

		// Token: 0x0600C98B RID: 51595 RVA: 0x002B2659 File Offset: 0x002B0859
		internal TResult Substitute<TResult>(Func<FormulaExpression, bool> extractSelector, Func<IEnumerable<FormulaExpression>, Dictionary<FormulaExpression, FormulaExpression>> substituteSelector) where TResult : FormulaExpression
		{
			return this.Substitute(extractSelector, substituteSelector) as TResult;
		}

		// Token: 0x0600C98C RID: 51596 RVA: 0x002B266D File Offset: 0x002B086D
		internal FormulaExpression Substitute(IReadOnlyDictionary<FormulaExpression, FormulaExpression> substitutions)
		{
			return FormulaSubstitutionVisitor.Substitute(this, substitutions);
		}

		// Token: 0x0600C98D RID: 51597 RVA: 0x002B2676 File Offset: 0x002B0876
		internal FormulaExpression Transform(Func<FormulaExpression, FormulaExpression> transform)
		{
			return FormulaTransformVisitor.Transform(this, transform);
		}

		// Token: 0x0600C98E RID: 51598 RVA: 0x002B267F File Offset: 0x002B087F
		internal FormulaExpression Transform(Func<FormulaExpression, FormulaTransformNodeInfo, FormulaExpression> transform)
		{
			return FormulaTransformVisitor.Transform(this, transform);
		}

		// Token: 0x0600C98F RID: 51599 RVA: 0x002B2688 File Offset: 0x002B0888
		internal TResult Transform<TResult>(Func<FormulaExpression, FormulaExpression> transform) where TResult : FormulaExpression
		{
			return this.Transform(transform) as TResult;
		}

		// Token: 0x0600C990 RID: 51600 RVA: 0x002B269B File Offset: 0x002B089B
		internal TResult Transform<TResult>(Func<FormulaExpression, FormulaTransformNodeInfo, FormulaExpression> transform) where TResult : FormulaExpression
		{
			return this.Transform(transform) as TResult;
		}

		// Token: 0x04004F22 RID: 20258
		private string _codeString;

		// Token: 0x04004F23 RID: 20259
		private IReadOnlyList<FormulaExpressionDetail> _nodeDetails;

		// Token: 0x04004F24 RID: 20260
		private IReadOnlyList<FormulaExpression> _nodes;
	}
}
