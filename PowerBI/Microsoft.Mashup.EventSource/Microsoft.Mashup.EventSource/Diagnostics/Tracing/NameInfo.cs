using System;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x02000038 RID: 56
	internal sealed class NameInfo : ConcurrentSetItem<KeyValuePair<string, EventTags>, NameInfo>
	{
		// Token: 0x060001C5 RID: 453 RVA: 0x0000BFCC File Offset: 0x0000A1CC
		internal static void ReserveEventIDsBelow(int eventId)
		{
			int num;
			int num2;
			do
			{
				num = NameInfo.lastIdentity;
				num2 = (NameInfo.lastIdentity & -16777216) + eventId;
				num2 = Math.Max(num2, num);
			}
			while (Interlocked.CompareExchange(ref NameInfo.lastIdentity, num2, num) != num);
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000C004 File Offset: 0x0000A204
		public NameInfo(string name, EventTags tags, int typeMetadataSize)
		{
			this.name = name;
			this.tags = tags & (EventTags)268435455;
			this.identity = Interlocked.Increment(ref NameInfo.lastIdentity);
			int num = 0;
			Statics.EncodeTags((int)this.tags, ref num, null);
			this.nameMetadata = Statics.MetadataForString(name, num, 0, typeMetadataSize);
			num = 2;
			Statics.EncodeTags((int)this.tags, ref num, this.nameMetadata);
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0000C06F File Offset: 0x0000A26F
		public override int Compare(NameInfo other)
		{
			return this.Compare(other.name, other.tags);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000C083 File Offset: 0x0000A283
		public override int Compare(KeyValuePair<string, EventTags> key)
		{
			return this.Compare(key.Key, key.Value & (EventTags)268435455);
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000C0A0 File Offset: 0x0000A2A0
		private int Compare(string otherName, EventTags otherTags)
		{
			int num = StringComparer.Ordinal.Compare(this.name, otherName);
			if (num == 0 && this.tags != otherTags)
			{
				num = ((this.tags < otherTags) ? (-1) : 1);
			}
			return num;
		}

		// Token: 0x040000EE RID: 238
		private static int lastIdentity = 184549376;

		// Token: 0x040000EF RID: 239
		internal readonly string name;

		// Token: 0x040000F0 RID: 240
		internal readonly EventTags tags;

		// Token: 0x040000F1 RID: 241
		internal readonly int identity;

		// Token: 0x040000F2 RID: 242
		internal readonly byte[] nameMetadata;
	}
}
