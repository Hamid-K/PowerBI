using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200005A RID: 90
	internal sealed class AdomdDataAdapterDesigner : IDesigner, IDisposable, IDesignerFilter
	{
		// Token: 0x060005D4 RID: 1492 RVA: 0x00021791 File Offset: 0x0001F991
		internal AdomdDataAdapterDesigner()
		{
			this.theComponent = null;
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060005D5 RID: 1493 RVA: 0x000217A0 File Offset: 0x0001F9A0
		IComponent IDesigner.Component
		{
			get
			{
				return this.theComponent;
			}
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x000217A8 File Offset: 0x0001F9A8
		void IDesigner.DoDefaultAction()
		{
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x000217AA File Offset: 0x0001F9AA
		void IDesigner.Initialize(IComponent component)
		{
			this.theComponent = component;
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060005D8 RID: 1496 RVA: 0x000217B3 File Offset: 0x0001F9B3
		DesignerVerbCollection IDesigner.Verbs
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x000217B6 File Offset: 0x0001F9B6
		void IDesignerFilter.PostFilterAttributes(IDictionary attributes)
		{
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x000217B8 File Offset: 0x0001F9B8
		void IDesignerFilter.PostFilterEvents(IDictionary events)
		{
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x000217BC File Offset: 0x0001F9BC
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

		// Token: 0x060005DC RID: 1500 RVA: 0x000217F1 File Offset: 0x0001F9F1
		void IDesignerFilter.PreFilterAttributes(IDictionary attributes)
		{
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x000217F3 File Offset: 0x0001F9F3
		void IDesignerFilter.PreFilterEvents(IDictionary events)
		{
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x000217F5 File Offset: 0x0001F9F5
		void IDesignerFilter.PreFilterProperties(IDictionary properties)
		{
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x000217F7 File Offset: 0x0001F9F7
		void IDisposable.Dispose()
		{
			this.theComponent = null;
		}

		// Token: 0x0400042E RID: 1070
		private IComponent theComponent;

		// Token: 0x0400042F RID: 1071
		private static readonly string[] propertiesToHide = new string[] { "AcceptChangesDuringFill", "AcceptChangesDuringUpdate", "TableMappings", "UpdateBatchSize", "ContinueUpdateOnError" };
	}
}
