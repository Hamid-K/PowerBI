using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Security.Authentication;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020002D1 RID: 721
	internal class TcpUtility
	{
		// Token: 0x06001ABB RID: 6843 RVA: 0x000511C0 File Offset: 0x0004F3C0
		public static IList<ArraySegment<byte>> GetRemainingArraySegment(IList<ArraySegment<byte>> data, int size, int bytesRead)
		{
			if (size == bytesRead)
			{
				return null;
			}
			int num = 0;
			int i = 0;
			while (num + data[i].Count <= bytesRead)
			{
				num += data[i].Count;
				i++;
			}
			List<ArraySegment<byte>> list = new List<ArraySegment<byte>>();
			if (num != bytesRead)
			{
				int num2 = num + data[i].Count - bytesRead;
				int num3 = bytesRead - num + data[i].Offset;
				ArraySegment<byte> arraySegment = new ArraySegment<byte>(data[i].Array, num3, num2);
				list.Add(arraySegment);
				i++;
			}
			while (i < data.Count)
			{
				list.Add(data[i++]);
			}
			return list;
		}

		// Token: 0x06001ABC RID: 6844 RVA: 0x00051280 File Offset: 0x0004F480
		public static ArraySegment<byte> GetRemainingArraySegment(ArraySegment<byte> data, int bytesRead)
		{
			if (data.Count == bytesRead)
			{
				return default(ArraySegment<byte>);
			}
			int num = data.Count - bytesRead;
			int num2 = data.Offset + bytesRead;
			ArraySegment<byte> arraySegment = new ArraySegment<byte>(data.Array, num2, num);
			return arraySegment;
		}

		// Token: 0x06001ABD RID: 6845 RVA: 0x000512C8 File Offset: 0x0004F4C8
		public static bool HandleSocketException(string logSource, Exception e)
		{
			SocketException ex = e as SocketException;
			if (ex != null)
			{
				TcpUtility.Log(logSource, ex);
				return true;
			}
			return false;
		}

		// Token: 0x06001ABE RID: 6846 RVA: 0x000512E9 File Offset: 0x0004F4E9
		public static bool HandleException(string logSource, Exception e)
		{
			if (TcpUtility.HandleSocketException(logSource, e))
			{
				return true;
			}
			TcpUtility.Log(logSource, e);
			return e is ObjectDisposedException || e is TimeoutException;
		}

		// Token: 0x06001ABF RID: 6847 RVA: 0x00051310 File Offset: 0x0004F510
		public static bool IsAuthenticationFailure(Exception e)
		{
			return e is AuthenticationException || e is InvalidOperationException || e is IOException;
		}

		// Token: 0x06001AC0 RID: 6848 RVA: 0x00051330 File Offset: 0x0004F530
		public static bool HandleAuthException(string logSource, Exception e)
		{
			if (Provider.IsEnabled(TraceLevel.Error))
			{
				EventLogWriter.WriteError(logSource, "Exception received while Securing Channel {0}", new object[] { e });
			}
			return TcpUtility.IsAuthenticationFailure(e) || e is TimeoutException;
		}

		// Token: 0x06001AC1 RID: 6849 RVA: 0x0005136F File Offset: 0x0004F56F
		public static bool HandleSecureMessageFailure(string logSource, Exception ex)
		{
			TcpUtility.Log(logSource, ex);
			return ex is IOException || ex is NotSupportedException || ex is ObjectDisposedException || ex is InvalidOperationException;
		}

		// Token: 0x06001AC2 RID: 6850 RVA: 0x0005139C File Offset: 0x0004F59C
		public static OperationStatus GetChannelOpenFailureResponseCode(Exception exception)
		{
			ChannelAuthenticationException ex = exception as ChannelAuthenticationException;
			if (ex != null)
			{
				if (ex.ErrorStatus == ErrStatus.CHANNEL_AUTH_CRL_OFFLINE)
				{
					return OperationStatus.ChannelAuthOfflineRevocationFailure;
				}
				return OperationStatus.ChannelAuthFailure;
			}
			else
			{
				if (exception is AuthenticationException)
				{
					return OperationStatus.ChannelAuthFailure;
				}
				return OperationStatus.ChannelOpenFailed;
			}
		}

		// Token: 0x06001AC3 RID: 6851 RVA: 0x000513D0 File Offset: 0x0004F5D0
		public static DateTime GetDeadlineSafe(TimeSpan timeout)
		{
			if (timeout > TcpUtility.MaxTimeout)
			{
				timeout = TcpUtility.MaxTimeout;
			}
			return DateTime.UtcNow.Add(timeout);
		}

		// Token: 0x06001AC4 RID: 6852 RVA: 0x000513FF File Offset: 0x0004F5FF
		public static DateTime GetDeadlineSafe(TimeSpan timeout, DateTime epoch)
		{
			if (timeout > TcpUtility.MaxTimeout)
			{
				timeout = TcpUtility.MaxTimeout;
			}
			return epoch.Add(timeout);
		}

		// Token: 0x06001AC5 RID: 6853 RVA: 0x00051420 File Offset: 0x0004F620
		public static void Log(string logSource, SocketException e)
		{
			if (Provider.IsEnabled(TraceLevel.Info))
			{
				EventLogWriter.WriteInfo(logSource, "SocketException errorcode:{0} \n message {1}", new object[] { e.ErrorCode, e });
			}
		}

		// Token: 0x06001AC6 RID: 6854 RVA: 0x0005145C File Offset: 0x0004F65C
		public static void Log(string logSource, Exception e)
		{
			if (Provider.IsEnabled(TraceLevel.Info))
			{
				EventLogWriter.WriteInfo(logSource, "Exception received {0}", new object[] { e });
			}
		}

		// Token: 0x06001AC7 RID: 6855 RVA: 0x00051488 File Offset: 0x0004F688
		public static void Log(string logSource, string action, ArraySegment<byte> buffer, TimeSpan timeout)
		{
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<string, int, TimeSpan>(logSource, "Operation {0} Invoked: Length {1} timeout {2}", action, buffer.Count, timeout);
			}
		}

		// Token: 0x06001AC8 RID: 6856 RVA: 0x000514A8 File Offset: 0x0004F6A8
		public static void Log(string logSource, string action, IList<ArraySegment<byte>> buffer, TimeSpan timeout)
		{
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				int length = TcpUtility.GetLength(buffer);
				EventLogWriter.WriteVerbose<string, int, TimeSpan>(logSource, "Operation {0} Invoked: Length {1} timeout {2}", action, length, timeout);
			}
		}

		// Token: 0x06001AC9 RID: 6857 RVA: 0x000514D2 File Offset: 0x0004F6D2
		public static void Log(string logSource, string action, ArraySegment<byte> buffer)
		{
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<string, int>(logSource, "Operation {0} Invoked: Length {1}", action, buffer.Count);
			}
		}

		// Token: 0x06001ACA RID: 6858 RVA: 0x000514F0 File Offset: 0x0004F6F0
		public static void Log(string logSource, string action, IList<ArraySegment<byte>> buffer)
		{
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				int length = TcpUtility.GetLength(buffer);
				EventLogWriter.WriteVerbose<string, int>(logSource, "Operation {0} Invoked: Length {1}", action, length);
			}
		}

		// Token: 0x06001ACB RID: 6859 RVA: 0x00051519 File Offset: 0x0004F719
		public static void HandleZeroBytesPacket(ITcpChannel channel, IOState ioState, string logSource)
		{
			if (Provider.IsEnabled(TraceLevel.Info))
			{
				EventLogWriter.WriteInfo(logSource, "Zero number of bytes received", new object[0]);
			}
			channel.CloseGracefully();
		}

		// Token: 0x06001ACC RID: 6860 RVA: 0x0005153C File Offset: 0x0004F73C
		public static int GetLength(IList<ArraySegment<byte>> buffer)
		{
			int num = 0;
			for (int i = 0; i < buffer.Count; i++)
			{
				num += buffer[i].Count;
			}
			return num;
		}

		// Token: 0x06001ACD RID: 6861 RVA: 0x00051570 File Offset: 0x0004F770
		public static void InitializeSocketTransportProperties(Socket socket, TcpTransportProperty tcpTransportProperty)
		{
			socket.SendTimeout = (int)tcpTransportProperty.SendTimeout.TotalMilliseconds;
			socket.ReceiveTimeout = (int)tcpTransportProperty.ReceiveTimeout.TotalMilliseconds;
			socket.NoDelay = tcpTransportProperty.NoDelay;
			if (tcpTransportProperty.ConnectionBufferSize != -1)
			{
				socket.SendBufferSize = tcpTransportProperty.ConnectionBufferSize;
				socket.ReceiveBufferSize = tcpTransportProperty.ConnectionBufferSize;
			}
		}

		// Token: 0x06001ACE RID: 6862 RVA: 0x000515D4 File Offset: 0x0004F7D4
		public static void CompleteAsyncReceiveMessage(ITcpChannel channel, RecvMessageState messageState, InvokeMessageCallback messageCallback, string logSource)
		{
			try
			{
				while (messageState.BufferEnumerator.MoveNext())
				{
					ArraySegment<byte> arraySegment = messageState.BufferEnumerator.Current;
					int num = channel.Receive(arraySegment);
					if (num != arraySegment.Count)
					{
						return;
					}
				}
				messageCallback(null);
				messageState.BufferEnumerator.Dispose();
				messageState = null;
				channel.ReceiveMessage();
			}
			catch (VelocityPacketTooBigException ex)
			{
				if (Provider.IsEnabled(TraceLevel.Info))
				{
					EventLogWriter.WriteInfo(logSource, "Fatal exception '{0}' reading packet on channel {1}.", new object[] { ex, channel });
				}
				messageCallback(ex);
				channel.CloseGracefully();
			}
			catch (VelocityPacketFormatFatalException ex2)
			{
				if (Provider.IsEnabled(TraceLevel.Info))
				{
					EventLogWriter.WriteInfo(logSource, "Fatal exception '{0}' reading packet on channel {1}.", new object[] { ex2, channel });
				}
				channel.CloseGracefully();
			}
			catch (VelocityPacketException ex3)
			{
				if (Provider.IsEnabled(TraceLevel.Info))
				{
					EventLogWriter.WriteInfo(logSource, "Exception '{0}' reading packet on channel {1}.", new object[] { ex3, channel });
				}
				messageCallback(ex3);
				messageState.BufferEnumerator.Dispose();
				messageState = null;
				channel.ReceiveMessage();
			}
		}

		// Token: 0x04000E35 RID: 3637
		public static readonly TimeSpan MaxTimeout = new TimeSpan(365, 0, 0, 0);

		// Token: 0x04000E36 RID: 3638
		public static readonly TimeSpan ConnectionTimeout = new TimeSpan(0, 5, 0);
	}
}
