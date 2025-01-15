using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq.Expressions;

namespace System.Data.Entity.Core.Common.Internal.Materialization
{
	// Token: 0x0200063D RID: 1597
	internal class RecordStateScratchpad
	{
		// Token: 0x17000EDC RID: 3804
		// (get) Token: 0x06004CC7 RID: 19655 RVA: 0x0010F2E9 File Offset: 0x0010D4E9
		// (set) Token: 0x06004CC8 RID: 19656 RVA: 0x0010F2F1 File Offset: 0x0010D4F1
		internal int StateSlotNumber { get; set; }

		// Token: 0x17000EDD RID: 3805
		// (get) Token: 0x06004CC9 RID: 19657 RVA: 0x0010F2FA File Offset: 0x0010D4FA
		// (set) Token: 0x06004CCA RID: 19658 RVA: 0x0010F302 File Offset: 0x0010D502
		internal int ColumnCount { get; set; }

		// Token: 0x17000EDE RID: 3806
		// (get) Token: 0x06004CCB RID: 19659 RVA: 0x0010F30B File Offset: 0x0010D50B
		// (set) Token: 0x06004CCC RID: 19660 RVA: 0x0010F313 File Offset: 0x0010D513
		internal DataRecordInfo DataRecordInfo { get; set; }

		// Token: 0x17000EDF RID: 3807
		// (get) Token: 0x06004CCD RID: 19661 RVA: 0x0010F31C File Offset: 0x0010D51C
		// (set) Token: 0x06004CCE RID: 19662 RVA: 0x0010F324 File Offset: 0x0010D524
		internal Expression GatherData { get; set; }

		// Token: 0x17000EE0 RID: 3808
		// (get) Token: 0x06004CCF RID: 19663 RVA: 0x0010F32D File Offset: 0x0010D52D
		// (set) Token: 0x06004CD0 RID: 19664 RVA: 0x0010F335 File Offset: 0x0010D535
		internal string[] PropertyNames { get; set; }

		// Token: 0x17000EE1 RID: 3809
		// (get) Token: 0x06004CD1 RID: 19665 RVA: 0x0010F33E File Offset: 0x0010D53E
		// (set) Token: 0x06004CD2 RID: 19666 RVA: 0x0010F346 File Offset: 0x0010D546
		internal TypeUsage[] TypeUsages { get; set; }

		// Token: 0x06004CD3 RID: 19667 RVA: 0x0010F350 File Offset: 0x0010D550
		internal RecordStateFactory Compile()
		{
			RecordStateFactory[] array = new RecordStateFactory[this._nestedRecordStateScratchpads.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this._nestedRecordStateScratchpads[i].Compile();
			}
			return (RecordStateFactory)Activator.CreateInstance(typeof(RecordStateFactory), new object[] { this.StateSlotNumber, this.ColumnCount, array, this.DataRecordInfo, this.GatherData, this.PropertyNames, this.TypeUsages });
		}

		// Token: 0x04001B50 RID: 6992
		private readonly List<RecordStateScratchpad> _nestedRecordStateScratchpads = new List<RecordStateScratchpad>();
	}
}
