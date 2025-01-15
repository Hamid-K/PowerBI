using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x020000D2 RID: 210
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class PersistOperationFailedException : StateManagerBaseException
	{
		// Token: 0x0600089E RID: 2206 RVA: 0x0001CFD4 File Offset: 0x0001B1D4
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x0600089F RID: 2207 RVA: 0x0001CFDC File Offset: 0x0001B1DC
		// (set) Token: 0x060008A0 RID: 2208 RVA: 0x0001CFE4 File Offset: 0x0001B1E4
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

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060008A1 RID: 2209 RVA: 0x0001CFED File Offset: 0x0001B1ED
		// (set) Token: 0x060008A2 RID: 2210 RVA: 0x0001CFF5 File Offset: 0x0001B1F5
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

		// Token: 0x060008A3 RID: 2211 RVA: 0x0001CFFE File Offset: 0x0001B1FE
		public PersistOperationFailedException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x0001D017 File Offset: 0x0001B217
		public PersistOperationFailedException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x0001D02E File Offset: 0x0001B22E
		public PersistOperationFailedException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x0001D04C File Offset: 0x0001B24C
		protected PersistOperationFailedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("PersistOperationFailedException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.Operation = (string)info.GetValue("PersistOperationFailedException_Operation", typeof(string));
			}
			catch (SerializationException)
			{
				this.Operation = null;
			}
			try
			{
				this.Entity = (string)info.GetValue("PersistOperationFailedException_Entity", typeof(string));
			}
			catch (SerializationException)
			{
				this.Entity = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("PersistOperationFailedException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x0001D15C File Offset: 0x0001B35C
		public PersistOperationFailedException(string operation, string entity)
		{
			this.Operation = operation;
			this.Entity = entity;
			this.ConstructorInternal(false);
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x0001D179 File Offset: 0x0001B379
		public PersistOperationFailedException(string operation, string entity, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.Operation = operation;
			this.Entity = entity;
			this.ConstructorInternal(false);
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x0001D19E File Offset: 0x0001B39E
		public PersistOperationFailedException(string operation, string entity, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.Operation = operation;
			this.Entity = entity;
			this.ConstructorInternal(false);
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x0001D1CC File Offset: 0x0001B3CC
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x0001D203 File Offset: 0x0001B403
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x0001D20C File Offset: 0x0001B40C
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(PersistOperationFailedException))
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

		// Token: 0x060008AD RID: 2221 RVA: 0x0001D2DC File Offset: 0x0001B4DC
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("PersistOperationFailedException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("PersistOperationFailedException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.Operation != null)
			{
				info.AddValue("PersistOperationFailedException_Operation", this.Operation, typeof(string));
			}
			if (this.Entity != null)
			{
				info.AddValue("PersistOperationFailedException_Entity", this.Entity, typeof(string));
			}
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x0001D380 File Offset: 0x0001B580
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Entity '{0}' Operation type {1} failed to be persisted in Azure SQL.", (markupKind == PrivateInformationMarkupKind.None) ? ((this.Entity != null) ? this.Entity.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.Entity != null) ? this.Entity.MarkIfInternal() : string.Empty) : ((this.Entity != null) ? this.Entity.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)), (markupKind == PrivateInformationMarkupKind.None) ? ((this.Operation != null) ? this.Operation.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.Operation != null) ? this.Operation.MarkIfInternal() : string.Empty) : ((this.Operation != null) ? this.Operation.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060008AF RID: 2223 RVA: 0x0001D45A File Offset: 0x0001B65A
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

		// Token: 0x060008B0 RID: 2224 RVA: 0x0001D478 File Offset: 0x0001B678
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Operation={0}", (this.Operation != null) ? this.Operation.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Operation={0}", (this.Operation != null) ? this.Operation.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "Operation={0}", (this.Operation != null) ? this.Operation.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Entity={0}", (this.Entity != null) ? this.Entity.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Entity={0}", (this.Entity != null) ? this.Entity.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "Entity={0}", (this.Entity != null) ? this.Entity.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x0001D5E4 File Offset: 0x0001B7E4
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x0001D5ED File Offset: 0x0001B7ED
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x0001D5F6 File Offset: 0x0001B7F6
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x0001D5E4 File Offset: 0x0001B7E4
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x0001D600 File Offset: 0x0001B800
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

		// Token: 0x0400028C RID: 652
		private string creationMessage;

		// Token: 0x0400028D RID: 653
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x0400028E RID: 654
		private string m_operation;

		// Token: 0x0400028F RID: 655
		private string m_entity;
	}
}
