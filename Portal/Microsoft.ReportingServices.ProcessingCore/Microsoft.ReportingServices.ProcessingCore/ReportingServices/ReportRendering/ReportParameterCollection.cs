using System;
using System.Collections.Specialized;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x0200005D RID: 93
	public sealed class ReportParameterCollection : NameObjectCollectionBase
	{
		// Token: 0x0600068F RID: 1679 RVA: 0x00019351 File Offset: 0x00017551
		internal ReportParameterCollection(ParameterInfoCollection parameters)
		{
			this.Init(parameters, true);
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x00019361 File Offset: 0x00017561
		internal ReportParameterCollection(ParameterInfoCollection parameters, bool isValid)
		{
			this.Init(parameters, isValid);
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x00019374 File Offset: 0x00017574
		private void Init(ParameterInfoCollection parameters, bool isValid)
		{
			this.m_isValid = isValid;
			int count = parameters.Count;
			for (int i = 0; i < count; i++)
			{
				ParameterInfo parameterInfo = parameters[i];
				if (parameterInfo.PromptUser)
				{
					base.BaseAdd(parameterInfo.Name, new ReportParameter(parameterInfo));
				}
			}
		}

		// Token: 0x170004EA RID: 1258
		public ReportParameter this[string name]
		{
			get
			{
				return (ReportParameter)base.BaseGet(name);
			}
		}

		// Token: 0x170004EB RID: 1259
		public ReportParameter this[int index]
		{
			get
			{
				return (ReportParameter)base.BaseGet(index);
			}
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x06000694 RID: 1684 RVA: 0x000193DC File Offset: 0x000175DC
		public NameValueCollection AsNameValueCollection
		{
			get
			{
				if (this.m_asNameValueCollection == null)
				{
					int count = this.Count;
					this.m_asNameValueCollection = new NameValueCollection(count, StringComparer.Ordinal);
					for (int i = 0; i < count; i++)
					{
						ReportParameter reportParameter = this[i];
						this.m_asNameValueCollection.Add(reportParameter.Name, reportParameter.StringValues);
					}
				}
				return this.m_asNameValueCollection;
			}
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x06000695 RID: 1685 RVA: 0x0001943A File Offset: 0x0001763A
		public bool IsValid
		{
			get
			{
				return this.m_isValid;
			}
		}

		// Token: 0x040001B5 RID: 437
		private NameValueCollection m_asNameValueCollection;

		// Token: 0x040001B6 RID: 438
		private bool m_isValid;
	}
}
