using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Diagnostics.Metrics
{
	// Token: 0x02000044 RID: 68
	[SecuritySafeCritical]
	public abstract class Instrument<T> : Instrument where T : struct
	{
		// Token: 0x06000221 RID: 545 RVA: 0x000095EC File Offset: 0x000077EC
		[NullableContext(1)]
		protected Instrument(Meter meter, string name, [Nullable(2)] string unit, [Nullable(2)] string description)
			: base(meter, name, unit, description)
		{
			Instrument.ValidateTypeParameter<T>();
		}

		// Token: 0x06000222 RID: 546 RVA: 0x000095FE File Offset: 0x000077FE
		protected void RecordMeasurement(T measurement)
		{
			this.RecordMeasurement(measurement, MemoryExtensions.AsSpan<KeyValuePair<string, object>>(Instrument.EmptyTags));
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00009618 File Offset: 0x00007818
		protected void RecordMeasurement(T measurement, [Nullable(new byte[] { 0, 0, 1, 2 })] ReadOnlySpan<KeyValuePair<string, object>> tags)
		{
			for (DiagNode<ListenerSubscription> diagNode = this._subscriptions.First; diagNode != null; diagNode = diagNode.Next)
			{
				diagNode.Value.Listener.NotifyMeasurement<T>(this, measurement, tags, diagNode.Value.State);
			}
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000965C File Offset: 0x0000785C
		protected void RecordMeasurement(T measurement, [Nullable(new byte[] { 0, 1, 2 })] KeyValuePair<string, object> tag)
		{
			KeyValuePair<string, object>[] array = this.ts_tags ?? new KeyValuePair<string, object>[8];
			this.ts_tags = null;
			array[0] = tag;
			this.RecordMeasurement(measurement, MemoryExtensions.AsSpan<KeyValuePair<string, object>>(array).Slice(0, 1));
			this.ts_tags = array;
		}

		// Token: 0x06000225 RID: 549 RVA: 0x000096AC File Offset: 0x000078AC
		protected void RecordMeasurement(T measurement, [Nullable(new byte[] { 0, 1, 2 })] KeyValuePair<string, object> tag1, [Nullable(new byte[] { 0, 1, 2 })] KeyValuePair<string, object> tag2)
		{
			KeyValuePair<string, object>[] array = this.ts_tags ?? new KeyValuePair<string, object>[8];
			this.ts_tags = null;
			array[0] = tag1;
			array[1] = tag2;
			this.RecordMeasurement(measurement, MemoryExtensions.AsSpan<KeyValuePair<string, object>>(array).Slice(0, 2));
			this.ts_tags = array;
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00009704 File Offset: 0x00007904
		protected void RecordMeasurement(T measurement, [Nullable(new byte[] { 0, 1, 2 })] KeyValuePair<string, object> tag1, [Nullable(new byte[] { 0, 1, 2 })] KeyValuePair<string, object> tag2, [Nullable(new byte[] { 0, 1, 2 })] KeyValuePair<string, object> tag3)
		{
			KeyValuePair<string, object>[] array = this.ts_tags ?? new KeyValuePair<string, object>[8];
			this.ts_tags = null;
			array[0] = tag1;
			array[1] = tag2;
			array[2] = tag3;
			this.RecordMeasurement(measurement, MemoryExtensions.AsSpan<KeyValuePair<string, object>>(array).Slice(0, 3));
			this.ts_tags = array;
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00009768 File Offset: 0x00007968
		protected void RecordMeasurement(T measurement, in TagList tagList)
		{
			KeyValuePair<string, object>[] array = tagList.Tags;
			if (array != null)
			{
				this.RecordMeasurement(measurement, MemoryExtensions.AsSpan<KeyValuePair<string, object>>(array).Slice(0, tagList.Count));
				return;
			}
			array = this.ts_tags ?? new KeyValuePair<string, object>[8];
			switch (tagList.Count)
			{
			case 0:
				return;
			case 1:
				goto IL_00CA;
			case 2:
				goto IL_00BD;
			case 3:
				goto IL_00B0;
			case 4:
				goto IL_00A3;
			case 5:
				goto IL_0096;
			case 6:
				goto IL_0089;
			case 7:
				break;
			case 8:
				array[7] = tagList.Tag8;
				break;
			default:
				return;
			}
			array[6] = tagList.Tag7;
			IL_0089:
			array[5] = tagList.Tag6;
			IL_0096:
			array[4] = tagList.Tag5;
			IL_00A3:
			array[3] = tagList.Tag4;
			IL_00B0:
			array[2] = tagList.Tag3;
			IL_00BD:
			array[1] = tagList.Tag2;
			IL_00CA:
			array[0] = tagList.Tag1;
			this.ts_tags = null;
			this.RecordMeasurement(measurement, MemoryExtensions.AsSpan<KeyValuePair<string, object>>(array).Slice(0, tagList.Count));
			this.ts_tags = array;
		}

		// Token: 0x040000FA RID: 250
		[ThreadStatic]
		private KeyValuePair<string, object>[] ts_tags;

		// Token: 0x040000FB RID: 251
		private const int MaxTagsCount = 8;
	}
}
