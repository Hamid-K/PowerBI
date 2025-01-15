using System;
using System.Globalization;
using System.IO;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000171 RID: 369
	internal class ProtocolHandler
	{
		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000BA5 RID: 2981 RVA: 0x00027373 File Offset: 0x00025573
		// (set) Token: 0x06000BA6 RID: 2982 RVA: 0x0002737A File Offset: 0x0002557A
		internal static ServiceConfigurationManager ConfigurationManager { get; set; }

		// Token: 0x06000BA7 RID: 2983 RVA: 0x00027384 File Offset: 0x00025584
		public static byte[][] PerformOperation(AOMCacheItem item, InternalCacheItemVersion version, object opstate)
		{
			RequestBody requestBody = (RequestBody)opstate;
			if (requestBody.SerializationCategory == SerializationCategory.Native)
			{
				return VelocityProtocolHandler.PerformOperation(item, requestBody);
			}
			if (requestBody.SerializationCategory == SerializationCategory.Memcache)
			{
				return ProtocolHandler.MemcacheProtocolHandler.PerformOperation(item, version, requestBody);
			}
			throw new NotSupportedException();
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x000273BF File Offset: 0x000255BF
		public static SerializationCategory GetSerializationCategory(object opState)
		{
			return ((RequestBody)opState).SerializationCategory;
		}

		// Token: 0x02000172 RID: 370
		internal class MemcacheProtocolHandler
		{
			// Token: 0x06000BAA RID: 2986 RVA: 0x000273CC File Offset: 0x000255CC
			internal static byte[][] PerformOperation(AOMCacheItem item, InternalCacheItemVersion version, RequestBody request)
			{
				byte[][] array = ((item == null) ? null : (item.Value as byte[][]));
				InternalCacheItemVersion internalCacheItemVersion = ((item == null) ? InternalCacheItemVersion.Null : item.Version);
				return ProtocolHandler.MemcacheProtocolHandler.PerformOperation(array, internalCacheItemVersion, version, request);
			}

			// Token: 0x06000BAB RID: 2987 RVA: 0x00027408 File Offset: 0x00025608
			internal static byte[][] PerformOperation(byte[][] currentStoreValue, InternalCacheItemVersion currentVersion, InternalCacheItemVersion version, RequestBody request)
			{
				if (request.Req == ReqType.INCREMENT || request.Req == ReqType.DECREMENT)
				{
					return ProtocolHandler.MemcacheProtocolHandler.PerformIncrementDecrement(currentStoreValue, currentVersion, version, request);
				}
				if (request.Req == ReqType.APPEND || request.Req == ReqType.PREPEND)
				{
					return ProtocolHandler.MemcacheProtocolHandler.PerformConcatenate(currentStoreValue, currentVersion, version, request);
				}
				throw new NotSupportedException();
			}

			// Token: 0x06000BAC RID: 2988 RVA: 0x00027458 File Offset: 0x00025658
			private static byte[][] PerformIncrementDecrement(byte[][] currentStoreValue, InternalCacheItemVersion currentVersion, InternalCacheItemVersion version, RequestBody request)
			{
				if (currentStoreValue != null)
				{
					ProtocolHandler.MemcacheProtocolHandler.CheckVersion(currentVersion, version);
					uint num2;
					ulong num = MemcacheProtocolHelper.ReadValueAndFlags("DistributedCache.MemcacheProtocolHandler", request.Value, false, out num2);
					ulong num3 = MemcacheProtocolHelper.ReadValueAndFlags("DistributedCache.MemcacheProtocolHandler", currentStoreValue, true, out num2);
					if (request.Req == ReqType.INCREMENT)
					{
						num3 += num;
					}
					else if (num > num3)
					{
						num3 = 0UL;
					}
					else
					{
						num3 -= num;
					}
					return MemcacheProtocolHelper.WriteValueAndFlags(num3, true, num2);
				}
				if (ProtocolHandler.MemcacheProtocolHandler.FailRequestIfKeyNotFound(request.TTL))
				{
					throw DMGlobal.GetException(2002);
				}
				uint num5;
				ulong num4 = MemcacheProtocolHelper.ReadValueAndFlags("DistributedCache.MemcacheProtocolHandler", request.InitialValue, false, out num5);
				return MemcacheProtocolHelper.WriteValueAndFlags(num4, true, 0U);
			}

			// Token: 0x06000BAD RID: 2989 RVA: 0x000274F4 File Offset: 0x000256F4
			private static byte[][] PerformConcatenate(byte[][] currentStoreValue, InternalCacheItemVersion currentVersion, InternalCacheItemVersion version, RequestBody request)
			{
				if (currentStoreValue == null)
				{
					throw DMGlobal.GetException(2002);
				}
				ProtocolHandler.MemcacheProtocolHandler.CheckVersion(currentVersion, version);
				if (request.Req == ReqType.APPEND)
				{
					using (ChunkStream chunkStream = new ChunkStream())
					{
						foreach (byte[] array in currentStoreValue)
						{
							chunkStream.Write(array, 0, array.Length);
						}
						foreach (byte[] array2 in request.Value)
						{
							chunkStream.Write(array2, 0, array2.Length);
						}
						ProtocolHandler.MemcacheProtocolHandler.ValidateLength(chunkStream);
						return chunkStream.ToChunkedArray();
					}
				}
				byte[][] array6;
				using (ChunkStream chunkStream2 = new ChunkStream())
				{
					foreach (byte[] array3 in currentStoreValue)
					{
						chunkStream2.Write(array3, 0, array3.Length);
						if (chunkStream2.Length > 4L)
						{
							break;
						}
					}
					chunkStream2.Seek(4L, SeekOrigin.Begin);
					foreach (byte[] array4 in request.Value)
					{
						chunkStream2.Write(array4, 0, array4.Length);
					}
					bool flag = false;
					foreach (byte[] array5 in currentStoreValue)
					{
						int num = 0;
						if (!flag)
						{
							num = 4;
							flag = true;
						}
						chunkStream2.Write(array5, num, array5.Length - num);
					}
					ProtocolHandler.MemcacheProtocolHandler.ValidateLength(chunkStream2);
					array6 = chunkStream2.ToChunkedArray();
				}
				return array6;
			}

			// Token: 0x06000BAE RID: 2990 RVA: 0x00027680 File Offset: 0x00025880
			private static void CheckVersion(InternalCacheItemVersion currentVersion, InternalCacheItemVersion version)
			{
				if (version != InternalCacheItemVersion.Null && !currentVersion.MemcacheEquals(version))
				{
					throw DMGlobal.GetException(2003);
				}
			}

			// Token: 0x06000BAF RID: 2991 RVA: 0x000276A4 File Offset: 0x000258A4
			private static void ValidateLength(ChunkStream chunkStream)
			{
				if (chunkStream.Length - 4L > (long)MemcacheProtocolHelper.GetItemSizeMax(ProtocolHandler.ConfigurationManager))
				{
					throw new DataCacheException("DistributedCache.MemcacheProtocolHandler", 20001, GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, 20001), true);
				}
			}

			// Token: 0x06000BB0 RID: 2992 RVA: 0x000276DC File Offset: 0x000258DC
			internal static bool FailRequestIfKeyNotFound(TimeSpan ttl)
			{
				return ttl.TotalSeconds == 4294967295.0;
			}

			// Token: 0x0400086D RID: 2157
			private const string LogSource = "DistributedCache.MemcacheProtocolHandler";
		}
	}
}
