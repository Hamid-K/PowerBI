using System;
using System.IO;
using System.ServiceModel.Channels;

namespace Microsoft.Fabric.Common
{
	// Token: 0x0200041B RID: 1051
	internal class SignatureEncoderFactory : MessageEncoderFactory
	{
		// Token: 0x060024A9 RID: 9385 RVA: 0x000704D7 File Offset: 0x0006E6D7
		public SignatureEncoderFactory(MessageEncoderFactory innerEncoderFactory)
		{
			if (innerEncoderFactory == null)
			{
				throw new ArgumentNullException("innerEncoderFactory");
			}
			this.m_encoder = new SignatureEncoderFactory.SignatureEncoder(innerEncoderFactory.Encoder);
		}

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x060024AA RID: 9386 RVA: 0x000704FE File Offset: 0x0006E6FE
		public override MessageEncoder Encoder
		{
			get
			{
				return this.m_encoder;
			}
		}

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x060024AB RID: 9387 RVA: 0x00070506 File Offset: 0x0006E706
		public override MessageVersion MessageVersion
		{
			get
			{
				return this.m_encoder.MessageVersion;
			}
		}

		// Token: 0x04001676 RID: 5750
		private const int SignatureSize = 4;

		// Token: 0x04001677 RID: 5751
		private MessageEncoder m_encoder;

		// Token: 0x0200041C RID: 1052
		private class SignatureEncoder : MessageEncoder
		{
			// Token: 0x060024AC RID: 9388 RVA: 0x00070514 File Offset: 0x0006E714
			private static uint[] BuildTable()
			{
				uint[] array = new uint[256];
				for (int i = 0; i < 256; i++)
				{
					uint num = (uint)i;
					for (int j = 8; j > 0; j--)
					{
						if ((num & 1U) == 1U)
						{
							num = (num >> 1) ^ 79764919U;
						}
						else
						{
							num >>= 1;
						}
					}
					array[i] = num;
				}
				return array;
			}

			// Token: 0x060024AD RID: 9389 RVA: 0x00070565 File Offset: 0x0006E765
			internal SignatureEncoder(MessageEncoder messageEncoder)
			{
				if (messageEncoder == null)
				{
					throw new ArgumentNullException("messageEncoder");
				}
				this.m_innerEncoder = messageEncoder;
			}

			// Token: 0x1700074E RID: 1870
			// (get) Token: 0x060024AE RID: 9390 RVA: 0x00070582 File Offset: 0x0006E782
			public override string ContentType
			{
				get
				{
					return this.m_innerEncoder.ContentType;
				}
			}

			// Token: 0x1700074F RID: 1871
			// (get) Token: 0x060024AF RID: 9391 RVA: 0x0007058F File Offset: 0x0006E78F
			public override string MediaType
			{
				get
				{
					return this.m_innerEncoder.MediaType;
				}
			}

			// Token: 0x17000750 RID: 1872
			// (get) Token: 0x060024B0 RID: 9392 RVA: 0x0007059C File Offset: 0x0006E79C
			public override MessageVersion MessageVersion
			{
				get
				{
					return this.m_innerEncoder.MessageVersion;
				}
			}

			// Token: 0x060024B1 RID: 9393 RVA: 0x000705AC File Offset: 0x0006E7AC
			private unsafe static byte[] CalculateSignature(byte[] data, int offset, int count)
			{
				uint num = uint.MaxValue;
				fixed (uint* ptr = SignatureEncoderFactory.SignatureEncoder.s_table)
				{
					fixed (byte* ptr2 = data)
					{
						int num2 = offset + count;
						for (int i = offset; i < num2; i++)
						{
							ulong num3 = (ulong)((num & 255U) ^ (uint)ptr2[i]);
							num >>= 8;
							num ^= ptr[num3 * 4UL / 4UL];
						}
					}
				}
				num ^= uint.MaxValue;
				return BitConverter.GetBytes(num);
			}

			// Token: 0x060024B2 RID: 9394 RVA: 0x0007063C File Offset: 0x0006E83C
			public override Message ReadMessage(ArraySegment<byte> buffer, BufferManager bufferManager, string contentType)
			{
				int num = buffer.Count - 4;
				if (num <= 0)
				{
					throw new MessageSignatureException();
				}
				int num2 = buffer.Offset + num;
				byte[] array = SignatureEncoderFactory.SignatureEncoder.CalculateSignature(buffer.Array, buffer.Offset, num);
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i] != buffer.Array[num2 + i])
					{
						EventLogWriter.WriteError("SignatureEncoder", "Incoming message is corrupted!", new object[0]);
						throw new MessageSignatureException();
					}
				}
				ArraySegment<byte> arraySegment = new ArraySegment<byte>(buffer.Array, buffer.Offset, num);
				Message message = this.m_innerEncoder.ReadMessage(arraySegment, bufferManager);
				message.Properties.Encoder = this;
				return message;
			}

			// Token: 0x060024B3 RID: 9395 RVA: 0x000706EC File Offset: 0x0006E8EC
			public override ArraySegment<byte> WriteMessage(Message message, int maxMessageSize, BufferManager bufferManager, int messageOffset)
			{
				ArraySegment<byte> arraySegment = this.m_innerEncoder.WriteMessage(message, maxMessageSize, bufferManager, messageOffset);
				byte[] array = SignatureEncoderFactory.SignatureEncoder.CalculateSignature(arraySegment.Array, arraySegment.Offset, arraySegment.Count);
				int num = arraySegment.Offset + arraySegment.Count;
				int num2 = num + 4;
				if (arraySegment.Array.Length < num2)
				{
					byte[] array2 = bufferManager.TakeBuffer(num2);
					Array.Copy(arraySegment.Array, array2, num);
					Array.Copy(array, 0, array2, num, 4);
					bufferManager.ReturnBuffer(arraySegment.Array);
					return new ArraySegment<byte>(array2, arraySegment.Offset, arraySegment.Count + 4);
				}
				Array.Copy(array, 0, arraySegment.Array, arraySegment.Offset + arraySegment.Count, array.Length);
				return new ArraySegment<byte>(arraySegment.Array, arraySegment.Offset, arraySegment.Count + array.Length);
			}

			// Token: 0x060024B4 RID: 9396 RVA: 0x000707CB File Offset: 0x0006E9CB
			public override Message ReadMessage(Stream stream, int maxSizeOfHeaders, string contentType)
			{
				throw new NotImplementedException("Streaming mode not supported");
			}

			// Token: 0x060024B5 RID: 9397 RVA: 0x000707CB File Offset: 0x0006E9CB
			public override void WriteMessage(Message message, Stream stream)
			{
				throw new NotImplementedException("Streaming mode not supported");
			}

			// Token: 0x04001678 RID: 5752
			private const uint s_poly = 79764919U;

			// Token: 0x04001679 RID: 5753
			private MessageEncoder m_innerEncoder;

			// Token: 0x0400167A RID: 5754
			private static readonly uint[] s_table = SignatureEncoderFactory.SignatureEncoder.BuildTable();
		}
	}
}
