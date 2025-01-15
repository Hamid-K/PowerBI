using System;
using System.Collections.ObjectModel;

namespace Microsoft.SqlServer.XEvent.Linq
{
	// Token: 0x020000D5 RID: 213
	internal class GenericPackage : IPackage
	{
		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x0001C820 File Offset: 0x0001C820
		// (set) Token: 0x060002B5 RID: 693 RVA: 0x0001C834 File Offset: 0x0001C834
		public string Name { get; internal set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x0001C848 File Offset: 0x0001C848
		// (set) Token: 0x060002B7 RID: 695 RVA: 0x0001C85C File Offset: 0x0001C85C
		public string Description { get; internal set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x0001C870 File Offset: 0x0001C870
		// (set) Token: 0x060002B9 RID: 697 RVA: 0x0001C884 File Offset: 0x0001C884
		public Guid ModuleId { get; internal set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060002BA RID: 698 RVA: 0x0001C898 File Offset: 0x0001C898
		// (set) Token: 0x060002BB RID: 699 RVA: 0x0001C8AC File Offset: 0x0001C8AC
		public Guid PackageId { get; internal set; }

		// Token: 0x060002BC RID: 700 RVA: 0x0001C8C0 File Offset: 0x0001C8C0
		public override string ToString()
		{
			return this.ModuleId.ToString() + "." + this.Name;
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060002BD RID: 701 RVA: 0x0001C8F4 File Offset: 0x0001C8F4
		// (set) Token: 0x060002BE RID: 702 RVA: 0x0001C908 File Offset: 0x0001C908
		public IMetadataGeneration Generation { get; internal set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060002BF RID: 703 RVA: 0x0001C91C File Offset: 0x0001C91C
		// (set) Token: 0x060002C0 RID: 704 RVA: 0x0001C930 File Offset: 0x0001C930
		public ReadOnlyCollection<IEventMetadata> Events { get; internal set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x0001C944 File Offset: 0x0001C944
		// (set) Token: 0x060002C2 RID: 706 RVA: 0x0001C958 File Offset: 0x0001C958
		public ReadOnlyCollection<IActionMetadata> Actions { get; internal set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x0001C96C File Offset: 0x0001C96C
		// (set) Token: 0x060002C4 RID: 708 RVA: 0x0001C980 File Offset: 0x0001C980
		public ReadOnlyCollection<ITargetMetadata> Targets { get; internal set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x0001C994 File Offset: 0x0001C994
		// (set) Token: 0x060002C6 RID: 710 RVA: 0x0001C9A8 File Offset: 0x0001C9A8
		public ReadOnlyCollection<IMapMetadata> Maps { get; internal set; }
	}
}
