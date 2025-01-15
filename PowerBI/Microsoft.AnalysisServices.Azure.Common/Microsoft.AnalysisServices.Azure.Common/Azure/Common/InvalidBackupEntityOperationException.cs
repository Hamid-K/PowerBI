using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x020000D1 RID: 209
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class InvalidBackupEntityOperationException : StateManagerBaseException
	{
		// Token: 0x06000886 RID: 2182 RVA: 0x0001C7BC File Offset: 0x0001A9BC
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000887 RID: 2183 RVA: 0x0001C7C4 File Offset: 0x0001A9C4
		// (set) Token: 0x06000888 RID: 2184 RVA: 0x0001C7CC File Offset: 0x0001A9CC
		public string Operation
		{
			get
			{
				return this.m_operation;
			}
			protected set
			{
				this.m_operation = value;
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000889 RID: 2185 RVA: 0x0001C7D5 File Offset: 0x0001A9D5
		// (set) Token: 0x0600088A RID: 2186 RVA: 0x0001C7DD File Offset: 0x0001A9DD
		public string Entity
		{
			get
			{
				return this.m_entity;
			}
			protected set
			{
				this.m_entity = value;
			}
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x0001C7E6 File Offset: 0x0001A9E6
		public InvalidBackupEntityOperationException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x0001C7FF File Offset: 0x0001A9FF
		public InvalidBackupEntityOperationException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x0001C816 File Offset: 0x0001AA16
		public InvalidBackupEntityOperationException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x0001C834 File Offset: 0x0001AA34
		protected InvalidBackupEntityOperationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("InvalidBackupEntityOperationException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.Operation = (string)info.GetValue("InvalidBackupEntityOperationException_Operation", typeof(string));
			}
			catch (SerializationException)
			{
				this.Operation = null;
			}
			try
			{
				this.Entity = (string)info.GetValue("InvalidBackupEntityOperationException_Entity", typeof(string));
			}
			catch (SerializationException)
			{
				this.Entity = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("InvalidBackupEntityOperationException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x0001C944 File Offset: 0x0001AB44
		public InvalidBackupEntityOperationException(string operation, string entity)
		{
			this.Operation = operation;
			this.Entity = entity;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x0001C961 File Offset: 0x0001AB61
		public InvalidBackupEntityOperationException(string operation, string entity, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.Operation = operation;
			this.Entity = entity;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x0001C986 File Offset: 0x0001AB86
		public InvalidBackupEntityOperationException(string operation, string entity, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.Operation = operation;
			this.Entity = entity;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x0001C9B4 File Offset: 0x0001ABB4
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x0001C9EB File Offset: 0x0001ABEB
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x0001C9F4 File Offset: 0x0001ABF4
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(InvalidBackupEntityOperationException))
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

		// Token: 0x06000895 RID: 2197 RVA: 0x0001CAC4 File Offset: 0x0001ACC4
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("InvalidBackupEntityOperationException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("InvalidBackupEntityOperationException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.Operation != null)
			{
				info.AddValue("InvalidBackupEntityOperationException_Operation", this.Operation, typeof(string));
			}
			if (this.Entity != null)
			{
				info.AddValue("InvalidBackupEntityOperationException_Entity", this.Entity, typeof(string));
			}
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x0001CB68 File Offset: 0x0001AD68
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Operation type {0} is not supported on BackupEntity '{1}'", (markupKind == PrivateInformationMarkupKind.None) ? ((this.Operation != null) ? this.Operation.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.Operation != null) ? this.Operation.MarkIfInternal() : string.Empty) : ((this.Operation != null) ? this.Operation.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)), (markupKind == PrivateInformationMarkupKind.None) ? ((this.Entity != null) ? this.Entity.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.Entity != null) ? this.Entity.MarkIfInternal() : string.Empty) : ((this.Entity != null) ? this.Entity.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000897 RID: 2199 RVA: 0x0001CC42 File Offset: 0x0001AE42
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

		// Token: 0x06000898 RID: 2200 RVA: 0x0001CC60 File Offset: 0x0001AE60
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Operation={0}", (this.Operation != null) ? this.Operation.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Operation={0}", (this.Operation != null) ? this.Operation.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "Operation={0}", (this.Operation != null) ? this.Operation.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Entity={0}", (this.Entity != null) ? this.Entity.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Entity={0}", (this.Entity != null) ? this.Entity.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "Entity={0}", (this.Entity != null) ? this.Entity.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x0001CDCC File Offset: 0x0001AFCC
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x0001CDD5 File Offset: 0x0001AFD5
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x0001CDDE File Offset: 0x0001AFDE
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x0001CDCC File Offset: 0x0001AFCC
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x0001CDE8 File Offset: 0x0001AFE8
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

		// Token: 0x04000288 RID: 648
		private string creationMessage;

		// Token: 0x04000289 RID: 649
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x0400028A RID: 650
		private string m_operation;

		// Token: 0x0400028B RID: 651
		private string m_entity;
	}
}
