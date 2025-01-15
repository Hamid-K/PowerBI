using System;
using System.Reflection;

namespace Microsoft.Mashup.Engine1.Library.MQ
{
	// Token: 0x02000952 RID: 2386
	public class ReceiveOptions
	{
		// Token: 0x06004431 RID: 17457 RVA: 0x000E5B0D File Offset: 0x000E3D0D
		public ReceiveOptions()
		{
			this.RealValue = ReceiveOptions.Constructor.Invoke(AssemblyLoader.EmptyArray);
		}

		// Token: 0x170015C7 RID: 5575
		// (get) Token: 0x06004432 RID: 17458 RVA: 0x000E5B2A File Offset: 0x000E3D2A
		// (set) Token: 0x06004433 RID: 17459 RVA: 0x000E5B32 File Offset: 0x000E3D32
		public object RealValue { get; protected set; }

		// Token: 0x170015C8 RID: 5576
		// (get) Token: 0x06004434 RID: 17460 RVA: 0x000E5B3B File Offset: 0x000E3D3B
		// (set) Token: 0x06004435 RID: 17461 RVA: 0x000E5B42 File Offset: 0x000E3D42
		public static Type RealType { get; protected set; } = AssemblyLoader.HisConnectors.GetType("Microsoft.HostIntegration.MqClient.ReceiveOptions");

		// Token: 0x170015C9 RID: 5577
		// (get) Token: 0x06004436 RID: 17462 RVA: 0x000E5B4A File Offset: 0x000E3D4A
		// (set) Token: 0x06004437 RID: 17463 RVA: 0x000E5B62 File Offset: 0x000E3D62
		public ReceiveOption Options
		{
			get
			{
				return (ReceiveOption)ReceiveOptions.OptionsInfo.GetValue(this.RealValue, null);
			}
			internal set
			{
				ReceiveOptions.OptionsInfo.SetValue(this.RealValue, value, null);
			}
		}

		// Token: 0x170015CA RID: 5578
		// (get) Token: 0x06004438 RID: 17464 RVA: 0x000E5B7B File Offset: 0x000E3D7B
		// (set) Token: 0x06004439 RID: 17465 RVA: 0x000E5B93 File Offset: 0x000E3D93
		public int Timeout
		{
			get
			{
				return (int)ReceiveOptions.TimeoutInfo.GetValue(this.RealValue, null);
			}
			set
			{
				ReceiveOptions.TimeoutInfo.SetValue(this.RealValue, value, null);
			}
		}

		// Token: 0x170015CB RID: 5579
		// (get) Token: 0x0600443A RID: 17466 RVA: 0x000E5BAC File Offset: 0x000E3DAC
		// (set) Token: 0x0600443B RID: 17467 RVA: 0x000E5BC4 File Offset: 0x000E3DC4
		public bool Wait
		{
			get
			{
				return (bool)ReceiveOptions.WaitInfo.GetValue(this.RealValue, null);
			}
			set
			{
				ReceiveOptions.WaitInfo.SetValue(this.RealValue, value, null);
			}
		}

		// Token: 0x170015CC RID: 5580
		// (get) Token: 0x0600443C RID: 17468 RVA: 0x000E5BDD File Offset: 0x000E3DDD
		// (set) Token: 0x0600443D RID: 17469 RVA: 0x000E5BF5 File Offset: 0x000E3DF5
		public byte[] MessageId
		{
			get
			{
				return (byte[])ReceiveOptions.MessageIdInfo.GetValue(this.RealValue, null);
			}
			set
			{
				ReceiveOptions.MessageIdInfo.SetValue(this.RealValue, value, null);
			}
		}

		// Token: 0x170015CD RID: 5581
		// (get) Token: 0x0600443E RID: 17470 RVA: 0x000E5C09 File Offset: 0x000E3E09
		// (set) Token: 0x0600443F RID: 17471 RVA: 0x000E5C21 File Offset: 0x000E3E21
		public byte[] Correlator
		{
			get
			{
				return (byte[])ReceiveOptions.CorrelatorInfo.GetValue(this.RealValue, null);
			}
			set
			{
				ReceiveOptions.CorrelatorInfo.SetValue(this.RealValue, value, null);
			}
		}

		// Token: 0x170015CE RID: 5582
		// (get) Token: 0x06004440 RID: 17472 RVA: 0x000E5C35 File Offset: 0x000E3E35
		// (set) Token: 0x06004441 RID: 17473 RVA: 0x000E5C4D File Offset: 0x000E3E4D
		public MatchOption MatchOptions
		{
			get
			{
				return (MatchOption)ReceiveOptions.MatchOptionsInfo.GetValue(this.RealValue, null);
			}
			internal set
			{
				ReceiveOptions.MatchOptionsInfo.SetValue(this.RealValue, value, null);
			}
		}

		// Token: 0x170015CF RID: 5583
		// (get) Token: 0x06004442 RID: 17474 RVA: 0x000E5C66 File Offset: 0x000E3E66
		// (set) Token: 0x06004443 RID: 17475 RVA: 0x000E5C7E File Offset: 0x000E3E7E
		public int TruncationSize
		{
			get
			{
				return (int)ReceiveOptions.TruncationSizeInfo.GetValue(this.RealValue, null);
			}
			set
			{
				ReceiveOptions.TruncationSizeInfo.SetValue(this.RealValue, value, null);
			}
		}

		// Token: 0x170015D0 RID: 5584
		// (get) Token: 0x06004444 RID: 17476 RVA: 0x000E5C97 File Offset: 0x000E3E97
		// (set) Token: 0x06004445 RID: 17477 RVA: 0x000E5CAF File Offset: 0x000E3EAF
		public byte[] GroupId
		{
			get
			{
				return (byte[])ReceiveOptions.GroupIdInfo.GetValue(this.RealValue, null);
			}
			set
			{
				ReceiveOptions.GroupIdInfo.SetValue(this.RealValue, value, null);
			}
		}

		// Token: 0x170015D1 RID: 5585
		// (get) Token: 0x06004446 RID: 17478 RVA: 0x000E5CC3 File Offset: 0x000E3EC3
		// (set) Token: 0x06004447 RID: 17479 RVA: 0x000E5CDB File Offset: 0x000E3EDB
		public int Offset
		{
			get
			{
				return (int)ReceiveOptions.OffsetInfo.GetValue(this.RealValue, null);
			}
			set
			{
				ReceiveOptions.OffsetInfo.SetValue(this.RealValue, value, null);
			}
		}

		// Token: 0x170015D2 RID: 5586
		// (get) Token: 0x06004448 RID: 17480 RVA: 0x000E5CF4 File Offset: 0x000E3EF4
		// (set) Token: 0x06004449 RID: 17481 RVA: 0x000E5D0C File Offset: 0x000E3F0C
		public bool SegmentationAllowed
		{
			get
			{
				return (bool)ReceiveOptions.SegmentationAllowedInfo.GetValue(this.RealValue, null);
			}
			set
			{
				ReceiveOptions.SegmentationAllowedInfo.SetValue(this.RealValue, value, null);
			}
		}

		// Token: 0x170015D3 RID: 5587
		// (get) Token: 0x0600444A RID: 17482 RVA: 0x000E5D25 File Offset: 0x000E3F25
		// (set) Token: 0x0600444B RID: 17483 RVA: 0x000E5D3D File Offset: 0x000E3F3D
		public int SequenceNumber
		{
			get
			{
				return (int)ReceiveOptions.SequenceNumberInfo.GetValue(this.RealValue, null);
			}
			set
			{
				ReceiveOptions.SequenceNumberInfo.SetValue(this.RealValue, value, null);
			}
		}

		// Token: 0x170015D4 RID: 5588
		// (get) Token: 0x0600444C RID: 17484 RVA: 0x000E5D56 File Offset: 0x000E3F56
		// (set) Token: 0x0600444D RID: 17485 RVA: 0x000E5D6E File Offset: 0x000E3F6E
		public byte[] Token
		{
			get
			{
				return (byte[])ReceiveOptions.TokenInfo.GetValue(this.RealValue, null);
			}
			set
			{
				ReceiveOptions.TokenInfo.SetValue(this.RealValue, value, null);
			}
		}

		// Token: 0x0400241C RID: 9244
		private static readonly ConstructorInfo Constructor = ReceiveOptions.RealType.GetConstructor(Type.EmptyTypes);

		// Token: 0x0400241D RID: 9245
		private static readonly PropertyInfo OptionsInfo = ReceiveOptions.RealType.GetProperty("Options");

		// Token: 0x0400241E RID: 9246
		private static readonly PropertyInfo TimeoutInfo = ReceiveOptions.RealType.GetProperty("Timeout");

		// Token: 0x0400241F RID: 9247
		private static readonly PropertyInfo WaitInfo = ReceiveOptions.RealType.GetProperty("Wait");

		// Token: 0x04002420 RID: 9248
		private static readonly PropertyInfo MatchOptionsInfo = ReceiveOptions.RealType.GetProperty("MatchOptions");

		// Token: 0x04002421 RID: 9249
		private static readonly PropertyInfo CorrelatorInfo = ReceiveOptions.RealType.GetProperty("Correlator");

		// Token: 0x04002422 RID: 9250
		private static readonly PropertyInfo MessageIdInfo = ReceiveOptions.RealType.GetProperty("MessageId");

		// Token: 0x04002423 RID: 9251
		private static readonly PropertyInfo TruncationSizeInfo = ReceiveOptions.RealType.GetProperty("TruncationSize");

		// Token: 0x04002424 RID: 9252
		private static readonly PropertyInfo GroupIdInfo = ReceiveOptions.RealType.GetProperty("GroupId");

		// Token: 0x04002425 RID: 9253
		private static readonly PropertyInfo OffsetInfo = ReceiveOptions.RealType.GetProperty("Offset");

		// Token: 0x04002426 RID: 9254
		private static readonly PropertyInfo SegmentationAllowedInfo = ReceiveOptions.RealType.GetProperty("SegmentationAllowed");

		// Token: 0x04002427 RID: 9255
		private static readonly PropertyInfo SequenceNumberInfo = ReceiveOptions.RealType.GetProperty("SequenceNumber");

		// Token: 0x04002428 RID: 9256
		private static readonly PropertyInfo TokenInfo = ReceiveOptions.RealType.GetProperty("Token");
	}
}
