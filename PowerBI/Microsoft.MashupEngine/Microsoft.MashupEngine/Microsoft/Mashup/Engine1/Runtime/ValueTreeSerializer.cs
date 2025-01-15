using System;
using System.IO;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020016BF RID: 5823
	internal class ValueTreeSerializer : ValueSerializer
	{
		// Token: 0x06009437 RID: 37943 RVA: 0x001E95FE File Offset: 0x001E77FE
		public ValueTreeSerializer(IValueWriter writer, int maxDepth)
			: base(writer)
		{
			this.depthLimit = maxDepth;
		}

		// Token: 0x06009438 RID: 37944 RVA: 0x001E960E File Offset: 0x001E780E
		public override bool ShouldSkip(Value value)
		{
			if (this.depthLimit < 0)
			{
				throw new InvalidOperationException(Strings.ValueException_CyclicReference);
			}
			return false;
		}

		// Token: 0x06009439 RID: 37945 RVA: 0x001E962A File Offset: 0x001E782A
		public virtual void WriteLimited(Value value)
		{
			base.Write(value);
		}

		// Token: 0x0600943A RID: 37946 RVA: 0x001E9633 File Offset: 0x001E7833
		public sealed override void Write(Value value)
		{
			this.depthLimit--;
			this.WriteLimited(value);
			this.depthLimit++;
		}

		// Token: 0x0600943B RID: 37947 RVA: 0x001E9658 File Offset: 0x001E7858
		public static byte[] SerializeValue(IValue value)
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (StreamValueWriter streamValueWriter = new StreamValueWriter(memoryStream))
				{
					ValueTreeSerializer valueTreeSerializer = new ValueTreeSerializer(streamValueWriter, 1000);
					valueTreeSerializer.WriteNumber(NumberValue.New(1));
					if (value.IsFunction)
					{
						throw new NotImplementedException();
					}
					valueTreeSerializer.WriteLogical(LogicalValue.False);
					valueTreeSerializer.Write((Value)value);
					array = memoryStream.ToArray();
				}
			}
			return array;
		}

		// Token: 0x04004EE7 RID: 20199
		private int depthLimit;
	}
}
