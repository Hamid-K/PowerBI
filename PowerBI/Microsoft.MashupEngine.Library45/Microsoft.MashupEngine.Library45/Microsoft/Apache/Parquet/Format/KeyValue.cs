using System;
using Microsoft.Apache.Thrift;

namespace Microsoft.Apache.Parquet.Format
{
	// Token: 0x02002016 RID: 8214
	internal class KeyValue
	{
		// Token: 0x17002D70 RID: 11632
		// (get) Token: 0x06011215 RID: 70165 RVA: 0x003B0718 File Offset: 0x003AE918
		// (set) Token: 0x06011216 RID: 70166 RVA: 0x003B0720 File Offset: 0x003AE920
		public string Key { get; set; }

		// Token: 0x17002D71 RID: 11633
		// (get) Token: 0x06011217 RID: 70167 RVA: 0x003B0729 File Offset: 0x003AE929
		// (set) Token: 0x06011218 RID: 70168 RVA: 0x003B0731 File Offset: 0x003AE931
		public string Value { get; set; }

		// Token: 0x06011219 RID: 70169 RVA: 0x003B073C File Offset: 0x003AE93C
		public void Read(FastProtocol prot)
		{
			this.Key = null;
			this.Value = null;
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
					short id = tfield.ID;
					if (id != 1)
					{
						if (id == 2)
						{
							if (tfield.Type == TType.String)
							{
								this.Value = prot.ReadString();
								flag = false;
							}
						}
					}
					else if (tfield.Type == TType.String)
					{
						this.Key = prot.ReadString();
						flag = false;
					}
					prot.EndField(tfield.Type, flag);
				}
				prot.ReadStructEnd();
				if (this.Key == null)
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
