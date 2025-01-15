using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.Mashup.Common;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000E00 RID: 3584
	[DataContract]
	internal class CdpaMetricConfiguration : CdpaTableOrMetricConfiguration
	{
		// Token: 0x0600609B RID: 24731 RVA: 0x00149FD0 File Offset: 0x001481D0
		public CdpaMetricConfiguration()
		{
			this.Operands = EmptyArray<CdpaOperand>.Instance;
		}

		// Token: 0x17001C85 RID: 7301
		// (get) Token: 0x0600609C RID: 24732 RVA: 0x00149FE3 File Offset: 0x001481E3
		// (set) Token: 0x0600609D RID: 24733 RVA: 0x00149FEB File Offset: 0x001481EB
		[DataMember(Name = "operands", IsRequired = true)]
		public IList<CdpaOperand> Operands { get; set; }

		// Token: 0x17001C86 RID: 7302
		// (get) Token: 0x0600609E RID: 24734 RVA: 0x00149FF4 File Offset: 0x001481F4
		public bool IsEmpty
		{
			get
			{
				return this.Operands.Count == 0;
			}
		}

		// Token: 0x0600609F RID: 24735 RVA: 0x0014A004 File Offset: 0x00148204
		public CdpaMetricConfiguration ShallowCopy()
		{
			return new CdpaMetricConfiguration
			{
				Operands = this.Operands
			};
		}

		// Token: 0x060060A0 RID: 24736 RVA: 0x0014A018 File Offset: 0x00148218
		public CdpaMetricConfiguration Filter(CdpaPropertyFilterOrGroup filter)
		{
			CdpaMetricConfiguration cdpaMetricConfiguration = this.ShallowCopy();
			cdpaMetricConfiguration.Operands = this.Operands.Select((CdpaOperand o) => o.Filter(filter)).ToArray<CdpaOperand>();
			return cdpaMetricConfiguration;
		}
	}
}
