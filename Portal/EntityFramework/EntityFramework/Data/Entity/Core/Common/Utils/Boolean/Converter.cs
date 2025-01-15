using System;
using System.Linq;

namespace System.Data.Entity.Core.Common.Utils.Boolean
{
	// Token: 0x0200060A RID: 1546
	internal sealed class Converter<T_Identifier>
	{
		// Token: 0x06004B5F RID: 19295 RVA: 0x0010A37A File Offset: 0x0010857A
		internal Converter(BoolExpr<T_Identifier> expr, ConversionContext<T_Identifier> context)
		{
			this._context = context ?? IdentifierService<T_Identifier>.Instance.CreateConversionContext();
			this._vertex = ToDecisionDiagramConverter<T_Identifier>.TranslateToRobdd(expr, this._context);
		}

		// Token: 0x17000EB2 RID: 3762
		// (get) Token: 0x06004B60 RID: 19296 RVA: 0x0010A3A9 File Offset: 0x001085A9
		internal Vertex Vertex
		{
			get
			{
				return this._vertex;
			}
		}

		// Token: 0x17000EB3 RID: 3763
		// (get) Token: 0x06004B61 RID: 19297 RVA: 0x0010A3B1 File Offset: 0x001085B1
		internal DnfSentence<T_Identifier> Dnf
		{
			get
			{
				this.InitializeNormalForms();
				return this._dnf;
			}
		}

		// Token: 0x17000EB4 RID: 3764
		// (get) Token: 0x06004B62 RID: 19298 RVA: 0x0010A3BF File Offset: 0x001085BF
		internal CnfSentence<T_Identifier> Cnf
		{
			get
			{
				this.InitializeNormalForms();
				return this._cnf;
			}
		}

		// Token: 0x06004B63 RID: 19299 RVA: 0x0010A3D0 File Offset: 0x001085D0
		private void InitializeNormalForms()
		{
			if (this._cnf == null)
			{
				if (this._vertex.IsOne())
				{
					this._cnf = new CnfSentence<T_Identifier>(Set<CnfClause<T_Identifier>>.Empty);
					DnfClause<T_Identifier> dnfClause = new DnfClause<T_Identifier>(Set<Literal<T_Identifier>>.Empty);
					this._dnf = new DnfSentence<T_Identifier>(new Set<DnfClause<T_Identifier>> { dnfClause }.MakeReadOnly());
					return;
				}
				if (this._vertex.IsZero())
				{
					CnfClause<T_Identifier> cnfClause = new CnfClause<T_Identifier>(Set<Literal<T_Identifier>>.Empty);
					this._cnf = new CnfSentence<T_Identifier>(new Set<CnfClause<T_Identifier>> { cnfClause }.MakeReadOnly());
					this._dnf = new DnfSentence<T_Identifier>(Set<DnfClause<T_Identifier>>.Empty);
					return;
				}
				Set<DnfClause<T_Identifier>> set = new Set<DnfClause<T_Identifier>>();
				Set<CnfClause<T_Identifier>> set2 = new Set<CnfClause<T_Identifier>>();
				Set<Literal<T_Identifier>> set3 = new Set<Literal<T_Identifier>>();
				this.FindAllPaths(this._vertex, set2, set, set3);
				this._cnf = new CnfSentence<T_Identifier>(set2.MakeReadOnly());
				this._dnf = new DnfSentence<T_Identifier>(set.MakeReadOnly());
			}
		}

		// Token: 0x06004B64 RID: 19300 RVA: 0x0010A4C4 File Offset: 0x001086C4
		private void FindAllPaths(Vertex vertex, Set<CnfClause<T_Identifier>> cnfClauses, Set<DnfClause<T_Identifier>> dnfClauses, Set<Literal<T_Identifier>> path)
		{
			if (vertex.IsOne())
			{
				DnfClause<T_Identifier> dnfClause = new DnfClause<T_Identifier>(path);
				dnfClauses.Add(dnfClause);
				return;
			}
			if (vertex.IsZero())
			{
				CnfClause<T_Identifier> cnfClause = new CnfClause<T_Identifier>(new Set<Literal<T_Identifier>>(path.Select((Literal<T_Identifier> l) => l.MakeNegated())));
				cnfClauses.Add(cnfClause);
				return;
			}
			foreach (LiteralVertexPair<T_Identifier> literalVertexPair in this._context.GetSuccessors(vertex))
			{
				path.Add(literalVertexPair.Literal);
				this.FindAllPaths(literalVertexPair.Vertex, cnfClauses, dnfClauses, path);
				path.Remove(literalVertexPair.Literal);
			}
		}

		// Token: 0x04001A55 RID: 6741
		private readonly Vertex _vertex;

		// Token: 0x04001A56 RID: 6742
		private readonly ConversionContext<T_Identifier> _context;

		// Token: 0x04001A57 RID: 6743
		private DnfSentence<T_Identifier> _dnf;

		// Token: 0x04001A58 RID: 6744
		private CnfSentence<T_Identifier> _cnf;
	}
}
