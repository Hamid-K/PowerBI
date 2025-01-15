using System;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200032D RID: 813
	[DataContract(Name = "Key", Namespace = "http://schemas.microsoft.com/velocity/namespace")]
	internal class Key : IBinarySerializable
	{
		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x06001D4C RID: 7500 RVA: 0x00058706 File Offset: 0x00056906
		internal string StringValue
		{
			get
			{
				return this._keyStr;
			}
		}

		// Token: 0x06001D4D RID: 7501 RVA: 0x0005870E File Offset: 0x0005690E
		public override int GetHashCode()
		{
			return this._hash;
		}

		// Token: 0x06001D4E RID: 7502 RVA: 0x00058718 File Offset: 0x00056918
		public override bool Equals(object obj)
		{
			Key key = (Key)obj;
			return this._keyStr.Equals(key._keyStr, StringComparison.Ordinal);
		}

		// Token: 0x06001D4F RID: 7503 RVA: 0x00058706 File Offset: 0x00056906
		public override string ToString()
		{
			return this._keyStr;
		}

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x06001D50 RID: 7504 RVA: 0x0005873E File Offset: 0x0005693E
		public int Length
		{
			get
			{
				return this._keyStr.Length;
			}
		}

		// Token: 0x06001D51 RID: 7505 RVA: 0x0005874B File Offset: 0x0005694B
		public Key(string key)
		{
			this._keyStr = key;
			this._hash = Key.ComputeHash(key);
		}

		// Token: 0x06001D52 RID: 7506 RVA: 0x00058766 File Offset: 0x00056966
		internal static int ComputeHash(string key)
		{
			return (int)CsHash32.ComputeString(key, 0U, false);
		}

		// Token: 0x06001D53 RID: 7507 RVA: 0x00058770 File Offset: 0x00056970
		internal Key(string key, int hash)
		{
			this._keyStr = key;
			this._hash = hash;
		}

		// Token: 0x06001D54 RID: 7508 RVA: 0x00002061 File Offset: 0x00000261
		public Key()
		{
		}

		// Token: 0x06001D55 RID: 7509 RVA: 0x00058786 File Offset: 0x00056986
		void IBinarySerializable.ReadStream(ISerializationReader reader)
		{
			this._keyStr = reader.ReadString();
			this._hash = reader.ReadInt32();
		}

		// Token: 0x06001D56 RID: 7510 RVA: 0x000587A0 File Offset: 0x000569A0
		void IBinarySerializable.WriteStream(ISerializationWriter writer)
		{
			writer.Write(this._keyStr);
			writer.Write(this._hash);
		}

		// Token: 0x0400103B RID: 4155
		private const uint _seed = 0U;

		// Token: 0x0400103C RID: 4156
		public const int PointersOverhead = 4;

		// Token: 0x0400103D RID: 4157
		[DataMember]
		private string _keyStr;

		// Token: 0x0400103E RID: 4158
		[DataMember]
		private int _hash;
	}
}
