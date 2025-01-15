using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes
{
	// Token: 0x0200107B RID: 4219
	public struct resultFields : IProgramNodeBuilder, IEquatable<resultFields>
	{
		// Token: 0x17001664 RID: 5732
		// (get) Token: 0x06007E73 RID: 32371 RVA: 0x001A9C06 File Offset: 0x001A7E06
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007E74 RID: 32372 RVA: 0x001A9C0E File Offset: 0x001A7E0E
		private resultFields(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007E75 RID: 32373 RVA: 0x001A9C17 File Offset: 0x001A7E17
		public static resultFields CreateUnsafe(ProgramNode node)
		{
			return new resultFields(node);
		}

		// Token: 0x06007E76 RID: 32374 RVA: 0x001A9C20 File Offset: 0x001A7E20
		public static resultFields? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.resultFields)
			{
				return null;
			}
			return new resultFields?(resultFields.CreateUnsafe(node));
		}

		// Token: 0x06007E77 RID: 32375 RVA: 0x001A9C5A File Offset: 0x001A7E5A
		public static resultFields CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new resultFields(new Hole(g.Symbol.resultFields, holeId));
		}

		// Token: 0x06007E78 RID: 32376 RVA: 0x001A9C72 File Offset: 0x001A7E72
		public bool Is_resultFields_singletonField(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.resultFields_singletonField;
		}

		// Token: 0x06007E79 RID: 32377 RVA: 0x001A9C8C File Offset: 0x001A7E8C
		public bool Is_resultFields_singletonField(GrammarBuilders g, out resultFields_singletonField value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.resultFields_singletonField)
			{
				value = resultFields_singletonField.CreateUnsafe(this.Node);
				return true;
			}
			value = default(resultFields_singletonField);
			return false;
		}

		// Token: 0x06007E7A RID: 32378 RVA: 0x001A9CC4 File Offset: 0x001A7EC4
		public resultFields_singletonField? As_resultFields_singletonField(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.resultFields_singletonField)
			{
				return null;
			}
			return new resultFields_singletonField?(resultFields_singletonField.CreateUnsafe(this.Node));
		}

		// Token: 0x06007E7B RID: 32379 RVA: 0x001A9D04 File Offset: 0x001A7F04
		public resultFields_singletonField Cast_resultFields_singletonField(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.resultFields_singletonField)
			{
				return resultFields_singletonField.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_resultFields_singletonField is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06007E7C RID: 32380 RVA: 0x001A9D59 File Offset: 0x001A7F59
		public bool Is_AppendField(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.AppendField;
		}

		// Token: 0x06007E7D RID: 32381 RVA: 0x001A9D73 File Offset: 0x001A7F73
		public bool Is_AppendField(GrammarBuilders g, out AppendField value)
		{
			if (this.Node.GrammarRule == g.Rule.AppendField)
			{
				value = AppendField.CreateUnsafe(this.Node);
				return true;
			}
			value = default(AppendField);
			return false;
		}

		// Token: 0x06007E7E RID: 32382 RVA: 0x001A9DA8 File Offset: 0x001A7FA8
		public AppendField? As_AppendField(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.AppendField)
			{
				return null;
			}
			return new AppendField?(AppendField.CreateUnsafe(this.Node));
		}

		// Token: 0x06007E7F RID: 32383 RVA: 0x001A9DE8 File Offset: 0x001A7FE8
		public AppendField Cast_AppendField(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.AppendField)
			{
				return AppendField.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_AppendField is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06007E80 RID: 32384 RVA: 0x001A9E40 File Offset: 0x001A8040
		public T Switch<T>(GrammarBuilders g, Func<resultFields_singletonField, T> func0, Func<AppendField, T> func1)
		{
			resultFields_singletonField resultFields_singletonField;
			if (this.Is_resultFields_singletonField(g, out resultFields_singletonField))
			{
				return func0(resultFields_singletonField);
			}
			AppendField appendField;
			if (this.Is_AppendField(g, out appendField))
			{
				return func1(appendField);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol resultFields");
		}

		// Token: 0x06007E81 RID: 32385 RVA: 0x001A9E98 File Offset: 0x001A8098
		public void Switch(GrammarBuilders g, Action<resultFields_singletonField> func0, Action<AppendField> func1)
		{
			resultFields_singletonField resultFields_singletonField;
			if (this.Is_resultFields_singletonField(g, out resultFields_singletonField))
			{
				func0(resultFields_singletonField);
				return;
			}
			AppendField appendField;
			if (this.Is_AppendField(g, out appendField))
			{
				func1(appendField);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol resultFields");
		}

		// Token: 0x06007E82 RID: 32386 RVA: 0x001A9EEF File Offset: 0x001A80EF
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007E83 RID: 32387 RVA: 0x001A9F04 File Offset: 0x001A8104
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007E84 RID: 32388 RVA: 0x001A9F2E File Offset: 0x001A812E
		public bool Equals(resultFields other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003394 RID: 13204
		private ProgramNode _node;
	}
}
