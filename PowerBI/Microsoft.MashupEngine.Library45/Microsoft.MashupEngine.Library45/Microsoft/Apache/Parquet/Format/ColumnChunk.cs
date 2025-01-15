using System;
using Microsoft.Apache.Thrift;

namespace Microsoft.Apache.Parquet.Format
{
	// Token: 0x02001FFF RID: 8191
	internal class ColumnChunk
	{
		// Token: 0x17002D2D RID: 11565
		// (get) Token: 0x06011169 RID: 69993 RVA: 0x003AE7F4 File Offset: 0x003AC9F4
		// (set) Token: 0x0601116A RID: 69994 RVA: 0x003AE7FC File Offset: 0x003AC9FC
		public string FilePath { get; set; }

		// Token: 0x17002D2E RID: 11566
		// (get) Token: 0x0601116B RID: 69995 RVA: 0x003AE805 File Offset: 0x003ACA05
		// (set) Token: 0x0601116C RID: 69996 RVA: 0x003AE80D File Offset: 0x003ACA0D
		public long? FileOffset { get; set; }

		// Token: 0x17002D2F RID: 11567
		// (get) Token: 0x0601116D RID: 69997 RVA: 0x003AE816 File Offset: 0x003ACA16
		// (set) Token: 0x0601116E RID: 69998 RVA: 0x003AE81E File Offset: 0x003ACA1E
		public ColumnMetaData MetaData { get; set; }

		// Token: 0x17002D30 RID: 11568
		// (get) Token: 0x0601116F RID: 69999 RVA: 0x003AE827 File Offset: 0x003ACA27
		// (set) Token: 0x06011170 RID: 70000 RVA: 0x003AE82F File Offset: 0x003ACA2F
		public long? OffsetIndexOffset { get; set; }

		// Token: 0x17002D31 RID: 11569
		// (get) Token: 0x06011171 RID: 70001 RVA: 0x003AE838 File Offset: 0x003ACA38
		// (set) Token: 0x06011172 RID: 70002 RVA: 0x003AE840 File Offset: 0x003ACA40
		public int? OffsetIndexLength { get; set; }

		// Token: 0x17002D32 RID: 11570
		// (get) Token: 0x06011173 RID: 70003 RVA: 0x003AE849 File Offset: 0x003ACA49
		// (set) Token: 0x06011174 RID: 70004 RVA: 0x003AE851 File Offset: 0x003ACA51
		public long? ColumnIndexOffset { get; set; }

		// Token: 0x17002D33 RID: 11571
		// (get) Token: 0x06011175 RID: 70005 RVA: 0x003AE85A File Offset: 0x003ACA5A
		// (set) Token: 0x06011176 RID: 70006 RVA: 0x003AE862 File Offset: 0x003ACA62
		public int? ColumnIndexLength { get; set; }

		// Token: 0x17002D34 RID: 11572
		// (get) Token: 0x06011177 RID: 70007 RVA: 0x003AE86B File Offset: 0x003ACA6B
		// (set) Token: 0x06011178 RID: 70008 RVA: 0x003AE873 File Offset: 0x003ACA73
		public ColumnCryptoMetaData CryptoMetadata { get; set; }

		// Token: 0x17002D35 RID: 11573
		// (get) Token: 0x06011179 RID: 70009 RVA: 0x003AE87C File Offset: 0x003ACA7C
		// (set) Token: 0x0601117A RID: 70010 RVA: 0x003AE884 File Offset: 0x003ACA84
		public byte[] EncryptedColumnMetadata { get; set; }

		// Token: 0x0601117B RID: 70011 RVA: 0x003AE890 File Offset: 0x003ACA90
		public void Read(FastProtocol prot)
		{
			this.FilePath = null;
			this.FileOffset = null;
			this.MetaData = null;
			this.OffsetIndexOffset = null;
			this.OffsetIndexLength = null;
			this.ColumnIndexOffset = null;
			this.ColumnIndexLength = null;
			this.CryptoMetadata = null;
			this.EncryptedColumnMetadata = null;
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
						if (tfield.Type == TType.String)
						{
							this.FilePath = prot.ReadString();
							flag = false;
						}
						break;
					case 2:
						if (tfield.Type == TType.I64)
						{
							this.FileOffset = new long?(prot.ReadI64());
							flag = false;
						}
						break;
					case 3:
						if (tfield.Type == TType.Struct)
						{
							ColumnMetaData columnMetaData = new ColumnMetaData();
							columnMetaData.Read(prot);
							this.MetaData = columnMetaData;
							flag = false;
						}
						break;
					case 4:
						if (tfield.Type == TType.I64)
						{
							this.OffsetIndexOffset = new long?(prot.ReadI64());
							flag = false;
						}
						break;
					case 5:
						if (tfield.Type == TType.I32)
						{
							this.OffsetIndexLength = new int?(prot.ReadI32());
							flag = false;
						}
						break;
					case 6:
						if (tfield.Type == TType.I64)
						{
							this.ColumnIndexOffset = new long?(prot.ReadI64());
							flag = false;
						}
						break;
					case 7:
						if (tfield.Type == TType.I32)
						{
							this.ColumnIndexLength = new int?(prot.ReadI32());
							flag = false;
						}
						break;
					case 8:
						if (tfield.Type == TType.Struct)
						{
							ColumnCryptoMetaData columnCryptoMetaData = new ColumnCryptoMetaData();
							columnCryptoMetaData.Read(prot);
							this.CryptoMetadata = columnCryptoMetaData;
							flag = false;
						}
						break;
					case 9:
						if (tfield.Type == TType.String)
						{
							this.EncryptedColumnMetadata = prot.ReadBytes();
							flag = false;
						}
						break;
					}
					prot.EndField(tfield.Type, flag);
				}
				prot.ReadStructEnd();
				if (this.FileOffset == null)
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
