using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x020000C8 RID: 200
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class ServiceNotFoundException : StateManagerBaseException
	{
		// Token: 0x060007BE RID: 1982 RVA: 0x00018824 File Offset: 0x00016A24
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060007BF RID: 1983 RVA: 0x0001882C File Offset: 0x00016A2C
		// (set) Token: 0x060007C0 RID: 1984 RVA: 0x00018834 File Offset: 0x00016A34
		public string Service
		{
			get
			{
				return this.m_service;
			}
			protected set
			{
				this.m_service = value;
			}
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x0001883D File Offset: 0x00016A3D
		public ServiceNotFoundException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x00018851 File Offset: 0x00016A51
		public ServiceNotFoundException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x00018868 File Offset: 0x00016A68
		public ServiceNotFoundException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x00018888 File Offset: 0x00016A88
		protected ServiceNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("ServiceNotFoundException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.Service = (string)info.GetValue("ServiceNotFoundException_Service", typeof(string));
			}
			catch (SerializationException)
			{
				this.Service = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("ServiceNotFoundException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x0001895C File Offset: 0x00016B5C
		public ServiceNotFoundException(string service, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.Service = service;
			this.ConstructorInternal(false);
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x0001897A File Offset: 0x00016B7A
		public ServiceNotFoundException(string service, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.Service = service;
			this.ConstructorInternal(false);
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x000189A0 File Offset: 0x00016BA0
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x000189D7 File Offset: 0x00016BD7
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x000189E0 File Offset: 0x00016BE0
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(ServiceNotFoundException))
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

		// Token: 0x060007CA RID: 1994 RVA: 0x00018AB0 File Offset: 0x00016CB0
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("ServiceNotFoundException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("ServiceNotFoundException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.Service != null)
			{
				info.AddValue("ServiceNotFoundException_Service", this.Service, typeof(string));
			}
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x00018B30 File Offset: 0x00016D30
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Service '{0}' is not found in persistent storage", (markupKind == PrivateInformationMarkupKind.None) ? ((this.Service != null) ? this.Service.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.Service != null) ? this.Service.MarkIfInternal() : string.Empty) : ((this.Service != null) ? this.Service.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060007CC RID: 1996 RVA: 0x00018BAB File Offset: 0x00016DAB
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

		// Token: 0x060007CD RID: 1997 RVA: 0x00018BC8 File Offset: 0x00016DC8
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Service={0}", (this.Service != null) ? this.Service.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Service={0}", (this.Service != null) ? this.Service.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "Service={0}", (this.Service != null) ? this.Service.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x00018C8C File Offset: 0x00016E8C
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x00018C95 File Offset: 0x00016E95
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x00018C9E File Offset: 0x00016E9E
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x00018C8C File Offset: 0x00016E8C
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x00018CA8 File Offset: 0x00016EA8
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

		// Token: 0x04000268 RID: 616
		private string creationMessage;

		// Token: 0x04000269 RID: 617
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x0400026A RID: 618
		private string m_service;
	}
}
