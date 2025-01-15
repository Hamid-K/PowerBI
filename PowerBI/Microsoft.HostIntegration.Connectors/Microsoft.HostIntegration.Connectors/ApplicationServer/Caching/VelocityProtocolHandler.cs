using System;
using System.Globalization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200019D RID: 413
	internal class VelocityProtocolHandler
	{
		// Token: 0x06000D72 RID: 3442 RVA: 0x0002DE08 File Offset: 0x0002C008
		private static byte[][] PerformIncrement(byte[][] currentStoreValue, RequestBody request)
		{
			if (currentStoreValue == null)
			{
				return request.InitialValue;
			}
			long num = 0L;
			long num2 = 0L;
			try
			{
				num = (long)PrimitiveDataCacheObjectSerializationProvider.WireProtocolType.DeserializeUserObject(currentStoreValue, VelocityProtocolHandler.SerializationCategory);
				num2 = (long)PrimitiveDataCacheObjectSerializationProvider.WireProtocolType.DeserializeUserObject(request.Value, VelocityProtocolHandler.SerializationCategory);
			}
			catch (InvalidCastException)
			{
				throw new DataCacheException("SerializationProvider", 20, GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, 20), true);
			}
			try
			{
				num += num2;
			}
			catch (OverflowException)
			{
				throw new DataCacheException("ProtocolHandler", 20001, GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, 20001), true);
			}
			return PrimitiveDataCacheObjectSerializationProvider.WireProtocolType.SerializeUserObject(num, VelocityProtocolHandler.SerializationCategory);
		}

		// Token: 0x06000D73 RID: 3443 RVA: 0x0002DECC File Offset: 0x0002C0CC
		private static byte[][] PerformConcatenate(byte[][] currentStoreValue, bool isAppend, RequestBody request)
		{
			if (currentStoreValue == null)
			{
				throw DMGlobal.GetException(2002);
			}
			string text = null;
			string text2 = null;
			bool flag = false;
			try
			{
				text = (string)PrimitiveDataCacheObjectSerializationProvider.WireProtocolType.DeserializeUserObject(currentStoreValue, VelocityProtocolHandler.SerializationCategory, out flag);
				bool flag2;
				text2 = (string)PrimitiveDataCacheObjectSerializationProvider.WireProtocolType.DeserializeUserObject(request.Value, VelocityProtocolHandler.SerializationCategory, out flag2);
				flag = flag || flag2;
			}
			catch (InvalidCastException)
			{
				throw new DataCacheException("ProtocolHandler", 20, GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, 20), true);
			}
			if (isAppend)
			{
				text += text2;
			}
			else
			{
				text = text2 + text;
			}
			byte[][] array = PrimitiveDataCacheObjectSerializationProvider.WireProtocolType.SerializeUserObject(text, VelocityProtocolHandler.SerializationCategory, flag);
			if (Utility.Get2DByteArraySize(array) > ProtocolHandler.ConfigurationManager.BatchSize)
			{
				throw new DataCacheException("ProtocolHandler", 20001, GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, 20001), true);
			}
			return array;
		}

		// Token: 0x06000D74 RID: 3444 RVA: 0x0002DFB4 File Offset: 0x0002C1B4
		public static byte[][] PerformOperation(AOMCacheItem item, RequestBody request)
		{
			byte[][] array = ((item == null) ? null : (item.Value as byte[][]));
			if (request.Req == ReqType.INCREMENT)
			{
				return VelocityProtocolHandler.PerformIncrement(array, request);
			}
			if (request.Req == ReqType.APPEND || request.Req == ReqType.PREPEND)
			{
				bool flag = false;
				if (request.Req == ReqType.APPEND)
				{
					flag = true;
				}
				return VelocityProtocolHandler.PerformConcatenate(array, flag, request);
			}
			throw new NotSupportedException();
		}

		// Token: 0x0400095A RID: 2394
		private static SerializationCategory SerializationCategory;
	}
}
