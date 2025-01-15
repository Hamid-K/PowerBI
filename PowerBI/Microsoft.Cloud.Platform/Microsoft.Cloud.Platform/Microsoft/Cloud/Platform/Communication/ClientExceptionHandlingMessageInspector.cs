using System;
using System.IO;
using System.Runtime.ExceptionServices;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Xml;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004BB RID: 1211
	internal class ClientExceptionHandlingMessageInspector : IClientMessageInspector
	{
		// Token: 0x06002514 RID: 9492 RVA: 0x00083EEC File Offset: 0x000820EC
		public void AfterReceiveReply(ref Message reply, object correlationState)
		{
			if (reply.IsFault)
			{
				MessageBuffer messageBuffer = reply.CreateBufferedCopy(int.MaxValue);
				Message message = messageBuffer.CreateMessage();
				reply = messageBuffer.CreateMessage();
				Exception ex = ClientExceptionHandlingMessageInspector.ReadFaultDetail(message) as Exception;
				if (ex != null && !ErrorHandlerHelper.IsVersioningException(ex, reply.Version))
				{
					TraceSourceBase<CommunicationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Warning, "Received a remote error from an ECF call. Throwing the remote exception back to the calling service.{0}{1}", new object[]
					{
						Environment.NewLine,
						ex
					});
					ExceptionDispatchInfo.Capture(ex).Throw();
				}
			}
		}

		// Token: 0x06002515 RID: 9493 RVA: 0x00005EB7 File Offset: 0x000040B7
		public object BeforeSendRequest(ref Message request, IClientChannel channel)
		{
			return null;
		}

		// Token: 0x06002516 RID: 9494 RVA: 0x00083F68 File Offset: 0x00082168
		internal static object ReadFaultDetail(Message reply)
		{
			object obj;
			using (XmlDictionaryReader readerAtBodyContents = reply.GetReaderAtBodyContents())
			{
				while (readerAtBodyContents.Read() && (readerAtBodyContents.NodeType != XmlNodeType.Element || !"Detail".Equals(readerAtBodyContents.LocalName)))
				{
				}
				if (readerAtBodyContents.EOF)
				{
					obj = null;
				}
				else if (!readerAtBodyContents.Read())
				{
					obj = null;
				}
				else
				{
					NetDataContractSerializer netDataContractSerializer = new NetDataContractSerializer();
					try
					{
						obj = netDataContractSerializer.ReadObject(readerAtBodyContents);
					}
					catch (SerializationException ex)
					{
						obj = new CommunicationException("SerializationException", ex);
					}
					catch (FileNotFoundException ex2)
					{
						obj = new CommunicationException("SerializationException", ex2);
					}
					catch (FileLoadException ex3)
					{
						obj = new CommunicationException("SerializationException", ex3);
					}
				}
			}
			return obj;
		}
	}
}
