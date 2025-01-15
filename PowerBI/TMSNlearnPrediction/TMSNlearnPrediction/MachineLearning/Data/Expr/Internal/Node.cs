using System;
using System.IO;
using Microsoft.MachineLearning.Internal.Lexer;

namespace Microsoft.MachineLearning.Data.Expr.Internal
{
	// Token: 0x020001A5 RID: 421
	internal abstract class Node
	{
		// Token: 0x060008FA RID: 2298 RVA: 0x000322F0 File Offset: 0x000304F0
		protected Node(Token tok)
		{
			this.Token = tok;
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060008FB RID: 2299
		public abstract NodeKind Kind { get; }

		// Token: 0x060008FC RID: 2300
		public abstract void Accept(NodeVisitor visitor);

		// Token: 0x060008FD RID: 2301 RVA: 0x000322FF File Offset: 0x000304FF
		private T Cast<T>() where T : Node
		{
			return (T)((object)this);
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060008FE RID: 2302 RVA: 0x00032307 File Offset: 0x00030507
		public virtual LambdaNode AsPredicate
		{
			get
			{
				return this.Cast<LambdaNode>();
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060008FF RID: 2303 RVA: 0x0003230F File Offset: 0x0003050F
		public virtual LambdaNode TestPredicate
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000900 RID: 2304 RVA: 0x00032312 File Offset: 0x00030512
		public virtual ParamNode AsParam
		{
			get
			{
				return this.Cast<ParamNode>();
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000901 RID: 2305 RVA: 0x0003231A File Offset: 0x0003051A
		public virtual ParamNode TestParam
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000902 RID: 2306 RVA: 0x0003231D File Offset: 0x0003051D
		public virtual ConditionalNode AsConditional
		{
			get
			{
				return this.Cast<ConditionalNode>();
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000903 RID: 2307 RVA: 0x00032325 File Offset: 0x00030525
		public virtual ConditionalNode TestConditional
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000904 RID: 2308 RVA: 0x00032328 File Offset: 0x00030528
		public virtual BinaryOpNode AsBinaryOp
		{
			get
			{
				return this.Cast<BinaryOpNode>();
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000905 RID: 2309 RVA: 0x00032330 File Offset: 0x00030530
		public virtual BinaryOpNode TestBinaryOp
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000906 RID: 2310 RVA: 0x00032333 File Offset: 0x00030533
		public virtual UnaryOpNode AsUnaryOp
		{
			get
			{
				return this.Cast<UnaryOpNode>();
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000907 RID: 2311 RVA: 0x0003233B File Offset: 0x0003053B
		public virtual UnaryOpNode TestUnaryOp
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000908 RID: 2312 RVA: 0x0003233E File Offset: 0x0003053E
		public virtual CompareNode AsCompare
		{
			get
			{
				return this.Cast<CompareNode>();
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000909 RID: 2313 RVA: 0x00032346 File Offset: 0x00030546
		public virtual CompareNode TestCompare
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600090A RID: 2314 RVA: 0x00032349 File Offset: 0x00030549
		public virtual CallNode AsCall
		{
			get
			{
				return this.Cast<CallNode>();
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600090B RID: 2315 RVA: 0x00032351 File Offset: 0x00030551
		public virtual CallNode TestCall
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600090C RID: 2316 RVA: 0x00032354 File Offset: 0x00030554
		public virtual ListNode AsList
		{
			get
			{
				return this.Cast<ListNode>();
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600090D RID: 2317 RVA: 0x0003235C File Offset: 0x0003055C
		public virtual ListNode TestList
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600090E RID: 2318 RVA: 0x0003235F File Offset: 0x0003055F
		public virtual WithNode AsWith
		{
			get
			{
				return this.Cast<WithNode>();
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x0600090F RID: 2319 RVA: 0x00032367 File Offset: 0x00030567
		public virtual WithNode TestWith
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000910 RID: 2320 RVA: 0x0003236A File Offset: 0x0003056A
		public virtual WithLocalNode AsWithLocal
		{
			get
			{
				return this.Cast<WithLocalNode>();
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000911 RID: 2321 RVA: 0x00032372 File Offset: 0x00030572
		public virtual WithLocalNode TestWithLocal
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000912 RID: 2322 RVA: 0x00032375 File Offset: 0x00030575
		public virtual NameNode AsName
		{
			get
			{
				return this.Cast<NameNode>();
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000913 RID: 2323 RVA: 0x0003237D File Offset: 0x0003057D
		public virtual NameNode TestName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000914 RID: 2324 RVA: 0x00032380 File Offset: 0x00030580
		public virtual IdentNode AsIdent
		{
			get
			{
				return this.Cast<IdentNode>();
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000915 RID: 2325 RVA: 0x00032388 File Offset: 0x00030588
		public virtual IdentNode TestIdent
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000916 RID: 2326 RVA: 0x0003238B File Offset: 0x0003058B
		public virtual BoolLitNode AsBoolLit
		{
			get
			{
				return this.Cast<BoolLitNode>();
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000917 RID: 2327 RVA: 0x00032393 File Offset: 0x00030593
		public virtual BoolLitNode TestBoolLit
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000918 RID: 2328 RVA: 0x00032396 File Offset: 0x00030596
		public virtual NumLitNode AsNumLit
		{
			get
			{
				return this.Cast<NumLitNode>();
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000919 RID: 2329 RVA: 0x0003239E File Offset: 0x0003059E
		public virtual NumLitNode TestNumLit
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600091A RID: 2330 RVA: 0x000323A1 File Offset: 0x000305A1
		public virtual StrLitNode AsStrLit
		{
			get
			{
				return this.Cast<StrLitNode>();
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600091B RID: 2331 RVA: 0x000323A9 File Offset: 0x000305A9
		public virtual StrLitNode TestStrLit
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600091C RID: 2332 RVA: 0x000323AC File Offset: 0x000305AC
		public virtual ExprNode AsExpr
		{
			get
			{
				return this.Cast<ExprNode>();
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600091D RID: 2333 RVA: 0x000323B4 File Offset: 0x000305B4
		public virtual ExprNode TestExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x000323B8 File Offset: 0x000305B8
		public override string ToString()
		{
			string text;
			using (StringWriter stringWriter = new StringWriter())
			{
				NodePrinter.Print(this, stringWriter, false, false);
				text = stringWriter.ToString();
			}
			return text;
		}

		// Token: 0x040004D6 RID: 1238
		public readonly Token Token;
	}
}
