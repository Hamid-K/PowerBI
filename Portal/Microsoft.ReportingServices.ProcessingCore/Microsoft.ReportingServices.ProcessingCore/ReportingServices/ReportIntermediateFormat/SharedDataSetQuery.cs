using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004BB RID: 1211
	[Serializable]
	internal sealed class SharedDataSetQuery : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x17001A23 RID: 6691
		// (get) Token: 0x06003D2A RID: 15658 RVA: 0x00106AF7 File Offset: 0x00104CF7
		// (set) Token: 0x06003D2B RID: 15659 RVA: 0x00106AFF File Offset: 0x00104CFF
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

		// Token: 0x17001A24 RID: 6692
		// (get) Token: 0x06003D2C RID: 15660 RVA: 0x00106B08 File Offset: 0x00104D08
		// (set) Token: 0x06003D2D RID: 15661 RVA: 0x00106B10 File Offset: 0x00104D10
		internal string SharedDataSetReference
		{
			get
			{
				return this.m_originalSharedDataSetReference;
			}
			set
			{
				this.m_originalSharedDataSetReference = value;
			}
		}

		// Token: 0x06003D2E RID: 15662 RVA: 0x00106B1C File Offset: 0x00104D1C
		internal void Initialize(InitializationContext context)
		{
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

		// Token: 0x06003D2F RID: 15663 RVA: 0x00106BAB File Offset: 0x00104DAB
		internal void SetExprHost(IndexedExprHost queryParamsExprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(queryParamsExprHost != null && reportObjectModel != null, "(queryParamsExprHost != null && reportObjectModel != null)");
			this.m_queryParamsExprHost = queryParamsExprHost;
			this.m_queryParamsExprHost.SetReportObjectModel(reportObjectModel);
		}

		// Token: 0x06003D30 RID: 15664 RVA: 0x00106BDC File Offset: 0x00104DDC
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.SharedDataSetQuery, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.QueryParameters, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ParameterValue),
				new MemberInfo(MemberName.OriginalSharedDataSetReference, Token.String)
			});
		}

		// Token: 0x06003D31 RID: 15665 RVA: 0x00106C28 File Offset: 0x00104E28
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(SharedDataSetQuery.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.QueryParameters)
				{
					if (memberName != MemberName.OriginalSharedDataSetReference)
					{
						Global.Tracer.Assert(false, string.Empty);
					}
					else
					{
						writer.Write(this.m_originalSharedDataSetReference);
					}
				}
				else
				{
					writer.Write<ParameterValue>(this.m_queryParameters);
				}
			}
		}

		// Token: 0x06003D32 RID: 15666 RVA: 0x00106C9C File Offset: 0x00104E9C
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(SharedDataSetQuery.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.QueryParameters)
				{
					if (memberName != MemberName.OriginalSharedDataSetReference)
					{
						Global.Tracer.Assert(false, string.Empty);
					}
					else
					{
						this.m_originalSharedDataSetReference = reader.ReadString();
					}
				}
				else
				{
					this.m_queryParameters = reader.ReadGenericListOfRIFObjects<ParameterValue>();
				}
			}
		}

		// Token: 0x06003D33 RID: 15667 RVA: 0x00106D0D File Offset: 0x00104F0D
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false, string.Empty);
		}

		// Token: 0x06003D34 RID: 15668 RVA: 0x00106D1F File Offset: 0x00104F1F
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.SharedDataSetQuery;
		}

		// Token: 0x04001C9B RID: 7323
		private List<ParameterValue> m_queryParameters;

		// Token: 0x04001C9C RID: 7324
		private string m_originalSharedDataSetReference;

		// Token: 0x04001C9D RID: 7325
		[NonSerialized]
		private IndexedExprHost m_queryParamsExprHost;

		// Token: 0x04001C9E RID: 7326
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = SharedDataSetQuery.GetDeclaration();
	}
}
