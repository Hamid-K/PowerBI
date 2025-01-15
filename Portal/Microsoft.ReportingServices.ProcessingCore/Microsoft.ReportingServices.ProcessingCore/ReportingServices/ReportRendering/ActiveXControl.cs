using System;
using System.Collections.Specialized;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x0200002F RID: 47
	internal sealed class ActiveXControl : ReportItem
	{
		// Token: 0x060004A0 RID: 1184 RVA: 0x0000E18B File Offset: 0x0000C38B
		internal ActiveXControl(string uniqueName, int intUniqueName, ActiveXControl reportItemDef, ActiveXControlInstance reportItemInstance, RenderingContext renderingContext)
			: base(uniqueName, intUniqueName, reportItemDef, reportItemInstance, renderingContext)
		{
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x060004A1 RID: 1185 RVA: 0x0000E19A File Offset: 0x0000C39A
		public string ClassID
		{
			get
			{
				return ((ActiveXControl)base.ReportItemDef).ClassID;
			}
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x060004A2 RID: 1186 RVA: 0x0000E1AC File Offset: 0x0000C3AC
		public ReportUrl CodeBase
		{
			get
			{
				string codeBase = ((ActiveXControl)base.ReportItemDef).CodeBase;
				if (codeBase == null)
				{
					return null;
				}
				ReportUrl reportUrl = this.m_codeBase;
				if (this.m_codeBase == null)
				{
					reportUrl = new ReportUrl(base.RenderingContext, codeBase);
					if (base.RenderingContext.CacheState)
					{
						this.m_codeBase = reportUrl;
					}
				}
				return reportUrl;
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x060004A3 RID: 1187 RVA: 0x0000E200 File Offset: 0x0000C400
		public ActiveXControl.ParameterCollection Parameters
		{
			get
			{
				ActiveXControl.ParameterCollection parameterCollection = this.m_parameters;
				if (this.m_parameters == null)
				{
					ActiveXControl activeXControl = (ActiveXControl)base.ReportItemDef;
					if (activeXControl.Parameters != null && activeXControl.Parameters.Count > 0)
					{
						parameterCollection = new ActiveXControl.ParameterCollection();
						for (int i = 0; i < activeXControl.Parameters.Count; i++)
						{
							ParameterValue parameterValue = activeXControl.Parameters[i];
							object obj;
							if (parameterValue.Value.Type == ExpressionInfo.Types.Constant)
							{
								obj = parameterValue.Value.Value;
							}
							else if (base.ReportItemInstance == null)
							{
								obj = null;
							}
							else
							{
								obj = ((ActiveXControlInstanceInfo)base.InstanceInfo).ParameterValues[i];
							}
							parameterCollection.Add(new ActiveXControl.Parameter(parameterValue.Name, obj));
						}
						if (base.RenderingContext.CacheState)
						{
							this.m_parameters = parameterCollection;
						}
					}
				}
				return parameterCollection;
			}
		}

		// Token: 0x040000E7 RID: 231
		private ActiveXControl.ParameterCollection m_parameters;

		// Token: 0x040000E8 RID: 232
		private ReportUrl m_codeBase;

		// Token: 0x0200090D RID: 2317
		public sealed class Parameter
		{
			// Token: 0x06007F0F RID: 32527 RVA: 0x0020C4D4 File Offset: 0x0020A6D4
			internal Parameter(string name, object value)
			{
				this.m_name = name;
				this.m_value = value;
			}

			// Token: 0x17002954 RID: 10580
			// (get) Token: 0x06007F10 RID: 32528 RVA: 0x0020C4EA File Offset: 0x0020A6EA
			public string Name
			{
				get
				{
					return this.m_name;
				}
			}

			// Token: 0x17002955 RID: 10581
			// (get) Token: 0x06007F11 RID: 32529 RVA: 0x0020C4F2 File Offset: 0x0020A6F2
			public object Value
			{
				get
				{
					return this.m_value;
				}
			}

			// Token: 0x04003EEA RID: 16106
			private string m_name;

			// Token: 0x04003EEB RID: 16107
			private object m_value;
		}

		// Token: 0x0200090E RID: 2318
		public sealed class ParameterCollection : NameObjectCollectionBase
		{
			// Token: 0x06007F12 RID: 32530 RVA: 0x0020C4FA File Offset: 0x0020A6FA
			internal ParameterCollection()
			{
			}

			// Token: 0x17002956 RID: 10582
			public ActiveXControl.Parameter this[int index]
			{
				get
				{
					return (ActiveXControl.Parameter)base.BaseGet(index);
				}
			}

			// Token: 0x17002957 RID: 10583
			public ActiveXControl.Parameter this[string name]
			{
				get
				{
					return (ActiveXControl.Parameter)base.BaseGet(name);
				}
			}

			// Token: 0x06007F15 RID: 32533 RVA: 0x0020C51E File Offset: 0x0020A71E
			internal void Add(ActiveXControl.Parameter parameter)
			{
				base.BaseAdd(parameter.Name, parameter);
			}
		}
	}
}
