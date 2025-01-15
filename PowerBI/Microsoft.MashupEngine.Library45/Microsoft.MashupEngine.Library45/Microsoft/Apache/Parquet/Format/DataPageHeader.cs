using System;
using Microsoft.Apache.Thrift;

namespace Microsoft.Apache.Parquet.Format
{
	// Token: 0x02002006 RID: 8198
	internal class DataPageHeader
	{
		// Token: 0x17002D4D RID: 11597
		// (get) Token: 0x060111B3 RID: 70067 RVA: 0x003AF55C File Offset: 0x003AD75C
		// (set) Token: 0x060111B4 RID: 70068 RVA: 0x003AF564 File Offset: 0x003AD764
		public int? NumValues { get; set; }

		// Token: 0x17002D4E RID: 11598
		// (get) Token: 0x060111B5 RID: 70069 RVA: 0x003AF56D File Offset: 0x003AD76D
		// (set) Token: 0x060111B6 RID: 70070 RVA: 0x003AF575 File Offset: 0x003AD775
		public Encoding? Encoding { get; set; }

		// Token: 0x17002D4F RID: 11599
		// (get) Token: 0x060111B7 RID: 70071 RVA: 0x003AF57E File Offset: 0x003AD77E
		// (set) Token: 0x060111B8 RID: 70072 RVA: 0x003AF586 File Offset: 0x003AD786
		public Encoding? DefinitionLevelEncoding { get; set; }

		// Token: 0x17002D50 RID: 11600
		// (get) Token: 0x060111B9 RID: 70073 RVA: 0x003AF58F File Offset: 0x003AD78F
		// (set) Token: 0x060111BA RID: 70074 RVA: 0x003AF597 File Offset: 0x003AD797
		public Encoding? RepetitionLevelEncoding { get; set; }

		// Token: 0x17002D51 RID: 11601
		// (get) Token: 0x060111BB RID: 70075 RVA: 0x003AF5A0 File Offset: 0x003AD7A0
		// (set) Token: 0x060111BC RID: 70076 RVA: 0x003AF5A8 File Offset: 0x003AD7A8
		public Statistics Statistics { get; set; }

		// Token: 0x060111BD RID: 70077 RVA: 0x003AF5B4 File Offset: 0x003AD7B4
		public void Read(FastProtocol prot)
		{
			this.NumValues = null;
			this.Encoding = null;
			this.DefinitionLevelEncoding = null;
			this.RepetitionLevelEncoding = null;
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
							this.Encoding = new Encoding?((Encoding)prot.ReadI32());
							flag = false;
						}
						break;
					case 3:
						if (tfield.Type == TType.I32)
						{
							this.DefinitionLevelEncoding = new Encoding?((Encoding)prot.ReadI32());
							flag = false;
						}
						break;
					case 4:
						if (tfield.Type == TType.I32)
						{
							this.RepetitionLevelEncoding = new Encoding?((Encoding)prot.ReadI32());
							flag = false;
						}
						break;
					case 5:
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
				if (this.NumValues == null || this.Encoding == null || this.DefinitionLevelEncoding == null || this.RepetitionLevelEncoding == null)
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
