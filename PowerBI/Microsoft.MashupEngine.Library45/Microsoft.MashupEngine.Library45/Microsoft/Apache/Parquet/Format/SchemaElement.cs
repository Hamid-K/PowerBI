using System;
using Microsoft.Apache.Thrift;

namespace Microsoft.Apache.Parquet.Format
{
	// Token: 0x02002024 RID: 8228
	internal class SchemaElement
	{
		// Token: 0x17002D95 RID: 11669
		// (get) Token: 0x06011279 RID: 70265 RVA: 0x003B1824 File Offset: 0x003AFA24
		// (set) Token: 0x0601127A RID: 70266 RVA: 0x003B182C File Offset: 0x003AFA2C
		public Type? Type { get; set; }

		// Token: 0x17002D96 RID: 11670
		// (get) Token: 0x0601127B RID: 70267 RVA: 0x003B1835 File Offset: 0x003AFA35
		// (set) Token: 0x0601127C RID: 70268 RVA: 0x003B183D File Offset: 0x003AFA3D
		public int? TypeLength { get; set; }

		// Token: 0x17002D97 RID: 11671
		// (get) Token: 0x0601127D RID: 70269 RVA: 0x003B1846 File Offset: 0x003AFA46
		// (set) Token: 0x0601127E RID: 70270 RVA: 0x003B184E File Offset: 0x003AFA4E
		public FieldRepetitionType? RepetitionType { get; set; }

		// Token: 0x17002D98 RID: 11672
		// (get) Token: 0x0601127F RID: 70271 RVA: 0x003B1857 File Offset: 0x003AFA57
		// (set) Token: 0x06011280 RID: 70272 RVA: 0x003B185F File Offset: 0x003AFA5F
		public string Name { get; set; }

		// Token: 0x17002D99 RID: 11673
		// (get) Token: 0x06011281 RID: 70273 RVA: 0x003B1868 File Offset: 0x003AFA68
		// (set) Token: 0x06011282 RID: 70274 RVA: 0x003B1870 File Offset: 0x003AFA70
		public int? NumChildren { get; set; }

		// Token: 0x17002D9A RID: 11674
		// (get) Token: 0x06011283 RID: 70275 RVA: 0x003B1879 File Offset: 0x003AFA79
		// (set) Token: 0x06011284 RID: 70276 RVA: 0x003B1881 File Offset: 0x003AFA81
		public ConvertedType? ConvertedType { get; set; }

		// Token: 0x17002D9B RID: 11675
		// (get) Token: 0x06011285 RID: 70277 RVA: 0x003B188A File Offset: 0x003AFA8A
		// (set) Token: 0x06011286 RID: 70278 RVA: 0x003B1892 File Offset: 0x003AFA92
		public int? Scale { get; set; }

		// Token: 0x17002D9C RID: 11676
		// (get) Token: 0x06011287 RID: 70279 RVA: 0x003B189B File Offset: 0x003AFA9B
		// (set) Token: 0x06011288 RID: 70280 RVA: 0x003B18A3 File Offset: 0x003AFAA3
		public int? Precision { get; set; }

		// Token: 0x17002D9D RID: 11677
		// (get) Token: 0x06011289 RID: 70281 RVA: 0x003B18AC File Offset: 0x003AFAAC
		// (set) Token: 0x0601128A RID: 70282 RVA: 0x003B18B4 File Offset: 0x003AFAB4
		public int? FieldId { get; set; }

		// Token: 0x17002D9E RID: 11678
		// (get) Token: 0x0601128B RID: 70283 RVA: 0x003B18BD File Offset: 0x003AFABD
		// (set) Token: 0x0601128C RID: 70284 RVA: 0x003B18C5 File Offset: 0x003AFAC5
		public LogicalType LogicalType { get; set; }

		// Token: 0x0601128D RID: 70285 RVA: 0x003B18D0 File Offset: 0x003AFAD0
		public void Read(FastProtocol prot)
		{
			this.Type = null;
			this.TypeLength = null;
			this.RepetitionType = null;
			this.Name = null;
			this.NumChildren = null;
			this.ConvertedType = null;
			this.Scale = null;
			this.Precision = null;
			this.FieldId = null;
			this.LogicalType = null;
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
						if (tfield.Type == TType.I32)
						{
							this.TypeLength = new int?(prot.ReadI32());
							flag = false;
						}
						break;
					case 3:
						if (tfield.Type == TType.I32)
						{
							this.RepetitionType = new FieldRepetitionType?((FieldRepetitionType)prot.ReadI32());
							flag = false;
						}
						break;
					case 4:
						if (tfield.Type == TType.String)
						{
							this.Name = prot.ReadString();
							flag = false;
						}
						break;
					case 5:
						if (tfield.Type == TType.I32)
						{
							this.NumChildren = new int?(prot.ReadI32());
							flag = false;
						}
						break;
					case 6:
						if (tfield.Type == TType.I32)
						{
							this.ConvertedType = new ConvertedType?((ConvertedType)prot.ReadI32());
							flag = false;
						}
						break;
					case 7:
						if (tfield.Type == TType.I32)
						{
							this.Scale = new int?(prot.ReadI32());
							flag = false;
						}
						break;
					case 8:
						if (tfield.Type == TType.I32)
						{
							this.Precision = new int?(prot.ReadI32());
							flag = false;
						}
						break;
					case 9:
						if (tfield.Type == TType.I32)
						{
							this.FieldId = new int?(prot.ReadI32());
							flag = false;
						}
						break;
					case 10:
						if (tfield.Type == TType.Struct)
						{
							LogicalType logicalType = new LogicalType();
							logicalType.Read(prot);
							this.LogicalType = logicalType;
							flag = false;
						}
						break;
					}
					prot.EndField(tfield.Type, flag);
				}
				prot.ReadStructEnd();
				if (this.Name == null)
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
