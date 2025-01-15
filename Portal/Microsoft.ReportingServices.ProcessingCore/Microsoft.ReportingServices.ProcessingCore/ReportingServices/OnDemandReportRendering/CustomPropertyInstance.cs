using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020002B5 RID: 693
	public sealed class CustomPropertyInstance
	{
		// Token: 0x06001A80 RID: 6784 RVA: 0x0006AEC8 File Offset: 0x000690C8
		internal CustomPropertyInstance(CustomProperty customPropertyDef, string name, object value, TypeCode typeCode)
		{
			this.m_customPropertyDef = customPropertyDef;
			this.m_name = name;
			this.m_value = value;
			this.m_typeCode = typeCode;
		}

		// Token: 0x17000F15 RID: 3861
		// (get) Token: 0x06001A81 RID: 6785 RVA: 0x0006AEED File Offset: 0x000690ED
		// (set) Token: 0x06001A82 RID: 6786 RVA: 0x0006AEF8 File Offset: 0x000690F8
		public string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				ReportElement reportElementOwner = this.m_customPropertyDef.ReportElementOwner;
				if (reportElementOwner == null || reportElementOwner.CriGenerationPhase == ReportElement.CriGenerationPhases.None || (reportElementOwner.CriGenerationPhase == ReportElement.CriGenerationPhases.Instance && !this.m_customPropertyDef.Name.IsExpression))
				{
					throw new RenderingObjectModelException(RPRes.rsErrorDuringROMWriteback);
				}
				this.m_name = value;
			}
		}

		// Token: 0x17000F16 RID: 3862
		// (get) Token: 0x06001A83 RID: 6787 RVA: 0x0006AF49 File Offset: 0x00069149
		internal TypeCode TypeCode
		{
			get
			{
				return this.m_typeCode;
			}
		}

		// Token: 0x17000F17 RID: 3863
		// (get) Token: 0x06001A84 RID: 6788 RVA: 0x0006AF51 File Offset: 0x00069151
		// (set) Token: 0x06001A85 RID: 6789 RVA: 0x0006AF5C File Offset: 0x0006915C
		public object Value
		{
			get
			{
				return this.m_value;
			}
			set
			{
				ReportElement reportElementOwner = this.m_customPropertyDef.ReportElementOwner;
				if (reportElementOwner == null || reportElementOwner.CriGenerationPhase == ReportElement.CriGenerationPhases.None || (reportElementOwner.CriGenerationPhase == ReportElement.CriGenerationPhases.Instance && !this.m_customPropertyDef.Value.IsExpression))
				{
					throw new RenderingObjectModelException(RPRes.rsErrorDuringROMWriteback);
				}
				if (value != null)
				{
					if (reportElementOwner.CriGenerationPhase == ReportElement.CriGenerationPhases.Definition)
					{
						if (!(value is string))
						{
							throw new RenderingObjectModelException(RPRes.rsErrorDuringROMWritebackStringExpected);
						}
					}
					else if (!ReportRuntime.IsVariant(value))
					{
						throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
					}
				}
				this.m_value = value;
			}
		}

		// Token: 0x06001A86 RID: 6790 RVA: 0x0006AFDF File Offset: 0x000691DF
		internal void Update(string name, object value, TypeCode typeCode)
		{
			this.m_name = name;
			this.m_value = value;
			this.m_typeCode = typeCode;
		}

		// Token: 0x04000D33 RID: 3379
		private CustomProperty m_customPropertyDef;

		// Token: 0x04000D34 RID: 3380
		private string m_name;

		// Token: 0x04000D35 RID: 3381
		private object m_value;

		// Token: 0x04000D36 RID: 3382
		private TypeCode m_typeCode;
	}
}
