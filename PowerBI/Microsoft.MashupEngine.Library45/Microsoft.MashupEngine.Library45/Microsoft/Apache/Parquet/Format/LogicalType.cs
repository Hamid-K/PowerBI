using System;
using Microsoft.Apache.Thrift;

namespace Microsoft.Apache.Parquet.Format
{
	// Token: 0x02002018 RID: 8216
	internal class LogicalType
	{
		// Token: 0x17002D72 RID: 11634
		// (get) Token: 0x0601121D RID: 70173 RVA: 0x003B085C File Offset: 0x003AEA5C
		// (set) Token: 0x0601121E RID: 70174 RVA: 0x003B0864 File Offset: 0x003AEA64
		public StringType STRING { get; set; }

		// Token: 0x17002D73 RID: 11635
		// (get) Token: 0x0601121F RID: 70175 RVA: 0x003B086D File Offset: 0x003AEA6D
		// (set) Token: 0x06011220 RID: 70176 RVA: 0x003B0875 File Offset: 0x003AEA75
		public MapType MAP { get; set; }

		// Token: 0x17002D74 RID: 11636
		// (get) Token: 0x06011221 RID: 70177 RVA: 0x003B087E File Offset: 0x003AEA7E
		// (set) Token: 0x06011222 RID: 70178 RVA: 0x003B0886 File Offset: 0x003AEA86
		public ListType LIST { get; set; }

		// Token: 0x17002D75 RID: 11637
		// (get) Token: 0x06011223 RID: 70179 RVA: 0x003B088F File Offset: 0x003AEA8F
		// (set) Token: 0x06011224 RID: 70180 RVA: 0x003B0897 File Offset: 0x003AEA97
		public EnumType ENUM { get; set; }

		// Token: 0x17002D76 RID: 11638
		// (get) Token: 0x06011225 RID: 70181 RVA: 0x003B08A0 File Offset: 0x003AEAA0
		// (set) Token: 0x06011226 RID: 70182 RVA: 0x003B08A8 File Offset: 0x003AEAA8
		public DecimalType DECIMAL { get; set; }

		// Token: 0x17002D77 RID: 11639
		// (get) Token: 0x06011227 RID: 70183 RVA: 0x003B08B1 File Offset: 0x003AEAB1
		// (set) Token: 0x06011228 RID: 70184 RVA: 0x003B08B9 File Offset: 0x003AEAB9
		public DateType DATE { get; set; }

		// Token: 0x17002D78 RID: 11640
		// (get) Token: 0x06011229 RID: 70185 RVA: 0x003B08C2 File Offset: 0x003AEAC2
		// (set) Token: 0x0601122A RID: 70186 RVA: 0x003B08CA File Offset: 0x003AEACA
		public TimeType TIME { get; set; }

		// Token: 0x17002D79 RID: 11641
		// (get) Token: 0x0601122B RID: 70187 RVA: 0x003B08D3 File Offset: 0x003AEAD3
		// (set) Token: 0x0601122C RID: 70188 RVA: 0x003B08DB File Offset: 0x003AEADB
		public TimestampType TIMESTAMP { get; set; }

		// Token: 0x17002D7A RID: 11642
		// (get) Token: 0x0601122D RID: 70189 RVA: 0x003B08E4 File Offset: 0x003AEAE4
		// (set) Token: 0x0601122E RID: 70190 RVA: 0x003B08EC File Offset: 0x003AEAEC
		public IntType INTEGER { get; set; }

		// Token: 0x17002D7B RID: 11643
		// (get) Token: 0x0601122F RID: 70191 RVA: 0x003B08F5 File Offset: 0x003AEAF5
		// (set) Token: 0x06011230 RID: 70192 RVA: 0x003B08FD File Offset: 0x003AEAFD
		public NullType UNKNOWN { get; set; }

		// Token: 0x17002D7C RID: 11644
		// (get) Token: 0x06011231 RID: 70193 RVA: 0x003B0906 File Offset: 0x003AEB06
		// (set) Token: 0x06011232 RID: 70194 RVA: 0x003B090E File Offset: 0x003AEB0E
		public JsonType JSON { get; set; }

		// Token: 0x17002D7D RID: 11645
		// (get) Token: 0x06011233 RID: 70195 RVA: 0x003B0917 File Offset: 0x003AEB17
		// (set) Token: 0x06011234 RID: 70196 RVA: 0x003B091F File Offset: 0x003AEB1F
		public BsonType BSON { get; set; }

		// Token: 0x17002D7E RID: 11646
		// (get) Token: 0x06011235 RID: 70197 RVA: 0x003B0928 File Offset: 0x003AEB28
		// (set) Token: 0x06011236 RID: 70198 RVA: 0x003B0930 File Offset: 0x003AEB30
		public UUIDType UUID { get; set; }

		// Token: 0x06011237 RID: 70199 RVA: 0x003B093C File Offset: 0x003AEB3C
		public void Read(FastProtocol prot)
		{
			this.STRING = null;
			this.MAP = null;
			this.LIST = null;
			this.ENUM = null;
			this.DECIMAL = null;
			this.DATE = null;
			this.TIME = null;
			this.TIMESTAMP = null;
			this.INTEGER = null;
			this.UNKNOWN = null;
			this.JSON = null;
			this.BSON = null;
			this.UUID = null;
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
						if (tfield.Type == TType.Struct)
						{
							StringType stringType = new StringType();
							stringType.Read(prot);
							this.STRING = stringType;
							flag = false;
						}
						break;
					case 2:
						if (tfield.Type == TType.Struct)
						{
							MapType mapType = new MapType();
							mapType.Read(prot);
							this.MAP = mapType;
							flag = false;
						}
						break;
					case 3:
						if (tfield.Type == TType.Struct)
						{
							ListType listType = new ListType();
							listType.Read(prot);
							this.LIST = listType;
							flag = false;
						}
						break;
					case 4:
						if (tfield.Type == TType.Struct)
						{
							EnumType enumType = new EnumType();
							enumType.Read(prot);
							this.ENUM = enumType;
							flag = false;
						}
						break;
					case 5:
						if (tfield.Type == TType.Struct)
						{
							DecimalType decimalType = new DecimalType();
							decimalType.Read(prot);
							this.DECIMAL = decimalType;
							flag = false;
						}
						break;
					case 6:
						if (tfield.Type == TType.Struct)
						{
							DateType dateType = new DateType();
							dateType.Read(prot);
							this.DATE = dateType;
							flag = false;
						}
						break;
					case 7:
						if (tfield.Type == TType.Struct)
						{
							TimeType timeType = new TimeType();
							timeType.Read(prot);
							this.TIME = timeType;
							flag = false;
						}
						break;
					case 8:
						if (tfield.Type == TType.Struct)
						{
							TimestampType timestampType = new TimestampType();
							timestampType.Read(prot);
							this.TIMESTAMP = timestampType;
							flag = false;
						}
						break;
					case 10:
						if (tfield.Type == TType.Struct)
						{
							IntType intType = new IntType();
							intType.Read(prot);
							this.INTEGER = intType;
							flag = false;
						}
						break;
					case 11:
						if (tfield.Type == TType.Struct)
						{
							NullType nullType = new NullType();
							nullType.Read(prot);
							this.UNKNOWN = nullType;
							flag = false;
						}
						break;
					case 12:
						if (tfield.Type == TType.Struct)
						{
							JsonType jsonType = new JsonType();
							jsonType.Read(prot);
							this.JSON = jsonType;
							flag = false;
						}
						break;
					case 13:
						if (tfield.Type == TType.Struct)
						{
							BsonType bsonType = new BsonType();
							bsonType.Read(prot);
							this.BSON = bsonType;
							flag = false;
						}
						break;
					case 14:
						if (tfield.Type == TType.Struct)
						{
							UUIDType uuidtype = new UUIDType();
							uuidtype.Read(prot);
							this.UUID = uuidtype;
							flag = false;
						}
						break;
					}
					prot.EndField(tfield.Type, flag);
				}
				prot.ReadStructEnd();
			}
			finally
			{
				prot.DecrementRecursionDepth();
			}
		}
	}
}
