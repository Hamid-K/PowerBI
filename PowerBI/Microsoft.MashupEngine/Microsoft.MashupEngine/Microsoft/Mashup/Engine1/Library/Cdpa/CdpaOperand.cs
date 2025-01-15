using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Mashup.Common;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000DFF RID: 3583
	[DataContract]
	internal class CdpaOperand
	{
		// Token: 0x0600608E RID: 24718 RVA: 0x00149EFD File Offset: 0x001480FD
		public CdpaOperand()
		{
			this.Splits = EmptyArray<CdpaMetricSplit>.Instance;
		}

		// Token: 0x17001C80 RID: 7296
		// (get) Token: 0x0600608F RID: 24719 RVA: 0x00149F10 File Offset: 0x00148110
		// (set) Token: 0x06006090 RID: 24720 RVA: 0x00149F18 File Offset: 0x00148118
		[DataMember(Name = "id", IsRequired = true)]
		public string Id { get; set; }

		// Token: 0x17001C81 RID: 7297
		// (get) Token: 0x06006091 RID: 24721 RVA: 0x00149F21 File Offset: 0x00148121
		// (set) Token: 0x06006092 RID: 24722 RVA: 0x00149F29 File Offset: 0x00148129
		[DataMember(Name = "table", IsRequired = true)]
		public CdpaTableConfiguration Table { get; set; }

		// Token: 0x17001C82 RID: 7298
		// (get) Token: 0x06006093 RID: 24723 RVA: 0x00149F32 File Offset: 0x00148132
		// (set) Token: 0x06006094 RID: 24724 RVA: 0x00149F3A File Offset: 0x0014813A
		[DataMember(Name = "measure", IsRequired = false)]
		public CdpaMetricMeasure Measure { get; set; }

		// Token: 0x17001C83 RID: 7299
		// (get) Token: 0x06006095 RID: 24725 RVA: 0x00149F43 File Offset: 0x00148143
		// (set) Token: 0x06006096 RID: 24726 RVA: 0x00149F4B File Offset: 0x0014814B
		[DataMember(Name = "splits", IsRequired = false)]
		public IList<CdpaMetricSplit> Splits { get; set; }

		// Token: 0x17001C84 RID: 7300
		// (get) Token: 0x06006097 RID: 24727 RVA: 0x00149F54 File Offset: 0x00148154
		// (set) Token: 0x06006098 RID: 24728 RVA: 0x00149F5C File Offset: 0x0014815C
		[DataMember(Name = "top", IsRequired = false)]
		public CdpaTopConfiguration Top { get; set; }

		// Token: 0x06006099 RID: 24729 RVA: 0x00149F68 File Offset: 0x00148168
		public CdpaOperand ShallowCopy()
		{
			return new CdpaOperand
			{
				Id = this.Id,
				Table = this.Table,
				Measure = this.Measure,
				Splits = this.Splits,
				Top = this.Top
			};
		}

		// Token: 0x0600609A RID: 24730 RVA: 0x00149FB6 File Offset: 0x001481B6
		public CdpaOperand Filter(CdpaPropertyFilterOrGroup filter)
		{
			CdpaOperand cdpaOperand = this.ShallowCopy();
			cdpaOperand.Table = this.Table.Filter(filter);
			return cdpaOperand;
		}
	}
}
