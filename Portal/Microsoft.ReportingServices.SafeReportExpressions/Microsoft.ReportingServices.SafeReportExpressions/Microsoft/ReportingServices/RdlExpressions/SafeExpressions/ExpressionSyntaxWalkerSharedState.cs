using System;

namespace Microsoft.ReportingServices.RdlExpressions.SafeExpressions
{
	// Token: 0x02000010 RID: 16
	internal sealed class ExpressionSyntaxWalkerSharedState
	{
		// Token: 0x0600004F RID: 79 RVA: 0x00002714 File Offset: 0x00000914
		public ExpressionSyntaxWalkerSharedState(ISafeExpressionsReportContext reportContext)
		{
			FunctionFactory functionFactory = new FunctionFactory(reportContext);
			this._ternaryConditionalExpressionVisitor = new TernaryConditionalExpressionVisitor();
			this._binaryExpressionVisitor = new BinaryExpressionVisitor();
			this._unaryExpressionVisitor = new UnaryExpressionVisitor();
			this._literalExpressionVisitor = new LiteralExpressionVisitor();
			this._memberAccessExpressionVisitor = new MemberAccessExpressionVisitor(reportContext);
			this._invocationExpressionVisitor = new InvocationExpressionVisitor(functionFactory);
			this._parenthesizedExpressionVisitor = new ParenthesizedExpressionVisitor();
			this._identifierNameVisitor = new IdentifierNameVisitor(functionFactory);
			this._collectionInitializerVisitor = new CollectionInitializerVisitor();
			this._predefinedCastExpressionSyntaxVisitor = new PredefinedCastExpressionSyntaxVisitor();
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000050 RID: 80 RVA: 0x0000279F File Offset: 0x0000099F
		public TernaryConditionalExpressionVisitor TernaryConditionalExpressionVisitor
		{
			get
			{
				return this._ternaryConditionalExpressionVisitor;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000051 RID: 81 RVA: 0x000027A7 File Offset: 0x000009A7
		public BinaryExpressionVisitor BinaryExpressionVisitor
		{
			get
			{
				return this._binaryExpressionVisitor;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000052 RID: 82 RVA: 0x000027AF File Offset: 0x000009AF
		public UnaryExpressionVisitor UnaryExpressionVisitor
		{
			get
			{
				return this._unaryExpressionVisitor;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000053 RID: 83 RVA: 0x000027B7 File Offset: 0x000009B7
		public LiteralExpressionVisitor LiteralExpressionVisitor
		{
			get
			{
				return this._literalExpressionVisitor;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000054 RID: 84 RVA: 0x000027BF File Offset: 0x000009BF
		public MemberAccessExpressionVisitor MemberAccessExpressionVisitor
		{
			get
			{
				return this._memberAccessExpressionVisitor;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000055 RID: 85 RVA: 0x000027C7 File Offset: 0x000009C7
		public InvocationExpressionVisitor InvocationExpressionVisitor
		{
			get
			{
				return this._invocationExpressionVisitor;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000056 RID: 86 RVA: 0x000027CF File Offset: 0x000009CF
		public ParenthesizedExpressionVisitor ParenthesizedExpressionVisitor
		{
			get
			{
				return this._parenthesizedExpressionVisitor;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000057 RID: 87 RVA: 0x000027D7 File Offset: 0x000009D7
		public IdentifierNameVisitor IdentifierNameVisitor
		{
			get
			{
				return this._identifierNameVisitor;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000058 RID: 88 RVA: 0x000027DF File Offset: 0x000009DF
		public CollectionInitializerVisitor CollectionInitializerVisitor
		{
			get
			{
				return this._collectionInitializerVisitor;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000059 RID: 89 RVA: 0x000027E7 File Offset: 0x000009E7
		public PredefinedCastExpressionSyntaxVisitor PredefinedCastExpressionSyntaxVisitor
		{
			get
			{
				return this._predefinedCastExpressionSyntaxVisitor;
			}
		}

		// Token: 0x04000014 RID: 20
		private readonly TernaryConditionalExpressionVisitor _ternaryConditionalExpressionVisitor;

		// Token: 0x04000015 RID: 21
		private readonly BinaryExpressionVisitor _binaryExpressionVisitor;

		// Token: 0x04000016 RID: 22
		private readonly UnaryExpressionVisitor _unaryExpressionVisitor;

		// Token: 0x04000017 RID: 23
		private readonly LiteralExpressionVisitor _literalExpressionVisitor;

		// Token: 0x04000018 RID: 24
		private readonly MemberAccessExpressionVisitor _memberAccessExpressionVisitor;

		// Token: 0x04000019 RID: 25
		private readonly InvocationExpressionVisitor _invocationExpressionVisitor;

		// Token: 0x0400001A RID: 26
		private readonly ParenthesizedExpressionVisitor _parenthesizedExpressionVisitor;

		// Token: 0x0400001B RID: 27
		private readonly IdentifierNameVisitor _identifierNameVisitor;

		// Token: 0x0400001C RID: 28
		private readonly CollectionInitializerVisitor _collectionInitializerVisitor;

		// Token: 0x0400001D RID: 29
		private readonly PredefinedCastExpressionSyntaxVisitor _predefinedCastExpressionSyntaxVisitor;
	}
}
