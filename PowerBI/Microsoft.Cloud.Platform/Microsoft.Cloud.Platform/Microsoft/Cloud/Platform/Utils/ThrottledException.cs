using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000324 RID: 804
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public abstract class ThrottledException : MonitoredException
	{
		// Token: 0x0600178A RID: 6026 RVA: 0x00056834 File Offset: 0x00054A34
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x0600178B RID: 6027 RVA: 0x0005683C File Offset: 0x00054A3C
		// (set) Token: 0x0600178C RID: 6028 RVA: 0x00056844 File Offset: 0x00054A44
		public string Key
		{
			get
			{
				return this.m_key;
			}
			protected set
			{
				this.m_key = value;
			}
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x0600178D RID: 6029 RVA: 0x0005684D File Offset: 0x00054A4D
		// (set) Token: 0x0600178E RID: 6030 RVA: 0x00056855 File Offset: 0x00054A55
		public int RetryAfterSeconds
		{
			get
			{
				return this.m_retryAfterSeconds;
			}
			protected set
			{
				this.m_retryAfterSeconds = value;
			}
		}

		// Token: 0x0600178F RID: 6031 RVA: 0x0005685E File Offset: 0x00054A5E
		public ThrottledException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
			CompileCheck.IsValidValueField<int>();
		}

		// Token: 0x06001790 RID: 6032 RVA: 0x00056877 File Offset: 0x00054A77
		public ThrottledException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001791 RID: 6033 RVA: 0x0005688E File Offset: 0x00054A8E
		public ThrottledException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001792 RID: 6034 RVA: 0x000568AC File Offset: 0x00054AAC
		protected ThrottledException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("ThrottledException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.Key = (string)info.GetValue("ThrottledException_Key", typeof(string));
			}
			catch (SerializationException)
			{
				this.Key = null;
			}
			this.RetryAfterSeconds = (int)info.GetValue("ThrottledException_RetryAfterSeconds", typeof(int));
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("ThrottledException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06001793 RID: 6035 RVA: 0x000569A0 File Offset: 0x00054BA0
		public ThrottledException(string key, int retryAfterSeconds)
		{
			this.Key = key;
			this.RetryAfterSeconds = retryAfterSeconds;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001794 RID: 6036 RVA: 0x000569BD File Offset: 0x00054BBD
		public ThrottledException(string key, int retryAfterSeconds, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.Key = key;
			this.RetryAfterSeconds = retryAfterSeconds;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001795 RID: 6037 RVA: 0x000569E2 File Offset: 0x00054BE2
		public ThrottledException(string key, int retryAfterSeconds, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.Key = key;
			this.RetryAfterSeconds = retryAfterSeconds;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001796 RID: 6038 RVA: 0x00056A10 File Offset: 0x00054C10
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06001797 RID: 6039 RVA: 0x00056A47 File Offset: 0x00054C47
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06001798 RID: 6040 RVA: 0x00056A50 File Offset: 0x00054C50
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(ThrottledException))
			{
				TraceSourceBase<UtilsTrace>.Tracer.TraceError("Exception object created: {0}: {1}{2}", new object[]
				{
					type,
					this.Message,
					(base.InnerException != null) ? "(wrapped: {0}/{1})".FormatWithInvariantCulture(new object[]
					{
						base.InnerException.GetType().Name,
						base.InnerException.Message
					}) : string.Empty
				});
				if (TraceSourceBase<UtilsTrace>.Tracer.ShouldTrace(TraceVerbosity.Error) && (base.InnerException == null || !(base.InnerException is IMonitoredError)))
				{
					TraceSourceBase<UtilsTrace>.Tracer.TraceError("StackTrace: {0}", new object[]
					{
						new StackTrace(0, true)
					});
				}
			}
		}

		// Token: 0x06001799 RID: 6041 RVA: 0x00056B20 File Offset: 0x00054D20
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("ThrottledException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("ThrottledException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.Key != null)
			{
				info.AddValue("ThrottledException_Key", this.Key, typeof(string));
			}
			info.AddValue("ThrottledException_RetryAfterSeconds", this.RetryAfterSeconds, typeof(int));
		}

		// Token: 0x0600179A RID: 6042 RVA: 0x00056BC0 File Offset: 0x00054DC0
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "The request for '{0}' was throttled. Further requests will fail until '{1}' seconds.", new object[]
			{
				(markupKind == PrivateInformationMarkupKind.None) ? ((this.Key != null) ? this.Key.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.Key != null) ? this.Key.MarkIfInternal() : string.Empty) : ((this.Key != null) ? this.Key.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)),
				(markupKind == PrivateInformationMarkupKind.None) ? this.RetryAfterSeconds.ToString(CultureInfo.InvariantCulture) : ((markupKind == PrivateInformationMarkupKind.Internal) ? this.RetryAfterSeconds.ToString(CultureInfo.InvariantCulture) : this.RetryAfterSeconds.ToString(CultureInfo.InvariantCulture))
			});
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x0600179B RID: 6043 RVA: 0x00056C8B File Offset: 0x00054E8B
		public override string Message
		{
			get
			{
				if (!string.IsNullOrEmpty(this.creationMessage))
				{
					return this.creationMessage;
				}
				return this.CreateMessageFromTemplate(PrivateInformationMarkupKind.None);
			}
		}

		// Token: 0x0600179C RID: 6044 RVA: 0x00056CA8 File Offset: 0x00054EA8
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Key={0}", new object[] { (this.Key != null) ? this.Key.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Key={0}", new object[] { (this.Key != null) ? this.Key.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "Key={0}", new object[] { (this.Key != null) ? this.Key.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "RetryAfterSeconds={0}", new object[] { this.RetryAfterSeconds.ToString(CultureInfo.InvariantCulture) }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "RetryAfterSeconds={0}", new object[] { this.RetryAfterSeconds.ToString(CultureInfo.InvariantCulture) }) : string.Format(CultureInfo.CurrentCulture, "RetryAfterSeconds={0}", new object[] { this.RetryAfterSeconds.ToString(CultureInfo.InvariantCulture) })));
		}

		// Token: 0x0600179D RID: 6045 RVA: 0x00056E2F File Offset: 0x0005502F
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x0600179E RID: 6046 RVA: 0x00056E38 File Offset: 0x00055038
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x0600179F RID: 6047 RVA: 0x00056E41 File Offset: 0x00055041
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x060017A0 RID: 6048 RVA: 0x00056E2F File Offset: 0x0005502F
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060017A1 RID: 6049 RVA: 0x00056E4C File Offset: 0x0005504C
		private string ToString(PrivateInformationMarkupKind markupKind)
		{
			string text = "[" + ExceptionsTemplateHelper.MagicLevel.ToString(CultureInfo.CurrentCulture) + "]" + base.GetType().FullName;
			string text2 = this.CreateMessageFromTemplate(markupKind);
			string text3 = text + ": ";
			if (string.IsNullOrEmpty(this.creationMessage))
			{
				text3 += text2;
			}
			else
			{
				if (markupKind == PrivateInformationMarkupKind.Private || markupKind == PrivateInformationMarkupKind.Internal)
				{
					text3 += this.creationMessage.ObfuscatePrivateValue(true);
				}
				else
				{
					text3 += this.creationMessage;
				}
				if (!string.Equals(this.creationMessage, text2))
				{
					text3 = text3 + Environment.NewLine + "  TemplateMessage: " + text2;
				}
			}
			text3 += this.GetPropertiesString(markupKind);
			text3 = text3 + Environment.NewLine + "ExceptionCulprit=" + this.exceptionCulprit.ToString();
			if (base.InnerException != null)
			{
				try
				{
					ExceptionsTemplateHelper.IncrementMagicLevel();
					IMonitoredError monitoredError = base.InnerException as MonitoredException;
					string text4;
					if (markupKind != PrivateInformationMarkupKind.None)
					{
						if (markupKind != PrivateInformationMarkupKind.Internal)
						{
							text4 = ((monitoredError == null) ? base.InnerException.MarkIfPrivate() : monitoredError.ToPrivateString());
							text4 = text4.ObfuscatePrivateValue(true);
						}
						else
						{
							text4 = ((monitoredError == null) ? base.InnerException.MarkIfInternal() : monitoredError.ToInternalString());
						}
					}
					else
					{
						text4 = ((monitoredError == null) ? base.InnerException.ToString() : monitoredError.ToOriginalString());
					}
					text3 = string.Concat(new string[]
					{
						text3,
						" --->",
						Environment.NewLine,
						text4,
						Environment.NewLine,
						"   --- End of inner exception stack trace ---",
						Environment.NewLine,
						"  (",
						text,
						".StackTrace:)"
					});
				}
				finally
				{
					ExceptionsTemplateHelper.DecrementMagicLevel();
				}
			}
			if (this.StackTrace != null)
			{
				text3 = text3 + Environment.NewLine + this.StackTrace;
			}
			return text3;
		}

		// Token: 0x0400082B RID: 2091
		private string creationMessage;

		// Token: 0x0400082C RID: 2092
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x0400082D RID: 2093
		private string m_key;

		// Token: 0x0400082E RID: 2094
		private int m_retryAfterSeconds;
	}
}
