using System;
using System.Diagnostics;

namespace DocumentFormat.OpenXml.Internal.SchemaValidation
{
	// Token: 0x0200310C RID: 12556
	[DebuggerDisplay("ParticleIndex={ParticleIndex}")]
	internal class SdbParticleChildrenIndex : SdbData
	{
		// Token: 0x170098BA RID: 39098
		// (get) Token: 0x0601B3B5 RID: 111541 RVA: 0x00373399 File Offset: 0x00371599
		// (set) Token: 0x0601B3B6 RID: 111542 RVA: 0x003733A1 File Offset: 0x003715A1
		public ushort ParticleIndex { get; set; }

		// Token: 0x0601B3B7 RID: 111543 RVA: 0x003733AA File Offset: 0x003715AA
		public SdbParticleChildrenIndex()
		{
			this.ParticleIndex = ushort.MaxValue;
		}

		// Token: 0x0601B3B8 RID: 111544 RVA: 0x003733BD File Offset: 0x003715BD
		public SdbParticleChildrenIndex(ushort index)
		{
			this.ParticleIndex = index;
		}

		// Token: 0x0601B3B9 RID: 111545 RVA: 0x003733CC File Offset: 0x003715CC
		public SdbParticleChildrenIndex(int index)
		{
			if (index >= 65535)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			this.ParticleIndex = (ushort)index;
		}

		// Token: 0x170098BB RID: 39099
		// (get) Token: 0x0601B3BA RID: 111546 RVA: 0x000023C4 File Offset: 0x000005C4
		public static int TypeSize
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x170098BC RID: 39100
		// (get) Token: 0x0601B3BB RID: 111547 RVA: 0x003733EF File Offset: 0x003715EF
		public override int DataSize
		{
			get
			{
				return SdbParticleChildrenIndex.TypeSize;
			}
		}

		// Token: 0x0601B3BC RID: 111548 RVA: 0x003733F6 File Offset: 0x003715F6
		public override byte[] GetBytes()
		{
			return this.ParticleIndex.Bytes();
		}

		// Token: 0x0601B3BD RID: 111549 RVA: 0x00373403 File Offset: 0x00371603
		public override void LoadFromBytes(byte[] value, int startIndex)
		{
			this.ParticleIndex = SdbData.LoadSdbIndex(value, ref startIndex);
		}
	}
}
