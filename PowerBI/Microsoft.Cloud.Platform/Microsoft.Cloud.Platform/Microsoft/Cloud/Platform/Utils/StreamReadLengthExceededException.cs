using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000323 RID: 803
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class StreamReadLengthExceededException : MonitoredException
	{
		// Token: 0x06001772 RID: 6002 RVA: 0x00056080 File Offset: 0x00054280
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06001773 RID: 6003 RVA: 0x00056088 File Offset: 0x00054288
		// (set) Token: 0x06001774 RID: 6004 RVA: 0x00056090 File Offset: 0x00054290
		public long BytesRead
		{
			get
			{
				return this.m_bytesRead;
			}
			protected set
			{
				this.m_bytesRead = value;
			}
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06001775 RID: 6005 RVA: 0x00056099 File Offset: 0x00054299
		// (set) Token: 0x06001776 RID: 6006 RVA: 0x000560A1 File Offset: 0x000542A1
		public long MaxReadLength
		{
			get
			{
				return this.m_maxReadLength;
			}
			protected set
			{
				this.m_maxReadLength = value;
			}
		}

		// Token: 0x06001777 RID: 6007 RVA: 0x000560AA File Offset: 0x000542AA
		public StreamReadLengthExceededException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidValueField<long>();
			CompileCheck.IsValidValueField<long>();
		}

		// Token: 0x06001778 RID: 6008 RVA: 0x000560C3 File Offset: 0x000542C3
		public StreamReadLengthExceededException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001779 RID: 6009 RVA: 0x000560DA File Offset: 0x000542DA
		public StreamReadLengthExceededException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600177A RID: 6010 RVA: 0x000560F8 File Offset: 0x000542F8
		protected StreamReadLengthExceededException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("StreamReadLengthExceededException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			this.BytesRead = (long)info.GetValue("StreamReadLengthExceededException_BytesRead", typeof(long));
			this.MaxReadLength = (long)info.GetValue("StreamReadLengthExceededException_MaxReadLength", typeof(long));
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("StreamReadLengthExceededException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x0600177B RID: 6011 RVA: 0x000561D4 File Offset: 0x000543D4
		public StreamReadLengthExceededException(long bytesRead, long maxReadLength)
		{
			this.BytesRead = bytesRead;
			this.MaxReadLength = maxReadLength;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600177C RID: 6012 RVA: 0x000561F1 File Offset: 0x000543F1
		public StreamReadLengthExceededException(long bytesRead, long maxReadLength, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.BytesRead = bytesRead;
			this.MaxReadLength = maxReadLength;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600177D RID: 6013 RVA: 0x00056216 File Offset: 0x00054416
		public StreamReadLengthExceededException(long bytesRead, long maxReadLength, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.BytesRead = bytesRead;
			this.MaxReadLength = maxReadLength;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600177E RID: 6014 RVA: 0x00056244 File Offset: 0x00054444
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x0600177F RID: 6015 RVA: 0x0005627B File Offset: 0x0005447B
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06001780 RID: 6016 RVA: 0x00056284 File Offset: 0x00054484
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(StreamReadLengthExceededException))
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

		// Token: 0x06001781 RID: 6017 RVA: 0x00056354 File Offset: 0x00054554
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("StreamReadLengthExceededException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("StreamReadLengthExceededException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			info.AddValue("StreamReadLengthExceededException_BytesRead", this.BytesRead, typeof(long));
			info.AddValue("StreamReadLengthExceededException_MaxReadLength", this.MaxReadLength, typeof(long));
		}

		// Token: 0x06001782 RID: 6018 RVA: 0x000563F0 File Offset: 0x000545F0
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Failed to read '{0}' bytes from stream. The maximum number of bytes allowed to read is '{1}'.", new object[]
			{
				(markupKind == PrivateInformationMarkupKind.None) ? this.BytesRead.ToString(CultureInfo.InvariantCulture) : ((markupKind == PrivateInformationMarkupKind.Internal) ? this.BytesRead.ToString(CultureInfo.InvariantCulture) : this.BytesRead.ToString(CultureInfo.InvariantCulture)),
				(markupKind == PrivateInformationMarkupKind.None) ? this.MaxReadLength.ToString(CultureInfo.InvariantCulture) : ((markupKind == PrivateInformationMarkupKind.Internal) ? this.MaxReadLength.ToString(CultureInfo.InvariantCulture) : this.MaxReadLength.ToString(CultureInfo.InvariantCulture))
			});
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06001783 RID: 6019 RVA: 0x000564A0 File Offset: 0x000546A0
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

		// Token: 0x06001784 RID: 6020 RVA: 0x000564C0 File Offset: 0x000546C0
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "BytesRead={0}", new object[] { this.BytesRead.ToString(CultureInfo.InvariantCulture) }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "BytesRead={0}", new object[] { this.BytesRead.ToString(CultureInfo.InvariantCulture) }) : string.Format(CultureInfo.CurrentCulture, "BytesRead={0}", new object[] { this.BytesRead.ToString(CultureInfo.InvariantCulture) })));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "MaxReadLength={0}", new object[] { this.MaxReadLength.ToString(CultureInfo.InvariantCulture) }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "MaxReadLength={0}", new object[] { this.MaxReadLength.ToString(CultureInfo.InvariantCulture) }) : string.Format(CultureInfo.CurrentCulture, "MaxReadLength={0}", new object[] { this.MaxReadLength.ToString(CultureInfo.InvariantCulture) })));
		}

		// Token: 0x06001785 RID: 6021 RVA: 0x0005662C File Offset: 0x0005482C
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06001786 RID: 6022 RVA: 0x00056635 File Offset: 0x00054835
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06001787 RID: 6023 RVA: 0x0005663E File Offset: 0x0005483E
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06001788 RID: 6024 RVA: 0x0005662C File Offset: 0x0005482C
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06001789 RID: 6025 RVA: 0x00056648 File Offset: 0x00054848
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

		// Token: 0x04000827 RID: 2087
		private string creationMessage;

		// Token: 0x04000828 RID: 2088
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x04000829 RID: 2089
		private long m_bytesRead;

		// Token: 0x0400082A RID: 2090
		private long m_maxReadLength;
	}
}
