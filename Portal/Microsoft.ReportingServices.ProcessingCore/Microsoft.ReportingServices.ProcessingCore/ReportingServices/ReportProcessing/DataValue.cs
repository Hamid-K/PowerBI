using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel;
using Microsoft.ReportingServices.ReportProcessing.Persistence;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000765 RID: 1893
	[Serializable]
	internal sealed class DataValue
	{
		// Token: 0x17002517 RID: 9495
		// (get) Token: 0x0600690C RID: 26892 RVA: 0x00199399 File Offset: 0x00197599
		// (set) Token: 0x0600690D RID: 26893 RVA: 0x001993A1 File Offset: 0x001975A1
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

		// Token: 0x17002518 RID: 9496
		// (get) Token: 0x0600690E RID: 26894 RVA: 0x001993AA File Offset: 0x001975AA
		// (set) Token: 0x0600690F RID: 26895 RVA: 0x001993B2 File Offset: 0x001975B2
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

		// Token: 0x17002519 RID: 9497
		// (get) Token: 0x06006910 RID: 26896 RVA: 0x001993BB File Offset: 0x001975BB
		// (set) Token: 0x06006911 RID: 26897 RVA: 0x001993C3 File Offset: 0x001975C3
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

		// Token: 0x1700251A RID: 9498
		// (get) Token: 0x06006912 RID: 26898 RVA: 0x001993CC File Offset: 0x001975CC
		internal DataValueExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x06006913 RID: 26899 RVA: 0x001993D4 File Offset: 0x001975D4
		internal DataValue DeepClone(InitializationContext context)
		{
			DataValue dataValue = new DataValue();
			Global.Tracer.Assert(-1 == this.m_exprHostID);
			dataValue.m_name = ((this.m_name == null) ? null : this.m_name.DeepClone(context));
			dataValue.m_value = ((this.m_value == null) ? null : this.m_value.DeepClone(context));
			return dataValue;
		}

		// Token: 0x06006914 RID: 26900 RVA: 0x00199434 File Offset: 0x00197634
		internal void Initialize(string propertyName, bool isCustomProperty, CustomPropertyUniqueNameValidator validator, InitializationContext context)
		{
			context.ExprHostBuilder.DataValueStart();
			if (this.m_name != null)
			{
				this.m_name.Initialize(propertyName + ".Name", context);
				if (isCustomProperty && ExpressionInfo.Types.Constant == this.m_name.Type)
				{
					validator.Validate(Severity.Error, context.ObjectType, context.ObjectName, this.m_name.Value, context.ErrorContext);
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

		// Token: 0x06006915 RID: 26901 RVA: 0x00199500 File Offset: 0x00197700
		internal void SetExprHost(IList<DataValueExprHost> dataValueHosts, ObjectModelImpl reportObjectModel)
		{
			if (this.m_exprHostID >= 0)
			{
				Global.Tracer.Assert(dataValueHosts != null && dataValueHosts.Count > this.m_exprHostID && reportObjectModel != null);
				this.m_exprHost = dataValueHosts[this.m_exprHostID];
				this.m_exprHost.SetReportObjectModel(reportObjectModel);
			}
		}

		// Token: 0x06006916 RID: 26902 RVA: 0x00199558 File Offset: 0x00197758
		internal static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.None, new MemberInfoList
			{
				new MemberInfo(MemberName.Name, ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Value, ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.ExprHostID, Token.Int32)
			});
		}

		// Token: 0x040033C5 RID: 13253
		private ExpressionInfo m_name;

		// Token: 0x040033C6 RID: 13254
		private ExpressionInfo m_value;

		// Token: 0x040033C7 RID: 13255
		private int m_exprHostID = -1;

		// Token: 0x040033C8 RID: 13256
		[NonSerialized]
		private DataValueExprHost m_exprHost;
	}
}
