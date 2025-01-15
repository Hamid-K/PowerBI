using System;
using System.Diagnostics;

namespace DocumentFormat.OpenXml.Internal.SchemaValidation
{
	// Token: 0x0200310B RID: 12555
	[DebuggerDisplay("ParticleType={ParticleType}")]
	internal class SdbParticleConstraint : SdbData
	{
		// Token: 0x170098B1 RID: 39089
		// (get) Token: 0x0601B3A3 RID: 111523 RVA: 0x00373251 File Offset: 0x00371451
		// (set) Token: 0x0601B3A4 RID: 111524 RVA: 0x00373259 File Offset: 0x00371459
		public ParticleType ParticleType { get; set; }

		// Token: 0x170098B2 RID: 39090
		// (get) Token: 0x0601B3A5 RID: 111525 RVA: 0x00373262 File Offset: 0x00371462
		// (set) Token: 0x0601B3A6 RID: 111526 RVA: 0x0037326A File Offset: 0x0037146A
		public ushort ElementTypeId { get; set; }

		// Token: 0x170098B3 RID: 39091
		// (get) Token: 0x0601B3A7 RID: 111527 RVA: 0x00373273 File Offset: 0x00371473
		// (set) Token: 0x0601B3A8 RID: 111528 RVA: 0x0037327B File Offset: 0x0037147B
		public ushort MinOccurs { get; set; }

		// Token: 0x170098B4 RID: 39092
		// (get) Token: 0x0601B3A9 RID: 111529 RVA: 0x00373284 File Offset: 0x00371484
		// (set) Token: 0x0601B3AA RID: 111530 RVA: 0x0037328C File Offset: 0x0037148C
		public int MaxOccurs { get; set; }

		// Token: 0x170098B5 RID: 39093
		// (get) Token: 0x0601B3AB RID: 111531 RVA: 0x00373295 File Offset: 0x00371495
		// (set) Token: 0x0601B3AC RID: 111532 RVA: 0x0037329D File Offset: 0x0037149D
		public ushort ChildrenCount { get; set; }

		// Token: 0x170098B6 RID: 39094
		// (get) Token: 0x0601B3AD RID: 111533 RVA: 0x003732A6 File Offset: 0x003714A6
		// (set) Token: 0x0601B3AE RID: 111534 RVA: 0x003732AE File Offset: 0x003714AE
		public ushort ChildrenStartIndex { get; set; }

		// Token: 0x170098B7 RID: 39095
		// (get) Token: 0x0601B3AF RID: 111535 RVA: 0x003732B7 File Offset: 0x003714B7
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public ushort XsdAnyNamespaceId
		{
			get
			{
				return this.ElementTypeId;
			}
		}

		// Token: 0x170098B8 RID: 39096
		// (get) Token: 0x0601B3B0 RID: 111536 RVA: 0x00140DB6 File Offset: 0x0013EFB6
		public static int TypeSize
		{
			get
			{
				return 13;
			}
		}

		// Token: 0x170098B9 RID: 39097
		// (get) Token: 0x0601B3B1 RID: 111537 RVA: 0x003732BF File Offset: 0x003714BF
		public override int DataSize
		{
			get
			{
				return SdbParticleConstraint.TypeSize;
			}
		}

		// Token: 0x0601B3B2 RID: 111538 RVA: 0x003732C8 File Offset: 0x003714C8
		public override byte[] GetBytes()
		{
			return base.GetBytes(new byte[][]
			{
				this.ParticleType.Bytes(),
				this.ElementTypeId.Bytes(),
				this.MinOccurs.Bytes(),
				this.MaxOccurs.Bytes(),
				this.ChildrenCount.Bytes(),
				this.ChildrenStartIndex.Bytes()
			});
		}

		// Token: 0x0601B3B3 RID: 111539 RVA: 0x00373338 File Offset: 0x00371538
		public override void LoadFromBytes(byte[] value, int startIndex)
		{
			this.ParticleType = (ParticleType)SdbData.LoadByte(value, ref startIndex);
			this.ElementTypeId = SdbData.LoadSdbIndex(value, ref startIndex);
			this.MinOccurs = SdbData.LoadUInt16(value, ref startIndex);
			this.MaxOccurs = SdbData.LoadInt(value, ref startIndex);
			this.ChildrenCount = SdbData.LoadSdbIndex(value, ref startIndex);
			this.ChildrenStartIndex = SdbData.LoadSdbIndex(value, ref startIndex);
		}

		// Token: 0x0400B4A5 RID: 46245
		public const ushort UnboundedMaxOccurs = 0;
	}
}
