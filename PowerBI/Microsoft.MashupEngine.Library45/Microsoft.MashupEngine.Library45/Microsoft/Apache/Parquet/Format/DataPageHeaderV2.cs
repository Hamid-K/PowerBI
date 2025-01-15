using System;
using Microsoft.Apache.Thrift;

namespace Microsoft.Apache.Parquet.Format
{
	// Token: 0x02002007 RID: 8199
	internal class DataPageHeaderV2
	{
		// Token: 0x17002D52 RID: 11602
		// (get) Token: 0x060111BF RID: 70079 RVA: 0x003AF780 File Offset: 0x003AD980
		// (set) Token: 0x060111C0 RID: 70080 RVA: 0x003AF788 File Offset: 0x003AD988
		public int? NumValues { get; set; }

		// Token: 0x17002D53 RID: 11603
		// (get) Token: 0x060111C1 RID: 70081 RVA: 0x003AF791 File Offset: 0x003AD991
		// (set) Token: 0x060111C2 RID: 70082 RVA: 0x003AF799 File Offset: 0x003AD999
		public int? NumNulls { get; set; }

		// Token: 0x17002D54 RID: 11604
		// (get) Token: 0x060111C3 RID: 70083 RVA: 0x003AF7A2 File Offset: 0x003AD9A2
		// (set) Token: 0x060111C4 RID: 70084 RVA: 0x003AF7AA File Offset: 0x003AD9AA
		public int? NumRows { get; set; }

		// Token: 0x17002D55 RID: 11605
		// (get) Token: 0x060111C5 RID: 70085 RVA: 0x003AF7B3 File Offset: 0x003AD9B3
		// (set) Token: 0x060111C6 RID: 70086 RVA: 0x003AF7BB File Offset: 0x003AD9BB
		public Encoding? Encoding { get; set; }

		// Token: 0x17002D56 RID: 11606
		// (get) Token: 0x060111C7 RID: 70087 RVA: 0x003AF7C4 File Offset: 0x003AD9C4
		// (set) Token: 0x060111C8 RID: 70088 RVA: 0x003AF7CC File Offset: 0x003AD9CC
		public int? DefinitionLevelsByteLength { get; set; }

		// Token: 0x17002D57 RID: 11607
		// (get) Token: 0x060111C9 RID: 70089 RVA: 0x003AF7D5 File Offset: 0x003AD9D5
		// (set) Token: 0x060111CA RID: 70090 RVA: 0x003AF7DD File Offset: 0x003AD9DD
		public int? RepetitionLevelsByteLength { get; set; }

		// Token: 0x17002D58 RID: 11608
		// (get) Token: 0x060111CB RID: 70091 RVA: 0x003AF7E6 File Offset: 0x003AD9E6
		// (set) Token: 0x060111CC RID: 70092 RVA: 0x003AF7EE File Offset: 0x003AD9EE
		public bool? IsCompressed { get; set; }

		// Token: 0x17002D59 RID: 11609
		// (get) Token: 0x060111CD RID: 70093 RVA: 0x003AF7F7 File Offset: 0x003AD9F7
		// (set) Token: 0x060111CE RID: 70094 RVA: 0x003AF7FF File Offset: 0x003AD9FF
		public Statistics Statistics { get; set; }

		// Token: 0x060111CF RID: 70095 RVA: 0x003AF808 File Offset: 0x003ADA08
		public void Read(FastProtocol prot)
		{
			this.NumValues = null;
			this.NumNulls = null;
			this.NumRows = null;
			this.Encoding = null;
			this.DefinitionLevelsByteLength = null;
			this.RepetitionLevelsByteLength = null;
			this.IsCompressed = null;
			this.Statistics = null;
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
							this.NumNulls = new int?(prot.ReadI32());
							flag = false;
						}
						break;
					case 3:
						if (tfield.Type == TType.I32)
						{
							this.NumRows = new int?(prot.ReadI32());
							flag = false;
						}
						break;
					case 4:
						if (tfield.Type == TType.I32)
						{
							this.Encoding = new Encoding?((Encoding)prot.ReadI32());
							flag = false;
						}
						break;
					case 5:
						if (tfield.Type == TType.I32)
						{
							this.DefinitionLevelsByteLength = new int?(prot.ReadI32());
							flag = false;
						}
						break;
					case 6:
						if (tfield.Type == TType.I32)
						{
							this.RepetitionLevelsByteLength = new int?(prot.ReadI32());
							flag = false;
						}
						break;
					case 7:
						if (tfield.Type == TType.Bool)
						{
							this.IsCompressed = new bool?(prot.ReadBool());
							flag = false;
						}
						break;
					case 8:
						if (tfield.Type == TType.Struct)
						{
							Statistics statistics = new Statistics();
							statistics.Read(prot);
							this.Statistics = statistics;
							flag = false;
						}
						break;
					}
					prot.EndField(tfield.Type, flag);
				}
				prot.ReadStructEnd();
				if (this.NumValues == null || this.NumNulls == null || this.NumRows == null || this.Encoding == null || this.DefinitionLevelsByteLength == null || this.RepetitionLevelsByteLength == null)
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
