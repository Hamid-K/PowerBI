using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x0200050D RID: 1293
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class CommunicationFrameworkAmbigiousResultAttributeException : CommunicationFrameworkECFExtendedResultException
	{
		// Token: 0x06002817 RID: 10263 RVA: 0x00090810 File Offset: 0x0008EA10
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x06002818 RID: 10264 RVA: 0x00090818 File Offset: 0x0008EA18
		// (set) Token: 0x06002819 RID: 10265 RVA: 0x00090820 File Offset: 0x0008EA20
		public string ClassName
		{
			get
			{
				return this.m_className;
			}
			protected set
			{
				this.m_className = value;
			}
		}

		// Token: 0x0600281A RID: 10266 RVA: 0x00090829 File Offset: 0x0008EA29
		public CommunicationFrameworkAmbigiousResultAttributeException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x0600281B RID: 10267 RVA: 0x0009083D File Offset: 0x0008EA3D
		public CommunicationFrameworkAmbigiousResultAttributeException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600281C RID: 10268 RVA: 0x00090854 File Offset: 0x0008EA54
		public CommunicationFrameworkAmbigiousResultAttributeException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600281D RID: 10269 RVA: 0x00090874 File Offset: 0x0008EA74
		protected CommunicationFrameworkAmbigiousResultAttributeException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("CommunicationFrameworkAmbigiousResultAttributeException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.ClassName = (string)info.GetValue("CommunicationFrameworkAmbigiousResultAttributeException_ClassName", typeof(string));
			}
			catch (SerializationException)
			{
				this.ClassName = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("CommunicationFrameworkAmbigiousResultAttributeException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x0600281E RID: 10270 RVA: 0x00090948 File Offset: 0x0008EB48
		public CommunicationFrameworkAmbigiousResultAttributeException(string className, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ClassName = className;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600281F RID: 10271 RVA: 0x00090966 File Offset: 0x0008EB66
		public CommunicationFrameworkAmbigiousResultAttributeException(string className, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ClassName = className;
			this.ConstructorInternal(false);
		}

		// Token: 0x06002820 RID: 10272 RVA: 0x0009098C File Offset: 0x0008EB8C
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06002821 RID: 10273 RVA: 0x000909C3 File Offset: 0x0008EBC3
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06002822 RID: 10274 RVA: 0x000909CC File Offset: 0x0008EBCC
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(CommunicationFrameworkAmbigiousResultAttributeException))
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

		// Token: 0x06002823 RID: 10275 RVA: 0x00090A9C File Offset: 0x0008EC9C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("CommunicationFrameworkAmbigiousResultAttributeException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("CommunicationFrameworkAmbigiousResultAttributeException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.ClassName != null)
			{
				info.AddValue("CommunicationFrameworkAmbigiousResultAttributeException_ClassName", this.ClassName, typeof(string));
			}
		}

		// Token: 0x06002824 RID: 10276 RVA: 0x00090B1C File Offset: 0x0008ED1C
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "The type {0} is not a valid ECFExtendedResult type since it has more then a single property decorated with ECFResultAttribute", new object[] { (markupKind == PrivateInformationMarkupKind.None) ? ((this.ClassName != null) ? this.ClassName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.ClassName != null) ? this.ClassName.MarkIfInternal() : string.Empty) : ((this.ClassName != null) ? this.ClassName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)) });
		}

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x06002825 RID: 10277 RVA: 0x00090BA0 File Offset: 0x0008EDA0
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

		// Token: 0x06002826 RID: 10278 RVA: 0x00090BC0 File Offset: 0x0008EDC0
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "ClassName={0}", new object[] { (this.ClassName != null) ? this.ClassName.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "ClassName={0}", new object[] { (this.ClassName != null) ? this.ClassName.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "ClassName={0}", new object[] { (this.ClassName != null) ? this.ClassName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
		}

		// Token: 0x06002827 RID: 10279 RVA: 0x00090C9F File Offset: 0x0008EE9F
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06002828 RID: 10280 RVA: 0x00090CA8 File Offset: 0x0008EEA8
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06002829 RID: 10281 RVA: 0x00090CB1 File Offset: 0x0008EEB1
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x0600282A RID: 10282 RVA: 0x00090C9F File Offset: 0x0008EE9F
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x0600282B RID: 10283 RVA: 0x00090CBC File Offset: 0x0008EEBC
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

		// Token: 0x04000DEA RID: 3562
		private string creationMessage;

		// Token: 0x04000DEB RID: 3563
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x04000DEC RID: 3564
		private string m_className;
	}
}
