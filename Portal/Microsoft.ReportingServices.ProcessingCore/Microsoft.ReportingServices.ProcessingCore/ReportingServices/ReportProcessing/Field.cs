using System;
using Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel;
using Microsoft.ReportingServices.ReportProcessing.Persistence;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006DA RID: 1754
	[Serializable]
	internal sealed class Field
	{
		// Token: 0x1700216D RID: 8557
		// (get) Token: 0x06005F1E RID: 24350 RVA: 0x0018192D File Offset: 0x0017FB2D
		// (set) Token: 0x06005F1F RID: 24351 RVA: 0x00181935 File Offset: 0x0017FB35
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

		// Token: 0x1700216E RID: 8558
		// (get) Token: 0x06005F20 RID: 24352 RVA: 0x0018193E File Offset: 0x0017FB3E
		// (set) Token: 0x06005F21 RID: 24353 RVA: 0x00181946 File Offset: 0x0017FB46
		internal string DataField
		{
			get
			{
				return this.m_dataField;
			}
			set
			{
				this.m_dataField = value;
			}
		}

		// Token: 0x1700216F RID: 8559
		// (get) Token: 0x06005F22 RID: 24354 RVA: 0x0018194F File Offset: 0x0017FB4F
		// (set) Token: 0x06005F23 RID: 24355 RVA: 0x00181957 File Offset: 0x0017FB57
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

		// Token: 0x17002170 RID: 8560
		// (get) Token: 0x06005F24 RID: 24356 RVA: 0x00181960 File Offset: 0x0017FB60
		internal bool IsCalculatedField
		{
			get
			{
				return this.m_dataField == null;
			}
		}

		// Token: 0x17002171 RID: 8561
		// (get) Token: 0x06005F25 RID: 24357 RVA: 0x0018196B File Offset: 0x0017FB6B
		// (set) Token: 0x06005F26 RID: 24358 RVA: 0x00181973 File Offset: 0x0017FB73
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

		// Token: 0x17002172 RID: 8562
		// (get) Token: 0x06005F27 RID: 24359 RVA: 0x0018197C File Offset: 0x0017FB7C
		// (set) Token: 0x06005F28 RID: 24360 RVA: 0x00181984 File Offset: 0x0017FB84
		internal bool DynamicPropertyReferences
		{
			get
			{
				return this.m_dynamicPropertyReferences;
			}
			set
			{
				this.m_dynamicPropertyReferences = value;
			}
		}

		// Token: 0x17002173 RID: 8563
		// (get) Token: 0x06005F29 RID: 24361 RVA: 0x0018198D File Offset: 0x0017FB8D
		// (set) Token: 0x06005F2A RID: 24362 RVA: 0x00181995 File Offset: 0x0017FB95
		internal FieldPropertyHashtable ReferencedProperties
		{
			get
			{
				return this.m_referencedProperties;
			}
			set
			{
				this.m_referencedProperties = value;
			}
		}

		// Token: 0x17002174 RID: 8564
		// (get) Token: 0x06005F2B RID: 24363 RVA: 0x0018199E File Offset: 0x0017FB9E
		internal CalcFieldExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x06005F2C RID: 24364 RVA: 0x001819A8 File Offset: 0x0017FBA8
		internal void Initialize(InitializationContext context)
		{
			if (this.Value != null)
			{
				context.ExprHostBuilder.CalcFieldStart(this.m_name);
				this.m_value.Initialize("Field", context);
				context.ExprHostBuilder.GenericValue(this.m_value);
				this.m_exprHostID = context.ExprHostBuilder.CalcFieldEnd();
			}
		}

		// Token: 0x06005F2D RID: 24365 RVA: 0x00181A04 File Offset: 0x0017FC04
		internal void SetExprHost(DataSetExprHost dataSetExprHost, ObjectModelImpl reportObjectModel)
		{
			if (this.ExprHostID >= 0)
			{
				Global.Tracer.Assert(dataSetExprHost != null && reportObjectModel != null);
				this.m_exprHost = dataSetExprHost.FieldHostsRemotable[this.ExprHostID];
				this.m_exprHost.SetReportObjectModel(reportObjectModel);
			}
		}

		// Token: 0x06005F2E RID: 24366 RVA: 0x00181A54 File Offset: 0x0017FC54
		internal static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.None, new MemberInfoList
			{
				new MemberInfo(MemberName.Name, Token.String),
				new MemberInfo(MemberName.DataField, Token.String),
				new MemberInfo(MemberName.Value, ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.ExprHostID, Token.Int32),
				new MemberInfo(MemberName.DynamicPropertyReferences, Token.Boolean),
				new MemberInfo(MemberName.ReferencedProperties, ObjectType.FieldPropertyHashtable)
			});
		}

		// Token: 0x04003076 RID: 12406
		private string m_name;

		// Token: 0x04003077 RID: 12407
		private string m_dataField;

		// Token: 0x04003078 RID: 12408
		private ExpressionInfo m_value;

		// Token: 0x04003079 RID: 12409
		private int m_exprHostID = -1;

		// Token: 0x0400307A RID: 12410
		private bool m_dynamicPropertyReferences;

		// Token: 0x0400307B RID: 12411
		private FieldPropertyHashtable m_referencedProperties;

		// Token: 0x0400307C RID: 12412
		[NonSerialized]
		private CalcFieldExprHost m_exprHost;
	}
}
