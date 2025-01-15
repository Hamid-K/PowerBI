using System;
using System.Collections.ObjectModel;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.Mashup.Engine1.Library.OData.V4_7.QueryBuilder
{
	// Token: 0x020007CB RID: 1995
	internal sealed class ODataExpression
	{
		// Token: 0x06003A0C RID: 14860 RVA: 0x000BB984 File Offset: 0x000B9B84
		public ODataExpression(SingleValueNode node, TypeValue type)
		{
			this.node = node;
			this.type = type;
		}

		// Token: 0x17001391 RID: 5009
		// (get) Token: 0x06003A0D RID: 14861 RVA: 0x000BB99A File Offset: 0x000B9B9A
		public SingleValueNode Node
		{
			get
			{
				return this.node;
			}
		}

		// Token: 0x17001392 RID: 5010
		// (get) Token: 0x06003A0E RID: 14862 RVA: 0x000BB9A2 File Offset: 0x000B9BA2
		public TypeValue Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x06003A0F RID: 14863 RVA: 0x000BB9AC File Offset: 0x000B9BAC
		public static ConstantNode BuildConstantNode(Value value)
		{
			ODataQueryExpressionVisitor odataQueryExpressionVisitor = new ODataQueryExpressionVisitor(null, null, null);
			ConstantNode constantNode;
			try
			{
				constantNode = (ConstantNode)odataQueryExpressionVisitor.Compile(new ConstantQueryExpression(value)).Node;
			}
			catch (NotSupportedException)
			{
				throw ValueException.NewExpressionError<Message1>(Strings.ODataConstantNotSupported(value.Kind), null, null);
			}
			return constantNode;
		}

		// Token: 0x06003A10 RID: 14864 RVA: 0x000BBA08 File Offset: 0x000B9C08
		public static SingleValueNode AppendNullRowsExprToExpandedRecordFilter(SingleValueNode recordExpr, SingleNavigationNode singleNavSource)
		{
			if (recordExpr != null)
			{
				SingleValueNode singleValueNode = new BinaryOperatorNode(BinaryOperatorKind.Equal, singleNavSource, new ConstantNode(null));
				return new BinaryOperatorNode(BinaryOperatorKind.Or, recordExpr, singleValueNode);
			}
			return recordExpr;
		}

		// Token: 0x06003A11 RID: 14865 RVA: 0x000BBA30 File Offset: 0x000B9C30
		public static SingleValueNode AppendNullRowsExprToExpandedTableFilter(SingleValueNode collectionExpr, CollectionNavigationNode collectionSource)
		{
			if (collectionExpr != null)
			{
				AnyNode anyNode = new AnyNode(new Collection<RangeVariable> { null })
				{
					Body = new ConstantNode(null),
					Source = collectionSource
				};
				SingleValueNode singleValueNode = new UnaryOperatorNode(UnaryOperatorKind.Not, anyNode);
				return new BinaryOperatorNode(BinaryOperatorKind.Or, collectionExpr, singleValueNode);
			}
			return collectionExpr;
		}

		// Token: 0x04001E21 RID: 7713
		public static readonly ConstantNode EmptyStringConstant = new ConstantNode(string.Empty, "''", EdmCoreModel.Instance.GetPrimitive(Microsoft.OData.Edm.EdmPrimitiveTypeKind.String, true));

		// Token: 0x04001E22 RID: 7714
		private readonly SingleValueNode node;

		// Token: 0x04001E23 RID: 7715
		private readonly TypeValue type;
	}
}
