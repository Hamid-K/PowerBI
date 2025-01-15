using System;
using System.Runtime.Serialization;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x020000D2 RID: 210
	[DataContract]
	internal sealed class IntermediateExpressionTableSchema : IIntermediateTableSchema
	{
		// Token: 0x06000773 RID: 1907 RVA: 0x0001C34C File Offset: 0x0001A54C
		internal IntermediateExpressionTableSchema(ExpressionNode referenceExpression, ConceptualTableType type)
		{
			this.ReferenceExpression = referenceExpression;
			this.Type = type;
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000774 RID: 1908 RVA: 0x0001C362 File Offset: 0x0001A562
		public ExpressionNode ReferenceExpression { get; }

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000775 RID: 1909 RVA: 0x0001C36A File Offset: 0x0001A56A
		public ConceptualTableType Type { get; }

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000776 RID: 1910 RVA: 0x0001C372 File Offset: 0x0001A572
		[DataMember(Name = "ReferenceExpression", Order = 1)]
		private string ReferenceExpressionForSerialization
		{
			get
			{
				return this.ReferenceExpression.ToString();
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000777 RID: 1911 RVA: 0x0001C37F File Offset: 0x0001A57F
		[DataMember(Name = "Type", Order = 2)]
		private string TypeForSerialization
		{
			get
			{
				return this.Type.ToString();
			}
		}
	}
}
