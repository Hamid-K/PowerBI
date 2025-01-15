using System;
using System.Collections.Generic;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000180 RID: 384
	internal class VelocityPacketPropertyBag : IVelocityPacketPropertyBag
	{
		// Token: 0x06000C35 RID: 3125 RVA: 0x00028CA0 File Offset: 0x00026EA0
		internal void FromByteArray(byte[] buffer, int offset, int count)
		{
			if (count == 0)
			{
				return;
			}
			if (count < 0 || offset < 0)
			{
				throw new VelocityPacketFormatException("Property bag buffer offset/length is invalid: {0}, {1}", new object[] { offset, count });
			}
			int num = offset + count;
			if (num > buffer.Length)
			{
				throw new VelocityPacketFormatFatalException("Property bag buffer invalid: Expected length {0}, Actual {1}", new object[]
				{
					count,
					buffer.Length - offset
				});
			}
			while (offset < num)
			{
				if (offset + 2 + 4 > num)
				{
					throw new VelocityPacketFormatException("Property bag buffer truncated");
				}
				VelocityPacketProperty velocityPacketProperty = (VelocityPacketProperty)BitConverter.ToInt16(buffer, offset);
				int num2 = BitConverter.ToInt32(buffer, offset + 2);
				offset += 6;
				if (num2 < 0 || offset + num2 > num)
				{
					throw new VelocityPacketFormatException("Invalid property bag item length. Key: {0}, Actual: {1}, Maximum expected: {2}", new object[]
					{
						velocityPacketProperty,
						num2,
						num - offset
					});
				}
				if (num2 == 0)
				{
					this.AddRequestedProperty(velocityPacketProperty);
				}
				else
				{
					byte[] array = new byte[num2];
					Array.Copy(buffer, offset, array, 0, num2);
					offset += num2;
					this.SetElement(velocityPacketProperty, array);
				}
			}
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x00028DB8 File Offset: 0x00026FB8
		internal uint GetLength()
		{
			if (this._propertyBag.Count == 0)
			{
				return 0U;
			}
			int num = 0;
			foreach (KeyValuePair<VelocityPacketProperty, byte[]> keyValuePair in this._propertyBag)
			{
				num += 6;
				if (keyValuePair.Value != null)
				{
					num += keyValuePair.Value.Length;
				}
			}
			return (uint)num;
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x00028E2C File Offset: 0x0002702C
		internal void Write(byte[] array, ref int offset)
		{
			if (this._propertyBag.Count == 0)
			{
				return;
			}
			checked
			{
				foreach (KeyValuePair<VelocityPacketProperty, byte[]> keyValuePair in this._propertyBag)
				{
					BitConverter.GetBytes((short)keyValuePair.Key).CopyTo(array, offset);
					if (keyValuePair.Value != null)
					{
						BitConverter.GetBytes(keyValuePair.Value.Length).CopyTo(array, offset + 2);
						keyValuePair.Value.CopyTo(array, offset + 2 + 4);
						offset += keyValuePair.Value.Length + 2 + 4;
					}
					else
					{
						BitConverter.GetBytes(0).CopyTo(array, offset + 2);
						offset += 6;
					}
				}
			}
		}

		// Token: 0x06000C38 RID: 3128 RVA: 0x00028EF8 File Offset: 0x000270F8
		private bool SetElementInternal(VelocityPacketProperty key, byte[] value)
		{
			this._propertyBag[key] = value;
			return true;
		}

		// Token: 0x06000C39 RID: 3129 RVA: 0x00028F08 File Offset: 0x00027108
		private bool RemoveElementInternal(VelocityPacketProperty key)
		{
			return this._propertyBag.Remove(key);
		}

		// Token: 0x06000C3A RID: 3130 RVA: 0x00028F16 File Offset: 0x00027116
		public bool Exists(VelocityPacketProperty key)
		{
			return this._propertyBag.Keys.Contains(key);
		}

		// Token: 0x06000C3B RID: 3131 RVA: 0x00028F29 File Offset: 0x00027129
		public bool TryGetElement(VelocityPacketProperty key, out byte[] value)
		{
			return this._propertyBag.TryGetValue(key, out value) && value != null;
		}

		// Token: 0x06000C3C RID: 3132 RVA: 0x00028F44 File Offset: 0x00027144
		public bool SetElement(VelocityPacketProperty key, byte[] value)
		{
			if (value != null && value.Length != 0)
			{
				return this.SetElementInternal(key, value);
			}
			return this.RemoveElementInternal(key);
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x00028F5E File Offset: 0x0002715E
		public IEnumerable<VelocityPacketProperty> GetRequestedProperties()
		{
			return this._requestedProperties;
		}

		// Token: 0x06000C3E RID: 3134 RVA: 0x00028F66 File Offset: 0x00027166
		public void AddRequestedProperty(VelocityPacketProperty property)
		{
			this._requestedProperties.Add(property);
			this.SetElementInternal(property, null);
		}

		// Token: 0x040008C8 RID: 2248
		private const int _keyLength = 2;

		// Token: 0x040008C9 RID: 2249
		private const int _valueSizeLength = 4;

		// Token: 0x040008CA RID: 2250
		private readonly ICollection<VelocityPacketProperty> _requestedProperties = new LinkedList<VelocityPacketProperty>();

		// Token: 0x040008CB RID: 2251
		private readonly IDictionary<VelocityPacketProperty, byte[]> _propertyBag = new Dictionary<VelocityPacketProperty, byte[]>(VelocityPacketPropertyBag.VelocityPacketPropertyEqualityComparer.Instance);

		// Token: 0x02000181 RID: 385
		private sealed class VelocityPacketPropertyEqualityComparer : IEqualityComparer<VelocityPacketProperty>
		{
			// Token: 0x06000C40 RID: 3136 RVA: 0x00002061 File Offset: 0x00000261
			private VelocityPacketPropertyEqualityComparer()
			{
			}

			// Token: 0x06000C41 RID: 3137 RVA: 0x00028FA0 File Offset: 0x000271A0
			public bool Equals(VelocityPacketProperty x, VelocityPacketProperty y)
			{
				return x == y;
			}

			// Token: 0x06000C42 RID: 3138 RVA: 0x00028FA6 File Offset: 0x000271A6
			public int GetHashCode(VelocityPacketProperty prop)
			{
				return (int)prop;
			}

			// Token: 0x040008CC RID: 2252
			internal static readonly VelocityPacketPropertyBag.VelocityPacketPropertyEqualityComparer Instance = new VelocityPacketPropertyBag.VelocityPacketPropertyEqualityComparer();
		}
	}
}
