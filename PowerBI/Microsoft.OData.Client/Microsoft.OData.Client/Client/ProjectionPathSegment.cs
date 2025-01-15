using System;
using System.Diagnostics;
using System.Linq.Expressions;
using Microsoft.OData.Client.Metadata;

namespace Microsoft.OData.Client
{
	// Token: 0x0200005E RID: 94
	[DebuggerDisplay("Segment {ProjectionType} {Member}")]
	internal class ProjectionPathSegment
	{
		// Token: 0x060002F8 RID: 760 RVA: 0x0000B5D3 File Offset: 0x000097D3
		internal ProjectionPathSegment(ProjectionPath startPath, string member, Type projectionType)
		{
			this.Member = member;
			this.StartPath = startPath;
			this.ProjectionType = projectionType;
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000B5F0 File Offset: 0x000097F0
		internal ProjectionPathSegment(ProjectionPath startPath, MemberExpression memberExpression)
		{
			this.StartPath = startPath;
			Expression expression = ResourceBinder.StripTo<Expression>(memberExpression.Expression);
			this.Member = ClientTypeUtil.GetServerDefinedName(memberExpression.Member);
			this.ProjectionType = memberExpression.Type;
			this.SourceTypeAs = ((expression.NodeType == ExpressionType.TypeAs) ? expression.Type : null);
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060002FA RID: 762 RVA: 0x0000B64C File Offset: 0x0000984C
		// (set) Token: 0x060002FB RID: 763 RVA: 0x0000B654 File Offset: 0x00009854
		internal string Member { get; private set; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060002FC RID: 764 RVA: 0x0000B65D File Offset: 0x0000985D
		// (set) Token: 0x060002FD RID: 765 RVA: 0x0000B665 File Offset: 0x00009865
		internal Type ProjectionType { get; set; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060002FE RID: 766 RVA: 0x0000B66E File Offset: 0x0000986E
		// (set) Token: 0x060002FF RID: 767 RVA: 0x0000B676 File Offset: 0x00009876
		internal Type SourceTypeAs { get; set; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000300 RID: 768 RVA: 0x0000B67F File Offset: 0x0000987F
		// (set) Token: 0x06000301 RID: 769 RVA: 0x0000B687 File Offset: 0x00009887
		internal ProjectionPath StartPath { get; private set; }
	}
}
