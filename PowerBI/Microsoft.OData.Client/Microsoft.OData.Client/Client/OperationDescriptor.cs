using System;

namespace Microsoft.OData.Client
{
	// Token: 0x0200005A RID: 90
	public abstract class OperationDescriptor : Descriptor
	{
		// Token: 0x060002D4 RID: 724 RVA: 0x0000AFEC File Offset: 0x000091EC
		internal OperationDescriptor()
			: base(EntityStates.Unchanged)
		{
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x0000AFF5 File Offset: 0x000091F5
		// (set) Token: 0x060002D6 RID: 726 RVA: 0x0000AFFD File Offset: 0x000091FD
		public string Title
		{
			get
			{
				return this.title;
			}
			internal set
			{
				this.title = value;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x0000B006 File Offset: 0x00009206
		// (set) Token: 0x060002D8 RID: 728 RVA: 0x0000B00E File Offset: 0x0000920E
		public Uri Metadata
		{
			get
			{
				return this.metadata;
			}
			internal set
			{
				this.metadata = value;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x0000B017 File Offset: 0x00009217
		// (set) Token: 0x060002DA RID: 730 RVA: 0x0000B01F File Offset: 0x0000921F
		public Uri Target
		{
			get
			{
				return this.target;
			}
			internal set
			{
				this.target = value;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060002DB RID: 731 RVA: 0x00006F67 File Offset: 0x00005167
		internal override DescriptorKind DescriptorKind
		{
			get
			{
				return DescriptorKind.OperationDescriptor;
			}
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000B028 File Offset: 0x00009228
		internal override void ClearChanges()
		{
		}

		// Token: 0x040000F0 RID: 240
		private string title;

		// Token: 0x040000F1 RID: 241
		private Uri metadata;

		// Token: 0x040000F2 RID: 242
		private Uri target;
	}
}
