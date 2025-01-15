using System;
using Microsoft.Apache.Thrift;

namespace Microsoft.Apache.Parquet.Format
{
	// Token: 0x02002025 RID: 8229
	internal class SortingColumn
	{
		// Token: 0x17002D9F RID: 11679
		// (get) Token: 0x0601128F RID: 70287 RVA: 0x003B1B7C File Offset: 0x003AFD7C
		// (set) Token: 0x06011290 RID: 70288 RVA: 0x003B1B84 File Offset: 0x003AFD84
		public int? ColumnIdx { get; set; }

		// Token: 0x17002DA0 RID: 11680
		// (get) Token: 0x06011291 RID: 70289 RVA: 0x003B1B8D File Offset: 0x003AFD8D
		// (set) Token: 0x06011292 RID: 70290 RVA: 0x003B1B95 File Offset: 0x003AFD95
		public bool? Descending { get; set; }

		// Token: 0x17002DA1 RID: 11681
		// (get) Token: 0x06011293 RID: 70291 RVA: 0x003B1B9E File Offset: 0x003AFD9E
		// (set) Token: 0x06011294 RID: 70292 RVA: 0x003B1BA6 File Offset: 0x003AFDA6
		public bool? NullsFirst { get; set; }

		// Token: 0x06011295 RID: 70293 RVA: 0x003B1BB0 File Offset: 0x003AFDB0
		public void Read(FastProtocol prot)
		{
			this.ColumnIdx = null;
			this.Descending = null;
			this.NullsFirst = null;
			prot.IncrementRecursionDepth();
			try
			{
				prot.ReadStructBegin();
				for (;;)
				{
					TField tfield = prot.ReadFieldBegin();
					if (tfield.Type == TType.Stop)
					{
						break;
					}
					bool flag = true;
					switch (tfield.ID)
					{
					case 1:
						if (tfield.Type == TType.I32)
						{
							this.ColumnIdx = new int?(prot.ReadI32());
							flag = false;
						}
						break;
					case 2:
						if (tfield.Type == TType.Bool)
						{
							this.Descending = new bool?(prot.ReadBool());
							flag = false;
						}
						break;
					case 3:
						if (tfield.Type == TType.Bool)
						{
							this.NullsFirst = new bool?(prot.ReadBool());
							flag = false;
						}
						break;
					}
					prot.EndField(tfield.Type, flag);
				}
				prot.ReadStructEnd();
				if (this.ColumnIdx == null || this.Descending == null || this.NullsFirst == null)
				{
					throw new InvalidOperationException("Required field not found");
				}
			}
			finally
			{
				prot.DecrementRecursionDepth();
			}
		}
	}
}
