using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004AD RID: 1197
	[Serializable]
	internal sealed class DataValue : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x17001957 RID: 6487
		// (get) Token: 0x06003AD6 RID: 15062 RVA: 0x000FEC09 File Offset: 0x000FCE09
		// (set) Token: 0x06003AD7 RID: 15063 RVA: 0x000FEC11 File Offset: 0x000FCE11
		internal ExpressionInfo Name
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

		// Token: 0x17001958 RID: 6488
		// (get) Token: 0x06003AD8 RID: 15064 RVA: 0x000FEC1A File Offset: 0x000FCE1A
		// (set) Token: 0x06003AD9 RID: 15065 RVA: 0x000FEC22 File Offset: 0x000FCE22
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

		// Token: 0x17001959 RID: 6489
		// (get) Token: 0x06003ADA RID: 15066 RVA: 0x000FEC2B File Offset: 0x000FCE2B
		// (set) Token: 0x06003ADB RID: 15067 RVA: 0x000FEC33 File Offset: 0x000FCE33
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

		// Token: 0x1700195A RID: 6490
		// (get) Token: 0x06003ADC RID: 15068 RVA: 0x000FEC3C File Offset: 0x000FCE3C
		internal DataValueExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x06003ADD RID: 15069 RVA: 0x000FEC44 File Offset: 0x000FCE44
		public object PublishClone(AutomaticSubtotalContext context)
		{
			Microsoft.ReportingServices.ReportIntermediateFormat.DataValue dataValue = (Microsoft.ReportingServices.ReportIntermediateFormat.DataValue)base.MemberwiseClone();
			if (this.m_name == null)
			{
				dataValue.m_name = (ExpressionInfo)this.m_name.PublishClone(context);
			}
			if (this.m_value == null)
			{
				dataValue.m_value = (ExpressionInfo)this.m_value.PublishClone(context);
			}
			return dataValue;
		}

		// Token: 0x06003ADE RID: 15070 RVA: 0x000FEC9C File Offset: 0x000FCE9C
		internal void Initialize(string propertyName, bool isCustomProperty, DynamicImageOrCustomUniqueNameValidator validator, InitializationContext context)
		{
			context.ExprHostBuilder.DataValueStart();
			if (this.m_name != null)
			{
				this.m_name.Initialize(propertyName + ".Name", context);
				if (isCustomProperty && ExpressionInfo.Types.Constant == this.m_name.Type)
				{
					validator.Validate(Severity.Error, propertyName + ".Name", context.ObjectType, context.ObjectName, this.m_name.StringValue, context.ErrorContext);
				}
				context.ExprHostBuilder.DataValueName(this.m_name);
			}
			if (this.m_value != null)
			{
				this.m_value.Initialize(propertyName + ".Value", context);
				context.ExprHostBuilder.DataValueValue(this.m_value);
			}
			this.m_exprHostID = context.ExprHostBuilder.DataValueEnd(isCustomProperty);
		}

		// Token: 0x06003ADF RID: 15071 RVA: 0x000FED70 File Offset: 0x000FCF70
		internal void SetExprHost(IList<DataValueExprHost> dataValueHosts, ObjectModelImpl reportObjectModel)
		{
			if (this.m_exprHostID >= 0)
			{
				Global.Tracer.Assert(dataValueHosts != null && dataValueHosts.Count > this.m_exprHostID && reportObjectModel != null);
				this.m_exprHost = dataValueHosts[this.m_exprHostID];
				this.m_exprHost.SetReportObjectModel(reportObjectModel);
			}
		}

		// Token: 0x06003AE0 RID: 15072 RVA: 0x000FEDC8 File Offset: 0x000FCFC8
		internal void EvaluateNameAndValue(ReportElement reportElementOwner, IReportScopeInstance romInstance, IInstancePath instancePath, OnDemandProcessingContext context, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName, out string name, out object value, out TypeCode valueTypeCode)
		{
			context.SetupContext(instancePath, romInstance);
			name = null;
			value = null;
			valueTypeCode = TypeCode.Empty;
			if (this.m_name != null)
			{
				if (!this.m_name.IsExpression)
				{
					name = this.m_name.StringValue;
				}
				else if (reportElementOwner == null || (reportElementOwner != null && reportElementOwner.CriOwner == null))
				{
					name = context.ReportRuntime.EvaluateDataValueNameExpression(this, objectType, objectName, "Name");
				}
			}
			if (this.m_value != null)
			{
				if (!this.m_value.IsExpression)
				{
					value = this.m_value.Value;
					return;
				}
				if (reportElementOwner == null || (reportElementOwner != null && reportElementOwner.CriOwner == null))
				{
					value = context.ReportRuntime.EvaluateDataValueValueExpression(this, objectType, objectName, "Value", out valueTypeCode);
				}
			}
		}

		// Token: 0x06003AE1 RID: 15073 RVA: 0x000FEE84 File Offset: 0x000FD084
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataValue, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Name, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Value, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.ExprHostID, Token.Int32)
			});
		}

		// Token: 0x06003AE2 RID: 15074 RVA: 0x000FEEDC File Offset: 0x000FD0DC
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(Microsoft.ReportingServices.ReportIntermediateFormat.DataValue.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.Name)
				{
					if (memberName != MemberName.Value)
					{
						if (memberName != MemberName.ExprHostID)
						{
							Global.Tracer.Assert(false);
						}
						else
						{
							writer.Write(this.m_exprHostID);
						}
					}
					else
					{
						writer.Write(this.m_value);
					}
				}
				else
				{
					writer.Write(this.m_name);
				}
			}
		}

		// Token: 0x06003AE3 RID: 15075 RVA: 0x000FEF5C File Offset: 0x000FD15C
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(Microsoft.ReportingServices.ReportIntermediateFormat.DataValue.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.Name)
				{
					if (memberName != MemberName.Value)
					{
						if (memberName != MemberName.ExprHostID)
						{
							Global.Tracer.Assert(false);
						}
						else
						{
							this.m_exprHostID = reader.ReadInt32();
						}
					}
					else
					{
						this.m_value = (ExpressionInfo)reader.ReadRIFObject();
					}
				}
				else
				{
					this.m_name = (ExpressionInfo)reader.ReadRIFObject();
				}
			}
		}

		// Token: 0x06003AE4 RID: 15076 RVA: 0x000FEFE3 File Offset: 0x000FD1E3
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x06003AE5 RID: 15077 RVA: 0x000FEFF0 File Offset: 0x000FD1F0
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataValue;
		}

		// Token: 0x04001BF4 RID: 7156
		private ExpressionInfo m_name;

		// Token: 0x04001BF5 RID: 7157
		private ExpressionInfo m_value;

		// Token: 0x04001BF6 RID: 7158
		private int m_exprHostID = -1;

		// Token: 0x04001BF7 RID: 7159
		[NonSerialized]
		private DataValueExprHost m_exprHost;

		// Token: 0x04001BF8 RID: 7160
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = Microsoft.ReportingServices.ReportIntermediateFormat.DataValue.GetDeclaration();
	}
}
