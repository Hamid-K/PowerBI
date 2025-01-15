using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Modularization
{
	// Token: 0x020000E1 RID: 225
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class ServiceNotFoundException : MonitoredException
	{
		// Token: 0x06000674 RID: 1652 RVA: 0x00017230 File Offset: 0x00015430
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000675 RID: 1653 RVA: 0x00017238 File Offset: 0x00015438
		// (set) Token: 0x06000676 RID: 1654 RVA: 0x00017240 File Offset: 0x00015440
		public Type ServiceType
		{
			get
			{
				return this.m_serviceType;
			}
			protected set
			{
				this.m_serviceType = value;
			}
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x00017249 File Offset: 0x00015449
		public ServiceNotFoundException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<Type>();
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x0001725D File Offset: 0x0001545D
		public ServiceNotFoundException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x00017274 File Offset: 0x00015474
		public ServiceNotFoundException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x00017294 File Offset: 0x00015494
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
				this.ServiceType = (Type)info.GetValue("ServiceNotFoundException_ServiceType", typeof(Type));
			}
			catch (SerializationException)
			{
				this.ServiceType = null;
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

		// Token: 0x0600067B RID: 1659 RVA: 0x00017368 File Offset: 0x00015568
		public ServiceNotFoundException(Type serviceType)
		{
			this.ServiceType = serviceType;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x0001737E File Offset: 0x0001557E
		public ServiceNotFoundException(Type serviceType, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ServiceType = serviceType;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x0001739C File Offset: 0x0001559C
		public ServiceNotFoundException(Type serviceType, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ServiceType = serviceType;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x000173C0 File Offset: 0x000155C0
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x000173F7 File Offset: 0x000155F7
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x00017400 File Offset: 0x00015600
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(ServiceNotFoundException))
			{
				TraceSourceBase<ModularizationFrameworkTrace>.Tracer.TraceError("Exception object created: {0}: {1}{2}", new object[]
				{
					type,
					this.Message,
					(base.InnerException != null) ? "(wrapped: {0}/{1})".FormatWithInvariantCulture(new object[]
					{
						base.InnerException.GetType().Name,
						base.InnerException.Message
					}) : string.Empty
				});
				if (TraceSourceBase<ModularizationFrameworkTrace>.Tracer.ShouldTrace(TraceVerbosity.Error) && (base.InnerException == null || !(base.InnerException is IMonitoredError)))
				{
					TraceSourceBase<ModularizationFrameworkTrace>.Tracer.TraceError("StackTrace: {0}", new object[]
					{
						new StackTrace(0, true)
					});
				}
			}
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x000174D0 File Offset: 0x000156D0
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("ServiceNotFoundException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("ServiceNotFoundException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.ServiceType != null)
			{
				info.AddValue("ServiceNotFoundException_ServiceType", this.ServiceType, typeof(Type));
			}
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x00017554 File Offset: 0x00015754
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "The block service '{0}' could not be located", new object[] { (markupKind == PrivateInformationMarkupKind.None) ? ((this.ServiceType != null) ? this.ServiceType.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.ServiceType != null) ? this.ServiceType.MarkIfInternal() : string.Empty) : ((this.ServiceType != null) ? this.ServiceType.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)) });
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000683 RID: 1667 RVA: 0x000175EA File Offset: 0x000157EA
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

		// Token: 0x06000684 RID: 1668 RVA: 0x00017608 File Offset: 0x00015808
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "ServiceType={0}", new object[] { (this.ServiceType != null) ? this.ServiceType.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "ServiceType={0}", new object[] { (this.ServiceType != null) ? this.ServiceType.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "ServiceType={0}", new object[] { (this.ServiceType != null) ? this.ServiceType.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x000176F9 File Offset: 0x000158F9
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x00017702 File Offset: 0x00015902
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x0001770B File Offset: 0x0001590B
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x000176F9 File Offset: 0x000158F9
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x00017714 File Offset: 0x00015914
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

		// Token: 0x04000235 RID: 565
		private string creationMessage;

		// Token: 0x04000236 RID: 566
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x04000237 RID: 567
		private Type m_serviceType;
	}
}
