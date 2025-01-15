using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004BC RID: 1212
	[Serializable]
	internal sealed class ReportQuery : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x17001A25 RID: 6693
		// (get) Token: 0x06003D37 RID: 15671 RVA: 0x00106D3A File Offset: 0x00104F3A
		// (set) Token: 0x06003D38 RID: 15672 RVA: 0x00106D42 File Offset: 0x00104F42
		internal CommandType CommandType
		{
			get
			{
				return this.m_commandType;
			}
			set
			{
				this.m_commandType = value;
			}
		}

		// Token: 0x17001A26 RID: 6694
		// (get) Token: 0x06003D39 RID: 15673 RVA: 0x00106D4B File Offset: 0x00104F4B
		// (set) Token: 0x06003D3A RID: 15674 RVA: 0x00106D53 File Offset: 0x00104F53
		internal ExpressionInfo CommandText
		{
			get
			{
				return this.m_commandText;
			}
			set
			{
				this.m_commandText = value;
			}
		}

		// Token: 0x17001A27 RID: 6695
		// (get) Token: 0x06003D3B RID: 15675 RVA: 0x00106D5C File Offset: 0x00104F5C
		// (set) Token: 0x06003D3C RID: 15676 RVA: 0x00106D64 File Offset: 0x00104F64
		internal string PreviewCommandText
		{
			get
			{
				return this.m_previewCommandText;
			}
			set
			{
				this.m_previewCommandText = value;
			}
		}

		// Token: 0x17001A28 RID: 6696
		// (get) Token: 0x06003D3D RID: 15677 RVA: 0x00106D6D File Offset: 0x00104F6D
		// (set) Token: 0x06003D3E RID: 15678 RVA: 0x00106D75 File Offset: 0x00104F75
		internal List<ParameterValue> Parameters
		{
			get
			{
				return this.m_queryParameters;
			}
			set
			{
				this.m_queryParameters = value;
			}
		}

		// Token: 0x17001A29 RID: 6697
		// (get) Token: 0x06003D3F RID: 15679 RVA: 0x00106D7E File Offset: 0x00104F7E
		// (set) Token: 0x06003D40 RID: 15680 RVA: 0x00106D86 File Offset: 0x00104F86
		internal int TimeOut
		{
			get
			{
				return this.m_timeOut;
			}
			set
			{
				this.m_timeOut = value;
			}
		}

		// Token: 0x17001A2A RID: 6698
		// (get) Token: 0x06003D41 RID: 15681 RVA: 0x00106D8F File Offset: 0x00104F8F
		// (set) Token: 0x06003D42 RID: 15682 RVA: 0x00106D97 File Offset: 0x00104F97
		internal string DataSourceName
		{
			get
			{
				return this.m_dataSourceName;
			}
			set
			{
				this.m_dataSourceName = value;
			}
		}

		// Token: 0x06003D43 RID: 15683 RVA: 0x00106DA0 File Offset: 0x00104FA0
		internal void Initialize(InitializationContext context)
		{
			if (this.m_commandText != null)
			{
				this.m_commandText.Initialize("CommandText", context);
				context.ExprHostBuilder.DataSetQueryCommandText(this.m_commandText);
			}
			if (this.m_queryParameters != null)
			{
				Microsoft.ReportingServices.ReportProcessing.ObjectType objectType = context.ObjectType;
				string objectName = context.ObjectName;
				context.ObjectType = Microsoft.ReportingServices.ReportProcessing.ObjectType.QueryParameter;
				context.ExprHostBuilder.QueryParametersStart();
				for (int i = 0; i < this.m_queryParameters.Count; i++)
				{
					ParameterValue parameterValue = this.m_queryParameters[i];
					context.ObjectName = parameterValue.Name;
					parameterValue.Initialize(null, context, true);
				}
				context.ExprHostBuilder.QueryParametersEnd();
				context.ObjectType = objectType;
				context.ObjectName = objectName;
			}
		}

		// Token: 0x06003D44 RID: 15684 RVA: 0x00106E5A File Offset: 0x0010505A
		internal void SetExprHost(IndexedExprHost queryParamsExprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(queryParamsExprHost != null && reportObjectModel != null, "(queryParamsExprHost != null && reportObjectModel != null)");
			this.m_queryParamsExprHost = queryParamsExprHost;
			this.m_queryParamsExprHost.SetReportObjectModel(reportObjectModel);
		}

		// Token: 0x06003D45 RID: 15685 RVA: 0x00106E88 File Offset: 0x00105088
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ReportQuery, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.CommandType, Token.Enum),
				new MemberInfo(MemberName.CommandText, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.PreviewCommandText, Token.String),
				new MemberInfo(MemberName.QueryParameters, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ParameterValue),
				new MemberInfo(MemberName.Timeout, Token.Int32)
			});
		}

		// Token: 0x06003D46 RID: 15686 RVA: 0x00106F10 File Offset: 0x00105110
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(ReportQuery.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				switch (memberName)
				{
				case MemberName.CommandType:
					writer.WriteEnum((int)this.m_commandType);
					break;
				case MemberName.CommandText:
					writer.Write(this.m_commandText);
					break;
				case MemberName.QueryParameters:
					writer.Write<ParameterValue>(this.m_queryParameters);
					break;
				case MemberName.Timeout:
					writer.Write(this.m_timeOut);
					break;
				default:
					if (memberName != MemberName.PreviewCommandText)
					{
						Global.Tracer.Assert(false, string.Empty);
					}
					else
					{
						writer.Write(this.m_previewCommandText);
					}
					break;
				}
			}
		}

		// Token: 0x06003D47 RID: 15687 RVA: 0x00106FC8 File Offset: 0x001051C8
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(ReportQuery.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				switch (memberName)
				{
				case MemberName.CommandType:
					this.m_commandType = (CommandType)reader.ReadEnum();
					break;
				case MemberName.CommandText:
					this.m_commandText = (ExpressionInfo)reader.ReadRIFObject();
					break;
				case MemberName.QueryParameters:
					this.m_queryParameters = reader.ReadGenericListOfRIFObjects<ParameterValue>();
					break;
				case MemberName.Timeout:
					this.m_timeOut = reader.ReadInt32();
					break;
				default:
					if (memberName != MemberName.PreviewCommandText)
					{
						Global.Tracer.Assert(false, string.Empty);
					}
					else
					{
						this.m_previewCommandText = reader.ReadString();
					}
					break;
				}
			}
		}

		// Token: 0x06003D48 RID: 15688 RVA: 0x00107085 File Offset: 0x00105285
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false, string.Empty);
		}

		// Token: 0x06003D49 RID: 15689 RVA: 0x00107097 File Offset: 0x00105297
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ReportQuery;
		}

		// Token: 0x04001C9F RID: 7327
		private CommandType m_commandType = CommandType.Text;

		// Token: 0x04001CA0 RID: 7328
		private ExpressionInfo m_commandText;

		// Token: 0x04001CA1 RID: 7329
		private string m_previewCommandText;

		// Token: 0x04001CA2 RID: 7330
		private List<ParameterValue> m_queryParameters;

		// Token: 0x04001CA3 RID: 7331
		private int m_timeOut;

		// Token: 0x04001CA4 RID: 7332
		[NonSerialized]
		private string m_dataSourceName;

		// Token: 0x04001CA5 RID: 7333
		[NonSerialized]
		private IndexedExprHost m_queryParamsExprHost;

		// Token: 0x04001CA6 RID: 7334
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = ReportQuery.GetDeclaration();
	}
}
