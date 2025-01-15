using System;
using System.Web.Http.ExceptionHandling;

namespace System.Web.Http.Owin
{
	// Token: 0x0200000C RID: 12
	public static class OwinExceptionCatchBlocks
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00002AE3 File Offset: 0x00000CE3
		public static ExceptionContextCatchBlock HttpMessageHandlerAdapterBufferContent
		{
			get
			{
				return OwinExceptionCatchBlocks._httpMessageHandlerAdapterBufferContent;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00002AEA File Offset: 0x00000CEA
		public static ExceptionContextCatchBlock HttpMessageHandlerAdapterBufferError
		{
			get
			{
				return OwinExceptionCatchBlocks._httpMessageHandlerAdapterBufferError;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00002AF1 File Offset: 0x00000CF1
		public static ExceptionContextCatchBlock HttpMessageHandlerAdapterComputeContentLength
		{
			get
			{
				return OwinExceptionCatchBlocks._httpMessageHandlerAdapterComputeContentLength;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00002AF8 File Offset: 0x00000CF8
		public static ExceptionContextCatchBlock HttpMessageHandlerAdapterStreamContent
		{
			get
			{
				return OwinExceptionCatchBlocks._httpMessageHandlerAdapterStreamContent;
			}
		}

		// Token: 0x0400000F RID: 15
		private static readonly ExceptionContextCatchBlock _httpMessageHandlerAdapterBufferContent = new ExceptionContextCatchBlock(typeof(HttpMessageHandlerAdapter).Name + ".BufferContent", true, true);

		// Token: 0x04000010 RID: 16
		private static readonly ExceptionContextCatchBlock _httpMessageHandlerAdapterBufferError = new ExceptionContextCatchBlock(typeof(HttpMessageHandlerAdapter).Name + ".BufferError", true, false);

		// Token: 0x04000011 RID: 17
		private static readonly ExceptionContextCatchBlock _httpMessageHandlerAdapterComputeContentLength = new ExceptionContextCatchBlock(typeof(HttpMessageHandlerAdapter).Name + ".ComputeContentLength", true, false);

		// Token: 0x04000012 RID: 18
		private static readonly ExceptionContextCatchBlock _httpMessageHandlerAdapterStreamContent = new ExceptionContextCatchBlock(typeof(HttpMessageHandlerAdapter).Name + ".StreamContent", true, false);
	}
}
