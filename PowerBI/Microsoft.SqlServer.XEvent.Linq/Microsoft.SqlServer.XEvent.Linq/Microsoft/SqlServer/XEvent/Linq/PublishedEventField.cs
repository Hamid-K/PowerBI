using System;

namespace Microsoft.SqlServer.XEvent.Linq
{
	// Token: 0x020000D4 RID: 212
	public class PublishedEventField : IField
	{
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060002AF RID: 687 RVA: 0x0001C784 File Offset: 0x0001C784
		public string Name
		{
			get
			{
				return this.m_Metadata.Fields[this.m_fieldIndex].Name;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x0001C7AC File Offset: 0x0001C7AC
		public Type Type
		{
			get
			{
				return this.m_Metadata.Fields[this.m_fieldIndex].Type;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x0001C7D4 File Offset: 0x0001C7D4
		// (set) Token: 0x060002B2 RID: 690 RVA: 0x0001C7E8 File Offset: 0x0001C7E8
		public object Value { get; internal set; }

		// Token: 0x060002B3 RID: 691 RVA: 0x0001C7FC File Offset: 0x0001C7FC
		internal PublishedEventField(int fieldIndex, IEventMetadata eventMd)
		{
			this.m_fieldIndex = fieldIndex;
			this.m_Metadata = eventMd;
		}

		// Token: 0x0400029A RID: 666
		private IEventMetadata m_Metadata;

		// Token: 0x0400029B RID: 667
		private int m_fieldIndex;
	}
}
