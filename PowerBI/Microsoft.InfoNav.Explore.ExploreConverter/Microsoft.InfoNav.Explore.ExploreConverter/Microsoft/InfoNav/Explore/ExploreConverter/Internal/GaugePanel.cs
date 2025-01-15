using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x02000095 RID: 149
	internal sealed class GaugePanel : ReportItem
	{
		// Token: 0x060002E9 RID: 745 RVA: 0x0000CDE0 File Offset: 0x0000AFE0
		internal GaugePanel(string name, ReportItemRect rect, int zIndex, ReportParsingDiagnosticContext diagnosticContext, List<StateIndicator> stateIndicators)
			: base("GaugePanel", name, rect, zIndex, diagnosticContext)
		{
			this._stateIndicators = stateIndicators;
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060002EA RID: 746 RVA: 0x0000CDFC File Offset: 0x0000AFFC
		public Expression FirstValue
		{
			get
			{
				Contract.Check(this._stateIndicators != null, "Expect stateIndicators to not be null");
				if (this._stateIndicators.Count > 0)
				{
					StateIndicator stateIndicator = this._stateIndicators[0];
					if (stateIndicator != null)
					{
						return stateIndicator.Value;
					}
				}
				return null;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060002EB RID: 747 RVA: 0x0000CE42 File Offset: 0x0000B042
		public List<StateIndicator> StateIndicators
		{
			get
			{
				return this._stateIndicators;
			}
		}

		// Token: 0x040001EF RID: 495
		private List<StateIndicator> _stateIndicators;
	}
}
