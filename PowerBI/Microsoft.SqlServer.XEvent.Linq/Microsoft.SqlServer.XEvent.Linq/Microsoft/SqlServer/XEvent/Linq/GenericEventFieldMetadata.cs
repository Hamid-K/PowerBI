using System;

namespace Microsoft.SqlServer.XEvent.Linq
{
	// Token: 0x020000D6 RID: 214
	internal class GenericEventFieldMetadata : IEventFieldMetadata
	{
		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x0001C9D0 File Offset: 0x0001C9D0
		// (set) Token: 0x060002C9 RID: 713 RVA: 0x0001C9E4 File Offset: 0x0001C9E4
		public string Name { get; internal set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060002CA RID: 714 RVA: 0x0001C9F8 File Offset: 0x0001C9F8
		// (set) Token: 0x060002CB RID: 715 RVA: 0x0001CA0C File Offset: 0x0001CA0C
		public Type Type { get; internal set; }

		// Token: 0x060002CC RID: 716 RVA: 0x0001CA20 File Offset: 0x0001CA20
		internal GenericEventFieldMetadata(string name, Type type)
		{
			this.Name = name;
			this.Type = type;
		}
	}
}
