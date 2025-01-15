using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200005A RID: 90
	internal sealed class AdomdDataAdapterDesigner : IDesigner, IDisposable, IDesignerFilter
	{
		// Token: 0x060005E1 RID: 1505 RVA: 0x00021AC1 File Offset: 0x0001FCC1
		internal AdomdDataAdapterDesigner()
		{
			this.theComponent = null;
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060005E2 RID: 1506 RVA: 0x00021AD0 File Offset: 0x0001FCD0
		IComponent IDesigner.Component
		{
			get
			{
				return this.theComponent;
			}
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x00021AD8 File Offset: 0x0001FCD8
		void IDesigner.DoDefaultAction()
		{
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x00021ADA File Offset: 0x0001FCDA
		void IDesigner.Initialize(IComponent component)
		{
			this.theComponent = component;
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060005E5 RID: 1509 RVA: 0x00021AE3 File Offset: 0x0001FCE3
		DesignerVerbCollection IDesigner.Verbs
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x00021AE6 File Offset: 0x0001FCE6
		void IDesignerFilter.PostFilterAttributes(IDictionary attributes)
		{
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x00021AE8 File Offset: 0x0001FCE8
		void IDesignerFilter.PostFilterEvents(IDictionary events)
		{
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x00021AEC File Offset: 0x0001FCEC
		void IDesignerFilter.PostFilterProperties(IDictionary properties)
		{
			foreach (string text in AdomdDataAdapterDesigner.propertiesToHide)
			{
				if (properties.Contains(text))
				{
					properties.Remove(text);
				}
			}
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x00021B21 File Offset: 0x0001FD21
		void IDesignerFilter.PreFilterAttributes(IDictionary attributes)
		{
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x00021B23 File Offset: 0x0001FD23
		void IDesignerFilter.PreFilterEvents(IDictionary events)
		{
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x00021B25 File Offset: 0x0001FD25
		void IDesignerFilter.PreFilterProperties(IDictionary properties)
		{
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x00021B27 File Offset: 0x0001FD27
		void IDisposable.Dispose()
		{
			this.theComponent = null;
		}

		// Token: 0x0400043B RID: 1083
		private IComponent theComponent;

		// Token: 0x0400043C RID: 1084
		private static readonly string[] propertiesToHide = new string[] { "AcceptChangesDuringFill", "AcceptChangesDuringUpdate", "TableMappings", "UpdateBatchSize", "ContinueUpdateOnError" };
	}
}
