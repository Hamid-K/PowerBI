using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.QueryBindingDescriptor;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x0200000F RID: 15
	internal sealed class DataShapeExpressionsAxisGroupingKeyBuilder
	{
		// Token: 0x06000090 RID: 144 RVA: 0x00004A16 File Offset: 0x00002C16
		internal DataShapeExpressionsAxisGroupingKeyBuilder()
		{
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00004A1E File Offset: 0x00002C1E
		internal void WithSource(ConceptualPropertyReference source, IConceptualColumn sourceField)
		{
			this.Source = source;
			this.SourceField = sourceField;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00004A2E File Offset: 0x00002C2E
		internal void WithId(int? selectIndex, string calcId, IReadOnlyList<int> selectIndicesWithThisIdentity)
		{
			this.Select = selectIndex;
			this.Calc = calcId;
			this.SelectIndicesWithThisIdentity = selectIndicesWithThisIdentity;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00004A45 File Offset: 0x00002C45
		internal void WithIsIdentity(bool isIdentityKey)
		{
			this.IsIdentityKey = isIdentityKey;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00004A4E File Offset: 0x00002C4E
		// (set) Token: 0x06000095 RID: 149 RVA: 0x00004A56 File Offset: 0x00002C56
		internal ConceptualPropertyReference Source { get; private set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00004A5F File Offset: 0x00002C5F
		// (set) Token: 0x06000097 RID: 151 RVA: 0x00004A67 File Offset: 0x00002C67
		internal int? Select { get; private set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00004A70 File Offset: 0x00002C70
		// (set) Token: 0x06000099 RID: 153 RVA: 0x00004A78 File Offset: 0x00002C78
		internal string Calc { get; private set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00004A81 File Offset: 0x00002C81
		// (set) Token: 0x0600009B RID: 155 RVA: 0x00004A89 File Offset: 0x00002C89
		internal IConceptualColumn SourceField { get; private set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00004A92 File Offset: 0x00002C92
		// (set) Token: 0x0600009D RID: 157 RVA: 0x00004A9A File Offset: 0x00002C9A
		internal IReadOnlyList<int> SelectIndicesWithThisIdentity { get; private set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00004AA3 File Offset: 0x00002CA3
		// (set) Token: 0x0600009F RID: 159 RVA: 0x00004AAB File Offset: 0x00002CAB
		internal bool IsIdentityKey { get; private set; }

		// Token: 0x060000A0 RID: 160 RVA: 0x00004AB4 File Offset: 0x00002CB4
		internal string ResolveCalcId(Func<int, string> getCalcIdForSelect)
		{
			string text = this.Calc;
			if (text == null && this.Select != null)
			{
				text = getCalcIdForSelect(this.Select.Value);
			}
			return text;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00004AF1 File Offset: 0x00002CF1
		internal DataShapeExpressionsAxisGroupingKey Build()
		{
			return new DataShapeExpressionsAxisGroupingKey
			{
				Source = this.Source,
				Select = this.Select,
				Calc = this.Calc
			};
		}
	}
}
