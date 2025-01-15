using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000112 RID: 274
	public abstract class ScopedReadOnlyAstVisitor<TBinding> : ReadOnlyAstVisitor
	{
		// Token: 0x06000474 RID: 1140 RVA: 0x00005F4F File Offset: 0x0000414F
		protected ScopedReadOnlyAstVisitor<TBinding>.EnvironmentScope EnterScope()
		{
			return new ScopedReadOnlyAstVisitor<TBinding>.EnvironmentScope(this.environment);
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x00005F5C File Offset: 0x0000415C
		protected bool TryGetValue(Identifier identifier, bool inclusive, out TBinding value)
		{
			return this.environment.TryGetValue(identifier, inclusive, out value);
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x00005F6C File Offset: 0x0000416C
		protected bool TryGetValueAtCurrentDepth(Identifier identifier, bool inclusive, out TBinding value)
		{
			return this.environment.TryGetValueAtCurrentDepth(identifier, inclusive, out value);
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x00005F7C File Offset: 0x0000417C
		protected void EnterScope(TryCatchExceptionCase tryCatchCase, TBinding binding)
		{
			this.environment.EnterScope();
			this.environment.Add(tryCatchCase.Variable, binding);
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x00005F9C File Offset: 0x0000419C
		protected void EnterScope(IFunctionExpression function, IList<TBinding> bindings)
		{
			IList<IParameter> parameters = function.FunctionType.Parameters;
			this.environment.EnterScope();
			for (int i = 0; i < parameters.Count; i++)
			{
				this.environment.Add(parameters[i].Identifier, bindings[i]);
			}
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x00005FF0 File Offset: 0x000041F0
		protected void EnterScope(IList<VariableInitializer> members, IList<TBinding> bindings)
		{
			this.environment.EnterScope();
			for (int i = 0; i < members.Count; i++)
			{
				this.environment.Add(members[i].Name, bindings[i]);
			}
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x0000603C File Offset: 0x0000423C
		protected void EnterScope(IList<ISectionMember> members, IList<TBinding> bindings)
		{
			this.environment.EnterScope();
			for (int i = 0; i < members.Count; i++)
			{
				this.environment.Add(members[i].Name, bindings[i]);
			}
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x00006083 File Offset: 0x00004283
		protected void ExitScope(TryCatchExceptionCase tryCatchCase)
		{
			this.environment.Remove(tryCatchCase.Variable);
			this.environment.ExitScope();
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x000060A4 File Offset: 0x000042A4
		protected void ExitScope(IFunctionExpression function)
		{
			IList<IParameter> parameters = function.FunctionType.Parameters;
			for (int i = 0; i < parameters.Count; i++)
			{
				this.environment.Remove(parameters[i].Identifier);
			}
			this.environment.ExitScope();
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x000060F0 File Offset: 0x000042F0
		protected void ExitScope(IList<VariableInitializer> members)
		{
			for (int i = 0; i < members.Count; i++)
			{
				this.environment.Remove(members[i].Name);
			}
			this.environment.ExitScope();
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x00006134 File Offset: 0x00004334
		protected void ExitScope(IList<ISectionMember> members)
		{
			for (int i = 0; i < members.Count; i++)
			{
				this.environment.Remove(members[i].Name);
			}
			this.environment.ExitScope();
		}

		// Token: 0x0600047F RID: 1151
		protected abstract IList<TBinding> CreateBindings(IDeclarator declarator);

		// Token: 0x06000480 RID: 1152 RVA: 0x00006174 File Offset: 0x00004374
		protected override void VisitFunction(IFunctionExpression function)
		{
			this.VisitFunction(function, this.CreateBindings(function));
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x00006184 File Offset: 0x00004384
		protected void VisitFunction(IFunctionExpression function, IList<TBinding> bindings)
		{
			this.VisitSignature(function.FunctionType);
			this.EnterScope(function, bindings);
			this.VisitExpression(function.Expression);
			this.ExitScope(function);
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x000061AD File Offset: 0x000043AD
		protected sealed override void VisitInitializer(VariableInitializer member)
		{
			this.environment.EnterExclusion(member.Name);
			this.VisitExpression(member.Value);
			this.environment.ExitExclusion(member.Name);
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x000061E0 File Offset: 0x000043E0
		protected override void VisitLet(ILetExpression let)
		{
			this.VisitLet(let, this.CreateBindings(let));
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x000061F0 File Offset: 0x000043F0
		protected void VisitLet(ILetExpression let, IList<TBinding> bindings)
		{
			this.EnterScope(let.Variables, bindings);
			base.VisitListElements(let.Variables);
			this.VisitExpression(let.Expression);
			this.ExitScope(let.Variables);
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x00006224 File Offset: 0x00004424
		protected override void VisitRecord(IRecordExpression record)
		{
			this.VisitRecord(record, default(TBinding), this.CreateBindings(record));
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x00006248 File Offset: 0x00004448
		protected void VisitRecord(IRecordExpression record, TBinding binding, IList<TBinding> bindings)
		{
			this.EnterScope(record.Members, bindings);
			if (record.Identifier != null)
			{
				this.environment.Add(record.Identifier, binding);
			}
			base.VisitListElements(record.Members);
			if (record.Identifier != null)
			{
				this.environment.Remove(record.Identifier);
			}
			this.ExitScope(record.Members);
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x000062B9 File Offset: 0x000044B9
		protected override void VisitTryCatchExceptionCase(TryCatchExceptionCase tryCatchExceptionCase)
		{
			this.VisitTryCatchExceptionCase(tryCatchExceptionCase, this.CreateBindings(tryCatchExceptionCase)[0]);
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x000062D4 File Offset: 0x000044D4
		protected void VisitTryCatchExceptionCase(TryCatchExceptionCase tryCatchExceptionCase, TBinding value)
		{
			this.EnterScope(tryCatchExceptionCase, value);
			this.VisitExpression(tryCatchExceptionCase.Expression);
			this.ExitScope(tryCatchExceptionCase);
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x000062F2 File Offset: 0x000044F2
		protected override void VisitModule(ISection module)
		{
			this.VisitModule(module, this.CreateBindings(module));
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x00006302 File Offset: 0x00004502
		protected void VisitModule(ISection module, IList<TBinding> bindings)
		{
			this.EnterScope(module.Members, bindings);
			base.VisitListElements(module.Members);
			this.ExitScope(module.Members);
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x00006329 File Offset: 0x00004529
		protected sealed override void VisitModuleMember(ISectionMember member)
		{
			this.environment.EnterExclusion(member.Name);
			this.VisitExpression(member.Value);
			this.environment.ExitExclusion(member.Name);
		}

		// Token: 0x0400029E RID: 670
		private readonly ScopedReadOnlyAstVisitor<TBinding>.Environment environment = new ScopedReadOnlyAstVisitor<TBinding>.Environment();

		// Token: 0x02000113 RID: 275
		protected struct EnvironmentScope : IDisposable
		{
			// Token: 0x0600048D RID: 1165 RVA: 0x0000636C File Offset: 0x0000456C
			internal EnvironmentScope(ScopedReadOnlyAstVisitor<TBinding>.Environment environment)
			{
				this.environment = environment;
				this.identifiers = new List<Identifier>(4);
				this.environment.EnterScope();
			}

			// Token: 0x0600048E RID: 1166 RVA: 0x0000638C File Offset: 0x0000458C
			public void Add(Identifier identifier, TBinding value)
			{
				this.environment.Add(identifier, value);
				this.identifiers.Add(identifier);
			}

			// Token: 0x0600048F RID: 1167 RVA: 0x000063A8 File Offset: 0x000045A8
			void IDisposable.Dispose()
			{
				for (int i = this.identifiers.Count - 1; i >= 0; i--)
				{
					this.environment.Remove(this.identifiers[i]);
				}
				this.environment.ExitScope();
			}

			// Token: 0x0400029F RID: 671
			private readonly ScopedReadOnlyAstVisitor<TBinding>.Environment environment;

			// Token: 0x040002A0 RID: 672
			private readonly List<Identifier> identifiers;
		}

		// Token: 0x02000114 RID: 276
		internal sealed class Environment
		{
			// Token: 0x06000490 RID: 1168 RVA: 0x000063EF File Offset: 0x000045EF
			public Environment()
			{
				this.dictionary = new Dictionary<Identifier, ScopedReadOnlyAstVisitor<TBinding>.Environment.Entry>();
			}

			// Token: 0x06000491 RID: 1169 RVA: 0x00006402 File Offset: 0x00004602
			public void EnterScope()
			{
				this.depth++;
			}

			// Token: 0x06000492 RID: 1170 RVA: 0x00006412 File Offset: 0x00004612
			public void ExitScope()
			{
				this.depth--;
				if (this.depth == 0)
				{
					this.dictionary.Clear();
				}
			}

			// Token: 0x06000493 RID: 1171 RVA: 0x00006438 File Offset: 0x00004638
			public void Add(Identifier identifier, TBinding value)
			{
				ScopedReadOnlyAstVisitor<TBinding>.Environment.Entry entry;
				if (this.dictionary.TryGetValue(identifier, out entry) && entry.Depth == this.depth)
				{
					ScopedReadOnlyAstVisitor<TBinding>.Environment.Entry entry2 = entry;
					while (entry2.Exclusion)
					{
						entry2 = entry2.Previous;
					}
					value = entry2.Value;
				}
				this.dictionary[identifier] = ScopedReadOnlyAstVisitor<TBinding>.Environment.Entry.New(value, this.depth, entry);
			}

			// Token: 0x06000494 RID: 1172 RVA: 0x00006498 File Offset: 0x00004698
			public void Remove(Identifier identifier)
			{
				ScopedReadOnlyAstVisitor<TBinding>.Environment.Entry previous = this.dictionary[identifier].Previous;
				if (previous != null)
				{
					this.dictionary[identifier] = previous;
					return;
				}
				this.dictionary.Remove(identifier);
			}

			// Token: 0x06000495 RID: 1173 RVA: 0x000064D8 File Offset: 0x000046D8
			public TBinding GetValue(Identifier identifier, bool inclusive)
			{
				TBinding tbinding;
				if (!this.TryGetValue(identifier, inclusive, out tbinding))
				{
					throw new InvalidOperationException("Identifier not found: " + identifier);
				}
				return tbinding;
			}

			// Token: 0x06000496 RID: 1174 RVA: 0x00006508 File Offset: 0x00004708
			public void EnterExclusion(Identifier identifier)
			{
				ScopedReadOnlyAstVisitor<TBinding>.Environment.Entry entry;
				this.dictionary.TryGetValue(identifier, out entry);
				this.dictionary[identifier] = ScopedReadOnlyAstVisitor<TBinding>.Environment.Entry.NewExclusion(this.depth, entry);
			}

			// Token: 0x06000497 RID: 1175 RVA: 0x0000653C File Offset: 0x0000473C
			public void ExitExclusion(Identifier identifier)
			{
				ScopedReadOnlyAstVisitor<TBinding>.Environment.Entry previous = this.dictionary[identifier].Previous;
				if (previous != null)
				{
					this.dictionary[identifier] = previous;
					return;
				}
				this.dictionary.Remove(identifier);
			}

			// Token: 0x06000498 RID: 1176 RVA: 0x0000657C File Offset: 0x0000477C
			public bool TryGetValue(Identifier identifier, bool inclusive, out TBinding value)
			{
				ScopedReadOnlyAstVisitor<TBinding>.Environment.Entry entry;
				if (this.TryGetEntry(identifier, inclusive, out entry))
				{
					value = entry.Value;
					return true;
				}
				value = default(TBinding);
				return false;
			}

			// Token: 0x06000499 RID: 1177 RVA: 0x000065AC File Offset: 0x000047AC
			public bool TryGetValueAtCurrentDepth(Identifier identifier, bool inclusive, out TBinding value)
			{
				ScopedReadOnlyAstVisitor<TBinding>.Environment.Entry entry;
				if (this.TryGetEntry(identifier, inclusive, out entry) && entry.Depth == this.depth)
				{
					value = entry.Value;
					return true;
				}
				value = default(TBinding);
				return false;
			}

			// Token: 0x0600049A RID: 1178 RVA: 0x000065E9 File Offset: 0x000047E9
			private bool TryGetEntry(Identifier identifier, bool inclusive, out ScopedReadOnlyAstVisitor<TBinding>.Environment.Entry entry)
			{
				if (this.dictionary.TryGetValue(identifier, out entry))
				{
					while (entry != null)
					{
						if (!entry.Exclusion)
						{
							return true;
						}
						if (!inclusive)
						{
							entry = entry.Previous;
						}
						entry = entry.Previous;
					}
				}
				entry = null;
				return false;
			}

			// Token: 0x040002A1 RID: 673
			private int depth;

			// Token: 0x040002A2 RID: 674
			private Dictionary<Identifier, ScopedReadOnlyAstVisitor<TBinding>.Environment.Entry> dictionary;

			// Token: 0x02000115 RID: 277
			private class Entry
			{
				// Token: 0x0600049B RID: 1179 RVA: 0x00006626 File Offset: 0x00004826
				public static ScopedReadOnlyAstVisitor<TBinding>.Environment.Entry New(TBinding value, int depth, ScopedReadOnlyAstVisitor<TBinding>.Environment.Entry previous)
				{
					return new ScopedReadOnlyAstVisitor<TBinding>.Environment.Entry(value, depth, false, previous);
				}

				// Token: 0x0600049C RID: 1180 RVA: 0x00006634 File Offset: 0x00004834
				public static ScopedReadOnlyAstVisitor<TBinding>.Environment.Entry NewExclusion(int depth, ScopedReadOnlyAstVisitor<TBinding>.Environment.Entry previous)
				{
					return new ScopedReadOnlyAstVisitor<TBinding>.Environment.Entry(default(TBinding), depth, true, previous);
				}

				// Token: 0x0600049D RID: 1181 RVA: 0x00006652 File Offset: 0x00004852
				private Entry(TBinding value, int depth, bool exclusion, ScopedReadOnlyAstVisitor<TBinding>.Environment.Entry previous)
				{
					this.value = value;
					this.depthAndExclusion = (uint)(depth & -134217729);
					if (exclusion)
					{
						this.depthAndExclusion |= 134217728U;
					}
					this.previous = previous;
				}

				// Token: 0x170001B1 RID: 433
				// (get) Token: 0x0600049E RID: 1182 RVA: 0x0000668B File Offset: 0x0000488B
				public TBinding Value
				{
					get
					{
						return this.value;
					}
				}

				// Token: 0x170001B2 RID: 434
				// (get) Token: 0x0600049F RID: 1183 RVA: 0x00006693 File Offset: 0x00004893
				public int Depth
				{
					get
					{
						return (int)(this.depthAndExclusion & 4160749567U);
					}
				}

				// Token: 0x170001B3 RID: 435
				// (get) Token: 0x060004A0 RID: 1184 RVA: 0x000066A1 File Offset: 0x000048A1
				public bool Exclusion
				{
					get
					{
						return (this.depthAndExclusion & 134217728U) > 0U;
					}
				}

				// Token: 0x170001B4 RID: 436
				// (get) Token: 0x060004A1 RID: 1185 RVA: 0x000066B2 File Offset: 0x000048B2
				public ScopedReadOnlyAstVisitor<TBinding>.Environment.Entry Previous
				{
					get
					{
						return this.previous;
					}
				}

				// Token: 0x040002A3 RID: 675
				private const uint exclusionMask = 134217728U;

				// Token: 0x040002A4 RID: 676
				private TBinding value;

				// Token: 0x040002A5 RID: 677
				private uint depthAndExclusion;

				// Token: 0x040002A6 RID: 678
				private ScopedReadOnlyAstVisitor<TBinding>.Environment.Entry previous;
			}
		}
	}
}
