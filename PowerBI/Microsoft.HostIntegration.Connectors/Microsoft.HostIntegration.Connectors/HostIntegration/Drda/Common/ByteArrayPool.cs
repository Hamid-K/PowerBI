using System;
using System.Collections.Generic;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x02000799 RID: 1945
	public class ByteArrayPool
	{
		// Token: 0x06003EBC RID: 16060 RVA: 0x000D24C0 File Offset: 0x000D06C0
		private ByteArrayPool()
		{
			for (int i = 0; i < this._minPoolSize; i++)
			{
				this._pool.Push(new byte[66558]);
			}
		}

		// Token: 0x17000EDE RID: 3806
		// (get) Token: 0x06003EBD RID: 16061 RVA: 0x000D2517 File Offset: 0x000D0717
		public static ByteArrayPool Instance
		{
			get
			{
				return ByteArrayPool._instance;
			}
		}

		// Token: 0x06003EBE RID: 16062 RVA: 0x000D2520 File Offset: 0x000D0720
		public static byte[] Get()
		{
			ByteArrayPool instance = ByteArrayPool._instance;
			byte[] array2;
			lock (instance)
			{
				byte[] array;
				if (ByteArrayPool.Instance._pool.Count != 0)
				{
					array = ByteArrayPool.Instance._pool.Pop();
				}
				else
				{
					array = new byte[66558];
				}
				array2 = array;
			}
			return array2;
		}

		// Token: 0x06003EBF RID: 16063 RVA: 0x000D258C File Offset: 0x000D078C
		public static void Put(byte[] buffer)
		{
			if (buffer == null)
			{
				return;
			}
			ByteArrayPool instance = ByteArrayPool._instance;
			lock (instance)
			{
				if (ByteArrayPool.Instance._pool.Count < ByteArrayPool.Instance._maxPoolSize)
				{
					ByteArrayPool.Instance._pool.Push(buffer);
				}
			}
		}

		// Token: 0x04002545 RID: 9541
		private int _minPoolSize = 10;

		// Token: 0x04002546 RID: 9542
		private int _maxPoolSize = 200;

		// Token: 0x04002547 RID: 9543
		private Stack<byte[]> _pool = new Stack<byte[]>();

		// Token: 0x04002548 RID: 9544
		private static readonly ByteArrayPool _instance = new ByteArrayPool();
	}
}
