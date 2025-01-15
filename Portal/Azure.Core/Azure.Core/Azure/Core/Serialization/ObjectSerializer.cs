using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core.Pipeline;

namespace Azure.Core.Serialization
{
	// Token: 0x020000C7 RID: 199
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class ObjectSerializer
	{
		// Token: 0x06000699 RID: 1689
		public abstract void Serialize(Stream stream, [Nullable(2)] object value, Type inputType, CancellationToken cancellationToken);

		// Token: 0x0600069A RID: 1690
		public abstract ValueTask SerializeAsync(Stream stream, [Nullable(2)] object value, Type inputType, CancellationToken cancellationToken);

		// Token: 0x0600069B RID: 1691
		[return: Nullable(2)]
		public abstract object Deserialize(Stream stream, Type returnType, CancellationToken cancellationToken);

		// Token: 0x0600069C RID: 1692
		[return: Nullable(new byte[] { 0, 2 })]
		public abstract ValueTask<object> DeserializeAsync(Stream stream, Type returnType, CancellationToken cancellationToken);

		// Token: 0x0600069D RID: 1693 RVA: 0x00016886 File Offset: 0x00014A86
		[NullableContext(2)]
		[return: Nullable(1)]
		public virtual BinaryData Serialize(object value, Type inputType = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.SerializeToBinaryDataInternalAsync(value, inputType, false, cancellationToken).EnsureCompleted<BinaryData>();
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x00016898 File Offset: 0x00014A98
		[NullableContext(2)]
		[return: Nullable(new byte[] { 0, 1 })]
		public virtual async ValueTask<BinaryData> SerializeAsync(object value, Type inputType = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return await this.SerializeToBinaryDataInternalAsync(value, inputType, true, cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x000168F4 File Offset: 0x00014AF4
		[NullableContext(2)]
		[return: Nullable(new byte[] { 0, 1 })]
		private async ValueTask<BinaryData> SerializeToBinaryDataInternalAsync(object value, Type inputType, bool async, CancellationToken cancellationToken)
		{
			BinaryData binaryData;
			using (MemoryStream stream = new MemoryStream())
			{
				if (inputType == null)
				{
					inputType = ((value != null) ? value.GetType() : null) ?? typeof(object);
				}
				if (async)
				{
					await this.SerializeAsync(stream, value, inputType, cancellationToken).ConfigureAwait(false);
				}
				else
				{
					this.Serialize(stream, value, inputType, cancellationToken);
				}
				binaryData = new BinaryData(MemoryExtensions.AsMemory<byte>(stream.GetBuffer(), 0, (int)stream.Position));
			}
			return binaryData;
		}
	}
}
