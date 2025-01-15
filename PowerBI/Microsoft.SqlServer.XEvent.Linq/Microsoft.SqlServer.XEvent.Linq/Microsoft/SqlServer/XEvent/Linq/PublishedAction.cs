using System;

namespace Microsoft.SqlServer.XEvent.Linq
{
	// Token: 0x020000D3 RID: 211
	public class PublishedAction : IField
	{
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x0001C6DC File Offset: 0x0001C6DC
		public string Name
		{
			get
			{
				return this.m_metadata.Name;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x0001C6F4 File Offset: 0x0001C6F4
		public IPackage Package
		{
			get
			{
				return this.m_metadata.Package;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060002AA RID: 682 RVA: 0x0001C70C File Offset: 0x0001C70C
		public Type Type
		{
			get
			{
				return this.m_metadata.Type;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060002AB RID: 683 RVA: 0x0001C724 File Offset: 0x0001C724
		public IActionMetadata Metadata
		{
			get
			{
				return this.m_metadata;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060002AC RID: 684 RVA: 0x0001C738 File Offset: 0x0001C738
		// (set) Token: 0x060002AD RID: 685 RVA: 0x0001C74C File Offset: 0x0001C74C
		public object Value { get; private set; }

		// Token: 0x060002AE RID: 686 RVA: 0x0001C760 File Offset: 0x0001C760
		internal PublishedAction(IActionMetadata md, object value)
		{
			this.m_metadata = md;
			this.Value = value;
		}

		// Token: 0x04000298 RID: 664
		private IActionMetadata m_metadata;
	}
}
