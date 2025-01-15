using System;
using Microsoft.Apache.Thrift;

namespace Microsoft.Apache.Parquet.Format
{
	// Token: 0x02002002 RID: 8194
	internal class ColumnMetaData
	{
		// Token: 0x17002D3D RID: 11581
		// (get) Token: 0x0601118F RID: 70031 RVA: 0x003AEEA4 File Offset: 0x003AD0A4
		// (set) Token: 0x06011190 RID: 70032 RVA: 0x003AEEAC File Offset: 0x003AD0AC
		public Type? Type { get; set; }

		// Token: 0x17002D3E RID: 11582
		// (get) Token: 0x06011191 RID: 70033 RVA: 0x003AEEB5 File Offset: 0x003AD0B5
		// (set) Token: 0x06011192 RID: 70034 RVA: 0x003AEEBD File Offset: 0x003AD0BD
		public Encoding[] Encodings { get; set; }

		// Token: 0x17002D3F RID: 11583
		// (get) Token: 0x06011193 RID: 70035 RVA: 0x003AEEC6 File Offset: 0x003AD0C6
		// (set) Token: 0x06011194 RID: 70036 RVA: 0x003AEECE File Offset: 0x003AD0CE
		public string[] PathInSchema { get; set; }

		// Token: 0x17002D40 RID: 11584
		// (get) Token: 0x06011195 RID: 70037 RVA: 0x003AEED7 File Offset: 0x003AD0D7
		// (set) Token: 0x06011196 RID: 70038 RVA: 0x003AEEDF File Offset: 0x003AD0DF
		public CompressionCodec? Codec { get; set; }

		// Token: 0x17002D41 RID: 11585
		// (get) Token: 0x06011197 RID: 70039 RVA: 0x003AEEE8 File Offset: 0x003AD0E8
		// (set) Token: 0x06011198 RID: 70040 RVA: 0x003AEEF0 File Offset: 0x003AD0F0
		public long? NumValues { get; set; }

		// Token: 0x17002D42 RID: 11586
		// (get) Token: 0x06011199 RID: 70041 RVA: 0x003AEEF9 File Offset: 0x003AD0F9
		// (set) Token: 0x0601119A RID: 70042 RVA: 0x003AEF01 File Offset: 0x003AD101
		public long? TotalUncompressedSize { get; set; }

		// Token: 0x17002D43 RID: 11587
		// (get) Token: 0x0601119B RID: 70043 RVA: 0x003AEF0A File Offset: 0x003AD10A
		// (set) Token: 0x0601119C RID: 70044 RVA: 0x003AEF12 File Offset: 0x003AD112
		public long? TotalCompressedSize { get; set; }

		// Token: 0x17002D44 RID: 11588
		// (get) Token: 0x0601119D RID: 70045 RVA: 0x003AEF1B File Offset: 0x003AD11B
		// (set) Token: 0x0601119E RID: 70046 RVA: 0x003AEF23 File Offset: 0x003AD123
		public KeyValue[] KeyValueMetadata { get; set; }

		// Token: 0x17002D45 RID: 11589
		// (get) Token: 0x0601119F RID: 70047 RVA: 0x003AEF2C File Offset: 0x003AD12C
		// (set) Token: 0x060111A0 RID: 70048 RVA: 0x003AEF34 File Offset: 0x003AD134
		public long? DataPageOffset { get; set; }

		// Token: 0x17002D46 RID: 11590
		// (get) Token: 0x060111A1 RID: 70049 RVA: 0x003AEF3D File Offset: 0x003AD13D
		// (set) Token: 0x060111A2 RID: 70050 RVA: 0x003AEF45 File Offset: 0x003AD145
		public long? IndexPageOffset { get; set; }

		// Token: 0x17002D47 RID: 11591
		// (get) Token: 0x060111A3 RID: 70051 RVA: 0x003AEF4E File Offset: 0x003AD14E
		// (set) Token: 0x060111A4 RID: 70052 RVA: 0x003AEF56 File Offset: 0x003AD156
		public long? DictionaryPageOffset { get; set; }

		// Token: 0x17002D48 RID: 11592
		// (get) Token: 0x060111A5 RID: 70053 RVA: 0x003AEF5F File Offset: 0x003AD15F
		// (set) Token: 0x060111A6 RID: 70054 RVA: 0x003AEF67 File Offset: 0x003AD167
		public Statistics Statistics { get; set; }

		// Token: 0x17002D49 RID: 11593
		// (get) Token: 0x060111A7 RID: 70055 RVA: 0x003AEF70 File Offset: 0x003AD170
		// (set) Token: 0x060111A8 RID: 70056 RVA: 0x003AEF78 File Offset: 0x003AD178
		public PageEncodingStats[] EncodingStats { get; set; }

		// Token: 0x17002D4A RID: 11594
		// (get) Token: 0x060111A9 RID: 70057 RVA: 0x003AEF81 File Offset: 0x003AD181
		// (set) Token: 0x060111AA RID: 70058 RVA: 0x003AEF89 File Offset: 0x003AD189
		public long? BloomFilterOffset { get; set; }

		// Token: 0x17002D4B RID: 11595
		// (get) Token: 0x060111AB RID: 70059 RVA: 0x003AEF92 File Offset: 0x003AD192
		// (set) Token: 0x060111AC RID: 70060 RVA: 0x003AEF9A File Offset: 0x003AD19A
		public int? BloomFilterLength { get; set; }

		// Token: 0x060111AD RID: 70061 RVA: 0x003AEFA4 File Offset: 0x003AD1A4
		public void Read(FastProtocol prot)
		{
			this.Type = null;
			this.Encodings = null;
			this.PathInSchema = null;
			this.Codec = null;
			this.NumValues = null;
			this.TotalUncompressedSize = null;
			this.TotalCompressedSize = null;
			this.KeyValueMetadata = null;
			this.DataPageOffset = null;
			this.IndexPageOffset = null;
			this.DictionaryPageOffset = null;
			this.Statistics = null;
			this.EncodingStats = null;
			this.BloomFilterOffset = null;
			this.BloomFilterLength = null;
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
							this.Type = new Type?((Type)prot.ReadI32());
							flag = false;
						}
						break;
					case 2:
						if (tfield.Type == TType.List)
						{
							TList tlist = prot.ReadListBegin();
							this.Encodings = new Encoding[tlist.Count];
							for (int i = 0; i < tlist.Count; i++)
							{
								this.Encodings[i] = (Encoding)prot.ReadI32();
							}
							prot.ReadListEnd();
							flag = false;
						}
						break;
					case 3:
						if (tfield.Type == TType.List)
						{
							TList tlist2 = prot.ReadListBegin();
							this.PathInSchema = new string[tlist2.Count];
							for (int j = 0; j < tlist2.Count; j++)
							{
								this.PathInSchema[j] = prot.ReadString();
							}
							prot.ReadListEnd();
							flag = false;
						}
						break;
					case 4:
						if (tfield.Type == TType.I32)
						{
							this.Codec = new CompressionCodec?((CompressionCodec)prot.ReadI32());
							flag = false;
						}
						break;
					case 5:
						if (tfield.Type == TType.I64)
						{
							this.NumValues = new long?(prot.ReadI64());
							flag = false;
						}
						break;
					case 6:
						if (tfield.Type == TType.I64)
						{
							this.TotalUncompressedSize = new long?(prot.ReadI64());
							flag = false;
						}
						break;
					case 7:
						if (tfield.Type == TType.I64)
						{
							this.TotalCompressedSize = new long?(prot.ReadI64());
							flag = false;
						}
						break;
					case 8:
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
					case 9:
						if (tfield.Type == TType.I64)
						{
							this.DataPageOffset = new long?(prot.ReadI64());
							flag = false;
						}
						break;
					case 10:
						if (tfield.Type == TType.I64)
						{
							this.IndexPageOffset = new long?(prot.ReadI64());
							flag = false;
						}
						break;
					case 11:
						if (tfield.Type == TType.I64)
						{
							this.DictionaryPageOffset = new long?(prot.ReadI64());
							flag = false;
						}
						break;
					case 12:
						if (tfield.Type == TType.Struct)
						{
							Statistics statistics = new Statistics();
							statistics.Read(prot);
							this.Statistics = statistics;
							flag = false;
						}
						break;
					case 13:
						if (tfield.Type == TType.List)
						{
							TList tlist4 = prot.ReadListBegin();
							this.EncodingStats = new PageEncodingStats[tlist4.Count];
							for (int l = 0; l < tlist4.Count; l++)
							{
								PageEncodingStats pageEncodingStats = new PageEncodingStats();
								pageEncodingStats.Read(prot);
								this.EncodingStats[l] = pageEncodingStats;
							}
							prot.ReadListEnd();
							flag = false;
						}
						break;
					case 14:
						if (tfield.Type == TType.I64)
						{
							this.BloomFilterOffset = new long?(prot.ReadI64());
							flag = false;
						}
						break;
					case 15:
						if (tfield.Type == TType.I32)
						{
							this.BloomFilterLength = new int?(prot.ReadI32());
							flag = false;
						}
						break;
					}
					prot.EndField(tfield.Type, flag);
				}
				prot.ReadStructEnd();
				if (this.Type == null || this.Encodings == null || this.PathInSchema == null || this.Codec == null || this.NumValues == null || this.TotalUncompressedSize == null || this.TotalCompressedSize == null || this.DataPageOffset == null)
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
