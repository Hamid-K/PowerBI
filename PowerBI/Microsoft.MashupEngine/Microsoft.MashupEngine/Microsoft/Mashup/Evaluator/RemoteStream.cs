using System;
using System.IO;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001D47 RID: 7495
	public static class RemoteStream
	{
		// Token: 0x0600BA88 RID: 47752 RVA: 0x0025C4CC File Offset: 0x0025A6CC
		public static void RunStub(IEngineHost engineHost, IMessageChannel channel, Func<Stream> getStream)
		{
			using (Stream targetStream = RemoteStream.CreateWriterStub(engineHost, channel))
			{
				EvaluationHost.ReportExceptions("RemoteStream/RunStub", engineHost, channel, delegate
				{
					using (Stream stream = getStream())
					{
						stream.CopyTo(targetStream);
					}
				});
			}
		}

		// Token: 0x0600BA89 RID: 47753 RVA: 0x0025C534 File Offset: 0x0025A734
		public static Stream CreateWriterStub(IEngineHost engineHost, IMessageChannel channel)
		{
			return new MessageBasedOutputStream(channel);
		}

		// Token: 0x0600BA8A RID: 47754 RVA: 0x0025C53C File Offset: 0x0025A73C
		public static Stream CreateReaderProxy(IEngineHost engineHost, IMessageChannel channel, ExceptionHandler exceptionHandler)
		{
			return new MessageBasedInputStream(channel, exceptionHandler);
		}
	}
}
