using System;
using Microsoft.Apache.Thrift;

namespace Microsoft.Apache.Parquet.Format
{
	// Token: 0x02002012 RID: 8210
	internal class FileMetaData
	{
		// Token: 0x17002D65 RID: 11621
		// (get) Token: 0x060111F7 RID: 70135 RVA: 0x003B013C File Offset: 0x003AE33C
		// (set) Token: 0x060111F8 RID: 70136 RVA: 0x003B0144 File Offset: 0x003AE344
		public int? Version { get; set; }

		// Token: 0x17002D66 RID: 11622
		// (get) Token: 0x060111F9 RID: 70137 RVA: 0x003B014D File Offset: 0x003AE34D
		// (set) Token: 0x060111FA RID: 70138 RVA: 0x003B0155 File Offset: 0x003AE355
		public SchemaElement[] Schema { get; set; }

		// Token: 0x17002D67 RID: 11623
		// (get) Token: 0x060111FB RID: 70139 RVA: 0x003B015E File Offset: 0x003AE35E
		// (set) Token: 0x060111FC RID: 70140 RVA: 0x003B0166 File Offset: 0x003AE366
		public long? NumRows { get; set; }

		// Token: 0x17002D68 RID: 11624
		// (get) Token: 0x060111FD RID: 70141 RVA: 0x003B016F File Offset: 0x003AE36F
		// (set) Token: 0x060111FE RID: 70142 RVA: 0x003B0177 File Offset: 0x003AE377
		public RowGroup[] RowGroups { get; set; }

		// Token: 0x17002D69 RID: 11625
		// (get) Token: 0x060111FF RID: 70143 RVA: 0x003B0180 File Offset: 0x003AE380
		// (set) Token: 0x06011200 RID: 70144 RVA: 0x003B0188 File Offset: 0x003AE388
		public KeyValue[] KeyValueMetadata { get; set; }

		// Token: 0x17002D6A RID: 11626
		// (get) Token: 0x06011201 RID: 70145 RVA: 0x003B0191 File Offset: 0x003AE391
		// (set) Token: 0x06011202 RID: 70146 RVA: 0x003B0199 File Offset: 0x003AE399
		public string CreatedBy { get; set; }

		// Token: 0x17002D6B RID: 11627
		// (get) Token: 0x06011203 RID: 70147 RVA: 0x003B01A2 File Offset: 0x003AE3A2
		// (set) Token: 0x06011204 RID: 70148 RVA: 0x003B01AA File Offset: 0x003AE3AA
		public ColumnOrder[] ColumnOrders { get; set; }

		// Token: 0x17002D6C RID: 11628
		// (get) Token: 0x06011205 RID: 70149 RVA: 0x003B01B3 File Offset: 0x003AE3B3
		// (set) Token: 0x06011206 RID: 70150 RVA: 0x003B01BB File Offset: 0x003AE3BB
		public EncryptionAlgorithm EncryptionAlgorithm { get; set; }

		// Token: 0x17002D6D RID: 11629
		// (get) Token: 0x06011207 RID: 70151 RVA: 0x003B01C4 File Offset: 0x003AE3C4
		// (set) Token: 0x06011208 RID: 70152 RVA: 0x003B01CC File Offset: 0x003AE3CC
		public byte[] FooterSigningKeyMetadata { get; set; }

		// Token: 0x06011209 RID: 70153 RVA: 0x003B01D8 File Offset: 0x003AE3D8
		public void Read(FastProtocol prot)
		{
			this.Version = null;
			this.Schema = null;
			this.NumRows = null;
			this.RowGroups = null;
			this.KeyValueMetadata = null;
			this.CreatedBy = null;
			this.ColumnOrders = null;
			this.EncryptionAlgorithm = null;
			this.FooterSigningKeyMetadata = null;
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
							this.Version = new int?(prot.ReadI32());
							flag = false;
						}
						break;
					case 2:
						if (tfield.Type == TType.List)
						{
							TList tlist = prot.ReadListBegin();
							this.Schema = new SchemaElement[tlist.Count];
							for (int i = 0; i < tlist.Count; i++)
							{
								SchemaElement schemaElement = new SchemaElement();
								schemaElement.Read(prot);
								this.Schema[i] = schemaElement;
							}
							prot.ReadListEnd();
							flag = false;
						}
						break;
					case 3:
						if (tfield.Type == TType.I64)
						{
							this.NumRows = new long?(prot.ReadI64());
							flag = false;
						}
						break;
					case 4:
						if (tfield.Type == TType.List)
						{
							TList tlist2 = prot.ReadListBegin();
							this.RowGroups = new RowGroup[tlist2.Count];
							for (int j = 0; j < tlist2.Count; j++)
							{
								RowGroup rowGroup = new RowGroup();
								rowGroup.Read(prot);
								this.RowGroups[j] = rowGroup;
							}
							prot.ReadListEnd();
							flag = false;
						}
						break;
					case 5:
						if (tfield.Type == TType.List)
						{
							TList tlist3 = prot.ReadListBegin();
							this.KeyValueMetadata = new KeyValue[tlist3.Count];
							for (int k = 0; k < tlist3.Count; k++)
							{
								KeyValue keyValue = new KeyValue();
								keyValue.Read(prot);
								this.KeyValueMetadata[k] = keyValue;
							}
							prot.ReadListEnd();
							flag = false;
						}
						break;
					case 6:
						if (tfield.Type == TType.String)
						{
							this.CreatedBy = prot.ReadString();
							flag = false;
						}
						break;
					case 7:
						if (tfield.Type == TType.List)
						{
							TList tlist4 = prot.ReadListBegin();
							this.ColumnOrders = new ColumnOrder[tlist4.Count];
							for (int l = 0; l < tlist4.Count; l++)
							{
								ColumnOrder columnOrder = new ColumnOrder();
								columnOrder.Read(prot);
								this.ColumnOrders[l] = columnOrder;
							}
							prot.ReadListEnd();
							flag = false;
						}
						break;
					case 8:
						if (tfield.Type == TType.Struct)
						{
							EncryptionAlgorithm encryptionAlgorithm = new EncryptionAlgorithm();
							encryptionAlgorithm.Read(prot);
							this.EncryptionAlgorithm = encryptionAlgorithm;
							flag = false;
						}
						break;
					case 9:
						if (tfield.Type == TType.String)
						{
							this.FooterSigningKeyMetadata = prot.ReadBytes();
							flag = false;
						}
						break;
					}
					prot.EndField(tfield.Type, flag);
				}
				prot.ReadStructEnd();
				if (this.Version == null || this.Schema == null || this.NumRows == null || this.RowGroups == null)
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
