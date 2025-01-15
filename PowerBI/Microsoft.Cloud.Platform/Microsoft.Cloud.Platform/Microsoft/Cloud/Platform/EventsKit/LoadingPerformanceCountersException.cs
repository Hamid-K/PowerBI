using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Eventing.Base;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.EventsKit
{
	// Token: 0x02000380 RID: 896
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class LoadingPerformanceCountersException : MonitoredException
	{
		// Token: 0x06001B6C RID: 7020 RVA: 0x000676E0 File Offset: 0x000658E0
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06001B6D RID: 7021 RVA: 0x000676E8 File Offset: 0x000658E8
		// (set) Token: 0x06001B6E RID: 7022 RVA: 0x000676F0 File Offset: 0x000658F0
		public int ExitCode
		{
			get
			{
				return this.m_exitCode;
			}
			protected set
			{
				this.m_exitCode = value;
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06001B6F RID: 7023 RVA: 0x000676F9 File Offset: 0x000658F9
		// (set) Token: 0x06001B70 RID: 7024 RVA: 0x00067701 File Offset: 0x00065901
		public string Output
		{
			get
			{
				return this.m_output;
			}
			protected set
			{
				this.m_output = value;
			}
		}

		// Token: 0x06001B71 RID: 7025 RVA: 0x0006770A File Offset: 0x0006590A
		public LoadingPerformanceCountersException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidValueField<int>();
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06001B72 RID: 7026 RVA: 0x00067723 File Offset: 0x00065923
		public LoadingPerformanceCountersException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001B73 RID: 7027 RVA: 0x0006773A File Offset: 0x0006593A
		public LoadingPerformanceCountersException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001B74 RID: 7028 RVA: 0x00067758 File Offset: 0x00065958
		protected LoadingPerformanceCountersException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("LoadingPerformanceCountersException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			this.ExitCode = (int)info.GetValue("LoadingPerformanceCountersException_ExitCode", typeof(int));
			try
			{
				this.Output = (string)info.GetValue("LoadingPerformanceCountersException_Output", typeof(string));
			}
			catch (SerializationException)
			{
				this.Output = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("LoadingPerformanceCountersException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06001B75 RID: 7029 RVA: 0x0006784C File Offset: 0x00065A4C
		public LoadingPerformanceCountersException(int exitCode, string output)
		{
			this.ExitCode = exitCode;
			this.Output = output;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001B76 RID: 7030 RVA: 0x00067869 File Offset: 0x00065A69
		public LoadingPerformanceCountersException(int exitCode, string output, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ExitCode = exitCode;
			this.Output = output;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001B77 RID: 7031 RVA: 0x0006788E File Offset: 0x00065A8E
		public LoadingPerformanceCountersException(int exitCode, string output, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ExitCode = exitCode;
			this.Output = output;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001B78 RID: 7032 RVA: 0x000678BC File Offset: 0x00065ABC
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06001B79 RID: 7033 RVA: 0x000678F3 File Offset: 0x00065AF3
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06001B7A RID: 7034 RVA: 0x000678FC File Offset: 0x00065AFC
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(LoadingPerformanceCountersException))
			{
				TraceSourceBase<EventingTrace>.Tracer.TraceError("Exception object created: {0}: {1}{2}", new object[]
				{
					type,
					this.Message,
					(base.InnerException != null) ? "(wrapped: {0}/{1})".FormatWithInvariantCulture(new object[]
					{
						base.InnerException.GetType().Name,
						base.InnerException.Message
					}) : string.Empty
				});
				if (TraceSourceBase<EventingTrace>.Tracer.ShouldTrace(TraceVerbosity.Error) && (base.InnerException == null || !(base.InnerException is IMonitoredError)))
				{
					TraceSourceBase<EventingTrace>.Tracer.TraceError("StackTrace: {0}", new object[]
					{
						new StackTrace(0, true)
					});
				}
			}
		}

		// Token: 0x06001B7B RID: 7035 RVA: 0x000679CC File Offset: 0x00065BCC
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("LoadingPerformanceCountersException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("LoadingPerformanceCountersException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			info.AddValue("LoadingPerformanceCountersException_ExitCode", this.ExitCode, typeof(int));
			if (this.Output != null)
			{
				info.AddValue("LoadingPerformanceCountersException_Output", this.Output, typeof(string));
			}
		}

		// Token: 0x06001B7C RID: 7036 RVA: 0x00067A6C File Offset: 0x00065C6C
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Loading performance counters V2 failed with exit code {0}: {1}.", new object[]
			{
				(markupKind == PrivateInformationMarkupKind.None) ? this.ExitCode.ToString(CultureInfo.InvariantCulture) : ((markupKind == PrivateInformationMarkupKind.Internal) ? this.ExitCode.ToString(CultureInfo.InvariantCulture) : this.ExitCode.ToString(CultureInfo.InvariantCulture)),
				(markupKind == PrivateInformationMarkupKind.None) ? ((this.Output != null) ? this.Output.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.Output != null) ? this.Output.MarkIfInternal() : string.Empty) : ((this.Output != null) ? this.Output.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty))
			});
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06001B7D RID: 7037 RVA: 0x00067B37 File Offset: 0x00065D37
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

		// Token: 0x06001B7E RID: 7038 RVA: 0x00067B54 File Offset: 0x00065D54
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "ExitCode={0}", new object[] { this.ExitCode.ToString(CultureInfo.InvariantCulture) }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "ExitCode={0}", new object[] { this.ExitCode.ToString(CultureInfo.InvariantCulture) }) : string.Format(CultureInfo.CurrentCulture, "ExitCode={0}", new object[] { this.ExitCode.ToString(CultureInfo.InvariantCulture) })));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Output={0}", new object[] { (this.Output != null) ? this.Output.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Output={0}", new object[] { (this.Output != null) ? this.Output.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "Output={0}", new object[] { (this.Output != null) ? this.Output.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
		}

		// Token: 0x06001B7F RID: 7039 RVA: 0x00067CDB File Offset: 0x00065EDB
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06001B80 RID: 7040 RVA: 0x00067CE4 File Offset: 0x00065EE4
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06001B81 RID: 7041 RVA: 0x00067CED File Offset: 0x00065EED
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06001B82 RID: 7042 RVA: 0x00067CDB File Offset: 0x00065EDB
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06001B83 RID: 7043 RVA: 0x00067CF8 File Offset: 0x00065EF8
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

		// Token: 0x04000944 RID: 2372
		private string creationMessage;

		// Token: 0x04000945 RID: 2373
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x04000946 RID: 2374
		private int m_exitCode;

		// Token: 0x04000947 RID: 2375
		private string m_output;
	}
}
