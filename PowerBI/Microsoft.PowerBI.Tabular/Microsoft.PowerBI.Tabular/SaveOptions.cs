using System;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000114 RID: 276
	public class SaveOptions
	{
		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x060011E8 RID: 4584 RVA: 0x0007E67E File Offset: 0x0007C87E
		// (set) Token: 0x060011E9 RID: 4585 RVA: 0x0007E68C File Offset: 0x0007C88C
		public bool DelayValidation
		{
			get
			{
				return SaveOptions.IsFlagEnabled(this.SaveFlags, SaveFlags.DelayValidation);
			}
			set
			{
				this.SaveFlags = SaveOptions.Update(this.SaveFlags, SaveFlags.DelayValidation, value);
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x060011EA RID: 4586 RVA: 0x0007E6A1 File Offset: 0x0007C8A1
		// (set) Token: 0x060011EB RID: 4587 RVA: 0x0007E6AF File Offset: 0x0007C8AF
		public bool ForceValidation
		{
			get
			{
				return SaveOptions.IsFlagEnabled(this.SaveFlags, SaveFlags.ForceValidation);
			}
			set
			{
				this.SaveFlags = SaveOptions.Update(this.SaveFlags, SaveFlags.ForceValidation, value);
			}
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x060011EC RID: 4588 RVA: 0x0007E6C4 File Offset: 0x0007C8C4
		// (set) Token: 0x060011ED RID: 4589 RVA: 0x0007E6CC File Offset: 0x0007C8CC
		public SaveFlags SaveFlags { get; set; }

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x060011EE RID: 4590 RVA: 0x0007E6D5 File Offset: 0x0007C8D5
		// (set) Token: 0x060011EF RID: 4591 RVA: 0x0007E6DD File Offset: 0x0007C8DD
		public int MaxParallelism { get; set; }

		// Token: 0x060011F0 RID: 4592 RVA: 0x0007E6E6 File Offset: 0x0007C8E6
		internal static bool IsFlagEnabled(SaveFlags flags, SaveFlags flag)
		{
			return (flags & flag) == flag;
		}

		// Token: 0x060011F1 RID: 4593 RVA: 0x0007E6EE File Offset: 0x0007C8EE
		internal static bool IsFlagDisabled(SaveFlags flags, SaveFlags flag)
		{
			return (flags & flag) == SaveFlags.Default;
		}

		// Token: 0x060011F2 RID: 4594 RVA: 0x0007E6F6 File Offset: 0x0007C8F6
		private static SaveFlags Update(SaveFlags flags, SaveFlags flag, bool isEnabled)
		{
			if (isEnabled)
			{
				return flags |= flag;
			}
			return flags &= ~flag;
		}
	}
}
