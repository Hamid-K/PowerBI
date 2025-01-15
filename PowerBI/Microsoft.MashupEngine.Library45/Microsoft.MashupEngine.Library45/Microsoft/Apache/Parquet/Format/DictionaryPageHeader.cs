using System;
using Microsoft.Apache.Thrift;

namespace Microsoft.Apache.Parquet.Format
{
	// Token: 0x0200200A RID: 8202
	internal class DictionaryPageHeader
	{
		// Token: 0x17002D5C RID: 11612
		// (get) Token: 0x060111D9 RID: 70105 RVA: 0x003AFC1C File Offset: 0x003ADE1C
		// (set) Token: 0x060111DA RID: 70106 RVA: 0x003AFC24 File Offset: 0x003ADE24
		public int? NumValues { get; set; }

		// Token: 0x17002D5D RID: 11613
		// (get) Token: 0x060111DB RID: 70107 RVA: 0x003AFC2D File Offset: 0x003ADE2D
		// (set) Token: 0x060111DC RID: 70108 RVA: 0x003AFC35 File Offset: 0x003ADE35
		public Encoding? Encoding { get; set; }

		// Token: 0x17002D5E RID: 11614
		// (get) Token: 0x060111DD RID: 70109 RVA: 0x003AFC3E File Offset: 0x003ADE3E
		// (set) Token: 0x060111DE RID: 70110 RVA: 0x003AFC46 File Offset: 0x003ADE46
		public bool? IsSorted { get; set; }

		// Token: 0x060111DF RID: 70111 RVA: 0x003AFC50 File Offset: 0x003ADE50
		public void Read(FastProtocol prot)
		{
			this.NumValues = null;
			this.Encoding = null;
			this.IsSorted = null;
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
							this.NumValues = new int?(prot.ReadI32());
							flag = false;
						}
						break;
					case 2:
						if (tfield.Type == TType.I32)
						{
							this.Encoding = new Encoding?((Encoding)prot.ReadI32());
							flag = false;
						}
						break;
					case 3:
						if (tfield.Type == TType.Bool)
						{
							this.IsSorted = new bool?(prot.ReadBool());
							flag = false;
						}
						break;
					}
					prot.EndField(tfield.Type, flag);
				}
				prot.ReadStructEnd();
				if (this.NumValues == null || this.Encoding == null)
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
