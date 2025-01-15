using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;

namespace Microsoft.InfoNav.DataShapeQueryGeneration.DSQ
{
	// Token: 0x0200010B RID: 267
	[DataContract]
	internal sealed class IntermediateGroupingKey
	{
		// Token: 0x060008C9 RID: 2249 RVA: 0x0002336D File Offset: 0x0002156D
		internal IntermediateGroupingKey(ExpressionNode dsqRefNode, IConceptualColumn lineageProp, int? selectIndex, IReadOnlyList<int> selectIndicesWithThisIdentity, string formatString, bool isIdentityKey)
		{
			this.DsqReferenceExpression = dsqRefNode;
			this.LineageProperty = lineageProp;
			this.SelectIndex = selectIndex;
			this.SelectIndicesWithThisIdentity = selectIndicesWithThisIdentity;
			this.FormatString = formatString;
			this.IsIdentityKey = isIdentityKey;
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060008CA RID: 2250 RVA: 0x000233A2 File Offset: 0x000215A2
		internal ExpressionNode DsqReferenceExpression { get; }

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060008CB RID: 2251 RVA: 0x000233AA File Offset: 0x000215AA
		[DataMember(Name = "DsqReferenceExpression", Order = 1)]
		private string DsqReferenceExpressionForSerialization
		{
			get
			{
				return this.DsqReferenceExpression.ToString();
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060008CC RID: 2252 RVA: 0x000233B7 File Offset: 0x000215B7
		internal IConceptualColumn LineageProperty { get; }

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060008CD RID: 2253 RVA: 0x000233BF File Offset: 0x000215BF
		[DataMember(Name = "LineageProperty", EmitDefaultValue = false, Order = 2)]
		private string LineagePropertyForSerialization
		{
			get
			{
				return IntermediateTableSchemaSerializationUtils.Serialize(this.LineageProperty);
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x060008CE RID: 2254 RVA: 0x000233CC File Offset: 0x000215CC
		[DataMember(Order = 3, EmitDefaultValue = false)]
		internal int? SelectIndex { get; }

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x060008CF RID: 2255 RVA: 0x000233D4 File Offset: 0x000215D4
		[DataMember(Order = 4, EmitDefaultValue = false)]
		internal IReadOnlyList<int> SelectIndicesWithThisIdentity { get; }

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x060008D0 RID: 2256 RVA: 0x000233DC File Offset: 0x000215DC
		[DataMember(Order = 5, EmitDefaultValue = false)]
		internal string FormatString { get; }

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x060008D1 RID: 2257 RVA: 0x000233E4 File Offset: 0x000215E4
		[DataMember(Order = 6, EmitDefaultValue = false)]
		internal bool IsIdentityKey { get; }
	}
}
