using System;
using Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel;
using Microsoft.ReportingServices.ReportProcessing.Persistence;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006F2 RID: 1778
	[Serializable]
	internal sealed class Subtotal : IDOwner
	{
		// Token: 0x06006267 RID: 25191 RVA: 0x0018899D File Offset: 0x00186B9D
		internal Subtotal()
		{
		}

		// Token: 0x06006268 RID: 25192 RVA: 0x001889B3 File Offset: 0x00186BB3
		internal Subtotal(int id, int idForReportItems, bool autoDerived)
			: base(id)
		{
			this.m_autoDerived = autoDerived;
			this.m_reportItems = new ReportItemCollection(idForReportItems, false);
		}

		// Token: 0x170022C6 RID: 8902
		// (get) Token: 0x06006269 RID: 25193 RVA: 0x001889DE File Offset: 0x00186BDE
		// (set) Token: 0x0600626A RID: 25194 RVA: 0x001889E6 File Offset: 0x00186BE6
		internal bool AutoDerived
		{
			get
			{
				return this.m_autoDerived;
			}
			set
			{
				this.m_autoDerived = value;
			}
		}

		// Token: 0x170022C7 RID: 8903
		// (get) Token: 0x0600626B RID: 25195 RVA: 0x001889EF File Offset: 0x00186BEF
		// (set) Token: 0x0600626C RID: 25196 RVA: 0x001889F7 File Offset: 0x00186BF7
		internal ReportItemCollection ReportItems
		{
			get
			{
				return this.m_reportItems;
			}
			set
			{
				this.m_reportItems = value;
			}
		}

		// Token: 0x170022C8 RID: 8904
		// (get) Token: 0x0600626D RID: 25197 RVA: 0x00188A00 File Offset: 0x00186C00
		internal Microsoft.ReportingServices.ReportProcessing.ReportItem ReportItem
		{
			get
			{
				if (this.m_reportItems != null && 0 < this.m_reportItems.Count)
				{
					return this.m_reportItems[0];
				}
				return null;
			}
		}

		// Token: 0x170022C9 RID: 8905
		// (get) Token: 0x0600626E RID: 25198 RVA: 0x00188A26 File Offset: 0x00186C26
		// (set) Token: 0x0600626F RID: 25199 RVA: 0x00188A2E File Offset: 0x00186C2E
		internal Style StyleClass
		{
			get
			{
				return this.m_styleClass;
			}
			set
			{
				this.m_styleClass = value;
			}
		}

		// Token: 0x170022CA RID: 8906
		// (get) Token: 0x06006270 RID: 25200 RVA: 0x00188A37 File Offset: 0x00186C37
		// (set) Token: 0x06006271 RID: 25201 RVA: 0x00188A3F File Offset: 0x00186C3F
		internal Subtotal.PositionType Position
		{
			get
			{
				return this.m_position;
			}
			set
			{
				this.m_position = value;
			}
		}

		// Token: 0x170022CB RID: 8907
		// (get) Token: 0x06006272 RID: 25202 RVA: 0x00188A48 File Offset: 0x00186C48
		// (set) Token: 0x06006273 RID: 25203 RVA: 0x00188A50 File Offset: 0x00186C50
		internal bool FirstInstance
		{
			get
			{
				return this.m_firstInstance;
			}
			set
			{
				this.m_firstInstance = value;
			}
		}

		// Token: 0x170022CC RID: 8908
		// (get) Token: 0x06006274 RID: 25204 RVA: 0x00188A59 File Offset: 0x00186C59
		// (set) Token: 0x06006275 RID: 25205 RVA: 0x00188A61 File Offset: 0x00186C61
		internal string RenderingModelID
		{
			get
			{
				return this.m_renderingModelID;
			}
			set
			{
				this.m_renderingModelID = value;
			}
		}

		// Token: 0x170022CD RID: 8909
		// (get) Token: 0x06006276 RID: 25206 RVA: 0x00188A6A File Offset: 0x00186C6A
		// (set) Token: 0x06006277 RID: 25207 RVA: 0x00188A72 File Offset: 0x00186C72
		internal bool Computed
		{
			get
			{
				return this.m_computed;
			}
			set
			{
				this.m_computed = value;
			}
		}

		// Token: 0x170022CE RID: 8910
		// (get) Token: 0x06006278 RID: 25208 RVA: 0x00188A7B File Offset: 0x00186C7B
		// (set) Token: 0x06006279 RID: 25209 RVA: 0x00188A83 File Offset: 0x00186C83
		internal string DataElementName
		{
			get
			{
				return this.m_dataElementName;
			}
			set
			{
				this.m_dataElementName = value;
			}
		}

		// Token: 0x170022CF RID: 8911
		// (get) Token: 0x0600627A RID: 25210 RVA: 0x00188A8C File Offset: 0x00186C8C
		// (set) Token: 0x0600627B RID: 25211 RVA: 0x00188A94 File Offset: 0x00186C94
		internal DataElementOutputTypes DataElementOutput
		{
			get
			{
				return this.m_dataElementOutput;
			}
			set
			{
				this.m_dataElementOutput = value;
			}
		}

		// Token: 0x0600627C RID: 25212 RVA: 0x00188A9D File Offset: 0x00186C9D
		internal void RegisterReportItems(InitializationContext context)
		{
			context.RegisterReportItems(this.m_reportItems);
		}

		// Token: 0x0600627D RID: 25213 RVA: 0x00188AAC File Offset: 0x00186CAC
		internal void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.SubtotalStart();
			this.DataRendererInitialize(context);
			context.RegisterRunningValues(this.m_reportItems.RunningValues);
			if (this.m_styleClass != null)
			{
				this.m_styleClass.Initialize(context);
			}
			this.m_reportItems.Initialize(context, false);
			context.UnRegisterRunningValues(this.m_reportItems.RunningValues);
			context.ExprHostBuilder.SubtotalEnd();
		}

		// Token: 0x0600627E RID: 25214 RVA: 0x00188B1E File Offset: 0x00186D1E
		internal void UnregisterReportItems(InitializationContext context)
		{
			context.UnRegisterReportItems(this.m_reportItems);
		}

		// Token: 0x0600627F RID: 25215 RVA: 0x00188B2D File Offset: 0x00186D2D
		internal void RegisterReceiver(InitializationContext context)
		{
			context.RegisterReportItems(this.m_reportItems);
			this.m_reportItems.RegisterReceiver(context);
			context.UnRegisterReportItems(this.m_reportItems);
		}

		// Token: 0x06006280 RID: 25216 RVA: 0x00188B55 File Offset: 0x00186D55
		private void DataRendererInitialize(InitializationContext context)
		{
			CLSNameValidator.ValidateDataElementName(ref this.m_dataElementName, "Total", context.ObjectType, context.ObjectName, "DataElementName", context.ErrorContext);
		}

		// Token: 0x06006281 RID: 25217 RVA: 0x00188B82 File Offset: 0x00186D82
		internal void SetExprHost(StyleExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null && this.m_styleClass != null);
			exprHost.SetReportObjectModel(reportObjectModel);
			this.m_styleClass.SetStyleExprHost(exprHost);
		}

		// Token: 0x06006282 RID: 25218 RVA: 0x00188BB4 File Offset: 0x00186DB4
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.IDOwner, new MemberInfoList
			{
				new MemberInfo(MemberName.AutoDerived, Token.Boolean),
				new MemberInfo(MemberName.ReportItems, ObjectType.ReportItemCollection),
				new MemberInfo(MemberName.StyleClass, ObjectType.Style),
				new MemberInfo(MemberName.Position, Token.Enum),
				new MemberInfo(MemberName.DataElementName, Token.String),
				new MemberInfo(MemberName.DataElementOutput, Token.Enum)
			});
		}

		// Token: 0x040031AC RID: 12716
		private bool m_autoDerived;

		// Token: 0x040031AD RID: 12717
		private ReportItemCollection m_reportItems;

		// Token: 0x040031AE RID: 12718
		private Style m_styleClass;

		// Token: 0x040031AF RID: 12719
		private Subtotal.PositionType m_position;

		// Token: 0x040031B0 RID: 12720
		private string m_dataElementName;

		// Token: 0x040031B1 RID: 12721
		private DataElementOutputTypes m_dataElementOutput = DataElementOutputTypes.NoOutput;

		// Token: 0x040031B2 RID: 12722
		[NonSerialized]
		private bool m_firstInstance = true;

		// Token: 0x040031B3 RID: 12723
		[NonSerialized]
		private string m_renderingModelID;

		// Token: 0x040031B4 RID: 12724
		[NonSerialized]
		private bool m_computed;

		// Token: 0x02000CC8 RID: 3272
		internal enum PositionType
		{
			// Token: 0x04004E9D RID: 20125
			After,
			// Token: 0x04004E9E RID: 20126
			Before
		}
	}
}
