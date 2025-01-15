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
	// Token: 0x02000383 RID: 899
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class LinkCompiledResourceFileToDynamicLibraryException : MonitoredException
	{
		// Token: 0x06001BC0 RID: 7104 RVA: 0x00069924 File Offset: 0x00067B24
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06001BC1 RID: 7105 RVA: 0x0006992C File Offset: 0x00067B2C
		// (set) Token: 0x06001BC2 RID: 7106 RVA: 0x00069934 File Offset: 0x00067B34
		public string LinkCommand
		{
			get
			{
				return this.m_linkCommand;
			}
			protected set
			{
				this.m_linkCommand = value;
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06001BC3 RID: 7107 RVA: 0x0006993D File Offset: 0x00067B3D
		// (set) Token: 0x06001BC4 RID: 7108 RVA: 0x00069945 File Offset: 0x00067B45
		public string LinkCommandArguments
		{
			get
			{
				return this.m_linkCommandArguments;
			}
			protected set
			{
				this.m_linkCommandArguments = value;
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06001BC5 RID: 7109 RVA: 0x0006994E File Offset: 0x00067B4E
		// (set) Token: 0x06001BC6 RID: 7110 RVA: 0x00069956 File Offset: 0x00067B56
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

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06001BC7 RID: 7111 RVA: 0x0006995F File Offset: 0x00067B5F
		// (set) Token: 0x06001BC8 RID: 7112 RVA: 0x00069967 File Offset: 0x00067B67
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

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06001BC9 RID: 7113 RVA: 0x00069970 File Offset: 0x00067B70
		// (set) Token: 0x06001BCA RID: 7114 RVA: 0x00069978 File Offset: 0x00067B78
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

		// Token: 0x06001BCB RID: 7115 RVA: 0x00069981 File Offset: 0x00067B81
		public LinkCompiledResourceFileToDynamicLibraryException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
			CompileCheck.IsValidReferenceField<string>();
			CompileCheck.IsValidValueField<int>();
			CompileCheck.IsValidReferenceField<string>();
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06001BCC RID: 7116 RVA: 0x000699A9 File Offset: 0x00067BA9
		public LinkCompiledResourceFileToDynamicLibraryException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001BCD RID: 7117 RVA: 0x000699C0 File Offset: 0x00067BC0
		public LinkCompiledResourceFileToDynamicLibraryException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001BCE RID: 7118 RVA: 0x000699E0 File Offset: 0x00067BE0
		protected LinkCompiledResourceFileToDynamicLibraryException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("LinkCompiledResourceFileToDynamicLibraryException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.LinkCommand = (string)info.GetValue("LinkCompiledResourceFileToDynamicLibraryException_LinkCommand", typeof(string));
			}
			catch (SerializationException)
			{
				this.LinkCommand = null;
			}
			try
			{
				this.LinkCommandArguments = (string)info.GetValue("LinkCompiledResourceFileToDynamicLibraryException_LinkCommandArguments", typeof(string));
			}
			catch (SerializationException)
			{
				this.LinkCommandArguments = null;
			}
			this.ExitCode = (int)info.GetValue("LinkCompiledResourceFileToDynamicLibraryException_ExitCode", typeof(int));
			try
			{
				this.Output = (string)info.GetValue("LinkCompiledResourceFileToDynamicLibraryException_Output", typeof(string));
			}
			catch (SerializationException)
			{
				this.Output = null;
			}
			try
			{
				this.Error = (string)info.GetValue("LinkCompiledResourceFileToDynamicLibraryException_Error", typeof(string));
			}
			catch (SerializationException)
			{
				this.Error = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("LinkCompiledResourceFileToDynamicLibraryException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06001BCF RID: 7119 RVA: 0x00069B80 File Offset: 0x00067D80
		public LinkCompiledResourceFileToDynamicLibraryException(string linkCommand, string linkCommandArguments, int exitCode, string output, string error)
		{
			this.LinkCommand = linkCommand;
			this.LinkCommandArguments = linkCommandArguments;
			this.ExitCode = exitCode;
			this.Output = output;
			this.Error = error;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001BD0 RID: 7120 RVA: 0x00069BB4 File Offset: 0x00067DB4
		public LinkCompiledResourceFileToDynamicLibraryException(string linkCommand, string linkCommandArguments, int exitCode, string output, string error, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.LinkCommand = linkCommand;
			this.LinkCommandArguments = linkCommandArguments;
			this.ExitCode = exitCode;
			this.Output = output;
			this.Error = error;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001BD1 RID: 7121 RVA: 0x00069BF4 File Offset: 0x00067DF4
		public LinkCompiledResourceFileToDynamicLibraryException(string linkCommand, string linkCommandArguments, int exitCode, string output, string error, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.LinkCommand = linkCommand;
			this.LinkCommandArguments = linkCommandArguments;
			this.ExitCode = exitCode;
			this.Output = output;
			this.Error = error;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001BD2 RID: 7122 RVA: 0x00069C44 File Offset: 0x00067E44
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06001BD3 RID: 7123 RVA: 0x00069C7B File Offset: 0x00067E7B
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06001BD4 RID: 7124 RVA: 0x00069C84 File Offset: 0x00067E84
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(LinkCompiledResourceFileToDynamicLibraryException))
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

		// Token: 0x06001BD5 RID: 7125 RVA: 0x00069D54 File Offset: 0x00067F54
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("LinkCompiledResourceFileToDynamicLibraryException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("LinkCompiledResourceFileToDynamicLibraryException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.LinkCommand != null)
			{
				info.AddValue("LinkCompiledResourceFileToDynamicLibraryException_LinkCommand", this.LinkCommand, typeof(string));
			}
			if (this.LinkCommandArguments != null)
			{
				info.AddValue("LinkCompiledResourceFileToDynamicLibraryException_LinkCommandArguments", this.LinkCommandArguments, typeof(string));
			}
			info.AddValue("LinkCompiledResourceFileToDynamicLibraryException_ExitCode", this.ExitCode, typeof(int));
			if (this.Output != null)
			{
				info.AddValue("LinkCompiledResourceFileToDynamicLibraryException_Output", this.Output, typeof(string));
			}
			if (this.Error != null)
			{
				info.AddValue("LinkCompiledResourceFileToDynamicLibraryException_Error", this.Error, typeof(string));
			}
		}

		// Token: 0x06001BD6 RID: 7126 RVA: 0x00069E5C File Offset: 0x0006805C
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Linking the compiled resource file into a dynamic library has failed. {0} {1} has exit code {2}. Output: {3}. Error: {4}.", new object[]
			{
				(markupKind == PrivateInformationMarkupKind.None) ? ((this.LinkCommand != null) ? this.LinkCommand.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.LinkCommand != null) ? this.LinkCommand.MarkIfInternal() : string.Empty) : ((this.LinkCommand != null) ? this.LinkCommand.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)),
				(markupKind == PrivateInformationMarkupKind.None) ? ((this.LinkCommandArguments != null) ? this.LinkCommandArguments.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.LinkCommandArguments != null) ? this.LinkCommandArguments.MarkIfInternal() : string.Empty) : ((this.LinkCommandArguments != null) ? this.LinkCommandArguments.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)),
				(markupKind == PrivateInformationMarkupKind.None) ? this.ExitCode.ToString(CultureInfo.InvariantCulture) : ((markupKind == PrivateInformationMarkupKind.Internal) ? this.ExitCode.ToString(CultureInfo.InvariantCulture) : this.ExitCode.ToString(CultureInfo.InvariantCulture)),
				(markupKind == PrivateInformationMarkupKind.None) ? ((this.Output != null) ? this.Output.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.Output != null) ? this.Output.MarkIfInternal() : string.Empty) : ((this.Output != null) ? this.Output.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)),
				(markupKind == PrivateInformationMarkupKind.None) ? ((this.Error != null) ? this.Error.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.Error != null) ? this.Error.MarkIfInternal() : string.Empty) : ((this.Error != null) ? this.Error.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty))
			});
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06001BD7 RID: 7127 RVA: 0x0006A04D File Offset: 0x0006824D
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

		// Token: 0x06001BD8 RID: 7128 RVA: 0x0006A06C File Offset: 0x0006826C
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "LinkCommand={0}", new object[] { (this.LinkCommand != null) ? this.LinkCommand.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "LinkCommand={0}", new object[] { (this.LinkCommand != null) ? this.LinkCommand.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "LinkCommand={0}", new object[] { (this.LinkCommand != null) ? this.LinkCommand.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "LinkCommandArguments={0}", new object[] { (this.LinkCommandArguments != null) ? this.LinkCommandArguments.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "LinkCommandArguments={0}", new object[] { (this.LinkCommandArguments != null) ? this.LinkCommandArguments.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "LinkCommandArguments={0}", new object[] { (this.LinkCommandArguments != null) ? this.LinkCommandArguments.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "ExitCode={0}", new object[] { this.ExitCode.ToString(CultureInfo.InvariantCulture) }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "ExitCode={0}", new object[] { this.ExitCode.ToString(CultureInfo.InvariantCulture) }) : string.Format(CultureInfo.CurrentCulture, "ExitCode={0}", new object[] { this.ExitCode.ToString(CultureInfo.InvariantCulture) })));
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Output={0}", new object[] { (this.Output != null) ? this.Output.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Output={0}", new object[] { (this.Output != null) ? this.Output.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "Output={0}", new object[] { (this.Output != null) ? this.Output.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Error={0}", new object[] { (this.Error != null) ? this.Error.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Error={0}", new object[] { (this.Error != null) ? this.Error.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "Error={0}", new object[] { (this.Error != null) ? this.Error.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
		}

		// Token: 0x06001BD9 RID: 7129 RVA: 0x0006A43C File Offset: 0x0006863C
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06001BDA RID: 7130 RVA: 0x0006A445 File Offset: 0x00068645
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06001BDB RID: 7131 RVA: 0x0006A44E File Offset: 0x0006864E
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06001BDC RID: 7132 RVA: 0x0006A43C File Offset: 0x0006863C
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06001BDD RID: 7133 RVA: 0x0006A458 File Offset: 0x00068658
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

		// Token: 0x04000956 RID: 2390
		private string creationMessage;

		// Token: 0x04000957 RID: 2391
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x04000958 RID: 2392
		private string m_linkCommand;

		// Token: 0x04000959 RID: 2393
		private string m_linkCommandArguments;

		// Token: 0x0400095A RID: 2394
		private int m_exitCode;

		// Token: 0x0400095B RID: 2395
		private string m_output;

		// Token: 0x0400095C RID: 2396
		private string m_error;
	}
}
