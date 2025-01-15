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
	// Token: 0x02000382 RID: 898
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class CompilePerformanceCountersResourceFileToCompiledResourceFileException : MonitoredException
	{
		// Token: 0x06001BA2 RID: 7074 RVA: 0x00068C04 File Offset: 0x00066E04
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06001BA3 RID: 7075 RVA: 0x00068C0C File Offset: 0x00066E0C
		// (set) Token: 0x06001BA4 RID: 7076 RVA: 0x00068C14 File Offset: 0x00066E14
		public string RcCommand
		{
			get
			{
				return this.m_rcCommand;
			}
			protected set
			{
				this.m_rcCommand = value;
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06001BA5 RID: 7077 RVA: 0x00068C1D File Offset: 0x00066E1D
		// (set) Token: 0x06001BA6 RID: 7078 RVA: 0x00068C25 File Offset: 0x00066E25
		public string RcCommandArguments
		{
			get
			{
				return this.m_rcCommandArguments;
			}
			protected set
			{
				this.m_rcCommandArguments = value;
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06001BA7 RID: 7079 RVA: 0x00068C2E File Offset: 0x00066E2E
		// (set) Token: 0x06001BA8 RID: 7080 RVA: 0x00068C36 File Offset: 0x00066E36
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

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06001BA9 RID: 7081 RVA: 0x00068C3F File Offset: 0x00066E3F
		// (set) Token: 0x06001BAA RID: 7082 RVA: 0x00068C47 File Offset: 0x00066E47
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

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06001BAB RID: 7083 RVA: 0x00068C50 File Offset: 0x00066E50
		// (set) Token: 0x06001BAC RID: 7084 RVA: 0x00068C58 File Offset: 0x00066E58
		public string Error
		{
			get
			{
				return this.m_error;
			}
			protected set
			{
				this.m_error = value;
			}
		}

		// Token: 0x06001BAD RID: 7085 RVA: 0x00068C61 File Offset: 0x00066E61
		public CompilePerformanceCountersResourceFileToCompiledResourceFileException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
			CompileCheck.IsValidReferenceField<string>();
			CompileCheck.IsValidValueField<int>();
			CompileCheck.IsValidReferenceField<string>();
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06001BAE RID: 7086 RVA: 0x00068C89 File Offset: 0x00066E89
		public CompilePerformanceCountersResourceFileToCompiledResourceFileException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001BAF RID: 7087 RVA: 0x00068CA0 File Offset: 0x00066EA0
		public CompilePerformanceCountersResourceFileToCompiledResourceFileException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001BB0 RID: 7088 RVA: 0x00068CC0 File Offset: 0x00066EC0
		protected CompilePerformanceCountersResourceFileToCompiledResourceFileException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("CompilePerformanceCountersResourceFileToCompiledResourceFileException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.RcCommand = (string)info.GetValue("CompilePerformanceCountersResourceFileToCompiledResourceFileException_RcCommand", typeof(string));
			}
			catch (SerializationException)
			{
				this.RcCommand = null;
			}
			try
			{
				this.RcCommandArguments = (string)info.GetValue("CompilePerformanceCountersResourceFileToCompiledResourceFileException_RcCommandArguments", typeof(string));
			}
			catch (SerializationException)
			{
				this.RcCommandArguments = null;
			}
			this.ExitCode = (int)info.GetValue("CompilePerformanceCountersResourceFileToCompiledResourceFileException_ExitCode", typeof(int));
			try
			{
				this.Output = (string)info.GetValue("CompilePerformanceCountersResourceFileToCompiledResourceFileException_Output", typeof(string));
			}
			catch (SerializationException)
			{
				this.Output = null;
			}
			try
			{
				this.Error = (string)info.GetValue("CompilePerformanceCountersResourceFileToCompiledResourceFileException_Error", typeof(string));
			}
			catch (SerializationException)
			{
				this.Error = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("CompilePerformanceCountersResourceFileToCompiledResourceFileException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06001BB1 RID: 7089 RVA: 0x00068E60 File Offset: 0x00067060
		public CompilePerformanceCountersResourceFileToCompiledResourceFileException(string rcCommand, string rcCommandArguments, int exitCode, string output, string error)
		{
			this.RcCommand = rcCommand;
			this.RcCommandArguments = rcCommandArguments;
			this.ExitCode = exitCode;
			this.Output = output;
			this.Error = error;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001BB2 RID: 7090 RVA: 0x00068E94 File Offset: 0x00067094
		public CompilePerformanceCountersResourceFileToCompiledResourceFileException(string rcCommand, string rcCommandArguments, int exitCode, string output, string error, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.RcCommand = rcCommand;
			this.RcCommandArguments = rcCommandArguments;
			this.ExitCode = exitCode;
			this.Output = output;
			this.Error = error;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001BB3 RID: 7091 RVA: 0x00068ED4 File Offset: 0x000670D4
		public CompilePerformanceCountersResourceFileToCompiledResourceFileException(string rcCommand, string rcCommandArguments, int exitCode, string output, string error, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.RcCommand = rcCommand;
			this.RcCommandArguments = rcCommandArguments;
			this.ExitCode = exitCode;
			this.Output = output;
			this.Error = error;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001BB4 RID: 7092 RVA: 0x00068F24 File Offset: 0x00067124
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06001BB5 RID: 7093 RVA: 0x00068F5B File Offset: 0x0006715B
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06001BB6 RID: 7094 RVA: 0x00068F64 File Offset: 0x00067164
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(CompilePerformanceCountersResourceFileToCompiledResourceFileException))
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

		// Token: 0x06001BB7 RID: 7095 RVA: 0x00069034 File Offset: 0x00067234
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("CompilePerformanceCountersResourceFileToCompiledResourceFileException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("CompilePerformanceCountersResourceFileToCompiledResourceFileException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.RcCommand != null)
			{
				info.AddValue("CompilePerformanceCountersResourceFileToCompiledResourceFileException_RcCommand", this.RcCommand, typeof(string));
			}
			if (this.RcCommandArguments != null)
			{
				info.AddValue("CompilePerformanceCountersResourceFileToCompiledResourceFileException_RcCommandArguments", this.RcCommandArguments, typeof(string));
			}
			info.AddValue("CompilePerformanceCountersResourceFileToCompiledResourceFileException_ExitCode", this.ExitCode, typeof(int));
			if (this.Output != null)
			{
				info.AddValue("CompilePerformanceCountersResourceFileToCompiledResourceFileException_Output", this.Output, typeof(string));
			}
			if (this.Error != null)
			{
				info.AddValue("CompilePerformanceCountersResourceFileToCompiledResourceFileException_Error", this.Error, typeof(string));
			}
		}

		// Token: 0x06001BB8 RID: 7096 RVA: 0x0006913C File Offset: 0x0006733C
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Compiling the performance counters resource file into a compiled resource file has failed. {0} {1} has exit code {2}. Output: {3}. Error: {4}.", new object[]
			{
				(markupKind == PrivateInformationMarkupKind.None) ? ((this.RcCommand != null) ? this.RcCommand.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.RcCommand != null) ? this.RcCommand.MarkIfInternal() : string.Empty) : ((this.RcCommand != null) ? this.RcCommand.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)),
				(markupKind == PrivateInformationMarkupKind.None) ? ((this.RcCommandArguments != null) ? this.RcCommandArguments.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.RcCommandArguments != null) ? this.RcCommandArguments.MarkIfInternal() : string.Empty) : ((this.RcCommandArguments != null) ? this.RcCommandArguments.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)),
				(markupKind == PrivateInformationMarkupKind.None) ? this.ExitCode.ToString(CultureInfo.InvariantCulture) : ((markupKind == PrivateInformationMarkupKind.Internal) ? this.ExitCode.ToString(CultureInfo.InvariantCulture) : this.ExitCode.ToString(CultureInfo.InvariantCulture)),
				(markupKind == PrivateInformationMarkupKind.None) ? ((this.Output != null) ? this.Output.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.Output != null) ? this.Output.MarkIfInternal() : string.Empty) : ((this.Output != null) ? this.Output.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)),
				(markupKind == PrivateInformationMarkupKind.None) ? ((this.Error != null) ? this.Error.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.Error != null) ? this.Error.MarkIfInternal() : string.Empty) : ((this.Error != null) ? this.Error.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty))
			});
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06001BB9 RID: 7097 RVA: 0x0006932D File Offset: 0x0006752D
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

		// Token: 0x06001BBA RID: 7098 RVA: 0x0006934C File Offset: 0x0006754C
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "RcCommand={0}", new object[] { (this.RcCommand != null) ? this.RcCommand.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "RcCommand={0}", new object[] { (this.RcCommand != null) ? this.RcCommand.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "RcCommand={0}", new object[] { (this.RcCommand != null) ? this.RcCommand.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "RcCommandArguments={0}", new object[] { (this.RcCommandArguments != null) ? this.RcCommandArguments.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "RcCommandArguments={0}", new object[] { (this.RcCommandArguments != null) ? this.RcCommandArguments.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "RcCommandArguments={0}", new object[] { (this.RcCommandArguments != null) ? this.RcCommandArguments.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "ExitCode={0}", new object[] { this.ExitCode.ToString(CultureInfo.InvariantCulture) }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "ExitCode={0}", new object[] { this.ExitCode.ToString(CultureInfo.InvariantCulture) }) : string.Format(CultureInfo.CurrentCulture, "ExitCode={0}", new object[] { this.ExitCode.ToString(CultureInfo.InvariantCulture) })));
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Output={0}", new object[] { (this.Output != null) ? this.Output.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Output={0}", new object[] { (this.Output != null) ? this.Output.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "Output={0}", new object[] { (this.Output != null) ? this.Output.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Error={0}", new object[] { (this.Error != null) ? this.Error.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Error={0}", new object[] { (this.Error != null) ? this.Error.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "Error={0}", new object[] { (this.Error != null) ? this.Error.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
		}

		// Token: 0x06001BBB RID: 7099 RVA: 0x0006971C File Offset: 0x0006791C
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06001BBC RID: 7100 RVA: 0x00069725 File Offset: 0x00067925
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06001BBD RID: 7101 RVA: 0x0006972E File Offset: 0x0006792E
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06001BBE RID: 7102 RVA: 0x0006971C File Offset: 0x0006791C
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06001BBF RID: 7103 RVA: 0x00069738 File Offset: 0x00067938
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

		// Token: 0x0400094F RID: 2383
		private string creationMessage;

		// Token: 0x04000950 RID: 2384
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x04000951 RID: 2385
		private string m_rcCommand;

		// Token: 0x04000952 RID: 2386
		private string m_rcCommandArguments;

		// Token: 0x04000953 RID: 2387
		private int m_exitCode;

		// Token: 0x04000954 RID: 2388
		private string m_output;

		// Token: 0x04000955 RID: 2389
		private string m_error;
	}
}
