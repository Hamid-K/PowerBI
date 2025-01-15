using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x02001599 RID: 5529
	public struct result : IProgramNodeBuilder, IEquatable<result>
	{
		// Token: 0x17001FBF RID: 8127
		// (get) Token: 0x0600B538 RID: 46392 RVA: 0x00275F0A File Offset: 0x0027410A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B539 RID: 46393 RVA: 0x00275F12 File Offset: 0x00274112
		private result(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B53A RID: 46394 RVA: 0x00275F1B File Offset: 0x0027411B
		public static result CreateUnsafe(ProgramNode node)
		{
			return new result(node);
		}

		// Token: 0x0600B53B RID: 46395 RVA: 0x00275F24 File Offset: 0x00274124
		public static result? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.result)
			{
				return null;
			}
			return new result?(result.CreateUnsafe(node));
		}

		// Token: 0x0600B53C RID: 46396 RVA: 0x00275F5E File Offset: 0x0027415E
		public static result CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new result(new Hole(g.Symbol.result, holeId));
		}

		// Token: 0x0600B53D RID: 46397 RVA: 0x00275F76 File Offset: 0x00274176
		public bool Is_result_output(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.result_output;
		}

		// Token: 0x0600B53E RID: 46398 RVA: 0x00275F90 File Offset: 0x00274190
		public bool Is_result_output(GrammarBuilders g, out result_output value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.result_output)
			{
				value = result_output.CreateUnsafe(this.Node);
				return true;
			}
			value = default(result_output);
			return false;
		}

		// Token: 0x0600B53F RID: 46399 RVA: 0x00275FC8 File Offset: 0x002741C8
		public result_output? As_result_output(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.result_output)
			{
				return null;
			}
			return new result_output?(result_output.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B540 RID: 46400 RVA: 0x00276008 File Offset: 0x00274208
		public result_output Cast_result_output(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.result_output)
			{
				return result_output.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_result_output is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B541 RID: 46401 RVA: 0x0027605D File Offset: 0x0027425D
		public bool Is_result_inull(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.result_inull;
		}

		// Token: 0x0600B542 RID: 46402 RVA: 0x00276077 File Offset: 0x00274277
		public bool Is_result_inull(GrammarBuilders g, out result_inull value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.result_inull)
			{
				value = result_inull.CreateUnsafe(this.Node);
				return true;
			}
			value = default(result_inull);
			return false;
		}

		// Token: 0x0600B543 RID: 46403 RVA: 0x002760AC File Offset: 0x002742AC
		public result_inull? As_result_inull(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.result_inull)
			{
				return null;
			}
			return new result_inull?(result_inull.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B544 RID: 46404 RVA: 0x002760EC File Offset: 0x002742EC
		public result_inull Cast_result_inull(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.result_inull)
			{
				return result_inull.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_result_inull is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B545 RID: 46405 RVA: 0x00276141 File Offset: 0x00274341
		public bool Is_If(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.If;
		}

		// Token: 0x0600B546 RID: 46406 RVA: 0x0027615B File Offset: 0x0027435B
		public bool Is_If(GrammarBuilders g, out If value)
		{
			if (this.Node.GrammarRule == g.Rule.If)
			{
				value = If.CreateUnsafe(this.Node);
				return true;
			}
			value = default(If);
			return false;
		}

		// Token: 0x0600B547 RID: 46407 RVA: 0x00276190 File Offset: 0x00274390
		public If? As_If(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.If)
			{
				return null;
			}
			return new If?(If.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B548 RID: 46408 RVA: 0x002761D0 File Offset: 0x002743D0
		public If Cast_If(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.If)
			{
				return If.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_If is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B549 RID: 46409 RVA: 0x00276228 File Offset: 0x00274428
		public T Switch<T>(GrammarBuilders g, Func<result_output, T> func0, Func<result_inull, T> func1, Func<If, T> func2)
		{
			result_output result_output;
			if (this.Is_result_output(g, out result_output))
			{
				return func0(result_output);
			}
			result_inull result_inull;
			if (this.Is_result_inull(g, out result_inull))
			{
				return func1(result_inull);
			}
			If @if;
			if (this.Is_If(g, out @if))
			{
				return func2(@if);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol result");
		}

		// Token: 0x0600B54A RID: 46410 RVA: 0x00276294 File Offset: 0x00274494
		public void Switch(GrammarBuilders g, Action<result_output> func0, Action<result_inull> func1, Action<If> func2)
		{
			result_output result_output;
			if (this.Is_result_output(g, out result_output))
			{
				func0(result_output);
				return;
			}
			result_inull result_inull;
			if (this.Is_result_inull(g, out result_inull))
			{
				func1(result_inull);
				return;
			}
			If @if;
			if (this.Is_If(g, out @if))
			{
				func2(@if);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol result");
		}

		// Token: 0x0600B54B RID: 46411 RVA: 0x002762FF File Offset: 0x002744FF
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B54C RID: 46412 RVA: 0x00276314 File Offset: 0x00274514
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B54D RID: 46413 RVA: 0x0027633E File Offset: 0x0027453E
		public bool Equals(result other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004647 RID: 17991
		private ProgramNode _node;
	}
}
