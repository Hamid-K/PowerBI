using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000526 RID: 1318
	[Serializable]
	internal class ParameterValue : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x17001D94 RID: 7572
		// (get) Token: 0x06004718 RID: 18200 RVA: 0x0012A1D4 File Offset: 0x001283D4
		// (set) Token: 0x06004719 RID: 18201 RVA: 0x0012A1DC File Offset: 0x001283DC
		internal string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
			}
		}

		// Token: 0x17001D95 RID: 7573
		// (get) Token: 0x0600471A RID: 18202 RVA: 0x0012A1E5 File Offset: 0x001283E5
		// (set) Token: 0x0600471B RID: 18203 RVA: 0x0012A1ED File Offset: 0x001283ED
		internal ExpressionInfo Value
		{
			get
			{
				return this.m_value;
			}
			set
			{
				this.m_value = value;
			}
		}

		// Token: 0x17001D96 RID: 7574
		// (get) Token: 0x0600471C RID: 18204 RVA: 0x0012A1F6 File Offset: 0x001283F6
		// (set) Token: 0x0600471D RID: 18205 RVA: 0x0012A1FE File Offset: 0x001283FE
		internal DataType ConstantDataType
		{
			get
			{
				return this.m_constantDataType;
			}
			set
			{
				this.m_constantDataType = value;
			}
		}

		// Token: 0x17001D97 RID: 7575
		// (get) Token: 0x0600471E RID: 18206 RVA: 0x0012A207 File Offset: 0x00128407
		// (set) Token: 0x0600471F RID: 18207 RVA: 0x0012A20F File Offset: 0x0012840F
		internal ExpressionInfo Omit
		{
			get
			{
				return this.m_omit;
			}
			set
			{
				this.m_omit = value;
			}
		}

		// Token: 0x17001D98 RID: 7576
		// (get) Token: 0x06004720 RID: 18208 RVA: 0x0012A218 File Offset: 0x00128418
		// (set) Token: 0x06004721 RID: 18209 RVA: 0x0012A220 File Offset: 0x00128420
		internal bool UseAllValidValues
		{
			get
			{
				return this.m_useAllValidValues;
			}
			set
			{
				this.m_useAllValidValues = value;
			}
		}

		// Token: 0x17001D99 RID: 7577
		// (get) Token: 0x06004722 RID: 18210 RVA: 0x0012A229 File Offset: 0x00128429
		// (set) Token: 0x06004723 RID: 18211 RVA: 0x0012A23B File Offset: 0x0012843B
		internal string UniqueName
		{
			get
			{
				return this.m_uniqueName ?? this.m_name;
			}
			set
			{
				this.m_uniqueName = value;
			}
		}

		// Token: 0x17001D9A RID: 7578
		// (get) Token: 0x06004724 RID: 18212 RVA: 0x0012A244 File Offset: 0x00128444
		// (set) Token: 0x06004725 RID: 18213 RVA: 0x0012A24C File Offset: 0x0012844C
		internal int ExprHostID
		{
			get
			{
				return this.m_exprHostID;
			}
			set
			{
				this.m_exprHostID = value;
			}
		}

		// Token: 0x17001D9B RID: 7579
		// (get) Token: 0x06004726 RID: 18214 RVA: 0x0012A255 File Offset: 0x00128455
		// (set) Token: 0x06004727 RID: 18215 RVA: 0x0012A25D File Offset: 0x0012845D
		internal ParamExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
			set
			{
				this.m_exprHost = value;
			}
		}

		// Token: 0x06004728 RID: 18216 RVA: 0x0012A268 File Offset: 0x00128468
		internal void Initialize(string containerPropertyName, InitializationContext context, bool queryParam)
		{
			string text = ((containerPropertyName == null) ? "" : (containerPropertyName + "."));
			if (this.m_value != null)
			{
				this.m_value.Initialize(text + "Value", context);
				if (!queryParam)
				{
					context.ExprHostBuilder.GenericValue(this.m_value);
				}
				else
				{
					context.ExprHostBuilder.QueryParameterValue(this.m_value);
				}
			}
			if (this.m_omit != null)
			{
				this.m_omit.Initialize(text + "Omit", context);
				context.ExprHostBuilder.ParameterOmit(this.m_omit);
			}
		}

		// Token: 0x06004729 RID: 18217 RVA: 0x0012A304 File Offset: 0x00128504
		internal void SetExprHost(IList<ParamExprHost> paramExprHosts, ObjectModelImpl reportObjectModel)
		{
			if (this.m_exprHostID >= 0)
			{
				Global.Tracer.Assert(paramExprHosts != null && reportObjectModel != null, "(paramExprHosts != null && reportObjectModel != null)");
				this.m_exprHost = paramExprHosts[this.m_exprHostID];
				this.m_exprHost.SetReportObjectModel(reportObjectModel);
			}
		}

		// Token: 0x0600472A RID: 18218 RVA: 0x0012A351 File Offset: 0x00128551
		internal object EvaluateQueryParameterValue(OnDemandProcessingContext odpContext, DataSetExprHost dataSetExprHost)
		{
			return odpContext.ReportRuntime.EvaluateQueryParamValue(this.m_value, (dataSetExprHost != null) ? dataSetExprHost.QueryParametersHost : null, Microsoft.ReportingServices.ReportProcessing.ObjectType.QueryParameter, this.m_name);
		}

		// Token: 0x0600472B RID: 18219 RVA: 0x0012A378 File Offset: 0x00128578
		internal object PublishClone(AutomaticSubtotalContext context)
		{
			ParameterValue parameterValue = (ParameterValue)base.MemberwiseClone();
			if (this.m_name != null)
			{
				parameterValue.m_name = (string)this.m_name.Clone();
			}
			if (this.m_value != null)
			{
				parameterValue.m_value = (ExpressionInfo)this.m_value.PublishClone(context);
			}
			parameterValue.m_constantDataType = this.m_constantDataType;
			if (this.m_omit != null)
			{
				parameterValue.m_omit = (ExpressionInfo)this.m_omit.PublishClone(context);
			}
			return parameterValue;
		}

		// Token: 0x0600472C RID: 18220 RVA: 0x0012A3FC File Offset: 0x001285FC
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ParameterValue, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Name, Token.String),
				new MemberInfo(MemberName.Value, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.DataType, Token.Enum),
				new MemberInfo(MemberName.ExprHostID, Token.Int32),
				new MemberInfo(MemberName.Omit, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.UniqueName, Token.String),
				new MemberInfo(MemberName.UseAllValidValues, Token.Boolean)
			});
		}

		// Token: 0x0600472D RID: 18221 RVA: 0x0012A4A0 File Offset: 0x001286A0
		public virtual void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(ParameterValue.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.ExprHostID)
				{
					switch (memberName)
					{
					case MemberName.UniqueName:
						writer.Write(this.m_uniqueName);
						continue;
					case MemberName.ReportPath:
					case MemberName.ParametersFromCatalog:
						break;
					case MemberName.Name:
						writer.Write(this.m_name);
						continue;
					case MemberName.Value:
						writer.Write(this.m_value);
						continue;
					case MemberName.DataType:
						writer.WriteEnum((int)this.m_constantDataType);
						continue;
					default:
						if (memberName == MemberName.ExprHostID)
						{
							writer.Write(this.m_exprHostID);
							continue;
						}
						break;
					}
				}
				else
				{
					if (memberName == MemberName.Omit)
					{
						writer.Write(this.m_omit);
						continue;
					}
					if (memberName == MemberName.UseAllValidValues)
					{
						writer.Write(this.m_useAllValidValues);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x0600472E RID: 18222 RVA: 0x0012A590 File Offset: 0x00128790
		public virtual void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(ParameterValue.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.ExprHostID)
				{
					switch (memberName)
					{
					case MemberName.UniqueName:
						this.m_uniqueName = reader.ReadString();
						continue;
					case MemberName.ReportPath:
					case MemberName.ParametersFromCatalog:
						break;
					case MemberName.Name:
						this.m_name = reader.ReadString();
						continue;
					case MemberName.Value:
						this.m_value = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.DataType:
						this.m_constantDataType = (DataType)reader.ReadEnum();
						continue;
					default:
						if (memberName == MemberName.ExprHostID)
						{
							this.m_exprHostID = reader.ReadInt32();
							continue;
						}
						break;
					}
				}
				else
				{
					if (memberName == MemberName.Omit)
					{
						this.m_omit = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.UseAllValidValues)
					{
						this.m_useAllValidValues = reader.ReadBoolean();
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x0600472F RID: 18223 RVA: 0x0012A68D File Offset: 0x0012888D
		public virtual void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x06004730 RID: 18224 RVA: 0x0012A69A File Offset: 0x0012889A
		public virtual Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ParameterValue;
		}

		// Token: 0x04001FC8 RID: 8136
		private string m_name;

		// Token: 0x04001FC9 RID: 8137
		private string m_uniqueName;

		// Token: 0x04001FCA RID: 8138
		private ExpressionInfo m_value;

		// Token: 0x04001FCB RID: 8139
		private DataType m_constantDataType = DataType.String;

		// Token: 0x04001FCC RID: 8140
		private int m_exprHostID = -1;

		// Token: 0x04001FCD RID: 8141
		private ExpressionInfo m_omit;

		// Token: 0x04001FCE RID: 8142
		private bool m_useAllValidValues;

		// Token: 0x04001FCF RID: 8143
		[NonSerialized]
		private ParamExprHost m_exprHost;

		// Token: 0x04001FD0 RID: 8144
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = ParameterValue.GetDeclaration();
	}
}
