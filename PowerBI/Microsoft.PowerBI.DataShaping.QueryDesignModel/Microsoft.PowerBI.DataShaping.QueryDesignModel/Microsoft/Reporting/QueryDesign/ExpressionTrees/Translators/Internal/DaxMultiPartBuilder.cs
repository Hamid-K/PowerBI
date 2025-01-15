using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Translators.Internal
{
	// Token: 0x0200013C RID: 316
	internal sealed class DaxMultiPartBuilder
	{
		// Token: 0x06001153 RID: 4435 RVA: 0x00030646 File Offset: 0x0002E846
		internal DaxMultiPartBuilder()
		{
			this._builder = new StringBuilder();
			this._currentLineNumber = new int?(1);
			this._currentState = DaxMultiPartBuilder.BuilderState.Start;
		}

		// Token: 0x06001154 RID: 4436 RVA: 0x0003066C File Offset: 0x0002E86C
		public string ToCommandText(out IReadOnlyList<QueryItemSourceLocation> querySourceMap)
		{
			querySourceMap = this._querySourceMap;
			return this._builder.ToString();
		}

		// Token: 0x06001155 RID: 4437 RVA: 0x00030681 File Offset: 0x0002E881
		public void Define(bool onSeparateLine)
		{
			this.ValidateAndMoveToState(DaxMultiPartBuilder.BuilderState.DefineKeyword);
			this._builder.Append("DEFINE");
			if (onSeparateLine)
			{
				this.NewLine();
				this._indentDefines = true;
				return;
			}
			this._builder.Append(" ");
		}

		// Token: 0x06001156 RID: 4438 RVA: 0x000306BD File Offset: 0x0002E8BD
		public void AddDefinition(DaxExpression daxExpr, QueryItemSourceLocation sourceLocation)
		{
			this.ValidateAndMoveToState(DaxMultiPartBuilder.BuilderState.Definitions);
			this.Append(daxExpr, sourceLocation, this._indentDefines);
		}

		// Token: 0x06001157 RID: 4439 RVA: 0x000306D4 File Offset: 0x0002E8D4
		public void AddStatement(DaxExpression daxExpr)
		{
			this.ValidateAndMoveToState(DaxMultiPartBuilder.BuilderState.Statements);
			this.Append(daxExpr, null, false);
		}

		// Token: 0x06001158 RID: 4440 RVA: 0x000306E8 File Offset: 0x0002E8E8
		private void Append(DaxExpression daxExpr, QueryItemSourceLocation sourceLocation, bool indent)
		{
			if (this._needsSeparator)
			{
				this.NewLine();
				this.NewLine();
			}
			string text = daxExpr.Text;
			if (indent)
			{
				this._builder.Append('\t');
				text = DaxFormat.IncreaseIndent(text);
			}
			this._builder.Append(text);
			this._needsSeparator = true;
			if (sourceLocation == null)
			{
				this._currentLineNumber = null;
				return;
			}
			if (this._currentLineNumber != null)
			{
				Util.AddToLazyList<QueryItemSourceLocation>(ref this._querySourceMap, sourceLocation);
				int num = this._currentLineNumber.Value - 1;
				sourceLocation.AddLineNumberOffset(num);
				this._currentLineNumber = new int?(sourceLocation.WrapperLineEnd - 1);
			}
		}

		// Token: 0x06001159 RID: 4441 RVA: 0x00030790 File Offset: 0x0002E990
		private void NewLine()
		{
			this._builder.Append(DaxFormat.NewLine);
			if (this._currentLineNumber != null)
			{
				this._currentLineNumber++;
			}
		}

		// Token: 0x0600115A RID: 4442 RVA: 0x000307EC File Offset: 0x0002E9EC
		private void ValidateAndMoveToState(DaxMultiPartBuilder.BuilderState targetState)
		{
			if (!DaxMultiPartBuilder.StateTransitions.IsAllowed(this._currentState, targetState))
			{
				throw new InvalidOperationException(StringUtil.FormatInvariant("Cannot move from state {0} to state {1}", new object[] { this._currentState, targetState }));
			}
			this._currentState = targetState;
		}

		// Token: 0x04000AC9 RID: 2761
		private static readonly DaxMultiPartBuilder.BuilderStateTransitions StateTransitions = new DaxMultiPartBuilder.BuilderStateTransitions(new global::System.ValueTuple<DaxMultiPartBuilder.BuilderState, DaxMultiPartBuilder.BuilderState>[]
		{
			new global::System.ValueTuple<DaxMultiPartBuilder.BuilderState, DaxMultiPartBuilder.BuilderState>(DaxMultiPartBuilder.BuilderState.Start, DaxMultiPartBuilder.BuilderState.DefineKeyword),
			new global::System.ValueTuple<DaxMultiPartBuilder.BuilderState, DaxMultiPartBuilder.BuilderState>(DaxMultiPartBuilder.BuilderState.Start, DaxMultiPartBuilder.BuilderState.Definitions),
			new global::System.ValueTuple<DaxMultiPartBuilder.BuilderState, DaxMultiPartBuilder.BuilderState>(DaxMultiPartBuilder.BuilderState.Start, DaxMultiPartBuilder.BuilderState.Statements),
			new global::System.ValueTuple<DaxMultiPartBuilder.BuilderState, DaxMultiPartBuilder.BuilderState>(DaxMultiPartBuilder.BuilderState.DefineKeyword, DaxMultiPartBuilder.BuilderState.Definitions),
			new global::System.ValueTuple<DaxMultiPartBuilder.BuilderState, DaxMultiPartBuilder.BuilderState>(DaxMultiPartBuilder.BuilderState.Definitions, DaxMultiPartBuilder.BuilderState.Definitions),
			new global::System.ValueTuple<DaxMultiPartBuilder.BuilderState, DaxMultiPartBuilder.BuilderState>(DaxMultiPartBuilder.BuilderState.Definitions, DaxMultiPartBuilder.BuilderState.Statements),
			new global::System.ValueTuple<DaxMultiPartBuilder.BuilderState, DaxMultiPartBuilder.BuilderState>(DaxMultiPartBuilder.BuilderState.Statements, DaxMultiPartBuilder.BuilderState.Statements)
		});

		// Token: 0x04000ACA RID: 2762
		private StringBuilder _builder;

		// Token: 0x04000ACB RID: 2763
		private int? _currentLineNumber;

		// Token: 0x04000ACC RID: 2764
		private DaxMultiPartBuilder.BuilderState _currentState;

		// Token: 0x04000ACD RID: 2765
		private bool _indentDefines;

		// Token: 0x04000ACE RID: 2766
		private bool _needsSeparator;

		// Token: 0x04000ACF RID: 2767
		private List<QueryItemSourceLocation> _querySourceMap;

		// Token: 0x02000386 RID: 902
		private sealed class BuilderStateTransitions
		{
			// Token: 0x06001FAA RID: 8106 RVA: 0x00057238 File Offset: 0x00055438
			internal BuilderStateTransitions([global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "From", "To" })] global::System.ValueTuple<DaxMultiPartBuilder.BuilderState, DaxMultiPartBuilder.BuilderState>[] allowedTransitions)
			{
				this._transitions = DaxMultiPartBuilder.BuilderStateTransitions.CreateEmptyStateMap();
				for (int i = 0; i < allowedTransitions.Length; i++)
				{
					this.Allow(allowedTransitions[i].Item1, allowedTransitions[i].Item2);
				}
			}

			// Token: 0x06001FAB RID: 8107 RVA: 0x00057284 File Offset: 0x00055484
			private static bool[][] CreateEmptyStateMap()
			{
				int length = Enum.GetValues(typeof(DaxMultiPartBuilder.BuilderState)).Length;
				bool[][] array = new bool[length][];
				for (int i = 0; i < length; i++)
				{
					array[i] = new bool[length];
				}
				return array;
			}

			// Token: 0x06001FAC RID: 8108 RVA: 0x000572C3 File Offset: 0x000554C3
			private unsafe void Allow(DaxMultiPartBuilder.BuilderState from, DaxMultiPartBuilder.BuilderState to)
			{
				*this.GetTransition(from, to) = true;
			}

			// Token: 0x06001FAD RID: 8109 RVA: 0x000572CF File Offset: 0x000554CF
			internal unsafe bool IsAllowed(DaxMultiPartBuilder.BuilderState from, DaxMultiPartBuilder.BuilderState to)
			{
				return *this.GetTransition(from, to);
			}

			// Token: 0x06001FAE RID: 8110 RVA: 0x000572DA File Offset: 0x000554DA
			internal ref bool GetTransition(DaxMultiPartBuilder.BuilderState from, DaxMultiPartBuilder.BuilderState to)
			{
				return ref this._transitions[(int)from][(int)to];
			}

			// Token: 0x040012C6 RID: 4806
			private readonly bool[][] _transitions;
		}

		// Token: 0x02000387 RID: 903
		private enum BuilderState
		{
			// Token: 0x040012C8 RID: 4808
			Start,
			// Token: 0x040012C9 RID: 4809
			DefineKeyword,
			// Token: 0x040012CA RID: 4810
			Definitions,
			// Token: 0x040012CB RID: 4811
			Statements
		}
	}
}
