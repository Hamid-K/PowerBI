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
	// Token: 0x02000381 RID: 897
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class CompilePerformanceCountersManifestToResourceFileException : MonitoredException
	{
		// Token: 0x06001B84 RID: 7044 RVA: 0x00067EE4 File Offset: 0x000660E4
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06001B85 RID: 7045 RVA: 0x00067EEC File Offset: 0x000660EC
		// (set) Token: 0x06001B86 RID: 7046 RVA: 0x00067EF4 File Offset: 0x000660F4
		public string CtrppCommand
		{
			get
			{
				return this.m_ctrppCommand;
			}
			protected set
			{
				this.m_ctrppCommand = value;
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06001B87 RID: 7047 RVA: 0x00067EFD File Offset: 0x000660FD
		// (set) Token: 0x06001B88 RID: 7048 RVA: 0x00067F05 File Offset: 0x00066105
		public string CtrppCommandArguments
		{
			get
			{
				return this.m_ctrppCommandArguments;
			}
			protected set
			{
				this.m_ctrppCommandArguments = value;
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06001B89 RID: 7049 RVA: 0x00067F0E File Offset: 0x0006610E
		// (set) Token: 0x06001B8A RID: 7050 RVA: 0x00067F16 File Offset: 0x00066116
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

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06001B8B RID: 7051 RVA: 0x00067F1F File Offset: 0x0006611F
		// (set) Token: 0x06001B8C RID: 7052 RVA: 0x00067F27 File Offset: 0x00066127
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

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06001B8D RID: 7053 RVA: 0x00067F30 File Offset: 0x00066130
		// (set) Token: 0x06001B8E RID: 7054 RVA: 0x00067F38 File Offset: 0x00066138
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

		// Token: 0x06001B8F RID: 7055 RVA: 0x00067F41 File Offset: 0x00066141
		public CompilePerformanceCountersManifestToResourceFileException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
			CompileCheck.IsValidReferenceField<string>();
			CompileCheck.IsValidValueField<int>();
			CompileCheck.IsValidReferenceField<string>();
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06001B90 RID: 7056 RVA: 0x00067F69 File Offset: 0x00066169
		public CompilePerformanceCountersManifestToResourceFileException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001B91 RID: 7057 RVA: 0x00067F80 File Offset: 0x00066180
		public CompilePerformanceCountersManifestToResourceFileException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001B92 RID: 7058 RVA: 0x00067FA0 File Offset: 0x000661A0
		protected CompilePerformanceCountersManifestToResourceFileException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("CompilePerformanceCountersManifestToResourceFileException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.CtrppCommand = (string)info.GetValue("CompilePerformanceCountersManifestToResourceFileException_CtrppCommand", typeof(string));
			}
			catch (SerializationException)
			{
				this.CtrppCommand = null;
			}
			try
			{
				this.CtrppCommandArguments = (string)info.GetValue("CompilePerformanceCountersManifestToResourceFileException_CtrppCommandArguments", typeof(string));
			}
			catch (SerializationException)
			{
				this.CtrppCommandArguments = null;
			}
			this.ExitCode = (int)info.GetValue("CompilePerformanceCountersManifestToResourceFileException_ExitCode", typeof(int));
			try
			{
				this.Output = (string)info.GetValue("CompilePerformanceCountersManifestToResourceFileException_Output", typeof(string));
			}
			catch (SerializationException)
			{
				this.Output = null;
			}
			try
			{
				this.Error = (string)info.GetValue("CompilePerformanceCountersManifestToResourceFileException_Error", typeof(string));
			}
			catch (SerializationException)
			{
				this.Error = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("CompilePerformanceCountersManifestToResourceFileException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06001B93 RID: 7059 RVA: 0x00068140 File Offset: 0x00066340
		public CompilePerformanceCountersManifestToResourceFileException(string ctrppCommand, string ctrppCommandArguments, int exitCode, string output, string error)
		{
			this.CtrppCommand = ctrppCommand;
			this.CtrppCommandArguments = ctrppCommandArguments;
			this.ExitCode = exitCode;
			this.Output = output;
			this.Error = error;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001B94 RID: 7060 RVA: 0x00068174 File Offset: 0x00066374
		public CompilePerformanceCountersManifestToResourceFileException(string ctrppCommand, string ctrppCommandArguments, int exitCode, string output, string error, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.CtrppCommand = ctrppCommand;
			this.CtrppCommandArguments = ctrppCommandArguments;
			this.ExitCode = exitCode;
			this.Output = output;
			this.Error = error;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001B95 RID: 7061 RVA: 0x000681B4 File Offset: 0x000663B4
		public CompilePerformanceCountersManifestToResourceFileException(string ctrppCommand, string ctrppCommandArguments, int exitCode, string output, string error, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.CtrppCommand = ctrppCommand;
			this.CtrppCommandArguments = ctrppCommandArguments;
			this.ExitCode = exitCode;
			this.Output = output;
			this.Error = error;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001B96 RID: 7062 RVA: 0x00068204 File Offset: 0x00066404
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06001B97 RID: 7063 RVA: 0x0006823B File Offset: 0x0006643B
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06001B98 RID: 7064 RVA: 0x00068244 File Offset: 0x00066444
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(CompilePerformanceCountersManifestToResourceFileException))
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

		// Token: 0x06001B99 RID: 7065 RVA: 0x00068314 File Offset: 0x00066514
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("CompilePerformanceCountersManifestToResourceFileException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("CompilePerformanceCountersManifestToResourceFileException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.CtrppCommand != null)
			{
				info.AddValue("CompilePerformanceCountersManifestToResourceFileException_CtrppCommand", this.CtrppCommand, typeof(string));
			}
			if (this.CtrppCommandArguments != null)
			{
				info.AddValue("CompilePerformanceCountersManifestToResourceFileException_CtrppCommandArguments", this.CtrppCommandArguments, typeof(string));
			}
			info.AddValue("CompilePerformanceCountersManifestToResourceFileException_ExitCode", this.ExitCode, typeof(int));
			if (this.Output != null)
			{
				info.AddValue("CompilePerformanceCountersManifestToResourceFileException_Output", this.Output, typeof(string));
			}
			if (this.Error != null)
			{
				info.AddValue("CompilePerformanceCountersManifestToResourceFileException_Error", this.Error, typeof(string));
			}
		}

		// Token: 0x06001B9A RID: 7066 RVA: 0x0006841C File Offset: 0x0006661C
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Compiling the performance counters manifest into a resource file has failed. {0} {1} has exit code {2}. Output: {3}. Error: {4}.", new object[]
			{
				(markupKind == PrivateInformationMarkupKind.None) ? ((this.CtrppCommand != null) ? this.CtrppCommand.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.CtrppCommand != null) ? this.CtrppCommand.MarkIfInternal() : string.Empty) : ((this.CtrppCommand != null) ? this.CtrppCommand.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)),
				(markupKind == PrivateInformationMarkupKind.None) ? ((this.CtrppCommandArguments != null) ? this.CtrppCommandArguments.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.CtrppCommandArguments != null) ? this.CtrppCommandArguments.MarkIfInternal() : string.Empty) : ((this.CtrppCommandArguments != null) ? this.CtrppCommandArguments.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)),
				(markupKind == PrivateInformationMarkupKind.None) ? this.ExitCode.ToString(CultureInfo.InvariantCulture) : ((markupKind == PrivateInformationMarkupKind.Internal) ? this.ExitCode.ToString(CultureInfo.InvariantCulture) : this.ExitCode.ToString(CultureInfo.InvariantCulture)),
				(markupKind == PrivateInformationMarkupKind.None) ? ((this.Output != null) ? this.Output.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.Output != null) ? this.Output.MarkIfInternal() : string.Empty) : ((this.Output != null) ? this.Output.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)),
				(markupKind == PrivateInformationMarkupKind.None) ? ((this.Error != null) ? this.Error.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.Error != null) ? this.Error.MarkIfInternal() : string.Empty) : ((this.Error != null) ? this.Error.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty))
			});
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06001B9B RID: 7067 RVA: 0x0006860D File Offset: 0x0006680D
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

		// Token: 0x06001B9C RID: 7068 RVA: 0x0006862C File Offset: 0x0006682C
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "CtrppCommand={0}", new object[] { (this.CtrppCommand != null) ? this.CtrppCommand.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "CtrppCommand={0}", new object[] { (this.CtrppCommand != null) ? this.CtrppCommand.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "CtrppCommand={0}", new object[] { (this.CtrppCommand != null) ? this.CtrppCommand.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "CtrppCommandArguments={0}", new object[] { (this.CtrppCommandArguments != null) ? this.CtrppCommandArguments.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "CtrppCommandArguments={0}", new object[] { (this.CtrppCommandArguments != null) ? this.CtrppCommandArguments.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "CtrppCommandArguments={0}", new object[] { (this.CtrppCommandArguments != null) ? this.CtrppCommandArguments.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "ExitCode={0}", new object[] { this.ExitCode.ToString(CultureInfo.InvariantCulture) }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "ExitCode={0}", new object[] { this.ExitCode.ToString(CultureInfo.InvariantCulture) }) : string.Format(CultureInfo.CurrentCulture, "ExitCode={0}", new object[] { this.ExitCode.ToString(CultureInfo.InvariantCulture) })));
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Output={0}", new object[] { (this.Output != null) ? this.Output.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Output={0}", new object[] { (this.Output != null) ? this.Output.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "Output={0}", new object[] { (this.Output != null) ? this.Output.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Error={0}", new object[] { (this.Error != null) ? this.Error.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Error={0}", new object[] { (this.Error != null) ? this.Error.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "Error={0}", new object[] { (this.Error != null) ? this.Error.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
		}

		// Token: 0x06001B9D RID: 7069 RVA: 0x000689FC File Offset: 0x00066BFC
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06001B9E RID: 7070 RVA: 0x00068A05 File Offset: 0x00066C05
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06001B9F RID: 7071 RVA: 0x00068A0E File Offset: 0x00066C0E
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06001BA0 RID: 7072 RVA: 0x000689FC File Offset: 0x00066BFC
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06001BA1 RID: 7073 RVA: 0x00068A18 File Offset: 0x00066C18
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

		// Token: 0x04000948 RID: 2376
		private string creationMessage;

		// Token: 0x04000949 RID: 2377
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x0400094A RID: 2378
		private string m_ctrppCommand;

		// Token: 0x0400094B RID: 2379
		private string m_ctrppCommandArguments;

		// Token: 0x0400094C RID: 2380
		private int m_exitCode;

		// Token: 0x0400094D RID: 2381
		private string m_output;

		// Token: 0x0400094E RID: 2382
		private string m_error;
	}
}
