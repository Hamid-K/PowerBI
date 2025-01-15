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
	// Token: 0x0200037F RID: 895
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class UnloadingPerformanceCountersException : MonitoredException
	{
		// Token: 0x06001B54 RID: 6996 RVA: 0x00066EDC File Offset: 0x000650DC
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06001B55 RID: 6997 RVA: 0x00066EE4 File Offset: 0x000650E4
		// (set) Token: 0x06001B56 RID: 6998 RVA: 0x00066EEC File Offset: 0x000650EC
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

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06001B57 RID: 6999 RVA: 0x00066EF5 File Offset: 0x000650F5
		// (set) Token: 0x06001B58 RID: 7000 RVA: 0x00066EFD File Offset: 0x000650FD
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

		// Token: 0x06001B59 RID: 7001 RVA: 0x00066F06 File Offset: 0x00065106
		public UnloadingPerformanceCountersException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidValueField<int>();
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06001B5A RID: 7002 RVA: 0x00066F1F File Offset: 0x0006511F
		public UnloadingPerformanceCountersException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001B5B RID: 7003 RVA: 0x00066F36 File Offset: 0x00065136
		public UnloadingPerformanceCountersException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001B5C RID: 7004 RVA: 0x00066F54 File Offset: 0x00065154
		protected UnloadingPerformanceCountersException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("UnloadingPerformanceCountersException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			this.ExitCode = (int)info.GetValue("UnloadingPerformanceCountersException_ExitCode", typeof(int));
			try
			{
				this.Output = (string)info.GetValue("UnloadingPerformanceCountersException_Output", typeof(string));
			}
			catch (SerializationException)
			{
				this.Output = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("UnloadingPerformanceCountersException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06001B5D RID: 7005 RVA: 0x00067048 File Offset: 0x00065248
		public UnloadingPerformanceCountersException(int exitCode, string output)
		{
			this.ExitCode = exitCode;
			this.Output = output;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001B5E RID: 7006 RVA: 0x00067065 File Offset: 0x00065265
		public UnloadingPerformanceCountersException(int exitCode, string output, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ExitCode = exitCode;
			this.Output = output;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001B5F RID: 7007 RVA: 0x0006708A File Offset: 0x0006528A
		public UnloadingPerformanceCountersException(int exitCode, string output, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ExitCode = exitCode;
			this.Output = output;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001B60 RID: 7008 RVA: 0x000670B8 File Offset: 0x000652B8
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06001B61 RID: 7009 RVA: 0x000670EF File Offset: 0x000652EF
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06001B62 RID: 7010 RVA: 0x000670F8 File Offset: 0x000652F8
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(UnloadingPerformanceCountersException))
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

		// Token: 0x06001B63 RID: 7011 RVA: 0x000671C8 File Offset: 0x000653C8
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("UnloadingPerformanceCountersException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("UnloadingPerformanceCountersException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			info.AddValue("UnloadingPerformanceCountersException_ExitCode", this.ExitCode, typeof(int));
			if (this.Output != null)
			{
				info.AddValue("UnloadingPerformanceCountersException_Output", this.Output, typeof(string));
			}
		}

		// Token: 0x06001B64 RID: 7012 RVA: 0x00067268 File Offset: 0x00065468
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Unloading performance counters V2 failed with exit code {0}: {1}.", new object[]
			{
				(markupKind == PrivateInformationMarkupKind.None) ? this.ExitCode.ToString(CultureInfo.InvariantCulture) : ((markupKind == PrivateInformationMarkupKind.Internal) ? this.ExitCode.ToString(CultureInfo.InvariantCulture) : this.ExitCode.ToString(CultureInfo.InvariantCulture)),
				(markupKind == PrivateInformationMarkupKind.None) ? ((this.Output != null) ? this.Output.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.Output != null) ? this.Output.MarkIfInternal() : string.Empty) : ((this.Output != null) ? this.Output.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty))
			});
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06001B65 RID: 7013 RVA: 0x00067333 File Offset: 0x00065533
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

		// Token: 0x06001B66 RID: 7014 RVA: 0x00067350 File Offset: 0x00065550
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "ExitCode={0}", new object[] { this.ExitCode.ToString(CultureInfo.InvariantCulture) }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "ExitCode={0}", new object[] { this.ExitCode.ToString(CultureInfo.InvariantCulture) }) : string.Format(CultureInfo.CurrentCulture, "ExitCode={0}", new object[] { this.ExitCode.ToString(CultureInfo.InvariantCulture) })));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Output={0}", new object[] { (this.Output != null) ? this.Output.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Output={0}", new object[] { (this.Output != null) ? this.Output.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "Output={0}", new object[] { (this.Output != null) ? this.Output.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
		}

		// Token: 0x06001B67 RID: 7015 RVA: 0x000674D7 File Offset: 0x000656D7
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06001B68 RID: 7016 RVA: 0x000674E0 File Offset: 0x000656E0
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06001B69 RID: 7017 RVA: 0x000674E9 File Offset: 0x000656E9
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06001B6A RID: 7018 RVA: 0x000674D7 File Offset: 0x000656D7
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06001B6B RID: 7019 RVA: 0x000674F4 File Offset: 0x000656F4
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

		// Token: 0x04000940 RID: 2368
		private string creationMessage;

		// Token: 0x04000941 RID: 2369
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x04000942 RID: 2370
		private int m_exitCode;

		// Token: 0x04000943 RID: 2371
		private string m_output;
	}
}
