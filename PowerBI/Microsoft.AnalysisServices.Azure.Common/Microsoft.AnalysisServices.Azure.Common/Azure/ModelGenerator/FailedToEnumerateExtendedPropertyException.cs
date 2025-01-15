using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.AnalysisServices.Azure.Common;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.ModelGenerator
{
	// Token: 0x02000029 RID: 41
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class FailedToEnumerateExtendedPropertyException : MonitoredException
	{
		// Token: 0x060002A6 RID: 678 RVA: 0x0000D980 File Offset: 0x0000BB80
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x0000D988 File Offset: 0x0000BB88
		// (set) Token: 0x060002A8 RID: 680 RVA: 0x0000D990 File Offset: 0x0000BB90
		public string ExtendedProperty
		{
			get
			{
				return this.m_extendedProperty;
			}
			protected set
			{
				this.m_extendedProperty = value;
			}
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000D999 File Offset: 0x0000BB99
		public FailedToEnumerateExtendedPropertyException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000D9AD File Offset: 0x0000BBAD
		public FailedToEnumerateExtendedPropertyException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000D9C4 File Offset: 0x0000BBC4
		public FailedToEnumerateExtendedPropertyException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0000D9E4 File Offset: 0x0000BBE4
		protected FailedToEnumerateExtendedPropertyException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("FailedToEnumerateExtendedPropertyException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.ExtendedProperty = (string)info.GetValue("FailedToEnumerateExtendedPropertyException_ExtendedProperty", typeof(string));
			}
			catch (SerializationException)
			{
				this.ExtendedProperty = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("FailedToEnumerateExtendedPropertyException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000DAB8 File Offset: 0x0000BCB8
		public FailedToEnumerateExtendedPropertyException(string extendedProperty, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ExtendedProperty = extendedProperty;
			this.ConstructorInternal(false);
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000DAD6 File Offset: 0x0000BCD6
		public FailedToEnumerateExtendedPropertyException(string extendedProperty, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ExtendedProperty = extendedProperty;
			this.ConstructorInternal(false);
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0000DAFC File Offset: 0x0000BCFC
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000DB33 File Offset: 0x0000BD33
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000DB3C File Offset: 0x0000BD3C
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(FailedToEnumerateExtendedPropertyException))
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

		// Token: 0x060002B2 RID: 690 RVA: 0x0000DC0C File Offset: 0x0000BE0C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("FailedToEnumerateExtendedPropertyException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("FailedToEnumerateExtendedPropertyException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.ExtendedProperty != null)
			{
				info.AddValue("FailedToEnumerateExtendedPropertyException_ExtendedProperty", this.ExtendedProperty, typeof(string));
			}
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000DC8C File Offset: 0x0000BE8C
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Failed to enumerate Extended Property '{0}'", (markupKind == PrivateInformationMarkupKind.None) ? ((this.ExtendedProperty != null) ? this.ExtendedProperty.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.ExtendedProperty != null) ? this.ExtendedProperty.MarkIfInternal() : string.Empty) : ((this.ExtendedProperty != null) ? this.ExtendedProperty.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x0000DD07 File Offset: 0x0000BF07
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

		// Token: 0x060002B5 RID: 693 RVA: 0x0000DD24 File Offset: 0x0000BF24
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "ExtendedProperty={0}", (this.ExtendedProperty != null) ? this.ExtendedProperty.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "ExtendedProperty={0}", (this.ExtendedProperty != null) ? this.ExtendedProperty.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "ExtendedProperty={0}", (this.ExtendedProperty != null) ? this.ExtendedProperty.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000DDE8 File Offset: 0x0000BFE8
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000DDF1 File Offset: 0x0000BFF1
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000DDFA File Offset: 0x0000BFFA
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000DDE8 File Offset: 0x0000BFE8
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000DE04 File Offset: 0x0000C004
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

		// Token: 0x0400007C RID: 124
		private string creationMessage;

		// Token: 0x0400007D RID: 125
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x0400007E RID: 126
		private string m_extendedProperty;
	}
}
