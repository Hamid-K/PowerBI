using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004FD RID: 1277
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class CommunicationFrameworkRouterResolveEndpointException : CommunicationFrameworkRouterException
	{
		// Token: 0x060026E4 RID: 9956 RVA: 0x0008AFBC File Offset: 0x000891BC
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x060026E5 RID: 9957 RVA: 0x0008AFC4 File Offset: 0x000891C4
		// (set) Token: 0x060026E6 RID: 9958 RVA: 0x0008AFCC File Offset: 0x000891CC
		public string RouterType
		{
			get
			{
				return this.m_routerType;
			}
			protected set
			{
				this.m_routerType = value;
			}
		}

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x060026E7 RID: 9959 RVA: 0x0008AFD5 File Offset: 0x000891D5
		// (set) Token: 0x060026E8 RID: 9960 RVA: 0x0008AFDD File Offset: 0x000891DD
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

		// Token: 0x060026E9 RID: 9961 RVA: 0x0008AFE6 File Offset: 0x000891E6
		public CommunicationFrameworkRouterResolveEndpointException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x060026EA RID: 9962 RVA: 0x0008AFFF File Offset: 0x000891FF
		public CommunicationFrameworkRouterResolveEndpointException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060026EB RID: 9963 RVA: 0x0008B016 File Offset: 0x00089216
		public CommunicationFrameworkRouterResolveEndpointException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060026EC RID: 9964 RVA: 0x0008B034 File Offset: 0x00089234
		protected CommunicationFrameworkRouterResolveEndpointException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("CommunicationFrameworkRouterResolveEndpointException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.RouterType = (string)info.GetValue("CommunicationFrameworkRouterResolveEndpointException_RouterType", typeof(string));
			}
			catch (SerializationException)
			{
				this.RouterType = null;
			}
			try
			{
				this.Service = (string)info.GetValue("CommunicationFrameworkRouterResolveEndpointException_Service", typeof(string));
			}
			catch (SerializationException)
			{
				this.Service = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("CommunicationFrameworkRouterResolveEndpointException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x060026ED RID: 9965 RVA: 0x0008B144 File Offset: 0x00089344
		public CommunicationFrameworkRouterResolveEndpointException(string routerType, string service)
		{
			this.RouterType = routerType;
			this.Service = service;
			this.ConstructorInternal(false);
		}

		// Token: 0x060026EE RID: 9966 RVA: 0x0008B161 File Offset: 0x00089361
		public CommunicationFrameworkRouterResolveEndpointException(string routerType, string service, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.RouterType = routerType;
			this.Service = service;
			this.ConstructorInternal(false);
		}

		// Token: 0x060026EF RID: 9967 RVA: 0x0008B186 File Offset: 0x00089386
		public CommunicationFrameworkRouterResolveEndpointException(string routerType, string service, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.RouterType = routerType;
			this.Service = service;
			this.ConstructorInternal(false);
		}

		// Token: 0x060026F0 RID: 9968 RVA: 0x0008B1B4 File Offset: 0x000893B4
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x060026F1 RID: 9969 RVA: 0x0008B1EB File Offset: 0x000893EB
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x060026F2 RID: 9970 RVA: 0x0008B1F4 File Offset: 0x000893F4
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(CommunicationFrameworkRouterResolveEndpointException))
			{
				TraceSourceBase<CommunicationFrameworkTrace>.Tracer.TraceError("Exception object created: {0}: {1}{2}", new object[]
				{
					type,
					this.Message,
					(base.InnerException != null) ? "(wrapped: {0}/{1})".FormatWithInvariantCulture(new object[]
					{
						base.InnerException.GetType().Name,
						base.InnerException.Message
					}) : string.Empty
				});
				if (TraceSourceBase<CommunicationFrameworkTrace>.Tracer.ShouldTrace(TraceVerbosity.Error) && (base.InnerException == null || !(base.InnerException is IMonitoredError)))
				{
					TraceSourceBase<CommunicationFrameworkTrace>.Tracer.TraceError("StackTrace: {0}", new object[]
					{
						new StackTrace(0, true)
					});
				}
			}
		}

		// Token: 0x060026F3 RID: 9971 RVA: 0x0008B2C4 File Offset: 0x000894C4
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("CommunicationFrameworkRouterResolveEndpointException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("CommunicationFrameworkRouterResolveEndpointException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.RouterType != null)
			{
				info.AddValue("CommunicationFrameworkRouterResolveEndpointException_RouterType", this.RouterType, typeof(string));
			}
			if (this.Service != null)
			{
				info.AddValue("CommunicationFrameworkRouterResolveEndpointException_Service", this.Service, typeof(string));
			}
		}

		// Token: 0x060026F4 RID: 9972 RVA: 0x0008B368 File Offset: 0x00089568
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Could not resolve service '{0}' in router {1}", new object[]
			{
				(markupKind == PrivateInformationMarkupKind.None) ? ((this.Service != null) ? this.Service.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.Service != null) ? this.Service.MarkIfInternal() : string.Empty) : ((this.Service != null) ? this.Service.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)),
				(markupKind == PrivateInformationMarkupKind.None) ? ((this.RouterType != null) ? this.RouterType.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.RouterType != null) ? this.RouterType.MarkIfInternal() : string.Empty) : ((this.RouterType != null) ? this.RouterType.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty))
			});
		}

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x060026F5 RID: 9973 RVA: 0x0008B44E File Offset: 0x0008964E
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

		// Token: 0x060026F6 RID: 9974 RVA: 0x0008B46C File Offset: 0x0008966C
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "RouterType={0}", new object[] { (this.RouterType != null) ? this.RouterType.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "RouterType={0}", new object[] { (this.RouterType != null) ? this.RouterType.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "RouterType={0}", new object[] { (this.RouterType != null) ? this.RouterType.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Service={0}", new object[] { (this.Service != null) ? this.Service.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Service={0}", new object[] { (this.Service != null) ? this.Service.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "Service={0}", new object[] { (this.Service != null) ? this.Service.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
		}

		// Token: 0x060026F7 RID: 9975 RVA: 0x0008B60E File Offset: 0x0008980E
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060026F8 RID: 9976 RVA: 0x0008B617 File Offset: 0x00089817
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x060026F9 RID: 9977 RVA: 0x0008B620 File Offset: 0x00089820
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x060026FA RID: 9978 RVA: 0x0008B60E File Offset: 0x0008980E
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060026FB RID: 9979 RVA: 0x0008B62C File Offset: 0x0008982C
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

		// Token: 0x04000DC2 RID: 3522
		private string creationMessage;

		// Token: 0x04000DC3 RID: 3523
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x04000DC4 RID: 3524
		private string m_routerType;

		// Token: 0x04000DC5 RID: 3525
		private string m_service;
	}
}
