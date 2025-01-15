using System;
using System.Diagnostics;

namespace DocumentFormat.OpenXml.Internal.SchemaValidation
{
	// Token: 0x02003109 RID: 12553
	[DebuggerDisplay("ClassId={ClassId}")]
	internal class SdbClassIdToSchemaTypeIndex : SdbData
	{
		// Token: 0x170098A5 RID: 39077
		// (get) Token: 0x0601B388 RID: 111496 RVA: 0x0037308C File Offset: 0x0037128C
		// (set) Token: 0x0601B389 RID: 111497 RVA: 0x00373094 File Offset: 0x00371294
		public ushort ClassId { get; set; }

		// Token: 0x170098A6 RID: 39078
		// (get) Token: 0x0601B38A RID: 111498 RVA: 0x0037309D File Offset: 0x0037129D
		// (set) Token: 0x0601B38B RID: 111499 RVA: 0x003730A5 File Offset: 0x003712A5
		public ushort SchemaTypeIndex { get; set; }

		// Token: 0x0601B38C RID: 111500 RVA: 0x0036FD3E File Offset: 0x0036DF3E
		public SdbClassIdToSchemaTypeIndex()
		{
		}

		// Token: 0x0601B38D RID: 111501 RVA: 0x003730AE File Offset: 0x003712AE
		public SdbClassIdToSchemaTypeIndex(ushort classId, ushort schemaTypeIndex)
		{
			this.ClassId = classId;
			this.SchemaTypeIndex = schemaTypeIndex;
		}

		// Token: 0x0601B38E RID: 111502 RVA: 0x003730C4 File Offset: 0x003712C4
		public static ushort ArrayIndexFromClassId(ushort classId)
		{
			return classId - 10001;
		}

		// Token: 0x170098A7 RID: 39079
		// (get) Token: 0x0601B38F RID: 111503 RVA: 0x0000244F File Offset: 0x0000064F
		public static int TypeSize
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x170098A8 RID: 39080
		// (get) Token: 0x0601B390 RID: 111504 RVA: 0x003730CE File Offset: 0x003712CE
		public override int DataSize
		{
			get
			{
				return SdbClassIdToSchemaTypeIndex.TypeSize;
			}
		}

		// Token: 0x0601B391 RID: 111505 RVA: 0x003730D8 File Offset: 0x003712D8
		public override byte[] GetBytes()
		{
			return base.GetBytes(new byte[][]
			{
				this.ClassId.Bytes(),
				this.SchemaTypeIndex.Bytes()
			});
		}

		// Token: 0x0601B392 RID: 111506 RVA: 0x0037310F File Offset: 0x0037130F
		public override void LoadFromBytes(byte[] value, int startIndex)
		{
			this.ClassId = SdbData.LoadSdbIndex(value, ref startIndex);
			this.SchemaTypeIndex = SdbData.LoadSdbIndex(value, ref startIndex);
		}

		// Token: 0x0400B49D RID: 46237
		public const ushort StartClassId = 10001;

		// Token: 0x0400B49E RID: 46238
		public const ushort InvalidSchemaTypeIndex = 65535;
	}
}
