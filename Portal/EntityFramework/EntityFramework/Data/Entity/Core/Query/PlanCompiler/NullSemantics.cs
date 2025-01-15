using System;
using System.Data.Entity.Core.Query.InternalTrees;
using System.Linq;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x02000353 RID: 851
	internal class NullSemantics : BasicOpVisitorOfNode
	{
		// Token: 0x0600293B RID: 10555 RVA: 0x00083DF9 File Offset: 0x00081FF9
		private NullSemantics(Command command)
		{
			this._command = command;
		}

		// Token: 0x0600293C RID: 10556 RVA: 0x00083E18 File Offset: 0x00082018
		public static bool Process(Command command)
		{
			NullSemantics nullSemantics = new NullSemantics(command);
			command.Root = nullSemantics.VisitNode(command.Root);
			return nullSemantics._modified;
		}

		// Token: 0x0600293D RID: 10557 RVA: 0x00083E44 File Offset: 0x00082044
		protected override Node VisitDefault(Node n)
		{
			bool negated = this._negated;
			OpType opType = n.Op.OpType;
			if (opType != OpType.EQ)
			{
				if (opType != OpType.NE)
				{
					switch (opType)
					{
					case OpType.And:
						n = base.VisitDefault(n);
						goto IL_0096;
					case OpType.Or:
						n = this.HandleOr(n);
						goto IL_0096;
					case OpType.Not:
						this._negated = !this._negated;
						n = base.VisitDefault(n);
						goto IL_0096;
					}
					this._negated = false;
					n = base.VisitDefault(n);
				}
				else
				{
					n = this.HandleNE(n);
				}
			}
			else
			{
				this._negated = false;
				n = this.HandleEQ(n, negated);
			}
			IL_0096:
			this._negated = negated;
			return n;
		}

		// Token: 0x0600293E RID: 10558 RVA: 0x00083EF0 File Offset: 0x000820F0
		private Node HandleOr(Node n)
		{
			Node node = ((n.Child0.Op.OpType == OpType.IsNull) ? n.Child0 : null);
			if (node == null || node.Child0.Op.OpType != OpType.VarRef)
			{
				return base.VisitDefault(n);
			}
			Var var = ((VarRefOp)node.Child0.Op).Var;
			bool flag = this._variableNullabilityTable[var];
			this._variableNullabilityTable[var] = false;
			n.Child1 = base.VisitNode(n.Child1);
			this._variableNullabilityTable[var] = flag;
			return n;
		}

		// Token: 0x0600293F RID: 10559 RVA: 0x00083F8C File Offset: 0x0008218C
		private Node HandleEQ(Node n, bool negated)
		{
			this._modified |= n.Child0 != (n.Child0 = base.VisitNode(n.Child0)) || n.Child1 != (n.Child1 = base.VisitNode(n.Child1)) || n != (n = this.ImplementEquality(n, negated));
			return n;
		}

		// Token: 0x06002940 RID: 10560 RVA: 0x00083FF8 File Offset: 0x000821F8
		private Node HandleNE(Node n)
		{
			ComparisonOp comparisonOp = (ComparisonOp)n.Op;
			n = this._command.CreateNode(this._command.CreateConditionalOp(OpType.Not), this._command.CreateNode(this._command.CreateComparisonOp(OpType.EQ, comparisonOp.UseDatabaseNullSemantics), n.Child0, n.Child1));
			this._modified = true;
			return base.VisitDefault(n);
		}

		// Token: 0x06002941 RID: 10561 RVA: 0x00084063 File Offset: 0x00082263
		private bool IsNullableVarRef(Node n)
		{
			return n.Op.OpType == OpType.VarRef && this._variableNullabilityTable[((VarRefOp)n.Op).Var];
		}

		// Token: 0x06002942 RID: 10562 RVA: 0x00084090 File Offset: 0x00082290
		private Node ImplementEquality(Node n, bool negated)
		{
			if (((ComparisonOp)n.Op).UseDatabaseNullSemantics)
			{
				return n;
			}
			Node child = n.Child0;
			Node child2 = n.Child1;
			OpType opType = child.Op.OpType;
			if (opType > OpType.NullSentinel)
			{
				if (opType != OpType.Null)
				{
					OpType opType2 = child2.Op.OpType;
					if (opType2 > OpType.NullSentinel)
					{
						if (opType2 == OpType.Null)
						{
							return this.IsNull(child);
						}
						if (!negated)
						{
							return this.Or(n, this.And(this.IsNull(this.Clone(child)), this.IsNull(this.Clone(child2))));
						}
						return this.And(n, this.NotXor(this.Clone(child), this.Clone(child2)));
					}
					else
					{
						if (!negated || !this.IsNullableVarRef(n))
						{
							return n;
						}
						return this.And(n, this.Not(this.IsNull(this.Clone(child))));
					}
				}
				else
				{
					OpType opType2 = child2.Op.OpType;
					if (opType2 <= OpType.NullSentinel)
					{
						return this.False();
					}
					if (opType2 != OpType.Null)
					{
						return this.IsNull(child2);
					}
					return this.True();
				}
			}
			else
			{
				OpType opType2 = child2.Op.OpType;
				if (opType2 <= OpType.NullSentinel)
				{
					return n;
				}
				if (opType2 == OpType.Null)
				{
					return this.False();
				}
				if (!negated)
				{
					return n;
				}
				return this.And(n, this.Not(this.IsNull(this.Clone(child2))));
			}
		}

		// Token: 0x06002943 RID: 10563 RVA: 0x000841CD File Offset: 0x000823CD
		private Node Clone(Node x)
		{
			return OpCopier.Copy(this._command, x);
		}

		// Token: 0x06002944 RID: 10564 RVA: 0x000841DB File Offset: 0x000823DB
		private Node False()
		{
			return this._command.CreateNode(this._command.CreateFalseOp());
		}

		// Token: 0x06002945 RID: 10565 RVA: 0x000841F3 File Offset: 0x000823F3
		private Node True()
		{
			return this._command.CreateNode(this._command.CreateTrueOp());
		}

		// Token: 0x06002946 RID: 10566 RVA: 0x0008420B File Offset: 0x0008240B
		private Node IsNull(Node x)
		{
			return this._command.CreateNode(this._command.CreateConditionalOp(OpType.IsNull), x);
		}

		// Token: 0x06002947 RID: 10567 RVA: 0x00084226 File Offset: 0x00082426
		private Node Not(Node x)
		{
			return this._command.CreateNode(this._command.CreateConditionalOp(OpType.Not), x);
		}

		// Token: 0x06002948 RID: 10568 RVA: 0x00084241 File Offset: 0x00082441
		private Node And(Node x, Node y)
		{
			return this._command.CreateNode(this._command.CreateConditionalOp(OpType.And), x, y);
		}

		// Token: 0x06002949 RID: 10569 RVA: 0x0008425D File Offset: 0x0008245D
		private Node Or(Node x, Node y)
		{
			return this._command.CreateNode(this._command.CreateConditionalOp(OpType.Or), x, y);
		}

		// Token: 0x0600294A RID: 10570 RVA: 0x00084279 File Offset: 0x00082479
		private Node Boolean(bool value)
		{
			return this._command.CreateNode(this._command.CreateConstantOp(this._command.BooleanType, value));
		}

		// Token: 0x0600294B RID: 10571 RVA: 0x000842A4 File Offset: 0x000824A4
		private Node NotXor(Node x, Node y)
		{
			return this._command.CreateNode(this._command.CreateComparisonOp(OpType.EQ, false), this._command.CreateNode(this._command.CreateCaseOp(this._command.BooleanType), this.IsNull(x), this.Boolean(true), this.Boolean(false)), this._command.CreateNode(this._command.CreateCaseOp(this._command.BooleanType), this.IsNull(y), this.Boolean(true), this.Boolean(false)));
		}

		// Token: 0x04000E38 RID: 3640
		private Command _command;

		// Token: 0x04000E39 RID: 3641
		private bool _modified;

		// Token: 0x04000E3A RID: 3642
		private bool _negated;

		// Token: 0x04000E3B RID: 3643
		private NullSemantics.VariableNullabilityTable _variableNullabilityTable = new NullSemantics.VariableNullabilityTable(32);

		// Token: 0x020009F0 RID: 2544
		private struct VariableNullabilityTable
		{
			// Token: 0x06006003 RID: 24579 RVA: 0x0014A387 File Offset: 0x00148587
			public VariableNullabilityTable(int capacity)
			{
				this._entries = Enumerable.Repeat<bool>(true, capacity).ToArray<bool>();
			}

			// Token: 0x1700108D RID: 4237
			public bool this[Var variable]
			{
				get
				{
					return variable.Id >= this._entries.Length || this._entries[variable.Id];
				}
				set
				{
					this.EnsureCapacity(variable.Id + 1);
					this._entries[variable.Id] = value;
				}
			}

			// Token: 0x06006006 RID: 24582 RVA: 0x0014A3DC File Offset: 0x001485DC
			private void EnsureCapacity(int minimum)
			{
				if (this._entries.Length < minimum)
				{
					int num = this._entries.Length * 2;
					if (num < minimum)
					{
						num = minimum;
					}
					bool[] array = Enumerable.Repeat<bool>(true, num).ToArray<bool>();
					Array.Copy(this._entries, 0, array, 0, this._entries.Length);
					this._entries = array;
				}
			}

			// Token: 0x040028AF RID: 10415
			private bool[] _entries;
		}
	}
}
