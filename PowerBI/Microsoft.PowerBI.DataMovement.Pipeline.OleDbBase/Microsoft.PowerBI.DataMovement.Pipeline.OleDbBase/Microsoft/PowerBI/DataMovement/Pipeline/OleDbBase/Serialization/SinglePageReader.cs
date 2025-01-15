using System;
using System.Data;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase.Serialization
{
	// Token: 0x020000E6 RID: 230
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public class SinglePageReader : IDisposable
	{
		// Token: 0x060004A3 RID: 1187 RVA: 0x0000E28F File Offset: 0x0000C48F
		public SinglePageReader(IPageReader reader)
		{
			this.reader = reader;
			this.page = reader.CreatePage();
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060004A4 RID: 1188 RVA: 0x0000E2AA File Offset: 0x0000C4AA
		public DataTable SchemaTable
		{
			get
			{
				return this.reader.SchemaTable;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060004A5 RID: 1189 RVA: 0x0000E2B7 File Offset: 0x0000C4B7
		public IPage Page
		{
			get
			{
				return this.page;
			}
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x0000E2BF File Offset: 0x0000C4BF
		public void Read()
		{
			this.reader.Read(this.page);
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x0000E2D2 File Offset: 0x0000C4D2
		public void Dispose()
		{
			this.page.Dispose();
			this.reader.Dispose();
		}

		// Token: 0x040003FB RID: 1019
		private readonly IPageReader reader;

		// Token: 0x040003FC RID: 1020
		private readonly IPage page;
	}
}
