using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020002DD RID: 733
	public sealed class ParameterCollection : ReportElementCollectionBase<Parameter>
	{
		// Token: 0x06001B59 RID: 7001 RVA: 0x0006D9F4 File Offset: 0x0006BBF4
		internal ParameterCollection(ActionDrillthrough actionDef, List<Microsoft.ReportingServices.ReportIntermediateFormat.ParameterValue> parameters)
		{
			this.m_isOldSnapshot = false;
			this.m_actionDef = actionDef;
			if (parameters == null)
			{
				this.m_list = new List<Parameter>();
				return;
			}
			int count = parameters.Count;
			this.m_list = new List<Parameter>(count);
			for (int i = 0; i < count; i++)
			{
				this.m_list.Add(new Parameter(actionDef, parameters[i]));
			}
		}

		// Token: 0x06001B5A RID: 7002 RVA: 0x0006DA5C File Offset: 0x0006BC5C
		internal ParameterCollection(ActionDrillthrough actionDef, NameValueCollection drillthroughParameters, DrillthroughParameters parametersNameObjectCollection, ParameterValueList parameters, ActionItemInstance actionInstance)
		{
			this.m_isOldSnapshot = true;
			this.m_actionDef = actionDef;
			this.m_drillthroughParameters = drillthroughParameters;
			this.m_parametersNameObjectCollection = parametersNameObjectCollection;
			if (parameters == null)
			{
				this.m_list = new List<Parameter>();
				return;
			}
			int count = parameters.Count;
			this.m_list = new List<Parameter>(count);
			for (int i = 0; i < count; i++)
			{
				this.m_list.Add(new Parameter(actionDef, parameters[i], actionInstance, i));
			}
		}

		// Token: 0x17000F4E RID: 3918
		// (get) Token: 0x06001B5B RID: 7003 RVA: 0x0006DAD8 File Offset: 0x0006BCD8
		public NameValueCollection ToNameValueCollection
		{
			get
			{
				if (!this.m_isOldSnapshot && this.m_drillthroughParameters == null && this.m_list != null)
				{
					bool[] array;
					this.m_drillthroughParameters = this.ConvertToNameValueCollection(false, out array);
					if (0 < this.m_drillthroughParameters.Count)
					{
						this.m_drillthroughParameters.Add("rs:ParameterLanguage", "");
					}
					bool flag = false;
					ReportProcessing.StoreServerParameters storeServerParameters = this.m_actionDef.Owner.RenderingContext.OdpContext.StoreServerParameters;
					if (storeServerParameters != null)
					{
						string reportName = this.m_actionDef.Instance.ReportName;
						ICatalogItemContext subreportContext = this.m_actionDef.PathResolutionContext.GetSubreportContext(reportName);
						this.m_drillthroughParameters = storeServerParameters(subreportContext, this.m_drillthroughParameters, array, out flag);
					}
				}
				return this.m_drillthroughParameters;
			}
		}

		// Token: 0x06001B5C RID: 7004 RVA: 0x0006DB9C File Offset: 0x0006BD9C
		internal NameValueCollection ToNameValueCollectionForDrillthroughEvent()
		{
			bool[] array;
			return this.ConvertToNameValueCollection(true, out array);
		}

		// Token: 0x06001B5D RID: 7005 RVA: 0x0006DBB4 File Offset: 0x0006BDB4
		private NameValueCollection ConvertToNameValueCollection(bool forDrillthroughEvent, out bool[] sharedParams)
		{
			int count = this.m_list.Count;
			NameValueCollection nameValueCollection = new NameValueCollection(count);
			sharedParams = new bool[count];
			for (int i = 0; i < count; i++)
			{
				Parameter parameter = this.m_list[i];
				ParameterInstance instance = parameter.Instance;
				Microsoft.ReportingServices.ReportIntermediateFormat.ParameterValue parameterDef = parameter.ParameterDef;
				if (parameterDef.Value != null && parameterDef.Value.Type == Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.Token)
				{
					sharedParams[i] = true;
				}
				else
				{
					sharedParams[i] = false;
				}
				if (!instance.Omit)
				{
					object value = instance.Value;
					if (value == null)
					{
						nameValueCollection.Add(parameter.Name, null);
					}
					else
					{
						object[] array = value as object[];
						if (array != null)
						{
							for (int j = 0; j < array.Length; j++)
							{
								nameValueCollection.Add(parameter.Name, this.ConvertValueToString(array[j], forDrillthroughEvent));
							}
						}
						else
						{
							nameValueCollection.Add(parameter.Name, this.ConvertValueToString(value, forDrillthroughEvent));
						}
					}
				}
			}
			return nameValueCollection;
		}

		// Token: 0x06001B5E RID: 7006 RVA: 0x0006DCA7 File Offset: 0x0006BEA7
		private string ConvertValueToString(object value, bool forDrillthroughEvent)
		{
			if (forDrillthroughEvent)
			{
				return Formatter.FormatWithClientCulture(value);
			}
			return Formatter.FormatWithInvariantCulture(value);
		}

		// Token: 0x17000F4F RID: 3919
		public override Parameter this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { index, 0, this.Count });
				}
				return this.m_list[index];
			}
		}

		// Token: 0x17000F50 RID: 3920
		// (get) Token: 0x06001B60 RID: 7008 RVA: 0x0006DD13 File Offset: 0x0006BF13
		public override int Count
		{
			get
			{
				return this.m_list.Count;
			}
		}

		// Token: 0x17000F51 RID: 3921
		// (get) Token: 0x06001B61 RID: 7009 RVA: 0x0006DD20 File Offset: 0x0006BF20
		internal DrillthroughParameters ParametersNameObjectCollection
		{
			get
			{
				if (!this.m_isOldSnapshot && this.m_parametersNameObjectCollection == null && this.m_list != null)
				{
					int count = this.m_list.Count;
					this.m_parametersNameObjectCollection = new DrillthroughParameters(count);
					for (int i = 0; i < count; i++)
					{
						Parameter parameter = this.m_list[i];
						ParameterInstance instance = parameter.Instance;
						if (!instance.Omit)
						{
							this.m_parametersNameObjectCollection.Add(parameter.Name, instance.Value);
						}
					}
				}
				return this.m_parametersNameObjectCollection;
			}
		}

		// Token: 0x06001B62 RID: 7010 RVA: 0x0006DDA4 File Offset: 0x0006BFA4
		internal Parameter Add(ActionDrillthrough owner, Microsoft.ReportingServices.ReportIntermediateFormat.ParameterValue paramDef)
		{
			Parameter parameter = new Parameter(owner, paramDef);
			this.m_list.Add(parameter);
			return parameter;
		}

		// Token: 0x06001B63 RID: 7011 RVA: 0x0006DDC8 File Offset: 0x0006BFC8
		internal void Update(NameValueCollection drillthroughParameters, DrillthroughParameters nameObjectCollection, ActionItemInstance actionInstance)
		{
			int count = this.m_list.Count;
			for (int i = 0; i < count; i++)
			{
				this.m_list[i].Update(actionInstance, i);
			}
			this.m_parametersNameObjectCollection = nameObjectCollection;
			this.m_drillthroughParameters = drillthroughParameters;
			this.m_parametersNameObjectCollection = nameObjectCollection;
		}

		// Token: 0x06001B64 RID: 7012 RVA: 0x0006DE18 File Offset: 0x0006C018
		internal void SetNewContext()
		{
			if (!this.m_isOldSnapshot)
			{
				this.m_drillthroughParameters = null;
				this.m_parametersNameObjectCollection = null;
			}
			if (this.m_list != null)
			{
				for (int i = 0; i < this.m_list.Count; i++)
				{
					this.m_list[i].SetNewContext();
				}
			}
		}

		// Token: 0x06001B65 RID: 7013 RVA: 0x0006DE6C File Offset: 0x0006C06C
		internal void ConstructParameterDefinitions()
		{
			foreach (Parameter parameter in this.m_list)
			{
				parameter.ConstructParameterDefinition();
			}
		}

		// Token: 0x04000D88 RID: 3464
		private bool m_isOldSnapshot;

		// Token: 0x04000D89 RID: 3465
		private List<Parameter> m_list;

		// Token: 0x04000D8A RID: 3466
		private NameValueCollection m_drillthroughParameters;

		// Token: 0x04000D8B RID: 3467
		private DrillthroughParameters m_parametersNameObjectCollection;

		// Token: 0x04000D8C RID: 3468
		private ActionDrillthrough m_actionDef;
	}
}
