using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.VisualBasic;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

namespace Microsoft.ReportingServices.RdlExpressions.SafeExpressions
{
	// Token: 0x0200000F RID: 15
	internal sealed class ExpressionSyntaxWalker : VisualBasicSyntaxWalker, IExpressionVisitorHost
	{
		// Token: 0x06000039 RID: 57 RVA: 0x00002484 File Offset: 0x00000684
		public ExpressionSyntaxWalker(ExpressionSyntaxWalkerSharedState sharedState)
			: base(0)
		{
			this._sharedState = sharedState;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002494 File Offset: 0x00000694
		public void InitCollectionOfNodeEvaluations()
		{
			if (this._nodeEvaluations == null)
			{
				this._nodeEvaluations = new List<SyntaxNodeEvaluation>();
				return;
			}
			this._nodeEvaluations.Clear();
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000024B5 File Offset: 0x000006B5
		public List<SyntaxNodeEvaluation> GetNodeEvaluations()
		{
			return this._nodeEvaluations;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000024BD File Offset: 0x000006BD
		public bool ValidateAndCheckIsEnabled(ExpressionSyntax node)
		{
			this._isEnabled = true;
			this.Validate(node);
			return this._isEnabled;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000024D3 File Offset: 0x000006D3
		public override void DefaultVisit(SyntaxNode node)
		{
			throw new NotSupportedException("SyntaxNode <" + node.GetType().Name + "> is not supported yet.");
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000024F4 File Offset: 0x000006F4
		public override void VisitTernaryConditionalExpression(TernaryConditionalExpressionSyntax node)
		{
			this.Traverse<TernaryConditionalExpressionSyntax>(this._sharedState.TernaryConditionalExpressionVisitor, node);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002508 File Offset: 0x00000708
		public override void VisitBinaryExpression(BinaryExpressionSyntax node)
		{
			this.Traverse<BinaryExpressionSyntax>(this._sharedState.BinaryExpressionVisitor, node);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x0000251C File Offset: 0x0000071C
		public override void VisitUnaryExpression(UnaryExpressionSyntax node)
		{
			this.Traverse<UnaryExpressionSyntax>(this._sharedState.UnaryExpressionVisitor, node);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002530 File Offset: 0x00000730
		public override void VisitLiteralExpression(LiteralExpressionSyntax node)
		{
			this.Traverse<LiteralExpressionSyntax>(this._sharedState.LiteralExpressionVisitor, node);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002544 File Offset: 0x00000744
		public override void VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
		{
			this.Traverse<MemberAccessExpressionSyntax>(this._sharedState.MemberAccessExpressionVisitor, node);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002558 File Offset: 0x00000758
		public override void VisitInvocationExpression(InvocationExpressionSyntax node)
		{
			this.Traverse<InvocationExpressionSyntax>(this._sharedState.InvocationExpressionVisitor, node);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x0000256C File Offset: 0x0000076C
		public override void VisitParenthesizedExpression(ParenthesizedExpressionSyntax node)
		{
			this.Traverse<ParenthesizedExpressionSyntax>(this._sharedState.ParenthesizedExpressionVisitor, node);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002580 File Offset: 0x00000780
		public override void VisitIdentifierName(IdentifierNameSyntax node)
		{
			this.Traverse<IdentifierNameSyntax>(this._sharedState.IdentifierNameVisitor, node);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002594 File Offset: 0x00000794
		public override void VisitCollectionInitializer(CollectionInitializerSyntax node)
		{
			this.Traverse<CollectionInitializerSyntax>(this._sharedState.CollectionInitializerVisitor, node);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000025A8 File Offset: 0x000007A8
		public override void VisitPredefinedCastExpression(PredefinedCastExpressionSyntax node)
		{
			this.Traverse<PredefinedCastExpressionSyntax>(this._sharedState.PredefinedCastExpressionSyntaxVisitor, node);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000025BC File Offset: 0x000007BC
		public ExpressionEvaluationResult Evaluate(ExpressionSyntax node)
		{
			this._traversalType = TraversalType.Evaluate;
			this._result = ExpressionEvaluationResult.CreateNull();
			this.Visit(node);
			this.AddToNodeEvaluations(node, this._result);
			return this._result;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000025EC File Offset: 0x000007EC
		[return: TupleElementNames(new string[] { "Result", "Details" })]
		public ValueTuple<ExpressionEvaluationResult, ExpressionEvaluationDetails> EvaluateWithDetails(ExpressionSyntax node)
		{
			this._typeAlignmentInvalidated = false;
			ExpressionEvaluationResult expressionEvaluationResult = this.Evaluate(node);
			ExpressionEvaluationDetails expressionEvaluationDetails = new ExpressionEvaluationDetails(this._typeAlignmentInvalidated);
			return new ValueTuple<ExpressionEvaluationResult, ExpressionEvaluationDetails>(expressionEvaluationResult, expressionEvaluationDetails);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x0000261A File Offset: 0x0000081A
		public void Validate(ExpressionSyntax node)
		{
			this._traversalType = TraversalType.Validate;
			this.Visit(node);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x0000262A File Offset: 0x0000082A
		public ExpressionAnalysisResult Analyze(ExpressionSyntax node)
		{
			this._traversalType = TraversalType.Analyze;
			this._analysis = default(ExpressionAnalysisResult);
			this.Visit(node);
			return this._analysis;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x0000264C File Offset: 0x0000084C
		public void InvalidateTypeAlignment()
		{
			this._typeAlignmentInvalidated = true;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002658 File Offset: 0x00000858
		private void Traverse<T>(IExpressionSyntaxVisitor<T> visitor, T node)
		{
			switch (this._traversalType)
			{
			case TraversalType.Evaluate:
				this._result = visitor.Evaluate(this, node);
				return;
			case TraversalType.Validate:
				visitor.Validate(this, node);
				if (this._isEnabled)
				{
					this._isEnabled = visitor.IsEnabled(node);
					return;
				}
				break;
			case TraversalType.Analyze:
				this._analysis = visitor.Analyze(this, node);
				break;
			default:
				return;
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000026BC File Offset: 0x000008BC
		private void AddToNodeEvaluations(ExpressionSyntax node, ExpressionEvaluationResult result)
		{
			if (this._nodeEvaluations == null)
			{
				return;
			}
			if (this._nodeEvaluations.Count == 50)
			{
				this._nodeEvaluations.RemoveAt(this._nodeEvaluations.Count - 1);
			}
			this._nodeEvaluations.Insert(0, new SyntaxNodeEvaluation(node, this._result));
		}

		// Token: 0x0400000C RID: 12
		private const int MaxCountForCollectedNodeEvaluations = 50;

		// Token: 0x0400000D RID: 13
		private readonly ExpressionSyntaxWalkerSharedState _sharedState;

		// Token: 0x0400000E RID: 14
		private TraversalType _traversalType;

		// Token: 0x0400000F RID: 15
		private ExpressionEvaluationResult _result;

		// Token: 0x04000010 RID: 16
		private ExpressionAnalysisResult _analysis;

		// Token: 0x04000011 RID: 17
		private bool _isEnabled;

		// Token: 0x04000012 RID: 18
		private bool _typeAlignmentInvalidated;

		// Token: 0x04000013 RID: 19
		private List<SyntaxNodeEvaluation> _nodeEvaluations;
	}
}
