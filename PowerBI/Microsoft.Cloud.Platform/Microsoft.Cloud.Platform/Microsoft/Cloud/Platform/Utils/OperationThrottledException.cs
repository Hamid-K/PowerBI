using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000325 RID: 805
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class OperationThrottledException : ThrottledException
	{
		// Token: 0x060017A2 RID: 6050 RVA: 0x00057038 File Offset: 0x00055238
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x060017A3 RID: 6051 RVA: 0x00057040 File Offset: 0x00055240
		public OperationThrottledException()
		{
			this.ConstructorInternal(false);
		}

		// Token: 0x060017A4 RID: 6052 RVA: 0x0005704F File Offset: 0x0005524F
		public OperationThrottledException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060017A5 RID: 6053 RVA: 0x00057066 File Offset: 0x00055266
		public OperationThrottledException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060017A6 RID: 6054 RVA: 0x00057084 File Offset: 0x00055284
		protected OperationThrottledException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("OperationThrottledException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("OperationThrottledException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x060017A7 RID: 6055 RVA: 0x00057120 File Offset: 0x00055320
		public OperationThrottledException(string key, int retryAfterSeconds, string message, Exception innerException)
			: base(key, retryAfterSeconds, message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060017A8 RID: 6056 RVA: 0x00057140 File Offset: 0x00055340
		public OperationThrottledException(string key, int retryAfterSeconds, string message)
			: base(key, retryAfterSeconds, message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060017A9 RID: 6057 RVA: 0x0005715C File Offset: 0x0005535C
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x060017AA RID: 6058 RVA: 0x00057193 File Offset: 0x00055393
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x060017AB RID: 6059 RVA: 0x000034FD File Offset: 0x000016FD
		public override bool IsBenign()
		{
			return true;
		}

		// Token: 0x060017AC RID: 6060 RVA: 0x0005719C File Offset: 0x0005539C
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(OperationThrottledException))
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

		// Token: 0x060017AD RID: 6061 RVA: 0x0005726C File Offset: 0x0005546C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("OperationThrottledException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("OperationThrottledException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
		}

		// Token: 0x060017AE RID: 6062 RVA: 0x000572C7 File Offset: 0x000554C7
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "You have exceeded the amount of requests allowed in the current time frame and further requests will fail.", new object[0]);
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x060017AF RID: 6063 RVA: 0x000572DE File Offset: 0x000554DE
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

		// Token: 0x060017B0 RID: 6064 RVA: 0x000572FB File Offset: 0x000554FB
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string newLine = Environment.NewLine;
			return base.GetPropertiesString(markupKind);
		}

		// Token: 0x060017B1 RID: 6065 RVA: 0x0005730A File Offset: 0x0005550A
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060017B2 RID: 6066 RVA: 0x00057313 File Offset: 0x00055513
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x060017B3 RID: 6067 RVA: 0x0005731C File Offset: 0x0005551C
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x060017B4 RID: 6068 RVA: 0x0005730A File Offset: 0x0005550A
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060017B5 RID: 6069 RVA: 0x00057328 File Offset: 0x00055528
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

		// Token: 0x0400082F RID: 2095
		private string creationMessage;

		// Token: 0x04000830 RID: 2096
		private ExceptionCulprit exceptionCulprit;
	}
}
