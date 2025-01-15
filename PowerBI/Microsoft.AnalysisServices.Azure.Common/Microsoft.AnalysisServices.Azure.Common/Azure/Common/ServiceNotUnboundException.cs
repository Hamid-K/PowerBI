using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x020000CC RID: 204
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class ServiceNotUnboundException : StateManagerBaseException
	{
		// Token: 0x0600081C RID: 2076 RVA: 0x0001A794 File Offset: 0x00018994
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x0600081D RID: 2077 RVA: 0x0001A79C File Offset: 0x0001899C
		// (set) Token: 0x0600081E RID: 2078 RVA: 0x0001A7A4 File Offset: 0x000189A4
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

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x0600081F RID: 2079 RVA: 0x0001A7AD File Offset: 0x000189AD
		// (set) Token: 0x06000820 RID: 2080 RVA: 0x0001A7B5 File Offset: 0x000189B5
		public string Database
		{
			get
			{
				return this.m_database;
			}
			protected set
			{
				this.m_database = value;
			}
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x0001A7BE File Offset: 0x000189BE
		public ServiceNotUnboundException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x0001A7D7 File Offset: 0x000189D7
		public ServiceNotUnboundException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x0001A7EE File Offset: 0x000189EE
		public ServiceNotUnboundException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x0001A80C File Offset: 0x00018A0C
		protected ServiceNotUnboundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("ServiceNotUnboundException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.Service = (string)info.GetValue("ServiceNotUnboundException_Service", typeof(string));
			}
			catch (SerializationException)
			{
				this.Service = null;
			}
			try
			{
				this.Database = (string)info.GetValue("ServiceNotUnboundException_Database", typeof(string));
			}
			catch (SerializationException)
			{
				this.Database = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("ServiceNotUnboundException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x0001A91C File Offset: 0x00018B1C
		public ServiceNotUnboundException(string service, string database)
		{
			this.Service = service;
			this.Database = database;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x0001A939 File Offset: 0x00018B39
		public ServiceNotUnboundException(string service, string database, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.Service = service;
			this.Database = database;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x0001A95E File Offset: 0x00018B5E
		public ServiceNotUnboundException(string service, string database, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.Service = service;
			this.Database = database;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x0001A98C File Offset: 0x00018B8C
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x0001A9C3 File Offset: 0x00018BC3
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x0001A9CC File Offset: 0x00018BCC
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(ServiceNotUnboundException))
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

		// Token: 0x0600082B RID: 2091 RVA: 0x0001AA9C File Offset: 0x00018C9C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("ServiceNotUnboundException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("ServiceNotUnboundException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.Service != null)
			{
				info.AddValue("ServiceNotUnboundException_Service", this.Service, typeof(string));
			}
			if (this.Database != null)
			{
				info.AddValue("ServiceNotUnboundException_Database", this.Database, typeof(string));
			}
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x0001AB40 File Offset: 0x00018D40
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Failed to delete service {0} because the database is not unbound from database '{1}'", (markupKind == PrivateInformationMarkupKind.None) ? ((this.Service != null) ? this.Service.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.Service != null) ? this.Service.MarkIfInternal() : string.Empty) : ((this.Service != null) ? this.Service.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)), (markupKind == PrivateInformationMarkupKind.None) ? ((this.Database != null) ? this.Database.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.Database != null) ? this.Database.MarkIfInternal() : string.Empty) : ((this.Database != null) ? this.Database.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x0600082D RID: 2093 RVA: 0x0001AC1A File Offset: 0x00018E1A
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

		// Token: 0x0600082E RID: 2094 RVA: 0x0001AC38 File Offset: 0x00018E38
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Service={0}", (this.Service != null) ? this.Service.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Service={0}", (this.Service != null) ? this.Service.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "Service={0}", (this.Service != null) ? this.Service.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Database={0}", (this.Database != null) ? this.Database.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Database={0}", (this.Database != null) ? this.Database.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "Database={0}", (this.Database != null) ? this.Database.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x0001ADA4 File Offset: 0x00018FA4
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x0001ADAD File Offset: 0x00018FAD
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x0001ADB6 File Offset: 0x00018FB6
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x0001ADA4 File Offset: 0x00018FA4
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x0001ADC0 File Offset: 0x00018FC0
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

		// Token: 0x04000278 RID: 632
		private string creationMessage;

		// Token: 0x04000279 RID: 633
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x0400027A RID: 634
		private string m_service;

		// Token: 0x0400027B RID: 635
		private string m_database;
	}
}
