using System;
using System.Reflection;
using Microsoft.Mashup.Common;

namespace Microsoft.Mashup.Engine1.Library.MQ
{
	// Token: 0x0200094E RID: 2382
	public class Queue
	{
		// Token: 0x06004401 RID: 17409 RVA: 0x000E531A File Offset: 0x000E351A
		public Queue(string name, QueueManager queueManager)
		{
			this.RealValue = Queue.Constructor.Invoke(new object[] { name, queueManager.RealValue });
		}

		// Token: 0x170015B5 RID: 5557
		// (get) Token: 0x06004402 RID: 17410 RVA: 0x000E5345 File Offset: 0x000E3545
		// (set) Token: 0x06004403 RID: 17411 RVA: 0x000E534D File Offset: 0x000E354D
		public object RealValue { get; protected set; }

		// Token: 0x170015B6 RID: 5558
		// (get) Token: 0x06004404 RID: 17412 RVA: 0x000E5356 File Offset: 0x000E3556
		// (set) Token: 0x06004405 RID: 17413 RVA: 0x000E535D File Offset: 0x000E355D
		private static Type RealType { get; set; } = AssemblyLoader.HisConnectors.GetType("Microsoft.HostIntegration.MqClient.Queue");

		// Token: 0x170015B7 RID: 5559
		// (get) Token: 0x06004406 RID: 17414 RVA: 0x000E5365 File Offset: 0x000E3565
		// (set) Token: 0x06004407 RID: 17415 RVA: 0x000E537D File Offset: 0x000E357D
		public OpenOption Options
		{
			get
			{
				return (OpenOption)Queue.OptionsInfo.GetValue(this.RealValue, null);
			}
			set
			{
				Queue.OptionsInfo.SetValue(this.RealValue, value, null);
			}
		}

		// Token: 0x170015B8 RID: 5560
		// (get) Token: 0x06004408 RID: 17416 RVA: 0x000E5396 File Offset: 0x000E3596
		public QueueState State
		{
			get
			{
				return (QueueState)Queue.StateInfo.GetValue(this.RealValue, null);
			}
		}

		// Token: 0x06004409 RID: 17417 RVA: 0x000E53B0 File Offset: 0x000E35B0
		public ReceiveMessage Receive(ReceiveOptions options)
		{
			object obj;
			try
			{
				obj = Queue.ReceiveInfo.Invoke(this.RealValue, new object[] { options.RealValue });
			}
			catch (TargetInvocationException ex)
			{
				throw MqException.New(ex.InnerException);
			}
			return new ReceiveMessage(obj);
		}

		// Token: 0x0600440A RID: 17418 RVA: 0x000E5400 File Offset: 0x000E3600
		public void Send(SendMessage message)
		{
			try
			{
				Queue.SendInfo.Invoke(this.RealValue, new object[] { message.RealValue });
			}
			catch (TargetInvocationException ex)
			{
				throw MqException.New(ex.InnerException);
			}
		}

		// Token: 0x0600440B RID: 17419 RVA: 0x000E544C File Offset: 0x000E364C
		public void OpenForReceive()
		{
			try
			{
				Queue.OpenForReceiveInfo.Invoke(this.RealValue, null);
			}
			catch (TargetInvocationException ex)
			{
				throw MqException.New(ex.InnerException);
			}
		}

		// Token: 0x0600440C RID: 17420 RVA: 0x000E5488 File Offset: 0x000E3688
		public void OpenForSend()
		{
			try
			{
				Queue.OpenForSendInfo.Invoke(this.RealValue, null);
			}
			catch (TargetInvocationException ex)
			{
				throw MqException.New(ex.InnerException);
			}
		}

		// Token: 0x0600440D RID: 17421 RVA: 0x000E54C4 File Offset: 0x000E36C4
		public void Close()
		{
			if (this.State == QueueState.OpenReceive || this.State == QueueState.OpenSend)
			{
				try
				{
					Queue.CloseInfo.Invoke(this.RealValue, null);
				}
				catch (TargetInvocationException ex)
				{
					if (!SafeExceptions.IsSafeException(ex.InnerException))
					{
						throw ex.InnerException;
					}
				}
			}
		}

		// Token: 0x04002400 RID: 9216
		private static readonly ConstructorInfo Constructor = Queue.RealType.GetConstructor(new Type[]
		{
			typeof(string),
			QueueManager.RealType
		});

		// Token: 0x04002401 RID: 9217
		private static readonly MethodInfo OpenForReceiveInfo = Queue.RealType.GetMethod("OpenForReceive", Type.EmptyTypes);

		// Token: 0x04002402 RID: 9218
		private static readonly MethodInfo OpenForSendInfo = Queue.RealType.GetMethod("OpenForSend", Type.EmptyTypes);

		// Token: 0x04002403 RID: 9219
		private static readonly MethodInfo CloseInfo = Queue.RealType.GetMethod("Close", Type.EmptyTypes);

		// Token: 0x04002404 RID: 9220
		private static readonly MethodInfo ReceiveInfo = Queue.RealType.GetMethod("Receive", new Type[] { ReceiveOptions.RealType });

		// Token: 0x04002405 RID: 9221
		private static readonly MethodInfo SendInfo = Queue.RealType.GetMethod("Send", new Type[] { SendMessage.RealType });

		// Token: 0x04002406 RID: 9222
		private static readonly PropertyInfo OptionsInfo = Queue.RealType.GetProperty("Options");

		// Token: 0x04002407 RID: 9223
		private static readonly PropertyInfo StateInfo = Queue.RealType.GetProperty("State");
	}
}
