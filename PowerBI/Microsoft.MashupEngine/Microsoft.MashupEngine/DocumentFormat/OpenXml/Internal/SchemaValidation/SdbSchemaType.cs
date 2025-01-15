using System;

namespace DocumentFormat.OpenXml.Internal.SchemaValidation
{
	// Token: 0x0200310A RID: 12554
	internal class SdbSchemaType : SdbData
	{
		// Token: 0x170098A9 RID: 39081
		// (get) Token: 0x0601B393 RID: 111507 RVA: 0x0037312D File Offset: 0x0037132D
		// (set) Token: 0x0601B394 RID: 111508 RVA: 0x00373135 File Offset: 0x00371335
		public ushort ParticleIndex { get; set; }

		// Token: 0x170098AA RID: 39082
		// (get) Token: 0x0601B395 RID: 111509 RVA: 0x0037313E File Offset: 0x0037133E
		// (set) Token: 0x0601B396 RID: 111510 RVA: 0x00373146 File Offset: 0x00371346
		public ushort SimpleTypeIndex { get; set; }

		// Token: 0x170098AB RID: 39083
		// (get) Token: 0x0601B397 RID: 111511 RVA: 0x0037314F File Offset: 0x0037134F
		// (set) Token: 0x0601B398 RID: 111512 RVA: 0x00373157 File Offset: 0x00371357
		public ushort AttributesCount { get; set; }

		// Token: 0x170098AC RID: 39084
		// (get) Token: 0x0601B399 RID: 111513 RVA: 0x00373160 File Offset: 0x00371360
		// (set) Token: 0x0601B39A RID: 111514 RVA: 0x00373168 File Offset: 0x00371368
		public ushort StartIndexOfAttributes { get; set; }

		// Token: 0x0601B39B RID: 111515 RVA: 0x0036FD3E File Offset: 0x0036DF3E
		public SdbSchemaType()
		{
		}

		// Token: 0x0601B39C RID: 111516 RVA: 0x00373171 File Offset: 0x00371371
		public SdbSchemaType(ushort particleIndex, ushort simpleTypeIndex, ushort attributeCount, ushort startAttribute)
		{
			this.ParticleIndex = particleIndex;
			this.SimpleTypeIndex = simpleTypeIndex;
			this.AttributesCount = attributeCount;
			this.StartIndexOfAttributes = startAttribute;
		}

		// Token: 0x170098AD RID: 39085
		// (get) Token: 0x0601B39D RID: 111517 RVA: 0x00373196 File Offset: 0x00371396
		public bool IsCompositeType
		{
			get
			{
				return this.ParticleIndex != ushort.MaxValue;
			}
		}

		// Token: 0x170098AE RID: 39086
		// (get) Token: 0x0601B39E RID: 111518 RVA: 0x003731A8 File Offset: 0x003713A8
		public bool IsSimpleContent
		{
			get
			{
				return this.SimpleTypeIndex != ushort.MaxValue;
			}
		}

		// Token: 0x170098AF RID: 39087
		// (get) Token: 0x0601B39F RID: 111519 RVA: 0x000024ED File Offset: 0x000006ED
		public static int TypeSize
		{
			get
			{
				return 8;
			}
		}

		// Token: 0x170098B0 RID: 39088
		// (get) Token: 0x0601B3A0 RID: 111520 RVA: 0x003731BA File Offset: 0x003713BA
		public override int DataSize
		{
			get
			{
				return SdbSchemaType.TypeSize;
			}
		}

		// Token: 0x0601B3A1 RID: 111521 RVA: 0x003731C4 File Offset: 0x003713C4
		public override byte[] GetBytes()
		{
			return base.GetBytes(new byte[][]
			{
				this.ParticleIndex.Bytes(),
				this.SimpleTypeIndex.Bytes(),
				this.AttributesCount.Bytes(),
				this.StartIndexOfAttributes.Bytes()
			});
		}

		// Token: 0x0601B3A2 RID: 111522 RVA: 0x00373217 File Offset: 0x00371417
		public override void LoadFromBytes(byte[] value, int startIndex)
		{
			this.ParticleIndex = SdbData.LoadSdbIndex(value, ref startIndex);
			this.SimpleTypeIndex = SdbData.LoadSdbIndex(value, ref startIndex);
			this.AttributesCount = SdbData.LoadSdbIndex(value, ref startIndex);
			this.StartIndexOfAttributes = SdbData.LoadSdbIndex(value, ref startIndex);
		}
	}
}
