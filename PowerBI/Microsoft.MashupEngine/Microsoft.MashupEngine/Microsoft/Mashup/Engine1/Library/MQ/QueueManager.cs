using System;
using System.Reflection;
using Microsoft.Mashup.Common;

namespace Microsoft.Mashup.Engine1.Library.MQ
{
	// Token: 0x0200094F RID: 2383
	public class QueueManager
	{
		// Token: 0x0600440F RID: 17423 RVA: 0x000E5644 File Offset: 0x000E3844
		public QueueManager(string name, string channelName, string host, int port, bool useSsl)
		{
			this.RealValue = QueueManager.Constructor.Invoke(new object[]
			{
				name,
				channelName,
				host,
				(port == 0) ? 1414 : port,
				useSsl
			});
		}

		// Token: 0x170015B9 RID: 5561
		// (get) Token: 0x06004410 RID: 17424 RVA: 0x000E5698 File Offset: 0x000E3898
		// (set) Token: 0x06004411 RID: 17425 RVA: 0x000E56A0 File Offset: 0x000E38A0
		public object RealValue { get; protected set; }

		// Token: 0x170015BA RID: 5562
		// (get) Token: 0x06004412 RID: 17426 RVA: 0x000E56A9 File Offset: 0x000E38A9
		// (set) Token: 0x06004413 RID: 17427 RVA: 0x000E56B0 File Offset: 0x000E38B0
		public static Type RealType { get; set; } = AssemblyLoader.HisConnectors.GetType("Microsoft.HostIntegration.MqClient.QueueManager");

		// Token: 0x170015BB RID: 5563
		// (get) Token: 0x06004414 RID: 17428 RVA: 0x000E56B8 File Offset: 0x000E38B8
		public QueueManagerState State
		{
			get
			{
				return (QueueManagerState)QueueManager.StateInfo.GetValue(this.RealValue, null);
			}
		}

		// Token: 0x170015BC RID: 5564
		// (get) Token: 0x06004415 RID: 17429 RVA: 0x000E56D0 File Offset: 0x000E38D0
		// (set) Token: 0x06004416 RID: 17430 RVA: 0x000E56E8 File Offset: 0x000E38E8
		public string AuthorizationUser
		{
			get
			{
				return (string)QueueManager.AuthorizationUserInfo.GetValue(this.RealValue, null);
			}
			set
			{
				QueueManager.AuthorizationUserInfo.SetValue(this.RealValue, value, null);
			}
		}

		// Token: 0x170015BD RID: 5565
		// (get) Token: 0x06004417 RID: 17431 RVA: 0x000E56FC File Offset: 0x000E38FC
		// (set) Token: 0x06004418 RID: 17432 RVA: 0x000E5714 File Offset: 0x000E3914
		public string AuthorizationPassword
		{
			get
			{
				return (string)QueueManager.AuthorizationPasswordInfo.GetValue(this.RealValue, null);
			}
			set
			{
				QueueManager.AuthorizationPasswordInfo.SetValue(this.RealValue, value, null);
			}
		}

		// Token: 0x170015BE RID: 5566
		// (get) Token: 0x06004419 RID: 17433 RVA: 0x000E5728 File Offset: 0x000E3928
		// (set) Token: 0x0600441A RID: 17434 RVA: 0x000E5740 File Offset: 0x000E3940
		public string ConnectAs
		{
			get
			{
				return (string)QueueManager.ConnectAsInfo.GetValue(this.RealValue, null);
			}
			set
			{
				QueueManager.ConnectAsInfo.SetValue(this.RealValue, value, null);
			}
		}

		// Token: 0x170015BF RID: 5567
		// (get) Token: 0x0600441B RID: 17435 RVA: 0x000E5754 File Offset: 0x000E3954
		// (set) Token: 0x0600441C RID: 17436 RVA: 0x000E576C File Offset: 0x000E396C
		public bool UseSsl
		{
			get
			{
				return (bool)QueueManager.UseSslInfo.GetValue(this.RealValue, null);
			}
			set
			{
				QueueManager.UseSslInfo.SetValue(this.RealValue, value, null);
			}
		}

		// Token: 0x170015C0 RID: 5568
		// (get) Token: 0x0600441D RID: 17437 RVA: 0x000E5785 File Offset: 0x000E3985
		public static QueueManagerPooling Pooling
		{
			get
			{
				return new QueueManagerPooling(QueueManager.PoolingInfo.GetValue(null, null));
			}
		}

		// Token: 0x0600441E RID: 17438 RVA: 0x000E5798 File Offset: 0x000E3998
		public void Connect()
		{
			if (this.State == QueueManagerState.Connected)
			{
				return;
			}
			try
			{
				QueueManager.ConnectInfo.Invoke(this.RealValue, null);
			}
			catch (TargetInvocationException ex)
			{
				throw MqException.New(ex.InnerException);
			}
		}

		// Token: 0x0600441F RID: 17439 RVA: 0x000E57E0 File Offset: 0x000E39E0
		public void Disconnect()
		{
			if (this.State != QueueManagerState.Connected)
			{
				return;
			}
			try
			{
				QueueManager.DisconnectInfo.Invoke(this.RealValue, null);
			}
			catch (TargetInvocationException ex)
			{
				if (!SafeExceptions.IsSafeException(ex.InnerException))
				{
					throw ex.InnerException;
				}
			}
		}

		// Token: 0x06004420 RID: 17440 RVA: 0x000E5834 File Offset: 0x000E3A34
		public static QueueManager New(MqConnectionParameters connectionParameters)
		{
			QueueManager queueManager = new QueueManager(connectionParameters.QueueManager, connectionParameters.Channel, connectionParameters.Host, connectionParameters.Port, connectionParameters.UseSsl);
			if (connectionParameters.ConnectAs != null)
			{
				queueManager.ConnectAs = connectionParameters.ConnectAs;
			}
			if (connectionParameters.Username != null)
			{
				queueManager.AuthorizationUser = connectionParameters.Username;
			}
			if (connectionParameters.Password != null)
			{
				queueManager.AuthorizationPassword = connectionParameters.Password;
			}
			return queueManager;
		}

		// Token: 0x0400240A RID: 9226
		private static readonly ConstructorInfo Constructor = QueueManager.RealType.GetConstructor(new Type[]
		{
			typeof(string),
			typeof(string),
			typeof(string),
			typeof(int),
			typeof(bool)
		});

		// Token: 0x0400240B RID: 9227
		private static readonly MethodInfo ConnectInfo = QueueManager.RealType.GetMethod("Connect", Type.EmptyTypes);

		// Token: 0x0400240C RID: 9228
		private static readonly MethodInfo DisconnectInfo = QueueManager.RealType.GetMethod("Disconnect", Type.EmptyTypes);

		// Token: 0x0400240D RID: 9229
		private static readonly PropertyInfo StateInfo = QueueManager.RealType.GetProperty("State");

		// Token: 0x0400240E RID: 9230
		private static readonly PropertyInfo AuthorizationUserInfo = QueueManager.RealType.GetProperty("AuthorizationUser");

		// Token: 0x0400240F RID: 9231
		private static readonly PropertyInfo AuthorizationPasswordInfo = QueueManager.RealType.GetProperty("AuthorizationPassword");

		// Token: 0x04002410 RID: 9232
		private static readonly PropertyInfo ConnectAsInfo = QueueManager.RealType.GetProperty("ConnectAs");

		// Token: 0x04002411 RID: 9233
		private static readonly PropertyInfo PoolingInfo = QueueManager.RealType.GetProperty("Pooling");

		// Token: 0x04002412 RID: 9234
		private static readonly PropertyInfo UseSslInfo = QueueManager.RealType.GetProperty("UseSsl");
	}
}
