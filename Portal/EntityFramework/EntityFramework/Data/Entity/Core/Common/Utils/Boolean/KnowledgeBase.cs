using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.Entity.Core.Common.Utils.Boolean
{
	// Token: 0x02000614 RID: 1556
	internal class KnowledgeBase<T_Identifier>
	{
		// Token: 0x06004B8C RID: 19340 RVA: 0x0010AB2D File Offset: 0x00108D2D
		internal KnowledgeBase()
		{
			this._facts = new List<BoolExpr<T_Identifier>>();
			this._knowledge = Vertex.One;
			this._context = IdentifierService<T_Identifier>.Instance.CreateConversionContext();
		}

		// Token: 0x17000EBB RID: 3771
		// (get) Token: 0x06004B8D RID: 19341 RVA: 0x0010AB5B File Offset: 0x00108D5B
		protected IEnumerable<BoolExpr<T_Identifier>> Facts
		{
			get
			{
				return this._facts;
			}
		}

		// Token: 0x06004B8E RID: 19342 RVA: 0x0010AB64 File Offset: 0x00108D64
		internal void AddKnowledgeBase(KnowledgeBase<T_Identifier> kb)
		{
			foreach (BoolExpr<T_Identifier> boolExpr in kb._facts)
			{
				this.AddFact(boolExpr);
			}
		}

		// Token: 0x06004B8F RID: 19343 RVA: 0x0010ABB8 File Offset: 0x00108DB8
		internal virtual void AddFact(BoolExpr<T_Identifier> fact)
		{
			this._facts.Add(fact);
			Vertex vertex = new Converter<T_Identifier>(fact, this._context).Vertex;
			this._knowledge = this._context.Solver.And(this._knowledge, vertex);
		}

		// Token: 0x06004B90 RID: 19344 RVA: 0x0010AC00 File Offset: 0x00108E00
		internal void AddImplication(BoolExpr<T_Identifier> condition, BoolExpr<T_Identifier> implies)
		{
			this.AddFact(new KnowledgeBase<T_Identifier>.Implication(condition, implies));
		}

		// Token: 0x06004B91 RID: 19345 RVA: 0x0010AC0F File Offset: 0x00108E0F
		internal void AddEquivalence(BoolExpr<T_Identifier> left, BoolExpr<T_Identifier> right)
		{
			this.AddFact(new KnowledgeBase<T_Identifier>.Equivalence(left, right));
		}

		// Token: 0x06004B92 RID: 19346 RVA: 0x0010AC20 File Offset: 0x00108E20
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("Facts:");
			foreach (BoolExpr<T_Identifier> boolExpr in this._facts)
			{
				stringBuilder.Append("\t").AppendLine(boolExpr.ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04001A6D RID: 6765
		private readonly List<BoolExpr<T_Identifier>> _facts;

		// Token: 0x04001A6E RID: 6766
		private Vertex _knowledge;

		// Token: 0x04001A6F RID: 6767
		private readonly ConversionContext<T_Identifier> _context;

		// Token: 0x02000C50 RID: 3152
		protected class Implication : OrExpr<T_Identifier>
		{
			// Token: 0x17001173 RID: 4467
			// (get) Token: 0x06006A7A RID: 27258 RVA: 0x0016C03B File Offset: 0x0016A23B
			internal BoolExpr<T_Identifier> Condition
			{
				get
				{
					return this._condition;
				}
			}

			// Token: 0x17001174 RID: 4468
			// (get) Token: 0x06006A7B RID: 27259 RVA: 0x0016C043 File Offset: 0x0016A243
			internal BoolExpr<T_Identifier> Implies
			{
				get
				{
					return this._implies;
				}
			}

			// Token: 0x06006A7C RID: 27260 RVA: 0x0016C04B File Offset: 0x0016A24B
			internal Implication(BoolExpr<T_Identifier> condition, BoolExpr<T_Identifier> implies)
				: base(new BoolExpr<T_Identifier>[]
				{
					condition.MakeNegated(),
					implies
				})
			{
				this._condition = condition;
				this._implies = implies;
			}

			// Token: 0x06006A7D RID: 27261 RVA: 0x0016C074 File Offset: 0x0016A274
			public override string ToString()
			{
				return StringUtil.FormatInvariant("{0} --> {1}", new object[] { this._condition, this._implies });
			}

			// Token: 0x040030CB RID: 12491
			private readonly BoolExpr<T_Identifier> _condition;

			// Token: 0x040030CC RID: 12492
			private readonly BoolExpr<T_Identifier> _implies;
		}

		// Token: 0x02000C51 RID: 3153
		protected class Equivalence : AndExpr<T_Identifier>
		{
			// Token: 0x17001175 RID: 4469
			// (get) Token: 0x06006A7E RID: 27262 RVA: 0x0016C098 File Offset: 0x0016A298
			internal BoolExpr<T_Identifier> Left
			{
				get
				{
					return this._left;
				}
			}

			// Token: 0x17001176 RID: 4470
			// (get) Token: 0x06006A7F RID: 27263 RVA: 0x0016C0A0 File Offset: 0x0016A2A0
			internal BoolExpr<T_Identifier> Right
			{
				get
				{
					return this._right;
				}
			}

			// Token: 0x06006A80 RID: 27264 RVA: 0x0016C0A8 File Offset: 0x0016A2A8
			internal Equivalence(BoolExpr<T_Identifier> left, BoolExpr<T_Identifier> right)
				: base(new BoolExpr<T_Identifier>[]
				{
					new KnowledgeBase<T_Identifier>.Implication(left, right),
					new KnowledgeBase<T_Identifier>.Implication(right, left)
				})
			{
				this._left = left;
				this._right = right;
			}

			// Token: 0x06006A81 RID: 27265 RVA: 0x0016C0D8 File Offset: 0x0016A2D8
			public override string ToString()
			{
				return StringUtil.FormatInvariant("{0} <--> {1}", new object[] { this._left, this._right });
			}

			// Token: 0x040030CD RID: 12493
			private readonly BoolExpr<T_Identifier> _left;

			// Token: 0x040030CE RID: 12494
			private readonly BoolExpr<T_Identifier> _right;
		}
	}
}
