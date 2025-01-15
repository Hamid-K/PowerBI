using System;
using System.Text;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator.Services;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001B33 RID: 6963
	public sealed class SerializableValue : ISerializableValue
	{
		// Token: 0x0600AE50 RID: 44624 RVA: 0x0023B0C0 File Offset: 0x002392C0
		public SerializableValue(byte[] bytes, IValue value, int maxBinaryLength, object serializerLock)
		{
			this.serializerLock = serializerLock;
			this.maxBinaryLength = maxBinaryLength;
			this.bytes = bytes;
			this.value = value;
		}

		// Token: 0x0600AE51 RID: 44625 RVA: 0x0023B0E5 File Offset: 0x002392E5
		public SerializableValue(byte[] bytes)
			: this(bytes, null, 0, null)
		{
		}

		// Token: 0x0600AE52 RID: 44626 RVA: 0x0023B0F1 File Offset: 0x002392F1
		public SerializableValue(IValue value, int maxBinaryLength, object serializerLock)
			: this(null, value, maxBinaryLength, serializerLock)
		{
		}

		// Token: 0x0600AE53 RID: 44627 RVA: 0x0023B100 File Offset: 0x00239300
		public byte[] GetBytes()
		{
			if (this.bytes == null)
			{
				ValueSerializerOptions valueSerializerOptions = new ValueSerializerOptions
				{
					MaxValueDepth = 5,
					NestedRecords = true,
					TruncatedBinaryLength = this.maxBinaryLength,
					StripFieldDescriptions = false
				};
				object obj = this.serializerLock;
				lock (obj)
				{
					this.bytes = Encoding.UTF8.GetBytes(ValueSerializer.SerializePreviewValue(MashupEngines.Version1, this.value, null, new ValueSerializerOptions?(valueSerializerOptions)));
				}
			}
			return this.bytes;
		}

		// Token: 0x0600AE54 RID: 44628 RVA: 0x0023B1A0 File Offset: 0x002393A0
		public IValue GetValue()
		{
			if (this.value == null)
			{
				this.value = ValueDeserializer.Deserialize(MashupEngines.Version1, Encoding.UTF8.GetString(this.bytes));
			}
			return this.value;
		}

		// Token: 0x040059E2 RID: 23010
		private readonly object serializerLock;

		// Token: 0x040059E3 RID: 23011
		private readonly int maxBinaryLength;

		// Token: 0x040059E4 RID: 23012
		private byte[] bytes;

		// Token: 0x040059E5 RID: 23013
		private IValue value;
	}
}
