using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.AnalysisServices.Azure.Common;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Resta
{
	// Token: 0x02000010 RID: 16
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class RestaUnexpectedPdsVersionException : RestaBaseException
	{
		// Token: 0x060000DE RID: 222 RVA: 0x00005B80 File Offset: 0x00003D80
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00005B88 File Offset: 0x00003D88
		public RestaUnexpectedPdsVersionException()
		{
			this.ConstructorInternal(false);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00005B97 File Offset: 0x00003D97
		public RestaUnexpectedPdsVersionException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00005BAE File Offset: 0x00003DAE
		public RestaUnexpectedPdsVersionException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00005BCC File Offset: 0x00003DCC
		protected RestaUnexpectedPdsVersionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("RestaUnexpectedPdsVersionException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("RestaUnexpectedPdsVersionException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00005C68 File Offset: 0x00003E68
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00005C9F File Offset: 0x00003E9F
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00005CA8 File Offset: 0x00003EA8
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(RestaUnexpectedPdsVersionException))
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

		// Token: 0x060000E6 RID: 230 RVA: 0x00005D78 File Offset: 0x00003F78
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("RestaUnexpectedPdsVersionException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("RestaUnexpectedPdsVersionException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00005DD3 File Offset: 0x00003FD3
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Push Data Service on the database entity is unexpected.", Array.Empty<object>());
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00005DE9 File Offset: 0x00003FE9
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

		// Token: 0x060000E9 RID: 233 RVA: 0x00004740 File Offset: 0x00002940
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string newLine = Environment.NewLine;
			return base.GetPropertiesString(markupKind);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00005E06 File Offset: 0x00004006
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00005E0F File Offset: 0x0000400F
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00005E18 File Offset: 0x00004018
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00005E06 File Offset: 0x00004006
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00005E24 File Offset: 0x00004024
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

		// Token: 0x04000042 RID: 66
		private string creationMessage;

		// Token: 0x04000043 RID: 67
		private ExceptionCulprit exceptionCulprit;
	}
}
