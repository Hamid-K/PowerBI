using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Diagnostics;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Rules
{
	// Token: 0x0200039D RID: 925
	[DebuggerDisplay("{Display}")]
	[DataContract]
	public abstract class OperatorRule : NonterminalRule
	{
		// Token: 0x060014D8 RID: 5336 RVA: 0x0003CF73 File Offset: 0x0003B173
		public OperatorRule(string id, Symbol head, params Symbol[] body)
			: this(id, head, body.AsEnumerable<Symbol>())
		{
		}

		// Token: 0x060014D9 RID: 5337 RVA: 0x0003CF83 File Offset: 0x0003B183
		public OperatorRule(string id, Symbol head, IEnumerable<Symbol> body)
			: base(head, body)
		{
			base.Id = id;
			this.Name = id;
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x060014DA RID: 5338 RVA: 0x0003CF9B File Offset: 0x0003B19B
		// (set) Token: 0x060014DB RID: 5339 RVA: 0x0003CFA3 File Offset: 0x0003B1A3
		[DataMember]
		public string Name { get; private set; }

		// Token: 0x060014DC RID: 5340 RVA: 0x0003CFAC File Offset: 0x0003B1AC
		internal override State ValidStateFromArgumentInvocations(params Record<State, object>[] argumentInvocations)
		{
			return argumentInvocations.Select((Record<State, object> t) => t.Item1).Distinct<State>().SingleOrDefault<State>();
		}

		// Token: 0x060014DD RID: 5341 RVA: 0x0003CFE0 File Offset: 0x0003B1E0
		internal override Dictionary<object, ProgramSet> Cluster(JoinProgramSet space, State inputState)
		{
			int count = base.Body.Count;
			Dictionary<object, ProgramSet>[] parameterIndexes = new Dictionary<object, ProgramSet>[count];
			ProgramNode[] array = new ProgramNode[count];
			for (int i = 0; i < count; i++)
			{
				int iClosure = i;
				array[i] = new Hole(base.Body[i], null);
				array[i].OnEvaluationFinished += delegate(object _, EvaluationFinishedEventArgs args)
				{
					parameterIndexes[iClosure] = space.ParameterSpaces[iClosure].ClusterOnInput(args.Input);
				};
			}
			base.BuildASTNode(array).Invoke(inputState);
			MultiValueDictionary<object, ProgramSet> multiValueDictionary = new MultiValueDictionary<object, ProgramSet>(ValueEquality.Comparer);
			foreach (IEnumerable<KeyValuePair<object, ProgramSet>> enumerable in parameterIndexes.CartesianProduct<KeyValuePair<object, ProgramSet>>())
			{
				KeyValuePair<object, ProgramSet>[] array2 = (enumerable as KeyValuePair<object, ProgramSet>[]) ?? enumerable.ToArray<KeyValuePair<object, ProgramSet>>();
				ProgramNode[] array3 = new ProgramNode[count];
				for (int j = 0; j < count; j++)
				{
					array3[j] = new LiteralNode(base.Body[j], array2[j].Key);
				}
				object obj = base.BuildASTNode(array3).Invoke(inputState);
				ProgramSet[] array4 = array2.Select((KeyValuePair<object, ProgramSet> kvp) => kvp.Value).ToArray<ProgramSet>();
				multiValueDictionary.Add(obj.NullToBottom(), new JoinProgramSet(this, array4));
			}
			return multiValueDictionary.ToDictionary((KeyValuePair<object, IReadOnlyCollection<ProgramSet>> kvp) => kvp.Key, (KeyValuePair<object, IReadOnlyCollection<ProgramSet>> kvp) => kvp.Value.NormalizedUnion(), ValueEquality.Comparer);
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x060014DE RID: 5342 RVA: 0x0003D1C0 File Offset: 0x0003B3C0
		internal OperatorRule.OperatorSemantics Semantics
		{
			get
			{
				MethodReference<OperatorRule.OperatorSemantics> semantics = this._semantics;
				if (((semantics != null) ? semantics.Invoke : null) == null)
				{
					this._semantics = this.InitializeSemantics();
				}
				return this._semantics.Invoke;
			}
		}

		// Token: 0x060014DF RID: 5343 RVA: 0x0003D1ED File Offset: 0x0003B3ED
		internal override void Validate(DiagnosticsContext diagnosticsContext)
		{
			base.Validate(diagnosticsContext);
			if ((diagnosticsContext.ValidationFlags & GrammarValidation.Semantics) != GrammarValidation.None)
			{
				this.ValidateSemantics(diagnosticsContext);
			}
			this._semantics = this.InitializeSemantics();
		}

		// Token: 0x060014E0 RID: 5344
		internal abstract void ValidateSemantics(DiagnosticsContext diagnosticsContext);

		// Token: 0x060014E1 RID: 5345
		protected abstract MethodReference<OperatorRule.OperatorSemantics> InitializeSemantics();

		// Token: 0x060014E2 RID: 5346 RVA: 0x0003D214 File Offset: 0x0003B414
		internal sealed override object Evaluate(object[] args)
		{
			object[] array = args;
			int i = 0;
			while (i < array.Length)
			{
				object obj = array[i];
				if (obj == null || obj is Bottom)
				{
					if (!this.Lazy)
					{
						return null;
					}
					IEnumerable<object> enumerable = args;
					Func<object, object> func;
					if ((func = OperatorRule.<>O.<0>__BottomToNull) == null)
					{
						func = (OperatorRule.<>O.<0>__BottomToNull = new Func<object, object>(ObjectUtils.BottomToNull));
					}
					args = enumerable.Select(func).ToArray<object>();
					break;
				}
				else
				{
					i++;
				}
			}
			return this.Semantics(args);
		}

		// Token: 0x060014E3 RID: 5347 RVA: 0x00002188 File Offset: 0x00000388
		internal override FeatureCalculator BuildDefaultFeatureCalculator(FeatureInfo feature)
		{
			return null;
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x060014E4 RID: 5348 RVA: 0x0003D280 File Offset: 0x0003B480
		[EditorBrowsable(EditorBrowsableState.Never)]
		internal string Display
		{
			get
			{
				return FormattableString.Invariant(FormattableStringFactory.Create("{0} := {1}({2})", new object[]
				{
					base.Head,
					base.Id,
					string.Join<Symbol>(", ", base.Body)
				}));
			}
		}

		// Token: 0x060014E5 RID: 5349 RVA: 0x0003D2BC File Offset: 0x0003B4BC
		public override string ToString()
		{
			return base.Id;
		}

		// Token: 0x060014E6 RID: 5350 RVA: 0x0003D2C4 File Offset: 0x0003B4C4
		internal override CodeBuilder FormatAST(IEnumerable<CodeBuilder> parameters, ASTSerializationSettings settings)
		{
			CodeBuilder codeBuilder = CodeBuilder.Create(base.Id);
			codeBuilder.Append("(");
			using (codeBuilder.NewScope(null, settings.IndentIncrement))
			{
				bool flag = true;
				foreach (CodeBuilder codeBuilder2 in parameters)
				{
					if (flag)
					{
						flag = false;
					}
					else
					{
						codeBuilder.Append(", ");
					}
					if (settings.HasIndent)
					{
						codeBuilder.AppendLine();
					}
					codeBuilder.Append(codeBuilder2);
				}
			}
			codeBuilder.Append(")");
			return codeBuilder;
		}

		// Token: 0x04000A4A RID: 2634
		[DataMember]
		protected bool Lazy;

		// Token: 0x04000A4B RID: 2635
		[DataMember]
		private MethodReference<OperatorRule.OperatorSemantics> _semantics;

		// Token: 0x0200039E RID: 926
		// (Invoke) Token: 0x060014E8 RID: 5352
		public delegate object OperatorSemantics(params object[] args);

		// Token: 0x0200039F RID: 927
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04000A4C RID: 2636
			public static Func<object, object> <0>__BottomToNull;
		}
	}
}
