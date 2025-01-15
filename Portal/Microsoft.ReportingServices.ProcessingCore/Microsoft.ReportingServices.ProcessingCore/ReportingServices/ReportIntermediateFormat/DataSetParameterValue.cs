using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004BE RID: 1214
	[Serializable]
	internal class DataSetParameterValue : ParameterValue, IParameterDef
	{
		// Token: 0x17001A35 RID: 6709
		// (get) Token: 0x06003D67 RID: 15719 RVA: 0x001074BA File Offset: 0x001056BA
		// (set) Token: 0x06003D68 RID: 15720 RVA: 0x001074C2 File Offset: 0x001056C2
		internal bool ReadOnly
		{
			get
			{
				return this.m_readOnly;
			}
			set
			{
				this.m_readOnly = value;
			}
		}

		// Token: 0x17001A36 RID: 6710
		// (get) Token: 0x06003D69 RID: 15721 RVA: 0x001074CB File Offset: 0x001056CB
		// (set) Token: 0x06003D6A RID: 15722 RVA: 0x001074D3 File Offset: 0x001056D3
		internal bool Nullable
		{
			get
			{
				return this.m_nullable;
			}
			set
			{
				this.m_nullable = value;
			}
		}

		// Token: 0x17001A37 RID: 6711
		// (get) Token: 0x06003D6B RID: 15723 RVA: 0x001074DC File Offset: 0x001056DC
		// (set) Token: 0x06003D6C RID: 15724 RVA: 0x001074E4 File Offset: 0x001056E4
		internal bool OmitFromQuery
		{
			get
			{
				return this.m_omitFromQuery;
			}
			set
			{
				this.m_omitFromQuery = value;
			}
		}

		// Token: 0x06003D6D RID: 15725 RVA: 0x001074F0 File Offset: 0x001056F0
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataSetParameterValue, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ParameterValue, new List<MemberInfo>
			{
				new MemberInfo(MemberName.ReadOnly, Token.Boolean),
				new MemberInfo(MemberName.Nullable, Token.Boolean),
				new MemberInfo(MemberName.OmitFromQuery, Token.Boolean)
			});
		}

		// Token: 0x06003D6E RID: 15726 RVA: 0x00107552 File Offset: 0x00105752
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataSetParameterValue;
		}

		// Token: 0x06003D6F RID: 15727 RVA: 0x0010755C File Offset: 0x0010575C
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(DataSetParameterValue.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.Nullable)
				{
					if (memberName != MemberName.ReadOnly)
					{
						if (memberName != MemberName.OmitFromQuery)
						{
							Global.Tracer.Assert(false, string.Empty);
						}
						else
						{
							writer.Write(this.m_omitFromQuery);
						}
					}
					else
					{
						writer.Write(this.m_readOnly);
					}
				}
				else
				{
					writer.Write(this.m_nullable);
				}
			}
		}

		// Token: 0x06003D70 RID: 15728 RVA: 0x001075EC File Offset: 0x001057EC
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(DataSetParameterValue.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.Nullable)
				{
					if (memberName != MemberName.ReadOnly)
					{
						if (memberName != MemberName.OmitFromQuery)
						{
							Global.Tracer.Assert(false);
						}
						else
						{
							this.m_omitFromQuery = reader.ReadBoolean();
						}
					}
					else
					{
						this.m_readOnly = reader.ReadBoolean();
					}
				}
				else
				{
					this.m_nullable = reader.ReadBoolean();
				}
			}
		}

		// Token: 0x17001A38 RID: 6712
		// (get) Token: 0x06003D71 RID: 15729 RVA: 0x00107676 File Offset: 0x00105876
		string IParameterDef.Name
		{
			get
			{
				return base.Name;
			}
		}

		// Token: 0x17001A39 RID: 6713
		// (get) Token: 0x06003D72 RID: 15730 RVA: 0x0010767E File Offset: 0x0010587E
		Microsoft.ReportingServices.ReportProcessing.ObjectType IParameterDef.ParameterObjectType
		{
			get
			{
				return Microsoft.ReportingServices.ReportProcessing.ObjectType.QueryParameter;
			}
		}

		// Token: 0x17001A3A RID: 6714
		// (get) Token: 0x06003D73 RID: 15731 RVA: 0x00107682 File Offset: 0x00105882
		DataType IParameterDef.DataType
		{
			get
			{
				return DataType.Object;
			}
		}

		// Token: 0x17001A3B RID: 6715
		// (get) Token: 0x06003D74 RID: 15732 RVA: 0x00107685 File Offset: 0x00105885
		public bool MultiValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003D75 RID: 15733 RVA: 0x00107688 File Offset: 0x00105888
		public bool ValidateValueForNull(object newValue, ErrorContext errorContext, string parameterValueProperty)
		{
			return ParameterBase.ValidateValueForNull(newValue, this.Nullable, errorContext, Microsoft.ReportingServices.ReportProcessing.ObjectType.QueryParameter, base.Name, parameterValueProperty);
		}

		// Token: 0x06003D76 RID: 15734 RVA: 0x001076A0 File Offset: 0x001058A0
		public bool ValidateValueForBlank(object newValue, ErrorContext errorContext, string parameterValueProperty)
		{
			return true;
		}

		// Token: 0x06003D77 RID: 15735 RVA: 0x001076A3 File Offset: 0x001058A3
		public bool HasDefaultValuesExpressions()
		{
			return base.Value != null && base.Value.IsExpression;
		}

		// Token: 0x06003D78 RID: 15736 RVA: 0x001076BA File Offset: 0x001058BA
		public bool HasDefaultValuesDataSource()
		{
			return false;
		}

		// Token: 0x17001A3C RID: 6716
		// (get) Token: 0x06003D79 RID: 15737 RVA: 0x001076BD File Offset: 0x001058BD
		public int DefaultValuesExpressionCount
		{
			get
			{
				return (base.Value != null) ? 1 : 0;
			}
		}

		// Token: 0x06003D7A RID: 15738 RVA: 0x001076C8 File Offset: 0x001058C8
		public bool HasValidValuesValueExpressions()
		{
			return false;
		}

		// Token: 0x06003D7B RID: 15739 RVA: 0x001076CB File Offset: 0x001058CB
		public bool HasValidValuesLabelExpressions()
		{
			return false;
		}

		// Token: 0x06003D7C RID: 15740 RVA: 0x001076CE File Offset: 0x001058CE
		public bool HasValidValuesDataSource()
		{
			return false;
		}

		// Token: 0x17001A3D RID: 6717
		// (get) Token: 0x06003D7D RID: 15741 RVA: 0x001076D1 File Offset: 0x001058D1
		public int ValidValuesValueExpressionCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17001A3E RID: 6718
		// (get) Token: 0x06003D7E RID: 15742 RVA: 0x001076D4 File Offset: 0x001058D4
		public int ValidValuesLabelExpressionCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17001A3F RID: 6719
		// (get) Token: 0x06003D7F RID: 15743 RVA: 0x001076D7 File Offset: 0x001058D7
		public IParameterDataSource DefaultDataSource
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001A40 RID: 6720
		// (get) Token: 0x06003D80 RID: 15744 RVA: 0x001076DE File Offset: 0x001058DE
		public IParameterDataSource ValidValuesDataSource
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001A41 RID: 6721
		// (get) Token: 0x06003D81 RID: 15745 RVA: 0x001076E5 File Offset: 0x001058E5
		public new bool UseAllValidValues
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04001CB2 RID: 7346
		private bool m_readOnly;

		// Token: 0x04001CB3 RID: 7347
		private bool m_nullable;

		// Token: 0x04001CB4 RID: 7348
		private bool m_omitFromQuery;

		// Token: 0x04001CB5 RID: 7349
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = DataSetParameterValue.GetDeclaration();
	}
}
