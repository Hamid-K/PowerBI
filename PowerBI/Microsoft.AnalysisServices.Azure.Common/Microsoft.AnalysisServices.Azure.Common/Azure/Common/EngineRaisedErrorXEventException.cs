using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x02000101 RID: 257
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class EngineRaisedErrorXEventException : MonitoredException
	{
		// Token: 0x06000C7F RID: 3199 RVA: 0x0002FB84 File Offset: 0x0002DD84
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000C80 RID: 3200 RVA: 0x0002FB8C File Offset: 0x0002DD8C
		// (set) Token: 0x06000C81 RID: 3201 RVA: 0x0002FB94 File Offset: 0x0002DD94
		public int ErrorId
		{
			get
			{
				return this.m_errorId;
			}
			protected set
			{
				this.m_errorId = value;
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000C82 RID: 3202 RVA: 0x0002FB9D File Offset: 0x0002DD9D
		// (set) Token: 0x06000C83 RID: 3203 RVA: 0x0002FBA5 File Offset: 0x0002DDA5
		public string ErrorText
		{
			get
			{
				return this.m_errorText;
			}
			protected set
			{
				this.m_errorText = value;
			}
		}

		// Token: 0x06000C84 RID: 3204 RVA: 0x0002FBAE File Offset: 0x0002DDAE
		public EngineRaisedErrorXEventException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidValueField<int>();
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06000C85 RID: 3205 RVA: 0x0002FBC7 File Offset: 0x0002DDC7
		public EngineRaisedErrorXEventException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000C86 RID: 3206 RVA: 0x0002FBDE File Offset: 0x0002DDDE
		public EngineRaisedErrorXEventException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000C87 RID: 3207 RVA: 0x0002FBFC File Offset: 0x0002DDFC
		protected EngineRaisedErrorXEventException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("EngineRaisedErrorXEventException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			this.ErrorId = (int)info.GetValue("EngineRaisedErrorXEventException_ErrorId", typeof(int));
			try
			{
				this.ErrorText = (string)info.GetValue("EngineRaisedErrorXEventException_ErrorText", typeof(string));
			}
			catch (SerializationException)
			{
				this.ErrorText = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("EngineRaisedErrorXEventException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06000C88 RID: 3208 RVA: 0x0002FCF0 File Offset: 0x0002DEF0
		public EngineRaisedErrorXEventException(int errorId, string errorText)
		{
			this.ErrorId = errorId;
			this.ErrorText = errorText;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000C89 RID: 3209 RVA: 0x0002FD0D File Offset: 0x0002DF0D
		public EngineRaisedErrorXEventException(int errorId, string errorText, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ErrorId = errorId;
			this.ErrorText = errorText;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000C8A RID: 3210 RVA: 0x0002FD32 File Offset: 0x0002DF32
		public EngineRaisedErrorXEventException(int errorId, string errorText, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ErrorId = errorId;
			this.ErrorText = errorText;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000C8B RID: 3211 RVA: 0x0002FD60 File Offset: 0x0002DF60
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06000C8C RID: 3212 RVA: 0x0002FD97 File Offset: 0x0002DF97
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06000C8D RID: 3213 RVA: 0x0002FDA0 File Offset: 0x0002DFA0
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(EngineRaisedErrorXEventException))
			{
				TraceSourceBase<ANCommonTrace>.Tracer.TraceError("Exception object created: {0}: {1}{2}", new object[]
				{
					type,
					this.Message,
					(base.InnerException != null) ? "(wrapped: {0}/{1})".FormatWithInvariantCulture(new object[]
					{
						base.InnerException.GetType().Name,
						base.InnerException.Message
					}) : string.Empty
				});
				if (TraceSourceBase<ANCommonTrace>.Tracer.ShouldTrace(TraceVerbosity.Error) && (base.InnerException == null || !(base.InnerException is IMonitoredError)))
				{
					TraceSourceBase<ANCommonTrace>.Tracer.TraceError("StackTrace: {0}", new object[]
					{
						new StackTrace(0, true)
					});
				}
			}
		}

		// Token: 0x06000C8E RID: 3214 RVA: 0x0002FE70 File Offset: 0x0002E070
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("EngineRaisedErrorXEventException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("EngineRaisedErrorXEventException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			info.AddValue("EngineRaisedErrorXEventException_ErrorId", this.ErrorId, typeof(int));
			if (this.ErrorText != null)
			{
				info.AddValue("EngineRaisedErrorXEventException_ErrorText", this.ErrorText, typeof(string));
			}
		}

		// Token: 0x06000C8F RID: 3215 RVA: 0x0002FF10 File Offset: 0x0002E110
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "The AS engine has raised an error XEvent: ErrorId:[0x{0}], ErrorText:[{1}]", (markupKind == PrivateInformationMarkupKind.None) ? this.ErrorId.ToString("X", CultureInfo.InvariantCulture) : ((markupKind == PrivateInformationMarkupKind.Internal) ? this.ErrorId.ToString("X", CultureInfo.InvariantCulture) : this.ErrorId.ToString("X", CultureInfo.InvariantCulture)), (markupKind == PrivateInformationMarkupKind.None) ? ((this.ErrorText != null) ? this.ErrorText.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.ErrorText != null) ? this.ErrorText.MarkIfInternal() : string.Empty) : ((this.ErrorText != null) ? this.ErrorText.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000C90 RID: 3216 RVA: 0x0002FFDE File Offset: 0x0002E1DE
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

		// Token: 0x06000C91 RID: 3217 RVA: 0x0002FFFC File Offset: 0x0002E1FC
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "ErrorId={0}", this.ErrorId.ToString("X", CultureInfo.InvariantCulture)) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "ErrorId={0}", this.ErrorId.ToString("X", CultureInfo.InvariantCulture)) : string.Format(CultureInfo.CurrentCulture, "ErrorId={0}", this.ErrorId.ToString("X", CultureInfo.InvariantCulture))));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "ErrorText={0}", (this.ErrorText != null) ? this.ErrorText.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "ErrorText={0}", (this.ErrorText != null) ? this.ErrorText.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "ErrorText={0}", (this.ErrorText != null) ? this.ErrorText.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x06000C92 RID: 3218 RVA: 0x0003015C File Offset: 0x0002E35C
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000C93 RID: 3219 RVA: 0x00030165 File Offset: 0x0002E365
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000C94 RID: 3220 RVA: 0x0003016E File Offset: 0x0002E36E
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06000C95 RID: 3221 RVA: 0x0003015C File Offset: 0x0002E35C
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000C96 RID: 3222 RVA: 0x00030178 File Offset: 0x0002E378
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

		// Token: 0x0400032C RID: 812
		private string creationMessage;

		// Token: 0x0400032D RID: 813
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x0400032E RID: 814
		private int m_errorId;

		// Token: 0x0400032F RID: 815
		private string m_errorText;
	}
}
