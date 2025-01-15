using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Eventing
{
	// Token: 0x02000395 RID: 917
	[Serializable]
	public class SinkFactoryStillInUseException : MonitoredException
	{
		// Token: 0x06001C3C RID: 7228 RVA: 0x0000EB75 File Offset: 0x0000CD75
		public SinkFactoryStillInUseException()
		{
		}

		// Token: 0x06001C3D RID: 7229 RVA: 0x0000EB7D File Offset: 0x0000CD7D
		public SinkFactoryStillInUseException(string message)
			: base(message)
		{
		}

		// Token: 0x06001C3E RID: 7230 RVA: 0x0000EB86 File Offset: 0x0000CD86
		public SinkFactoryStillInUseException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06001C3F RID: 7231 RVA: 0x0000EB9E File Offset: 0x0000CD9E
		protected SinkFactoryStillInUseException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x06001C40 RID: 7232 RVA: 0x0006B920 File Offset: 0x00069B20
		public SinkFactoryStillInUseException([NotNull] ISinkFactory factory, int createdSinkCount)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<ISinkFactory>(factory, "factory");
			this.m_factory = factory;
			this.m_createdSinkCount = createdSinkCount;
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06001C41 RID: 7233 RVA: 0x0006B941 File Offset: 0x00069B41
		public override string Message
		{
			get
			{
				return string.Format(CultureInfo.CurrentCulture, "Factory for protocol '{0}' cannot be unregistered, it has {1} pending sinks", new object[] { this.m_factory, this.m_createdSinkCount });
			}
		}

		// Token: 0x06001C42 RID: 7234 RVA: 0x0006B970 File Offset: 0x00069B70
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			if (info != null)
			{
				info.AddValue("sink_factory", this.m_factory, typeof(ISinkFactory));
				info.AddValue("created_sink_count", this.m_createdSinkCount, typeof(int));
			}
		}

		// Token: 0x04000982 RID: 2434
		private ISinkFactory m_factory;

		// Token: 0x04000983 RID: 2435
		private int m_createdSinkCount;
	}
}
