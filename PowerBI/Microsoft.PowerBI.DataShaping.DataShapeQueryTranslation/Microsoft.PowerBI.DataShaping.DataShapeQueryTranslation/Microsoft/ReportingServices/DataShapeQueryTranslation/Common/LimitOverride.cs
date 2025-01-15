using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeDefinition;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.Common
{
	// Token: 0x02000110 RID: 272
	internal sealed class LimitOverride : IStructuredToString
	{
		// Token: 0x06000A71 RID: 2673 RVA: 0x000289A5 File Offset: 0x00026BA5
		private LimitOverride(Identifier limitId, ExpressionId? countOverride, ExpressionId? dbCount, ExpressionId? isExceededDbCount, bool shouldRemove, IReadOnlyDictionary<string, ExpressionId> properties, ExceededDetectionKind? exceededDetection)
		{
			this.m_limitId = limitId;
			this.m_countOverride = countOverride;
			this.m_dbCount = dbCount;
			this.m_isExceededDbCount = isExceededDbCount;
			this.m_shouldRemove = shouldRemove;
			this.m_properties = properties;
			this.m_exceededDetection = exceededDetection;
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x000289E4 File Offset: 0x00026BE4
		internal static LimitOverride RemoveLimit(Identifier limitId)
		{
			return new LimitOverride(limitId, null, null, null, true, null, null);
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x00028A1D File Offset: 0x00026C1D
		internal static LimitOverride OverrideLimit(Identifier limitId, ExpressionId? countOverride, ExpressionId? dbCount, ExpressionId? isExceededDbCount, ExceededDetectionKind? exceededDetection = null)
		{
			return new LimitOverride(limitId, countOverride, dbCount, isExceededDbCount, false, null, exceededDetection);
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x00028A2C File Offset: 0x00026C2C
		internal static LimitOverride OverrideLimit(Identifier limitId, ExpressionId? countOverride, ExpressionId? dbCount, ExpressionId? isExceededDbCount, IReadOnlyDictionary<string, ExpressionId> properties, ExceededDetectionKind? exceededDetection = null)
		{
			return new LimitOverride(limitId, countOverride, dbCount, isExceededDbCount, false, properties, exceededDetection);
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000A75 RID: 2677 RVA: 0x00028A3C File Offset: 0x00026C3C
		public Identifier LimitId
		{
			get
			{
				return this.m_limitId;
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000A76 RID: 2678 RVA: 0x00028A44 File Offset: 0x00026C44
		public ExpressionId? CountOverride
		{
			get
			{
				return this.m_countOverride;
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000A77 RID: 2679 RVA: 0x00028A4C File Offset: 0x00026C4C
		public ExpressionId? IsExceededDbCount
		{
			get
			{
				return this.m_isExceededDbCount;
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000A78 RID: 2680 RVA: 0x00028A54 File Offset: 0x00026C54
		public ExpressionId? DbCount
		{
			get
			{
				return this.m_dbCount;
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000A79 RID: 2681 RVA: 0x00028A5C File Offset: 0x00026C5C
		public bool ShouldRemove
		{
			get
			{
				return this.m_shouldRemove;
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000A7A RID: 2682 RVA: 0x00028A64 File Offset: 0x00026C64
		public ExceededDetectionKind? ExceededDetection
		{
			get
			{
				return this.m_exceededDetection;
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000A7B RID: 2683 RVA: 0x00028A6C File Offset: 0x00026C6C
		public IReadOnlyDictionary<string, ExpressionId> Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x00028A74 File Offset: 0x00026C74
		public override string ToString()
		{
			StructuredStringBuilder structuredStringBuilder = new StructuredStringBuilder(ExpressionStringBuilderFactory.Create(null, false), 0, false);
			this.WriteTo(structuredStringBuilder);
			return structuredStringBuilder.ToString();
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x00028AA0 File Offset: 0x00026CA0
		public void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("LimitOverride");
			builder.WriteAttribute<Identifier>("LimitId", this.m_limitId, false, false);
			builder.WriteProperty<ExpressionId?>("CountOverride", this.m_countOverride, false);
			builder.WriteProperty<ExpressionId?>("DbCount", this.m_dbCount, false);
			builder.WriteProperty<ExpressionId?>("IsExceededDbCount", this.m_isExceededDbCount, false);
			builder.WriteProperty<bool>("ShouldRemove", this.m_shouldRemove, false);
			builder.WriteProperty<ExceededDetectionKind?>("ExceededDetectionKind", this.m_exceededDetection, false);
			if (this.m_properties != null)
			{
				foreach (KeyValuePair<string, ExpressionId> keyValuePair in this.m_properties)
				{
					builder.WriteProperty<ExpressionId>(keyValuePair.Key, keyValuePair.Value, false);
				}
			}
			builder.EndObject();
		}

		// Token: 0x04000529 RID: 1321
		private readonly Identifier m_limitId;

		// Token: 0x0400052A RID: 1322
		private readonly ExpressionId? m_countOverride;

		// Token: 0x0400052B RID: 1323
		private readonly ExpressionId? m_dbCount;

		// Token: 0x0400052C RID: 1324
		private readonly ExpressionId? m_isExceededDbCount;

		// Token: 0x0400052D RID: 1325
		private readonly bool m_shouldRemove;

		// Token: 0x0400052E RID: 1326
		private readonly IReadOnlyDictionary<string, ExpressionId> m_properties;

		// Token: 0x0400052F RID: 1327
		private readonly ExceededDetectionKind? m_exceededDetection;
	}
}
