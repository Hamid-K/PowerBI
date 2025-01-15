using System;
using System.Data;
using System.IO;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase.Serialization
{
	// Token: 0x020000DF RID: 223
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public class OleDbPageWriter : IDisposable
	{
		// Token: 0x06000427 RID: 1063 RVA: 0x0000C8C9 File Offset: 0x0000AAC9
		public OleDbPageWriter(Stream stream, DataTable schemaTable, bool writeColmnOrdinals, bool closeStreamOnDispose)
		{
			this.m_writer = new PageWriter(stream);
			this.m_schemaTable = schemaTable;
			this.m_writer.WriteSchema(schemaTable, writeColmnOrdinals);
			this.m_closeStreamOnDispose = closeStreamOnDispose;
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000428 RID: 1064 RVA: 0x0000C8F9 File Offset: 0x0000AAF9
		public DataTable SchemaTable
		{
			get
			{
				return this.m_schemaTable;
			}
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x0000C901 File Offset: 0x0000AB01
		public void Write(IPage page)
		{
			this.m_writer.WritePage(page);
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0000C90F File Offset: 0x0000AB0F
		public void Flush()
		{
			this.m_writer.Flush();
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0000C91C File Offset: 0x0000AB1C
		public void Dispose()
		{
			if (this.m_closeStreamOnDispose)
			{
				this.m_writer.Dispose();
			}
		}

		// Token: 0x040003D8 RID: 984
		private readonly PageWriter m_writer;

		// Token: 0x040003D9 RID: 985
		private readonly DataTable m_schemaTable;

		// Token: 0x040003DA RID: 986
		private readonly bool m_closeStreamOnDispose;
	}
}
